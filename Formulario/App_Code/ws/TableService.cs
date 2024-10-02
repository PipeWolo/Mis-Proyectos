using Navigator.Ajax.Main;
using Navigator.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Descripción breve de TableService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
[ScriptService]
public class TableService : System.Web.Services.WebService
{

    public TableService()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
    }

    //[WebMethod]
    //public void CargarHistorial(string RUT)
    //{
    //    var nvc = HttpContext.Current.Request.Form;
    //    var Response = HttpContext.Current.Response;
    //    var draw = nvc["draw"];
    //    var ID = "0";
    //    var start = nvc["start"];
    //    var length = nvc["length"];
    //    var search = nvc["search"];
    //    var paramCols = nvc.AllKeys.ToList().Where(w => w.StartsWith("columns"));
    //    var paramOrder = nvc.AllKeys.ToList().Where(w => w.StartsWith("order"));
    //    var orden = nvc["order[0][column]"];
    //    var direccion = nvc["order[0][dir]"];
    //    var filtro = new FiltroGeneral();
    //    var column = "";
    //    switch (orden.ToString())
    //    {
    //        case "1":
    //            column = "N_REQUERIMIENTO";
    //            break;
    //        case "2":
    //            column = "RUT_CLIENTE";
    //            break;
    //        case "3":
    //            column = "NOMBRE_EMPRESA_PERSONA";
    //            break;
    //        case "4":
    //            column = "TO_DATE(FECHA_HORA_LLAMADA,'DD/MM/RRRR HH24:MI:SS')";
    //            break;
    //        case "5":
    //            column = "HABILIDAD";
    //            break;
    //        case "6":
    //            column = "OPERACION";
    //            break;
    //        case "7":
    //            column = "SUBOPERACION";
    //            break;
    //        case "8":
    //            column = "tipo";
    //            break;
    //        default:
    //            column = "TO_DATE(FECHA_HORA_LLAMADA,'DD/MM/RRRR HH24:MI:SS')";
    //            break;
    //    }

    //    filtro.campo1 = RUT;

    //    filtro.start = nvc["start"];
    //    filtro.length = nvc["length"];
    //    filtro.direction = nvc["order[0][dir]"];
    //    filtro.column = column;

    //    var result = new ResultDatatable();
    //    result.draw = Convert.ToInt32(draw);
    //    result.data = new List<object>();
    //    try
    //    {
    //        var ajax = new NavigatorMain();
    //        var ret = ajax.CargarHistorial(filtro);
    //        var listado = (List<Historial>)ret.values[0];
    //        var total = (int)ret.values[1];


    //        result.recordsFiltered = total;
    //        result.recordsTotal = total;

    //        listado.ForEach(t =>
    //        {                
    //            result.data.Add(t);
    //        });

    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    var serializer = new JavaScriptSerializer();
    //    Response.Clear();
    //    Response.ClearHeaders();
    //    Response.ContentType = "application/json";
    //    Response.Write(serializer.Serialize(result));
    //    Response.Flush();
    //    HttpContext.Current.ApplicationInstance.CompleteRequest();
    //}

}
