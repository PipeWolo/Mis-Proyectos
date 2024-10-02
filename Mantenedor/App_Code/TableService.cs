using Navigator.Ajax;
using Navigator.Clases;
using Navigator.Mantenedores;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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

    [WebMethod]
    public void CargarUsuarios(string Usuario, string IdPerfil)
    {
        var nvc = HttpContext.Current.Request.Form;
        var Response = HttpContext.Current.Response;
        var draw = nvc["draw"];
        var ID = "0";
        var start = nvc["start"];
        var length = nvc["length"];
        var search = nvc["search"];
        var paramCols = nvc.AllKeys.ToList().Where(w => w.StartsWith("columns"));
        var paramOrder = nvc.AllKeys.ToList().Where(w => w.StartsWith("order"));
        var orden = nvc["order[0][column]"];
        var direccion = nvc["order[0][dir]"];
        var filtro = new FiltroUsuario();
        var column = "";
        switch (orden.ToString())
        {
            case "1":
                column = "A.USUARIO";
                break;
            case "2":
                column = "A.NOMBRE";
                break;
            case "3":
                column = "B.PERFIL";
                break;
            case "4":
                column = "A.FECHA_CREACION";
                break;
            default:
                column = "A.USUARIO";
                break;
        }

        filtro.IdPerfil = IdPerfil;
        filtro.Usuario = Usuario;

        filtro.Cantidad = length;
        filtro.Pagina = start;

        var ajax = new MantenedorUsuarios();
        var ret = ajax.CargarUsuario(filtro, column, direccion);

        var atenciones = (List<Usuario>)ret.values[0];
        var total = int.Parse(((KVP)ret.values[1]).KeyValue);

        var result = new ResultDatatable();
        result.draw = Convert.ToInt32(draw);
        result.data = new List<object>();
        result.recordsFiltered = total;
        result.recordsTotal = total;

        atenciones.ForEach(t =>
        {
            result.data.Add(t);
        });

        var serializer = new JavaScriptSerializer();
        Response.Clear();
        Response.ClearHeaders();
        //Response.AddHeader("Content-Length", fileToDownload.Length.ToString());
        //Response.AddHeader("Content-Disposition", "attachment;filename=SampleTemplate.txt");
        Response.ContentType = "application/json";
        Response.Write(serializer.Serialize(result));
        Response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    [WebMethod]
    public void CargarBandeja(string CONNID, string SKILL, string FECHA, string ANI)
    {
        var nvc = HttpContext.Current.Request.Form;
        var Response = HttpContext.Current.Response;
        var draw = nvc["draw"];
        var ID = "0";
        var start = nvc["start"];
        var length = nvc["length"];
        var search = nvc["search"];
        var paramCols = nvc.AllKeys.ToList().Where(w => w.StartsWith("columns"));
        var paramOrder = nvc.AllKeys.ToList().Where(w => w.StartsWith("order"));
        var orden = nvc["order[0][column]"];
        var direccion = nvc["order[0][dir]"];
        var filtro = new FiltroBandeja();
        var column = "";
        switch (orden.ToString())
        {
            case "0":
                column = "CONNID";
                break;
            case "1":
                column = "AGENTE";
                break;
            case "2":
                column = "FECHA";
                break;
            case "3":
                column = "ANI";
                break;
            case "4":
                column = "RESPUESTA1";
                break;
            case "5":
                column = "RESPUESTA2";
                break;
            case "6":
                column = "RESPUESTA3";
                break;
            case "7":
                column = "FECHA";
                break;
            default:
                column = "FECHA DESC";
                break;
        }

        filtro.CONNID = CONNID;
        filtro.SKILL = SKILL;
        filtro.ANI = ANI;
        filtro.FECHA = FECHA;
        
        filtro.Cantidad = length;
        filtro.Pagina = start;

        var ajax = new MantenedorSMS();
        var ret = ajax.CargarBandeja(filtro, column, direccion);

        var atenciones = (List<Bandeja>)ret.values[0];
        var total = int.Parse(((KVP)ret.values[1]).KeyValue);

        var result = new ResultDatatable();
        result.draw = Convert.ToInt32(draw);
        result.data = new List<object>();
        result.recordsFiltered = total;
        result.recordsTotal = total;

        atenciones.ForEach(t =>
        {
            result.data.Add(t);
        });

        var serializer = new JavaScriptSerializer();
        Response.Clear();
        Response.ClearHeaders();
        //Response.AddHeader("Content-Length", fileToDownload.Length.ToString());
        //Response.AddHeader("Content-Disposition", "attachment;filename=SampleTemplate.txt");
        Response.ContentType = "application/json";
        Response.Write(serializer.Serialize(result));
        Response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    [WebMethod]
    public void CargarCampanas(string Campana)
    {
        var nvc = HttpContext.Current.Request.Form;
        var Response = HttpContext.Current.Response;
        var draw = nvc["draw"];
        var ID = "0";
        var start = nvc["start"];
        var length = nvc["length"];
        var search = nvc["search"];
        var paramCols = nvc.AllKeys.ToList().Where(w => w.StartsWith("columns"));
        var paramOrder = nvc.AllKeys.ToList().Where(w => w.StartsWith("order"));
        var orden = nvc["order[0][column]"];
        var direccion = nvc["order[0][dir]"];
        var filtro = new FiltroCampana();
        var column = "";
        switch (orden.ToString())
        {
            case "0":
                column = "A.CODIGO_SERVICIO";
                break;
            case "1":
                column = "A.NOMBRE_CAMPANA";
                break;
            case "3":
                column = "A.MODO_DISCADO";
                break;
            case "4":
                column = "A.PREFIJO";
                break;
            default:
                column = "A.NOMBRE_CAMPANA";
                break;
        }


        filtro.Cantidad = length;
        filtro.Pagina = start;

        var ajax = new MantenedorCampanas();
        var ret = ajax.CargarCampanas(filtro, column, direccion);

        var atenciones = (List<Campana>)ret.values[0];
        var total = int.Parse(((KVP)ret.values[1]).KeyValue);

        var result = new ResultDatatable();
        result.draw = Convert.ToInt32(draw);
        result.data = new List<object>();
        result.recordsFiltered = total;
        result.recordsTotal = total;

        atenciones.ForEach(t =>
        {
            result.data.Add(t);
        });

        var serializer = new JavaScriptSerializer();
        Response.Clear();
        Response.ClearHeaders();
        //Response.AddHeader("Content-Length", fileToDownload.Length.ToString());
        //Response.AddHeader("Content-Disposition", "attachment;filename=SampleTemplate.txt");
        Response.ContentType = "application/json";
        Response.Write(serializer.Serialize(result));
        Response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    [WebMethod]
    public void CargarSegmentoxAgente(string Agente, string CodigoServicio, string Segmento)
    {
        var nvc = HttpContext.Current.Request.Form;
        var Response = HttpContext.Current.Response;
        var draw = nvc["draw"];
        var ID = "0";
        var start = nvc["start"];
        var length = nvc["length"];
        var search = nvc["search"];
        var paramCols = nvc.AllKeys.ToList().Where(w => w.StartsWith("columns"));
        var paramOrder = nvc.AllKeys.ToList().Where(w => w.StartsWith("order"));
        var orden = nvc["order[0][column]"];
        var direccion = nvc["order[0][dir]"];
        var filtro = new FiltroSegmentoxAgente();
        var column = "";
        switch (orden.ToString())
        {
            case "0":
                column = "A.RUT";
                break;
            case "1":
                column = "A.SEGMENTO_AGENTE";
                break;
            case "3":
                column = "A.CAMPANA";
                break;
            case "4":
                column = "A.CODIGO_SERVICIO";
                break;
            default:
                column = "A.CAMPANA";
                break;
        }

        filtro.Agente = Agente;
        filtro.CodigoServicio = CodigoServicio;
        filtro.Segmento = Segmento;

        filtro.Cantidad = length;
        filtro.Pagina = start;

        var ajax = new MantenedorSegmentoxAgente();
        var ret = ajax.Cargar(filtro, column, direccion);

        var atenciones = (List<SegmentoxAgente>)ret.values[0];
        var total = int.Parse(((KVP)ret.values[1]).KeyValue);

        var result = new ResultDatatable();
        result.draw = Convert.ToInt32(draw);
        result.data = new List<object>();
        result.recordsFiltered = total;
        result.recordsTotal = total;

        atenciones.ForEach(t =>
        {
            result.data.Add(t);
        });

        var serializer = new JavaScriptSerializer();
        Response.Clear();
        Response.ClearHeaders();
        //Response.AddHeader("Content-Length", fileToDownload.Length.ToString());
        //Response.AddHeader("Content-Disposition", "attachment;filename=SampleTemplate.txt");
        Response.ContentType = "application/json";
        Response.Write(serializer.Serialize(result));
        Response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    [WebMethod]
    public void CargarReporteria(string CodigoServicio, string Segmento, string Fecha, string ResultadoLlamada, string MotivoLlamada, string ResultadoCampana, string MotivoCampana)
    {
        var nvc = HttpContext.Current.Request.Form;
        var Response = HttpContext.Current.Response;
        var draw = nvc["draw"];
        var ID = "0";
        var start = nvc["start"];
        var length = nvc["length"];
        var search = nvc["search"];
        var paramCols = nvc.AllKeys.ToList().Where(w => w.StartsWith("columns"));
        var paramOrder = nvc.AllKeys.ToList().Where(w => w.StartsWith("order"));
        var orden = nvc["order[0][column]"];
        var direccion = nvc["order[0][dir]"];
        var filtro = new FiltroReporteria();
        var column = "";
        switch (orden.ToString())
        {
            case "0":
                column = "NUMERO_LLAMADA";
                break;
            case "1":
                column = "RESULTADO_LLAMADO";
                break;
            case "3":
                column = "MOTIVO_LLAMADO";
                break;
            case "4":
                column = "RESULTADO_CAMPANA";
                break;
            case "5":
                column = "MOTIVO_CAMPANA";
                break;
            case "6":
                column = "NOMBRE_AGENTE";
                break;
            default:
                column = "NUMERO_LLAMADA";
                break;
        }

        filtro.CodigoServicio = CodigoServicio;
        filtro.Segmento = Segmento;

        string[] Fechas = Fecha.Split('-');
        string FechaDesde = Fechas[0].Trim();
        string FechaHasta = Fechas[1].Trim();

        filtro.FechaDesde = FechaDesde;
        filtro.FechaHasta = FechaHasta;
        filtro.ResultadoLlamada = ResultadoLlamada;
        filtro.MotivoLlamada = MotivoLlamada;
        filtro.ResultadoCampana = ResultadoCampana;
        filtro.MotivoCampana = MotivoCampana;

        filtro.Cantidad = length;
        filtro.Pagina = start;

        var ajax = new Reporteria();
        var ret = ajax.Cargar(filtro, column, direccion);

        var atenciones = (List<DatosReporteria>)ret.values[0];
        var total = int.Parse(((KVP)ret.values[1]).KeyValue);

        var result = new ResultDatatable();
        result.draw = Convert.ToInt32(draw);
        result.data = new List<object>();
        result.recordsFiltered = total;
        result.recordsTotal = total;

        atenciones.ForEach(t =>
        {
            result.data.Add(t);
        });

        var serializer = new JavaScriptSerializer();
        Response.Clear();
        Response.ClearHeaders();
        //Response.AddHeader("Content-Length", fileToDownload.Length.ToString());
        //Response.AddHeader("Content-Disposition", "attachment;filename=SampleTemplate.txt");
        Response.ContentType = "application/json";
        Response.Write(serializer.Serialize(result));
        Response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    private DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        //Get all the properties by using reflection   
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names  
            dataTable.Columns.Add(prop.Name, prop.PropertyType);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {

                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

}
