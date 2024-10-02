function valida_fecha(fecha) {

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

function valida_rango_fechas(fdesde, fhasta) {

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

function valida_fecha_mayor(fdesde, fhasta) {

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


function valida_fecha_hora(fdesde, hdesde, fhasta,hhasta) {

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

function validarFechaMenorActual(date) {
    var ret = false;

    var afdesde = new Date();
    var fecha2 = date.split(" ");
    var afhasta = fecha2[0].split("/");

    var fec1 = afhasta[1] + "/" + afhasta[0] + "/" + afhasta[2] + " " + fecha2[1] + ":00";
    var fec2 = afdesde.getMonth() + 1 + "/" + afdesde.getDate() + "/" + afdesde.getFullYear() + " " + afdesde.getHours() + ":" + afdesde.getMinutes() + ":" + afdesde.getSeconds();

    var startDate = Date.parse(fec1);
    var endDate = Date.parse(fec2);

    if (startDate > endDate)
        ret = true;

    return ret;
}


function fecha_actual(date) {
    var d = new Date(date || Date.now()),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('/');
}

function fecha_hora_actual(date) {
    var d = new Date(date || Date.now()),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('/') + " " + d.getHours() + ":" + d.getMinutes() + ":59";
}

function addDays(date, days) {
    var result = new Date(date);
    result.setDate(date.getDate() + days);
    return result;
}

function fecha_hora_full() {
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