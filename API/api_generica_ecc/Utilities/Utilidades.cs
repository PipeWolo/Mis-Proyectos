using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace api_generica_ecc.Utilities
{
    public class Utilidades
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static DataTable ToDataTable<T>(List<T> items, string name)
        {
            DataTable dataTable = new DataTable(name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                // dataTable.Columns.Add(prop.Name, prop.PropertyType);
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static string ValidaFecha(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return dateString;
            }

            string Fecha = string.Empty;
            try
            {
                string[] formats = {
                    "yyyy-MM-dd HH:mm:ss",
                    "MM/dd/yyyy HH:mm:ss",
                    "dd/MM/yyyy HH:mm:ss",
                    "yyyy/MM/dd HH:mm:ss",
                    "dd-MM-yyyy HH:mm:ss",
                    "dd-MM-yyyy hh:mm:ss tt",
                    "MM/dd/yyyy hh:mm:ss tt",
                    "dd/MM/yyyy hh:mm:ss tt",
                    "yyyy/MM/dd hh:mm:ss tt",
                    "HH:mm:ss dd-MM-yyyy",
                    "hh:mm:ss tt MM/dd/yyyy",
                    "hh:mm:ss tt dd/MM/yyyy",
                    "hh:mm:ss tt yyyy/MM/dd",
                    "yyyy-MM-ddTHH:mm:ss",
                    "MM/dd/yyyyTHH:mm:ss",
                    "dd/MM/yyyyTHH:mm:ss",
                    "yyyy/MM/ddTHH:mm:ss",
                    "dd-MM-yyyyTHH:mm:ss",
                    "dd/M/yyyy HH:mm:ss",
                    "d/M/yyyy HH:mm:ss",
                    "d/MM/yyyy HH:mm:ss",
                    "dd/MM/yyyy HH:mm:ss",
                    "dd/MM/yy HH:mm:ss",
                    "dd/M/yy HH:mm:ss",
                    "d/MM/yy HH:mm:ss",
                    "d/M/yy HH:mm:ss",
                    "dd/MM/yy HH:mm:ss",
                    "dd/MM/yyyy HH:mm:ss",
                    "MM/dd/yy HH:mm:ss",
                    "MM/dd/yyyy HH:mm:ss",
                    "yyyy-MM-ddTHH:mm:ss",
                    "dd/MM/yy HH:mm",
                    "MM/dd/yy HH:mm",
                    "dd/MM/yy H:mm",
                    "d/MM/yy HH:mm",
                    "d/MM/yy H:mm"
                };

                DateTime result;
                if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out result))
                {
                    Fecha = result.ToString("yyyy-MM-dd HH:mm:ss");
                    if (string.IsNullOrEmpty(Fecha))
                    {
                        Fecha = "NOCONVERTIDA:" + dateString;
                    }
                }
                else
                {
                    Fecha = "NOCONVERTIDA:" + dateString;
                }
            }
            catch
            {
                Fecha = "NOCONVERTIDA: " + dateString;
            }
            return Fecha;
        }

        public static DateTime? GetFecha(string dateString)
        {
            CultureInfo culturaChile = new CultureInfo("es-CL");
            CultureInfo culturaUSA = new CultureInfo("en-US");

            DateTime result = new();

            //  Espacio: " "
            //  Tabulación: "\t"
            //  Salto de línea: "\n"(para Windows: "\r\n")
            //  tabulación vertical "\v"
            //  Avance de página: "\f"
            //  Salto de línea de carro: "\r"
            //  secuencia de escape "\0" representa un carácter nulo.
            //  la T que se usa en sistemas como SQL SERVER

            var fechaVerificacion = dateString;

            if (String.IsNullOrEmpty(dateString))
            {
                return null;
            }

            dateString = dateString.Replace("T", " ").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ").Replace("\v", " ").Replace("\f", " ");
            dateString = Regex.Replace(dateString, @"\s+", " ");

            try
            {
                string[] formats = {
                    "yyyy-MM-dd HH:mm:ss",
                    "MM/dd/yyyy HH:mm:ss",
                    "dd/MM/yyyy HH:mm:ss",
                    "d/MM/yyyy HH:mm:ss",
                    "dd/M/yyyy HH:mm:ss",
                    "dd/MM/yy HH:mm:ss",
                    "d/M/yyyy HH:mm:ss",
                    "d/M/yy HH:mm:ss",
                    "dd/MM/yyyy HH:mm",
                    "d/MM/yyyy HH:mm",
                    "d/M/yyyy HH:mm",
                    "d/M/yy HH:mm",
                    "d/MM/yy HH:mm",
                    "dd/M/yyyy HH:mm",
                    "dd/M/yy HH:mm",
                    "yyyy/MM/dd HH:mm:ss",
                    "dd-MM-yyyy HH:mm:ss",
                    "dd-MM-yyyy hh:mm:ss tt",
                    "MM/dd/yyyy hh:mm:ss tt",
                    "dd/MM/yyyy hh:mm:ss tt",
                    "yyyy/MM/dd hh:mm:ss tt",
                    "HH:mm:ss dd-MM-yyyy",
                    "hh:mm:ss tt MM/dd/yyyy",
                    "hh:mm:ss tt dd/MM/yyyy",
                    "hh:mm:ss tt yyyy/MM/dd",
                    "MM/dd/yyyy h:mm:ss tt",
                    "MM/d/yyyy h:mm:ss tt",
                    "MM/d/yy h:mm:ss tt",
                    "MM/dd/yy h:mm:ss tt",
                    "M/dd/yyyy h:mm:ss tt",
                    "M/d/yyyy h:mm:ss tt",
                    "M/d/yy h:mm:ss tt",
                    "dd-MM-yy H:mm",
                    "dd/MM/yyyy h:mm:ss tt",
                    "MM/dd/yyyy h:mm:ss tt",
                    "M-dd-yy H:mm",
                    "dd-M-yy H:mm"
                };

                if (DateTime.TryParseExact(dateString, formats, culturaChile, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    if (DateTime.TryParseExact(dateString, formats, culturaUSA, DateTimeStyles.None, out result))
                    {
                        return result;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static decimal ConvertStringToDecimal(string numberAsString)
        {
            if (decimal.TryParse(numberAsString, out decimal number))
            {
                return decimal.Round(number, 2);
            }
            else
            {
                return 0;
            }
        }
    }
}
