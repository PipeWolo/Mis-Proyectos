using api_generica_ecc.Models;
using api_generica_ecc.Utilities;
using Aspose.Cells;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace api_generica_ecc.Repository
{
    public class GenericoRepository
    {

        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;

        public GenericoRepository(IConfiguration iConfig, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = iConfig;
            this.webHostEnvironment = webHostEnvironment;
        }

        public DataSet SeleccionaRegistros(RequestAPI request, ref string error)
        {
            bool res = true;
            DataSet ds = new DataSet();
            try
            {
                string connectionString = configuration.GetSection("ConnectionStrings").GetSection(request.APLICACION).Value;
                if (connectionString != null && connectionString.Length > 0)
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
                                cmd.CommandText = request.PACKAGE + "." + request.PROCEDURE;
                                cmd.CommandType = CommandType.StoredProcedure;
                                List<ParametroAPI> paramsOut = new List<ParametroAPI>();
                                ProcesaParametros(request.PARAMETROS, paramsOut, cmd, con);

                                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                                {
                                    da.Fill(ds);
                                    error = cmd.Parameters["PERROR"].Value != null ? cmd.Parameters["PERROR"].Value.ToString() : "";
                                    ProcesarParametrosSalida(paramsOut, ds, cmd);
                                }

                            }
                            catch (Exception ex)
                            {
                                error = ex.Message;
                                con.Close();
                                con.Dispose();
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
                else
                {
                    error = "ERR001";
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return ds;
        }

        public DataSet AccionesRegistros(RequestAPI request, ref string error)
        {
            bool res = true;
            DataSet ds = new DataSet();
            try
            {
                string connectionString = configuration.GetSection("ConnectionStrings").GetSection(request.APLICACION).Value;
                if (connectionString != null && connectionString.Length > 0)
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
                                cmd.CommandText = request.PACKAGE + "." + request.PROCEDURE;
                                cmd.CommandType = CommandType.StoredProcedure;
                                List<ParametroAPI> paramsOut = new List<ParametroAPI>();
                                ProcesaParametros(request.PARAMETROS, paramsOut, cmd, con);

                                cmd.ExecuteNonQuery();
                                error = cmd.Parameters["PERROR"].Value != null ? cmd.Parameters["PERROR"].Value.ToString() : "";
                                ProcesarParametrosSalida(paramsOut, ds, cmd);

                            }
                            catch (Exception ex)
                            {
                                error = ex.Message;
                                res = false;
                                con.Close();
                                con.Dispose();
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
                else
                {
                    error = "ERR001";
                    res = false;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                res = false;
            }
            return ds;
        }

        public void ProcesaParametros(List<ParametroAPI> parametros, List<ParametroAPI> paramsOut, OracleCommand cmd, OracleConnection con)
        {

            foreach (ParametroAPI param in parametros)
            {
                if (param.TIPO == "CLOB")
                {
                    string valor = param.VALOR;
                    byte[] binaryData = System.Text.Encoding.Unicode.GetBytes(valor);
                    OracleClob clob = new OracleClob(con);
                    clob.Write(binaryData, 0, binaryData.Length);
                    ParameterDirection direction = ParameterDirection.Input;
                    if (param.DIRECCION == "INPUT")
                    {
                        direction = ParameterDirection.Input;
                    }
                    else if (param.DIRECCION == "OUTPUT")
                    {
                        direction = ParameterDirection.Output;
                        paramsOut.Add(param);
                    }
                    cmd.Parameters.Add(param.NOMBRE, OracleDbType.Clob, clob, direction);
                }
                else if (param.TIPO == "BLOB")
                {
                    string valor = param.VALOR;
                    byte[] binaryData = Convert.FromBase64String(valor);
                    OracleBlob blob = new OracleBlob(con);
                    blob.Write(binaryData, 0, binaryData.Length);
                    ParameterDirection direction = ParameterDirection.Input;
                    if (param.DIRECCION == "INPUT")
                    {
                        direction = ParameterDirection.Input;
                    }
                    else if (param.DIRECCION == "OUTPUT")
                    {
                        direction = ParameterDirection.Output;
                        paramsOut.Add(param);
                    }
                    cmd.Parameters.Add(param.NOMBRE, OracleDbType.Blob, blob, direction);
                }
                else if (param.TIPO == "CURSOR")
                {
                    ParameterDirection direction = ParameterDirection.Output;
                    //if (param.DIRECCION == "INPUT")
                    //{
                    //    direction = ParameterDirection.Input;
                    //}
                    //else if (param.DIRECCION == "OUTPUT")
                    //{
                    //direction = ParameterDirection.Output;
                    //}
                    cmd.Parameters.Add(param.NOMBRE, OracleDbType.RefCursor, direction);
                }
                else
                {
                    OracleDbType type = new OracleDbType();
                    if (param.TIPO == "VARCHAR2")
                    {
                        type = OracleDbType.Varchar2;
                    }
                    else if (param.TIPO == "NUMBER" || param.TIPO == "NUMERIC")
                    {
                        type = OracleDbType.Double;
                    }

                    ParameterDirection direction = new ParameterDirection();
                    if (param.DIRECCION == "INPUT")
                    {
                        direction = ParameterDirection.Input;
                    }
                    else if (param.DIRECCION == "OUTPUT")
                    {
                        direction = ParameterDirection.Output;
                        paramsOut.Add(param);
                    }

                    cmd.Parameters.Add(param.NOMBRE, type, int.Parse(param.LARGO), param.VALOR, direction);

                }
            }

            cmd.Parameters.Add("PERROR", OracleDbType.Varchar2, 4000).Value = "";
            cmd.Parameters["PERROR"].Direction = ParameterDirection.Output;
        }

        public void ProcesarParametrosSalida(List<ParametroAPI> paramsOut, DataSet ds, OracleCommand cmd)
        {

            foreach (ParametroAPI param in paramsOut)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(param.NOMBRE, typeof(string));
                if (param.TIPO == "CLOB")
                {
                    if (cmd.Parameters[param.NOMBRE].Value != null)
                    {
                        OracleClob value = (OracleClob)cmd.Parameters[param.NOMBRE].Value;
                        DataRow dr = dt.NewRow();
                        dr[param.NOMBRE] = value.Value;
                        dt.Rows.Add(dr);
                        ds.Tables.Add(dt);
                    }
                }
                else if (param.TIPO == "BLOB")
                {
                    if (cmd.Parameters[param.NOMBRE].Value != null)
                    {
                        OracleBlob value = (OracleBlob)cmd.Parameters[param.NOMBRE].Value;
                        DataRow dr = dt.NewRow();
                        dr[param.NOMBRE] = Convert.ToBase64String(value.Value);
                        dt.Rows.Add(dr);
                        ds.Tables.Add(dt);
                    }
                }
                else
                {
                    string value = cmd.Parameters[param.NOMBRE].Value != null ? cmd.Parameters[param.NOMBRE].Value.ToString() : "";
                    DataRow dr = dt.NewRow();
                    dr[param.NOMBRE] = value;
                    dt.Rows.Add(dr);
                    ds.Tables.Add(dt);
                }
            }
        }

        public bool CargaMasiva(RequestAPICM request, DataTable dt, ref string error)
        {
            string connectionString = configuration.GetSection("ConnectionStrings").GetSection(request.APLICACION).Value;
            error = "OK";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.TnsAdmin = configuration.GetSection("TNS_ADMIN_ROUTE").Value;
                string SQLString = string.Format("select {0} from {1} where rownum=0", request.COLUMNAS, request.TABLA);
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        using (OracleBulkCopy bulkCopy = new OracleBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = request.TABLA;
                            bulkCopy.WriteToServer(dt);
                        }
                        //    OracleDataAdapter myDataAdapter = new OracleDataAdapter();
                        //myDataAdapter.SelectCommand = new OracleCommand(SQLString, connection);
                        //myDataAdapter.UpdateBatchSize = 0;
                        //OracleCommandBuilder custCB = new OracleCommandBuilder(myDataAdapter);
                        //DataTable dtTemp = dt.Clone();

                        //int times = 0;
                        ////for (int count = 0; count < dt.Rows.Count; times++)
                        //{
                        //    for (int i = 0; i < 400 && 400 * times + i < dt.Rows.Count; i++, count++)
                        //    {
                        //        dtTemp.Rows.Add(dt.Rows[count].ItemArray);
                        //    }
                        //myDataAdapter.Update(dt);
                        //dtTemp.Rows.Clear();
                        //}

                        dt.Dispose();
                        //dtTemp.Dispose();
                        //myDataAdapter.Dispose();
                        return true;
                    }
                    catch (Exception E)
                    {
                        error = E.Message;
                        connection.Close();
                        connection.Dispose();
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        public bool LimpiarTablas(ref string error)
        {
            bool res = true;
            try
            {
                string connectionString = configuration.GetSection("ConnectionStrings").GetSection("RPA_SMBO_PERU").Value;
                if (connectionString != null && connectionString.Length > 0)
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
                                cmd.CommandText = "SMARTBACKOFFICE.SP_RPA_LIMPIAR_SMBO_RPA_PERU";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                error = ex.Message;
                                con.Close();
                                con.Dispose();
                                res = false;
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
                else
                {
                    error = "ERR001";
                    res = false;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                res = false;
            }
            return res;
        }

        public DataSet ProcesarArchivo(RequestAPIArchivo request, ref string error)
        {
            bool res = true;
            DataSet ds = new DataSet();
            try
            {
                var urlSFTP = configuration.GetSection("SFTP").GetSection("URL").Value;
                var puertoSFTP = configuration.GetSection("SFTP").GetSection("PUERTO").Value;
                var usuarioSFTP = configuration.GetSection("SFTP").GetSection("USUARIO").Value;
                var contrasenaSFTP = configuration.GetSection("SFTP").GetSection("CONTRASENA").Value;
                var directorioSFTP = configuration.GetSection("SFTP").GetSection("DIRECTORIO").Value;

                List<Registro> listado = new List<Registro>();

                try
                {
                    SFTP miSftp = new SFTP(urlSFTP, puertoSFTP, usuarioSFTP, contrasenaSFTP);
                    if (miSftp.FileExists(request.NOMBRE_ARCHIVO, directorioSFTP))
                    {
                        byte[] archivoBytes = miSftp.DownloadFile(request.NOMBRE_ARCHIVO, directorioSFTP, ref error);
                        if (String.IsNullOrEmpty(error))
                        {
                            Stream stream = new MemoryStream(archivoBytes);
                            // Instantiate a Workbook object
                            //Opening the Excel file through the file stream
                            CultureInfo culturaChile = new CultureInfo("es-CL");
                            //cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                            //cultureInfo.DateTimeFormat.LongDatePattern = "dd/MM/yyyy HH:mm:ss";
                            Workbook workbook = new Workbook(stream, new XmlLoadOptions() { CultureInfo = culturaChile, LoadFilter = new LoadFilter(LoadDataFilterOptions.CellData), CheckExcelRestriction = false });

                            // Access the first worksheet in the Excel file
                            Worksheet worksheet = workbook.Worksheets[0];

                            // Export the contents of 2 rows and 2 columns starting from 1st cell to DataTable
                            DataTable Lineas = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, new ExportTableOptions() { CheckMixedValueType = false, ExportColumnName = true, ExportAsString = true });
                            stream.Close();
                            try
                            {
                                foreach (DataRow valores in Lineas.Rows)
                                {
                                    if (valores["Nº de SS"] != null)
                                    {
                                        Registro registro = new Registro();
                                        #region PROCESANDO DATOS
                                        //DNI
                                        if (Lineas.Columns.Contains("Nº de SS") && valores["Nº de SS"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Nº de SS"].ToString().Trim();
                                                registro.N_SS = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.N_SS = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.N_SS = "";
                                        }

                                        //NOMBRE
                                        if (Lineas.Columns.Contains("Tipo") && valores["Tipo"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Tipo"].ToString().Trim();
                                                registro.TIPO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.TIPO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.TIPO = "";
                                        }

                                        //MUESTRA
                                        if (Lineas.Columns.Contains("Área") && valores["Área"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Área"].ToString().Trim();
                                                registro.AREA = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.AREA = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.AREA = "";
                                        }

                                        //Forma De Expresarse
                                        if (Lineas.Columns.Contains("Subárea") && valores["Subárea"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Subárea"].ToString().Trim();
                                                registro.SUBAREA = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.SUBAREA = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.SUBAREA = "";
                                        }

                                        //Tiempo de espera
                                        if (Lineas.Columns.Contains("Estado") && valores["Estado"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Estado"].ToString().Trim();
                                                registro.ESTADO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.ESTADO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.ESTADO = "";
                                        }

                                        //Cumple Procesos De Atencion
                                        if (Lineas.Columns.Contains("Fecha de apertura") && valores["Fecha de apertura"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Fecha de apertura"].ToString().Trim();
                                                registro.FECHA_DE_APERTURA = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.FECHA_DE_APERTURA = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.FECHA_DE_APERTURA = "";
                                        }

                                        //Uso De Los Aplicativos
                                        if (Lineas.Columns.Contains("Descripción") && valores["Descripción"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Descripción"].ToString().Trim();
                                                registro.DESCRIPCION = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.DESCRIPCION = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.DESCRIPCION = "";
                                        }

                                        //Abordaje Crosselling
                                        if (Lineas.Columns.Contains("Tipo de documento") && valores["Tipo de documento"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Tipo de documento"].ToString().Trim();
                                                registro.TIPO_DOCUMENTO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.TIPO_DOCUMENTO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.TIPO_DOCUMENTO = "";
                                        }

                                        //Manejo De Objeciones Crosselling
                                        if (Lineas.Columns.Contains("Número de documento") && valores["Número de documento"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Número de documento"].ToString().Trim();
                                                registro.NUMERO_DOCUMENTO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.NUMERO_DOCUMENTO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.NUMERO_DOCUMENTO = "";
                                        }

                                        //Cierre De Venta Y Aceptación Cross Selling
                                        if (Lineas.Columns.Contains("Contacto") && valores["Contacto"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Contacto"].ToString().Trim();
                                                registro.CONTACTO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.CONTACTO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.CONTACTO = "";
                                        }

                                        //Protocolo Y Personalización
                                        if (Lineas.Columns.Contains("Grupo") && valores["Grupo"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Grupo"].ToString().Trim();
                                                registro.GRUPO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.GRUPO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.GRUPO = "";
                                        }

                                        //Conocimiento De La Información
                                        if (Lineas.Columns.Contains("Creado por") && valores["Creado por"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Creado por"].ToString().Trim();
                                                registro.CREADO_POR = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.CREADO_POR = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.CREADO_POR = "";
                                        }

                                        //Conocimiento De La Información
                                        if (Lineas.Columns.Contains("Subestado") && valores["Subestado"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Subestado"].ToString().Trim();
                                                registro.SUBESTADO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.SUBESTADO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.SUBESTADO = "";
                                        }

                                        //Conocimiento De La Información
                                        if (Lineas.Columns.Contains("Nº de activo") && valores["Nº de activo"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Nº de activo"].ToString().Trim();
                                                registro.N_ACTIVO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.N_ACTIVO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.N_ACTIVO = "";
                                        }

                                        //Conocimiento De La Información
                                        if (Lineas.Columns.Contains("SS Principal") && valores["SS Principal"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["SS Principal"].ToString().Trim();
                                                registro.SS_PRINCIPAL = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.SS_PRINCIPAL = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.SS_PRINCIPAL = "";
                                        }

                                        //Conocimiento De La Información
                                        if (Lineas.Columns.Contains("Plan del activo") && valores["Plan del activo"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Plan del activo"].ToString().Trim();
                                                registro.PLAN_ACTIVO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.PLAN_ACTIVO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.PLAN_ACTIVO = "";
                                        }

                                        //Conocimiento De La Información
                                        if (Lineas.Columns.Contains("Tipo de Activo") && valores["Tipo de Activo"] != null)
                                        {
                                            try
                                            {
                                                var VALOR = valores["Tipo de Activo"].ToString().Trim();
                                                registro.TIPO_ACTIVO = VALOR;
                                            }
                                            catch (Exception)
                                            {
                                                registro.TIPO_ACTIVO = "";
                                            }
                                        }
                                        else
                                        {
                                            registro.TIPO_ACTIVO = "";
                                        }
                                        #endregion PROCESANDO DATOS

                                        if (registro.N_SS != null && registro.N_SS.Trim().Length > 0)
                                        {
                                            listado.Add(registro);
                                        }
                                    }

                                }

                            }
                            catch (Exception ex)
                            {
                                error = "Error al recorrer lineas de archivo: " + ex.Message;
                                return null;
                            }
                        }
                    }
                    else
                    {
                        error = "Error al descargar archivo de SFTP: No existe el archivo en el repositorio.";
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    error = "Error al descargar archivo de SFTP: " + ex.Message;
                    return null;
                }



                if (LimpiarTablas(ref error))
                {

                    RequestAPICM reqCM = new RequestAPICM();
                    reqCM.APLICACION = "RPA_SMBO_PERU";
                    //CONSEGUIR DE BASE DE DATOS
                    reqCM.TABLA = "TB_SMBO_RPA_PERU";
                    reqCM.COLUMNAS = "N_SS, TIPO, AREA, SUBAREA, ESTADO, FECHA_DE_APERTURA, DESCRIPCION, TIPO_DOCUMENTO, NUMERO_DOCUMENTO, CONTACTO, GRUPO, CREADO_POR, SUBESTADO, N_DE_ACTIVO, SS_PRINCIPAL, PLAN_DEL_ACTIVO, TIPO_DE_ACTIVO ";
                    DataTable dt = Utilidades.ToDataTable(listado, "asignaciones");

                    bool masiva = CargaMasiva(reqCM, dt, ref error);

                    if (masiva)
                    {
                        string connectionString = configuration.GetSection("ConnectionStrings").GetSection(reqCM.APLICACION).Value;
                        if (connectionString != null && connectionString.Length > 0)
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
                                        cmd.CommandText = "SMARTBACKOFFICE.SP_AGREGACION_DE_DATOS";
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.ExecuteNonQuery();

                                    }
                                    catch (Exception ex)
                                    {
                                        error = ex.Message;
                                        con.Close();
                                        con.Dispose();
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
                        else
                        {
                            error = "ERR001";
                            ds = null;
                        }
                    }

                }
                else
                {
                    ds = null;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                ds = null;
            }
            return ds;
        }

    }
}
