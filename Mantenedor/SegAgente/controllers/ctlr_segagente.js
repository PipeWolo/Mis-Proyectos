var ctlr_segagente = new ctlr_segagente();
var valorValidacion = '';
var pestanaActual = "datos";
var jsonArchivoTipif = [];
var camposActual = [];
var registrosTipifOK = [];

var idioma_espanol = {
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "No se encontraron registros.",
    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sUrl": "",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
}

function ctlr_segagente() {
    this.Init = function () {

    }
    this.Filtro = function Filtro() {
        var filtro = {};
        filtro.Agente = '';
        filtro.CodigoServicio = '';
        filtro.Segmento = '';
        return filtro;
    }
    this.Actualizar = function Actualizar() {
        var filtro = this.Filtro();
        this.Cargar(filtro);
    }

    this.SetListeners = function () {
        $('#nuevo').on('shown.bs.modal', function () {
            $('.modal-backdrop').remove();
            if (!$("body").hasClass('sidebar-mini')) {
                $("a[data-toggle='sidebar']").trigger('click');
            }
        });

        $('#mdlCargaMasiva').on('shown.bs.modal', function () {
            $('.modal-backdrop').remove();
            if (!$("body").hasClass('sidebar-mini')) {
                $("a[data-toggle='sidebar']").trigger('click');
            }
        });

        $('#mdlCargaMasiva').on('hide.bs.modal', function () {
            $("body").removeClass('modal-open');
        });

        $("#btnReestablecer").click(function () {
            
            $("#txtAgenteBuscar").val('');
            $("#txtCodigoServicioBuscar").val('');
            $("#cboSegmentoBusqueda").val('0');            
            ctlr_segagente.Actualizar();
        });

        $("#btnFiltrar").click(function () {
            var filtro = ctlr_segagente.Filtro();

            filtro.Agente = $("#txtAgenteBuscar").val();
            filtro.CodigoServicio = $("#txtCodigoServicioBuscar").val();
            filtro.Segmento = $("#cboSegmentoBusqueda").val();

            if (filtro.Segmento == "0")
                filtro.Segmento = "";
            
            ctlr_segagente.Cargar(filtro);
        });

        $("#btnNuevo").click(function () {
            ctlr_segagente.Editar(0);
        });

        $("#btnExportar").click(function () {
            ctlr_segagente.Exportar();
        });

        $("#btnCargaMasiva").click(function () {
            $(".fileinput").fileinput('clear');
            if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                $("#tablaResumenTipif").DataTable().destroy();
            }

            $("#tablaResumenTipif tbody").html('<tr><td colspan="4" style="text-align:center;">Suba archivo para validar datos</td></tr>');
            $("#mdlCargaMasiva").modal('show');
        });

        $("#txtDnis").keyup(function () {
            var obj = document.getElementById("txtDnis");
            ctlr_util.CharsRestantes(obj, 255);
            $("#txtDnis").focus();
        });

        $("#txtSkill").keyup(function () {
            var obj = document.getElementById("txtSkill");
            ctlr_util.CharsRestantes(obj, 255);
            $("#txtSkill").focus();
        });

        $("#txtServicio").keyup(function () {
            var obj = document.getElementById("txtServicio");
            ctlr_util.CharsRestantes(obj, 255);
            $("#txtServicio").focus();
        });

        $("#txtPrefijo").keyup(function () {
            var obj = document.getElementById("txtPrefijo");
            ctlr_util.CharsRestantes(obj, 255);
            $("#txtPrefijo").focus();
        });

        $("#txtAgente").blur(function () {
            var rut = $("#txtAgente").val();
            ctlr_segagente.NombreAgente(rut);
        });

        $("#btnGrabar").click(function () {
            ctlr_segagente.Grabar();
        });

        $("#cboCampana").change(function () {
            var camp = $("#cboCampana").val();

            if (camp != "0") {
                $("#txtCodigoServicio").val(camp);
            } else {
                $("#txtCodigoServicio").val("");
            }
        });

        $('#tblservicios tbody').on('click', '.row-edit-button', function (e) {
            e.preventDefault();
            if ($.fn.dataTable.isDataTable('#tblservicios')) {
                var table = $('#tblservicios').DataTable();
                var data = table.row($(this).parents('tr')).data();
                var id = data.Id;
                ctlr_segagente.Editar(id);
            }
        });

        $('#tblservicios tbody').on('click', '.row-delete-button', function (e) {
            e.preventDefault();
            if ($.fn.dataTable.isDataTable('#tblservicios')) {
                var table = $('#tblservicios').DataTable();
                var data = table.row($(this).parents('tr')).data();
                var id = data.Id;
                ctlr_segagente.Eliminar(id);
            }
        });

        $("#btnDescargaPlantillaTipif").click(function () {
            ctlr_segagente.DescargarPlantilla();
        });

        $("#fileUploadTipif").fileupload({
            add: function (e, data) {
                data.submit();
            },
            beforeSend: function (e, data) {
                $.busyLoadFull("show", { text: 'Procesando el Archivo' });
                var regexp = /\.(xls)|(xlsx)$/i;

                if (!regexp.test(data.files[0].name)) {
                    try {
                        $.busyLoadFull("hide");
                        swal("Error", "¡Formato de archivo no permitido!", "error");
                    }
                    catch (err) {
                        $.busyLoadFull("hide");
                        alert("Formato de archivo no permitido");
                    }
                    $(".fileinput").fileinput('clear');
                    return false;
                }
            },
            done: function (e, data) {
                var excelRows;
                var files = data.files;
                var i, f;
                for (i = 0, f = files[i]; i != files.length; ++i) {
                    var reader = new FileReader();
                    var name = f.name;
                    reader.onload = function (e) {
                        var data = e.target.result;
                        data = new Uint8Array(data);
                        var workbook = XLSX.read(data, { type: 'array', cellDates: true, dateNF: 'yyyy/mm/dd;@' });
                        var firstSheet = workbook.SheetNames[0];
                        excelRows = XLSX.utils.sheet_to_json(workbook.Sheets[firstSheet], { raw: true, defval: '' });
                        jsonArchivoTipif = excelRows;
                        $.busyLoadFull("hide");
                        ctlr_segagente.ValidarExcelTipif();
                    };
                    reader.readAsArrayBuffer(f);
                }
            }
        });

        $("#btnConfirmarArchTipif").click(function () {
            ctlr_segagente.SubirCargaTipificaciones();
        });
    }
    this.Editar = function (id) {

        $("#hid").val(id);

        var values = JSON.stringify({ id: id })

        if (id != "0") {
            $.ajax({
                url: "Main.aspx/Cargar",
                data: values,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    $("#loading").show();
                },
                success: function (response) {
                    var r = response.d;

                    $("#loading").hide();

                    if (r.ret == 'OK') {

                        var age = response.d.values[0];

                        if (age != null) {
                            $("#hid").val(age.Id);
                            $("#txtAgente").val(age.Agente);
                            $("#txtNombreAgente").val(age.NombreAgente);
                            $("#cboSegmento").val(age.Segmento);
                            $("#cboCampana").val(age.CodigoServicio).trigger('change');
                            $("#txtCodigoServicio").val(age.CodigoServicio);
                        } else {
                            $("#hid").val('');
                            ctlr_segagente.LimpiarCampos();
                        }
                        $("#nuevo").modal('show');
                    } else {
                        $("#loading").hide();
                        swal.fire('Error', r.msg, 'error');
                    }
                },
                error: function (response) {
                    $("#loading").hide();
                    swal.fire('Error', "ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText, 'error');
                }
            });
        } else {
            $("#hid").val('0');
            ctlr_segagente.LimpiarCampos();
            $("#nuevo").modal('show');
        }
    }
    this.CargaCombo = function () {

        var values = "";

        $.ajax({
            url: "Main.aspx/CargaCombo",
            data: values,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (response) {
                var r = response.d;

                $("#loading").hide();

                if (r.ret == 'OK') {

                    var age = response.d.values[0];
                    var info = response.d.values[1];

                    sessionStorage.setItem("InfoAgentes", JSON.stringify(info));

                    if (age != null) {
                        html = '<option value="0">Seleccione...</option>';
                        $.each(age, function (index, item) {
                            html += '<option value="' + item.KeyValue + '">' + item.KeyName + '</option>';
                        });

                        $("#cboCampana").html("");
                        $("#cboCampana").append(html);
                    }
                } else {
                    $("#loading").hide();
                    swal.fire('Error', r.msg, 'error');
                }
            },
            error: function (response) {
                $("#loading").hide();
                swal.fire('Error', "ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText, 'error');
            }
        });
    }
    this.NombreAgente = function (rut) {

        var values = JSON.stringify({ rut: rut })

        $.ajax({
            url: "Main.aspx/NombreAgente",
            data: values,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (response) {
                var r = response.d;

                $("#loading").hide();

                if (r.ret == 'OK') {
                    var age = response.d.values[0];
                    $("#txtNombreAgente").val(age.KeyName);
                } else {
                    $("#loading").hide();
                    swal.fire('Error', r.msg, 'error');
                }
            },
            error: function (response) {
                $("#loading").hide();
                swal.fire('Error', "ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText, 'error');
            }
        });
    }
    this.Cargar = function Cargar(filtro) {
        try {
            $('#tblservicios').DataTable().destroy();
        } catch (ex) {

        }
        try {
            $("#tblservicios tbody").html("");
            var table = $('#tblservicios').DataTable({
                "language": idioma_espanol,
                "pageLength": 10,
                responsive: true,
                scrollX: false,
                scrollCollapse: false,
                "destroy": true,
                "searching": false,
                language: {
                    url: '../assets/plugins/datatables/Spanish.json'
                },
                "processing": true,
                "serverSide": true,
                "ajax": {
                    "url": "../TableService.asmx/CargarSegmentoxAgente",
                    "type": "POST",
                    "data": {
                        'Agente': filtro.Agente,
                        'CodigoServicio': filtro.CodigoServicio,
                        'Segmento': filtro.Segmento
                    }
                },
                "columnDefs": [
                    {
                        "targets": 0,
                        "data": null,
                        "width": "50px",
                        "render": function (data, type, row, meta) {

                            var click = "<a href='#' class='row-edit-button'><i class='fas fa-pencil-alt'></i></a>";
                            var eliminar = "<a href='#' class='row-delete-button'><i class='fa fa-trash'></i></a>";

                            var html = click + "&nbsp;&nbsp;" + eliminar;
                            return html;
                        }
                    },
                    { "targets": 1, "data": 'Agente' },
                    { "targets": 2, "data": 'NombreAgente' },
                    { "targets": 3, "data": 'Segmento' },
                    { "targets": 4, "data": 'NombreCampana' },
                    { "targets": 5, "data": 'CodigoServicio' },
                ],
                "drawCallback": function () {
                }
            });
        } catch (ex) {
            console.log(ex.message);
        }
    }
    this.Eliminar = function Eliminar(id) {

        var title = '¿Está seguro de que desea eliminar al Agente de la Campaña?';
        var values = JSON.stringify({ id: id });

        swal.fire({
            title: title,
            icon: 'info',
            html: "",
            showCancelButton: true,
            confirmButtonText: 'Si',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    url: "Main.aspx/Eliminar",
                    data: values,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        var r = response.d;

                        if (r.ret == 'OK') {
                            swal.fire('Éxito', "Agente eliminado con éxito", 'success');
                            ctlr_segagente.Actualizar();
                        } else {
                            swal.fire("Error", r.msg, 'error');
                        }
                    },
                    error: function (response) {
                        swal.fire('Error', "ERROR " + response.status + ' ' + response.statusText, 'error');
                    }
                });
            }
        });
    }
    this.Grabar = function Grabar() {

        var SegmentoxAgente = {};
        SegmentoxAgente.Id = $("#hid").val();
        SegmentoxAgente.Agente = $("#txtAgente").val();
        SegmentoxAgente.CodigoServicio = $("#txtCodigoServicio").val();
        SegmentoxAgente.Segmento = $("#cboSegmento").val();
        SegmentoxAgente.NombreCampana = ctlr_util.GetComboText("#cboCampana");
        var nombreAgente = $("#txtNombreAgente").val();

        if (nombreAgente == "NO REGISTRADO") {
            swal.fire('Error de validacion', "Debe ingresar un Agente registrado como Usuario del Sistema", 'warning');
            return false;
        }

        if (SegmentoxAgente.Agente == '') {
            swal.fire('Error de validacion', "Debe ingresar Agente", 'warning');
            return false;
        } else if (SegmentoxAgente.Segmento == '0') {
            swal.fire('Error de validacion', "Debe seleccionar Segmento", 'warning');
            return false;
        } else if (SegmentoxAgente.CodigoServicio == '') {
            swal.fire('Error de validacion', "Debe seleccionar Campaña", 'warning');
            return false;
        }

        swal.fire({
            title: '¿Esta seguro que desea asociar el Agente a la Campaña?',
            icon: 'info',
            html: "Esta acción no se puede deshacer.",
            showCancelButton: true,
            confirmButtonText: 'Si',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                var values = JSON.stringify({ SegmentoXAgente: SegmentoxAgente })

                $.ajax({
                    url: "Main.aspx/Grabar",
                    data: values,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        $("#loading").show();
                    },
                    success: function (response) {
                        var r = response.d;

                        $("#loading").hide();

                        if (r.ret == 'OK') {
                            swal.fire('Exito', "Operación exitosa", 'success');
                            $("#nuevo").modal("hide");
                            ctlr_segagente.LimpiarCampos();
                            ctlr_segagente.Actualizar();
                        } else {
                            $("#loading").hide();
                            swal.fire('Error', r.msg, 'error');
                        }
                    },
                    error: function (response) {
                        $("#loading").hide();
                        swal.fire('Error', "ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText, 'error');
                    }
                });        
            }
        });
    }
    this.LimpiarCampos = function () {
        $("#txtAgente").val('');
        $("#txtNombreAgente").val('');
        $("#txtCodigoServicio").val('');
        $("#cboCampana").val('0');
        $("#cboSegmento").val('0');
    }
    this.Exportar = function Exportar() {

        var filtro = this.Filtro();

        filtro.Skill = $("#txtSkillBuscar").val();
        filtro.Servicio = $("#txtServicioBuscar").val();
        filtro.Prefijo = $("#txtPrefijoBuscar").val();

        var values = JSON.stringify({ filtro: filtro });
        var encodedString = btoa(values);

        var url = "../ExportService.asmx/Servicios?data=" + encodedString;

        var win = window.open(url, '_blank');
    }
    this.ValidarExcelTipif = function () {
        var resumen = [];
        var msg = "";
        registrosTipifOK = [];
        var ID_SERVICIO = $("#hidId").val();
        var tip = JSON.parse(sessionStorage.getItem("InfoAgentes"));

        if (jsonArchivoTipif != null) {
            if (jsonArchivoTipif.length > 0) {
                var columnaValida = true;
                try {

                    var primeraFila = jsonArchivoTipif[0];
                    if (primeraFila.RUT == null) {
                        msg += "No existe columna RUT en el archivo. ";
                        columnaValida = false;
                    }
                    if (primeraFila.CAMPANA == null) {
                        msg += "No existe columna CAMPANA en el archivo. ";
                        columnaValida = false;
                    }
                    if (primeraFila.SEGMENTO == null) {
                        msg += "No existe columna SEGMENTO en el archivo. ";
                        columnaValida = false;
                    }
                }
                catch (e) {
                    console.error(e);
                }

                try {
                    if (columnaValida) {
                        $.each(jsonArchivoTipif, function (indexJson, fila) {
                            var error = false;
                            var tipif = new Tipificacion();

                            var tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                            if (fila.RUT != null && fila.RUT != "") {
                                if (fila.RUT.length > 8) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El largo del la columna RUT supera el máximo de 8 caracteres.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else {
                                    tipif.Agente = fila.RUT;
                                }
                            } else {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "No se ha ingresado valor para columna RUT.";
                                resumen.push(tmpResumen);
                                error = true;
                            }

                            tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                            if (fila.CAMPANA != null && fila.CAMPANA != "") {
                                if (fila.CAMPANA.length > 100) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El largo del la columna CAMPANA supera el máximo de 100 caracteres.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else {
                                    tipif.NombreCampana = fila.CAMPANA;
                                }
                            } else {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "No se ha ingresado valor para columna CAMPANA.";
                                resumen.push(tmpResumen);
                                error = true;
                            }

                            tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                            if (fila.SEGMENTO != null && fila.SEGMENTO != "") {
                                if (fila.SEGMENTO.length > 60) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El largo del la columna SEGMENTO supera el máximo de 60 caracteres.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else {
                                    tipif.Segmento = fila.SEGMENTO;
                                }
                            } else {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "No se ha ingresado valor para columna SEGMENTO.";
                                resumen.push(tmpResumen);
                                error = true;
                            }

                            var repetido = registrosTipifOK.filter(function (tipifRev) {
                                return tipifRev.Agente == tipif.Agente
                                    && tipifRev.NombreCampana == tipif.NombreCampana
                                    && tipifRev.Segmento == tipif.SEGMENTO;
                            });

                            var tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__;
                            if (repetido.length > 0) {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "Ya existe una fila con los mismos datos en la carga masiva.";
                                resumen.push(tmpResumen);
                                error = true;
                            }

                            var ExisteAgente = 0;
                            if (!error) {
                                var tmpResumen = new ValidacionCarga();
                                tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                                if (tip != null) {
                                    $.each(tip, function (i, c) {
                                        if (c.KeyValue == tipif.Agente)
                                            ExisteAgente = 1;
                                    });
                                }

                                if (ExisteAgente == 0) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El Agente " + tipif.Agente + " no existe en el sistema.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                }
                            }

                            if (!error) {
                                var tmpResumen = new ValidacionCarga();
                                tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                                var cont = 0;
                                var sel = document.getElementById("cboCampana");
                                for (var i = 0; i < sel.length; i++) {
                                    //  Aca haces referencia al "option" actual
                                    var opt = sel[i];

                                    if (tipif.NombreCampana == opt.label)
                                        cont = cont + 1;
                                }

                                if (cont == 0) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "La campaña " + tipif.NombreCampana + " no existe.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                }
                            }

                            if (!error) {
                                var tmpResumen = new ValidacionCarga();
                                tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                                var cont = 0;
                                var sel = document.getElementById("cboSegmentoBusqueda");
                                for (var i = 0; i < sel.length; i++) {
                                    //  Aca haces referencia al "option" actual
                                    var opt = sel[i];

                                    if (tipif.Segmento == opt.label)
                                        cont = cont + 1;
                                }

                                if (cont == 0) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El segmento " + tipif.Segmento + " no existe.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                }
                            }

                            if (!error) {
                                tipif.CODIGO_SERVICIO = $("#hidIdCS").val();
                                registrosTipifOK.push(tipif);
                            }
                        });
                        ctlr_segagente.CargaResumenTipif(resumen);

                        if (registrosTipifOK.length > 0) {
                            //$("#btnConfirmarArchTipif").removeAttr('disabled');
                            swal.fire({
                                title: "Informacion Validación",
                                text: "Registros Válidos: " + registrosTipifOK.length + ". Presione el botón Procesar, para subir los datos",
                                type: "info"
                            }).then(function (result) {
                                document.getElementById('btnConfirmarArchTipif').disabled = false;
                            });
                            
                        } else {
                            swal.fire({
                                title: "Informacion Validación",
                                text: "No se encontraron registros válidos.",
                                type: "info"
                            }).then(function (result) {
                                document.getElementById('btnConfirmarArchTipif').disabled = true;
                            });
                           
                            //$("#btnConfirmarArchTipif").attr('disabled', 'disabled');
                        }
                    } else {
                        swal({
                            title: "Validación",
                            text: msg,
                            type: "info"
                        }).then(function (result) {
                            $(".fileinput").fileinput('clear');
                        });
                    }
                }
                catch (e) {
                    console.error(e);
                }
            } else {
                swal.fire({
                    title: "Validación",
                    text: "El archivo está vacío.",
                    icon: "info"
                }).then(function (result) {
                    $(".fileinput").fileinput('clear');
                });
            }
        } else {
            swal.fire({
                title: "Error de validación",
                text: "El archivo no se pudo leer correctamente, favor inténtelo mas tarde.",
                icon: "error"
            }).then(function (result) {
                $(".fileinput").fileinput('clear');
            });
        }
    }
    this.CargaResumenTipif = function (resumen) {

        if (resumen && resumen.length > 0) {
            var html = "";
            $.each(resumen, function (idx, tmp) {
                html += "<tr>";
                html += "<td>" + tmp.N_LINEA + "</td>";
                html += "<td style='text-align:center;'>" + tmp.VALIDO + "</td>";
                html += "<td>" + tmp.MENSAJE + "</td>";
                html += "</tr>";
            });

            if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                $("#tablaResumenTipif").DataTable().destroy();
            }

            $("#tablaResumenTipif tbody").html(html);
            $("#tablaResumenTipif").DataTable({
                url: '../assets/plugins/datatables/Spanish.json',
                "searching": false,
            });

        }
        else {
            if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                $("#tablaResumenTipif").DataTable().destroy();
            }

            $("#tablaResumenTipif tbody").html('<tr><td colspan="4" style="text-align:center;">Todos los datos son válidos</td></tr>');
            $("#filtrarResumen").hide();
        }
    }
    this.QuitaErrorValidacion = function () {
        $(".error-validacion").remove();
    }

    this.MuestraErrorValidacion = function (input) {
        var mensaje = $(input).data("validacion");
        $(input).parent().append('<span class="text-danger error-validacion" style="display:none;">' + mensaje + '</span>');
        $(".error-validacion").fadeIn();
    }
    this.SubirCargaTipificaciones = function (btn) {

        ctlr_segagente.QuitaErrorValidacion();

        if (registrosTipifOK.length > 0) {
            swal.fire({
                title: "Confirmación",
                text: "¿Esta seguro que desea cargar los registros válidos? Estos se agregaran a los ya existentes.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: 'Guardar',
                cancelButtonText: 'Cancelar'
            }).then(function (isConfirm) {
                if (isConfirm && isConfirm.value) {
                    var values = JSON.stringify({ segxagente: registrosTipifOK });
                    $.ajax({
                        url: "Main.aspx/SubirCargaAgentexCampana",
                        data: values,
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                            $(btn).buttonLoader('start');
                        },
                        success: function (response) {
                            $(btn).buttonLoader('stop');
                            var r = response.d;
                            if (r.ret == 'OK') {
                                try {
                                    swal.fire({
                                        title: "Operación exitosa",
                                        text: r.msg,
                                        icon: "success"
                                    }).then(function (result) {
                                        $(".fileinput").fileinput('clear');
                                        if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                                            $("#tablaResumenTipif").DataTable().destroy();
                                        }

                                        $("#tablaResumenTipif tbody").html('<tr><td colspan="4" style="text-align:center;">Suba archivo para validar datos</td></tr>');
                                        $("#mdlCargaMasiva").modal("hide");
                                        ctlr_segagente.Actualizar();
                                    });
                                }
                                catch (err) {
                                    alert('Operación exitosa: ' + r.msg);
                                    $(".fileinput").fileinput('clear');
                                    if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                                        $("#tablaResumenTipif").DataTable().destroy();
                                    }

                                    $("#tablaResumenTipif tbody").html('<tr><td colspan="4" style="text-align:center;">Suba archivo para validar datos</td></tr>');
                                    $("#mdlCargaMasiva").modal("hide");
                                    ctlr_segagente.Actualizar();
                                }
                            } else {
                                try {
                                    swal.fire("Error", r.msg, "error");
                                }
                                catch (err) {
                                    alert("Error" + ': ' + r.msg);
                                }
                            }
                        },
                        error: function (response) {
                            $(btn).buttonLoader('stop');
                            try {
                                swal.fire("Error inesperado", 'Ocurrió un error inesperado, inténtelo más tarde.', "error");
                            }
                            catch (err) {
                                alert("Error inesperado" + ': ' + 'Ocurrió un error inesperado, inténtelo más tarde.');
                            }
                        }
                    });
                }
            });
        }

    }
    this.DescargarPlantilla = function () {

        var url = "../ExportService.asmx/DescargarPlantillaTipificaciones";

        var win = window.open(url, '_blank');
    }
}