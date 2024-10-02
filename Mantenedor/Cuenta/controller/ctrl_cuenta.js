var ctrl_cuenta = new ctrl_cuenta();

function ctrl_cuenta() {
    this.Init = function () {

    }

    this.SetListners = function SetListners() {
        $(".btnConfiguracionCuenta").click(function () {
            $("#modalConfiguracionCuenta").modal('show');
        });
        //$('#modalConfiguracionCuenta').on('shown.bs.modal', function () {
        //    $('.modal-backdrop').remove();
        //    if (!$("body").hasClass('sidebar-mini')) {
        //        $("a[data-toggle='sidebar']").trigger('click');
        //    }
        //});
    }

    this.Mensaje = function Mensaje(title, text, type, elemento, reload) {
        try {
            swal.fire(title, text, type).then(function () {
                ctrl_cuenta.StopLoader();
                if (elemento) {
                    $(elemento).focus();
                }
                if (reload) {
                    location.reload();
                }
            });
        }
        catch (err) {
            alert(title + ': ' + text);
            ctrl_cuenta.StopLoader();
            if (elemento) {
                $(elemento).focus();
            }
            if (reload) {
                location.reload();
            }
        }
    }


    this.StartLoader = function StartLoader() {
        $("#ctl00_btnConfiguracionCuentaCambiar").buttonLoader('start');
    }

    this.StopLoader = function StopLoader() {
        $("#ctl00_btnConfiguracionCuentaCambiar").buttonLoader('stop');
    }

    this.NotificacionVencimientoContrasena = function NotificacionVencimientoContrasena() {
        var avisoContrasena = sessionStorage.getItem('avisoContrasena');
        if (avisoContrasena != moment().format('DD/MM/YYYY')) {
            var id_usuario = sessionStorage.getItem('userid');
            var values = JSON.stringify({ id_usuario: id_usuario });
            var url = window.location.href.replace(/^(?:\/\/|[^/]+)*\//, '');
            var urlAjax = "../Login.aspx/NotificacionVencimientoContrasena";
            if (url.indexOf("Mantenedores") > -1) {
                urlAjax = "." + urlAjax;    
            }
            $.ajax({
                url: urlAjax,
                data: values,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var r = response.d;
                    if (r.ret == 'OK') {
                        var mensaje = r.values[0];
                        if (mensaje && mensaje.KeyValue && mensaje.KeyValue.length > 0) {
                            if (mensaje.KeyValue.toLowerCase().includes("caducada")) {
                                swal.fire({
                                    title: "Advertencia",
                                    text: mensaje.KeyValue,
                                    icon: "warning",
                                    confirmButtonText: "Cambiar Contraseña",
                                    showCancelButton: false,
                                    allowOutsideClick: false
                                }).then(function (ok) {
                                    if (ok.value) {
                                        $(".btnConfiguracionCuenta").trigger('click');
                                    }
                                });
                            }
                            else {
                                sessionStorage.setItem('avisoContrasena', moment().format('DD/MM/YYYY'));
                                swal.fire({
                                    title: "Advertencia",
                                    text: mensaje.KeyValue,
                                    icon: "warning",
                                    confirmButtonText: "Cambiar contraseña",
                                    cancelButtonText: "Aceptar",
                                    showCancelButton: true,
                                    allowOutsideClick: false
                                }).then(function (ok) {
                                    if (ok.value) {
                                        $("#btnConfiguracionCuenta").trigger('click');
                                    }
                                });
                            }
                        } else {
                            sessionStorage.setItem('avisoContrasena', moment().format('DD/MM/YYYY'));
                        }
                    }
                }
            });
        }
    }

}