using api_generica_ecc.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Web;

namespace api_generica_ecc.Repository
{
    public class TokenizadorRepository
    {
        public DataSet ValidarApiKey(string apikey, ref string error)
        {
            DataSet ds = new DataSet();
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            IConfigurationRoot configuration = builder.Build();
            string aplicacion = configuration.GetSection("Jwt").GetSection("aplicacion").Value;
            string connectionString = configuration.GetSection("ConnectionStrings").GetSection(aplicacion).Value;
            try
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.TnsAdmin = configuration.GetSection("TNS_ADMIN_ROUTE").Value;
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        try
                        {
                            con.Open();
                            OracleTransaction tx = con.BeginTransaction();
                            cmd.Transaction = tx;
                            cmd.CommandText = "PKG_SMBO_RPA_PERU.VALIDAR_APIKEY";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("PAPIKEY", OracleDbType.Varchar2, 255, apikey, ParameterDirection.Input);
                            cmd.Parameters.Add("IO_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                            cmd.Parameters.Add("PERROR", OracleDbType.Varchar2, 4000).Value = "";
                            cmd.Parameters["PERROR"].Direction = ParameterDirection.Output;

                            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                            {
                                da.Fill(ds);
                                error = cmd.Parameters["PERROR"].Value != null ? cmd.Parameters["PERROR"].Value.ToString() : "";
                            }
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message;
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            OracleConnection.ClearPool(con);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return ds;
        }

        internal bool ValidarToken(string token, ref string error)
        {
            bool res = true;

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            IConfigurationRoot configuration = builder.Build();
            string aplicacion = configuration.GetSection("Jwt").GetSection("aplicacion").Value;
            string connectionString = configuration.GetSection("ConnectionStrings").GetSection(aplicacion).Value;
            try
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.TnsAdmin = configuration.GetSection("TNS_ADMIN_ROUTE").Value;
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        try
                        {
                            con.Open();
                            OracleTransaction tx = con.BeginTransaction();
                            cmd.Transaction = tx;
                            cmd.CommandText = "PKG_SMBO_RPA_PERU.VALIDAR_TOKEN_USUARIO";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("PTOKEN", OracleDbType.Varchar2, 500, token, ParameterDirection.Input);
                            cmd.Parameters.Add("PERROR", OracleDbType.Varchar2, 4000).Value = "";
                            cmd.Parameters["PERROR"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            error = cmd.Parameters["PERROR"].Value != null ? cmd.Parameters["PERROR"].Value.ToString() : "";
                            if (error != "OK")
                            {
                                res = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message;
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            OracleConnection.ClearPool(con);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return res;
        }

        public bool GrabarToken(Usuario usuario, string token, ref string error)
        {
            bool res = true;

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            IConfigurationRoot configuration = builder.Build();
            string aplicacion = configuration.GetSection("Jwt").GetSection("aplicacion").Value;
            string connectionString = configuration.GetSection("ConnectionStrings").GetSection(aplicacion).Value;
            try
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.TnsAdmin = configuration.GetSection("TNS_ADMIN_ROUTE").Value;
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        try
                        {
                            con.Open();
                            OracleTransaction tx = con.BeginTransaction();
                            cmd.Transaction = tx;
                            cmd.CommandText = "PKG_SMBO_RPA_PERU.GRABAR_TOKEN_USUARIO";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("PUSUARIO_API", OracleDbType.Varchar2, 255, usuario.USUARIO_API, ParameterDirection.Input);
                            cmd.Parameters.Add("PTOKEN", OracleDbType.Varchar2, 500, token, ParameterDirection.Input);
                            cmd.Parameters.Add("PERROR", OracleDbType.Varchar2, 4000).Value = "";
                            cmd.Parameters["PERROR"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            error = cmd.Parameters["PERROR"].Value != null ? cmd.Parameters["PERROR"].Value.ToString() : "";

                        }
                        catch (Exception ex)
                        {
                            error = ex.Message;
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            OracleConnection.ClearPool(con);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return res;
        }
    }
}
