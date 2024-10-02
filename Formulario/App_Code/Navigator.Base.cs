using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using Entel.ECC.UtilNet;
using Navigator.Clases;
using webservicecliente;
/// <summary>
/// MotorBase
/// </summary>
namespace Navigator.Base
{
    public class NavigatorBase
    {

        public string MsgError { get; set; }
        public string Usuario { get; set; }

        public Aplicacion BaseUtilApp { get; set; }
        public UtilVariables BaseUtilVariables { get; set; }
        public UtilAsp BaseUtilAsp { get; set; }

        public webservicecliente.ServicioWebCliente WebServiceConsultas { get; set; }
        public webservicecliente.ServicioWebCliente WebServiceExecutor { get; set; }
        public NavigatorBase()
        {
            this.BaseUtilVariables = new UtilVariables();
            this.BaseUtilAsp = new UtilAsp();
            this.BaseUtilApp = new Aplicacion();

            //Webservices
            this.WebServiceConsultas = new webservicecliente.ServicioWebCliente();
            this.WebServiceExecutor = new webservicecliente.ServicioWebCliente();

            //CONFIGURADOR IRIS - GENERAL
            this.BaseUtilApp.IdAplicacion = ConfigurationManager.AppSettings.Get("IDAPLICACION");
            this.BaseUtilApp.Instancia = ConfigurationManager.AppSettings.Get("INSTANCIA");
            this.BaseUtilApp.Package = ConfigurationManager.AppSettings.Get("PACKAGE");
            this.BaseUtilApp.ApiUrl = ConfigurationManager.AppSettings.Get("API_URL");
            this.BaseUtilApp.ApiKey = ConfigurationManager.AppSettings.Get("API_KEY");
            this.BaseUtilApp.ApiChexpressURL = ConfigurationManager.AppSettings.Get("API_CHEXPRESS");
            this.BaseUtilApp.ApiChexpressAuth = ConfigurationManager.AppSettings.Get("API_CHEXPRESS_AUTHORIZATION");
            this.BaseUtilApp.ApiChexpressSistema = ConfigurationManager.AppSettings.Get("API_CHEXPRESS_SISTEMA");
            this.BaseUtilApp.idServicio = ConfigurationManager.AppSettings.Get("ID_SERVICIO");
        }
    }
}