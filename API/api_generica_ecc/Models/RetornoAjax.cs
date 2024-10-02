namespace api_generica_ecc.Models
{
    public class RetornoAjax
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string debug { get; set; }
        public List<object> values { get; set; }
    }
}
