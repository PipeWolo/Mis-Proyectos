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
    public class MantenedorSegmentoxAgente : NavigatorBase 
    {
        public RetornoAjax Cargar(FiltroSegmentoxAgente filtro, string orden, string dir)
        {
            RetornoAjax ret = new RetornoAjax();

            List<SegmentoxAgente> infoSegmentoxAgente = new List<SegmentoxAgente>();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PAGENTE", "VARCHAR2", "100", "INPUT", filtro.Agente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "100", "INPUT", filtro.CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSEGMENTO_AGENTE", "VARCHAR2", "100", "INPUT", filtro.Segmento));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSTART", "NUMERIC", "10", "INPUT", filtro.Pagina));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PLENGTH", "VARCHAR2", "10", "INPUT", filtro.Cantidad));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCOLUMNA", "VARCHAR2", "20", "INPUT", orden));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PDIR", "VARCHAR2", "10", "INPUT", dir));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_SEGMENTOXAGENTE",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var age = ds.Tables[0].AsEnumerable();
                    var tot = from item in ds.Tables[1].AsEnumerable() select item;

                    infoSegmentoxAgente = (from item in age
                               select new SegmentoxAgente
                               {
                                   Id = Convert.ToString(item.Field<decimal?>("ID").GetValueOrDefault()),
                                   NombreAgente = item.Field<string>("NOMBRE_COMPLETO"),
                                   Agente = item.Field<string>("RUT"),
                                   CodigoServicio = item.Field<string>("CODIGO_SERVICIO"),
                                   Segmento = item.Field<string>("SEGMENTO_AGENTE"),
                                   NombreCampana = item.Field<string>("CAMPANA")
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
            ret.values.Add(infoSegmentoxAgente);
            ret.values.Add(kvp);

            return ret;
        }

        public RetornoAjax CargarCombo()
        {
            RetornoAjax ret = new RetornoAjax();

            List<KVP> infoCombo = new List<KVP>();
            List<KVP> infoAgentes = new List<KVP>();

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

                    var com = ds.Tables[0].AsEnumerable();
                    var age = ds.Tables[1].AsEnumerable();

                    infoCombo = (from item in com
                                 select new KVP
                                           {
                                               KeyValue = item.Field<string>("CODIGO_SERVICIO"),
                                               KeyName = item.Field<string>("NOMBRE_CAMPANA"),
                                           }).ToList();

                    infoAgentes = (from item in age
                                 select new KVP
                                 {
                                     KeyValue = item.Field<string>("USUARIO"),
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
            ret.values.Add(infoAgentes);

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

        public RetornoAjax SubirCargaAgentexCampana(List<SegmentoxAgente> segxagente)
        {
            RetornoAjax ret = new RetornoAjax();
            KVP idCarga = new KVP();
            List<ResumenValidacion> resumen = new List<ResumenValidacion>();
  
            try
            {
                string errorValidacion = String.Empty;

                DataTable dtTipif = TipificacionesToDataTable(segxagente);

                DataSet dsTipif = new DataSet();
                dsTipif.Tables.Add(dtTipif);

                var columnas = "RUT, CAMPANA, SEGMENTO_AGENTE";

                bool res = this.WebServiceConsultas.MultiInsertData(this.BaseUtilApp.IdAplicacion,
                                                                    dsTipif,
                                                                    columnas,
                                                                    "TB_HDC_CAMPANAS_SERVSEGAGENTE",
                                                                    ref errorValidacion);

                if (res)
                {
                    StringBuilder parametros = new StringBuilder();
                    string error = String.Empty;
                    int filas = 0;

                    bool res2 = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                     this.BaseUtilApp.Instancia,
                                                                     this.BaseUtilApp.Package,
                                                                     "LIMPIA_SEGMENTOXAGENTE",
                                                                     parametros.ToString(),
                                                                     ref filas,
                                                                     ref error);
                    ret.ret = "OK";
                    ret.msg = "Se han cargado los Agentes x Campañas validos correctamente.";
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = errorValidacion;
                    ret.msg = "Ha ocurrido un error al cargar los Agentes x Campañas.";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Falla al cargar datos";
                ret.debug = ex.Message;
            }


            ret.values = new List<object>();
            return ret;
        }

        private DataTable TipificacionesToDataTable(List<SegmentoxAgente> segxagente)
        {
            DataTable result = new DataTable();
            if (segxagente.Count == 0)
                return result;

            result.Columns.Add("RUT");
            result.Columns.Add("CAMPANA");
            result.Columns.Add("SEGMENTO_AGENTE");

            foreach (SegmentoxAgente tipif in segxagente)
            {
                var row = result.NewRow();

                row["RUT"] = tipif.Agente;
                row["CAMPANA"] = tipif.NombreCampana;
                row["SEGMENTO_AGENTE"] = tipif.Segmento;

                result.Rows.Add(row);
            }

            return result;
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

        public RetornoAjax ValidaCargaMasiva(string rut, string campana)
        {
            RetornoAjax ret = new RetornoAjax();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                string errorVal = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT", "VARCHAR2", "10", "INPUT", rut));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCAMPANA", "VARCHAR2", "100", "INPUT", campana));

                int filas = 0;

                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                      this.BaseUtilApp.Instancia,
                                                                      this.BaseUtilApp.Package,
                                                                      "VALIDA_SEGMENTOXAGENTE",
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
                    if (error.Contains("USUARIO_NO_CREADO"))
                    {
                        errorVal = "USUARIO_NO_CREADO";
                    }
                    else if (error.Contains("CAMPANA_NO_CREADO"))
                    {
                        errorVal = "CAMPANA_NO_CREADO";
                    }

                    ret.ret = errorVal;
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

        public MantenedorSegmentoxAgente()
        {
            
        }
    }
}
