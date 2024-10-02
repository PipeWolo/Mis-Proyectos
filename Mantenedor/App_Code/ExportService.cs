using ClosedXML.Excel;
using Navigator.Ajax;
using Navigator.Clases;
using Navigator.Mantenedores;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Descripción breve de ExportService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
[ScriptService]
public class ExportService : System.Web.Services.WebService
{

    public ExportService()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
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

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }


    [WebMethod]
    public void ExcelPlantillaAgendamientos()
    {
        HttpResponse response = HttpContext.Current.Response;

        var pathServer = HttpContext.Current.Server.MapPath("~") + "/plantillas/";
        Byte[] bytes = File.ReadAllBytes(pathServer + "PlantillaCargaMasivaAgendamientos.xlsx");



        response.Clear();
        response.Buffer = true;
        response.Charset = "";
        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        response.AddHeader("content-disposition", "attachment;filename=PlantillaCargaMasivaAgendamientos.xlsx");
        response.BinaryWrite(bytes);
        response.Flush();
        response.End();
    }

    [WebMethod]
    public void DescargarPlantillaTipificaciones()
    {
        HttpResponse response = HttpContext.Current.Response;

        var pathServer = HttpContext.Current.Server.MapPath("~") + "/plantillas/";
        Byte[] bytes = File.ReadAllBytes(pathServer + "PlantillaCargaMasivaAgentesXCampanas.xlsx");

        response.Clear();
        response.Buffer = true;
        response.Charset = "";
        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        response.AddHeader("content-disposition", "attachment;filename=PlantillaAgenteXCampana.xlsx");
        response.BinaryWrite(bytes);
        response.Flush();
        response.End();
    }

    [WebMethod]
    public void DescargarPlantillaUsuarios()
    {
        HttpResponse response = HttpContext.Current.Response;

        var pathServer = HttpContext.Current.Server.MapPath("~") + "/plantillas/";
        Byte[] bytes = File.ReadAllBytes(pathServer + "PlantillaCargaMasivaUsuarios.xlsx");

        response.Clear();
        response.Buffer = true;
        response.Charset = "";
        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        response.AddHeader("content-disposition", "attachment;filename=PlantillaUsuarios.xlsx");
        response.BinaryWrite(bytes);
        response.Flush();
        response.End();
    }

    [WebMethod]
    public void ReporteCarga(string CodigoServicio, string Segmento, string tipo)
    {
        //var nvc = HttpContext.Current.Request.QueryString;
        //var datos = nvc["data"];
        //var dataDecoded = Base64Decode(datos);
        //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        //Dictionary<string, object> filtroDecoded = (Dictionary<string, object>)json_serializer.DeserializeObject(dataDecoded);
        //Dictionary<string, object> filtroInterno = (Dictionary<string, object>)filtroDecoded["filtro"];

        FiltroCarga filtro = new FiltroCarga();
        filtro.CodigoServicio = CodigoServicio;
        filtro.Segmento = Segmento;

        var ajax = new ReporteCarga();
        var ret = ajax.CargarTabla(filtro.CodigoServicio, filtro.Segmento, tipo);
        DataTable dt = new DataTable("registros");
        HttpResponse response = HttpContext.Current.Response;

        var data = (List<ReporteCargasTablas>)ret.values[0];

        if (ret.ret == "ERROR")
        {
            response.Clear();
            response.StatusCode = 400;
        }
        else
        {
            dt.Columns.Add("TELEFONO_MOVIL", typeof(System.String));
            dt.Columns.Add("TELEFONO_FIJO", typeof(System.String));
            dt.Columns.Add("TELEFONO_DEL_TRABAJO", typeof(System.String));
            dt.Columns.Add("TELEFONO_MOVIL_TRABAJO", typeof(System.String));
            dt.Columns.Add("CORREO_ELECTRONICO", typeof(System.String));
            dt.Columns.Add("CORREO_ELECTRONICO_2", typeof(System.String));
            dt.Columns.Add("CODIGO_CAMPANA", typeof(System.String));
            dt.Columns.Add("TIPO_DE_CAMPANA", typeof(System.String));
            dt.Columns.Add("NOMBRE_CAMPANA", typeof(System.String));
            dt.Columns.Add("CODIGO_SOCIO", typeof(System.String));
            dt.Columns.Add("RUT", typeof(System.String));
            dt.Columns.Add("NOMBRE_COMPLETO", typeof(System.String));
            dt.Columns.Add("SEGMENTO", typeof(System.String));
            dt.Columns.Add("COMUNA", typeof(System.String));
            dt.Columns.Add("ACUERDO", typeof(System.String));
            dt.Columns.Add("MONTO_APORTE", typeof(System.String));
            dt.Columns.Add("DIVISA", typeof(System.String));
            dt.Columns.Add("FUNDACION", typeof(System.String));
            dt.Columns.Add("ESTADO_DEL_ACUERDO", typeof(System.String));
            dt.Columns.Add("MECANISMO_RECAUDACION", typeof(System.String));
            dt.Columns.Add("TIPO_MEDIO_DE_PAGO", typeof(System.String));
            dt.Columns.Add("MEDIO_DE_PAGO", typeof(System.String));
            dt.Columns.Add("OFICINA_DE_VENTA", typeof(System.String));
            dt.Columns.Add("SEDE", typeof(System.String));
            dt.Columns.Add("FECHA_DE_CREACION", typeof(System.String));
            dt.Columns.Add("URL_CALL", typeof(System.String));
            dt.Columns.Add("MONTO_BASE", typeof(System.String));
            dt.Columns.Add("SEGMENTO_APORTE", typeof(System.String));
            dt.Columns.Add("MONTO_PROPUESTO", typeof(System.String));
            dt.Columns.Add("GENERICO1", typeof(System.String));
            dt.Columns.Add("GENERICO2", typeof(System.String));
            dt.Columns.Add("GENERICO3", typeof(System.String));
            dt.Columns.Add("GENERICO4", typeof(System.String));

            foreach (var fila in data)
            {

                DataRow row = dt.NewRow();

                row[0] = fila.TELEFONO_MOVIL;
                row[1] = fila.TELEFONO_FIJO;
                row[2] = fila.TELEFONO_DEL_TRABAJO;
                row[3] = fila.TELEFONO_MOVIL_TRABAJO;
                row[4] = fila.CORREO_ELECTRONICO;
                row[5] = fila.CORREO_ELECTRONICO_2;
                row[6] = fila.CODIGO_CAMPANA;
                row[7] = fila.TIPO_DE_CAMPANA;
                row[8] = fila.NOMBRE_CAMPANA;
                row[9] = fila.CODIGO_SOCIO;
                row[10] = fila.RUT;
                row[11] = fila.NOMBRE_COMPLETO;
                row[12] = fila.SEGMENTO;
                row[13] = fila.COMUNA;
                row[14] = fila.ACUERDO;
                row[15] = fila.MONTO_APORTE;
                row[16] = fila.DIVISA;
                row[17] = fila.FUNDACION;
                row[18] = fila.ESTADO_DEL_ACUERDO;
                row[19] = fila.MECANISMO_RECAUDACION;
                row[20] = fila.TIPO_MEDIO_DE_PAGO;
                row[21] = fila.MEDIO_DE_PAGO;
                row[22] = fila.OFICINA_DE_VENTA;
                row[23] = fila.SEDE;
                row[24] = fila.FECHA_DE_CREACION;
                row[25] = fila.URL_CALL;
                row[26] = fila.MONTO_BASE;
                row[27] = fila.SEGMENTO_APORTE;
                row[28] = fila.MONTO_PROPUESTO;
                row[29] = fila.GENERICO1;
                row[30] = fila.GENERICO2;
                row[31] = fila.GENERICO3;
                row[32] = fila.GENERICO4;

                dt.Rows.Add(row);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                var ws = wb.Worksheets.Worksheet(1);

                ws.Column("B").AdjustToContents();
                ws.Column("C").AdjustToContents();
                ws.Column("D").AdjustToContents();
                ws.Column("E").AdjustToContents();
                ws.Column("F").AdjustToContents();
                ws.Column("G").AdjustToContents();
                ws.Column("H").AdjustToContents();
                ws.Column("I").AdjustToContents();
                ws.Column("J").AdjustToContents();
                ws.Column("K").AdjustToContents();

                var name = "Registros" + tipo + ".xlsx";
                response.Clear();
                response.Buffer = true;
                response.Charset = "";
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", string.Format("attachment;filename={0}", name));

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.WriteTo(response.OutputStream);
                    response.Flush();
                    response.End();
                }
            }
        }
    }


    [WebMethod]
    public void Reporteria()
    {
        var nvc = HttpContext.Current.Request.QueryString;
        var datos = nvc["data"];
        FiltroReporteria filtro = new FiltroReporteria();
        
        var dataDecoded = Base64Decode(datos);
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        Dictionary<string, object> filtroDecoded = (Dictionary<string, object>)json_serializer.DeserializeObject(dataDecoded);
        Dictionary<string, object> filtroInterno = (Dictionary<string, object>)filtroDecoded["filtro"];

        filtro.CodigoServicio = filtroInterno["CodigoServicio"].ToString();
        filtro.Segmento = filtroInterno["Segmento"].ToString();

        string[] Fechas = filtroInterno["Fecha"].ToString().Split('-');
        string FechaDesde = Fechas[0].Trim();
        string FechaHasta = Fechas[1].Trim();

        filtro.FechaDesde = FechaDesde;
        filtro.FechaHasta = FechaHasta;

        filtro.ResultadoLlamada = filtroInterno["ResultadoLlamada"].ToString();
        filtro.MotivoLlamada = filtroInterno["MotivoLlamada"].ToString();
        filtro.ResultadoCampana = filtroInterno["ResultadoCampana"].ToString();
        filtro.MotivoCampana = filtroInterno["MotivoCampana"].ToString();
        filtro.Pagina = "0";
        filtro.Cantidad = "99";

        var orden = "NUMERO_LLAMADA";
        var direccion = "ASC";

        var ajax = new Reporteria();
        var ret = ajax.Cargar(filtro, orden, direccion);
        DataTable dt = new DataTable("registros");
        HttpResponse response = HttpContext.Current.Response;

        var data = (List<DatosReporteria>)ret.values[0];

        if (ret.ret == "ERROR")
        {
            response.Clear();
            response.StatusCode = 400;
        }
        else
        {
            dt.Columns.Add("NUMERO_LLAMADA", typeof(System.String));
            dt.Columns.Add("FECHA_INGRESO", typeof(System.String));
            dt.Columns.Add("CLIENTE", typeof(System.String));
            dt.Columns.Add("CAMPANA", typeof(System.String));
            dt.Columns.Add("CODIGO_SERVICIO", typeof(System.String));
            dt.Columns.Add("CONNID", typeof(System.String));
            dt.Columns.Add("SKILL", typeof(System.String));
            dt.Columns.Add("RUT_CLIENTE", typeof(System.String));
            dt.Columns.Add("DV", typeof(System.String));
            dt.Columns.Add("RUT_CLIENTE_DV", typeof(System.String));
            dt.Columns.Add("NOMBRE_CLIENTE", typeof(System.String));
            dt.Columns.Add("COMUNA", typeof(System.String));
            dt.Columns.Add("TELEFONO_MOVIL", typeof(System.String));
            dt.Columns.Add("TELEFONO_FIJO", typeof(System.String));
            dt.Columns.Add("TELEFONO_DEL_TRABAJO", typeof(System.String));
            dt.Columns.Add("TELEFONO_MOVIL_TRABAJO", typeof(System.String));
            dt.Columns.Add("CORREO_ELECTRONICO", typeof(System.String));
            dt.Columns.Add("CORREO_ELECTRONICO_2", typeof(System.String));
            dt.Columns.Add("CODIGO_CAMPANA", typeof(System.String));
            dt.Columns.Add("TIPO_DE_CAMPANA", typeof(System.String));
            dt.Columns.Add("NOMBRE_CAMPANA", typeof(System.String));
            dt.Columns.Add("CODIGO_SOCIO", typeof(System.String));
            dt.Columns.Add("SEGMENTO", typeof(System.String));
            dt.Columns.Add("MONTO_APORTE", typeof(System.String));
            dt.Columns.Add("DIVISA", typeof(System.String));
            dt.Columns.Add("FUNDACION", typeof(System.String));
            dt.Columns.Add("ACUERDO", typeof(System.String));
            dt.Columns.Add("ESTADO_DEL_ACUERDO", typeof(System.String));
            dt.Columns.Add("MECANISMO_RECAUDACION", typeof(System.String));
            dt.Columns.Add("TIPO_MEDIO_DE_PAGO", typeof(System.String));
            dt.Columns.Add("MEDIO_DE_PAGO", typeof(System.String));
            dt.Columns.Add("OFICINA_DE_VENTA", typeof(System.String));
            dt.Columns.Add("SEDE", typeof(System.String));
            dt.Columns.Add("MONTO_BASE", typeof(System.String));
            dt.Columns.Add("SEGMENTO_APORTE", typeof(System.String));
            dt.Columns.Add("FECHA_DE_CREACION", typeof(System.String));
            dt.Columns.Add("MONTO_PROPUESTO", typeof(System.String));
            dt.Columns.Add("URL_CALL", typeof(System.String));
            dt.Columns.Add("OBSERVACIONES", typeof(System.String));
            dt.Columns.Add("GENERICO1", typeof(System.String));
            dt.Columns.Add("GENERICO2", typeof(System.String));
            dt.Columns.Add("GENERICO3", typeof(System.String));
            dt.Columns.Add("GENERICO4", typeof(System.String));
            dt.Columns.Add("RESULTADO_LLAMADO", typeof(System.String));
            dt.Columns.Add("MOTIVO_LLAMADO", typeof(System.String));
            dt.Columns.Add("RESULTADO_CAMPANA", typeof(System.String));
            dt.Columns.Add("MOTIVO_CAMPANA", typeof(System.String));
            dt.Columns.Add("SEGMENTO_AGENTE", typeof(System.String));
            dt.Columns.Add("FECHAHORA_APERTURAFORM", typeof(System.String));
            dt.Columns.Add("FECHAHORA_GRABAFORM", typeof(System.String));
            dt.Columns.Add("TIEMPO_HABLADO", typeof(System.String));
            dt.Columns.Add("COD_TIPIFICACION1", typeof(System.String));
            dt.Columns.Add("COD_TIPIFICACION2", typeof(System.String));
            dt.Columns.Add("COD_TIPIFICACION3", typeof(System.String));
            dt.Columns.Add("AGENTE", typeof(System.String));
            dt.Columns.Add("NOMBRE_AGENTE", typeof(System.String));
            dt.Columns.Add("FECHA_ULTIMA_LLAMADA", typeof(System.String));
            dt.Columns.Add("INTENTOS", typeof(System.String));
            dt.Columns.Add("FECHA_AGENDAMIENTO", typeof(System.String));
            dt.Columns.Add("FONO_CONTACTO", typeof(System.String));

            foreach (var fila in data)
            {

                DataRow row = dt.NewRow();

                row[0] = fila.NUMERO_LLAMADA;
                row[1] = fila.FECHA_INGRESO;
                row[2] = fila.CLIENTE;
                row[3] = fila.CAMPANA;
                row[4] = fila.CODIGO_SERVICIO;
                row[5] = fila.CONNID;
                row[6] = fila.SKILL;
                row[7] = fila.RUT_CLIENTE;
                row[8] = fila.DV;
                row[9] = fila.RUT_CLIENTE_DV;
                row[10] = fila.NOMBRE_CLIENTE;
                row[11] = fila.COMUNA;
                row[12] = fila.TELEFONO_MOVIL;
                row[13] = fila.TELEFONO_FIJO;
                row[14] = fila.TELEFONO_DEL_TRABAJO;
                row[15] = fila.TELEFONO_MOVIL_TRABAJO;
                row[16] = fila.CORREO_ELECTRONICO;
                row[17] = fila.CORREO_ELECTRONICO_2;
                row[18] = fila.CODIGO_CAMPANA;
                row[19] = fila.TIPO_DE_CAMPANA;
                row[20] = fila.NOMBRE_CAMPANA;
                row[21] = fila.CODIGO_SOCIO;
                row[22] = fila.SEGMENTO;
                row[23] = fila.MONTO_APORTE;
                row[24] = fila.DIVISA;
                row[25] = fila.FUNDACION;
                row[26] = fila.ACUERDO;
                row[27] = fila.ESTADO_DEL_ACUERDO;
                row[28] = fila.MECANISMO_RECAUDACION;
                row[29] = fila.TIPO_MEDIO_DE_PAGO;
                row[30] = fila.MEDIO_DE_PAGO;
                row[31] = fila.OFICINA_DE_VENTA;
                row[32] = fila.SEDE;
                row[33] = fila.MONTO_BASE;
                row[34] = fila.SEGMENTO_APORTE;
                row[35] = fila.FECHA_DE_CREACION;
                row[36] = fila.MONTO_PROPUESTO;
                row[37] = fila.URL_CALL;
                row[38] = fila.OBSERVACIONES;
                row[39] = fila.GENERICO1;
                row[40] = fila.GENERICO2;
                row[41] = fila.GENERICO3;
                row[42] = fila.GENERICO4;
                row[43] = fila.RESULTADO_LLAMADO;
                row[44] = fila.MOTIVO_LLAMADO;
                row[45] = fila.RESULTADO_CAMPANA;
                row[46] = fila.MOTIVO_CAMPANA;
                row[47] = fila.SEGMENTO_AGENTE;
                row[48] = fila.FECHAHORA_APERTURAFORM;
                row[49] = fila.FECHAHORA_GRABAFORM;
                row[50] = fila.TIEMPO_HABLADO;
                row[51] = fila.COD_TIPIFICACION1;
                row[52] = fila.COD_TIPIFICACION2;
                row[53] = fila.COD_TIPIFICACION3;
                row[54] = fila.AGENTE;
                row[55] = fila.NOMBRE_AGENTE;
                row[56] = fila.FECHA_ULTIMA_LLAMADA;
                row[57] = fila.INTENTOS;
                row[58] = fila.FECHA_AGENDAMIENTO;
                row[59] = fila.FONO_CONTACTO;

                dt.Rows.Add(row);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                var ws = wb.Worksheets.Worksheet(1);

                ws.Column("B").AdjustToContents();
                ws.Column("C").AdjustToContents();
                ws.Column("D").AdjustToContents();
                ws.Column("E").AdjustToContents();
                ws.Column("F").AdjustToContents();
                ws.Column("G").AdjustToContents();
                ws.Column("H").AdjustToContents();
                ws.Column("I").AdjustToContents();
                ws.Column("J").AdjustToContents();
                ws.Column("K").AdjustToContents();

                var name = "Registros.xlsx";
                response.Clear();
                response.Buffer = true;
                response.Charset = "";
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", string.Format("attachment;filename={0}", name));

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.WriteTo(response.OutputStream);
                    response.Flush();
                    response.End();
                }
            }
        }
    }
}
