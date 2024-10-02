using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de Navigator
/// </summary>
namespace Navigator.Librerias
{
    public class FTP2
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Archivo { get; set; }
        public string Path { get; set; }
        public string Error { get; set; }

        /// <summary>
        /// Sube un archivo al servidor
        /// </summary>
        /// <param name="filename">El archivo a subir al servidor</param>
        /// <returns>Verdadero si el archivo fue subido al servidor de forma exitosa</returns>
        public bool UploadFile(string filename, string file, ref string error)
        {
            bool ret = false;

            if (!this.Server.EndsWith("/")) { this.Server += "/"; }
            if (!this.Path.EndsWith("/")) { this.Path += "/"; }

            string uri = this.Server + this.Path + filename;

            this.Archivo = uri;

            FtpWebRequest reqFTP;

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(this.Usuario, this.Password);
            reqFTP.KeepAlive = false;
            reqFTP.Proxy = null;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;

            try
            {

                byte[] data = System.Convert.FromBase64String(file);
                reqFTP.ContentLength = data.Length;

                Stream stream = reqFTP.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                FtpWebResponse res = (FtpWebResponse)reqFTP.GetResponse();

                ret = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return ret;
        }

        /// <summary>
        /// Borrar un archivo desde el servidor FTP
        /// </summary>
        /// <param name="fileName">El archivo a eliminar</param>
        /// <returns>Verdadero si el archivo fue eliminado del servidor de forma exitosa</returns>
        public bool DeleteFile(string fileName, ref string error)
        {
            bool ret = false;

            try
            {
                if (!this.Server.EndsWith("/")) { this.Server += "/"; }
                if (!this.Path.EndsWith("/")) { this.Path += "/"; }

                string uri = this.Server + this.Path + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                reqFTP.Credentials = new NetworkCredential(this.Usuario, this.Password);
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.KeepAlive = false;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
                ret = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return ret;
        }


        /// <summary>
        /// Bajar un archivo desde el servidor FTP
        /// </summary>
        /// <param name="filePath">El path del archivo</param>
        /// <param name="fileName">El nombre del archivo</param>
        /// <returns>Verdadero si el archivo fue descargado desde el servidor FTP de forma exitosa</returns>
        public string DownloadFile(string filePathServer, string fileName, ref bool ret, ref string error)
        {
            string base64 = string.Empty;
            string file = filePathServer + fileName;
            ret = false;
            FtpWebRequest reqFTP;
            try
            {

                using (FileStream outputStream = new FileStream(file, FileMode.Create))
                {
                    if (!this.Server.EndsWith("/")) { this.Server += "/"; }
                    if (!this.Path.EndsWith("/")) { this.Path += "/"; }

                    this.Archivo = this.Server + this.Path + fileName;

                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.Archivo));
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(this.Usuario, this.Password);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    Stream ftpStream = response.GetResponseStream();
                    long cl = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[bufferSize];

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                    }
                }

                try
                {
                    Byte[] bytes = File.ReadAllBytes(file);
                    base64 = Convert.ToBase64String(bytes);

                    File.Delete(file);
                    ret = true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    base64 = string.Empty;
                }


            }
            catch (Exception ex)
            {
                base64 = string.Empty;
                error = ex.Message;
            }

            return base64;
        }

        private string[] GetFilesDetailList()
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.Server + "/"));
                reqFTP.Credentials = new NetworkCredential(this.Usuario, this.Password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();

                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                downloadFiles = null;
                return downloadFiles;
            }
        }

        /// <summary>
        /// Obtiene la lista de archivos desde el servidor FTP
        /// </summary>
        /// <returns>Arreglo conteniendo la lista de archivos desde el servidor FTP</returns>
        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.Server + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.Usuario, this.Password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public List<string> GetFileListFolder(ref string cantidad, ref string error)
        {
            List<string> list = new List<string>();
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                if (!this.Server.EndsWith("/")) { this.Server += "/"; }
                if (!this.Path.EndsWith("/")) { this.Path += "/"; }
                string uri = this.Server + this.Path;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.KeepAlive = false;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.Usuario, this.Password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                if (line != null)
                {
                    while (line != null)
                    {
                        result.Append(line);
                        result.Append("\n");
                        line = reader.ReadLine();
                    }
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    reader.Close();
                    response.Close();
                    list = result.ToString().Split('\n').ToList();
                    cantidad = list.Count().ToString();
                    error = "";
                }
                else
                {
                    cantidad = "0";
                    error = "";
                }
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                error = ex.Message;
            }

            return list;
        }

        public bool CreateDirectory(string folderName, ref string error)
        {
            bool ret = false;
            FtpWebRequest reqFTP = null;
            Stream ftpStream = null;
            //Intenta crear carpeta
            try
            {
                if (!this.Server.EndsWith("/")) { this.Server += "/"; }
                if (!this.Path.EndsWith("/")) { this.Path += "/"; }

                string carpeta = this.Server + this.Path + folderName;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(carpeta);
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.KeepAlive = false;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.Usuario, this.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
                error = "EXISTE";
                //error = ex.Message;
            }

            return ret;
        }

        public bool DeleteDirectory(string folderName, ref string error)
        {
            bool ret = false;
            //Intenta eliminar carpetas
            try
            {
                if (!this.Server.EndsWith("/")) { this.Server += "/"; }
                if (!this.Path.EndsWith("/")) { this.Path += "/"; }

                string carpeta = this.Server + this.Path + folderName;
                NetworkCredential credentials = new NetworkCredential(this.Usuario, this.Password);

                DeleteDirectoryRecursive(carpeta, credentials);

                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
                error = ex.Message;
            }

            return ret;
        }

        static void DeleteDirectoryRecursive(string url, NetworkCredential credentials)
        {
            FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(url);
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = credentials;

            List<string> lines = new List<string>();

            using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (StreamReader listReader = new StreamReader(listStream))
            {
                string line = listReader.ReadLine();
                if (line != null)
                {
                    while (line != null)
                    {
                        lines.Add(line);
                        line = listReader.ReadLine();
                    }
                }
            }

            foreach (string line in lines)
            {
                try
                {
                    string[] tokens =
                    line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                    string permissions = tokens[0];
                    string name = tokens[8];

                    if (!url.EndsWith("/")) { url += "/"; }
                    string fileUrl = url + name;

                    if (permissions[0] == 'd')
                    {
                        DeleteDirectoryRecursive(fileUrl + "/", credentials);
                    }
                    else
                    {
                        FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                        deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                        deleteRequest.Credentials = credentials;

                        deleteRequest.GetResponse();
                    }
                }
                catch (Exception)
                {
                    string[] tokens =
                    line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                    string permissions = tokens[2];
                    string name = tokens[3];

                    if (!url.EndsWith("/")) { url += "/"; }
                    string fileUrl = url + name;

                    if (permissions == "<DIR>")
                    {
                        DeleteDirectoryRecursive(fileUrl + "/", credentials);
                    }
                    else
                    {
                        FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                        deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                        deleteRequest.Credentials = credentials;

                        deleteRequest.GetResponse();
                    }
                }

            }

            FtpWebRequest removeRequest = (FtpWebRequest)WebRequest.Create(url);
            removeRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            removeRequest.Credentials = credentials;

            removeRequest.GetResponse();
        }

        public FTP2()
        {

        }

        public FTP2(string ip, string puerto, string usuario, string password, string path)
        {
            this.Server = "ftp://" + ip;
            this.Port = puerto;
            this.Usuario = usuario;
            this.Password = password;
            this.Path = path;
        }
    }
}