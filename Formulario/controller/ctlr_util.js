var ctlr_util = new ctlr_util();

function ctlr_util() {

    this.ValidaCaracteres = function ValidaCaracteres(valor) {
        var parsed = true;
        var validchars = "abcdefghijklmnñopqrstuvwxyz";

        for (var i = 0; i < valor.length; i++) {
            var letter = valor.charAt(i).toLowerCase();
            if (validchars.indexOf(letter) != -1)
                continue;
            parsed = false;
            break;
        }
        return parsed;
    }

    this.ValidaMayusculas = function ValidaMayusculas(valor) {
        var parsed = true;
        var validchars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
        for (var i = 0; i < valor.length; i++) {
            var letter = valor.charAt(i);
            if (validchars.indexOf(letter) != -1)
                continue;
            parsed = false;
            break;
        }
        return parsed;
    }

    this.ValidaMinusculas = function ValidaMinusculas(valor) {
        var parsed = true;
        var validchars = "abcdefghijklmnñopqrstuvwxyz";
        for (var i = 0; i < valor.length; i++) {
            var letter = valor.charAt(i);
            if (validchars.indexOf(letter) != -1)
                continue;
            parsed = false;
            break;
        }
        return parsed;
    }

    this.ValidaNumeros = function ValidaNumeros(valor) {
        var parsed = true;
        var validchars = "0123456789";
        for (var i = 0; i < valor.length; i++) {
            var letter = valor.charAt(i).toLowerCase();
            if (validchars.indexOf(letter) != -1)
                continue;
            parsed = false;
            break;
        }
        return parsed;
    }

    this.ValidaDigitoRut = function ValidaDigitoRut(valor) {
        var parsed = true;
        var validchars = "0123456789kK";
        for (var i = 0; i < valor.length; i++) {
            var letter = valor.charAt(i).toLowerCase();
            if (validchars.indexOf(letter) != -1)
                continue;
            parsed = false;
            break;
        }
        return parsed;
    }

    this.ValidaAlfanumerico = function ValidaAlfanumerico(valor) {
        var parsed = true;
        var validchars = "abcdefghijklmnñopqrstuvwxyz0123456789";
        for (var i = 0; i < valor.length; i++) {
            var letter = valor.charAt(i).toLowerCase();
            if (validchars.indexOf(letter) != -1)
                continue;
            parsed = false;
            break;
        }
        return parsed;
    }

    this.ValidaCorreo = function ValidaCorreo(email) {        
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    this.ValidaEspeciales = function ValidaEspeciales(juego, valor) {
        var parsed = true;
        var validchars = juego;
        for (var i = 0; i < valor.length; i++) {
            var letter = valor.charAt(i).toLowerCase();
            if (validchars.indexOf(letter) != -1)
                continue;
            parsed = false;
            break;
        }
        return parsed;
    }

    this.doPaste = function doPaste(accion) {

        var valor = window.clipboardData.getData("Text");

        var ret = true;

        if (accion == 1) {
            ret = ValidaNumeros(valor);
        } else if (accion == 2) {
            ret = ValidaCaracteres(valor);
        } else if (accion == 3) {
            ret = ValidaAlfanumerico(valor);
        } else if (accion == 4) {
            ret = ValidaCorreo(valor);
        } else if (accion == 5) {
            ret = ValidaMayusculas(valor);
        } else if (accion == 6) {
            ret = ValidaMinusculas(valor);
        } else if (accion == 7) {
            ret = ValidaDigitoRut(valor);
        }

        if (!ret) window.event.returnValue = false;
    }

    this.doPasteEsp = function doPasteEsp(juego) {

        var valor = window.clipboardData.getData("Text");

        var ret = true;

        ret = ValidaEspeciales(juego, valor);

        if (!ret) window.event.returnValue = false;
    }

    this.SoloNumeros = function SoloNumeros(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if (charCode > 31 && (charCode < 48 || charCode > 57)) return false;

        return true;
    }

    this.validaDirSer = function validaDirSer(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode != 46) {
            if (charCode > 31 && (charCode < 48 || charCode > 57)) return false;
        }

        return true;
    }

    this.SoloMayusculas = function SoloMayusculas(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if ((charCode >= 65 && charCode <= 90) || charCode == 209) return true;

        return false;
    }

    this.SoloMinusculas = function SoloMinusculas(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if ((charCode >= 97 && charCode <= 122) || charCode == 241) return true;

        return false;
    }

    this.SoloLetras = function SoloLetras(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 209 || charCode == 241 || charCode == 32)) return true;

        return false;
    }

    this.SoloCorreo = function SoloCorreo(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if (SoloLetras(evt) || SoloNumeros(evt) || charCode == 46 || charCode == 64 || charCode == 45 || charCode == 95) return true;

        return false;
    }

    this.SoloLetrasNumeros = function SoloLetrasNumeros(evt) {
        if (SoloLetras(evt) || SoloNumeros(evt)) return true;

        return false;
    }

    this.SoloDigitoRut = function SoloDigitoRut(evt) {

        var charCode = (evt.which) ? evt.which : event.keyCode;

        if (SoloNumeros(evt) || charCode == 107 || charCode == 75) return true;

        return false;
    }

    this.SoloEspeciales = function SoloEspeciales(evt, juego) {

        var charCode = (evt.which) ? evt.which : event.keyCode;

        for (var i = 0; i < juego.length; i++) {
            var letter = juego.charCodeAt(i);

            if (charCode == letter) return true;
        }

        return false;
    }

    this.CharsRestantes = function CharsRestantes(textbox, largomax) {

        var chars_restantes = largomax - textbox.value.length;

        if (chars_restantes < 0) {
            alert('El numero maximo de caracteres permitidos es : ' + largomax);
            textbox.value = textbox.value.substring(0, largomax);
        }
    }

    this.CompruebaRut = function CompruebaRut(txtRut, txtDv) {
        if (txtRut.value == '')
            txtDv.value = '';
    }

    this.CalcularDigitoVerificador = function CalcularDigitoVerificador(txtRut, txtDv) {
        
        // Definicion de Variables Utilizadas
        var Suma = 0;
        var NumMag = 2;
        var Resto = 0;
        var rut = txtRut.value;

        // Defino el arreglo con los posibles digitos verificadores
        var DigVer = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "K", "0");
        var ParteNumerica = new Array();

        txtDv.value = '';

        // Valida que el rut no este vacio
        if (rut.length == 0)
            return false;

        // Copio solo la parte numerica, sin espacios ni puntos
        // en otra variable para calcular el digito verificador
        for (j = 0, i = 0; j < rut.length; j++) {
            if (rut.charAt(j) != ' ' && rut.charAt(j) != '.' && rut.charAt(j) != '-' && rut.charAt(j) != '_') {
                ParteNumerica[i] = rut.charAt(j);
                ++i;
            }
        }

        // Se calcula el digito verificador del rut
        for (i = ParteNumerica.length - 1; i >= 0; i-- , NumMag++) {
            Suma += ParteNumerica[i] * NumMag;
            if (NumMag > 6) { NumMag = 1; }
        }

        Resto = 11 - (Suma % 11);
        if (DigVer[Resto]) {
            txtDv.value = DigVer[Resto];
        } else {
            txtDv.value = '-';
        }

    }

    this.revisarDigito = function revisarDigito(dvr) {
        dv = dvr + ""
        if (dv != '0' && dv != '1' && dv != '2' && dv != '3' && dv != '4' && dv != '5' && dv != '6' && dv != '7' && dv != '8' && dv != '9' && dv != 'k' && dv != 'K') {

            return false;
        }
        return true;
    }

    this.revisarDigito2 = function revisarDigito2(crut) {
        largo = crut.length;
        if (largo < 2) {
            return false;
        }
        if (largo > 2)
            rut = crut.substring(0, largo - 1);
        else
            rut = crut.charAt(0);
        dv = crut.charAt(largo - 1);
        ctlr_util.revisarDigito(dv);

        if (rut == null || dv == null)
            return 0

        var dvr = '0'
        suma = 0
        mul = 2

        for (i = rut.length - 1; i >= 0; i--) {
            suma = suma + rut.charAt(i) * mul
            if (mul == 7)
                mul = 2
            else
                mul++
        }
        res = suma % 11
        if (res == 1)
            dvr = 'k'
        else if (res == 0)
            dvr = '0'
        else {
            dvi = 11 - res
            dvr = dvi + ""
        }
        if (dvr != dv.toLowerCase()) {
            return false
        }
        return true
    }
    
    this.ValidaEmail = function ValidaEmail(valor) {
        // creamos nuestra regla con expresiones regulares.
        var filter = /[\w-\.]{3,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
        // utilizamos test para comprobar si el parametro valor cumple la regla
        if (filter.test(valor))
            return true;
        else
            return false;
    }

    this.ValidaDominio = function ValidaDominio(valor) {
        // creamos nuestra regla con expresiones regulares.
        var filter = /(\.){1}([A-Z])\w+/;
        // utilizamos test para comprobar si el parametro valor cumple la regla
        if (filter.test(valor))
            return true;
        else
            return false;
    }

    this.ValidaRut = function ValidaRut(txtRut, txtDv) {
        // Despejar Puntos
        var valor = txtRut.value.replace('.', '');
        // Despejar Guión
        valor = valor.replace('-', '');

        // Aislar Cuerpo y Dígito Verificador
        cuerpo = valor;
        dv = txtDv.value.toUpperCase();
                
        // Si no cumple con el mínimo ej. (n.nnn.nnn)
        if (cuerpo.length < 7) { return false; }

        // Calcular Dígito Verificador
        suma = 0;
        multiplo = 2;

        // Para cada dígito del Cuerpo
        for (i = 1; i <= cuerpo.length; i++) {

            // Obtener su Producto con el Múltiplo Correspondiente
            index = multiplo * valor.charAt(cuerpo.length - i);

            // Sumar al Contador General
            suma = suma + index;

            // Consolidar Múltiplo dentro del rango [2,7]
            if (multiplo < 7) { multiplo = multiplo + 1; } else { multiplo = 2; }

        }

        // Calcular Dígito Verificador en base al Módulo 11
        dvEsperado = 11 - (suma % 11);

        // Casos Especiales (0 y K)
        dv = (dv == 'K') ? 10 : dv;
        dv = (dv == 0) ? 11 : dv;

        // Validar que el Cuerpo coincide con su Dígito Verificador
        if (dvEsperado != dv) { return false; }

        return true;
    }

    this.MyReplace = function MyReplace(valor) {

        valor = valor.replace(/'/g, "");
        var regex = new RegExp('"', 'g');
        valor = valor.replace(regex, "");

        return valor;
    }

    this.SoloLetrasNumeros8 = function SoloLetrasNumeros8(evt) {
        if (SoloLetras8(evt) || SoloNumeros(evt)) return true;

        return false;
    }

    this.SoloLetras8 = function SoloLetras8(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 209 || charCode == 241 || charCode == 32 || charCode == 33 || charCode == 35 || charCode == 36 || charCode == 37 || charCode == 38 || charCode == 40 || charCode == 41 || charCode == 42 || charCode == 43 || charCode == 45 || charCode == 46 || charCode == 47 || charCode == 58 || charCode == 59 || charCode == 60 || charCode == 61 || charCode == 62 || charCode == 63 || charCode == 64 || charCode == 95 || charCode == 123 || charCode == 124 || charCode == 125 || charCode == 126 || charCode == 91 || charCode == 92 || charCode == 93 || charCode == 191 || charCode == 161)) return true;

        return false;
    }

    this.MyNull = function MyNull(valor) {
        if (valor == 'null' || valor == null)
            return '';
        return valor;
    }

    this.ResetCombo = function ResetCombo(nom) {
        var obj = $(nom); var opt = null;
        if (obj.prop) {
            opt = obj.prop('options');
        } else {
            opt = obj.attr('options');
        }
        $('option', obj).remove();
        opt[opt.length] = new Option('Seleccione ...', '0');

        return opt;
    }

    this.ResetCombo2 = function ResetCombo2(nom) {
        var obj = $(nom); var opt = null;
        if (obj.prop) {
            opt = obj.prop('options');
        } else {
            opt = obj.attr('options');
        }
        $('option', obj).remove();
        opt[opt.length] = new Option('Seleccione ...', '99');

        return opt;
    }

    this.ResetCombo3 = function ResetCombo3(nom) {
        var obj = $(nom); var opt = null;
        if (obj.prop) {
            opt = obj.prop('options');
        } else {
            opt = obj.attr('options');
        }
        $('option', obj).remove();
        opt[opt.length] = new Option('Seleccione ...', '999');

        return opt;
    }

    this.CortarDecimal = function CortarDecimal(x, y) {

        var ret = (parseFloat(x) / parseFloat(y)).toString();

        if (ret.indexOf(".") != -1)
            ret = ret.substr(0, ret.indexOf("."));

        return parseFloat(ret);
    }

    this.ValidaRutEspecial = function ValidaRutEspecial(rut) {

        if (rut.length == 1) {
            alert("Rut no válido");
            return false;
        }

        var largo = rut.length - 1;
        var rut2 = rut.substring(0, largo);
        var dv = rut.charAt(largo).toUpperCase();
        ret = valida_rut(rut2, dv);

        return ret;
    }

    this.ParametrosUrl = function ParametrosUrl(variable) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == variable) { return pair[1]; }
        }
        return (false);
    }

    this.GetComboText = function GetComboText(cbo) {
        return $(cbo).find("option:selected").text();
    }

    this.ValidaFecha = function ValidaFecha(fecha) {

        var formato = /^\d{2}\/\d{2}\/\d{4}$/

        if (!formato.test(fecha))
            return false;

        var day = fecha.split("/")[0]
        var month = fecha.split("/")[1]
        var year = fecha.split("/")[2]

        var valida = new Date(year, month - 1, day);
        var hoy = valida.getDay()
        var month = month - 1;
        var thedate = new Date(year, month, day);

        if (year != thedate.getFullYear())
            return false;

        if (month != thedate.getMonth())
            return false;

        if (day != thedate.getDate())
            return false;

        return true;
    }

    this.ValidaRangoFechas = function ValidaRangoFechas(fdesde, fhasta) {

        var ret = false;

        var afdesde = fdesde.split("/");
        var afhasta = fhasta.split("/");

        var fec1 = afdesde[1] + "/" + afdesde[0] + "/" + afdesde[2] + " " + "00:00:00";
        var fec2 = afhasta[1] + "/" + afhasta[0] + "/" + afhasta[2] + " " + "23:59:59";

        var startDate = Date.parse(fec1);
        var endDate = Date.parse(fec2);

        if (endDate > startDate)
            ret = true;

        return ret;
    }

    this.ValidaFechaMayor = function ValidaFechaMayor(fdesde, fhasta) {

        var ret = false;

        if (fdesde == fhasta)
            return false;

        var afdesde = fdesde.split("/");
        var afhasta = fhasta.split("/");

        var fec1 = afdesde[1] + "/" + afdesde[0] + "/" + afdesde[2] + " " + "00:00:00";
        var fec2 = afhasta[1] + "/" + afhasta[0] + "/" + afhasta[2] + " " + "23:59:59";

        var startDate = Date.parse(fec1);
        var endDate = Date.parse(fec2);

        if (endDate > startDate)
            ret = true;

        return ret;
    }

    this.ValidaFechaHora = function ValidaFechaHora(fdesde, hdesde, fhasta, hhasta) {

        var ret = false;

        var afdesde = fdesde.split("/");
        var afhasta = fhasta.split("/");

        var fec1 = afdesde[1] + "/" + afdesde[0] + "/" + afdesde[2] + " " + hdesde;
        var fec2 = afhasta[1] + "/" + afhasta[0] + "/" + afhasta[2] + " " + hhasta;

        var startDate = Date.parse(fec1);
        var endDate = Date.parse(fec2);

        if (endDate > startDate)
            ret = true;

        return ret;
    }

    this.FechaActual = function FechaActual(date) {
        var d = new Date(date || Date.now()),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [day, month, year].join('/');
    }

    this.FechaHoraActual = function FechaHoraActual(date) {
        var d = new Date(date || Date.now()),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [day, month, year].join('/') + " " + d.getHours() + ":" + d.getMinutes() + ":59";
    }

    this.addDays = function addDays(date, days) {
        var result = new Date(date);
        result.setDate(date.getDate() + days);
        return result;
    }

    this.FechaHoraFull = function FechaHoraFull() {
        var d = new Date();
        var fecha = d.getDate() + '/';
        if ((d.getMonth() + 1) < 10) {
            fecha = fecha + '0' + (d.getMonth() + 1);
        } else {
            fecha = fecha + (d.getMonth() + 1);
        }
        fecha = fecha + '/' + d.getFullYear();
        fecha = fecha + ' ';

        if (d.getHours() < 10) {
            fecha = fecha + '0' + d.getHours();
        } else {
            fecha = fecha + d.getHours();
        }
        fecha = fecha + ':';

        if (d.getMinutes() < 10) {
            fecha = fecha + '0' + d.getMinutes();
        } else {
            fecha = fecha + d.getMinutes();
        }
        fecha = fecha + ':';

        if (d.getSeconds() < 10) {
            fecha = fecha + '0' + d.getSeconds();
        } else {
            fecha = fecha + d.getSeconds();
        }

        return fecha;
    }

    this.Format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
                ? args[number]
                : match
                ;
        });
    };

    this.isIE = function (userAgent) {
        return /(MSIE|Trident\/)/i.test(navigator.userAgent);
    }

    this.ConfigObjectsPrototypes = function () {
        if (!Array.prototype.includes) {
            Object.defineProperty(Array.prototype, "includes", {
                enumerable: false,
                value: function (obj) {
                    var newArr = this.filter(function (el) {
                        return el == obj;
                    });
                    return newArr.length > 0;
                }
            });
        }
    }

    this.ConfigurarMenu = function () {
        var menu = sessionStorage.getItem("menuJSON");
        menu = JSON.parse(menu);
        //console.log(menu);
        $.each(menu, function (index, item) {
            $("[data-modulo='{0}']".format(item.modulo)).toggleClass("hidden");
            //console.log(item.modulo);
            var opciones = item.opciones.split(',');
            $.each(opciones, function (index, op) {
                $("[data-opcion='{0}.{1}']".format(item.modulo, op)).toggleClass("hidden");
                //console.log($("[data-opcion='{0}.{1}']".format(item.modulo, op)).toggleClass("hidden"));
            });
        });
    }

    this.formateaRut = function (rut) {

        var actual = rut.replace(/^0+/, "");
        if (actual != '' && actual.length > 1) {
            var sinPuntos = actual.replace(/\./g, "");
            var actualLimpio = sinPuntos.replace(/-/g, "");
            var inicio = actualLimpio.substring(0, actualLimpio.length - 1);
            var rutPuntos = "";
            var i = 0;
            var j = 1;
            for (i = inicio.length - 1; i >= 0; i--) {
                var letra = inicio.charAt(i);
                rutPuntos = letra + rutPuntos;
                if (j % 3 == 0 && j <= inicio.length - 1) {
                    rutPuntos = "" + rutPuntos;
                }
                j++;
            }
            var dv = actualLimpio.substring(actualLimpio.length - 1);
            rutPuntos = rutPuntos + "" + dv;
        }
        return rutPuntos;
    }

    this.ContadorCaracteres = function ContadorCaracteres(elemento, max, contador) {
        var cantidad = $(elemento).val().length;

        if (cantidad > max) {
            $(elemento).val($(elemento).val().slice(0, -1));
            cantidad = max;
        }

        $(contador).text(cantidad);
    }

}