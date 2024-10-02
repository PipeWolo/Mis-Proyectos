using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Navigator.Clases;
using Navigator.Base;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using Navigator.Librerias;

namespace Navigator.GestionCuenta
{
    public class NavigatorGestionCuenta : NavigatorBase
    {
        public RetornoAjax CambiarContrasenaCuenta(string idusuario, string passwordActual, string password)
        {

            RetornoAjax ret = new RetornoAjax();

            Usuario info = new Usuario();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;
                string salt = this.BaseUtilApp.SaltString;
                string hashActual = cSHA256.SHA256HashSalt(passwordActual, salt);
                string hash = cSHA256.SHA256HashSalt(password, salt);
                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_USUARIO", "VARCHAR2", "4000", "INPUT", idusuario));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCONTRASENA_ACTUAL", "VARCHAR2", "500", "INPUT", hashActual));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCONTRASENA_NUEVA", "VARCHAR2", "500", "INPUT", hash));

                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                        this.BaseUtilApp.Instancia,
                                                                        this.BaseUtilApp.PackageCuenta,
                                                                        "CAMBIA_CONTRASENA_CUENTA",
                                                                        parametros.ToString(),
                                                                        ref filas,
                                                                        ref error);

                if (res)
                {
                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else if (error.Contains("NO_PASS"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "La contraseña ingresada no es valida.";
                    ret.debug = error;
                }
                else if (error.Contains("EQUAL_OLD"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "La contraseña ingresada es una de las ultimas 3 utilizadas.";
                    ret.debug = error;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al cargar informacion.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al cargar informacion";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(info);

            return ret;
        }

        public RetornoAjax ValidaTokenOlvidoContrasena(string guid)
        {

            RetornoAjax ret = new RetornoAjax();

            Usuario info = new Usuario();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PGUID", "VARCHAR2", "4000", "INPUT", guid));

                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                        this.BaseUtilApp.Instancia,
                                                                        this.BaseUtilApp.PackageCuenta,
                                                                        "VALIDA_TOKEN_OLVIDO_CONTRASENA",
                                                                        parametros.ToString(),
                                                                        ref filas,
                                                                        ref error);

                if (res)
                {
                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else if (error.Contains("NO_TOKEN"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "El token no existe.";
                    ret.debug = error;
                }
                else if (error.Contains("TOKEN_EXPIRED"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "El token expiro.";
                    ret.debug = error;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al cargar informacion";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al cargar informacion";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(info);

            return ret;
        }

        public RetornoAjax CambiarContrasenaForgot(string guid, string contrasenaNueva)
        {

            RetornoAjax ret = new RetornoAjax();

            Usuario info = new Usuario();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;
                string salt = this.BaseUtilApp.SaltString;
                string hash = cSHA256.SHA256HashSalt(contrasenaNueva, salt);

                parametros.Append(this.BaseUtilAsp.CreaParametro("PGUID", "VARCHAR2", "4000", "INPUT", guid));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCONTRASENA_NUEVA", "VARCHAR2", "500", "INPUT", hash));

                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                        this.BaseUtilApp.Instancia,
                                                                        this.BaseUtilApp.PackageCuenta,
                                                                        "CAMBIA_CONTRASENA_FORGOT",
                                                                        parametros.ToString(),
                                                                        ref filas,
                                                                        ref error);

                if (res)
                {
                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else if (error.Contains("NO_USUARIO"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "El usuario ingresado no existe.";
                    ret.debug = error;
                }
                else if (error.Contains("EQUAL_PASS"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "La contraseña ingresada es la misma a la actual.";
                    ret.debug = error;
                }
                else if (error.Contains("EQUAL_OLD"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "La contraseña ingresada corresponde a una de las ultimas 3 utilizadas.";
                    ret.debug = error;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al cargar informacion.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al cargar informacion.";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(info);

            return ret;
        }

        public RetornoAjax GeneraTokenOlvidoContrasena(string usuario)
        {

            RetornoAjax ret = new RetornoAjax();

            Usuario info = new Usuario();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE_USUARIO", "VARCHAR2", "4000", "INPUT", usuario));

                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                        this.BaseUtilApp.Instancia,
                                                                        this.BaseUtilApp.PackageCuenta,
                                                                        "GENERA_TOKEN_OLVIDO_CONTRASENA",
                                                                        parametros.ToString(),
                                                                        ref filas,
                                                                        ref error);

                if (res)
                {
                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else if (error.Contains("NO_USUARIO"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "El usuario ingresado no existe.";
                    ret.debug = error;
                }
                else if (error.Contains("NO_CORREO"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "El usuario ingresado no tiene correo asociado.";
                    ret.debug = error;
                }
                else if (error.Contains("ERROR_CORREO"))
                {
                    ret.ret = "ERROR";
                    ret.msg = "Ocurrio un error al enviar el correo. Por favor intente nuevamente.";
                    ret.debug = error;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Error al cargar informacion.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al cargar informacion.";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(info);

            return ret;
        }

        public RetornoAjax NotificacionVencimientoContrasena(string id_usuario)
        {

            RetornoAjax ret = new RetornoAjax();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_USUARIO", "VARCHAR2", "4000", "INPUT", id_usuario));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                        this.BaseUtilApp.Instancia,
                                                                        this.BaseUtilApp.PackageCuenta,
                                                                        "NOTIFICACION_VENCIMIENTO_PASS",
                                                                        parametros.ToString(),
                                                                        ref error);

                if (ds != null)
                {

                    var user = ds.Tables[0].AsEnumerable();

                    kvp = (from item in user
                           select new KVP
                           {
                               KeyValue = item.Field<string>("MENSAJE"),
                           }).FirstOrDefault();

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = error;
                    ret.msg = "Error al cargar mensaje.";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al cargar mensaje.";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(kvp);

            return ret;
        }

        public NavigatorGestionCuenta()
        {

        }
    }
}