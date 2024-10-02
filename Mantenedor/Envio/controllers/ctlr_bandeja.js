var ctlr_bandeja = new ctlr_bandeja();

function ctlr_bandeja() {


    this.Init = function () {



        moment.locale('es');

        $('#txtFechaBuscar').daterangepicker({
            autoUpdateInput: false,
            format: "DD-MM-YYYY",
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

        $('#txtFechaBuscar').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD-MM-YYYY') + ' - ' + picker.endDate.format('DD-MM-YYYY'));
        });

        $('#txtFechaBuscar').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
        });

        $('#txtFechaBuscar').daterangepicker({
            autoUpdateInput: false,
            format: "DD-MM-YYYY",
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

        $('#txtFechaBuscar').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD-MM-YYYY') + ' - ' + picker.endDate.format('DD-MM-YYYY'));
        });

        $('#txtFechaBuscar').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
        });

        $('input[name="daterange2"]').daterangepicker({
            opens: 'right',
            singleDatePicker: true,
            locale: {
                format: 'DD/MM/YYYY',
                applyLabel: 'Aplicar',
                cancelLabel: 'Limpiar',
                fromLabel: 'Del',
                toLabel: 'al',
                customRangeLabel: 'Personalizado',
                daysOfWeek: ['Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sáb', 'Dom'],
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                firstDay: 1,
                setDate: null,
            }
        }, function (start, end, label) {
        });

        var date = new Date();
        date.setDate(date.getDate());
        $("#txtFechaReagenda").datetimepicker({
            format: 'dd/mm/yyyy HH:ii',
            startDate: date,
            autoclose: true,
            language: 'es'
        });
    }

    this.SetListeners = function () {
        $("#btnCancelar").click(function () {
            $("#txtANI").val('');
            $("#txtNombreCliente").val('');
        });

        $("#btnGrabar").click(function () {
            ctlr_bandeja.Grabar(this);
        });
    }

    this.Grabar = function (btn) {

        ctlr_bandeja.QuitaErrorValidacion();

        var isValid = true;

        var llamada = new SMS();

        isValid = ctlr_bandeja.ValidarSMS(llamada);

        if (isValid) {
            swal.fire({
                title: "Confirmación",
                text: "¿Desea enviar el SMS?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: 'Enviar',
                cancelButtonText: 'Cancelar'
            }).then(function (isConfirm) {

                if (isConfirm) {

                    var values = JSON.stringify({ llamada: llamada });

                    $.ajax({
                        url: "Main.aspx/EnvioSMS",
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
                                var msjok = "Se ha generado el SMS {id_solicitud}"
                                var solicitudid = r.values[0];
                                if (solicitudid && solicitudid.KeyName) {
                                    msjok = msjok.replace('{id_solicitud}', solicitudid.KeyName);
                                }
                                try {
                                    swal.fire({
                                        title: "Operación exitosa",
                                        text: msjok,
                                        icon: "success"
                                    }).then(function (result) {
                                        window.close();
                                        ctlr_bandeja.CreaRequerimiento();
                                    });
                                }
                                catch (err) {
                                    alert('Operación exitosa: ' + msjok);
                                    ctlr_bandeja.CreaRequerimiento();
                                }
                            } else {
                                try {
                                    swal("Error al guardar", r.msg, "error");
                                }
                                catch (err) {
                                    alert("Error alguardar: " + r.msg);
                                }
                            }
                        },
                        error: function (response) {
                            $(btn).buttonLoader('stop');
                            try {
                                swal("Error al guardar", 'Ocurrió un error inesperado, inténtelo más tarde.', "error");
                            }
                            catch (err) {
                                alert("Error al guardar: " + 'Ocurrió un error inesperado, inténtelo más tarde.');
                            }
                        }
                    });
                }
            })
        }
    }

    this.CreaRequerimiento = function () {

        var usuario = sessionStorage.getItem('userid');
        var nombreUsuario = sessionStorage.getItem('nombre');

        var values = JSON.stringify({ usuario: usuario, nombreUsuario: nombreUsuario });

        $.ajax({
            url: "Main.aspx/CreaRequerimiento",
            data: values,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                
            },
            success: function (response) {
                
                var r = response.d;
                if (r.ret == 'OK') {
                    var data = r.values[0];

                    $("#ctl00_body_txtIDRegistro").val(data.KeyName);
                    $("#ctl00_body_txtFechaIngreso").val(data.KeyValue);
                    $("#ctl00_body_txtHora").val(data.KeyValue2);
                    $("#txtANI").val('');
                    $("#txtNombreCliente").val('');
                } else {
                    try {
                        swal("Error al crear id de SMS", r.msg, "error");
                    }
                    catch (err) {
                        alert("Error al crear id de SMS: " + r.msg);
                    }
                }
            },
            error: function (response) {
                
                try {
                    swal("Error al crear id de SMS", 'Ocurrió un error inesperado, inténtelo más tarde.', "error");
                }
                catch (err) {
                    alert("Error al crear id de SMS: " + 'Ocurrió un error inesperado, inténtelo más tarde.');
                }
            }
        });  
    }

    this.ValidarSMS = function (SMS) {

        ctlr_bandeja.QuitaErrorValidacion();

        var isValid = true;

        //Datos cabecera
        SMS.ID = $("#ctl00_body_txtIDRegistro").val() || '0';
        SMS.ANI = $("#txtANI").val().trim();
        SMS.NOMBRE = $("#txtNombreCliente").val().trim();
        SMS.URL = $("#ctl00_body_txtURL").val();

        //Validacion
        if (!SMS.ANI) {
            ctlr_bandeja.MuestraErrorValidacion($("#txtANI"));
            isValid = false;
        } else if (SMS.ANI.length < 9) {
            ctlr_bandeja.MuestraErrorValidacion($("#txtANI"));
            isValid = false;
        }

        if (!SMS.NOMBRE) {
            ctlr_bandeja.MuestraErrorValidacion($("#txtNombreCliente"));
            isValid = false;
        }

        if (!isValid) {
            var input = $($(".error-validacion").get(0)).prev()[0];
            if (!$(input).is(':visible')) {
                var parenttab = $(input).closest('.tab-pane').attr('aria-labelledby');
                $("#" + parenttab).trigger('click');
            }
            setTimeout(function () {
                $(input).focus();
            }, 500);
        }

        return isValid;
    }

    this.Editar = function (id) {
        //ctlr_bandeja.LimpiarCampos();
        ctlr_bandeja.QuitaErrorValidacion();
        if (id == '0') {
            $("#hidId").val(id);
            $("#nuevo").modal('show');
        } else {
            ctlr_bandeja.CargarBandejaId(id)
        }
    }

    this.Cargar = function Cargar(filtro) {
        try {
            $('#tblServicios').DataTable().destroy();
        } catch (ex) {

        }
        try {

            $("#tblServicios tbody").html("");
            table = $('#tblServicios').DataTable({
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
                order: [[7, "desc"]],
                "ajax": {
                    "url": "../TableService.asmx/CargarBandeja",
                    "method": "POST",
                    "data": {
                        "CONNID": $("#txtConnidBuscar").val(),
                        "SKILL": $("#txtSkillBuscar").val(),
                        "FECHA": $("#txtFechaBuscar").val(),
                        "ANI": $("#txtAniBuscar").val(),
                    }
                },
                'columnDefs': [
                    { "targets": 0, "data": 'CONNID' },
                    { "targets": 1, "data": 'AGENTE' },
                    { "targets": 2, "data": 'FECHA' },
                    { "targets": 3, "data": 'ANI' },
                    { "targets": 4, "data": 'RESPUESTA1' },
                    { "targets": 5, "data": 'RESPUESTA2' },
                    { "targets": 6, "data": 'RESPUESTA3' },
                    {
                        "targets": 7,
                        "data": null,
                        "width": "50px",
                        "render": function (data, type, row, meta) {

                            var audio = data["AUDIO"];
                            var marca = data["MARCA_IVR"];
                            var ret = "";

                            if (audio == 'Sin audio') {
                                ret = data["AUDIO"];
                            }

                            if (marca != 'FN') {
                                ret = "Sin audio";
                            }
                            else {
                                ret = '<i class="fa fa-download row-descargar-button" style="cursor:pointer; color:#005f9f; font-size:20px;" ></i>';
                            }

                            return ret;
                        }
                    }
                ],
                "drawCallback": function () {
                }
            });
        } catch (ex) {
            console.log(ex.message);
        }
    }

    this.DescargarAudio = function DescargarAudio(connid) {

        var source = document.getElementById('audio');
        source.src = "";
        try {
            if (connid != '') {
                var values = JSON.stringify({ connid: connid });

                $.ajax({
                    url: "Main.aspx/DescargarAudio",
                    data: values,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        $("#loading").show();
                    },
                    success: function (response) {
                        $("#loading").hide();
                        var r = response.d;

                        if (r.ret == 'OK') {
                            var audio = r.values[0];
                            var nombre = r.values[1];

                            let link = document.createElement('a');
                            document.body.appendChild(link);
                            link.href = audio;
                            link.download = nombre;
                            link.click();
                        }
                    },
                    error: function (response) {
                    }
                });
            } else {
                $("#hidId").val('0');
                $("#txtANI").val('');
                $("#txtFechaLlamada").val('');
            }
            console.log('open')
            //$("#nuevo").modal('show');
        } catch (ex) {
            console.log(ex.message);
        }
    }

    this.QuitaErrorValidacion = function () {
        $(".error-validacion").remove();
    }

    this.MuestraErrorValidacion = function (input, pestana) {
        var mensaje = $(input).data("validacion");
        $(input).parent().append('<span class="text-danger error-validacion" style="display:none;">' + mensaje + '</span>');
        $(".error-validacion").fadeIn();
        //tabcontent = document.getElementsByClassName("tabcontent");

        if (pestana == 1) {
            $("a[href='#general']").tab('show');
        } else if (pestana == 2) {
            $("a[href='#viewtipificacion']").tab('show');
        }
    }

    this.LimpiarCampos = function () {

        $("#hidId").val('0');
        $("#txtANI").val('');
        $("#txtFechaLlamada").val('');
        $("#txtNombre").val('');
        $("#txtApellido").val('');
        $("#txtRUT").val('');
        $("#txtTelefonoContacto").val('');
        $("#txtCorreo").val('');
        $("#txtDireccion").val('');
        $("#cboRegion").val('0').trigger('change');
        $("#cboComuna").val('0').trigger('change');
        $("#cboTipificacion1").val('0').trigger('change');
        $("#cboTipificacion2").val('0').trigger('change');
        $("#cboTipificacion3").val('0').trigger('change'); 
        $("#txtFechaReagenda").val('');
    }

    this.Exportar = function Exportar(btn) {

        var filtroExcel = new FiltroBandeja();
        filtroExcel.CONNID = $("#txtConnidBuscar").val();
        filtroExcel.SKILL = $("#txtSkillBuscar").val();
        filtroExcel.FECHA = $("#txtFechaBuscar").val();
        filtroExcel.ANI = $("#txtAniBuscar").val();
        $(btn).buttonLoader('start');

        var values = JSON.stringify({ filtroExcel: filtroExcel });
        //var encodedString = btoa(values);

        //var url = "../ExportService.asmx/EPA?data=" + encodedString;

        //var win = window.open(url, '_blank');

        var name = "EASY_EPA_" + moment().format('YYYYMMDD') + ".xlsx";

        //var values = JSON.stringify({ filtro: filtro });
        var url = "./Descarga.asmx/EPA";

        // Use XMLHttpRequest instead of Jquery $ajax
        xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var a;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                //console.log(xhttp.response);
                if (window.navigator.msSaveBlob) { // IE
                    window.navigator.msSaveOrOpenBlob(xhttp.response, name)
                } else {
                    // Trick for making downloadable link
                    a = document.createElement('a');
                    a.href = window.URL.createObjectURL(xhttp.response);
                    // Give filename you wish to download
                    a.download = name;
                    a.style.display = 'none';
                    document.body.appendChild(a);
                    a.click();
                }
                $(btn).buttonLoader('stop');
                resolve('OK');
            } else {
                if (xhttp.readyState === 4 && xhttp.status != 200) {
                    if (xhttp.status == 400) {
                        swal("Información", 'Ocurrio un error al exportar datos.', "info");
                        $(btn).buttonLoader('stop');
                        resolve('ERROR');
                    } else {
                        swal("Información", "Ocurrió un error al descargar. Inténtelo más tarde", "info");
                        $(btn).buttonLoader('stop');
                        resolve('ERROR');
                    }
                }
            }
        };

        // Post data to URL which handles post request
        xhttp.open("POST", url);
        xhttp.setRequestHeader("Content-Type", "application/json");
        // You should set responseType as blob for binary responses
        xhttp.responseType = 'blob';
        xhttp.send(values);
    }
}