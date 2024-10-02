var url = "http://192.168.223.189/prueba_transferencia/senddde.asp?DDECommand=";
var param = '';

function Discar(anexo) {

    var soft = $("#hidsoft").val();
    var cs = $("#hidCodigoServicioTrans").val();
    var skill = $("#hidSkillTrans").val();
    var prefijo = $("#hidprefijo").val();
    var agente = "";

    if (soft == '0') {
        param = "|306|" + agente + "|" + cs + "|" + skill + "|||2|" + anexo + "|";
        window.open(url + param, "ir_pie");
    } else {
        param = "param=makecall&vdn=" + anexo + "&cs=" + cs + "&skill=" + skill + "&agente=" + agente;
        //alert("param = " + param);
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}

function Transferencia_Asis(anexo) {

    var soft = $("#hidsoft").val();
    var cs = $("#hidCodigoServicioTrans").val();
    var skill = $("#hidSkillTrans").val();
    var agente = "";

    if (soft == '0') {
        param = "|306|" + agente + "|" + cs + "|" + skill + "|||2|" + anexo + "|";
        window.open(url + param, "ir_pie");
    } else {
        param = "param=306&vdn=" + anexo + "&cs=" + cs + "&skill=" + skill + "&agente=" + agente;
        //alert("param = " + param);
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}

function Discar_Conf(cs, agente, skill, anexo) {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        param = "|307|" + agente + "|" + cs + "|" + skill + "|||3|" + anexo + "|";
        window.open(url + param, "ir_pie");
    } else {
        param = "param=307&vdn=" + anexo + "&cs=" + cs + "&skill=" + skill + "&agente=" + agente;
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}

function CompletarTransferencia() {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        param = "|12|"
        window.open(url + param, "ir_pie");
    } else {
        param = "param=12";
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}

function CancelarTransferencia() {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        param = "|13|"
        window.open(url + param, "ir_pie");
    } else {
        param = "param=13";
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}

function CompletarConferencia() {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        param = "|15|"
        window.open(url + param, "ir_pie");
    } else {
        param = "param=15";
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}

function CancelarConferencia() {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        param = "|16|"
        window.open(url + param, "ir_pie");
    } else {
        param = "param=16";
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}

function AbandonarConferencia() {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        param = "|7|"
        window.open(url + param, "ir_pie");
    } else {
        param = "param=7";
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}

function TransfDirectaEspecial(skill, vdn, agente) {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        agente = $("#hidagente").val();
        cs = $("#hidcs").val();
        param = "|306|" + agente + "|" + cs + "|" + skill + "|" + "|" + "" + "|2|9*975*" + vdn + "|"
        window.open(url + param, "ir_pie");
    } else {

    }
}

function CortarOut() {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        param = "|7|"
        window.open(url + param, "ir_pie");
    } else {
        param = "param=cortar";
        window.open("../SoftNet.aspx?" + param, "ir_pie");
    }
}

function TransfDirectaMesa(skill, vdn, agente) {

    var soft = $("#hidsoft").val();

    if (soft == '0') {
        var agente = $("#hidagente").val();
        var cs = $("#hidcs").val();
        var param = "|306|" + agente + "|" + cs + "|" + skill + "|" + "|" + "" + "|2|" + vdn + "|"
        window.open(url + param, "ir_pie");
    } else {
        param = "param=mesa&vdn=" + vdn;
        window.open("SoftNet.aspx?" + param, "ir_pie");
    }
}