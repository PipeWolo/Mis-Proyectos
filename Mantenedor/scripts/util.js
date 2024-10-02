//**************************************
// Libreria de funciones JavaScript
// --------------------------------
// Autor : Luis Nuñez
// Fecha : 08/10/2008
// Version : 1.0
//
// Modificaciones
// --------------
// Fecha :
// Autor :
// Version : 
// Lista de Errores : 
// Lista de Modificaciones :
//**************************************

//**************************************                                        
// Valida que solo se acepten caracteres
//**************************************
function ValidaCaracteres(valor) {
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

//**************************************                                        
// Valida que solo se acepten mayusculas
//**************************************
function ValidaMayusculas(valor) {
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

//**************************************                                        
// Valida que solo se acepten minusculas
//**************************************
function ValidaMinusculas(valor) {
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

//**************************************                                        
// Valida que solo se acepten numeros
//**************************************
function ValidaNumeros(valor) {
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

//**************************************                                        
// Valida que solo se acepten digitos y K 
//**************************************
function valida_digito_rut(valor) {
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

//**************************************                                        
// Valida que solo se acepten caracteres y numeros
//**************************************
function ValidaAlfanumerico(valor) {
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

//*************************************************************
// Valida que solo se acepten caracteres validos para un correo
//*************************************************************
function ValidaCorreo(email) {
    var parsed = true;
    var validchars = "abcdefghijklmnñopqrstuvwxyz0123456789@.-_";
    for (var i = 0; i < email.length; i++) {
        var letter = email.charAt(i).toLowerCase();
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//*************************************************************
// Valida que solo se acepten caracteres validos segun juego
//*************************************************************
function ValidaEspeciales(juego, valor) {
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

function cortar_decimal(x, y) {

    var ret = (parseFloat(x) / parseFloat(y)).toString();

    if (ret.indexOf(".") != -1)
        ret = ret.substr(0, ret.indexOf("."));

    return parseFloat(ret);
}
//**********************************************************
// Pega desde el portapapeles y que sea valido segun el tipo
//**********************************************************
function doPaste(accion) {

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

//***************************************************************************
// Pega desde el portapapeles y que sea valido solo si estan dentro del juego
//***************************************************************************
function doPasteEsp(juego) {

    var valor = window.clipboardData.getData("Text");

    var ret = true;

    ret = ValidaEspeciales(juego, valor);

    if (!ret) window.event.returnValue = false;
}

//**********************************************************
// Valida que solo se acepten numeros
//**********************************************************
function SoloNumeros(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode > 31 && (charCode < 48 || charCode > 57)) return false;

    return true;
}

//**********************************************************
// Valida que solo se acepten mayusculas
//**********************************************************
function SoloMayusculas(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 65 && charCode <= 90) || charCode == 209) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten minusculas
//**********************************************************				
function SoloMinusculas(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 97 && charCode <= 122) || charCode == 241) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten letras
//**********************************************************				
function SoloLetras(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 209 || charCode == 241 || charCode == 32)) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten caracteres validos para correo
//**********************************************************								
function SoloCorreo(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (SoloLetras(evt) || SoloNumeros(evt) || charCode == 46 || charCode == 64 || charCode == 45 || charCode == 95) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten numeros y letras
//**********************************************************								
function SoloLetrasNumeros(evt) {
    if (SoloLetras(evt) || SoloNumeros(evt)) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten numeros y la K
//**********************************************************												
function SoloDigitoRut(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (SoloNumeros(evt) || charCode == 107 || charCode == 75) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten caracteres dentro de juego
//**********************************************************
function SoloEspeciales(evt, juego) {

    var charCode = (evt.which) ? evt.which : event.keyCode;

    for (var i = 0; i < juego.length; i++) {
        var letter = juego.charCodeAt(i);

        if (charCode == letter) return true;
    }

    return false;
}

function chars_restantes(textbox, largomax) {

    var chars_restantes = largomax - textbox.value.length;

    if (chars_restantes < 0) {
        alert('El numero maximo de caracteres permitidos es : ' + largomax);
        textbox.value = textbox.value.substring(0, largomax);
    }
}

function comprueba_rut(txtRut, txtDv) {
    if (txtRut.value == '')
        txtDv.value = '';
}

function calcular_digito_verificador(txtRut, txtDv) {

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
        if (rut.charAt(j) != ' ' && rut.charAt(j) != '.' && rut.charAt(j) != '-') {
            ParteNumerica[i] = rut.charAt(j);
            ++i;
        }
    }

    // Se calcula el digito verificador del rut
    for (i = ParteNumerica.length - 1; i >= 0; i--, NumMag++) {
        Suma += ParteNumerica[i] * NumMag;
        if (NumMag > 6) { NumMag = 1; }
    }

    Resto = 11 - (Suma % 11);

    txtDv.value = DigVer[Resto];

}

function valida_email(valor) {
    // creamos nuestra regla con expresiones regulares.
    var filter = /[\w-\.]{3,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    // utilizamos test para comprobar si el parametro valor cumple la regla
    if (filter.test(valor))
        return true;
    else
        return false;
}

function valida_rut(txtRut, txtDv) {

    // Definicion de Variables Utilizadas
    var Suma = 0;
    var NumMag = 2;
    var Resto = 0;
    var rut = txtRut;

    // Defino el arreglo con los posibles digitos verificadores
    var DigVer = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "K", "0");
    var ParteNumerica = new Array();

    // Valida que el rut no este vacio
    if (rut.length == 0)
        return false;

    // Copio solo la parte numerica, sin espacios ni puntos
    // en otra variable para calcular el digito verificador
    for (j = 0, i = 0; j < rut.length; j++) {
        if (rut.charAt(j) != ' ' && rut.charAt(j) != '.' && rut.charAt(j) != '-') {
            ParteNumerica[i] = rut.charAt(j);
            ++i;
        }
    }

    // Se calcula el digito verificador del rut
    for (i = ParteNumerica.length - 1; i >= 0; i--, NumMag++) {
        Suma += ParteNumerica[i] * NumMag;
        if (NumMag > 6) { NumMag = 1; }
    }

    Resto = 11 - (Suma % 11);

    if (txtDv == DigVer[Resto]) {
        return true;
    } else {
        return false;
    }
}

function myreplace(valor) {

    valor = valor.replace(/'/g, "");
    var regex = new RegExp('"', 'g');
    valor = valor.replace(regex, "");

    return valor;
}

//**********************************************************
// Valida que solo se acepten numeros, letras, caracteres especiales, sin comillas.
//**********************************************************								
function SoloLetrasNumeros8(evt) {
    if (SoloLetras8(evt) || SoloNumeros(evt)) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten letras sin comillas. 
//**********************************************************				
function SoloLetras8(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 209 || charCode == 241 || charCode == 32 || charCode == 33 || charCode == 35 || charCode == 36 || charCode == 37 || charCode == 38 || charCode == 40 || charCode == 41 || charCode == 42 || charCode == 43 || charCode == 45 || charCode == 46 || charCode == 47 || charCode == 58 || charCode == 59 || charCode == 60 || charCode == 61 || charCode == 62 || charCode == 63 || charCode == 64 || charCode == 95 || charCode == 123 || charCode == 124 || charCode == 125 || charCode == 126 || charCode == 91 || charCode == 92 || charCode == 93 || charCode == 191 || charCode == 161)) return true;

    return false;
}

function mynull(valor) {
    if (valor == 'null' || valor == null)
        return '';
    return valor;
}

function reset_combo(nom) {
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