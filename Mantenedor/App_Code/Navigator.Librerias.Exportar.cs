using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Navigator.Clases;

namespace Navigator.Librerias
{
    /// <summary>
    /// Motor Exportacion
    /// </summary>
    public class Exportar
    {

        //public List<Tipificacion> ListaTipificacion { get; set; }
        //public List<Novedad> ListaNovedades { get; set; }

        //public StringBuilder Excel(Tabla tabla)
        //{
        //    StringBuilder ret = new StringBuilder();

        //    switch(tabla)
        //    {
        //       case Tabla.Novedades:
        //          ret = Novedades();
        //          break;
        //    }

        //    return ret;
        //}

        //private StringBuilder Header()
        //{
        //    StringBuilder html = new StringBuilder();

        //    int comilladi = 34;
        //    int arroai = 64;
        //    int backsi = 92;

        //    char comilladc = (char)comilladi;
        //    char arroac = (char)arroai;
        //    char backsc = (char)backsi;

        //    string comillads = comilladc.ToString();
        //    string arroas = arroac.ToString();
        //    string backss = backsc.ToString();

        //    html.Append("<html>" + Environment.NewLine);
        //    html.Append("<head>" + Environment.NewLine);
        //    html.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
        //    html.Append("<title></title>" + Environment.NewLine);
        //    html.Append("<style>" + Environment.NewLine);
        //    html.Append("#reporteria" + Environment.NewLine);
        //    html.Append("{" + Environment.NewLine);
        //    html.Append("font-family: Verdana, Arial, Helvetica, sans-serif;" + Environment.NewLine);
        //    html.Append("font-size: 10px;" + Environment.NewLine);
        //    html.Append("margin: 0px;" + Environment.NewLine);
        //    html.Append("width: 800px;" + Environment.NewLine);
        //    html.Append("text-align: left;" + Environment.NewLine);
        //    html.Append("border-collapse: collapse;" + Environment.NewLine);
        //    html.Append("}" + Environment.NewLine);
        //    html.Append(Environment.NewLine);
        //    html.Append("#reporteria th" + Environment.NewLine);
        //    html.Append("{" + Environment.NewLine);
        //    html.Append("padding: 8px;" + Environment.NewLine);
        //    html.Append("font-weight:bold;" + Environment.NewLine);
        //    html.Append("font-size: 12px;" + Environment.NewLine);
        //    html.Append("color: #fff;" + Environment.NewLine);
        //    html.Append("background: #0073AE;" + Environment.NewLine);
        //    html.Append("}" + Environment.NewLine);
        //    html.Append(Environment.NewLine);
        //    html.Append("#reporteria td" + Environment.NewLine);
        //    html.Append("{" + Environment.NewLine);
        //    html.Append("background: #e5f1f4; " + Environment.NewLine);
        //    html.Append("border-top: 1px solid #e5f1f4;" + Environment.NewLine);
        //    html.Append("border-collapse: collapse;" + Environment.NewLine);
        //    html.Append("font-size: 11px;" + Environment.NewLine);
        //    html.Append("color: #669;" + Environment.NewLine);
        //    html.Append("}" + Environment.NewLine);
        //    html.Append(Environment.NewLine);
        //    html.Append(".titulo" + Environment.NewLine);
        //    html.Append("{" + Environment.NewLine);
        //    html.Append("color:#FFFFFF;" + Environment.NewLine);
        //    html.Append("font-weight:bold;" + Environment.NewLine);
        //    html.Append("font-family:Arial;" + Environment.NewLine);
        //    html.Append("font-size:12px;" + Environment.NewLine);
        //    html.Append("}" + Environment.NewLine);
        //    html.Append(Environment.NewLine);
        //    html.Append(".titulo2" + Environment.NewLine);
        //    html.Append("{" + Environment.NewLine);
        //    html.Append("color:#4559A0;" + Environment.NewLine);
        //    html.Append("font-weight:bold;" + Environment.NewLine);
        //    html.Append("font-family:Arial;" + Environment.NewLine);
        //    html.Append("font-size:12px;" + Environment.NewLine);
        //    html.Append("}" + Environment.NewLine);
        //    html.Append(Environment.NewLine);
        //    html.Append(".text" + Environment.NewLine);
        //    html.Append("{" + Environment.NewLine);
        //    html.Append("mso-number-format:" + comillads + backss + arroas + comillads + ";" + Environment.NewLine);
        //    html.Append("}" + Environment.NewLine);
        //    html.Append("</style>" + Environment.NewLine);
        //    html.Append("</head>" + Environment.NewLine);
        //    html.Append("<body>" + Environment.NewLine);

        //    return html;
        //}

        //private StringBuilder Footer()
        //{
        //    StringBuilder ret = new StringBuilder();

        //    ret.Append("</body>");
        //    ret.Append("</html>");

        //    return ret;
        //}

        //private StringBuilder Novedades()
        //{
        //   StringBuilder ret = new StringBuilder();

        //   ret.Append(this.Header().ToString());
        //   ret.Append("<table id='reporteria'>");
        //   ret.Append("<tr>");
        //   ret.Append("<th>Id</th>");
        //   ret.Append("<th>Fecha Creación</th>");
        //   ret.Append("<th>Fecha Desde</th>");
        //   ret.Append("<th>Fecha Hasta</th>");
        //   ret.Append("<th>Descripción</th>");
        //   ret.Append("<th>Novedad</th>");
        //   ret.Append("<th>Activo</th>");

        //   ret.Append("</tr>");

        //   var query = (from q in this.ListaNovedades
        //                select q);

        //   foreach (var fila in query)
        //   {
        //      ret.Append("<tr>");
        //      ret.Append("<td>" + fila.Id + "</td>");
        //      ret.Append("<td>" + fila.FechaCreacion + "</td>");
        //      ret.Append("<td>" + fila.FechaDesde + "</td>");
        //      ret.Append("<td>" + fila.FechaHasta + "</td>");
        //      ret.Append("<td>" + fila.Descripcion + "</td>");
        //      ret.Append("<td>" + fila.Novedades + "</td>");
        //      ret.Append("<td>" + fila.Activo + "</td>");
        //      ret.Append("</tr>");
        //   }

        //   ret.Append("</table>");
        //   ret.Append(this.Footer().ToString());

        //   return ret;
        //}

        public Exportar()
        {
            
        }
    }
}