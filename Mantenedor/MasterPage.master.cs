using Navigator.Clases;
using Navigator.GestionCuenta;
using Navigator.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{

    public List<MapaAcceso> mapa { get; set; }
    public string menuHTML { get; set; }
    public Usuario loggedUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        loggedUser = (Usuario)Session["LoggedUser"];

        ControlAcceso ajax = new ControlAcceso();

        if (loggedUser != null)
        {
            string menuJSON = (string)Session["menuJSON"];
            if (menuJSON != null)
            {
                mapa = new JavaScriptSerializer().Deserialize<List<MapaAcceso>>(menuJSON);
            }

            menuHTML = (string)Session["menuHTML"];
            this.txtConfiguracionCuentaNombre.Value = string.IsNullOrEmpty(loggedUser.USUARIO) ? "Sin información" : loggedUser.USUARIO;
            this.txtConfiguracionCuentaCorreo.Value = string.IsNullOrEmpty(loggedUser.CORREO) ? "Sin información" : loggedUser.CORREO;
        }
    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        string url = "~/Login.aspx";

        ControlAcceso ajax = new ControlAcceso();
        ajax.GrabarCierre(loggedUser.ID_USUARIO);
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();

        Response.Redirect(url);
    }

    protected void btnConfiguracionCuentaCambiar_Click(object sender, EventArgs e)
    {
        try
        {
            var contrasenaActual = txtConfiguracionCuentaContrasenaActual.Value;
            var contrasenaNueva = txtConfiguracionCuentaContrasenaNueva.Value;
            var contrasenaNuevaConfirm = txtConfiguracionCuentaContrasenaNuevaConfirma.Value;

            if (contrasenaActual.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Debe ingresar su contraseña actual para continuar.','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                return;
            }

            if (contrasenaNueva.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Debe ingresar una nueva contraseña para continuar.','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                return;
            }
            else
            {
                if (contrasenaNueva.Length < 14)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su nueva contraseña debe ser de al menos 14 caracteres.','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                    return;
                }
                else
                {
                    if (contrasenaNueva.Length > 25)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su nueva contraseña debe tener máximo 25 caracteres.','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                        return;
                    }
                    else
                    {
                        bool tieneMayusculas = contrasenaNueva.Any(c => char.IsUpper(c));
                        if (!tieneMayusculas)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su nueva contraseña debe tener al menos una mayuscula.','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                            return;
                        }
                        else
                        {
                            bool tieneNumeros = contrasenaNueva.Any(c => char.IsDigit(c));
                            if (!tieneNumeros)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su nueva contraseña debe tener al menos un número.','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                                return;
                            }
                            else
                            {
                                bool tieneCaracterEspecial = contrasenaNueva.Any(c => c == '.' || c == ',' || c == '-' || c == '_' || c == '@' || c == '#' || c == '$');
                                if (!tieneCaracterEspecial)
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su nueva contraseña debe tener al menos un carácter especial(. , - _ @ # $).','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                                    return;
                                }
                                else
                                {
                                    if (contrasenaActual == contrasenaNueva)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su nueva contraseña no puede ser igual a la actual.','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (contrasenaNuevaConfirm.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Debe confirmar su contraseña.','info','#txtConfiguracionCuentaContrasenaNuevaConfirma');", true);
                return;
            }
            else
            {
                if (!contrasenaNuevaConfirm.Equals(contrasenaNueva))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su nueva contraseña y la confirmacion no son identicas.','info','#txtConfiguracionCuentaContrasenaNuevaConfirma');", true);
                    return;
                }
            }

            Usuario info = (Usuario)Session["LoggedUser"];
            if (info != null)
            {
                string id_usuario = info.ID_USUARIO;
                var ajax = new NavigatorGestionCuenta();
                var data = ajax.CambiarContrasenaCuenta(id_usuario, contrasenaActual, contrasenaNueva);
                if (data.ret == "OK")
                {
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.Abandon();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Exito','Su contraseña ha sido cambiada correctamente.','success','',true);", true);
                    return;
                }
                else
                {
                    if (data.debug.Contains("NO_PASS"))
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su contraseña actual esta incorrecta.','info','#txtConfiguracionCuentaContrasenaActual');", true);
                    }
                    else
                    {
                        if (data.debug.Contains("EQUAL_OLD"))
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Validación','Su nueva contraseña no puede ser igual a una de las ultimas 3 utilizadas.','info','#txtConfiguracionCuentaContrasenaNueva');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Error al cambiar su contraseña','Ocurrio un error inesperado, por favor intente nuevamente mas tarde.','error','');", true);
                        }
                    }
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Error inesperado','Por favor recargue la pagina o intente nuevamente mas tarde.','error','',true);", true);
                return;
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Popup", "ctrl_cuenta.Mensaje('Error inesperado','Ocurrio un error inesperado. Por favor, intente nuevamente mas tarde.','error','');", true);
            return;
        }

    }
}
