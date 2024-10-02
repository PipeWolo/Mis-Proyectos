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
                if (!mapa.Any(w => w.ID_MODULO == "4"))
                    Response.Redirect("~/Login.aspx");
            }
        }
    }

    [WebMethod]
    public static RetornoAjax Cargar(FiltroCarga filtro)
    {

        ReporteCarga Serv = new ReporteCarga();

        return Serv.Cargar(filtro);

    }

    [WebMethod]
    public static RetornoAjax CargaCombo()
    {

        MantenedorSegmentoxAgente Serv = new MantenedorSegmentoxAgente();

        return Serv.CargarCombo();

    }

    [WebMethod]
    public static RetornoAjax NombreAgente(string rut)
    {

        MantenedorSegmentoxAgente Serv = new MantenedorSegmentoxAgente();

        return Serv.NombreAgente(rut);

    }

    [WebMethod]
    public static RetornoAjax Eliminar(string id)
    {

        MantenedorSegmentoxAgente Serv = new MantenedorSegmentoxAgente();

        return Serv.Eliminar(id);

    }

    [WebMethod]
    public static RetornoAjax Grabar(SegmentoxAgente SegmentoXAgente)
    {

        MantenedorSegmentoxAgente Serv = new MantenedorSegmentoxAgente();

        return Serv.Grabar(SegmentoXAgente);

    }
}