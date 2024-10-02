using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using econtactSoftphone;

namespace Navigator.Softphone
{
    /// <summary>
    /// Summary description for Navigator
    /// </summary>
    public class Softphone
    {

        public RemoteClient client { get; set; }
        public string MsgError { get; set; }
        public string ConnId { get; set; }
        public string lamaquina { get; set; }

        public void Conectar(object ip)
        {
            try
            {

                string maquina = ip.ToString();
                string stringstart = maquina.Remove(6, maquina.Length - 6);
                string ipmaquina = maquina.Substring(6, maquina.Length - 6);
                string stringend = ipmaquina.Substring(ipmaquina.IndexOf(":"), ipmaquina.Length - ipmaquina.IndexOf(":"));
                ipmaquina = ipmaquina.Remove(ipmaquina.IndexOf(":"), ipmaquina.Length - ipmaquina.IndexOf(":"));

                ipmaquina = econtactSoftphone.Encryption.Cryptography.Decrypt(ipmaquina);
                maquina = stringstart + ipmaquina + stringend;

                this.lamaquina = maquina;

                this.client = new RemoteClient(maquina);
                this.client.Established += new EstablishedHandler(client_Established);

            }
            catch (Exception ex)
            {
                this.MsgError = "Conectar.Exception->" + ex.Message;
            }
        }

        void client_Established(string callID)
        {
            this.ConnId = callID;
        }

        public void Discar(string fono)
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                {
                    client.SharedObject.RequestBlindTransfer(fono);   
                }
            }
            catch (Exception ex)
            {
                this.MsgError = "Discar.Exception->" + ex.Message;
            }
        }

        public void DiscarFono(string fono, string CodigoServicio, string skill)
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                {
                    client.SharedObject.RequestMakeCall("9" + fono, CodigoServicio, skill, "1", "1");
                }
            }
            catch (Exception ex)
            {
                this.MsgError = "Discar.Exception->" + ex.Message;
            }
        }

        public void Cortar()
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                {
                    client.SharedObject.RequestHangUp();
                }
            }
            catch (Exception ex)
            {
                this.MsgError = "Discar.Exception->" + ex.Message;
            }
        }

        public void Discar(string cs, string skill, string vdn)
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                {
                    //string prefijo = ConfigurationManager.AppSettings.Get("PREDIRECTO").ToString();

                    client.SharedObject.RequestBlindTransfer(vdn);
                }
            }
            catch (Exception ex)
            {
                this.MsgError = "Discar.Exception->" + ex.Message;
            }
        }

        public void IniciarTransf(string ani, string cs, string skill, string agente)
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                {
                    //string prefijo = ConfigurationManager.AppSettings.Get("PREDIRECTO").ToString();

                    client.SharedObject.RequestInitTransfer(ani, cs, skill, agente);
                }
            }
            catch (Exception ex)
            {
                this.MsgError = "IniciarTransf.Exception->" + ex.Message;
            }
        }

        public void IniciarConferencia(string ani, string cs, string skill)
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                {
                    //string prefijo = ConfigurationManager.AppSettings.Get("PREDIRECTO").ToString();

                    client.SharedObject.RequestInitConference(ani, cs, skill, "1", "1");
                }
            }
            catch (Exception ex)
            {
                this.MsgError = "IniciarTransf.Exception->" + ex.Message;
            }
        }

        public void CompletarTransferencia()
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                    client.SharedObject.RequestCompleteTransfer();
            }
            catch (Exception ex)
            {
                this.MsgError = "CompletarTransferencia.Exception->" + ex.Message;
            }
        }

        public void CancelarTransferencia()
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                    client.SharedObject.RequestCancelTransfer();
            }
            catch (Exception ex)
            {
                this.MsgError = "CancelarTransferencia.Exception->" + ex.Message;
            }
        }

        public void CompletarConferencia()
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                    client.SharedObject.RequestCompleteConference();
            }
            catch (Exception ex)
            {
                this.MsgError = "CompletarConferencia.Exception->" + ex.Message;
            }
        }

        public void CancelarConferencia()
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                    client.SharedObject.RequestCancelConference();
            }
            catch (Exception ex)
            {
                this.MsgError = "CancelarConferencia.Exception->" + ex.Message;
            }
        }

        public void AbandonarConferencia()
        {
            try
            {
                if (client != null && client.SharedObject.Validate())
                    client.SharedObject.RequestHangUp();
            }
            catch (Exception ex)
            {
                this.MsgError = "AbandonarConferencia.Exception->" + ex.Message;
            }
        }

        public Softphone()
        {

        }
    }
}