using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using Navigator.Clases;
using Navigator.Base;
using System.Security.Cryptography;
using Navigator.Librerias;
using System.Web.Script.Serialization;

namespace Navigator.Mantenedores
{
    /// <summary>
    /// Mantenedor de Usuarios
    /// </summary>
    public class MantenedorSMS : NavigatorBase
    {
        public RetornoAjax CreaRequerimiento(string idUsuario, string nombre)
        {
            RetornoAjax ret = new RetornoAjax();
            KVP req = new KVP();
            KVP tk = new KVP();
            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                //Datos cabecera
                parametros.Append(this.BaseUtilAsp.CreaParametro("PUSUARIO", "VARCHAR2", "10", "INPUT", idUsuario));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE_USUARIO", "VARCHAR2", "60", "INPUT", nombre));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                            this.BaseUtilApp.Instancia,
                                                            this.BaseUtilApp.Package,
                                                            "CREA_REQUERIMIENTO",
                                                            parametros.ToString(),
                                                            ref error);
                if (grabar != null)
                {
                    var d = grabar.Tables[0].AsEnumerable();
                    if (d != null)
                    {
                        req = (from item in d
                               select new KVP
                               {
                                   KeyName = Convert.ToString(item.Field<decimal?>("ID").GetValueOrDefault()),
                                   KeyValue = item.Field<string>("FECHA"),
                                   KeyValue2 = item.Field<string>("HORA")
                               }).SingleOrDefault();

                    }
                    if (!string.IsNullOrEmpty(req.KeyName))
                    {
                        ret.ret = "OK";
                        ret.msg = "";
                        ret.debug = "";
                        ret.values = new List<object>();
                        ret.values.Add(req);
                    }
                    else
                    {
                        ret.ret = "ERROR";
                        ret.msg = "Ocurrió un error inesperado. Inténtelo mas tarde.";
                        ret.debug = error;
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


            return ret;
        }

        public RetornoAjax Enviar(SMS llamada)
        {
            RetornoAjax ret = new RetornoAjax();
            KVP req = new KVP();
            KVP tk = new KVP();
            string estado = "";
            string sms = "";

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                RestClient rest = new RestClient();
                string API_SMS = "http://192.168.239.203:9023";
                string method = API_SMS + "/envio_unico";
                string error = "";

                sms = "Estimado " + llamada.NOMBRE + " el link para ingresar su solicitud es: "+ llamada.URL + "?ID=" + llamada.ID;

                var bodysms = new
                {
                    idPublico = "vaafsjRmRB15",
                    linkCorto = 0,
                    programado = 0,
                    fechaProgramado = "",
                    mensaje = new
                    {
                        mensaje = sms,
                        destinatario = "56" + llamada.ANI
                    }
                };
                string sbodysms = js.Serialize(bodysms);

                RetornoAjax retSMS = rest.token("http://192.168.239.203:9025/api_sms/get_token", HttpVerb.POST, ref error);

                string tokenJS = js.Serialize(retSMS.values[0]);
                AccesoToken infoToken = js.Deserialize<AccesoToken>(tokenJS);

                retSMS = rest.MakeRequestListObject("http://192.168.239.203:9025/api_sms/envio_unico", infoToken.token, HttpVerb.POST, sbodysms, ref error);
                estado = "OK";
                if (retSMS.ret != "OK")
                {
                    estado = "ERROR";
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                //Datos cabecera
                parametros.Append(this.BaseUtilAsp.CreaParametro("PID", "VARCHAR2", "100", "INPUT", llamada.ID));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PANI", "VARCHAR2", "100", "INPUT", llamada.ANI));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE_CLIENTE", "VARCHAR2", "160", "INPUT", llamada.NOMBRE));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSMS", "VARCHAR2", "100", "INPUT", sms));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PESTADO", "VARCHAR2", "100", "INPUT", estado));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet grabar = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                            this.BaseUtilApp.Instancia,
                                                                            this.BaseUtilApp.Package,
                                                                            "GUARDA_REQUERIMIENTO",
                                                                            parametros.ToString(),
                                                                            ref error);
                if (grabar != null)
                {
                    var d = grabar.Tables[0].AsEnumerable();
                    if (d != null)
                    {
                        req = (from item in d
                               select new KVP
                               {
                                   KeyName = Convert.ToString(item.Field<decimal?>("ID").GetValueOrDefault())
                               }).SingleOrDefault();

                    }
                    if (!string.IsNullOrEmpty(req.KeyName))
                    {
                        #region Integracion API CHILE EXPRESS
                        //string tmptk = CreaTK(llamada, ref error);
                        //if (tmptk != "0")
                        //{
                        //    parametros = new StringBuilder();
                        //    parametros.Append(this.BaseUtilAsp.CreaParametro("PN_REQUERIMIENTO", "NUMERIC", "10", "INPUT", req.KeyName));
                        //    parametros.Append(this.BaseUtilAsp.CreaParametro("PN_CASO", "VARCHAR2", "200", "INPUT", tmptk));
                        //    int filas = 0;
                        //    bool guardaID = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                        //                                                this.BaseUtilApp.Instancia,
                        //                                                this.BaseUtilApp.Package,
                        //                                                "GUARDA_N_CASO",
                        //                                                parametros.ToString(),
                        //                                                ref filas,
                        //                                                ref error);
                        //    tk.KeyName = tmptk;
                        //    ret.ret = "OK";
                        //    ret.msg = "";
                        //    ret.debug = "";
                        //    ret.values = new List<object>();
                        //    ret.values.Add(tk);
                        //}
                        //else
                        //{
                        //    ret.ret = "ERROR";
                        //    ret.msg = "Error al grabar el requerimiento en bases del cliente. Descripción de error: "+ error;
                        //    ret.debug = error;
                        //}
                        #endregion
                        ret.ret = "OK";
                        ret.msg = "";
                        ret.debug = "";
                        ret.values = new List<object>();
                        ret.values.Add(req);
                    }
                    else
                    {
                        ret.ret = "ERROR";
                        ret.msg = "Ocurrió un error inesperado. Inténtelo mas tarde.";
                        ret.debug = error;
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


            return ret;
        }

        public RetornoAjax CargarBandeja(FiltroBandeja filtro, string orden, string dir)
        {
            RetornoAjax ret = new RetornoAjax();

            List<Bandeja> infoBandeja = new List<Bandeja>();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCONNID", "VARCHAR2", "100", "INPUT", filtro.CONNID));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSKILL", "VARCHAR2", "100", "INPUT", filtro.SKILL));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PFECHA", "VARCHAR2", "100", "INPUT", filtro.FECHA));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PANI", "VARCHAR2", "100", "INPUT", filtro.ANI));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSTART", "NUMERIC", "10", "INPUT", filtro.Pagina));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PLENGTH", "VARCHAR2", "10", "INPUT", filtro.Cantidad));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PDIR", "VARCHAR2", "10", "INPUT", dir));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCOLUMNA", "VARCHAR2", "20", "INPUT", orden));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,  //"EPA_EASY_ARCALL",
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_EPA",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var user = ds.Tables[0].AsEnumerable();
                    var tot = from item in ds.Tables[1].AsEnumerable() select item;

                    infoBandeja = (from item in user
                                 select new Bandeja
                                 {
                                     CONNID = item.Field<string>("CONNID"),
                                     ASTERISKID = item.Field<string>("ASTERISKID"),
                                     SKILL = item.Field<string>("SKILL"),
                                     FECHA = item.Field<string>("FECHA"),
                                     AGENTE = item.Field<string>("AGENTE"),
                                     RESPUESTA1 = item.Field<string>("RESPUESTA1"),
                                     RESPUESTA2 = item.Field<string>("RESPUESTA2"),
                                     RESPUESTA3 = item.Field<string>("RESPUESTA3"),
                                     MARCA_IVR = item.Field<string>("MARCA_IVR"),
                                     AUDIO = item.Field<string>("AUDIO"),
                                     ANI = item.Field<string>("ANI"),
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
                    ret.msg = "Error al cargar cliente";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al cargar cliente";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(infoBandeja);
            ret.values.Add(kvp);

            return ret;
        }

        public RetornoAjax DescargarAudio(string connid)
        {
            RetornoAjax ret = new RetornoAjax();

            Bandeja servicio = new Bandeja();
            List<Historial> historial = new List<Historial>();
            Gestion gestion = new Gestion();
            string audio = "";
            string pathServer = "";
            string extension = "";

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCONNID", "VARCHAR2", "50", "INPUT", connid));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,  //"EPA_EASY_ARCALL",
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_AUDIO",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var aud = ds.Tables[0].AsEnumerable();

                    servicio = (from item in aud
                                select new Bandeja
                                {
                                    AUDIO = item.Field<string>("AUDIO"),
                                }).SingleOrDefault();

                    SFTP sftp = new SFTP();
                    bool aux = false;
                    string err = "";
                    extension = Path.GetExtension(servicio.AUDIO);
                    pathServer = HttpContext.Current.Server.MapPath("~") + "/Bandeja/Temp/";

                    audio = sftp.DownloadFile(servicio.AUDIO, "/workdir/AUDIOS_EPA/", pathServer, extension, ref aux, ref err);

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;
                    ret.msg = "Falla al cargar datos";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Falla al cargar datos";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(audio);
            ret.values.Add(servicio.AUDIO);
            ret.values.Add(extension);
            return ret;
        }

        static string ComputeSha256Hash(string rawData, string saltString)
        {
            //Convierte el string de salt a byte[]
            byte[] saltBytes = Encoding.ASCII.GetBytes(saltString);
            //Convierte contraseña a byte[]
            byte[] passwordBytes = Encoding.UTF8.GetBytes(rawData);
            //Se crea byte[] para contener password + salt byte[]
            byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];
            //Se llena byte[] combinado
            for (int i = 0; i < passwordBytes.Length; i++)
            {
                passwordWithSaltBytes[i] = passwordBytes[i];
            }
            for (int i = 0; i < saltBytes.Length; i++)
            {
                passwordWithSaltBytes[passwordBytes.Length + i] = saltBytes[i];
            }
            HashAlgorithm hashAlgorith = new SHA256Managed();

            //Genera byte[] encriptado.
            byte[] hashBytes = hashAlgorith.ComputeHash(passwordWithSaltBytes);

            //Se crea byte[] para guardar el byte[] y encriptado junto con el byte[] de salt.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            //Se llena byte[] combinado
            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashWithSaltBytes[i] = hashBytes[i];
            }
            for (int i = 0; i < saltBytes.Length; i++)
            {
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
            }

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);
            return hashValue;
        }

        public MantenedorSMS()
        {
            
        }
    }
}
