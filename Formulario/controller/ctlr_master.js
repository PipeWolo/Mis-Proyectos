var ctlr_master = new ctlr_master();

function ctlr_master() {

    this.Init = function () {
        if (!ctlr_master.DetectIE()) {
            // Si no es internet exporer
            $("#css-ie").remove();
        }
    }

    this.SetListeners = function SetListeners() {

        $(document).ajaxStart(function () {
            ctlr_master.Cargando('start');
        }).ajaxStop(function () {
            ctlr_master.Cargando('stop');
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
                swal({
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
                    swal({
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
                    swal(title, text, type);
                }
                catch (err) {
                    alert(title + ': ' + text);
                }
            }
        }
    }

    this.Cargando = function Cargando(mode) {
        switch (mode.toLowerCase()) {
            case 'start':
                var loading = $('.loading-spinner');
                if (loading.length == 0) {
                    $('.navbar').append('<i class="fas fa-circle-notch fa-spin loading-spinner" style="font-size:2em;display:none;"></i>');
                    $('.loading-spinner').fadeIn();
                }
                break;
            case 'stop':
                $('.loading-spinner').fadeOut(function () { $('.loading-spinner').remove(); });
                break;
            default:
        }

    }
}

