namespace api_generica_ecc.Models
{
    public class RequestAPI
    {
        public string APLICACION { get; set; }
        public string? PACKAGE { get; set; }
        public string? PROCEDURE { get; set; }
        public List<ParametroAPI>? PARAMETROS { get; set; }
    }
}
