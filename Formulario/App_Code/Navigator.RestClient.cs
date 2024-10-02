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
    public class BodyRequest
    {
        public string package { get; set; }
        public string procedure { get; set; }
        public List<ParametersBodyRequest> parameters { get; set; }
    }
    public class ParametersBodyRequest
    {
        public string name { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }

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

    public RetornoAjax MakeRequestListObject(string tmpmethod, string parameters, HttpVerb type, ref string error)
    {
        RetornoAjax ret = new RetornoAjax();

        //Crea instancia de llamado
        string API_CHEXPRESS = ConfigurationManager.AppSettings.Get("API_CHEXPRESS");
        string API_CHEXPRESS_AUTHORIZATION = ConfigurationManager.AppSettings.Get("API_CHEXPRESS_AUTHORIZATION");
        string method = string.Format("{0}{1}?{2}", API_CHEXPRESS, tmpmethod, parameters);
        RestClient rest = new RestClient(method, type);

        var responseValue = string.Empty;

        try
        {
            var req = (HttpWebRequest)WebRequest.Create(rest.EndPoint);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            req.Method = rest.Method.ToString();
            req.ContentType = rest.ContentType;

            if (!string.IsNullOrEmpty(API_CHEXPRESS_AUTHORIZATION))
            {
                req.Headers.Add("Ocp-Apim-Subscription-Key", API_CHEXPRESS_AUTHORIZATION);
            }

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

                            ret.ret = "OK";
                            ret.values = new List<object>();
                            ret.values.Add(responseValue);

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