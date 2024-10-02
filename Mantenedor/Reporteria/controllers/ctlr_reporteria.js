var ctlr_reporteria = new ctlr_reporteria();
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

function ctlr_reporteria() {
    this.Init = function () {

        $("#cboResultadoLlamada").select2({ placeholder: 'Cargando...' });
        $("#cboMotivoLlamada").select2({ placeholder: 'Cargando...' }); $("#cboMotivoLlamada").attr('disabled', 'disabled');
        $("#cboResultadoCampana").select2({ placeholder: 'Cargando...' }); $("#cboResultadoCampana").attr('disabled', 'disabled');
        $("#cboMotivoCampana").select2({ placeholder: 'Cargando...' }); $("#cboMotivoCampana").attr('disabled', 'disabled');

        $('#txtDtCierre').daterangepicker({
            autoUpdateInput: false,
            format: "DD/MM/YYYY",
            //"singleDatePicker": true,
            locale: {
                "separator": " - ",
                "applyLabel": "Aplicar",
                "cancelLabel": "Cancelar",
                "fromLabel": "Desde",
                "toLabel": "Hasta",
                "customRangeLabel": "Personalizado",
                "daysOfWeek": [
                    "Dom",
                    "Lun",
                    "Mar",
                    "Mie",
                    "Jue",
                    "Vie",
                    "Sáb"
                ],
                "monthNames": [
                    "Enero",
                    "Febrero",
                    "Marzo",
                    "Abril",
                    "Mayo",
                    "Junio",
                    "Julio",
                    "Agosto",
                    "Septiembre",
                    "Octubre",
                    "Noviembre",
                    "Diciembre"
                ],
                "firstDay": 1
            }
        });

        $('#txtDtCierre').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD/MM/YYYY') + ' - ' + picker.endDate.format('DD/MM/YYYY'));
        });

        $('#txtDtCierre').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
        });
    }
    this.Filtro = function Filtro() {
        var filtro = {};
        filtro.CodigoServicio = '';
        filtro.Segmento = '';
        filtro.Fecha = '';
        filtro.ResultadoLlamada = '';
        filtro.MotivoLlamada = '';
        filtro.ResultadoCampana = '';
        filtro.MotivoCampana = '';
        return filtro;
    }
    this.Actualizar = function Actualizar() {
        var filtro = this.Filtro();
        this.Cargar(filtro);
    }

    this.SetListeners = function () {
 
        $("#btnFiltrar").click(function () {
            var filtro = ctlr_reporteria.Filtro();

            filtro.CodigoServicio = $("#cboCampanaBusqueda").val();
            filtro.Segmento = $("#cboSegmentoBusqueda").val();
            filtro.Fecha = $("#txtDtCierre").val();
            filtro.ResultadoLlamada = $("#cboResultadoLlamada").val();
            filtro.MotivoLlamada = $("#cboMotivoLlamada").val();
            filtro.ResultadoCampana = $("#cboResultadoCampana").val();
            filtro.MotivoCampana = $("#cboMotivoCampana").val();

            if (filtro.CodigoServicio == "0") {
                swal.fire('Error', "Debe seleccionar una campaña", 'error');
                return;
            }

            if (filtro.Segmento == "0") {
                swal.fire('Error', "Debe seleccionar un segmento", 'error');
                return;
            }

            if (filtro.Fecha == "") {
                swal.fire('Error', "Debe seleccionar un rango de fechas", 'error');
                return;
            }

            if (filtro.ResultadoLlamada == "0")
                filtro.ResultadoLlamada = "";

            if (filtro.MotivoLlamada == "0")
                filtro.MotivoLlamada = "";

            if (filtro.ResultadoCampana == "0")
                filtro.ResultadoCampana = "";

            if (filtro.MotivoCampana == "0")
                filtro.MotivoCampana = "";
            
            ctlr_reporteria.Cargar(filtro);
        });

        $("#cboResultadoLlamada").change(function () {
            var resultadoLlamada = $(this).val();

            if (resultadoLlamada == 'Seleccione ...') {
                ctlr_util.ResetCombo("#cboMotivoLlamada"); $("#cboMotivoLlamada").attr('disabled', 'disabled');
                ctlr_util.ResetCombo("#cboResultadoCampana"); $("#cboCampana").attr('disabled', 'disabled');
                ctlr_util.ResetCombo("#cboMotivoCampana"); $("#cboMotivoCampana").attr('disabled', 'disabled');
            } else if (resultadoLlamada == 'Contactado') {
                ctrl_tipif.CargaResultadoCampana();
            } else {
                ctrl_tipif.CargaMotivosLlamada();
            }
        });

        $("#cboCampanaBusqueda").change(function () {
            $("#cboResultadoLlamada").val("0").trigger('change');
            ctlr_util.ResetCombo("#cboMotivoLlamada"); $("#cboMotivoLlamada").attr('disabled', 'disabled');
            ctlr_util.ResetCombo("#cboResultadoCampana"); $("#cboResultadoCampana").attr('disabled', 'disabled');
            ctlr_util.ResetCombo("#cboMotivoCampana"); $("#cboMotivoCampana").attr('disabled', 'disabled');

            ctlr_reporteria.CargarTipificaciones();
        });

        $("#cboMotivoLlamada").change(function () {
            //ctrl_tipif.VerificaReprogramacion();
        });

        $("#cboResultadoCampana").change(function () {
            var resultadoCampana = $(this).val();

            if (resultadoCampana == '0') {
                ctlr_util.ResetCombo("#cboMotivoResultadoCampana"); $("#cboMotivoResultadoCampana").attr('disabled', 'disabled');
            } else {
                ctrl_tipif.CargaMotivosCampana();
            }
        });

        $("#btnExportar").click(function () {
            ctlr_reporteria.Exportar();
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
        try {
            $('#tblllamadas').DataTable().destroy();
        } catch (ex) {

        }
        try {
            $("#tblllamadas tbody").html("");
            var table = $('#tblllamadas').DataTable({
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
                    "url": "../TableService.asmx/CargarReporteria",
                    "type": "POST",
                    "data": {
                        'CodigoServicio': filtro.CodigoServicio,
                        'Segmento': filtro.Segmento,
                        'Fecha': filtro.Fecha,
                        'ResultadoLlamada': filtro.ResultadoLlamada,
                        'MotivoLlamada': filtro.MotivoLlamada,
                        'ResultadoCampana': filtro.ResultadoCampana,
                        'MotivoCampana': filtro.MotivoCampana,
                    }
                },
                "columnDefs": [
                    { "targets": 0, "data": 'NUMERO_LLAMADA' },
                    { "targets": 1, "data": 'RESULTADO_LLAMADO' },
                    { "targets": 2, "data": 'MOTIVO_LLAMADO' },
                    { "targets": 3, "data": 'RESULTADO_CAMPANA' },
                    { "targets": 4, "data": 'MOTIVO_CAMPANA' },
                    { "targets": 5, "data": 'NOMBRE_AGENTE' },
                ],
                "drawCallback": function () {
                }
            });
        } catch (ex) {
            console.log(ex.message);
        }
    }
    this.CargarTipificaciones = function CargarTipificaciones() {
        return new Promise(function (resolve) {

            var CodigoServicio = $("#cboCampanaBusqueda").val();

            if (CodigoServicio != "0") {
                var values = JSON.stringify({ CodigoServicio: CodigoServicio });

                $.ajax({
                    url: "Main.aspx/CargarTipificaciones",
                    data: values,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                    },
                    success: function (response) {
                        var r = response.d;

                        if (r.ret == 'OK') {
                            var tip = r.values[0];

                            sessionStorage.setItem("tip", JSON.stringify(tip));

                        } else {
                            resolve('ERROR');
                        }
                    },
                    error: function (response) {
                        resolve('ERROR');
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
    this.Exportar = function Exportar() {

        var filtro = this.Filtro();

        filtro.CodigoServicio = $("#cboCampanaBusqueda").val();
        filtro.Segmento = $("#cboSegmentoBusqueda").val();
        filtro.Fecha = $("#txtDtCierre").val();
        filtro.ResultadoLlamada = $("#cboResultadoLlamada").val();
        filtro.MotivoLlamada = $("#cboMotivoLlamada").val();
        filtro.ResultadoCampana = $("#cboResultadoCampana").val();
        filtro.MotivoCampana = $("#cboMotivoCampana").val();

        if (filtro.ResultadoLlamada == "0")
            filtro.ResultadoLlamada = "";

        if (filtro.MotivoLlamada == "0")
            filtro.MotivoLlamada = "";

        if (filtro.ResultadoCampana == "0")
            filtro.ResultadoCampana = "";

        if (filtro.MotivoCampana == "0")
            filtro.MotivoCampana = "";

        var values = JSON.stringify({ filtro: filtro});
        var encodedString = btoa(values);

        var url = "../ExportService.asmx/Reporteria?data=" + encodedString;

        var win = window.open(url, '_blank');
    }
}