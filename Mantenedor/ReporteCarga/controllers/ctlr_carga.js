var ctlr_carga = new ctlr_carga();
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

function ctlr_carga() {
    this.Init = function () {

    }
    this.Filtro = function Filtro() {
        var filtro = {};
        filtro.CodigoServicio = '';
        filtro.Segmento = '';
        return filtro;
    }
    this.Actualizar = function Actualizar() {
        var filtro = this.Filtro();
        this.Cargar(filtro);
    }

    this.SetListeners = function () {
 
        $("#btnFiltrar").click(function () {
            var filtro = ctlr_carga.Filtro();

            filtro.CodigoServicio = $("#cboCampanaBusqueda").val();
            filtro.Segmento = $("#cboSegmentoBusqueda").val();

            if (filtro.CodigoServicio == "0") {
                swal.fire('Error', "Debe seleccionar una campaña", 'error');
                return;
            }

            if (filtro.Segmento == "0") {
                swal.fire('Error', "Debe seleccionar un segmento", 'error');
                return;
            }
            
            ctlr_carga.Cargar(filtro);
        });

        $("#btnRepetidos").click(function () {
            var registro = $("#txtRepetidos").val();

            if (registro == "") {
                swal.fire('Advertencia', "No hay registros a descargar", 'info');
                return;
            }

            ctlr_carga.Exportar("Repetidos");
        });

        $("#btnErrores").click(function () {
            var registro = $("#txtErrores").val();

            if (registro == "") {
                swal.fire('Advertencia', "No hay registros a descargar", 'info');
                return;
            }
            ctlr_carga.Exportar("Errores");
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
                            $("#txtAgente").val('');
                            $("#txtNombreAgente").val('');
                            $("#cboSegmento").val('0');
                            $("#cboCampana").val('0');
                            $("#txtCodigoServicio").val('');
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
            $("#txtAgente").val('');
            $("#cboSegmento").val('0');
            $("#cboCampana").val('0');
            $("#txtCodigoServicio").val('');
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

                    if (age != null) {
                        html = '<option value="0">Seleccione...</option>';
                        $.each(age, function (index, item) {
                            html += '<option value="' + item.KeyValue + '">' + item.KeyName + '</option>';
                        });

                        $("#cboCampanaBusqueda").html("");
                        $("#cboCampanaBusqueda").append(html);
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
        var values = JSON.stringify({ filtro: filtro })

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
                        $("#txtFecha").val(age.Fecha);
                        $("#txtHora").val(age.Hora);
                        $("#txtCargados").val(age.QRecibidos);
                        $("#txtOk").val(age.QValidos);
                        $("#txtRepetidos").val(age.QRepetidos);
                        $("#txtErrores").val(age.QErroneos);
                        $("#txtGestionManual").val(age.QGestionManual);
                    } else {
                        $("#txtFecha").val("");
                        $("#txtHora").val("");
                        $("#txtCargados").val("");
                        $("#txtOk").val("");
                        $("#txtRepetidos").val("");
                        $("#txtErrores").val("");
                        $("#txtGestionManual").val("");

                        swal.fire('Advertencia', 'No hay registros cargados para la Campaña/Segmento seleccionada', 'info');
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
    this.Eliminar = function Eliminar(id) {

        var title = '¿Está seguro de que desea eliminar al Agente de la Camapaña?';
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
                            ctlr_carga.Actualizar();
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
            swal.fire('Error de validacion', "Debe ingresar un Agente registrado", 'warning');
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
                            ctlr_carga.Actualizar();
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
        $("#txtDnis").val('');
        $("#txtSkill").val('');
        $("#txtServicio").val('');
        $("#txtPrefijo").val('');
    }
    this.Exportar = function Exportar(tipo) {

        var filtroExcel = this.Filtro();

        var CodigoServicio = $("#cboCampanaBusqueda").val();
        var Segmento = $("#cboSegmentoBusqueda").val();

        var values = JSON.stringify({ CodigoServicio: CodigoServicio, Segmento: Segmento, tipo: tipo });
        var encodedString = btoa(values);

        var url = "../ExportService.asmx/ReporteCarga?CodigoServicio=" + CodigoServicio + "&Segmento=" + Segmento + "&tipo=" + tipo;

        var win = window.open(url, '_blank');
    }
}