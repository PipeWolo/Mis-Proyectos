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
    public class MantenedorCampanas : NavigatorBase
    {
        public RetornoAjax CargarCampanas(FiltroCampana filtro, string orden, string dir)
        {
            RetornoAjax ret = new RetornoAjax();

            List<Campana> campanas = new List<Campana>();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PSTART", "NUMERIC", "10", "INPUT", filtro.Pagina));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PLENGTH", "VARCHAR2", "10", "INPUT", filtro.Cantidad));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PDIR", "VARCHAR2", "10", "INPUT", dir));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCOLUMNA", "VARCHAR2", "20", "INPUT", orden));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_CAMPANAS",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var user = ds.Tables[0].AsEnumerable();
                    var tot = from item in ds.Tables[1].AsEnumerable() select item;

                    campanas = (from item in user
                               select new Campana
                               {
                                   CODIGO_SERVICIO = item.Field<string>("CODIGO_SERVICIO"),
                                   NOMBRE_CAMPANA = item.Field<string>("NOMBRE_CAMPANA"),
                                   SKILL_1 = item.Field<string>("SKILL_1"),
                                   SKILL_2 = item.Field<string>("SKILL_2"),
                                   MODO_DISCADO = item.Field<string>("MODO_DISCADO"),
                                   PREFIJO = item.Field<string>("PREFIJO"),
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
            ret.values.Add(campanas);
            ret.values.Add(kvp);

            return ret;
        }

        public RetornoAjax GrabarCampana(Campana campana)
        {
            RetornoAjax ret = new RetornoAjax();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", campana.CODIGO_SERVICIO));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE_CAMPANA", "VARCHAR2", "100", "INPUT", campana.NOMBRE_CAMPANA));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSKILL_1", "VARCHAR2", "100", "INPUT", campana.SKILL_1));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSKILL_2", "VARCHAR2", "100", "INPUT", campana.SKILL_2));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PMODO_DISCADO", "VARCHAR2", "100", "INPUT", campana.MODO_DISCADO));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PPREFIJO", "VARCHAR2", "100", "INPUT", campana.PREFIJO));
                
                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                     this.BaseUtilApp.Instancia,
                                                                     this.BaseUtilApp.Package,
                                                                     "GRABAR_CAMPANA",
                                                                     parametros.ToString(),
                                                                     ref filas,
                                                                     ref error);


                if (res)
                {
                    ret.ret = "OK";
                    ret.msg = "Campaña editada correctamente.";
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    if (error.Contains("ERR001"))
                    {
                        ret.debug = error;
                        ret.msg = "Ya existe un cliente con el nombre ingresado.";
                    }
                    else
                    {
                        ret.debug = error;
                        ret.msg = "Ocurrió un error inesperado, inténtelo más tarde.";
                    }
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Falla al grabar datos";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(kvp);

            return ret;
        }

        public RetornoAjax CargarCampanaId(string CS)
        {
            RetornoAjax ret = new RetornoAjax();

            Campana campana = new Campana();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", CS));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_CAMPANA_CS",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var usu = ds.Tables[0].AsEnumerable();



                    campana = (from item in usu
                               select new Campana
                               {
                                   CODIGO_SERVICIO = item.Field<string>("CODIGO_SERVICIO"),
                                   NOMBRE_CAMPANA = item.Field<string>("NOMBRE_CAMPANA"),
                                   SKILL_1 = item.Field<string>("SKILL_1"),
                                   SKILL_2 = item.Field<string>("SKILL_2"),
                                   MODO_DISCADO = item.Field<string>("MODO_DISCADO"),
                                   PREFIJO = item.Field<string>("PREFIJO"),
                               }).SingleOrDefault();


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
            ret.values.Add(campana);
            return ret;
        }

        public RetornoAjax ReciclarCampana(string CS, string Segmento)
        {
            RetornoAjax ret = new RetornoAjax();

            KVP cont = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PCODIGO_SERVICIO", "VARCHAR2", "30", "INPUT", CS));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSEGMENTO", "VARCHAR2", "30", "INPUT", Segmento));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "RECICLAR_CAMPANA",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var usu = ds.Tables[0].AsEnumerable();



                    cont = (from item in usu
                               select new KVP
                               {
                                   KeyValue = Convert.ToString(item.Field<decimal?>("CONTADOR")),
                               }).SingleOrDefault();


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
            ret.values.Add(cont);
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

        public MantenedorCampanas()
        {
            
        }
    }
}
