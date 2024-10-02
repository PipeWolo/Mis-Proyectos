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
        if (!Page.IsPostBack)
        {
            var loggedUser = (Usuario)Session["LoggedUser"];
            if (loggedUser == null)
                Response.Redirect("~/Login.aspx");
            else
            {
                string menuJSON = (string)Session["menuJSON"];
                List<MapaAcceso> mapa = new List<MapaAcceso>();
                mapa = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<MapaAcceso>>(menuJSON);
                if (!mapa.Any(w => w.ID_MODULO == "5"))
                    Response.Redirect("~/Login.aspx");
            }
        }
    }

    //[WebMethod]
    //public static RetornoAjax Cargar(FiltroReporteria filtro)
    //{

    //    Reporteria Serv = new Reporteria();

    //    return Serv.Cargar(filtro);

    //}

    [WebMethod]
    public static RetornoAjax CargaCombo()
    {

        MantenedorSegmentoxAgente Serv = new MantenedorSegmentoxAgente();

        return Serv.CargarCombo();

    }

    [WebMethod]
    public static RetornoAjax CargarTipificaciones(string CodigoServicio)
    {
        var ajax = new Reporteria();

        return ajax.CargarTipificaciones(CodigoServicio);
    }
}