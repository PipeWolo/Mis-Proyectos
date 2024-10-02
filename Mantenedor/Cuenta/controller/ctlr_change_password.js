var ctrl_change_password = new ctrl_change_password();

function ctrl_change_password() {
    this.Init = function () {
        $('.js-tilt').tilt({
            scale: 1.1
        });
    }

    this.SetListners = function SetListners() {
    }

    this.Mensaje = function Mensaje(title, text, type, elemento, reload) {
        try {
            swal.fire(title, text, type).then(function () {
                ctrl_change_password.StopLoader();
                if (elemento) {
                    $(elemento).focus();
                }
                if (reload) {
                    location.href = location.href.split("?")[0];
                }
            });
        }
        catch (err) {
            alert(title + ': ' + text);
            ctrl_change_password.StopLoader();
            if (elemento) {
                $(elemento).focus();
            }
            if (reload) {
                location.href = location.href.split("?")[0];
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