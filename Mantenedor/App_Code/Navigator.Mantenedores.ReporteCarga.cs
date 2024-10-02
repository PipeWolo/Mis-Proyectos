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
    public class ReporteCarga : NavigatorBase 
    {
        public RetornoAjax Cargar(FiltroCarga filtro)
        {
            RetornoAjax ret = new RetornoAjax();

            ReporteCargas infoReporteCarga = new ReporteCargas();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "100", "INPUT", filtro.CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSEGMENTO_AGENTE", "VARCHAR2", "100", "INPUT", filtro.Segmento));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_REPORTE_CARGA",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var age = ds.Tables[0].AsEnumerable();

                    infoReporteCarga = (from item in age
                               select new ReporteCargas
                               {
                                   Fecha = item.Field<string>("FECHA_CARGA"),
                                   Hora = item.Field<string>("HORA_CARGA"),
                                   QErroneos = Convert.ToString(item.Field<decimal?>("CANTIDAD_REGISTROS_RECHAZADOS").GetValueOrDefault()),
                                   QRecibidos = Convert.ToString(item.Field<decimal?>("CANTIDAD_REGISTROS_RECIBIDOS").GetValueOrDefault()),
                                   QValidos = Convert.ToString(item.Field<decimal?>("CANTIDAD_REGISTROS_VALIDOS").GetValueOrDefault()),
                                   QRepetidos = Convert.ToString(item.Field<decimal?>("CANTIDAD_REGISTROS_REPETIDOS").GetValueOrDefault()),
                                   QGestionManual = Convert.ToString(item.Field<decimal?>("CANTIDAD_REGISTROS_GESTMANUAL").GetValueOrDefault()),
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
            ret.values.Add(infoReporteCarga); 

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

        public RetornoAjax Grabar(SegmentoxAgente ser)
        {
            RetornoAjax ret = new RetornoAjax();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;
                
                parametros.Append(this.BaseUtilAsp.CreaParametro("PID", "NUMERIC", "10", "INPUT", ser.Id));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PAGENTE", "VARCHAR2", "255", "INPUT", ser.Agente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCAMPANA", "VARCHAR2", "255", "INPUT", ser.NombreCampana));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "255", "INPUT", ser.CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSEGMENTO_AGENTE", "VARCHAR2", "255", "INPUT", ser.Segmento));

                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                     this.BaseUtilApp.Instancia,
                                                                     this.BaseUtilApp.Package,
                                                                     "GRABAR_SEGMENTOXAGENTE",
                                                                     parametros.ToString(),
                                                                     ref filas,
                                                                     ref error);


                if (res)
                {
                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;

                    if (error.Contains("EXISTE")){
                        ret.msg = "Ya existe la asociación Agente/Segmento/Campaña.";
                    } else if (error.Contains("ASOCIADO")){
                        ret.msg = "El agente ya ha sido asociado a otra campaña.";
                    } else {
                        ret.msg = "Fallo al grabar Segmento x Agente";
                    }
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Excepción al grabar servicio";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();

            return ret;
        }

        public RetornoAjax Eliminar(string id)
        {
            RetornoAjax ret = new RetornoAjax();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("pid", "NUMERIC", "10", "INPUT", id));

                int filas = 0;
                
                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                      this.BaseUtilApp.Instancia,
                                                                      this.BaseUtilApp.Package,
                                                                      "ELIMINA_SEGMENTOXAGENTE",
                                                                      parametros.ToString(),
                                                                      ref filas,
                                                                      ref error);

                if (res)
                {
                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;
                    ret.msg = "Fallo al eliminar agente";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Excepcion al eliminar agente";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();

            return ret;
        }

        public ReporteCarga()
        {
            
        }
    }
}
