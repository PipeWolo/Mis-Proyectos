using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using Navigator.Clases;
using Navigator.Base;
using System.Security.Cryptography;

namespace Navigator.Mantenedores
{
    /// <summary>
    /// Mantenedor de Usuarios
    /// </summary>
    public class Reporteria : NavigatorBase 
    {
        public RetornoAjax Cargar(FiltroReporteria filtro, string orden, string dir)
        {
            RetornoAjax ret = new RetornoAjax();

            List<DatosReporteria> infoReporteria = new List<DatosReporteria>();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "100", "INPUT", filtro.CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSEGMENTO_AGENTE", "VARCHAR2", "100", "INPUT", filtro.Segmento));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PFECHA_DESDE", "VARCHAR2", "100", "INPUT", filtro.FechaDesde));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PFECHA_HASTA", "VARCHAR2", "100", "INPUT", filtro.FechaHasta));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PRESULTADO_LLAMADA", "VARCHAR2", "100", "INPUT", filtro.ResultadoLlamada));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PMOTIVO_LLAMADA", "VARCHAR2", "100", "INPUT", filtro.MotivoLlamada));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PRESULTADO_CAMPANA", "VARCHAR2", "100", "INPUT", filtro.ResultadoCampana));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PMOTIVO_CAMPANA", "VARCHAR2", "100", "INPUT", filtro.MotivoCampana));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSTART", "NUMERIC", "10", "INPUT", filtro.Pagina));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PLENGTH", "VARCHAR2", "10", "INPUT", filtro.Cantidad));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCOLUMNA", "VARCHAR2", "20", "INPUT", orden));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PDIR", "VARCHAR2", "10", "INPUT", dir));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_REPORTERIA",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var age = ds.Tables[0].AsEnumerable();
                    var tot = from item in ds.Tables[1].AsEnumerable() select item;

                    infoReporteria = (from item in age
                               select new DatosReporteria
                               {
                                   NUMERO_LLAMADA = Convert.ToString(item.Field<decimal?>("NUMERO_LLAMADA").GetValueOrDefault()),
                                   FECHA_INGRESO = Convert.ToString(item.Field<DateTime?>("FECHA_INGRESO").GetValueOrDefault()),
                                   CLIENTE = item.Field<string>("CLIENTE"),
                                   CAMPANA = item.Field<string>("CAMPANA"),
                                   CODIGO_SERVICIO = item.Field<string>("CODIGO_SERVICIO"),
                                   CONNID = item.Field<string>("CONNID"),
                                   SKILL = item.Field<string>("SKILL"),
                                   RUT_CLIENTE = item.Field<string>("RUT_CLIENTE"),
                                   DV = item.Field<string>("DV"),
                                   RUT_CLIENTE_DV = item.Field<string>("RUT_CLIENTE_DV"),
                                   NOMBRE_CLIENTE = item.Field<string>("NOMBRE_CLIENTE"),
                                   COMUNA = item.Field<string>("COMUNA"),
                                   TELEFONO_MOVIL = item.Field<string>("TELEFONO_MOVIL"),
                                   TELEFONO_FIJO = item.Field<string>("TELEFONO_FIJO"),
                                   TELEFONO_DEL_TRABAJO = item.Field<string>("TELEFONO_TRABAJO"),
                                   TELEFONO_MOVIL_TRABAJO = item.Field<string>("TELEFONO_MOVIL_TRABAJO"),
                                   CORREO_ELECTRONICO = item.Field<string>("CORREO_ELECTRONICO1"),
                                   CORREO_ELECTRONICO_2 = item.Field<string>("CORREO_ELECTRONICO2"),
                                   CODIGO_CAMPANA = item.Field<string>("CODIGO_CAMPANA"),
                                   TIPO_DE_CAMPANA = item.Field<string>("TIPO_CAMPANA"),
                                   NOMBRE_CAMPANA = item.Field<string>("NOMBRE_CAMPANA"),
                                   CODIGO_SOCIO = item.Field<string>("CODIGO_SOCIO"),
                                   SEGMENTO = item.Field<string>("SEGMENTO"),
                                   MONTO_APORTE = item.Field<string>("MONTO_APORTE"),
                                   DIVISA = item.Field<string>("DIVISA"),
                                   FUNDACION = item.Field<string>("FUNDACION"),
                                   ACUERDO = item.Field<string>("ACUERDO"),
                                   ESTADO_DEL_ACUERDO = item.Field<string>("ESTADO_DEL_ACUERDO"),
                                   MECANISMO_RECAUDACION = item.Field<string>("MECANISMO_RECAUDACION"),
                                   TIPO_MEDIO_DE_PAGO = item.Field<string>("TIPO_MEDIO_PAGO"),
                                   MEDIO_DE_PAGO = item.Field<string>("MEDIO_PAGO"),
                                   OFICINA_DE_VENTA = item.Field<string>("OFICINA_VENTA"),
                                   SEDE = item.Field<string>("SEDE"),
                                   FECHA_DE_CREACION = item.Field<string>("FECHA_CREACION"),
                                   URL_CALL = item.Field<string>("URL_CALL"),
                                   MONTO_BASE = item.Field<string>("MONTO_BASE"),
                                   SEGMENTO_APORTE = item.Field<string>("SEGMENTO_APORTE"),
                                   MONTO_PROPUESTO = item.Field<string>("MONTO_PROPUESTO"),
                                   OBSERVACIONES = item.Field<string>("OBSERVACIONES"),
                                   GENERICO1 = item.Field<string>("GENERICO1"),
                                   GENERICO2 = item.Field<string>("GENERICO2"),
                                   GENERICO3 = item.Field<string>("GENERICO3"),
                                   GENERICO4 = item.Field<string>("GENERICO4"),
                                   RESULTADO_LLAMADO = item.Field<string>("RESULTADO_LLAMADO"),
                                   MOTIVO_LLAMADO = item.Field<string>("MOTIVO_LLAMADO"),
                                   RESULTADO_CAMPANA = item.Field<string>("RESULTADO_CAMPANA"),
                                   MOTIVO_CAMPANA = item.Field<string>("MOTIVO_CAMPANA"),
                                   SEGMENTO_AGENTE = item.Field<string>("SEGMENTO_AGENTE"),
                                   FECHAHORA_APERTURAFORM = Convert.ToString(item.Field<DateTime?>("FECHAHORA_APERTURAFORM").GetValueOrDefault()),
                                   FECHAHORA_GRABAFORM = Convert.ToString(item.Field<DateTime?>("FECHAHORA_GRABAFORM").GetValueOrDefault()),
                                   TIEMPO_HABLADO = item.Field<string>("TIEMPO_HABLADO"),
                                   COD_TIPIFICACION1 = item.Field<string>("COD_TIPIFICACION1"),
                                   COD_TIPIFICACION2 = item.Field<string>("COD_TIPIFICACION2"),
                                   COD_TIPIFICACION3 = item.Field<string>("COD_TIPIFICACION3"),
                                   AGENTE = item.Field<string>("AGENTE"),
                                   NOMBRE_AGENTE = item.Field<string>("NOMBRE_AGENTE"),
                                   FECHA_ULTIMA_LLAMADA = Convert.ToString(item.Field<DateTime?>("FECHA_ULTIMA_LLAMADA").GetValueOrDefault()),
                                   INTENTOS = Convert.ToString(item.Field<decimal?>("INTENTOS").GetValueOrDefault()),
                                   FECHA_AGENDAMIENTO = Convert.ToString(item.Field<DateTime?>("FECHA_AGENDAMIENTO").GetValueOrDefault()),
                                   FONO_CONTACTO = item.Field<string>("FONO_CONTACTO"),
                               }).ToList();

                    foreach (var fila in tot)
                    {
                        kvp.KeyName = "Total";
                        kvp.KeyValue = fila["total"].ToString();
                    }

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;
                    ret.msg = "Fallo al cargar agentes";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Excepcion al cargar agentes";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(infoReporteria);
            ret.values.Add(kvp);

            return ret;
        }

        public RetornoAjax CargarTabla(string CodigoServicio, string Segmento, string Tipo)
        {
            RetornoAjax ret = new RetornoAjax();

            List<ReporteCargasTablas> infoTablas = new List<ReporteCargasTablas>();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "100", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSEGMENTO_AGENTE", "VARCHAR2", "100", "INPUT", Segmento));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PTIPO", "VARCHAR2", "100", "INPUT", Tipo));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_REPORTE_CARGA_TABLA",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var age = ds.Tables[0].AsEnumerable();

                    infoTablas = (from item in age
                                        select new ReporteCargasTablas
                                        {
                                            TELEFONO_MOVIL = item.Field<string>("TELEFONO_MOVIL"),
                                            TELEFONO_FIJO = item.Field<string>("TELEFONO_FIJO"),
                                            TELEFONO_DEL_TRABAJO = item.Field<string>("TELEFONO_DEL_TRABAJO"),
                                            TELEFONO_MOVIL_TRABAJO = item.Field<string>("TELEFONO_MOVIL_TRABAJO"),
                                            CORREO_ELECTRONICO = item.Field<string>("CORREO_ELECTRONICO"),
                                            CORREO_ELECTRONICO_2 = item.Field<string>("CORREO_ELECTRONICO_2"),
                                            CODIGO_CAMPANA = item.Field<string>("CODIGO_CAMPANA"),
                                            TIPO_DE_CAMPANA = item.Field<string>("TIPO_DE_CAMPANA"),
                                            NOMBRE_CAMPANA = item.Field<string>("NOMBRE_CAMPANA"),
                                            CODIGO_SOCIO = item.Field<string>("CODIGO_SOCIO"),
                                            RUT = item.Field<string>("RUT"),
                                            NOMBRE_COMPLETO = item.Field<string>("NOMBRE_COMPLETO"),
                                            SEGMENTO = item.Field<string>("SEGMENTO"),
                                            COMUNA = item.Field<string>("COMUNA"),
                                            ACUERDO = item.Field<string>("ACUERDO"),
                                            MONTO_APORTE = item.Field<string>("MONTO_APORTE"),
                                            DIVISA = item.Field<string>("DIVISA"),
                                            FUNDACION = item.Field<string>("FUNDACION"),
                                            ESTADO_DEL_ACUERDO = item.Field<string>("ESTADO_DEL_ACUERDO"),
                                            MECANISMO_RECAUDACION = item.Field<string>("MECANISMO_RECAUDACION"),
                                            TIPO_MEDIO_DE_PAGO = item.Field<string>("TIPO_MEDIO_DE_PAGO"),
                                            MEDIO_DE_PAGO = item.Field<string>("MEDIO_DE_PAGO"),
                                            OFICINA_DE_VENTA = item.Field<string>("OFICINA_DE_VENTA"),
                                            SEDE = item.Field<string>("SEDE"),
                                            FECHA_DE_CREACION = item.Field<string>("FECHA_DE_CREACION"),
                                            URL_CALL = item.Field<string>("URL_CALL"),
                                            MONTO_BASE = item.Field<string>("MONTO_BASE"),
                                            SEGMENTO_APORTE = item.Field<string>("SEGMENTO_APORTE"),
                                            MONTO_PROPUESTO = item.Field<string>("MONTO_PROPUESTO"),
                                            GENERICO1 = item.Field<string>("GENERICO1"),
                                            GENERICO2 = item.Field<string>("GENERICO2"),
                                            GENERICO3 = item.Field<string>("GENERICO3"),
                                            GENERICO4 = item.Field<string>("GENERICO4"),
                                        }).ToList();


                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;
                    ret.msg = "Fallo al cargar agentes";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Excepcion al cargar agentes";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(infoTablas);

            return ret;
        }

        public RetornoAjax CargarCombo()
        {
            RetornoAjax ret = new RetornoAjax();

            List<KVP> infoCombo = new List<KVP>();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_COMBO_SEGMENTOXAGENTE",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var age = ds.Tables[0].AsEnumerable();

                    infoCombo = (from item in age
                                           select new KVP
                                           {
                                               KeyValue = item.Field<string>("CODIGO_SERVICIO"),
                                               KeyName = item.Field<string>("NOMBRE_CAMPANA"),
                                           }).ToList();

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;
                    ret.msg = "Fallo al cargar agentes";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Excepcion al cargar agentes";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(infoCombo);

            return ret;
        }

        public RetornoAjax NombreAgente(string rut)
        {
            RetornoAjax ret = new RetornoAjax();

            KVP infoAgente = new KVP();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT", "VARCHAR2", "20", "INPUT", rut));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "TRAE_NOMBRE_AGENTE",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var age = ds.Tables[0].AsEnumerable();

                    infoAgente = (from item in age
                                 select new KVP
                                 {
                                     KeyName = item.Field<string>("NOMBRE_COMPLETO"),
                                 }).SingleOrDefault();

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;
                    ret.msg = "Fallo al cargar agentes";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Excepcion al cargar agentes";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(infoAgente);

            return ret;
        }

        public RetornoAjax Cargar(string id)
        {

            RetornoAjax ret = new RetornoAjax();

            SegmentoxAgente infoServicio = new SegmentoxAgente();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PID", "NUMERIC", "10", "INPUT", id));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_SEGMENTOXAGENTE_ID",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var age = ds.Tables[0].AsEnumerable();

                    infoServicio = (from item in age
                              select new SegmentoxAgente
                              {
                                  Id = Convert.ToString(item.Field<decimal?>("ID").GetValueOrDefault()),
                                  NombreAgente = item.Field<string>("NOMBRE_COMPLETO"),
                                  Agente = item.Field<string>("RUT"),
                                  CodigoServicio = item.Field<string>("CODIGO_SERVICIO"),
                                  Segmento = item.Field<string>("SEGMENTO_AGENTE"),
                                  NombreCampana = item.Field<string>("CAMPANA")
                              }).SingleOrDefault();

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;
                    ret.msg = "Fallo al cargar agente";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Excepcion al cargar agente";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(infoServicio);
            return ret;
        }

        public RetornoAjax CargarTipificaciones(string CodigoServicio)
        {
            RetornoAjax ret = new RetornoAjax();
            List<Tipificaciones> tipif = new List<Tipificaciones>();
            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                    this.BaseUtilApp.Instancia,
                                                                    this.BaseUtilApp.Package,
                                                                    "CARGA_TIPIFICACIONES",
                                                                    parametros.ToString(),
                                                                    ref error);

                if (ds != null)
                {
                    var c = ds.Tables[0].AsEnumerable();
                    if (c != null)
                    {
                        tipif = (from item in c
                                 select new Tipificaciones
                                 {
                                     ResultadoLlamada = item.Field<string>("RESULTADO_LLAMADA"),
                                     MotivoLlamada = item.Field<string>("MOTIVO_LLAMADA"),
                                     ResultadoCampana = item.Field<string>("RESULTADO_CAMPANA"),
                                     MotivoCampana = item.Field<string>("MOTIVO_CAMPANA"),
                                     Reprogramacion = item.Field<string>("REPROGRAMACION"),
                                 }).ToList();
                    }

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;

                    ret.values = new List<object>();
                    ret.values.Add(tipif);
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Falla al cargar datos " + error;
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Falla al cargar datos" + ex.Message;
                ret.debug = ex.Message;
            }


            return ret;
        }

        public Reporteria()
        {
            
        }
    }
}
