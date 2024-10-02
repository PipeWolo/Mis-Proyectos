var ctlr_main = new ctlr_main();
var tmpflpPago = null;
var tableHistorial = null;
function ctlr_main() {

    this.Init = function () {
        moment.locale('es');
        numeral.locale('es');
        //Inicia selects

        $("#cboResultadoLlamada").select2({ placeholder: 'Cargando...' }); 
        $("#cboMotivoLlamada").select2({ placeholder: 'Cargando...' }); $("#cboMotivoLlamada").attr('disabled', 'disabled');
        $("#cboResultadoCampana").select2({ placeholder: 'Cargando...' }); $("#cboResultadoCampana").attr('disabled', 'disabled');
        $("#cboMotivoCampana").select2({ placeholder: 'Cargando...' }); $("#cboMotivoCampana").attr('disabled', 'disabled');
        //Inicia Tooltip
        $('[data-toggle="tooltip"]').tooltip();

        var date = new Date();
        date.setDate(date.getDate());
        $("#txtFechaRepro").datetimepicker({
            format: 'dd/mm/yyyy HH:ii',
            startDate: date,
            autoclose: true,
            language: 'es'
        });

        //var ModoDiscado = "Predictivo"; //$("#ctl00_body_hModoDiscado").val();
        var ConnIdDec = $("#ctl00_body_hConnIdDec").val();
        document.getElementById("pnlLlamada").style.display = "none";

        var Error = $("#ctl00_body_hMensajeError").val();

        if (Error != "0") {
            swal({
                title: "Error",
                text: Error,
                icon: "warning"
            }).then(function (result) {
                var keysp = $("#ctl00_body_hKeySP").val();
                window.location.href = "Login.aspx?KeySP=" + keysp;
            });
            return;
        }

        //if (ModoDiscado == "Predictivo") {
        //    document.getElementById("pnlLlamada").style.display = "block";

        //    //Oculta botones de discado
        //    document.getElementById("btnDiscarFono").style.display = "none";
        //    document.getElementById("btnDiscarTelefonoMovil").style.display = "none";
        //    document.getElementById("btnDiscarTelefonoFijo").style.display = "none";
        //    document.getElementById("btnDiscarTelefonoTrabajo").style.display = "none";
        //    document.getElementById("btnDiscarTelefonoMovilTrabajo").style.display = "none";

        //    ctlr_main.CargarDatosPredictivo();
        //} else if (ModoDiscado == "Mixto") {
            if (ConnIdDec != "0") {
                document.getElementById("pnlLlamada").style.display = "block";
                var CodigoServicio = $("#ctl00_body_hCodigoServicio").val();

                //Oculta botones de discado
                document.getElementById("btnDiscarFono").style.display = "none";
                document.getElementById("btnDiscarTelefonoMovil").style.display = "none";
                document.getElementById("btnDiscarTelefonoFijo").style.display = "none";
                document.getElementById("btnDiscarTelefonoTrabajo").style.display = "none";
                document.getElementById("btnDiscarTelefonoMovilTrabajo").style.display = "none";

                ctlr_main.CargarDatosCampana(CodigoServicio);
                $("#cliente-tab").trigger('click');
                $("#hsalir").val('0');
            }
        //}
    }

    this.SetListeners = function SetListeners() {

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

            ctrl_tipif.VerificaReprogramacion();
        });

        $("#cboMotivoLlamada").change(function () {
            ctrl_tipif.VerificaReprogramacion();
        });

        $("#cboResultadoCampana").change(function () {
            var resultadoCampana = $(this).val();

            if (resultadoCampana == '0') {
                ctlr_util.ResetCombo("#cboMotivoResultadoCampana"); $("#cboMotivoResultadoCampana").attr('disabled', 'disabled');
            } else {
                ctrl_tipif.CargaMotivosCampana();
            }

            ctrl_tipif.VerificaReprogramacion();
        });

       
        $("#btnGrabar").click(function () {
            ctlr_main.Grabar(this);
        });

        $("#btnLink").click(function () {
            var url = $("#txtURLCall").val();
            window.open(url);
        });

        $("#btnBuscarRegistro").click(function () {

            var CodigoServicio = $("#ctl00_body_cboCampanas").val();

            if (CodigoServicio == "0") {
                swal("Error", 'Debe seleccionar una campaña.', "error");
                return;
            }

            document.getElementById("ctl00_body_pnlBuscarRegistro").style.display = "block";
            document.getElementById("ctl00_body_pnlAsistido").style.display = "none";
            $("#hsalir").val('1');
        });

        $("#btnCancelarBusqueda").click(function () {
            document.getElementById("ctl00_body_pnlBuscarRegistro").style.display = "none";
            document.getElementById("ctl00_body_pnlAsistido").style.display = "block";
        });

        $("#btnBuscar").click(function () {
            ctlr_main.Buscar(this);
        });

        $("#btnPedirRegistro").click(function () {
            var CodigoServicio = $("#ctl00_body_cboCampanas").val();

            if (CodigoServicio == "0") {
                swal("Error", 'Debe seleccionar una campaña.', "error");
                return;
            }

            ctlr_main.PedirRegistro("0");
        });

        $("#btnDiscarFono").click(function () {
            var fono = $("#ctl00_body_txtFonoContacto").val();
            $("#hOpcionDiscado").val("2"); 
            
            if (fono == "") {
                swal("Error", 'Debe ingresar el fono contacto.', "error");
                return;
            } else if (fono.length < 9) {
                swal('Error', "Teléfono debe ser de largo 9", 'error');
                return;
            }

            ctlr_main.Discar(fono);
        });

        $("#btnDiscarTelefonoMovil").click(function () {
            var fono = $("#txtTelefonoMovil").val();
            $("#hOpcionDiscado").val("1"); 

            ctlr_main.Discar(fono);
        });

        $("#btnDiscarTelefonoFijo").click(function () {
            var fono = $("#txtTelefonoFijo").val();
            $("#hOpcionDiscado").val("1"); 

            ctlr_main.Discar(fono);
        });

        $("#btnDiscarTelefonoTrabajo").click(function () {
            var fono = $("#txtTelefonoTrabajo").val();
            $("#hOpcionDiscado").val("1"); 

            ctlr_main.Discar(fono);
        });

        $("#btnDiscarTelefonoMovilTrabajo").click(function () {
            var fono = $("#txtTelefonoMovilTrabajo").val();
            $("#hOpcionDiscado").val("1"); 

            ctlr_main.Discar(fono);
        });

        $("#btnTransfiereEPA").click(function () {
            ctlr_main.TransfiereEPA(this);
        });

        $("#ctl00_body_cboCampanas").change(function () {
            var CodigoServicio = $("#ctl00_body_cboCampanas").val();

            if (CodigoServicio != "0") {
                var values = JSON.stringify({ CodigoServicio: CodigoServicio });

                $.ajax({
                    url: "Main.aspx/ModoDiscado",
                    data: values,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                    },
                    success: function (response) {
                        var r = response.d;

                        if (r.ret == 'OK') {
                            var dis = r.values[0];
                            var modo = dis.KeyValue;

                            if (modo == "Asistido") {
                                $("#btnPedirRegistro").show();
                            } else if (modo == "Mixto") {
                                $("#btnPedirRegistro").hide();
                            }

                            ctlr_main.CargarDatosCampana(CodigoServicio);
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

    this.Discar = function Discar(fono) {
        var CodigoServicio = $("#ctl00_body_hCodigoServicio").val();
        var Skill = $("#ctl00_body_txtSkillCampana").val().trim();
        var Prefijo = $("#hPrefijo").val().trim();

        $("#ctl00_body_txtFonoContacto").val(fono);
        document.getElementById('ctl00_body_txtFonoContacto').disabled = true;
        document.getElementById('btnDiscarFono').disabled = true;
        document.getElementById('btnDiscarTelefonoMovil').disabled = true;
        document.getElementById('btnDiscarTelefonoFijo').disabled = true;
        document.getElementById('btnDiscarTelefonoTrabajo').disabled = true;
        document.getElementById('btnDiscarTelefonoMovilTrabajo').disabled = true;

        var Fono = Prefijo + fono;
        param = "param=Discar&fono=" + Fono + "&skill=" + Skill + "&CodigoServicio=" + CodigoServicio;

        window.open("SoftNet.aspx?" + param, "ir_pie");
    }

    this.CargarTipificaciones = function CargarTipificaciones() {
        return new Promise(function (resolve) {

            var CodigoServicio = $("#ctl00_body_hCodigoServicio").val();

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

    this.CargarDatosCampana = function CargarDatosCampana(CodigoServicio) {
        if (CodigoServicio != "0") {
            var values = JSON.stringify({ CodigoServicio: CodigoServicio });

            $.ajax({
                url: "Main.aspx/CargaDatosCampana",
                data: values,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                },
                success: function (response) {
                    var r = response.d;

                    if (r.ret == 'OK') {
                        var dis = r.values[0];

                        if (dis != null) {
                            $("#hModoDiscado").val(dis.ModoDiscado);
                            $("#txtCampana").val(dis.NombreCampana);
                            $("#hPrefijo").val(dis.Prefijo);
                            $("#ctl00_body_hCodigoServicio").val(dis.CodigoServicio);
                        }

                        var ConnIdDec = $("#ctl00_body_hConnIdDec").val();
                        if (ConnIdDec != "0") {
                            ctlr_main.CargarDatosPredictivo();
                        } else {
                            document.getElementById('ctl00_body_txtConnID').disabled = false;
                        }

                    } else {
                        resolve('ERROR');
                    }
                },
                error: function (response) {
                    resolve('ERROR');
                }
            });
        }
    }

    this.CargarDatosPredictivo = function () {
        return new Promise(function (resolve) {

            var IdentificadorCliente = $("#ctl00_body_hIdentificadorCliente").val();
            var CodigoServicio = $("#ctl00_body_txtCodigoServicio").val();
            var Skill = $("#ctl00_body_txtSkillCampana").val();

            if (IdentificadorCliente != "0") {
                var values = JSON.stringify({ IdentificadorCliente: IdentificadorCliente, CodigoServicio: CodigoServicio, Skill: Skill });

                $.ajax({
                    url: "Main.aspx/CargarDatosPredictivo",
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
                            var hist = r.values[1];

                            if (data != null) {
                                $("#txtClienteRUT").val(data.RutCliente);
                                $("#txtClienteDV").val(data.DV);
                                $("#txtNombreCliente").val(data.NombreCliente);
                                $("#txtComuna").val(data.Comuna);
                                $("#txtTelefonoMovil").val(data.TelefonoMovil);
                                $("#txtTelefonoFijo").val(data.TelefonoFijo);
                                $("#txtTelefonoTrabajo").val(data.TelefonoTrabajo);
                                $("#txtTelefonoMovilTrabajo").val(data.TelefonoMovilTrabajo);
                                $("#txtCorreoElectronico1").val(data.CorreoElectrónico1);
                                $("#txtCorreoElectronico2").val(data.CorreoElectrónico2);
                                $("#txtCodigoSocio").val(data.CodigoSocio);
                                $("#txtMontoAporte").val(data.MontoAporte);
                                $("#txtDivisa").val(data.Divisa);
                                $("#txtFundacion").val(data.Fundacion);
                                $("#txtTipoMedioPago").val(data.TipoMedioPago);
                                $("#txtMedioPago").val(data.MedioPago);
                                $("#txtOficinaVenta").val(data.OficinaVenta);
                                $("#txtSede").val(data.Sede);
                                $("#txtMontoBase").val(data.MontoBase);
                                $("#txtMontoPropuesto").val(data.MontoPropuesto);
                                $("#txtURLCall").val(data.URLCall);
                                $("#txtAcuerdo").val(data.Acuerdo);
                                $("#hIDCS").val(data.ID_CS);

                                if (hist != null) {
                                    var html = "";

                                    $.each(hist, function (i, c) {
                                        html += "<tr>";

                                        html += "<td>" + c.NumeroLlamada + "</td>";
                                        html += "<td>" + ctlr_util.MyNull(c.FechaUltimaLlamada) + "</td>";
                                        html += "<td>" + ctlr_util.MyNull(c.ResultadoLlamado) + "</td>";
                                        html += "<td>" + ctlr_util.MyNull(c.MotivoLlamado) + "</td>";
                                        html += "<td>" + ctlr_util.MyNull(c.ResultadoCampana) + "</td>";
                                        html += "<td>" + ctlr_util.MyNull(c.MotivoCampana) + "</td>";

                                        html += "</tr>";

                                    });

                                    $("#tblHistorial > tbody").html("");
                                    $("#tblHistorial > tbody").append(html);
                                }
                            } else {
                                swal({
                                    title: "Error",
                                    text: "No existe información a cargar para el número de llamada.",
                                    icon: "error"
                                }).then(function (result) {
                                    window.location.replace("Login.aspx");
                                    window.close();
                                });                                
                            }

                            resolve('OK');
                        } else {
                            swal({
                                title: "Error",
                                text: "No existe información a cargar para el número de llamada.",
                                icon: "error"
                            }).then(function (result) {
                                window.location.replace("Login.aspx");
                                window.close();
                            });  
                        }
                    },
                    error: function (response) {
                        resolve('ERROR');
                    }
                });
            }
        });
    }

    this.PedirRegistro = function (NumeroLlamada) {
        return new Promise(function (resolve) {

            var CodigoServicio = $("#ctl00_body_hCodigoServicio").val();
            var Agente = $("#ctl00_body_hAgente").val();

            var values = JSON.stringify({ CodigoServicio: CodigoServicio, NumeroLlamada: NumeroLlamada, Agente: Agente });

            $.ajax({
                url: "Main.aspx/PedirRegistro",
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
                        var hist = r.values[1];

                        $(".nav-tabs a[href='#cliente']").tab("show");
                        ctlr_main.CargarTipificaciones();
                        $('#txtFonoContacto').removeAttr("disabled");
                        $("#ctl00_body_txtNLlamada").val(data.NumeroLlamada);
                        $("#ctl00_body_txtSkillCampana").val(data.Skill);
                        $("#ctl00_body_txtCodigoServicio").val(CodigoServicio);
                        $("#ctl00_body_txtAgente").val(data.Agente);
                        $("#txtCampana").val(data.NombreCampana);

                        $("#txtClienteRUT").val(data.RutCliente);
                        $("#txtClienteDV").val(data.DV);
                        $("#txtNombreCliente").val(data.NombreCliente);
                        $("#txtComuna").val(data.Comuna);
                        $("#txtTelefonoMovil").val(data.TelefonoMovil);
                        $("#txtTelefonoFijo").val(data.TelefonoFijo);
                        $("#txtTelefonoTrabajo").val(data.TelefonoTrabajo);
                        $("#txtTelefonoMovilTrabajo").val(data.TelefonoMovilTrabajo);
                        $("#txtCorreoElectronico1").val(data.CorreoElectrónico1);
                        $("#txtCorreoElectronico2").val(data.CorreoElectrónico2);
                        $("#txtCodigoSocio").val(data.CodigoSocio);
                        $("#txtMontoAporte").val(data.MontoAporte);
                        $("#txtDivisa").val(data.Divisa);
                        $("#txtFundacion").val(data.Fundacion);
                        $("#txtTipoMedioPago").val(data.TipoMedioPago);
                        $("#txtMedioPago").val(data.MedioPago);
                        $("#txtOficinaVenta").val(data.OficinaVenta);
                        $("#txtSede").val(data.Sede);
                        $("#txtMontoBase").val(data.MontoBase);
                        $("#txtMontoPropuesto").val(data.MontoPropuesto);
                        $("#txtURLCall").val(data.URLCall);
                        $("#txtAcuerdo").val(data.Acuerdo);
                        $("#hIDCS").val(data.ID_CS);

                        //Muestra los botones otra vez
                        document.getElementById("btnDiscarTelefonoMovil").style.display = "block";
                        document.getElementById("btnDiscarTelefonoFijo").style.display = "block";
                        document.getElementById("btnDiscarTelefonoTrabajo").style.display = "block";
                        document.getElementById("btnDiscarTelefonoMovilTrabajo").style.display = "block";

                        //Habilita los botones
                        document.getElementById('btnDiscarFono').disabled = false;
                        document.getElementById('btnDiscarTelefonoMovil').disabled = false;
                        document.getElementById('btnDiscarTelefonoFijo').disabled = false;
                        document.getElementById('btnDiscarTelefonoTrabajo').disabled = false;
                        document.getElementById('btnDiscarTelefonoMovilTrabajo').disabled = false;

                        if (ctlr_util.MyNull(data.TelefonoMovil) == "")
                            document.getElementById("btnDiscarTelefonoMovil").style.display = "none";

                        if (ctlr_util.MyNull(data.TelefonoFijo) == "")
                            document.getElementById("btnDiscarTelefonoFijo").style.display = "none";

                        if (ctlr_util.MyNull(data.TelefonoTrabajo) == "")
                            document.getElementById("btnDiscarTelefonoTrabajo").style.display = "none";

                        if (ctlr_util.MyNull(data.TelefonoMovilTrabajo) == "")
                            document.getElementById("btnDiscarTelefonoMovilTrabajo").style.display = "none";

                        if (hist != null) {
                            var html = "";

                            $.each(hist, function (i, c) {
                                html += "<tr>";

                                html += "<td>" + c.NumeroLlamada + "</td>";
                                html += "<td>" + ctlr_util.MyNull(c.FechaUltimaLlamada) + "</td>";
                                html += "<td>" + ctlr_util.MyNull(c.ResultadoLlamado) + "</td>";
                                html += "<td>" + ctlr_util.MyNull(c.MotivoLlamado) + "</td>";
                                html += "<td>" + ctlr_util.MyNull(c.ResultadoCampana) + "</td>";
                                html += "<td>" + ctlr_util.MyNull(c.MotivoCampana) + "</td>";

                                html += "</tr>";

                            });

                            $("#tblHistorial > tbody").html("");
                            $("#tblHistorial > tbody").append(html);
                        }

                        document.getElementById("ctl00_body_pnlBuscarRegistro").style.display = "none";
                        document.getElementById("ctl00_body_pnlAsistido").style.display = "none";
                        document.getElementById("pnlLlamada").style.display = "block";
                        //document.getElementById("pnlDatosLlamada").style.display = "block";

                        $("#hsalir").val('0');
                        resolve('OK');
                    } else if (r.msg == "No existen registros a discar") {
                        swal("Error", 'No existen registros a discar.', "info");
                        return;
                    } else {
                        resolve('ERROR');
                    }
                        
                },
                error: function (response) {
                    resolve('ERROR');
                }
            });
         
        });
    }

    this.Trabajar = function (NumeroLlamada, ResultadoLlamado, MotivoLlamado) {

        if (ResultadoLlamado == "Pendiente") {
            if (MotivoLlamado == "Volver a llamar" || MotivoLlamado == "Gestión manual" || MotivoLlamado == "Está manejando" || MotivoLlamado == "Fuera del país") {
                ctlr_main.PedirRegistro(NumeroLlamada)
            } else {
                swal("Error", 'Registro tipificado como Pendiente/' + MotivoLlamado + ', no se puede volver a trabajar.', "error");
                return;
            }
        } else if (ResultadoLlamado == "En Proceso") {
            ctlr_main.PedirRegistro(NumeroLlamada)
        } else if (ResultadoLlamado == "") {
            ctlr_main.PedirRegistro(NumeroLlamada)
        } else if (ResultadoLlamado == "Contactado") {
            swal("Error", 'Registro tipificado como Contactado, no se puede volver a trabajar.', "error");
            return;
        } else if (ResultadoLlamado == "Descartado") {
            swal("Error", 'Registro tipificado como Descartado, no se puede volver a trabajar.', "error");
            return;
        }
    }

    this.Buscar = function (btn) {

        var Rut = $("#txtRutClienteBusqueda").val();
        var CodigoServicio = $("#ctl00_body_hCodigoServicio").val();

        if (Rut == "") {
            swal("Error", 'Debe ingresar Rut Cliente', "error");
            document.getElementById("txtRutClienteBusqueda").focus();
            return;
        }

        var values = JSON.stringify({ Rut: Rut, CodigoServicio: CodigoServicio });

        $.ajax({
            url: "Main.aspx/Buscar",
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
                        
                    var hist = r.values[0];
                    var cont = 0;

                    if (hist != null) {
                        var html = "";

                        $.each(hist, function (i, c) {
                            cont = cont + 1;

                            html += "<tr>";
                            var click = "";

                            var Resultado = '"' + ctlr_util.MyNull(c.ResultadoLlamado) + '"';
                            var Motivo = '"' + ctlr_util.MyNull(c.MotivoLlamado) + '"';

                            click = "<a href='#' onclick='javascript:ctlr_main.Trabajar(" + c.NumeroLlamada + ", " + Resultado + ", " + Motivo + ")' class='btn btn-primary btn-xs'><i class='fa fa-folder'></i> Trabajar </a>";

                            var tipif = ctlr_util.MyNull(c.ResultadoLlamado);
                            if (tipif == "")
                                tipif = "Sin Tipificar";

                            html += "<td>" + click + "</td>";
                            html += "<td>" + c.NumeroLlamada + "</td>";
                            html += "<td>" + ctlr_util.MyNull(c.FechaUltimaLlamada) + "</td>";
                            html += "<td>" + tipif + "</td>";
                            html += "<td>" + ctlr_util.MyNull(c.MotivoLlamado) + "</td>";
                            html += "<td>" + ctlr_util.MyNull(c.ResultadoCampana) + "</td>";
                            html += "<td>" + ctlr_util.MyNull(c.MotivoCampana) + "</td>";

                            html += "</tr>";

                        });

                        if (cont == 0) {
                            swal("Error", 'RUT no encontrado', "error");
                            document.getElementById("txtRutClienteBusqueda").focus();
                        }

                        $("#tblBusqueda > tbody").html("");
                        $("#tblBusqueda > tbody").append(html);
                    }

                    //resolve('OK');
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

    this.Grabar = function (btn) {

        ctlr_main.QuitaErrorValidacion();

        var isValid = true;

        var llamada = new Llamada();

        isValid = ctlr_main.ValidarLlamada(llamada);

        var OpcionLlamado = $("#hOpcionDiscado").val(); 

        var values = JSON.stringify({ llamada: llamada, OpcionLlamado: OpcionLlamado });

        if (isValid) {
            $.ajax({
                url: "Main.aspx/Grabar",
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
			            $("#hsalir").val('1');
                        try {
                            swal({
                                title: "Operación exitosa",
                                text: "Registro Grabado.",
                                icon: "success"
                            }).then(function (result) {
                                var modoDiscado = $("#hModoDiscado").val(); 

                                if (modoDiscado == "Predictivo") {
                                    window.close();
                                    $("#btnGrabar").hide();
                                } else if (modoDiscado == "Asistido") {
                                    ctlr_main.LimpiarCampos();
                                } else if (modoDiscado == "Mixto") {
                                    var connid = $("#ctl00_body_hConnIdDec").val(); 
                                    if (connid == "0") {
                                        ctlr_main.LimpiarCampos();
                                    } else {
                                        window.close();
                                        $("#btnGrabar").hide();
                                    }
                                }
                            });
                        }
                        catch (err) {
                            alert('Operación exitosa: ' + msjok);
                            ctlr_main.LimpiarCampos();
                        }
                    } else {
                        try {
                            if (r.msg == "Registro lista fue grabado con éxito pero fallo al actualizar el resultado de la llamada en genesys") {
                                swal("Advertencia al guardar", r.msg, "info");
                                ctlr_main.LimpiarCampos();
                            } else {
                                swal("Error al grabar", r.msg, "error");
                            }
                        }
                        catch (err) {
                            alert("Advertencia al guardar: " + r.msg);
                            ctlr_main.LimpiarCampos();
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
    }

    this.ValidarLlamada = function (llamada) {

        ctlr_main.QuitaErrorValidacion();

        var isValid = true;

        //Datos cabecera
        llamada.NumeroLlamada = $("#ctl00_body_txtNLlamada").val().trim();
        llamada.RutAgente = $("#ctl00_body_hAgente").val().trim();
        llamada.Agente = $("#ctl00_body_txtAgente").val().trim();
        llamada.Skill = $("#ctl00_body_txtSkillCampana").val().trim();
        llamada.Numero_Cliente = $("#ctl00_body_txtFonoContacto").val().trim();
        llamada.ConnIdHex = $("#ctl00_body_hConnIdHex").val().trim();
        llamada.ConnIdDec = $("#ctl00_body_hConnIdDec").val().trim();
        llamada.CodigoServicio = $("#ctl00_body_txtCodigoServicio").val().trim();
        llamada.NombreLista = $("#ctl00_body_hNombreLista").val().trim();
        //Datos cliente
        llamada.RutCliente = $("#txtClienteRUT").val().trim();
        llamada.ResultadoLlamado = $("#cboResultadoLlamada").val().trim();
        llamada.RecordID = $("#ctl00_body_hRecordID").val().trim();

        try {
            llamada.MotivoLlamado = $("#cboMotivoLlamada").val().trim();
        } catch (e) {
            llamada.MotivoLlamado = "0";
        }

        try {
            llamada.ResultadoCampana = $("#cboResultadoCampana").val().trim();
        } catch (e) {
            llamada.ResultadoCampana = "0";
        }

        try {
            llamada.MotivoCampana = $("#cboMotivoCampana").val().trim();
        } catch (e) {
            llamada.MotivoCampana = "0";
        }
        
        llamada.Observaciones = $("#txtObservacion").val().trim();
        llamada.FechaReprogramacion = $("#txtFechaRepro").val().trim();

        //Validacion Discado
        var txtFono = document.getElementById("ctl00_body_txtFonoContacto").disabled;

        if (txtFono == false) {
            swal("Error", 'No ha efectuado comunicación con el cliente (discado)', "error");
            document.getElementById("ctl00_body_txtFonoContacto").focus();
            isValid = false;
            return;
        }

        var ConnIdHex = $("#ctl00_body_txtConnID").val().trim();
        if (ConnIdHex == "") {
            swal("Error", 'Debe ingresar el ConnID.', "error");
            document.getElementById("ctl00_body_txtConnID").focus();
            isValid = false;
            return;
        } else {
            llamada.ConnIdHex = ConnIdHex;
        }

        //Tipificacion
        if (llamada.ResultadoLlamado == "0") {
            $(".nav-tabs a[href='#tipificacion']").tab("show");
                
            swal({
                title: "",
                text: "Debe seleccionar Resultado Llamada",
                icon: "warning"
            }).then(function (result) {
                $("#cboResultadoLlamada").focus();
            });
            
            return false;
        } if (llamada.ResultadoLlamado == "Contactado") {
            if (llamada.ResultadoCampana == "0") {
                $(".nav-tabs a[href='#tipificacion']").tab("show");
                swal({
                    title: "",
                    text: "Debe seleccionar Resultado Campaña",
                    icon: "warning"
                }).then(function (result) {
                    $("#cboResultadoCampana").focus();
                });
                return false;
            } else {
                if (llamada.MotivoCampana == "0") {
                    $(".nav-tabs a[href='#tipificacion']").tab("show");
                    swal({
                        title: "",
                        text: "Debe seleccionar Motivo Campaña",
                        icon: "warning"
                    }).then(function (result) {
                        $("#cboMotivoCampana").focus();
                    });
                    return false;
                }
            }
        } else {
            if (llamada.MotivoLlamado == "0") {
                $(".nav-tabs a[href='#tipificacionn']").tab("show");
                swal({
                    title: "",
                    text: "Debe seleccionar Motivo Llamada",
                    icon: "warning"
                }).then(function (result) {
                    $("#cboMotivoLlamada").focus();
                });
                return false;
            }
        }

        //Reprogramacion 
        var repro = document.getElementById("pnlReprogramacion").style.display;

        if (repro == "block") {
            var fecha = $("#txtFechaRepro").val();

            if (fecha == "") {
                $(".nav-tabs a[href='#tab_tipificacion']").tab("show");
                swal({
                    title: "",
                    text: "Debe ingresar Fecha/Hora de Reprogramación",
                    icon: "warning"
                }).then(function (result) {
                    $("#cboMotivoCampana").focus();
                });
                return false;
                document.getElementById("txtFechaRepro").focus();
                return false;
            }
        }

        if (llamada.MotivoLlamado == "0")
            llamada.MotivoLlamado = "";

        if (llamada.ResultadoCampana == "0")
            llamada.ResultadoCampana = "";

        if (llamada.MotivoCampana == "0")
            llamada.MotivoCampana = "";

        return isValid;
    }

    this.LimpiarCampos = function () {

        document.getElementById("ctl00_body_pnlAsistido").style.display = "block";
        document.getElementById("pnlLlamada").style.display = "none";

        //Datos Busqueda
        $("#txtRutClienteBusqueda").val('');
        $("#tblBusqueda > tbody").html("");

        //Datos cabecera
        $("#ctl00_body_txtCodigoServicio").val('');
        $("#txtCampana").val('');
        $("#ctl00_body_txtSkillCampana").val('');
        $("#ctl00_body_txtNLlamada").val('');
        $("#ctl00_body_txtFonoContacto").val('');
        $("#ctl00_body_txtAgente").val('');
        $("#ctl00_body_txtConnID").val('');
        document.getElementById('ctl00_body_txtFonoContacto').disabled = false;

        //Datos cliente
        $("#txtObservacion").val('');

        //Tipificacion
        $("#cboResultadoLlamada").val('0').trigger('change');
        ctlr_util.ResetCombo("#cboMotivoLlamada");
        $("#cboMotivoLlamada").select2({ placeholder: 'Cargando...' }); $("#cboMotivoLlamada").attr('disabled', 'disabled');
        ctlr_util.ResetCombo("#cboResultadoCampana");
        $("#cboResultadoCampana").select2({ placeholder: 'Cargando...' }); $("#cboResultadoCampana").attr('disabled', 'disabled');
        ctlr_util.ResetCombo("#cboMotivoCampana");
        $("#cboMotivoCampana").select2({ placeholder: 'Cargando...' }); $("#cboMotivoCampana").attr('disabled', 'disabled');

        //Datos Reprogramacion
        document.getElementById("pnlReprogramacion").style.display = "none";
        $("#txtFechaRepro").val('')
        
        $("#cliente-tab").trigger('click')
        
    }

    this.TransfiereEPA = function () {

        var idcs = $("#hIDCS").val();
        var cs = $("#ctl00_body_hCodigoServicio").val();
        var cliente = $("#txtClienteRUT").val();
        var nroReq = $("#ctl00_body_txtNLlamada").val();
        var isValid = true;

        var txtFono = document.getElementById("ctl00_body_txtFonoContacto").disabled;

        if (txtFono == false) {
            swal("Error", 'No ha efectuado comunicación con el cliente (discado)', "error");
            document.getElementById("ctl00_body_txtFonoContacto").focus();
            isValid = false;
            return;
        }

        if (!cliente) {
            ctlr_main.MuestraErrorValidacion($("#txtClienteRUT"));
            isValid = false;
            $("#txtClienteRUT").focus();
        }

        var ceros = '';
        var ceros2 = '';
        for (fin = 0; fin < (8 - nroReq.length); fin++) {
            ceros = '0' + ceros;
        };
        for (fin = 0; fin < (8 - cliente.length); fin++) {
            ceros2 = '0' + ceros2;
        };

        var dnis = $("#ctl00_body_hidprefijoEPA").val().trim() + $('#ctl00_body_hidvdnEPA').val() + ceros + nroReq + ceros2 + cliente + (idcs.length == 2 ? idcs : '0' + idcs);

        if (isValid) {
            var param = "param=Transferencia&vdn=" + dnis;
            window.open("SoftNet.aspx?" + param, "ir_pie");
        }

    }

    this.QuitaErrorValidacion = function (obj) {
        if (obj) {
            $(obj).next(".error-validacion").remove();
        }
        else {
            $(".error-validacion").remove();
        }
    }

    this.MuestraErrorValidacion = function (input) {
        var mensaje = $(input).data("validacion");
        $(input).parent().append('<span class="text-danger error-validacion" style="display:none;">' + mensaje + '</span>');
        $(".error-validacion").fadeIn();
    }

    this.Mensaje = function Mensaje(title, text, type) {
        try {
            swal(title, text, type);
        }
        catch (err) {
            alert(title + ': ' + text);
        }
    }

    //#region Historial

    this.CargarHistorial = function () {
        return new Promise(function (resolve) {
            try {
                tableHistorial = $('#tblHistorial').DataTable({
                    "pageLength": 10,
                    "autoWidth": false,
                    "stateSave": true,
                    searching: false,
                    "responsive": true,
                    "destroy": true,
                    "processing": true,
                    "serverSide": true,
                    order: [5, "desc"],
                    language: {
                        url: './assets/plugins/datatables/Spanish.json'
                    },
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "url": "./ws/TableService.asmx/CargarHistorial",
                        "type": "POST",
                        "data": {
                            'RUT': $("#ctl00_body_txtClienteRUT").val().trim(),
                        }
                    },
                    'columnDefs': [
                        {
                            "targets": 0, "bSortable": false,
                            "data": null,
                            "render": function (data, type, row, meta) {
                                var click = "<a href='#' class='row-view-button' data-load-text=''><i class='fa fa-eye'></i></a>";
                                var html = '<p class="text-center">' + click + '</p>';
                                return html;
                            }
                        },
                        { "targets": 1, "data": 'N_REQUERIMIENTO', visible: false },
                        { "targets": 2, "data": 'RUT_CLIENTE' },
                        { "targets": 3, "data": 'NOMBRE_EMPRESA_PERSONA' },
                        { "targets": 4, "data": 'FECHA_HORA_LLAMADA' },
                        { "targets": 5, "data": 'HABILIDAD' },
                        { "targets": 6, "data": 'OPERACION' },
                        { "targets": 7, "data": 'SUBOPERACION' },
                        { "targets": 8, "data": 'TIPO' }
                    ],
                    "drawCallback": function () {
                        resolve('OK');
                    }
                });
            } catch (ex) {
                console.log(ex.message);
                resolve('ERROR');
            }
        });
    }

    this.CargarHistorialId = function (data) {
        return new Promise(function (resolve) {
            if (data) {
                var id = data.N_REQUERIMIENTO;
                var values = JSON.stringify({ id: id });

                $.ajax({
                    url: "Main.aspx/CargarHistorialID",
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
                            ctlr_main.CargarHistorialCampos(data);
                            resolve('OK');
                        } else {
                            resolve('ERROR');
                        }
                    },
                    error: function (response) {
                        resolve('ERROR');
                    }
                });
            } else {
                resolve('ERROR');
            }
        });
    }

    this.CargarHistorialCampos = function (data) {
        if (data) {
            //DATOS LLAMADA
            $("#viewRutAgente").val(data.RUT_AGENTE);
            $("#viewSkill").val(data.SKILL);
            $("#viewFecha").val(data.FECHA_HORA_LLAMADA);
            $("#viewANI").val(data.ANI);
            $("#viewCONNID").val(data.CONNID);
            $("#viewNRequerimiento").val(data.N_REQUERIMIENTO);
            //DATOS CLIENTE
            $("#viewRutCliente").val(data.RUT_CLIENTE);
            $("#viewNombreEmpresa").val(data.NOMBRE_EMPRESA_PERSONA);
            $("#viewNumeroContacto").val(data.NUMERO_CONTACTO);
            $("#viewNombreContacto").val(data.NOMBRE_CONTACTO);
            $("#viewCodigoServicio").val(data.CODIGO_SERVICIO);
            $("#viewMotivoContacto").val(data.MOTIVO_CONTACTO_DIAGNOSTICO);
            //DATOS TIPIFICACION
            $("#viewHabilidad").val(data.HABILIDAD);
            $("#viewOperacion").val(data.OPERACION);
            $("#viewSuboperacion").val(data.SUBOPERACION);
            $("#viewTipo").val(data.TIPO);
            $("#viewObservacion").val(data.OBSERVACIONES);
        }
        $("#general-tab").trigger('click');
    }

    //#endregion

    this.CargarUltimoContacto = function () {
        return new Promise(function (resolve) {
            var values = JSON.stringify({ RUT: $("#ctl00_body_txtClienteRUT").val().trim(), OT: $("#ctl00_body_txtOT").val().trim(), ANI: $("#ctl00_body_txtCabeceraANI").val().trim() });
            $.ajax({
                url: "Main.aspx/CargarUltimoContacto",
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
                        if (data) {
                            $("#txtClienteRUTContacto").val(data.RUT_CONTACTO).trigger('keyup');
                            $("#txtClienteNombre").val(data.NOMBRE_CONTACTO);
                            $("#txtClienteAP").val(data.APELLIDO_PATERNO_CONTACTO);
                            $("#txtClienteAM").val(data.APELLIDO_MATERNO_CONTACTO);
                            $("#txtClienteTelefonoMovil").val(data.TELEFONO_MOVIL_CONTACTO);
                            $("#txtClienteTelefonoFijo").val(data.TELEFONO_FIJO_CONTACTO);
                            $("#txtClienteTelefonoInterno").val(data.TELEFONO_INTERNO_CONTACTO);
                            $("#txtClienteCorreo").val(data.CORREO_CONTACTO);
                            $("#txtClienteNickname").val(data.NICKNAME);
                        }
                        resolve('OK');
                    } else {
                        resolve('ERROR');
                    }
                },
                error: function (response) {
                    resolve('ERROR');
                }
            });
        });
    }

    this.copyToClipboard = function copyToClipboard(text) {
        try {
            var $temp = $("<input>");
            $("body").append($temp);
            $temp.val(text).select();
            document.execCommand("copy");
            $temp.remove();
            swal("Copiado!", 'Se copió el enlace en el portapapeles.', "success");
        } catch (e) {
            swal("No Copiado!", 'No se copió el enlace en el portapapeles.', "error");
        }
    }

    this.CargarDatosCliente = function () {
        return new Promise(function (resolve) {
            var rutCliente = $("#ctl00_body_txtClienteRUT").val();
            var values = JSON.stringify({ rutCliente: rutCliente });
            $.ajax({
                url: "Main.aspx/CargarDatosCliente",
                data: values,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    $.busyLoadFull('show', { text: "Cargando Tipo Cliente" });
                },
                success: function (response) {
                    var r = response.d;
                    var tipoCliente = '';
                    try {
                        tipoCliente = r.values[0];
                    } catch (e) {
                        tipoCliente = 'Sin información';
                    }
                    $("#txtOtTipoCliente").val(tipoCliente);
                    $.busyLoadFull('hide');
                    resolve('OK');
                },
                error: function (response) {
                    var tipoCliente = 'Sin información';
                    $("#txtOtTipoCliente").val(tipoCliente);
                    $.busyLoadFull('hide');
                    resolve('ERROR');
                }
            });
        });
    }
}