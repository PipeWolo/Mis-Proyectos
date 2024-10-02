var ctlr_main = new ctlr_main();
var estados = [];
function ctlr_main() {
    this.Cargaticket = function Cargaticket(usuario) {

        var values = JSON.stringify({ agente: usuario });

        $.ajax({
            url: "Main.aspx/Cargaticket",
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

                $("#txtAgente").val('');
                $("#txtNombreagente").val('');
                $("#txtFechallamada").val('');

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];
                    var dat2 = response.d.values[1];
                    var dat3 = response.d.values[2];
                    var dat4 = response.d.values[3];
                    var dat5 = response.d.values[4];

                    if (dat1 != null) {
                        $("#txtAgente").val(dat1.Agente);
                        $("#txtNombreagente").val(dat1.Nombreagente);
                        $("#txtFechallamada").val(dat1.Fechallamada);
                    }

                    if (dat2 != null) {
                        $("#cboResultadollamada").html("");
                        $("#cboResultadollamada").append("<option value='0'>Seleccione...</option>");
                        $.each(dat2, function (i, d) {
                            $("#cboResultadollamada").append("<option value='" + d.KeyValue + "'>" + d.KeyName + "</option>");
                        });
                    }

                    if (dat3 != null) {
                        $("#cboCasuistica").html("");
                        $("#cboCasuistica").append("<option value='0'>Seleccione...</option>");
                        $.each(dat3, function (i, d) {
                            $("#cboCasuistica").append("<option value='" + d.KeyValue + "'>" + d.KeyName + "</option>");
                        });
                    }

                    if (dat4 != null) {
                        if (dat4 != null) {
                            $("#cboEstadoCaso").html("");
                            $("#cboEstadoCaso").append("<option value='0'>Seleccione...</option>");
                            $.each(dat4, function (i, d) {
                                $("#cboEstadoCaso").append("<option value='" + d.KeyName + "'>" + d.KeyName + "</option>");
                            });
                            estados = dat4;
                        }
                    }

                    if (dat5 != null) {
                        if (dat5 != null) {
                            $("#cboContacto").html("");
                            $("#cboContacto").append("<option value='0'>Seleccione...</option>");
                            $.each(dat5, function (i, d) {
                                $("#cboContacto").append("<option value='" + d.KeyName + "'>" + d.KeyName + "</option>");
                            });
                        }
                    }

                } else {
                    $("#loading").hide();
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#loading").hide();
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }
    
    this.CargaTicketCaso = function (caso, idCaso) {

        var values = JSON.stringify({ caso: caso, idCaso: idCaso });

        $.ajax({
            url: "Main.aspx/CargaTicketCaso",
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
                    var data = r.values[0];
                    var data2 = r.values[1];
                    $("#txtIdaplicacioncl").val(caso);
                    $("#dvRutUsuario").hide();
                    $("#dvNombreUsuario").hide();
                    $("#txtIdaplicacioncl").attr('disabled', 'disabled');
                    //if (data != null) {
                    //    $("#txtTicket").val(data.NUMERO_LLAMADA);
                    //    $("#txtFechallamada").val(data.FECHA_LLAMADA);
                    //    $("#txtRutusuario").val(data.RUT_USUARIO);
                    //    $("#txtNombreusuario").val(data.NOMBRE_USUARIO);
                    //    $("#txttelefono").val(data.TELEFONO_DISCADO);
                    //    $("#cboContacto").val(data.CONTACTO).trigger('change');
                    //    $("#cboMotivoContacto").val(data.MOTIVO_CONTACTO).trigger('change');
                    //    $("#cboResultado").val(data.RESULTADO);
                    //    $("#txtObsllamada").val(data.OBSERVACIONES);
                    //    $("#cboResultadollamada").val((data.TIPIFICACION == 'Contactado' ? '1' : (data.TIPIFICACION == 'No contactado' ? '2' : '0')));
                    //    $("#txtConnid").val(data.CONNID);
                    //    $("#txtFechaSeguimiento").val(data.SEGUIMIENTO);
                    //}
                    if (data2 != null && data2.ESTADO_CASO_BDGESTION != null) {
                        var existe = false;
                        $.each(estados, function (i, item) {
                            if (item.KeyName.toUpperCase() == data2.ESTADO_CASO_BDGESTION.toUpperCase()) {
                                existe = true;
                                data2.ESTADO_CASO_BDGESTION = item.KeyName;
                            }
                        });
                        if (existe) {
                            $("#cboEstadoCaso").val(data2.ESTADO_CASO_BDGESTION).trigger('change');
                            if (data != null && data2.ESTADO_CASO_BDGESTION == data.ESTADO_CASO) {
                                $("#cboMotivoCaso").val((data.MOTIVO_CASO == null || data.MOTIVO_CASO == '' ? '0' : data.MOTIVO_CASO)).trigger('change');
                                $("#cboAreaResponsable").val((data.AREA_RESPONSABLE == null || data.AREA_RESPONSABLE == '' ? '0' : data.AREA_RESPONSABLE));
                            }
                        }
                    }
                    if (data2.ESTADO_CASO_BDGESTION == 'Cerrado') {
                        alert('La base ya se encuentra en estado Cerrado');
                        salirMe();
                    }
                } else {
                    $("#loading").hide();
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#loading").hide();
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }

    this.ValidaFono = function ValidaFono(fono) {

        var retorno = true;
        var values = JSON.stringify({ fono: fono});

        $.ajax({
            url: "Main.aspx/ValidaFono",
            data: values,
            dataType: "json",
            type: "POST",
			async:false,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (response) {
                var r = response.d;

                $("#loading").hide();

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];

                    if (dat1 != null) {
                        if (dat1.KeyValue != "OK") {
                            retorno = false;
                            //alert("El teléfono a discar no es válido");
                            
                        }
                    }

                } else {
                    $("#loading").hide();
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#loading").hide();
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });

        return retorno;
    }

    this.CargaMotivoCaso = function CargaMotivoCaso(caso) {
        
        var values = JSON.stringify({ caso: caso });

        $.ajax({
            url: "Main.aspx/CargaMotivoCaso",
            data: values,
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (response) {
                var r = response.d;

                $("#loading").hide();

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];

                    if (dat1 != null) {
                        if (dat1 != null) {
                            $("#cboMotivoCaso").html("");
                            $("#cboMotivoCaso").removeAttr('disabled');
                            $("#cboMotivoCaso").append("<option value='0'>Seleccione...</option>");
                            $.each(dat1, function (i, d) {
                                if (d.KeyName != null) {
                                    $("#cboMotivoCaso").append("<option value='" + d.KeyName + "'>" + d.KeyName + "</option>");
                                }
                                
                            });
                            
                        }
                    }

                } else {
                    $("#loading").hide();
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#loading").hide();
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }
    this.CargaAreaCaso = function CargaAreaCaso(caso) {

        var values = JSON.stringify({ caso: caso });

        $.ajax({
            url: "Main.aspx/CargaAreaCaso",
            data: values,
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (response) {
                var r = response.d;

                $("#loading").hide();

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];

                    if (dat1 != null) {
                        if (dat1 != null) {
                            $("#cboAreaResponsable").html("");
                            $("#cboAreaResponsable").removeAttr('disabled');
                            $("#cboAreaResponsable").append("<option value='0'>Seleccione...</option>");
                            $.each(dat1, function (i, d) {
                                if (d.KeyName != null) {
                                    $("#cboAreaResponsable").append("<option value='" + d.KeyName + "'>" + d.KeyName + "</option>");
                                }
                            });
                        }
                    }

                } else {
                    $("#loading").hide();
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#loading").hide();
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }
   
    this.Graballamada = function Graballamada(datos) {

        var values = JSON.stringify({ datos: datos });

        $.ajax({
            url: "Main.aspx/Graballamada",
            data: values,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#toolbar").hide();
                $("#loading").show();
                $("#form").attr('disabled', 'disabled');
            },
            success: function (response) {
                var r = response.d;

                $("#toolbar").show();
                $("#loading").hide();
                $("#form").attr('disabled', false);

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];
                    alert("Ticket grabado con éxito\n\nNúmero de Ticket :" + dat1.Numerollamada);

                    $("#txtIdaplicacioncl").val('');
                    $("#txttelefono").val('');
                    $("#txtObsllamada").val('');
                    $("#cboResultadollamada").val('0').trigger("change");
                    $("#txtRutusuario").val('');
                    $("#txtNombreusuario").val('');
                    $("#txtEmail").val('');
                    $("#cboCasuistica").val('0');
                    //document.getElementById('chcerro').checked = false;
                    $("#hidfonodiscado").val('');

                    $("#txtFechaSeguimiento").val('');
                    $("#cboEstadoCaso").val(0);
                    $("#cboMotivoCaso").val(0);
                    $("#cboMotivoCaso").attr('disabled', 'disabled');
                    $("#cboAreaResponsable").val(0);
                    $("#cboAreaResponsable").attr('disabled', 'disabled');

                    $("#cboContacto").val(0);
                    $("#cboMotivoContacto").val(0);
                    $("#cboMotivoContacto").attr('disabled', 'disabled');
                    $("#cboResultado").val(0);
                    $("#cboResultado").attr('disabled', 'disabled');


                } else {
                    $("#toolbar").show();
                    $("#loading").hide();
                    $("#form").attr('disabled', false);
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#toolbar").show();
                $("#loading").hide();
                $("#form").attr('disabled', false);
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }

    this.GrabarCaso = function GrabarCaso(datos) {

        var values = JSON.stringify({ datos: datos, caso: $("#hidcaso").val(), numeroLlamada: $("#txtTicket").val(), idCaso: $("#hididcaso").val() });

        $.ajax({
            url: "Main.aspx/GrabarCaso",
            data: values,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#toolbar").hide();
                $("#loading").show();
                $("#form").attr('disabled', 'disabled');
            },
            success: function (response) {
                var r = response.d;

                $("#toolbar").show();
                $("#loading").hide();
                $("#form").attr('disabled', false);

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];
                    alert("Ticket grabado con éxito\n\nNúmero de Ticket :" + dat1.Numerollamada);

                    $("#txtIdaplicacioncl").val('');
                    $("#txttelefono").val('');
                    $("#txtObsllamada").val('');
                    $("#cboResultadollamada").val('0').trigger("change");
                    $("#txtRutusuario").val('');
                    $("#txtNombreusuario").val('');
                    $("#txtEmail").val('');
                    $("#cboCasuistica").val('0');
                    //document.getElementById('chcerro').checked = false;
                    $("#hidfonodiscado").val('');

                    $("#txtFechaSeguimiento").val('');
                    $("#cboEstadoCaso").val(0);
                    $("#cboMotivoCaso").val(0);
                    $("#cboMotivoCaso").attr('disabled', 'disabled');
                    $("#cboAreaResponsable").val(0);
                    $("#cboAreaResponsable").attr('disabled', 'disabled');

                    $("#cboContacto").val(0);
                    $("#cboMotivoContacto").val(0);
                    $("#cboMotivoContacto").attr('disabled', 'disabled');
                    $("#cboResultado").val(0);
                    $("#cboResultado").attr('disabled', 'disabled');

                    ctlr_main.salirMe();
                } else {
                    $("#toolbar").show();
                    $("#loading").hide();
                    $("#form").attr('disabled', false);
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#toolbar").show();
                $("#loading").hide();
                $("#form").attr('disabled', false);
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }

    this.salirMe = function salirMe() {
        if ((BrowserDetect.browser == "Explorer" && (BrowserDetect.version == "8" || BrowserDetect.version == "7"))) {
            window.open('', '_self', '');
            window.close();
        } else if ((BrowserDetect.browser == "Explorer" && BrowserDetect.version == "6")) {
            window.opener = null;
            window.close();
        } else {
            window.opener = null;
            window.close(); // attempt to close window first, show user warning message if fails
            //alert("To avoid data corruption/loss. Please close this window immedietly.");
        }
    }   

    this.CargaMotivoContacto = function CargaMotivoContacto(datos) {

        var values = JSON.stringify({ datos: datos });

        $.ajax({
            url: "Main.aspx/CargaMotivoContacto",
            data: values,
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (response) {
                var r = response.d;

                $("#loading").hide();

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];

                    if (dat1 != null) {
                        if (dat1 != null) {
                            $("#cboMotivoContacto").html("");
                            $("#cboMotivoContacto").removeAttr('disabled');
                            $("#cboMotivoContacto").append("<option value='0'>Seleccione...</option>");
                            $.each(dat1, function (i, d) {
                                if (d.KeyName != null) {
                                    $("#cboMotivoContacto").append("<option value='" + d.KeyName + "'>" + d.KeyName + "</option>");
                                }
                            });
                        }
                    }

                } else {
                    $("#loading").hide();
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#loading").hide();
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }

    this.CargaResultadoContacto = function CargaResultadoContacto(datos) {

        var values = JSON.stringify({ datos: datos });

        $.ajax({
            url: "Main.aspx/CargaResultadoContacto",
            data: values,
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (response) {
                var r = response.d;

                $("#loading").hide();

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];

                    if (dat1 != null) {
                        if (dat1 != null) {
                            $("#cboResultado").html("");
                            $("#cboResultado").removeAttr('disabled');
                            $("#cboResultado").append("<option value='0'>Seleccione...</option>");
                            $.each(dat1, function (i, d) {
                                if (d.KeyName != null) {
                                    $("#cboResultado").append("<option value='" + d.KeyName + "'>" + d.KeyName + "</option>");
                                }
                            });
                        }
                    }

                } else {
                    $("#loading").hide();
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#loading").hide();
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }

    this.CargaHistorial = function CargaHistorial(idAplicacion) {

        var values = JSON.stringify({ idAplicacion: idAplicacion});

        $.ajax({
            url: "Main.aspx/CargaHistorial",
            data: values,
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading").show();
            },
            success: function (response) {
                var r = response.d;

                $("#loading").hide();

                if (r.ret == 'OK') {
                    var dat1 = response.d.values[0];
                    var html = "";
                    if (dat1 != null) {
                        $.each(dat1, function (i, c) {

                            html += "<tr>";
                            html += "<td>" + mynull(c.Fechallamada) + "</td>";
                            html += "<td>" + mynull(c.EstadoCaso) + "</td>";
                            html += "<td>" + mynull(c.MotivoCaso) + "</td>";
                            html += "<td>" + mynull(c.AreaResponsable) + "</td>";
                            html += "<td>" + mynull(c.Seguimiento) + "</td>";
                            html += "<td>" + mynull(c.Contacto) + "</td>";
                            html += "<td>" + mynull(c.MotivoContacto) + "</td>";
                            html += "<td>" + mynull(c.Resultado) + "</td>";
                            html += "<td>" + mynull(c.Observaciones) + "</td>";
                            html += "</tr>";
           
                        });

                    }
                    $("#tbhistorial > tbody").html("");
                    $("#tbhistorial > tbody").append(html);

                } else {
                    $("#loading").hide();
                    alert("error : " + r.msg);
                }
            },
            error: function (response) {
                $("#loading").hide();
                alert("ERROR " + response.status + '\n\n' + response.statusText + '\n\n' + response.responseText);
            }
        });
    }
    
}