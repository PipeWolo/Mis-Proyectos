using Navigator.Ajax.Main;
using Navigator.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Navigator.Softphone;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Navigator.Login;
using System.Security.Cryptography;
using System.Text;

public partial class Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Llamada llamada = new Llamada();

        var NumeroLlamada = Request.QueryString["NumeroLlamada"];
        var Agente = Request.QueryString["Agente"];
        var NombreLista = Request.QueryString["NombreLista"];
        var ConnIdDec = Request.QueryString["ConnIdDec"];
        var ConnIdHex = Request.QueryString["ConnIdHex"];
        var CodigoServicio = Request.QueryString["CodigoServicio"];
        var Habilidad = Request.QueryString["Habilidad"];
        var Numero_Cliente = Request.QueryString["Numero_Cliente"];
        var IdentificadorCliente = Request.QueryString["IdentificadorCliente"];
        var KeySP = Request.QueryString["KeySP"];
        var RUT = Request.QueryString["RUT"];

        this.hidprefijoEPA.Value = ConfigurationManager.AppSettings.Get("PREFIJO");
        this.hidvdnEPA.Value = ConfigurationManager.AppSettings.Get("VDN");

        //Datos de prueba
        NumeroLlamada = "313160";
        Agente = "18146832";
        NombreLista = "HDC_YOMESUMO2_O";
        ConnIdDec = "029702669F3AB9E5";
        ConnIdHex = "029702669F3AB9E5";
        CodigoServicio = "EBAA1408";
        Habilidad = "SKILL_HDC_YOMESUMO2_O";
        Numero_Cliente = "992515189";
        IdentificadorCliente = "16093343";

        llamada.ConnIdDec = string.IsNullOrEmpty(ConnIdDec) ? "0" : ConnIdDec;        

        if (Agente == null)
            llamada.Agente = Convert.ToString(Session["USUARIO"]);
            
        try
        {
            var ajax = new NavigatorMain();
            RetornoAjax ret = new RetornoAjax();
            string nombreAgente = "";

            if (llamada.ConnIdDec == "0")
            {
                llamada.Agente = Convert.ToString(Session["USUARIO"]);
                llamada.KeySP = KeySP;

                if (llamada.Agente == "")
                {
                    this.hMensajeError.Value = "Usuario no autenticado.";
                    return;
                }
                else if (llamada.KeySP == "")
                {
                    //this.hMensajeError.Value = "No se ha declarado la conexion a Softphone.";
                    //return;
                }

                ret = ajax.CargaCampanaAgente(llamada);
                if (ret.ret == "OK")
                {
                    List<KVP> DatosCampana = (List<KVP>)ret.values[0];

                    ListItem itmx = null;
                    itmx = new ListItem("Seleccione...", "0");
                    this.cboCampanas.Items.Clear();
                    this.cboCampanas.Items.Add(itmx);
                    foreach (KVP InfoCampana in DatosCampana)
                    {
                        string campana = InfoCampana.KeyValue;
                        string codigoServicio = InfoCampana.KeyValue2;
                        nombreAgente = InfoCampana.KeyName;

                        itmx = new ListItem(campana, codigoServicio);
                        this.cboCampanas.Items.Add(itmx);
                    }

                    this.txtAgente.Value = nombreAgente;
                    this.txtNombreAgente.Value = nombreAgente;
                }

                if (llamada.KeySP != null && !String.IsNullOrEmpty(llamada.KeySP.ToString()))
                {
                    Softphone softphone = new Softphone();

                    softphone.Conectar(llamada.KeySP);

                    this.hidmaquina.Value = softphone.lamaquina;
                    this.hiderror.Value = softphone.MsgError;

                    Session["softphone"] = softphone;
                }

                this.hAgente.Value = llamada.Agente;
                this.pnlAsistido.Visible = true;
            }
            else //Significa que es predictiva
            {
                llamada.Agente = Agente;
                ret = ajax.CargaCampanaAgente(llamada);
                if (ret.ret == "OK")
                {
                    List<KVP> DatosCampana = (List<KVP>)ret.values[0];

                    foreach (KVP InfoCampana in DatosCampana)
                    {
                        nombreAgente = InfoCampana.KeyName;
                    }

                    this.txtAgente.Value = nombreAgente;
                    this.txtNombreAgente.Value = nombreAgente;
                }

                llamada.NumeroLlamada = NumeroLlamada;
                llamada.Agente = Agente;
                llamada.NombreLista = NombreLista;
                llamada.ConnIdDec = ConnIdDec;
                llamada.ConnIdHex = ConnIdHex;
                llamada.CodigoServicio = CodigoServicio;
                llamada.Skill = Habilidad;
                llamada.Numero_Cliente = Numero_Cliente;
                llamada.IdentificadorCliente = IdentificadorCliente;
                llamada.KeySP = KeySP;
                llamada.RUT = RUT;

                this.hRecordID.Value = NumeroLlamada;
                this.hAgente.Value = Agente;
                this.hNombreLista.Value = NombreLista;
                this.hConnIdDec.Value = ConnIdDec;
                this.hConnIdHex.Value = ConnIdHex;
                this.txtConnID.Value = ConnIdHex;
                this.txtCodigoServicio.Value = CodigoServicio;
                this.hCodigoServicio.Value = CodigoServicio;
                this.txtSkillCampana.Value = Habilidad;
                this.txtFonoContacto.Value = Numero_Cliente;
                this.hIdentificadorCliente.Value = IdentificadorCliente;
                this.hKeySP.Value = KeySP;
                this.hRUT.Value = RUT;

                if (llamada.KeySP != null && !String.IsNullOrEmpty(llamada.KeySP.ToString()))
                {
                    Softphone softphone = new Softphone();

                    softphone.Conectar(llamada.KeySP);

                    this.hidmaquina.Value = softphone.lamaquina;
                    this.hiderror.Value = softphone.MsgError;

                    Session["softphone"] = softphone;
                }
                llamada.ConnIdHex = ConnIdHex;
                RetornoAjax EnProceso = ajax.EnProceso(llamada);
                KVP NumLlamada = (KVP)EnProceso.values[0];
                this.txtNLlamada.Value = NumLlamada.KeyName;
                this.pnlAsistido.Visible = false;
                this.txtFonoContacto.Disabled = true;
            }
                
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctlr_main.Mensaje('Error','" + ex.Message + "','error');", true);
            Response.Redirect("Login.aspx?msg=" + ex.Message);
        }
        
    }

    [WebMethod]
    public static RetornoAjax CargaDatosCampana(string CodigoServicio)
    {
        var ajax = new NavigatorMain();

        return ajax.CargaDatosCampaña(CodigoServicio);
    }

    [WebMethod]
    public static RetornoAjax CargarTipificaciones(string CodigoServicio)
    {
        var ajax = new NavigatorMain();
        
        return ajax.CargarTipificaciones(CodigoServicio);
    }

    [WebMethod]
    public static RetornoAjax ModoDiscado(string CodigoServicio)
    {
        var ajax = new NavigatorMain();

        return ajax.ModoDiscado(CodigoServicio);
    }

    [WebMethod]
    public static RetornoAjax CargarDatosPredictivo(string IdentificadorCliente, string CodigoServicio, string Skill)
    {
        var ajax = new NavigatorMain();
        return ajax.CargarDatosPredictivo(IdentificadorCliente, CodigoServicio, Skill);
    }

    [WebMethod]
    public static RetornoAjax PedirRegistro(string CodigoServicio, string NumeroLlamada, string Agente)
    {
        var ajax = new NavigatorMain();
        return ajax.PedirRegistro(CodigoServicio, NumeroLlamada, Agente);
    }

    [WebMethod]
    public static RetornoAjax Grabar(Llamada llamada, string OpcionLlamado)
    {
        var ajax = new NavigatorMain();
        return ajax.Grabar(llamada, OpcionLlamado);
    }

    [WebMethod]
    public static RetornoAjax Buscar(string Rut, string CodigoServicio)
    {
        var ajax = new NavigatorMain();
        return ajax.Buscar(Rut, CodigoServicio);
    }
}