using Navigator.Base;
using Navigator.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Navigator.Softphone;

namespace Navigator.Ajax.Main
{
    public class NavigatorMain : NavigatorBase
    {
        #region "Cargar selects"
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

        public RetornoAjax ModoDiscado(string CodigoServicio)
        {
            RetornoAjax ret = new RetornoAjax();
            KVP modo = new KVP();
            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                    this.BaseUtilApp.Instancia,
                                                                    this.BaseUtilApp.Package,
                                                                    "CARGA_MODO_DISCADO",
                                                                    parametros.ToString(),
                                                                    ref error);

                if (ds != null)
                {
                    var c = ds.Tables[0].AsEnumerable();
                    if (c != null)
                    {
                        modo = (from item in c
                                 select new KVP
                                 {
                                     KeyValue = item.Field<string>("MODO_DISCADO"),
                                 }).SingleOrDefault();
                    }

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;

                    ret.values = new List<object>();
                    ret.values.Add(modo);
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

        //public RetornoAjax Discar(string Fono, string maquina, string CodigoServicio, string Skill, string Prefijo)
        //{
        //    Softphone.Softphone soft = new Softphone.Softphone();
        //    RetornoAjax ret = new RetornoAjax();

        //    try
        //    {
        //        soft.Conectar(maquina);

        //        soft.DiscarFono(Fono + "#", CodigoServicio, Skill, Prefijo);

        //        ret.ret = "OK";
        //        ret.msg = "";
        //        ret.debug = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.ret = "ERROR";
        //        ret.msg = ex.Message;
        //        ret.debug = ex.Message;
        //    }

        //    return ret;
        //}

        #endregion

        #region Campaña
        public RetornoAjax CargaDatosCampaña(string CodigoServicio)
        {
            RetornoAjax ret = new RetornoAjax();
            Campana DatosCampana = new Campana();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                //Datos cabecera
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                            this.BaseUtilApp.Instancia,
                                                            this.BaseUtilApp.Package,
                                                            "CARGA_DATOS_CAMPANA",
                                                            parametros.ToString(),
                                                            ref error);
                if (grabar != null)
                {
                    var d = grabar.Tables[0].AsEnumerable();

                    if (d != null)
                    {
                        DatosCampana = (from item in d
                                        select new Campana
                                        {
                                            CodigoServicio = item.Field<string>("CODIGO_SERVICIO"),
                                            NombreCampana = item.Field<string>("NOMBRE_CAMPANA"),
                                            Skill1 = item.Field<string>("SKILL_1"),
                                            Skill2 = item.Field<string>("SKILL_1"),
                                            ModoDiscado = item.Field<string>("MODO_DISCADO"),
                                            Prefijo = item.Field<string>("PREFIJO")
                                        }).SingleOrDefault();
                    }

                    ret.ret = "OK";
                    ret.msg = "";
                    ret.debug = "";
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al grabar el requerimiento en base de datos. Inténtelo mas tarde.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al grabar el requerimiento. Inténtelo mas tarde. ";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(DatosCampana);

            return ret;
        }

        public RetornoAjax CargaCampanaAgente(Llamada llamada)
        {
            RetornoAjax ret = new RetornoAjax();
            List<KVP> DatosCampana = new List<KVP>();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                //Datos cabecera
                parametros.Append(this.BaseUtilAsp.CreaParametro("PAGENTE", "VARCHAR2", "100", "INPUT", llamada.Agente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                            this.BaseUtilApp.Instancia,
                                                            this.BaseUtilApp.Package,
                                                            "CARGA_CAMPANAS_AGENTE",
                                                            parametros.ToString(),
                                                            ref error);
                if (grabar != null)
                {
                    var d = grabar.Tables[0].AsEnumerable();

                    if (d != null)
                    {
                        DatosCampana = (from item in d
                                        select new KVP
                                        {
                                            KeyName = item.Field<string>("NOMBRE_AGENTE"),
                                            KeyValue = item.Field<string>("CAMPANA"),
                                            KeyValue2 = item.Field<string>("CODIGO_SERVICIO"),
                                        }).ToList();
                    }

                    ret.ret = "OK";
                    ret.msg = "";
                    ret.debug = "";
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al grabar el requerimiento en base de datos. Inténtelo mas tarde.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al grabar el requerimiento. Inténtelo mas tarde. ";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(DatosCampana);

            return ret;
        }

        public RetornoAjax EnProceso(Llamada llamada)
        {
            RetornoAjax ret = new RetornoAjax();
            KVP NumeroLlamada = new KVP();
            this.MsgError = String.Empty;

            try
            {
                StringBuilder parametros = new StringBuilder();
                string MsgError = String.Empty;
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT", "VARCHAR2", "10", "INPUT", llamada.IdentificadorCliente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PAGENTE", "VARCHAR2", "30", "INPUT", llamada.Agente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSKILL", "VARCHAR2", "100", "INPUT", llamada.Skill));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "100", "INPUT", llamada.CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCONNID", "VARCHAR2", "100", "INPUT", llamada.ConnIdHex));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                             this.BaseUtilApp.Instancia,
                                                             this.BaseUtilApp.Package,
                                                             "EN_PROCESO",
                                                             parametros.ToString(),
                                                             ref error);

                if (grabar != null)
                {
                    var d = grabar.Tables[0].AsEnumerable();

                    if (d != null)
                    {
                        NumeroLlamada = (from item in d
                                         select new KVP
                                         {
                                             KeyName = Convert.ToString(item.Field<decimal?>("NUMERO_LLAMADA").GetValueOrDefault()),
                                         }).FirstOrDefault();
                    }

                    ret.ret = "OK";
                    ret.msg = "";
                    ret.debug = "";
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al grabar el requerimiento en base de datos. Inténtelo mas tarde.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                this.MsgError = "EnProceso.Exception -> " + ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(NumeroLlamada);

            return ret;
        }

        public RetornoAjax Buscar(string Rut, string CodigoServicio)
        {
            RetornoAjax ret = new RetornoAjax();
            List<Historial> InfoRegistro = new List<Historial>();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string MsgError = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_CLIENTE", "VARCHAR2", "30", "INPUT", Rut));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion, this.BaseUtilApp.Instancia, this.BaseUtilApp.Package, "BUSCAR_REGISTRO", parametros.ToString(), ref MsgError);

                if (grabar != null)
                {
                    var a = grabar.Tables[0].AsEnumerable();

                    if (a != null)
                    {
                        InfoRegistro = (from item in a
                                        select new Historial
                                        {
                                            NumeroLlamada = Convert.ToString(item.Field<decimal?>("NUMERO_LLAMADA").GetValueOrDefault()),
                                            FechaUltimaLlamada = item.Field<string>("FECHA_ULTIMA_LLAMADA"),
                                            RutCliente = item.Field<string>("RUT_CLIENTE"),
                                            DV = item.Field<string>("DV"),
                                            Agente = item.Field<string>("AGENTE"),
                                            ResultadoLlamado = item.Field<string>("RESULTADO_LLAMADO"),
                                            MotivoLlamado = item.Field<string>("MOTIVO_LLAMADO"),
                                            ResultadoCampana = item.Field<string>("RESULTADO_CAMPANA"),
                                            MotivoCampana = item.Field<string>("MOTIVO_CAMPANA"),
                                            FonoContacto = item.Field<string>("FONO_CONTACTO"),
                                        }).ToList();
                    }


                    ret.ret = "OK";
                    ret.msg = "";
                    ret.debug = "";
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al grabar el requerimiento en base de datos. Inténtelo mas tarde.";
                    ret.debug = MsgError;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al grabar el requerimiento. Inténtelo mas tarde. ";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(InfoRegistro);

            return ret;
        }

        public RetornoAjax Grabar(Llamada llamada, string OpcionLlamado)
        {
            RetornoAjax ret = new RetornoAjax();
            Llamada DatosLlamada = new Llamada();
            RegistrosGenesys InfoGenesys = new RegistrosGenesys();
            List<RegistrosGenesys> InfoGenesysDemas = new List<RegistrosGenesys>();

            int filas = 0;

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                //Datos cabecera
                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_AGENTE", "VARCHAR2", "30", "INPUT", llamada.RutAgente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE_AGENTE", "VARCHAR2", "60", "INPUT", llamada.Agente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSKILL", "VARCHAR2", "100", "INPUT", llamada.Skill));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PNUMERO_CLIENTE", "VARCHAR2", "100", "INPUT", llamada.Numero_Cliente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCONNID", "VARCHAR2", "100", "INPUT", llamada.ConnIdHex));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "100", "INPUT", llamada.CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_CLIENTE", "VARCHAR2", "100", "INPUT", llamada.RutCliente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PRESULTADO_LLAMADA", "VARCHAR2", "100", "INPUT", llamada.ResultadoLlamado));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PMOTIVO_LLAMADA", "VARCHAR2", "100", "INPUT", llamada.MotivoLlamado));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PRESULTADO_CAMPANA", "VARCHAR2", "100", "INPUT", llamada.ResultadoCampana));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PMOTIVO_CAMPANA", "VARCHAR2", "100", "INPUT", llamada.MotivoCampana));
                parametros.Append(this.BaseUtilAsp.CreaParametro("POBSERVACIONES", "VARCHAR2", "100", "INPUT", llamada.Observaciones));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PFECHA_REPROGRAMACION", "VARCHAR2", "100", "INPUT", llamada.FechaReprogramacion));

