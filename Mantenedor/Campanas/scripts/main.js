$(document).ready(function () {

    ctlr_campanas.Init();
    ctlr_campanas.SetListeners();

    var filtro = new FiltroCampana();

    //ctlr_clientes.CargarCombosAgendamientos();
    ctlr_campanas.Cargar(filtro);
    
    ctlr_master.ActivaPaginaMenu("2");
});