$(document).ready(function () {

    ctlr_usuario.Init();
    ctlr_usuario.SetListeners();

    var filtro = new FiltroUsuario();
    ctlr_usuario.CargarCombosUsuario();
    ctlr_usuario.Cargar(filtro);

    ctlr_master.ActivaPaginaMenu("1");
});