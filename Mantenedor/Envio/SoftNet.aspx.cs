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
using Navigator.Ajax;
using Navigator.Softphone;
using System.Configuration;

public partial class SoftNet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            Softphone softphone = (Softphone)Session["softphone"];

            if (softphone != null)
            {
                string fono = Request.QueryString["fono"].ToString();
                string skill = Request.QueryString["skill"].ToString();
                string agente = Request.QueryString["agente"].ToString();

                softphone.DiscarFono(fono, "", skill);

            }        
        }
    }
}