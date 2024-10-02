var ctlr_master = new ctlr_master();

function ctlr_master() {

    this.Init = function () {
        if (!ctlr_master.DetectIE()) {
            // Si no es internet exporer
            $("#css-ie").remove();
        }
    }

    this.SetListeners = function SetListeners() {
        $("#btnConfiguracionCuenta").click(function () {
            var nombre = sessionStorage.getItem('soprole_inspector_nombre');
            $("#txtConfiguracionCuentaUsuario").val(nombre);
            $("#modal-configuracion-cuenta").modal('show');
        });
		
		$('.modal').on('hide.bs.modal', function () {
			$("body").removeClass('modal-open');
		});
    }

    this.DetectIE = function DetectIE() {
        var ua = window.navigator.userAgent;

        var msie = ua.indexOf('MSIE ');
        if (msie > 0) {
            // IE 10 or older => return version number: parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
            return true;
        }

        var trident = ua.indexOf('Trident/');
        if (trident > 0) {
            // IE 11 => return version number: parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
            var rv = ua.indexOf('rv:');
            return true;
        }

        // other browser
        return false;
    }

    this.ActivaPaginaMenu = function ActivaPaginaMenu(modulo) {
        $('.sidebar-menu li').removeClass('active');
        $('[data-modulo="' + modulo + '"]').addClass('active');
        var parent = $('[data-modulo="' + modulo + '"]').parent("ul");
        if (parent) {
            var parentli = parent.parent("li");
            if (parentli) { parentli.addClass('active'); }
        }
    }

    this.Mensaje = function Mensaje(title, text, type, focus, redirect) {
        if (redirect) {
            try {
                swal.fire({
                    title: title,
                    text: text,
                    icon: type
                }).then(function (result) {
                    window.location.href = redirect;
                });
            }
            catch (err) {
                alert(title + ': ' + text);
                window.location.href = redirect;
            }
        }
        else {
            if (focus) {
                try {
                    swal.fire({
                        title: title,
                        text: text,
                        icon: type
                    }).then(function (result) {
                        $(focus).focus();
                    });
                }
                catch (err) {
                    alert(title + ': ' + text);
                    $(focus).focus();
                }
            }
            else {
                try {
                    swal.fire(title, text, type);
                }
                catch (err) {
                    alert(title + ': ' + text);
                }
            }
        }
    }
}

