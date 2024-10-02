using System.Collections.Generic;

namespace Navigator.Clases
{
    /// <summary>
    /// Aplicacion
    /// </summary>
    public class Aplicacion
    {

        public string IdAplicacion { get; set; }
        public string Instancia { get; set; }
        public string Package { get; set; }
        public string PackageCuenta { get; set; }
        public string SaltString { get; set; }
        public string IdLogin { get; set; }

        public Aplicacion()
        {

        }
    }

    /// <summary>
    /// Usuario
    /// </summary>
    public class Usuario
    {
        public string ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string ID_PERFIL { get; set; }
        public string PERFIL { get; set; }
        public string PASSWORD { get; set; }
        public string CORREO { get; set; }
        public string CAMBIO_CONTRASENA { get; set; }
        public string FECHA_CREACION { get; set; }
        public string SERVICIO { get; set; }

        public Usuario()
        {

        }
    }

    public class RetornoAjax
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string debug { get; set; }
        public List<object> values { get; set; }
    }

    public class Retorno
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string Numero { get; set; }
        public List<object> values { get; set; }
    }

    public class SaveAjax
    {
        public string ret { get; set; }
        public string debug { get; set; }
        public string msg { get; set; }
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
