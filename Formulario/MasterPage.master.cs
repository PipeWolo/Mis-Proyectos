using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Navigator.Ajax;
using Navigator.Clases;
public partial class MasterPage : System.Web.UI.MasterPage
{
    public Usuario loggedUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loggedUser = (Usuario)Session["LoggedUser"];
        }
    }
}
