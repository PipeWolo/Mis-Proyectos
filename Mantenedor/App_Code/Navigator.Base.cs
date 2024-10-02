using System;
using System.Collections.Generic;
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

        public ServicioWebCliente WebServiceConsultas { get; set; }
        public ServicioWebCliente WebServiceExecutor { get; set; }

        public NavigatorBase()
        {

            this.BaseUtilVariables = new UtilVariables();
            this.BaseUtilAsp = new UtilAsp();
            this.BaseUtilApp = new Aplicacion();

            this.WebServiceConsultas = new ServicioWebCliente();
            this.WebServiceExecutor = new ServicioWebCliente();

            this.BaseUtilApp.IdAplicacion = ConfigurationManager.AppSettings.Get("IDAPLICACION");
            this.BaseUtilApp.Instancia = ConfigurationManager.AppSettings.Get("INSTANCIA");
            this.BaseUtilApp.Package = ConfigurationManager.AppSettings.Get("PACKAGE");
            this.BaseUtilApp.PackageCuenta = ConfigurationManager.AppSettings.Get("PACKAGE_CUENTA");
            this.BaseUtilApp.SaltString = ConfigurationManager.AppSettings.Get("SALT");
        }
    }
}