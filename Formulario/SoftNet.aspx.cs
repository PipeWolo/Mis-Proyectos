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
            object param = Request.QueryString["param"].ToString();

            if (param != null && softphone != null)
            {
                if (param.Equals("Discar")) {
                    string fono = Request.QueryString["fono"].ToString();
                    string skill = Request.QueryString["skill"].ToString();
                    string CodigoServicio = Request.QueryString["CodigoServicio"].ToString();

                    softphone.DiscarFono(fono, CodigoServicio, skill);

                    this.hiderror.Value = softphone.MsgError;
                } else if (param.Equals("Transferencia"))
                {
                    string vdn = Request.QueryString["vdn"].ToString();
                    softphone.Discar(vdn);
                }
            }
        }
    }
}