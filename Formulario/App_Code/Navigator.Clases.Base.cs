using System.Collections.Generic;

namespace Navigator.Clases
{
    /// <summary>
    /// Aplicacion
    /// </summary>
    public class Aplicacion
    {
        //CONFIGURADOR GENERAL
        public string IdAplicacion { get; set; }
        public string Instancia { get; set; }
        public string Package { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiChexpressURL { get; set; }
        public string ApiChexpressAuth { get; set; }
        public string ApiChexpressSistema { get; set; }
        public string idServicio { get; set; }
    }

    public class RetornoAjax
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string debug { get; set; }
        public List<object> values { get; set; }
    }

    public class LoggedUser
    {
        public string Id_Usuario { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string ID_Perfil { get; set; }
        public string Nombre_Perfil { get; set; }
        public string Correo { get; set; }
    }

    public class Usuario
    {
        public string ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string CODIGO_SERVICIO { get; set; }
        public Usuario()
        {

        }
    }

    public class MapaAcceso
    {
        public string SECCION { get; set; }
        public string ID_MODULO { get; set; }
        public string OPCION { get; set; }
        public string MODULO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ID_MODULO_PADRE { get; set; }
        public string RUTA { get; set; }
        public string ICONO { get; set; }
        public string TIENE_HIJO { get; set; }
        public string ORDEN_PADRE { get; set; }
        public string ORDEN_HIJO { get; set; }
    }
}
