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

using Navigator.Login;
using System.Security.Cryptography;
using System.Text;


public partial class PaginaLogin : System.Web.UI.Page
{
    private void Mensaje(string tipo, string msg)
    {
        this.panError.Visible = true;
        //this.litMensaje.Text = Utilidades.Mensaje("", tipo, msg);
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();

        if (!Page.IsPostBack)
        {
            object maquina = Request.QueryString["KeySP"];

            if (maquina != null && !String.IsNullOrEmpty(maquina.ToString()))
            {
                this.hKeySP.Value = maquina.ToString();
            }
            Session["Error"] = Request.QueryString["Error"];
            IniciarSesion();
        }
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
        var err = (string)Session["Error"];

        if (err != null){
            ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctlr_login.Mensaje('Validación','" + err + "','info');", true);
            return;
        }
            

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

            //var maquina = (string)Session["maquina"];
            //var agente = (string)Session["agente"];
            //Response.Redirect("~/Main.aspx?KeySP=" + maquina + "&RUT=" + agente);
            Session["ID"] = loggedUser.ID_USUARIO;
            Session["USUARIO"] = loggedUser.USUARIO;
            Session["CODIGO_SERVICIO"] = loggedUser.CODIGO_SERVICIO;

            Response.Redirect("Main.aspx?KeySP=" + this.hKeySP.Value);
        }
    }

}