                bool grabar = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                            this.BaseUtilApp.Instancia,
                                                            this.BaseUtilApp.Package,
                                                            "GRABA_REGISTRO",
                                                            parametros.ToString(),
                                                            ref filas,
                                                            ref error);
                if (grabar == true)
                {

                    if (llamada.ConnIdDec == "0")//Significa que no es predictivo, es asistido o mixto
                    {
                        ret = DatosGenesys(llamada.RutCliente, llamada.Numero_Cliente, OpcionLlamado, llamada.CodigoServicio);
                        InfoGenesys = (RegistrosGenesys)ret.values[0];
                        InfoGenesysDemas = (List<RegistrosGenesys>)ret.values[1];
                    }
                    
                    grabar = GrabaResultadoGenesys(llamada, InfoGenesys, InfoGenesysDemas);

                    if (grabar == true)
                    {
                        ret.ret = "OK";
                        ret.msg = "Registro lista grabado con éxito";
                        ret.debug = "";
                    }
                    else
                    {
                        ret.ret = "ERROR";
                        ret.msg = "Registro lista fue grabado con éxito pero fallo al actualizar el resultado de la llamada en genesys";
                        ret.debug = "";
                    }
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al grabar el requerimiento en base de datos. Inténtelo mas tarde.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al grabar el requerimiento. Inténtelo mas tarde. ";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(DatosLlamada);

            return ret;
        }

