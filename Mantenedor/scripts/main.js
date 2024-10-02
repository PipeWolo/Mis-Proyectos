$(document).ready(function () {

    var cboresultadollamada = $("#cboResultadollamada");

    var telefono = $("#txttelefono");

    var tabs = $('#tabs');
    $('#tabs').tabs({});

    $("#loading").hide();
    $("#saving").hide();

    $("#txtObsllamada").keyup(function () {
        chars_restantes(document.form.txtObsllamada, 500);
    });
    $("#txttelefono").keypress(function (e) { return SoloNumeros(e) });
    $("#txtRutusuario").keypress(function (e) { return SoloNumeros(e) });

    $(telefono).bind('paste', function () { return doPaste(1) });

    ctlr_main.Cargaticket($("#hidagente").val());
    if ($("#hidcaso").val() != null && $("#hidcaso").val() != '') {
        ctlr_main.CargaTicketCaso($("#hidcaso").val(), $("#hididcaso").val());
    }

    var agente = $("#hidagente").val();
    var nombreagente = $("#hidnombreagente").val();
    var numerollamada = $("#hidnumerollamada").val();

    $("#btnDiscar").click(function () {
        var prefijo = $("#hidprefijo").val();
        var fono = $("#txttelefono").val();

        if (fono.length == '') {
            alert('Debe ingresar el número a discar');
            $("#txttelefono").focus();
            return false;
        }

        if (fono.length != 9) {
            alert('El teléfono debe contener 9 números');
            $("#txttelefono").focus();
            return false;
        }

        if (!ctlr_main.ValidaFono(fono)) {
            alert("El teléfono a discar no es válido");
            return false;
        }

        fono = prefijo + $("#txttelefono").val();

        Discar(fono);

        $("#hidfonodiscado").val(fono);

    });

    $("#btnGrabar").click(function () {

        var datos = new Ticket();

        datos.Idaplicacioncte = $.trim($("#txtIdaplicacioncl").val());
        datos.Telefono = $.trim($("#txttelefono").val());
        datos.Observaciones = $.trim($("#txtObsllamada").val());
        datos.Idtipificacion = $("#cboResultadollamada").val();
        datos.Tipificacion = $("#cboResultadollamada").find("option:selected").text();
        datos.Agente = $("#txtAgente").val();
        datos.Nombreagente = $("#txtNombreagente").val();
        datos.Rutusuario = $("#txtRutusuario").val();
        datos.Nombreusuario = $("#txtNombreusuario").val();

        datos.EstadoCaso = $("#cboEstadoCaso").find("option:selected").text();
        datos.MotivoCaso = $("#cboMotivoCaso").find("option:selected").text();
        datos.AreaResponsable = $("#cboAreaResponsable").find("option:selected").text();
        datos.Seguimiento = $("#txtFechaSeguimiento").val();
        datos.Contacto = $("#cboContacto").find("option:selected").text();
        datos.MotivoContacto = $("#cboMotivoContacto").find("option:selected").text();
        datos.Resultado = $("#cboResultado").find("option:selected").text();

        if (datos.EstadoCaso == 'Seleccione...') { datos.EstadoCaso = '' };
        if (datos.MotivoCaso == 'Seleccione...') { datos.MotivoCaso = '' };
        if (datos.AreaResponsable == 'Seleccione...') { datos.AreaResponsable = '' };
        if (datos.Contacto == 'Seleccione...') { datos.Contacto = '' };
        if (datos.MotivoContacto == 'Seleccione...') { datos.MotivoContacto = '' };
        if (datos.Resultado == 'Seleccione...') { datos.Resultado = '' };


        //if (document.getElementById('chcerro').checked) {
        //    datos.Cerroenlinea = '1';
        //} else {
        //    datos.Cerroenlinea = '0';
        //}
        
        //datos.Connid = $("#txtConnid").val();

        

        if (datos.Tipificacion == 'Seleccione...') {
            alert('Debe seleccionar la Tipificación');
            $("#cboResultadollamada").focus();
            return;
        }

        if (datos.Idaplicacioncte == '') {
            alert('Debe ingresar el campo ID Aplicación Cliente');
            $("#txtIdaplicacioncl").focus();
            return;
        }

        if (datos.Telefono == '') {
            alert('Debe ingresar el Número de Teléfono');
            $("#txttelefono").focus();
            return;
        }

        //if ($("#hidfonodiscado").val() == '') {
        //    alert('No ha discado ningún número de teléfono');
        //    return;
        //}
            

        if ($("#hidcaso").val() != null && $("#hidcaso").val() != '') {
            ctlr_main.GrabarCaso(datos);
        } else {
            ctlr_main.Graballamada(datos);
        }

    });

    $("#cboResultadollamada").change(function () {
        $("#cboCasuistica").val(0);
        $("#cboCasuistica").attr('disabled', 'disabled');
        if ($(this).val() == "5") {
            $("#cboCasuistica").removeAttr('disabled');
        }
    });

    $("#cboEstadoCaso").change(function () {
        $("#cboMotivoCaso").val(0);
        $("#cboMotivoCaso").attr('disabled', 'disabled');

        $("#cboAreaResponsable").val(0);
        $("#cboAreaResponsable").attr('disabled', 'disabled');
        var datos = new Caso();
        datos.Estado = $("#cboEstadoCaso").val();

        ctlr_main.CargaMotivoCaso(datos);
    });

    $("#cboMotivoCaso").change(function () {

        $("#cboAreaResponsable").val(0);
        $("#cboAreaResponsable").attr('disabled', 'disabled');
        var datos = new Caso();
        datos.Estado = $("#cboEstadoCaso").val();
        datos.Motivo = $("#cboMotivoCaso").val();

        ctlr_main.CargaAreaCaso(datos);
    });


    $("#cboContacto").change(function () {
        $("#cboMotivoContacto").val(0);
        $("#cboMotivoContacto").attr('disabled', 'disabled');

        $("#cboResultado").val(0);
        $("#cboResultado").attr('disabled', 'disabled');
        var datos = new DtContacto();
        datos.Contacto = $("#cboContacto").val();

        ctlr_main.CargaMotivoContacto(datos);
    });

    $("#cboMotivoContacto").change(function () {

        $("#cboResultado").val(0);
        $("#cboResultado").attr('disabled', 'disabled');
        var datos = new DtContacto();
        datos.Contacto = $("#cboContacto").val();
        datos.MotivoContacto = $("#cboMotivoContacto").val();

        ctlr_main.CargaResultadoContacto(datos);
    });

    $("#btnConsultar").click(function () {
        
        var idAplicacion = $.trim($("#txtIdaplicacioncl").val());

        if (idAplicacion == "") {
            alert('Debe ingresar un ID Aplicacion Cliente en la pestaña Datos Contacto');
            //$("#txtIdaplicacioncl").focus();
            return false;
        }


        ctlr_main.CargaHistorial(idAplicacion);

    });
});