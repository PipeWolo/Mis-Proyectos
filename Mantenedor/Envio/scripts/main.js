$(document).ready(function () {

    try {
        ctlr_bandeja.Init();
        ctlr_bandeja.SetListeners();

        var filtro = new FiltroBandeja();

        //ctlr_bandeja.CargarCombosBandeja();
        ctlr_bandeja.Cargar(filtro);

        ctlr_master.ActivaPaginaMenu("2");
    } catch (ex) {
        console.log(ex.message);
    }
});