        public RetornoAjax DatosGenesys(string Rut, string Fono, string Opcion, string CodigoServicio)
        {
            RetornoAjax ret = new RetornoAjax();
            RegistrosGenesys InfoGenesys = new RegistrosGenesys();
            List<RegistrosGenesys> InfoGenesysDemas = new List<RegistrosGenesys>();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string MsgError = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PIDENTIFICADOR", "VARCHAR2", "30", "INPUT", Rut));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PFONO", "VARCHAR2", "30", "INPUT", Fono));
                parametros.Append(this.BaseUtilAsp.CreaParametro("POPCION", "VARCHAR2", "30", "INPUT", Opcion));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion, this.BaseUtilApp.Instancia, this.BaseUtilApp.Package, "DATOS_GENESYS", parametros.ToString(), ref MsgError);

                if (grabar != null)
                {
                    var a = grabar.Tables[0].AsEnumerable();
                    var b = grabar.Tables[1].AsEnumerable();

                    if (a != null)
                    {
                        InfoGenesys = (from item in a
                                        select new RegistrosGenesys
                                        {
                                            RecordID = Convert.ToString(item.Field<decimal?>("RECORD_ID").GetValueOrDefault()),
                                            ListaGenesys = item.Field<string>("LISTA_GENESYS"),
                                        }).SingleOrDefault();
                    }

                    if (b != null)
                    {
                        InfoGenesysDemas = (from item in b
                                 select new RegistrosGenesys
                                 {
                                     RecordID = Convert.ToString(item.Field<decimal?>("RECORD_ID").GetValueOrDefault()),
                                     ListaGenesys = item.Field<string>("LISTA_GENESYS"),
                                 }).ToList();
                    }


                    ret.ret = "OK";
                    ret.msg = "";
                    ret.debug = "";
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al grabar el requerimiento en base de datos. Inténtelo mas tarde.";
                    ret.debug = MsgError;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al grabar el requerimiento. Inténtelo mas tarde. ";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(InfoGenesys);
            ret.values.Add(InfoGenesysDemas);

            return ret;
        }

        public bool GrabaResultadoGenesys(Llamada llamada, RegistrosGenesys InfoGenesys, List<RegistrosGenesys> InfoGenesysDemas)
        {

            bool ret = false;

            this.MsgError = String.Empty;

            try
            {
                string ResultadoLlamada = llamada.ResultadoLlamado.ToUpper();
                string MotivoResultadoLlamada = llamada.MotivoLlamado.ToUpper();
                string MotivoResultadoCampana = llamada.MotivoCampana.ToUpper();
                string MotivoTipificacion = String.Empty;

                string RecordType = String.Empty;
                string RecordStatus = String.Empty;
                string CallResult = String.Empty;
                string DialSchedTime = String.Empty;

                if (ResultadoLlamada.Equals("PENDIENTE"))
                {
                    MotivoTipificacion = MotivoResultadoLlamada;
                }
                else if (ResultadoLlamada.Equals("DESCARTADO"))
                {
                    MotivoTipificacion = MotivoResultadoLlamada;
                }
                else
                {
                    MotivoTipificacion = MotivoResultadoCampana;
                }

                if (ResultadoLlamada.Equals("CONTACTADO"))
                {
                    RecordType = "2";
                    RecordStatus = "3";
                    CallResult = "33";
                }
                else if (ResultadoLlamada.Equals("DESCARTADO"))
                {
                    RecordType = "2";
                    RecordStatus = "3";
                    CallResult = "51";
                }
                else if (ResultadoLlamada.Equals("PENDIENTE"))
                {
                    //if (this.Carterizada.Equals("SI"))
                    //{
                    //    if (!MotivoTipificacion.ToUpper().Equals("VOLVER A LLAMAR"))
                    //    {
                    //        RecordType = "6";
                    //    }
                    //    else
                    //    {
                    //        RecordType = "4";
                    //    }
                    //}
                    //else
                    //{
                    //    RecordType = "6";
                    //}

                    RecordType = "6";
                    RecordStatus = "1";
                    CallResult = "33";

                    if (!llamada.FechaReprogramacion.Equals(""))
                    {
                        DialSchedTime = ObtieneSegundosGenesys(llamada.RutCliente, llamada.CodigoServicio);

                        if (DialSchedTime == null || DialSchedTime.Equals("0"))
                        {
                            this.MsgError = "GrabaResultadoGenesys.ObtieneSegundosGenesys->Fallo al obtener segundos de fecha de agendamiento";
                        }
                    }
                    else
                    {
                        RecordType = "2";
                        RecordStatus = "3";
                        CallResult = "33";
                    }
                }

                if (this.MsgError.Equals(String.Empty))
                {

                    string Mensaje = String.Empty;
                    string Status = String.Empty;

                    if (llamada.ConnIdDec.Equals("0") && (InfoGenesys != null))
                    {
                        this.WebServiceExecutor.Inserta_Registro(ref Mensaje, InfoGenesys.RecordID, InfoGenesys.ListaGenesys,
                                                            llamada.RutAgente, RecordType,
                                                            RecordStatus, CallResult, DialSchedTime,
                                                            ResultadoLlamada, MotivoTipificacion, ref Status);
                        
                        if (ResultadoLlamada.Equals("CONTACTADO") || ResultadoLlamada.Equals("PENDIENTE"))
                        {
                            CallResult = "28";

                            foreach (RegistrosGenesys Fila in InfoGenesysDemas)
                            {

                                if (!Fila.RecordID.Equals("NORECORRER"))
                                {
                                    this.WebServiceExecutor.Inserta_Registro(ref Mensaje, Fila.RecordID, Fila.ListaGenesys,
                                                                 llamada.RutAgente, RecordType,
                                                                 RecordStatus, CallResult, DialSchedTime,
                                                                 "", "", ref Status);
                                }
                            }
                        }
                    }
                    else
                    {
                        this.WebServiceExecutor.Inserta_Registro(ref Mensaje, llamada.RecordID, llamada.NombreLista,
                                                             llamada.RutAgente, RecordType,
                                                             RecordStatus, CallResult, DialSchedTime,
                                                             ResultadoLlamada, MotivoTipificacion, ref Status);
                    }

                    if (Status.Equals("OK"))
                    {
                        ret = true;
                    }
                    else
                    {
                        this.MsgError = "GrabaResultadoGenesys.WebService->" + Mensaje;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgError = "GrabaResultadoGenesys.Exception -> " + ex.Message;
            }

            return ret;

        }

        private string ObtieneSegundosGenesys(string RutCliente, string CodigoServicio)
        {

            string ret = String.Empty;

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                //Datos cabecera
                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_CLIENTE", "VARCHAR2", "30", "INPUT", RutCliente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "100", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                            this.BaseUtilApp.Instancia,
                                                            this.BaseUtilApp.Package,
                                                            "OBTIENE_SEGUNDOS_GENESYS",
                                                            parametros.ToString(),
                                                            ref error);
                if (grabar != null)
                {
                    foreach (DataRow Fila in grabar.Tables[0].Rows)
                    {
                        ret = Convert.ToString(Fila["PSEGUNDOS_GENESYS"]);
                    }
                }
                else
                {
                    ret = "ERROR";
                }
            }
            catch (Exception ex)
            {
                ret = "ERROR";
            }

            return ret;
        }
        #endregion


        #region Llamada
        public RetornoAjax CargarDatosPredictivo(string IdentificadorCliente, string CodigoServicio, string Skill)
        {

            RetornoAjax ret = new RetornoAjax();
            Llamada DatosLlamada = new Llamada();
            List<Historial> InfoHistorial = new List<Historial>();
            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_CLIENTE", "VARCHAR2", "10", "INPUT", IdentificadorCliente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSKILL", "VARCHAR2", "50", "INPUT", Skill));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                            this.BaseUtilApp.Instancia,
                                                                            this.BaseUtilApp.Package,
                                                                            "CARGA_DATOS_PREDICTIVO",
                                                                            parametros.ToString(),
                                                                            ref error);

                if (string.IsNullOrEmpty(error) && ds != null)
                {
                    var a = ds.Tables[0].AsEnumerable();
                    var b = ds.Tables[1].AsEnumerable();

                    if (a != null)
                    {
                        DatosLlamada = (from item in a
                                     select new Llamada
                                     {
                                         RutCliente = item.Field<string>("RUT_CLIENTE"),
                                         DV = item.Field<string>("DV"),
                                         NombreCliente = item.Field<string>("NOMBRE_CLIENTE"),
                                         Comuna = item.Field<string>("COMUNA"),
                                         TelefonoMovil = item.Field<string>("TELEFONO_MOVIL"),
                                         TelefonoFijo = item.Field<string>("TELEFONO_FIJO"),
                                         TelefonoTrabajo = item.Field<string>("TELEFONO_TRABAJO"),
                                         TelefonoMovilTrabajo = item.Field<string>("TELEFONO_MOVIL_TRABAJO"),
                                         CorreoElectrónico1 = item.Field<string>("CORREO_ELECTRONICO1"),
                                         CorreoElectrónico2 = item.Field<string>("CORREO_ELECTRONICO2"),
                                         CodigoSocio = item.Field<string>("CODIGO_SOCIO"),
                                         MontoAporte = item.Field<string>("MONTO_APORTE"),
                                         Divisa = item.Field<string>("DIVISA"),
                                         Fundacion = item.Field<string>("FUNDACION"),
                                         TipoMedioPago = item.Field<string>("TIPO_MEDIO_PAGO"),
                                         MedioPago = item.Field<string>("MEDIO_PAGO"),
                                         OficinaVenta = item.Field<string>("OFICINA_VENTA"),
                                         Sede = item.Field<string>("SEDE"),
                                         MontoBase = item.Field<string>("MONTO_BASE"),
                                         MontoPropuesto = item.Field<string>("MONTO_PROPUESTO"),
                                         URLCall = item.Field<string>("URL_CALL"),
                                         Acuerdo = item.Field<string>("ACUERDO"),
                                         ID_CS = Convert.ToString(item.Field<decimal?>("ID_CS").GetValueOrDefault()),
                                     })
                                     .SingleOrDefault();

                    }

                    if (b != null)
                    {
                        InfoHistorial = (from item in b
                                            select new Historial
                                            {
                                                NumeroLlamada = Convert.ToString(item.Field<decimal?>("NUMERO_LLAMADA").GetValueOrDefault()),
                                                FechaUltimaLlamada = item.Field<string>("FECHA_ULTIMA_LLAMADA"),
                                                RutCliente = item.Field<string>("RUT_CLIENTE"),
                                                DV = item.Field<string>("DV"),
                                                Agente = item.Field<string>("AGENTE"),
                                                ResultadoLlamado = item.Field<string>("RESULTADO_LLAMADO"),
                                                MotivoLlamado = item.Field<string>("MOTIVO_LLAMADO"),
                                                ResultadoCampana = item.Field<string>("RESULTADO_CAMPANA"),
                                                MotivoCampana = item.Field<string>("MOTIVO_CAMPANA"),
                                                FonoContacto = item.Field<string>("FONO_CONTACTO"),
                                            }).ToList();
                    }

                    ret.ret = "OK";
                    ret.msg = "";
                    ret.debug = "";
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Falla al cargar bandeja";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Falla al cargar bandeja";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(DatosLlamada);
            ret.values.Add(InfoHistorial);

            return ret;
        }

        public bool ExistenRegistros(DataTable tbl)
        {
            bool ret = true;

            foreach (DataRow Fila in tbl.Rows)
            {
                if (Convert.ToString(Fila[0]).Equals("NOHAYREGISTROS"))
                {
                    ret = false;
                }
                break;
            }

            return ret;

        }

        public RetornoAjax PedirRegistro(string CodigoServicio, string NumeroLlamada, string Agente)
        {

            RetornoAjax ret = new RetornoAjax();
            Llamada DatosLlamada = new Llamada();
            List<Historial> InfoHistorial = new List<Historial>();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PNUMERO_LLAMADA", "NUMERIC", "10", "INPUT", NumeroLlamada));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PAGENTE", "NUMERIC", "10", "INPUT", Agente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "10", "INPUT", CodigoServicio));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                            this.BaseUtilApp.Instancia,
                                                                            this.BaseUtilApp.Package,
                                                                            "PIDE_REGISTRO",
                                                                            parametros.ToString(),
                                                                            ref error);

                if (string.IsNullOrEmpty(error) && ds != null)
                {
                    var a = ds.Tables[0].AsEnumerable();
                    var b = ds.Tables[1].AsEnumerable();

                    if (ExistenRegistros(ds.Tables[0]))
                    {
                        if (a != null)
                        {
                            DatosLlamada = (from item in a
                                            select new Llamada
                                            {
                                                Agente = item.Field<string>("NOMBRE_AGENTE"),
                                                NombreCampana = item.Field<string>("CAMPANA"),
                                                NumeroLlamada = Convert.ToString(item.Field<decimal?>("NUMERO_LLAMADA").GetValueOrDefault()),
                                                Skill = item.Field<string>("SKILL"),
                                                RutCliente = item.Field<string>("RUT_CLIENTE"),
                                                DV = item.Field<string>("DV"),
                                                NombreCliente = item.Field<string>("NOMBRE_CLIENTE"),
                                                Comuna = item.Field<string>("COMUNA"),
                                                TelefonoMovil = item.Field<string>("TELEFONO_MOVIL"),
                                                TelefonoFijo = item.Field<string>("TELEFONO_FIJO"),
                                                TelefonoTrabajo = item.Field<string>("TELEFONO_TRABAJO"),
                                                TelefonoMovilTrabajo = item.Field<string>("TELEFONO_MOVIL_TRABAJO"),
                                                CorreoElectrónico1 = item.Field<string>("CORREO_ELECTRONICO1"),
                                                CorreoElectrónico2 = item.Field<string>("CORREO_ELECTRONICO2"),
                                                CodigoSocio = item.Field<string>("CODIGO_SOCIO"),
                                                MontoAporte = item.Field<string>("MONTO_APORTE"),
                                                Divisa = item.Field<string>("DIVISA"),
                                                Fundacion = item.Field<string>("FUNDACION"),
                                                TipoMedioPago = item.Field<string>("TIPO_MEDIO_PAGO"),
                                                MedioPago = item.Field<string>("MEDIO_PAGO"),
                                                OficinaVenta = item.Field<string>("OFICINA_VENTA"),
                                                Sede = item.Field<string>("SEDE"),
                                                MontoBase = item.Field<string>("MONTO_BASE"),
                                                MontoPropuesto = item.Field<string>("MONTO_PROPUESTO"),
                                                URLCall = item.Field<string>("URL_CALL"),
                                                Acuerdo = item.Field<string>("ACUERDO"),
                                                ID_CS = Convert.ToString(item.Field<decimal?>("ID_CS").GetValueOrDefault()),
                                            })
                                     .SingleOrDefault();

                        }

                        if (b != null)
                        {
                            InfoHistorial = (from item in b
                                             select new Historial
                                             {
                                                 NumeroLlamada = Convert.ToString(item.Field<decimal?>("NUMERO_LLAMADA").GetValueOrDefault()),
                                                 FechaUltimaLlamada = item.Field<string>("FECHA_ULTIMA_LLAMADA"),
                                                 RutCliente = item.Field<string>("RUT_CLIENTE"),
                                                 DV = item.Field<string>("DV"),
                                                 Agente = item.Field<string>("AGENTE"),
                                                 ResultadoLlamado = item.Field<string>("RESULTADO_LLAMADO"),
                                                 MotivoLlamado = item.Field<string>("MOTIVO_LLAMADO"),
                                                 ResultadoCampana = item.Field<string>("RESULTADO_CAMPANA"),
                                                 MotivoCampana = item.Field<string>("MOTIVO_CAMPANA"),
                                                 FonoContacto = item.Field<string>("FONO_CONTACTO"),
                                             }).ToList();
                        }

                        ret.ret = "OK";
                        ret.msg = "";
                        ret.debug = "";
                    }
                    else
                    {
                        ret.ret = "ERROR";
                        ret.msg = "No existen registros a discar";
                        ret.debug = error;
                    }

                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Falla al cargar bandeja";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Falla al cargar bandeja";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(DatosLlamada);
            ret.values.Add(InfoHistorial);

            return ret;
        }


        //public RetornoAjax CreaRequerimiento(Llamada llamada)
        //{
        //    RetornoAjax ret = new RetornoAjax();
        //    KVP req = new KVP();
        //    KVP tk = new KVP();
        //    try
        //    {
        //        StringBuilder parametros = new StringBuilder();
        //        string error = String.Empty;

        //        //Datos cabecera
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_AGENTE", "VARCHAR2", "10", "INPUT", llamada.RUT_AGENTE));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PSKILL", "VARCHAR2", "100", "INPUT", llamada.SKILL));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PANI", "VARCHAR2", "20", "INPUT", llamada.ANI));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PCONNID", "VARCHAR2", "30", "INPUT", llamada.CONNID));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "20", "INPUT", llamada.CODIGO_SERVICIO));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE_CONTACTO", "VARCHAR2", "100", "INPUT", llamada.NOMBRE_CONTACTO));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PMOTIVO_CONTACTO_DIAGNOSTICO", "VARCHAR2", "100", "INPUT", llamada.MOTIVO_CONTACTO_DIAGNOSTICO));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

        //        DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
        //                                                    this.BaseUtilApp.Instancia,
        //                                                    this.BaseUtilApp.Package,
        //                                                    "CREA_REQUERIMIENTO",
        //                                                    parametros.ToString(),
        //                                                    ref error);
        //        if (grabar != null)
        //        {
        //            var d = grabar.Tables[0].AsEnumerable();
        //            if (d != null)
        //            {
        //                req = (from item in d
        //                       select new KVP
        //                       {
        //                           KeyName = Convert.ToString(item.Field<decimal?>("ID").GetValueOrDefault()),
        //                           KeyValue = item.Field<string>("FECHA")
        //                       }).SingleOrDefault();

        //            }
        //            if (!string.IsNullOrEmpty(req.KeyName))
        //            {
        //                ret.ret = "OK";
        //                ret.msg = "";
        //                ret.debug = "";
        //                ret.values = new List<object>();
        //                ret.values.Add(req);
        //            }
        //            else
        //            {
        //                ret.ret = "ERROR";
        //                ret.msg = "Ocurrió un error inesperado. Inténtelo mas tarde.";
        //                ret.debug = error;
        //            }
        //        }
        //        else
        //        {
        //            ret.ret = "ERROR";
        //            ret.msg = "Error al grabar el requerimiento en base de datos. Inténtelo mas tarde.";
        //            ret.debug = error;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.ret = "ERROR";
        //        ret.msg = "Error al grabar el requerimiento. Inténtelo mas tarde. ";
        //        ret.debug = ex.Message;
        //    }


        //    return ret;
        //}
        //public RetornoAjax GuardaTipificacion(Llamada llamada)
        //{
        //    RetornoAjax ret = new RetornoAjax();
        //    KVP req = new KVP();
        //    KVP tk = new KVP();
        //    try
        //    {
        //        StringBuilder parametros = new StringBuilder();
        //        string error = String.Empty;

        //        //Datos cabecera
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PN_REQUERIMIENTO", "NUMERIC", "10", "INPUT", llamada.N_REQUERIMIENTO));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_AGENTE", "VARCHAR2", "10", "INPUT", llamada.RUT_AGENTE));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PSKILL", "VARCHAR2", "100", "INPUT", llamada.SKILL));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PFECHA_HORA_LLAMADA", "VARCHAR2", "20", "INPUT", llamada.FECHA_HORA_LLAMADA));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PANI", "VARCHAR2", "20", "INPUT", llamada.ANI));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PCONNID", "VARCHAR2", "30", "INPUT", llamada.CONNID));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "20", "INPUT", llamada.CODIGO_SERVICIO));
        //        //Datos cliente
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_CLIENTE", "VARCHAR2", "10", "INPUT", llamada.RUT_CLIENTE));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PDV_CLIENTE", "VARCHAR2", "1", "INPUT", llamada.DV_CLIENTE));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE_EMPRESA_PERSONA", "VARCHAR2", "100", "INPUT", llamada.NOMBRE_EMPRESA_PERSONA));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PNUMERO_CONTACTO", "VARCHAR2", "100", "INPUT", llamada.NUMERO_CONTACTO));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE_CONTACTO", "VARCHAR2", "100", "INPUT", llamada.NOMBRE_CONTACTO));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PMOTIVO_CONTACTO_DIAGNOSTICO", "VARCHAR2", "100", "INPUT", llamada.MOTIVO_CONTACTO_DIAGNOSTICO));
        //        //Tipificacion
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PHABILIDAD", "VARCHAR2", "50", "INPUT", llamada.HABILIDAD));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("POPERACION", "VARCHAR2", "100", "INPUT", llamada.OPERACION));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PSUBOPERACION", "VARCHAR2", "100", "INPUT", llamada.SUBOPERACION));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PTIPO", "VARCHAR2", "100", "INPUT", llamada.TIPO));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("POBSERVACIONES", "VARCHAR2", "2000", "INPUT", llamada.OBSERVACIONES));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));


        //        DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
        //                                                                    this.BaseUtilApp.Instancia,
        //                                                                    this.BaseUtilApp.Package,
        //                                                                    "GUARDA_TIPIFICACION",
        //                                                                    parametros.ToString(),
        //                                                                    ref error);
        //        if (grabar != null)
        //        {
        //            var d = grabar.Tables[0].AsEnumerable();
        //            if (d != null)
        //            {
        //                req = (from item in d
        //                       select new KVP
        //                       {
        //                           KeyName = Convert.ToString(item.Field<decimal?>("ID").GetValueOrDefault())
        //                       }).SingleOrDefault();

        //            }
        //            if (!string.IsNullOrEmpty(req.KeyName))
        //            {
        //                ret.ret = "OK";
        //                ret.msg = "";
        //                ret.debug = "";
        //                ret.values = new List<object>();
        //                ret.values.Add(req);
        //            }
        //            else
        //            {
        //                ret.ret = "ERROR";
        //                ret.msg = "Ocurrió un error inesperado. Inténtelo mas tarde.";
        //                ret.debug = error;
        //            }
        //        }
        //        else
        //        {
        //            ret.ret = "ERROR";
        //            ret.msg = "Error al grabar el requerimiento en base de datos. Inténtelo mas tarde.";
        //            ret.debug = error;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.ret = "ERROR";
        //        ret.msg = "Error al grabar el requerimiento. Inténtelo mas tarde. ";
        //        ret.debug = ex.Message;
        //    }


        //    return ret;
        //}

        //public RetornoAjax CargarUltimoContacto(string RUT, string OT, string ANI)
        //{

        //    RetornoAjax ret = new RetornoAjax();
        //    Historial historial = new Historial();
        //    try
        //    {
        //        StringBuilder parametros = new StringBuilder();
        //        string error = String.Empty;

        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_CLIENTE", "VARCHAR2", "10", "INPUT", RUT));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("POT", "VARCHAR2", "20", "INPUT", OT));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PANI", "VARCHAR2", "20", "INPUT", ANI));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

        //        DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
        //                                                                    this.BaseUtilApp.Instancia,
        //                                                                    this.BaseUtilApp.Package,
        //                                                                    "CARGAR_ULT_CONTACTO",
        //                                                                    parametros.ToString(),
        //                                                                    ref error);

        //        if (string.IsNullOrEmpty(error) && ds != null)
        //        {
        //            var c = ds.Tables[0].AsEnumerable();
        //            if (c != null)
        //            {
        //                historial = (from item in c
        //                             select new Historial
        //                             {
        //                                 RUT_CONTACTO = item.Field<string>("RUT_CONTACTO"),
        //                                 NOMBRE_CONTACTO = item.Field<string>("NOMBRE_CONTACTO"),
        //                                 APELLIDO_PATERNO_CONTACTO = item.Field<string>("APELLIDO_PATERNO_CONTACTO"),
        //                                 APELLIDO_MATERNO_CONTACTO = item.Field<string>("APELLIDO_MATERNO_CONTACTO"),
        //                                 TELEFONO_MOVIL_CONTACTO = item.Field<string>("TELEFONO_MOVIL_CONTACTO"),
        //                                 TELEFONO_FIJO_CONTACTO = item.Field<string>("TELEFONO_FIJO_CONTACTO"),
        //                                 TELEFONO_INTERNO_CONTACTO = item.Field<string>("TELEFONO_INTERNO_CONTACTO"),
        //                                 CORREO_CONTACTO = item.Field<string>("CORREO_CONTACTO"),
        //                                 NICKNAME = item.Field<string>("NICKNAME")
        //                             })
        //                             .SingleOrDefault();

        //            }

        //            ret.ret = "OK";
        //            ret.msg = "";
        //            ret.debug = "";
        //        }
        //        else
        //        {
        //            ret.ret = "ERROR";
        //            ret.msg = "Falla al cargar bandeja";
        //            ret.debug = error;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.ret = "ERROR";
        //        ret.msg = "Falla al cargar bandeja";
        //        ret.debug = ex.Message;
        //    }

        //    ret.values = new List<object>();
        //    ret.values.Add(historial);

        //    return ret;
        //}
        public RetornoAjax CargarDatosOT(string ot, string rutCliente)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 1073741824; //1gb
            RetornoAjax ret = new RetornoAjax();
            ret.values = new List<object>();
            ConsultaDatosTracking consulta = new ConsultaDatosTracking();
            ConsultaDatosOT datosot = new ConsultaDatosOT();
            KVP tipoCliente = new KVP();
            string error = string.Empty;
            try
            {
                RestClient rest = new RestClient();
                string method = "v3/Tracking/GetTracking";
                string parameters = string.Format("gls_Consulta={0}", ot);
                string apiError = "";
                RetornoAjax retTracking = (RetornoAjax)rest.MakeRequestListObject(method, parameters, HttpVerb.GET, ref apiError);
                if (retTracking.ret == "OK")
                {
                    if (retTracking.values.Count > 0 && ((string)retTracking.values[0]).Length > 0)
                    {
                        try
                        {
                            consulta = js.Deserialize<ConsultaDatosTracking>((string)retTracking.values[0]);
                            ret.ret = "OK";
                        }
                        catch (Exception ex)
                        {
                            ret.ret = "ERROR";
                            ret.msg = "No se han podido obtener los datos de la OT";
                            ret.debug = ex.Message;
                        }
                        //Trae datos adicionales
                        if (ret.ret == "OK")
                        {
                            RetornoAjax retOt = GetDatosOT(ot);
                            if (retOt.ret == "OK")
                            {
                                if (retOt.values.Count > 0 && ((string)retOt.values[0]).Length > 0)
                                {
                                    try
                                    {
                                        datosot = js.Deserialize<ConsultaDatosOT>((string)retOt.values[0]);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "No se han podido obtener los datos de la OT";
                    ret.debug = "No se han podido obtener los datos de la OT";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "No se han podido obtener los datos de la OT";
                ret.debug = ex.Message;
            }

            RetornoAjax retTipo = CargarTipoCliente(rutCliente);
            string tipo = "";
            try { tipo = retTipo.values[0].ToString(); }
            catch (Exception) { tipo = "Sin información"; }

            ret.values.Add(consulta);
            ret.values.Add(datosot);
            ret.values.Add(tipo);

            return ret;
        }
        public RetornoAjax GetDatosOT(string ot)
        {
            RetornoAjax ret = new RetornoAjax();
            try
            {
                RestClient rest = new RestClient();
                string method = "v3/Tracking/GetDatosOT";
                string sistema = "cxp";
                string parameters = string.Format("nroOt={0}&Sistema={1}", ot, sistema);
                string apiError = "";
                ret = (RetornoAjax)rest.MakeRequestListObject(method, parameters, HttpVerb.GET, ref apiError);
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde. ";
                ret.debug = ex.Message;
            }

            return ret;
        }

        public RetornoAjax CargarTipoCliente(string rutCliente)
        {

            RetornoAjax ret = new RetornoAjax();
            KVP tipo = new KVP();
            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_CLIENTE", "NUMERIC", "10", "INPUT", rutCliente));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                            this.BaseUtilApp.Instancia,
                                                                            this.BaseUtilApp.Package,
                                                                            "CARGAR_TIPO_CLIENTE",
                                                                            parametros.ToString(),
                                                                            ref error);

                if (string.IsNullOrEmpty(error) && ds != null)
                {
                    var c = ds.Tables[0].AsEnumerable();
                    if (c != null)
                    {
                        tipo = (from item in c
                                select new KVP
                                {
                                    KeyValue = item.Field<string>("TIPO_CLIENTE")
                                })
                                     .SingleOrDefault();

                    }

                    if (tipo == null)
                    {
                        tipo = new KVP();
                        tipo.KeyValue = "Sin información";
                    }

                    ret.ret = "OK";
                    ret.msg = "";
                    ret.debug = "";
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Falla al cargar tipo cliente";
                    ret.debug = error;
                    tipo.KeyValue = "Sin información";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Falla al cargar bandeja";
                ret.debug = ex.Message;
                tipo.KeyValue = "Sin información";
            }

            ret.values = new List<object>();
            ret.values.Add(tipo.KeyValue);

            return ret;
        }

        #endregion

        #region Historial
        //public RetornoAjax CargarHistorial(FiltroGeneral filtro)
        //{

        //    RetornoAjax ret = new RetornoAjax();
        //    List<Historial> historial = new List<Historial>();
        //    var total = 0;
        //    try
        //    {
        //        StringBuilder parametros = new StringBuilder();
        //        string error = String.Empty;

        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PRUT_CLIENTE", "VARCHAR2", "10", "INPUT", filtro.campo1));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PSTART", "VARCHAR2", "10", "INPUT", filtro.start));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PLENGTH", "VARCHAR2", "10", "INPUT", filtro.length));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PDIR", "VARCHAR2", "255", "INPUT", filtro.direction));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PCOLUMNA", "VARCHAR2", "255", "INPUT", filtro.column));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

        //        DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
        //                                                                    this.BaseUtilApp.Instancia,
        //                                                                    this.BaseUtilApp.Package,
        //                                                                    "CARGAR_HISTORIAL",
        //                                                                    parametros.ToString(),
        //                                                                    ref error);

        //        if (string.IsNullOrEmpty(error) && ds != null)
        //        {
        //            var c = ds.Tables[0].AsEnumerable();
        //            if (c != null)
        //            {
        //                historial = (from item in c
        //                             select new Historial
        //                             {
        //                                 N_REQUERIMIENTO = Convert.ToString(item.Field<decimal?>("N_REQUERIMIENTO").GetValueOrDefault()),
        //                                 RUT_CLIENTE = item.Field<string>("RUT_CLIENTE"),
        //                                 NOMBRE_EMPRESA_PERSONA = item.Field<string>("NOMBRE_EMPRESA_PERSONA"),
        //                                 FECHA_HORA_LLAMADA = item.Field<string>("FECHA_HORA_LLAMADA"),
        //                                 HABILIDAD = item.Field<string>("HABILIDAD"),
        //                                 OPERACION = item.Field<string>("OPERACION"),
        //                                 SUBOPERACION = item.Field<string>("SUBOPERACION"),
        //                                 TIPO = item.Field<string>("TIPO"),
        //                             }).ToList();

        //                var tot = from item in ds.Tables[1].AsEnumerable() select item;
        //                foreach (var fila in tot)
        //                {
        //                    total = int.Parse(fila["total"].ToString());
        //                }
        //            }

        //            ret.ret = "OK";
        //            ret.msg = "";
        //            ret.debug = "";
        //        }
        //        else
        //        {
        //            ret.ret = "ERROR";
        //            ret.msg = "Falla al cargar bandeja";
        //            ret.debug = error;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.ret = "ERROR";
        //        ret.msg = "Falla al cargar bandeja";
        //        ret.debug = ex.Message;
        //    }

        //    ret.values = new List<object>();
        //    ret.values.Add(historial);
        //    ret.values.Add(total);

        //    return ret;
        //}
        //public RetornoAjax CargarHistorialID(string id)
        //{

        //    RetornoAjax ret = new RetornoAjax();
        //    Llamada historial = new Llamada();
        //    try
        //    {
        //        StringBuilder parametros = new StringBuilder();
        //        string error = String.Empty;

        //        parametros.Append(this.BaseUtilAsp.CreaParametro("PN_REQUERIMIENTO", "NUMERIC", "10", "INPUT", id));
        //        parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

        //        DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
        //                                                                    this.BaseUtilApp.Instancia,
        //                                                                    this.BaseUtilApp.Package,
        //                                                                    "CARGAR_HISTORIAL_ID",
        //                                                                    parametros.ToString(),
        //                                                                    ref error);

        //        if (string.IsNullOrEmpty(error) && ds != null)
        //        {
        //            var c = ds.Tables[0].AsEnumerable();
        //            if (c != null)
        //            {
        //                historial = (from item in c
        //                             select new Llamada
        //                             {
        //                                 N_REQUERIMIENTO = Convert.ToString(item.Field<decimal?>("N_REQUERIMIENTO").GetValueOrDefault()),
        //                                 RUT_CLIENTE = item.Field<string>("RUT_CLIENTE"),
        //                                 SKILL = item.Field<string>("SKILL"),
        //                                 ANI = item.Field<string>("ANI"),
        //                                 FECHA_HORA_LLAMADA = item.Field<string>("FECHA_HORA_LLAMADA"),
        //                                 CONNID = item.Field<string>("CONNID"),
        //                                 RUT_AGENTE = item.Field<string>("RUT_AGENTE"),
        //                                 CODIGO_SERVICIO = item.Field<string>("CODIGO_SERVICIO"),
        //                                 NOMBRE_EMPRESA_PERSONA = item.Field<string>("NOMBRE_EMPRESA_PERSONA"),
        //                                 NUMERO_CONTACTO = item.Field<string>("NUMERO_CONTACTO"),
        //                                 NOMBRE_CONTACTO = item.Field<string>("NOMBRE_CONTACTO"),
        //                                 MOTIVO_CONTACTO_DIAGNOSTICO = item.Field<string>("MOTIVO_CONTACTO_DIAGNOSTICO"),
        //                                 HABILIDAD = item.Field<string>("HABILIDAD"),
        //                                 OPERACION = item.Field<string>("OPERACION"),
        //                                 SUBOPERACION = item.Field<string>("SUBOPERACION"),
        //                                 TIPO = item.Field<string>("TIPO"),
        //                                 OBSERVACIONES = item.Field<string>("OBSERVACIONES"),
        //                             })
        //                             .SingleOrDefault();

        //            }

        //            ret.ret = "OK";
        //            ret.msg = "";
        //            ret.debug = "";
        //        }
        //        else
        //        {
        //            ret.ret = "ERROR";
        //            ret.msg = "Falla al cargar bandeja";
        //            ret.debug = error;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.ret = "ERROR";
        //        ret.msg = "Falla al cargar bandeja";
        //        ret.debug = ex.Message;
        //    }

        //    ret.values = new List<object>();
        //    ret.values.Add(historial);

        //    return ret;
        //}

        #endregion

        #region EPA

        #endregion
    }
}