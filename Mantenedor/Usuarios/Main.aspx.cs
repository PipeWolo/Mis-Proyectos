using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Navigator.Clases;
using Navigator.Librerias;
using Navigator.Mantenedores;

public partial class Reportes_Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var loggedUser = (Usuario)Session["LoggedUser"];
        if (loggedUser == null)
            Response.Redirect("~/Login.aspx");
        else
        {
            string menuJSON = (string)Session["menuJSON"];
            List<MapaAcceso> mapa = new List<MapaAcceso>();
            mapa = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<MapaAcceso>>(menuJSON);
            if (!mapa.Any(w => w.ID_MODULO == "1"))
                Response.Redirect("~/Login.aspx");
        }

    }


    [WebMethod]
    public static RetornoAjax GrabarUsuario(Usuario usuario)
    {
        var ajax = new MantenedorUsuarios();
        return ajax.GrabarUsuario(usuario);
    }

    [WebMethod]
    public static RetornoAjax CargarUsuarioId(string ID_USUARIO)
    {
        var ajax = new MantenedorUsuarios();
        return ajax.CargarUsuarioId(ID_USUARIO);
    }

    [WebMethod]
    public static RetornoAjax EliminarUsuario(string ID_USUARIO)
    {
        var ajax = new MantenedorUsuarios();
        return ajax.EliminarUsuario(ID_USUARIO);
    }

    [WebMethod]
    public static RetornoAjax CargarCombosUsuario()
    {
        var ajax = new MantenedorUsuarios();
        return ajax.CargarCombosUsuario();
    }

    [WebMethod]
    public static RetornoAjax SubirCargaUsuarios(List<Usuario> usuarios)
    {
        var ajax = new MantenedorUsuarios();
        return ajax.SubirCargaUsuarios(usuarios);
    }
}