namespace api_generica_ecc.Models
{
    public class RetornoSFTP
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string cantidad { get; set; }
        public List<string> archivos { get; set; }
    }
}
