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
using Navigator.Softphone;
using System.Configuration;

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

        var url = ConfigurationManager.AppSettings.Get("URL");

        var ajax = new MantenedorSMS();
        RetornoAjax ret = ajax.CreaRequerimiento(loggedUser.ID_USUARIO, loggedUser.NOMBRE);
        try
        {
            if (ret.ret == "OK")
            {
                KVP value = (KVP)ret.values[0];
                txtIDRegistro.Value = value.KeyName;
                txtFechaIngreso.Value = value.KeyValue;
                txtHora.Value = value.KeyValue2;
                txtURL.Value = url;
            }
            else
            {
                txtIDRegistro.Value = "0";
                txtFechaIngreso.Value = DateTime.Now.ToString("dd/MM/yyyy");
                txtHora.Value = DateTime.Now.ToString("HH:mm:ss");
                txtURL.Value = url;
            }
        }
        catch (Exception)
        {
            txtIDRegistro.Value = "0";
            txtFechaIngreso.Value = DateTime.Now.ToString("dd/MM/yyyy");
            txtHora.Value = DateTime.Now.ToString("HH:mm:ss");
            txtURL.Value = url;
        }

    }

    [WebMethod]
    public static RetornoAjax EnvioSMS(SMS llamada)
    {
        var ajax = new MantenedorSMS();
        return ajax.Enviar(llamada);
    }

    [WebMethod]
    public static RetornoAjax CreaRequerimiento(string usuario, string nombreUsuario)
    {
        var ajax = new MantenedorSMS();
        return ajax.CreaRequerimiento(usuario, nombreUsuario);
    }

    //[WebMethod]
    //public static RetornoAjax CargarBandejaId(string ID)
    //{
    //    var ajax = new MantenedorBandeja();
    //    return ajax.CargarBandejaId(ID);
    //}
}
