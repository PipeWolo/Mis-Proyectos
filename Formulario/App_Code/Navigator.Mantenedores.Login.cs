using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Navigator.Clases;
using Navigator.Base;
using System.Security.Cryptography;
using System.Configuration;

namespace Navigator.Login
{
    public class ControlAcceso : NavigatorBase
    {
        public RetornoAjax Login(string usuario, string password)
        {

            RetornoAjax ret = new RetornoAjax();

            Usuario info = new Usuario();

            string hash = "";
            if (password.Length > 0)
            {
                string saltString = ConfigurationManager.AppSettings.Get("SALT");
                hash = ComputeSha256Hash(password, saltString);
            }

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PAGENTE", "VARCHAR2", "100", "INPUT", usuario));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PPASSWORD", "VARCHAR2", "500", "INPUT", hash));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "LOGIN",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var user = ds.Tables[0].AsEnumerable();

                    info = (from item in user
                            select new Usuario
                            {
                                ID_USUARIO = Convert.ToString(item.Field<decimal>("ID")),
                                USUARIO = usuario,
                                NOMBRE = item.Field<string>("NOMBRE_COMPLETO"),
                                CODIGO_SERVICIO = item.Field<string>("CODIGO_SERVICIO"),
                            }).SingleOrDefault();

                    if (info == null) {
                        ret.ret = "ERROR";
                        ret.msg = "El usuario no ha sido asignado a ninguna campaña.";
                        ret.debug = "ERROR_USUARIO_CONTRASENA";
                    }
                    else
                    {
                        ret.ret = "OK";
                        ret.msg = String.Empty;
                        ret.debug = String.Empty;
                    }
                }
                else
                {
                    string msg = "Usuario/Contraseña incorrectos.";
                    ret.msg = "Fallo al cargar información de login";
                    
                    ret.ret = "ERROR";
                    ret.msg = msg;
                    ret.debug = msg;
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