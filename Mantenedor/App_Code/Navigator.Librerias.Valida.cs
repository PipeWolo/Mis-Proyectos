using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Navigator.Librerias
{
    public static class Valida
    {
        public static bool Usuario(string usuario)
        {
            var validchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (int k = 0; k <= usuario.Length - 1; k++)
            {
                string caracter = usuario.Substring(k, 1);
                bool valid = false;
                for (int i = 0; i < validchars.Length; i++)
                {
                    if (caracter.Equals(validchars.Substring(i, 1)))
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                    return false;
            }

            return true;
        }

        public static bool Password(string password)
        {
            var validchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,-_";

            for (int k = 0; k <= password.Length - 1; k++)
            {
                string caracter = password.Substring(k, 1);
                bool valid = false;
                for (int i = 0; i < validchars.Length; i++)
                {
                    if (caracter.Equals(validchars.Substring(i, 1)))
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                    return false;
            }

            return true;
        }

        public static bool Numeros(string valor)
        {

            var validos = "0123456789";

            for (int k = 0; k <= valor.Length - 1; k++)
            {
                string caracter = valor.Substring(k, 1);
                bool valid = false;
                for (int i = 0; i < validos.Length; i++)
                {
                    if (caracter.Equals(validos.Substring(i, 1)))
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                    return false;
            }

            return true;
        }

        public static bool Dv(string valor)
        {

            var validos = "0123456789Kk";

            for (int k = 0; k <= valor.Length - 1; k++)
            {
                string caracter = valor.Substring(k, 1);
                bool valid = false;
                for (int i = 0; i < validos.Length; i++)
                {
                    if (caracter.Equals(validos.Substring(i, 1)))
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                    return false;
            }

            return true;
        }

        public static bool Email(string Mail)
        {
            return Regex.IsMatch(Mail, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$");
        }

        public static string ArchivoAdjunto(HttpPostedFile adjunto, string maxfilesize)
        {
            string ret = String.Empty;

            if (adjunto.ContentLength == 0)
            {
                return "El tamaño del archivo adjunto no es válido o el archivo adjunto no es válido.";
            }

            if (adjunto.ContentLength > Convert.ToInt64(maxfilesize))
            {
                return "El tamaño del archivo excede el tamaño máximo permitido.";
            }

            return "OK";
        }
    }
}
