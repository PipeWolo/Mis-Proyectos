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
            if (!mapa.Any(w => w.ID_MODULO == "2"))
                Response.Redirect("~/Login.aspx");
        }

    }

    [WebMethod]
    public static RetornoAjax CargarCampana(FiltroCampana campana)
    {
        var ajax = new MantenedorCampanas();
        return ajax.CargarCampanas(campana, "", "");
    }

    [WebMethod]
    public static RetornoAjax Grabar(Campana campana)
    {
        var ajax = new MantenedorCampanas();
        return ajax.GrabarCampana(campana);
    }

    [WebMethod] 
    public static RetornoAjax CargarCampanaId(string CS)
    {
        var ajax = new MantenedorCampanas();
        return ajax.CargarCampanaId(CS);
    }

    [WebMethod]
    public static RetornoAjax ReciclarCampana(string CS, string Segmento)
    {
        var ajax = new MantenedorCampanas();
        return ajax.ReciclarCampana(CS, Segmento);
    }

}
