using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Navigator.Clases;
using Navigator.Base;
using System.Security.Cryptography;

namespace Navigator.Login
{
    public class ControlAcceso : NavigatorBase
    {
        public RetornoAjax Login(string usuario, string password)
        {

            RetornoAjax ret = new RetornoAjax();

            Usuario info = new Usuario();
            List<MapaAcceso> menu = new List<MapaAcceso>();

            string hash = "";
            if (password.Length > 0)
            {
                string saltString = this.BaseUtilApp.SaltString;
                hash = ComputeSha256Hash(password, saltString);
            }

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PUSUARIO", "VARCHAR2", "100", "INPUT", usuario));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCONTRASENA", "VARCHAR2", "500", "INPUT", hash));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.PackageCuenta,
                                                                          "INICIAR_SESION_MANT",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var user = ds.Tables[0].AsEnumerable();

                    info = (from item in user
                            select new Usuario
                            {
                                ID_USUARIO = Convert.ToString(item.Field<decimal>("ID_USUARIO")),
                                USUARIO = usuario,
                                PASSWORD = password,
                                NOMBRE = item.Field<string>("NOMBRE"),
                                ID_PERFIL = Convert.ToString(item.Field<decimal>("ID_PERFIL")),
                                PERFIL = item.Field<string>("PERFIL"),
                                CAMBIO_CONTRASENA = Convert.ToString(item.Field<decimal>("CAMBIO_CONTRASENA")),
                                CORREO = item.Field<string>("CORREO"),
                            }).SingleOrDefault();
                    if (user != null)
                    {
                        var b = ds.Tables[1].AsEnumerable();
                        menu = (from item in b
                                select new MapaAcceso
                                {
                                    ID_MODULO = Convert.ToString(item.Field<decimal>("ID_MODULO")),
                                    MODULO = item.Field<string>("MODULO"),
                                    DESCRIPCION = item.Field<string>("DESCRIPCION"),
                                    RUTA = item.Field<string>("RUTA"),
                                    ICONO = item.Field<string>("ICONO"),
                                    SECCION = item.Field<string>("SECCION"),
                                    ID_MODULO_PADRE = Convert.ToString(item.Field<decimal>("ID_MODULO_PADRE")),
                                    ORDEN_PADRE = Convert.ToString(item.Field<decimal>("ORDEN_PADRE")),
                                    ORDEN_HIJO = Convert.ToString(item.Field<decimal>("ORDEN_HIJO")),
                                    TIENE_HIJO = Convert.ToString(item.Field<decimal>("TIENE_HIJO"))
                                }).ToList();

                        if (menu.Count > 0)
                        {
                            ret.ret = "OK";
                            ret.msg = String.Empty;
                            ret.debug = String.Empty;
                        }
                        else
                        {
                            ret.ret = "ERROR";
                            ret.msg = "No se encontró mapa de acceso. Comuníquese con el administrador.";
                            ret.debug = "No se encontró mapa de acceso. Comuníquese con el administrador.";
                        }

                    }
                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;
                }
                else
                {
                    string msg = "Usuario/Contraseña incorrectos.";
                    ret.msg = "Fallo al cargar información de login";
                    if (error.Contains("USUARIO_BLOQUEADO"))
                    {
                        msg = "Su usuario esta bloqueado.";
                    }
                    else if (error.Contains("BLOQUEADO_INTENTOS"))
                    {
                        msg = "Su usuario fue bloqueado por exceso de intentos.";
                    }
                    else if (error.Contains("ERROR_CONTRASENA"))
                    {
                        string intentos = "0";
                        if (error.Split('#').Length > 1)
                        {
                            intentos = error.Split('#')[1];
                        }
                        msg = "Contraseña incorrecta. Numero de intentos fallidos: " + intentos + ".";
                    }
                    else if (error.Contains("ERROR_USUARIO_CONTRASENA"))
                    {
                        msg = "Usuario o contraseña ingresados son invalidos.";
                    }
                    ret.ret = "ERROR";
                    ret.msg = msg;
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Fallo al cargar información de login";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(info);
            ret.values.Add(menu);
            return ret;
        }

        public RetornoAjax GrabarCierre(string idUsuario)
        {

            RetornoAjax ret = new RetornoAjax();

            Usuario info = new Usuario();
            List<MapaAcceso> menu = new List<MapaAcceso>();

            string hash = "";

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;
                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_USUARIO", "VARCHAR2", "100", "INPUT", idUsuario));

                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                      this.BaseUtilApp.Instancia,
                                                                      this.BaseUtilApp.PackageCuenta,
                                                                      "GRABAR_LOG_CIERRE",
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
                    ret.msg = "Fallo al grabar información de cierre de sesión.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Fallo al grabar información de cierre de sesión.";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            return ret;
        }

        public string ComputeSha256Hash(string rawData, string saltString)
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

        public ControlAcceso()
        {

        }
    }
}