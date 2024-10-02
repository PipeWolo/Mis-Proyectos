using System;
using System.Web;
using System.Web.Services;
using Navigator.Clases;
using Navigator.Librerias;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string nombre = Request.QueryString["nombre"].ToString();

        SFTP navigator = new SFTP();

        RetornoAjax ret = navigator.DescargarDocumento(nombre);

        if (ret.ret.Equals("OK")) {
            
            KVP adj = (KVP)ret.values[0];

            Byte[] buffer = Convert.FromBase64String(adj.KeyValue);

            if (buffer != null)
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = adj.KeyValue2;
                response.AddHeader("Content-Disposition", "attachment; filename=" + adj.KeyName);
                response.AddHeader("content-length", buffer.Length.ToString());
                response.BinaryWrite(buffer);

                response.End();
            }
        }
    }
}