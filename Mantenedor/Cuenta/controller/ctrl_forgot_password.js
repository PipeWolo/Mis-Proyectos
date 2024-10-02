var ctrl_forgot_password = new ctrl_forgot_password();

function ctrl_forgot_password() {
    this.Init = function () {
        $('.js-tilt').tilt({
            scale: 1.1
        });
    }

    this.SetListners = function SetListners() {
        $("#btnRecuperar").click(function () {
            var btn = $(this);
            var usuario = $("#txtRecuperarUsuario").val().trim();
            if (usuario.length == 0) {
                ctrl_forgot_password.Mensaje("Validación", "Debe ingresar su login.", "info", "#txtRecuperarUsuario");
                return;
            } 
            ctrl_forgot_password.GeneraTokenOlvidoContrasena(btn, usuario);
        });
    }

    this.GeneraTokenOlvidoContrasena = function GeneraTokenOlvidoContrasena(btn, usuario) {
        var values = JSON.stringify({ usuario: usuario });
        $.ajax({
            url: "ForgotPassword.aspx/GeneraTokenOlvidoContrasena",
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
                    swal.fire("Operación exitosa", "Revise su casilla de correo para reestablecer su contraseña.", "success").then(function () {
                        location.href = "../Login.aspx";
                    });
                } else {
                    ctrl_forgot_password.Mensaje("Error", r.msg, "error");
                }
            },
            error: function (response) {
                $(btn).buttonLoader('stop');
                ctrl_forgot_password.Mensaje("Error", "Ocurrió un error inesperado, inténtelo mas tarde.", "error");
            }
        });
    }

    this.Mensaje = function Mensaje(title, text, type, elemento, reload) {
        try {
            swal.fire(title, text, type).then(function () {
                ctrl_forgot_password.StopLoader();
                if (elemento) {
                    $(elemento).focus();
                }
                if (reload) {
                    location.href = "../Login.aspx";
                }
            });
        }
        catch (err) {
            alert(title + ': ' + text);
            ctrl_forgot_password.StopLoader();
            if (elemento) {
                $(elemento).focus();
            }
            if (reload) {
                location.href = "../Login.aspx";
            }
        }
    }

    this.StartLoader = function StartLoader() {
        $("#btnConfiguracionCuentaCambiar").buttonLoader('start');
    }

    this.StopLoader = function StopLoader() {
        $("#btnConfiguracionCuentaCambiar").buttonLoader('stop');
    }

}