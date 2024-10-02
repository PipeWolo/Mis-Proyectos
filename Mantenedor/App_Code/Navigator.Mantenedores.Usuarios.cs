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
    public class MantenedorUsuarios : NavigatorBase
    {
        public RetornoAjax CargarCombosUsuario()
        {
            RetornoAjax ret = new RetornoAjax();
            List<KVP> perfiles = new List<KVP>();
            List<KVP> nombreUsuarios = new List<KVP>();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                    this.BaseUtilApp.Instancia,
                                                                    this.BaseUtilApp.Package,
                                                                    "CARGAR_COMBOS_USUARIO",
                                                                    parametros.ToString(),
                                                                    ref error);

                if (ds != null)
                {
                    var c = ds.Tables[0].AsEnumerable();
                    var u = ds.Tables[1].AsEnumerable();

                    if (c != null)
                    {
                        perfiles = (from item in c
                                    select new KVP
                                    {
                                        KeyName = Convert.ToString(item.Field<decimal?>("ID_PERFIL")),
                                        KeyValue = item.Field<string>("PERFIL"),
                                    }).ToList();
                    }

                    if (u != null)
                    {
                        nombreUsuarios = (from item in u
                                    select new KVP
                                    {
                                        KeyName = item.Field<string>("USUARIO"),
                                    }).ToList();
                    }

                    ret.ret = "OK";
                    ret.msg = String.Empty;
                    ret.debug = String.Empty;

                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "Ocurrió un error inesperado, inténtelo más tarde.";
                    ret.debug = error;
                }
            }
            catch (Exception ex)
            {
                ret.msg = "Falla al cargar datos" + ex.Message;
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(perfiles);
            ret.values.Add(nombreUsuarios);
            
            return ret;
        }

        public RetornoAjax CargarUsuario(FiltroUsuario filtro, string orden, string dir)
        {
            RetornoAjax ret = new RetornoAjax();

            List<Usuario> usuario = new List<Usuario>();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PUSUARIO", "VARCHAR2", "30", "INPUT", filtro.Usuario));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_PERFIL", "NUMERIC", "10", "INPUT", filtro.IdPerfil));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSTART", "NUMERIC", "10", "INPUT", filtro.Pagina));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PLENGTH", "VARCHAR2", "10", "INPUT", filtro.Cantidad));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PDIR", "VARCHAR2", "10", "INPUT", dir));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCOLUMNA", "VARCHAR2", "20", "INPUT", orden));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_USUARIOS",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var user = ds.Tables[0].AsEnumerable();
                    var tot = from item in ds.Tables[1].AsEnumerable() select item;

                    usuario = (from item in user
                               select new Usuario
                               {
                                   ID_USUARIO = Convert.ToString(item.Field<decimal?>("ID_USUARIO").GetValueOrDefault()),
                                   USUARIO = item.Field<string>("USUARIO"),
                                   NOMBRE = item.Field<string>("NOMBRE"),
                                   ID_PERFIL = Convert.ToString(item.Field<decimal?>("ID_PERFIL").GetValueOrDefault()),
                                   PERFIL = item.Field<string>("PERFIL"),
                                   CORREO = item.Field<string>("CORREO"),
                                   FECHA_CREACION = item.Field<string>("FECHA_CREACION"),
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
                    ret.msg = "Error al cargar usuario";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al cargar usuario";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(usuario);
            ret.values.Add(kvp);

            return ret;
        }

        public RetornoAjax GrabarUsuario(Usuario usuario)
        {
            RetornoAjax ret = new RetornoAjax();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;
                string hash = "";
                if (usuario.PASSWORD.Length > 0)
                {
                    string saltString = this.BaseUtilApp.SaltString;
                    hash = ComputeSha256Hash(usuario.PASSWORD, saltString);
                }
                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_USUARIO", "NUMERIC", "10", "INPUT", usuario.ID_USUARIO));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PUSUARIO", "VARCHAR2", "100", "INPUT", usuario.USUARIO));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PPASSWORD", "VARCHAR2", "400", "INPUT", hash));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PNOMBRE", "VARCHAR2", "100", "INPUT", usuario.NOMBRE));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCORREO", "VARCHAR2", "255", "INPUT", usuario.CORREO));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_PERFIL", "NUMERIC", "10", "INPUT", usuario.ID_PERFIL));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                     this.BaseUtilApp.Instancia,
                                                                     this.BaseUtilApp.Package,
                                                                     "GRABAR_USUARIO",
                                                                     parametros.ToString(),
                                                                     ref error);


                if (ds != null)
                {
                    var usu = ds.Tables[0].AsEnumerable();

                    kvp = (from item in usu
                               select new KVP
                               {
                                    KeyName = Convert.ToString(item.Field<decimal?>("ID_USUARIO").GetValueOrDefault()),
                               }).SingleOrDefault();


                    ret.ret = "OK";
                    if(usuario.ID_USUARIO == "0")
                    {
                        ret.msg = "Usuario creado correctamente.";
                    }
                    else
                    {
                        ret.msg = "Usuario editado correctamente.";
                    }

                    if (kvp.KeyName == "0")
                    {
                        ret.msg = "Error al crear Usuario.";
                    }
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    if (error.Contains("ERR001"))
                    {
                        ret.debug = error;
                        ret.msg = "Ya existe otro usuario con el mismo nombre de usuario.";
                    }
                    else if (error.Contains("EQUAL_OLD"))
                    {
                        ret.debug = error;
                        ret.msg = "No puede utilizar una de las ultimas 3 claves del usuario.";

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

        public RetornoAjax CargarUsuarioId(string id)
        {
            RetornoAjax ret = new RetornoAjax();

            Usuario usuario = new Usuario();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;


                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_USUARIO", "NUMERIC", "10", "INPUT", id));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_USUARIO_ID",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var usu = ds.Tables[0].AsEnumerable();

                    usuario = (from item in usu
                                    select new Usuario
                                    {
                                        ID_USUARIO = Convert.ToString(item.Field<decimal?>("ID_USUARIO").GetValueOrDefault()),
                                        USUARIO = item.Field<string>("USUARIO"),
                                        NOMBRE = item.Field<string>("NOMBRE"),
                                        ID_PERFIL = Convert.ToString(item.Field<decimal?>("ID_PERFIL").GetValueOrDefault()),
                                        PERFIL = item.Field<string>("PERFIL"),
                                        CORREO = item.Field<string>("CORREO"),
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
            ret.values.Add(usuario);
            return ret;
        }

        public RetornoAjax EliminarUsuario(string ID_USUARIO)
        {
            RetornoAjax ret = new RetornoAjax();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;
                int filas = 0;
                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_USUARIO", "NUMERIC", "10", "INPUT", ID_USUARIO));

                bool res = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                     this.BaseUtilApp.Instancia,
                                                                     this.BaseUtilApp.Package,
                                                                     "ELIMINAR_USUARIO",
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
                    ret.msg = "Ocurrió un error inesperado, inténtelo más tarde.";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Falla al eliminar datos";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(kvp);

            return ret;
        }

        public RetornoAjax CargarLogUsuario(FiltroLogUsuario filtro, string orden, string dir)
        {
            RetornoAjax ret = new RetornoAjax();

            List<LogUsuario> logs = new List<LogUsuario>();

            KVP kvp = new KVP();

            try
            {
                StringBuilder parametros = new StringBuilder();
                string error = String.Empty;

                parametros.Append(this.BaseUtilAsp.CreaParametro("PID_USUARIO", "VARCHAR2", "30", "INPUT", filtro.ID_USUARIO));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PSTART", "NUMERIC", "10", "INPUT", filtro.Pagina));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PLENGTH", "VARCHAR2", "10", "INPUT", filtro.Cantidad));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PDIR", "VARCHAR2", "10", "INPUT", dir));
                parametros.Append(this.BaseUtilAsp.CreaParametro("PCOLUMNA", "VARCHAR2", "20", "INPUT", orden));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR", "CURSOR", "0", "OUTPUT", "0"));
                parametros.Append(this.BaseUtilAsp.CreaParametro("IO_CURSOR2", "CURSOR", "0", "OUTPUT", "0"));

                DataSet ds = this.WebServiceConsultas.SeleccionaRegistros(this.BaseUtilApp.IdAplicacion,
                                                                          this.BaseUtilApp.Instancia,
                                                                          this.BaseUtilApp.Package,
                                                                          "CARGAR_LOG_USUARIO",
                                                                          parametros.ToString(),
                                                                          ref error);

                if (ds != null)
                {

                    var user = ds.Tables[0].AsEnumerable();
                    var tot = from item in ds.Tables[1].AsEnumerable() select item;

                    logs = (from item in user
                               select new LogUsuario
                               {
                                   ID_LOG = Convert.ToString(item.Field<decimal?>("ID_LOG").GetValueOrDefault()),
                                   ID_USUARIO = Convert.ToString(item.Field<decimal?>("ID_USUARIO").GetValueOrDefault()),
                                   USUARIO = item.Field<string>("USUARIO"),
                                   ID_TIPO = Convert.ToString(item.Field<decimal?>("ID_TIPO").GetValueOrDefault()),
                                   TIPO = item.Field<string>("TIPO"),
                                   FECHA = item.Field<string>("FECHA"),
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
                    ret.msg = "Error al cargar usuario";
                }
            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al cargar usuario";
                ret.debug = ex.Message;
            }

            ret.values = new List<object>();
            ret.values.Add(logs);
            ret.values.Add(kvp);

            return ret;
        }

        public RetornoAjax SubirCargaUsuarios(List<Usuario> usuarios)
        {
            RetornoAjax ret = new RetornoAjax();
            KVP idCarga = new KVP();
            List<ResumenValidacion> resumen = new List<ResumenValidacion>();

            try
            {
                string errorValidacion = String.Empty;

                DataTable dtTipif = UsuariosToDataTable(usuarios);

                DataSet dsTipif = new DataSet();
                dsTipif.Tables.Add(dtTipif);

                var columnas = "USUARIO, PASSWORD, NOMBRE, CORREO, ID_PERFIL";

                bool res = this.WebServiceConsultas.MultiInsertData(this.BaseUtilApp.IdAplicacion,
                                                                    dsTipif,
                                                                    columnas,
                                                                    "TB_HDC_OUT_USUARIOS",
                                                                    ref errorValidacion);

                if (res)
                {
                    StringBuilder parametros = new StringBuilder();
                    string error = String.Empty;
                    int filas = 0;

                    bool res2 = this.WebServiceConsultas.AccionesRegistros(this.BaseUtilApp.IdAplicacion,
                                                                     this.BaseUtilApp.Instancia,
                                                                     this.BaseUtilApp.Package,
                                                                     "LIMPIA_USUARIOS",
                                                                     parametros.ToString(),
                                                                     ref filas,
                                                                     ref error);
                    ret.ret = "OK";
                    ret.msg = "Se han cargado los usuarios válidos correctamente.";
                    ret.debug = String.Empty;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.debug = errorValidacion;
                    ret.msg = "Ha ocurrido un error al cargar los usuarios.";
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

        private DataTable UsuariosToDataTable(List<Usuario> usuarios)
        {
            DataTable result = new DataTable();
            if (usuarios.Count == 0)
                return result;

            result.Columns.Add("USUARIO");
            result.Columns.Add("PASSWORD");
            result.Columns.Add("NOMBRE");
            result.Columns.Add("CORREO");
            result.Columns.Add("ID_PERFIL");

            foreach (Usuario tipif in usuarios)
            {
                var row = result.NewRow();

                string hash = "";
                string saltString = this.BaseUtilApp.SaltString;
                hash = ComputeSha256Hash(tipif.USUARIO, saltString);

                row["USUARIO"] = tipif.USUARIO;
                row["PASSWORD"] = hash;
                row["NOMBRE"] = tipif.NOMBRE;
                row["CORREO"] = tipif.CORREO;
                row["ID_PERFIL"] = tipif.ID_PERFIL;

                result.Rows.Add(row);
            }

            return result;
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

        public MantenedorUsuarios()
        {
            
        }
    }
}
