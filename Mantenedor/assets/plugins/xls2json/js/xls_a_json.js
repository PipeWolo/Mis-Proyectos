var xlsToJson = function (urlXls) {
    /* set up XMLHttpRequest */
    var url = urlXls;
    var oReq = new XMLHttpRequest();
    oReq.open("GET", url, true);
    oReq.responseType = "arraybuffer";

    oReq.onload = function (e) {
        var arraybuffer = oReq.response;
        /* Convierte data a un string binario */
        var data = new Uint8Array(arraybuffer);
        var arr = new Array();
        for (var i = 0; i != data.length; ++i) arr[i] = String.fromCharCode(data[i]);
        var bstr = arr.join("");

        /* llama al excel */
        var workbook = XLSX.read(bstr, {
            type: "binary"
        });

        /* Revisa los nombres del excel */
        var first_sheet_name = workbook.SheetNames[0];

        /* Obtiene el Worksheet */
        var worksheet = workbook.Sheets[first_sheet_name];
        console.log(XLSX.utils.sheet_to_json(worksheet, {
            raw: true
        }));
    }

    oReq.send();
};