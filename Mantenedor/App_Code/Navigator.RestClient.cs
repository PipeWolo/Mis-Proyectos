using Navigator.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;


public enum HttpVerb
{
    GET,
    POST,
    PUT,
    DELETE
}

public class RestClient
{
    public string EndPoint { get; set; }
    public HttpVerb Method { get; set; }
    public string ContentType { get; set; }

    public RestClient()
    {
        EndPoint = "";
        Method = HttpVerb.GET;
        ContentType = "application/json";
    }

    public RestClient(string endpoint)
    {
        EndPoint = endpoint;
        Method = HttpVerb.GET;
        ContentType = "application/json";
    }
    public RestClient(string endpoint, HttpVerb method)
    {
        EndPoint = endpoint;
        Method = method;
        ContentType = "application/json";
    }

    public RetornoAjax MakeRequestListObject(string method, string token, HttpVerb type, string bodyJSON, ref string error)
    {
        RetornoAjax ret = new RetornoAjax();

        //Crea instancia de llamado
        RestClient rest = new RestClient(method, type);

        var responseValue = string.Empty;

        try
        {
            var req = (HttpWebRequest)WebRequest.Create(rest.EndPoint);

            req.Method = rest.Method.ToString();
            req.ContentType = rest.ContentType;

            if (!string.IsNullOrEmpty(token))
            {
                req.Headers.Add("Authorization", "Bearer " + token);
            }

            if (!string.IsNullOrEmpty(bodyJSON))
            {
                Byte[] byteArray = null;
                byteArray = Encoding.UTF8.GetBytes(bodyJSON);
                req.ContentLength = byteArray.Length;

                using (Stream dataStream = req.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (var response = (HttpWebResponse)req.GetResponse())
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            ret.ret = "ERROR";
                            ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde.";
                            ret.debug = "Falló al obtener información. Error: " + response.StatusCode;
                            error = "Falló al obtener información. Error: " + response.StatusCode;
                        }
                        else
                        {
                            using (var responseStream = response.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    using (var reader = new StreamReader(responseStream))
                                    {
                                        responseValue = reader.ReadToEnd();
                                    }

                                    JavaScriptSerializer js = new JavaScriptSerializer();
                                    js.MaxJsonLength = 1073741824; //1gb
                                    ret = js.Deserialize<RetornoAjax>(responseValue);
                                }
                                else
                                {
                                    ret.ret = "ERROR";
                                    ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde.";
                                    ret.debug = "API no realizó ningún retorno";
                                    error = "API no realizó ningún retorno";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (var response = (HttpWebResponse)req.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ret.ret = "ERROR";
                        ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde.";
                        ret.debug = "Falló al obtener información. Error: " + response.StatusCode;
                        error = "Falló al obtener información. Error: " + response.StatusCode;
                    }
                    else
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    responseValue = reader.ReadToEnd();
                                }

                                JavaScriptSerializer js = new JavaScriptSerializer();
                                js.MaxJsonLength = 1073741824; //1gb
                                ret = js.Deserialize<RetornoAjax>(responseValue);
                            }
                            else
                            {
                                ret.ret = "ERROR";
                                ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde.";
                                ret.debug = "API no realizó ningún retorno";
                                error = "API no realizó ningún retorno";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ret.ret = "ERROR";
            ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde.";
            ret.debug = ex.Message;
            error = ex.Message;
        }

        return ret;
    }

    public RetornoAjax token(string method, HttpVerb type, ref string error)
    {
        RetornoAjax ret = new RetornoAjax();

        //Crea instancia de llamado
        RestClient rest = new RestClient(method, type);
        var responseValue = string.Empty;

        try
        {
            var req = (HttpWebRequest)WebRequest.Create(rest.EndPoint);

            JavaScriptSerializer js = new JavaScriptSerializer();
            var bodyget = new
            {
                idPublico = "vaafsjRmRB15",
                contrasena = "J0F1VZOD0C5QLG"
            };
            string sbodyget = js.Serialize(bodyget);

            Byte[] byteArray = null;
            byteArray = Encoding.UTF8.GetBytes(sbodyget);
            req.ContentLength = byteArray.Length;

            req.Method = rest.Method.ToString();
            req.ContentType = rest.ContentType;
            req.Headers.Add("Authorization", "JQyecdYxDljXIjeillfOtRgfHcWIPmQvYRyHnWkHTyHEGRYzmvAqRbpiWGXhNLODxrvLXtXBeOvEnwYKUjEvoApLyewcbDxSlYtE");

            using (Stream dataStream = req.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (var response = (HttpWebResponse)req.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ret.ret = "ERROR";
                        ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde.";
                        ret.debug = "Falló al obtener información. Error: " + response.StatusCode;
                        error = "Falló al obtener información. Error: " + response.StatusCode;
                    }
                    else
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    responseValue = reader.ReadToEnd();
                                }

                                //JavaScriptSerializer js = new JavaScriptSerializer();
                                js.MaxJsonLength = 1073741824; //1gb
                                ret = js.Deserialize<RetornoAjax>(responseValue);
                            }
                            else
                            {
                                ret.ret = "ERROR";
                                ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde.";
                                ret.debug = "API no realizó ningún retorno";
                                error = "API no realizó ningún retorno";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ret.ret = "ERROR";
            ret.msg = "Ocurrió un error inesperado, inténtelo mas tarde.";
            ret.debug = ex.Message;
            error = ex.Message;
        }

        return ret;
    }
}