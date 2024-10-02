using api_generica_ecc.Models;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace api_generica_ecc.Utilities
{
    public class SFTP
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Archivo { get; set; }
        public string Error { get; set; }
        public int MAX_INTENTOS = 3;
        public int TIEMPO_ESPERA = 3000;

        public SFTP()
        {

        }

        public SFTP(string ip, string puerto, string usuario, string password)
        {
            this.Server = ip;
            this.Port = puerto;
            this.Usuario = usuario;
            this.Password = password;
        }


        public bool Upload(string NombreArchivo, string DirectorioOrigen, string DirectorioDestino, ref string error)
        {
            int contador = 1;
            bool subido = false;
            while (subido == false && contador <= MAX_INTENTOS)
            {

                try
                {
                    using (SftpClient sftpClient = new SftpClient(this.Server, int.Parse(this.Port), this.Usuario, this.Password))
                    {
                        if (!sftpClient.IsConnected)
                        {
                            sftpClient.Connect();
                        }

                        string rutaOrigen = string.Format("{0}\\{1}", DirectorioOrigen, NombreArchivo);
                        string rutaDestino = string.Format("{0}/{1}", DirectorioDestino, NombreArchivo);

                        if (!sftpClient.Exists(DirectorioDestino))
                        {
                            sftpClient.CreateDirectory(DirectorioDestino);
                        }

                        using (Stream localFile = File.OpenRead(rutaOrigen))
                        {
                            sftpClient.UploadFile(localFile, rutaDestino);
                            subido = true;
                        }

                        if (sftpClient.IsConnected)
                        {
                            sftpClient.Disconnect();
                        }

                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    contador++;
                    Thread.Sleep(TIEMPO_ESPERA);
                    subido = false;
                }

                if (contador == MAX_INTENTOS)
                {
                    error = "Se ha superado el máximo de intentos para cargar el archivo : " + NombreArchivo + " a sitio sftp";
                    subido = false;
                }
            }
            return subido;
        }

        public byte[] DownloadFile(string NombreArchivo, string DirectorioEnSFTP, ref string error)
        {
            int contador = 1;
            byte[] archivo = new byte[10];
            bool pru = true;

            while (pru)
            {
                if (contador <= MAX_INTENTOS)
                {
                    try
                    {
                        using (SftpClient sftpClient = new SftpClient(this.Server, int.Parse(this.Port), this.Usuario, this.Password))
                        {
                            sftpClient.Connect();

                            sftpClient.ChangeDirectory(DirectorioEnSFTP);

                            using (var fileStream = new MemoryStream())
                            {
                                sftpClient.DownloadFile(NombreArchivo, fileStream);
                                archivo = fileStream.ToArray();
                            }

                            sftpClient.Dispose();

                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        contador++;
                        Thread.Sleep(TIEMPO_ESPERA);
                    }

                }
                else
                {
                    error += ". Se ha superado el máximo de intentos para descargar del archivo : " + NombreArchivo + " desde sitio sftp";
                    pru = false;
                }
            }

            return archivo;

        }

        public bool DeleteFile(string sftpPath, ref string error)
        {
            try
            {
                using (SftpClient sftpClient = new SftpClient(this.Server, int.Parse(this.Port), this.Usuario, this.Password))
                {
                    if (!sftpClient.IsConnected)
                    {
                        sftpClient.Connect();
                    }
                    sftpClient.DeleteFile(sftpPath);
                    sftpClient.Disconnect();
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool DeleteFile(string filename, string folder)
        {
            bool ret = false;
            try
            {
                using (SftpClient sftpClient = new SftpClient(this.Server, int.Parse(this.Port), this.Usuario, this.Password))
                {
                    sftpClient.Connect();
                    if (sftpClient.Exists(folder))
                    {
                        sftpClient.ChangeDirectory(folder);
                        sftpClient.DeleteFile(filename);

                    }
                    sftpClient.Disconnect();
                }
                ret = true;
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
            }

            return ret;
        }

        public bool DeleteFolder(string folder, string subfolder)
        {
            bool ret = false;
            try
            {
                using (SftpClient sftpClient = new SftpClient(this.Server, int.Parse(this.Port), this.Usuario, this.Password))
                {
                    sftpClient.Connect();
                    if (sftpClient.Exists(folder))
                    {
                        sftpClient.ChangeDirectory(folder);
                        sftpClient.DeleteDirectory(subfolder);
                    }
                    sftpClient.Disconnect();
                }
                ret = true;
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
            }

            return ret;
        }

        public bool CreateDirectory(string folder, string subfolder)
        {
            bool ret = false;
            try
            {
                using (SftpClient sftpClient = new SftpClient(this.Server, int.Parse(this.Port), this.Usuario, this.Password))
                {
                    sftpClient.Connect();
                    if (!sftpClient.Exists(folder))
                    {
                        sftpClient.CreateDirectory(folder);
                        sftpClient.CreateDirectory(folder + "/" + subfolder);
                        ret = true;
                    }
                    else
                    {
                        sftpClient.CreateDirectory(folder + "/" + subfolder);
                        ret = true;
                    }
                    sftpClient.Disconnect();
                }
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
            }
            return ret;
        }

        public RetornoSFTP GetFileList(string folder, string extension)
        {
            RetornoSFTP ret = new RetornoSFTP();
            try
            {
                List<SftpFile> toReturn = null;

                using (SftpClient sftpClient = new SftpClient(this.Server, int.Parse(this.Port), this.Usuario, this.Password))
                {
                    sftpClient.Connect();
                    if (sftpClient.Exists(folder))
                    {
                        sftpClient.ChangeDirectory(folder);
                        toReturn = sftpClient.ListDirectory(folder, null).ToList<SftpFile>();
                        ret.ret = "OK";
                        ret.msg = "";
                    }
                    else
                    {
                        ret.ret = "ERROR";
                        ret.msg = "Error en la ubicación de la carpeta de los audios.";
                    }
                    sftpClient.Disconnect();
                }

                //Procesa retorno
                if (toReturn != null)
                {
                    List<string> archivos;
                    if (!String.IsNullOrEmpty(extension))
                    {
                        archivos = toReturn.Where(documento => documento.Name != "." && documento.Name != ".." && documento.Name.Contains(extension)).OrderByDescending(s => s.Attributes.LastWriteTime).Select(s => (string)s.Name).ToList();
                    }
                    else
                    {
                        archivos = toReturn.Where(documento => documento.Name != "." && documento.Name != "..").OrderByDescending(s => s.Attributes.LastWriteTime).Select(s => (string)s.Name).ToList();
                    }

                    ret.cantidad = archivos.Count.ToString();
                    ret.archivos = archivos;
                }
                else
                {
                    ret.ret = "ERROR";
                    ret.msg = "No hay audios disponibles.";
                }


            }
            catch (Exception ex)
            {
                ret.ret = "ERROR";
                ret.msg = "Error al conetar con SFTP: " + ex.Message;
            }

            return ret;
        }


        public bool FileExists(string file, string folder)
        {
            bool ret = false;
            try
            {
                using (SftpClient sftpClient = new SftpClient(this.Server, int.Parse(this.Port), this.Usuario, this.Password))
                {
                    sftpClient.Connect();
                    sftpClient.ChangeDirectory(folder);
                    if (sftpClient.Exists(file))
                    {
                        ret = true;
                    }
                    sftpClient.Disconnect();
                }
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
            }

            return ret;
        }
    }

}
