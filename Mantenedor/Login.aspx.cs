using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Navigator.Clases;
using Navigator.Librerias;
using Navigator.Login;
using System.Security.Cryptography;
using System.Text;
using Navigator.GestionCuenta;

public partial class PaginaLogin : System.Web.UI.Page
{
    private void Mensaje(string tipo, string msg)
    {
        this.panError.Visible = true;
        this.litMensaje.Text = Utilidades.Mensaje("", tipo, msg);
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            IniciarSesion();
        }
    }

    protected void IniciarSesion()
    {
        var usuario = txtUsuario.Value;
        var contrasena = txtPassword.Value;
        var ajax = new ControlAcceso();
        if (usuario.Length == 0 || contrasena.Length == 0)
        {
            if (usuario.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctlr_login.Mensaje('Validación','Debe ingresar su nombre de usuario y contraseña para continuar.','info','#txtUsuario');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctlr_login.Mensaje('Validación','Debe ingresar su nombre de usuario y contraseña para continuar.','info','#txtContrasena');", true);
                return;
            }
        }

        var data = ajax.Login(usuario, contrasena);

        if (data.ret != "OK")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctlr_login.Mensaje('Error al iniciar sesión','" + data.msg + "','error','#txtUsuario');", true);
            return;
        }
        else
        {
            var loggedUser = (Usuario)data.values[0];
            Session.Add("LoggedUser", loggedUser);

            var menu = (List<MapaAcceso>)data.values[1];

            if (loggedUser.CAMBIO_CONTRASENA == "1")
            {

                Response.Redirect("Cuenta/ChangePassword.aspx");
            }
            else
            {
                string menuJSON = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(menu);
                Session.Add("menuJSON", menuJSON);

                string menuHTML = GeneraMenu(menu);
                Session.Add("menuHTML", menuHTML);

                Route(menu);
            }
        }
    }

    private string GeneraMenu(List<MapaAcceso> mapa)
    {
        string menu = string.Empty;
        int indice = 0;
        foreach (MapaAcceso modulo in mapa)
        {
            if (modulo.ID_MODULO_PADRE == "0")
            {
                if (modulo.TIENE_HIJO == "1")
                {
                    if (!string.IsNullOrEmpty(modulo.SECCION))
                    {
                        menu += "<li class='menu-header'>";
                        menu += modulo.SECCION;
                        menu += "</li>";
                    }

                    menu += "<li class='dropdown' data-modulo='" + modulo.ID_MODULO + "'>";
                    menu += "  <a href='#' class='nav-link has-dropdown' data-toggle='dropdown'><i class='" + modulo.ICONO + "'></i><span>" + modulo.DESCRIPCION + "</span></a>";
                    menu += "  <ul class='dropdown-menu'>";
                    List<MapaAcceso> hijos = mapa.Where(x => x.ID_MODULO_PADRE == modulo.ID_MODULO).OrderBy(x => x.ORDEN_HIJO).ToList();
                    foreach (MapaAcceso hijo in hijos)
                    {
                        menu += "    <li data-modulo='" + hijo.ID_MODULO + "' data-modulo-padre='" + hijo.ID_MODULO_PADRE + "'><a class='nav-link' href='" + ResolveUrl(hijo.RUTA) + "'>" + hijo.DESCRIPCION + "</a></li>";
                    }
                    menu += "  </ul>";
                    menu += "</li>";

                    indice += 1;
                }
                else
                {
                    if (!string.IsNullOrEmpty(modulo.SECCION))
                    {
                        menu += "<li class='menu-header'>";
                        menu += modulo.SECCION;
                        menu += "</li>";
                    }

                    menu += "<li data-modulo='" + modulo.ID_MODULO + "'><a class='nav-link' href='" + ResolveUrl(modulo.RUTA) + "'><i class='" + modulo.ICONO + "'></i><span>" + modulo.DESCRIPCION + "</span></a></li>";

                }
            }
        }

        return menu;
    }

    private void Route(List<MapaAcceso> mapa)
    {
        if (Request.QueryString["r"] == null)
        {
            if (mapa.Count() > 0)
            {
                var prueba = mapa.First().RUTA;
                Response.Redirect(mapa.First().RUTA);
            }
            else
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
        }
        else
            Response.Redirect(Request.QueryString["r"]);
    }

    [WebMethod]
    public static RetornoAjax NotificacionVencimientoContrasena(string id_usuario)
    {
        var ajax = new NavigatorGestionCuenta();
        return ajax.NotificacionVencimientoContrasena(id_usuario);
    }
}