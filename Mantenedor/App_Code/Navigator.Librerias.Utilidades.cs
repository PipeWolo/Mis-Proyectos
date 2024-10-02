using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Clase Utilidades
/// </summary>
namespace Navigator.Librerias
{
    public static class Utilidades
    {
        public static string SeteaTexto(string Valor)
        {
            string ret = String.Empty;

            if (Valor.Equals("&nbsp;") || String.IsNullOrEmpty(Valor))
            {
                return String.Empty;
            }
            else
            {
                return HttpUtility.HtmlDecode(Valor);
            }

        }

        public static void BuscaElementoCombo(DropDownList cbo, string Valor)
        {

            cbo.SelectedIndex = 0;

            for (int k = 0; k <= cbo.Items.Count - 1; k++)
            {
                if (cbo.Items[k].Text.Equals(Valor))
                {
                    cbo.SelectedIndex = k;
                    break;
                }
            }

        }

        public static void BuscaValueCombo(DropDownList cbo, string Valor)
        {

            cbo.SelectedIndex = 0;

            for (int k = 0; k <= cbo.Items.Count - 1; k++)
            {
                if (cbo.Items[k].Value.Equals(Valor))
                {
                    cbo.SelectedIndex = k;
                    break;
                }
            }

        }

        public static string CerrarVentana()
        {

            StringBuilder ret = new StringBuilder();

            ret.Append("<script language=javascript> ");
            ret.Append("window.opener = null;");
            ret.Append("window.close();");
            ret.Append(" </script>");

            return ret.ToString();

        }

        public static string PreparaValor(string Valor)
        {

            string ret = String.Empty;

            if (!String.IsNullOrEmpty(Valor))
            {
                if (Valor.IndexOf(Convert.ToChar(39), 1) > 0)
                {
                    ret = Valor.Replace(Convert.ToChar(39).ToString(), "");
                }
                else
                {
                    ret = Valor;
                }
            }
            else
            {
                ret = Valor;
            }

            return ret;

        }

        public static string FormateaHora(string Horario)
        {
            string hora = String.Empty;
            string minuto = String.Empty;

            hora = Horario.Substring(0, Horario.IndexOf(":"));
            minuto = Horario.Substring(Horario.IndexOf(":") + 1, 2);

            if (Horario.ToLower().EndsWith("pm"))
            {
                if (hora != "12")
                    hora = Convert.ToString(Convert.ToInt32(hora) + 12);
            }
            else if (hora == "12")
            {
                hora = "0";
            }

            return hora + ":" + minuto;

        }

        public static string FormateaMinutos(string valor)
        {

            string ret = String.Empty;

            if (Convert.ToInt32(valor) < 10)
            {
                ret = "0" + valor;
            }
            else
            {
                ret = valor;
            }

            return ret;

        }

        public static void SeteaLargosTextArea(TextBox txt, string form, string nombre, string largo)
        {
            txt.Attributes.Add("onkeyup", "chars_restantes(document." + form + "." + nombre + "," + largo + ")");
        }

        public static DayOfWeek WeekDay(string fecha)
        {

            DateTime dt = Convert.ToDateTime(fecha);

            return dt.DayOfWeek;

        }

        public static string MyNull(object var)
        {

            if (var == null)
            {
                return String.Empty;
            }
            else
            {
                return Convert.ToString(var);
            }
        }

        public static string MyFechaNull(object var)
        {

            if (var == null)
            {
                return String.Empty;
            }
            else
            {
                return String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(var.ToString()));
            }
        }

        public static string GeneraPassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public static string EncriptaPassword(string password, int bytes)
        {
            byte[] saltBytes = new byte[bytes];

            return Encripta.ComputeHash(password, "MD5", saltBytes);
        }

        public static string Mensaje(string path, string tipo, string msg)
        {

            string ret = String.Empty;

            if (tipo.Equals("E") || tipo.Equals("L"))
            {
                ret = @"<img src='" + path + "images/exclamation.png' alt='error' title='error' />";
            }
            else if (tipo.Equals("W"))
            {
                ret = @"<img src='" + path + "images/bullet_error.png' alt='warning' title='warning' />";
            }
            else if (tipo.Equals("O"))
            {
                ret = @"<img src='" + path + "images/information.png' alt='ok' title='ok' />";
            }

            ret += "<strong>Mensaje</strong> " + msg;

            return ret;
        }

        public static string ReemplazaAcentos(string valor)
        {

            string ret = String.Empty;

            ret = valor.ToUpper().Trim();

            ret = ret.Replace("À", "A");
            ret = ret.Replace("Á", "A");
            ret = ret.Replace("È", "E");
            ret = ret.Replace("É", "E");
            ret = ret.Replace("Ì", "I");
            ret = ret.Replace("Í", "I");
            ret = ret.Replace("Ò", "O");
            ret = ret.Replace("Ó", "O");
            ret = ret.Replace("Ù", "U");
            ret = ret.Replace("Ú", "U");
            ret = ret.Replace(" ", "_");

            return ret;
        }

        public static string ObtieneFechaHora()
        {

            string ret = String.Empty;

            string fecha = String.Format("{0:yyyymmdd_HHmmss}", DateTime.Now.ToString());

            ret = fecha.Replace("/", "_");
            ret = ret.Replace("-", "_");
            ret = ret.Replace(" ", "_");
            ret = ret.Replace(":", "_");

            return ret;
        }

        public static int DiasHabiles(string desde, string hasta)
        {
            int ret = 0;

            DateTime fecha_desde = Convert.ToDateTime(desde);
            DateTime fecha_hasta = Convert.ToDateTime(hasta);
            DateTimeExtension datedif = new DateTimeExtension();
            long dif = datedif.DateDiff(DateInterval.Day, fecha_desde, fecha_hasta) + 1;

            string fecha = desde;

            for (long c = 1; c <= dif; c++)
            {

                DateTime dfecha = Convert.ToDateTime(fecha);

                int dia = (int)dfecha.DayOfWeek;

                if (dia > 0 && dia < 6)
                    ret++;

                fecha = String.Format("{0:dd/MM/yyyy}", dfecha.AddDays(1)).Replace("-", "/");

            }

            return ret;
        }

        public static string ScriptCalendar()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append("<script type='text/javascript'>" + Environment.NewLine);
            ret.Append("$(function () {" + Environment.NewLine);
            ret.Append("    $('#txtDesde').datepicker({" + Environment.NewLine);
            ret.Append("        showOn: 'both'," + Environment.NewLine);
            ret.Append("        buttonImage: '../images/calendar.png'," + Environment.NewLine);
            ret.Append("        buttonImageOnly: true," + Environment.NewLine);
            ret.Append("        changeYear: true," + Environment.NewLine);
            ret.Append("        changeMonth: true" + Environment.NewLine);
            ret.Append("    });" + Environment.NewLine);
            ret.Append(Environment.NewLine);
            ret.Append("    $('#txtHasta').datepicker({" + Environment.NewLine);
            ret.Append("        showOn: 'both'," + Environment.NewLine);
            ret.Append("        buttonImage: '../images/calendar.png'," + Environment.NewLine);
            ret.Append("        buttonImageOnly: true," + Environment.NewLine);
            ret.Append("        changeYear: true," + Environment.NewLine);
            ret.Append("        changeMonth: true" + Environment.NewLine);
            ret.Append("    });" + Environment.NewLine);
            ret.Append("});" + Environment.NewLine);
            ret.Append("</script>" + Environment.NewLine);

            return ret.ToString();
        }
    }
}