$(document).ready(function () {

    $('#tabs').tabs({});

    $("#btnCerrar").click(function () {
        salirMe();
    });

    proxy_ticket.Cargar();

});