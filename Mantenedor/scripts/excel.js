function ExportarTicket(desde, hasta,param) 
{
    $.ajax({
        url: "Reporteria.aspx/ExportarTicket",
        data: "{'desde':'" + desde + "','hasta':'" + hasta + "'}",
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
                var tkt = response.d.values[0];

                if (tkt != null) {
                    var url = "Exportar.aspx?param=" + param;
                    window.open(url, "", "width=10,height=10");
                }
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
            alert("ERROR " + response.status + ' ' + response.statusText);
        }
    });
}

function ExportarCliente(desde, hasta, param)
{
    $.ajax({
        url: "Reporteria.aspx/ExportarCliente",
        data: "{'desde':'" + desde + "','hasta':'" + hasta + "'}",
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
                var tkt = response.d.values[0];

                if (tkt != null) {
                    var url = "Exportar.aspx?param=" + param;
                    window.open(url, "", "width=10,height=10");
                }
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
            alert("ERROR " + response.status + ' ' + response.statusText);
        }
    });
}