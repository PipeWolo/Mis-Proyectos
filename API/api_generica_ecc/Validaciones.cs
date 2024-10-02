using System.Text.RegularExpressions;

namespace api_generica_ecc.Utilities
{
    public class Validaciones
    {

        public bool Email(string Mail)
        {
            return Regex.IsMatch(Mail, @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                     + @"([-a-zA-Z0-9\.!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                     + @"@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
        }

        public bool Numerico(string valor)
        {
            bool isNumeric = float.TryParse(valor, out float n);
            return isNumeric;
        }

        public bool checkForSQLInjection(string userInput)
        {

            bool isSQLInjection = false;
            string[] sqlCheckList = {
                "--",
                ";--",
                "; --",
                ";",
                "/*",
                "*/",
                "@@",
                "char",
                "nchar",
                "varchar",
                "nvarchar",
                "alter",
                "begin",
                "cast",
                "create",
                "cursor",
                "declare",
                "delete",
                "drop",
                "end",
                "exec",
                "execute",
                "fetch",
                "insert",
                "kill",
                "select",
                "sys",
                "sysobjects",
                "syscolumns",
                "table",
                "update"
            };

            string CheckString = userInput.Replace("'", "''");

            for (int i = 0; i <= sqlCheckList.Length - 1; i++)
            {
                if ((CheckString.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                { 
                    isSQLInjection = true;
                }
            }

            return isSQLInjection;
        }
    }
}
