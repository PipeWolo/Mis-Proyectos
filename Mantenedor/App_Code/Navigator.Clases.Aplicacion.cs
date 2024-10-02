using System.Collections.Generic;

namespace Navigator.Clases
{
    public class KVP
    {
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public string KeyValue2 { get; set; }
        public string KeyValue3 { get; set; }
    }
    public class FiltroUsuario
    {
        public string Usuario { get; set; }
        public string IdPerfil { get; set; }
        public string Cantidad { get; set; }
        public string Pagina { get; set; }
    }
    public class FiltroLogUsuario
    {
        public string ID_USUARIO { get; set; }
        public string Cantidad { get; set; }
        public string Pagina { get; set; }
    }

    public class LogUsuario
    {
        public string ID_LOG { get; set; }
        public string ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string ID_TIPO { get; set; }
        public string TIPO { get; set; }
        public string FECHA { get; set; }
    }

    
    public class ResultDatatable
    {
        public List<object> data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
    }
    public class FiltroCampana
    {
        public string Campana { get; set; }
        public string Cantidad { get; set; }
        public string Pagina { get; set; }
    }

    public class Campana
    {
        public string ID_CAMPANA { get; set; }
        public string CODIGO_SERVICIO { get; set; }
        public string NOMBRE_CAMPANA { get; set; }
        public string SKILL_1 { get; set; }
        public string SKILL_2 { get; set; }
        public string MODO_DISCADO { get; set; }
        public string PREFIJO { get; set; }
    }
    public class FiltroSegmentoxAgente
    {
        public string Agente { get; set; }
        public string CodigoServicio { get; set; }
        public string Segmento { get; set; }
        public string Cantidad { get; set; }
        public string Pagina { get; set; }
    }

    public class FiltroCarga
    {
        public string CodigoServicio { get; set; }
        public string Segmento { get; set; }
    }

    public class FiltroReporteria
    {
        public string CodigoServicio { get; set; }
        public string Segmento { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta{ get; set; }
        public string ResultadoLlamada { get; set; }
        public string MotivoLlamada { get; set; }
        public string ResultadoCampana { get; set; }
        public string MotivoCampana { get; set; }
        public string Cantidad { get; set; }
        public string Pagina { get; set; }
    }

    public class  SegmentoxAgente
    {
        public string Id { get; set; }
        public string Agente { get; set; }
        public string NombreAgente { get; set; }
        public string CodigoServicio { get; set; }
        public string Segmento { get; set; }
        public string NombreCampana { get; set; }
    }

    public class ReporteCargas
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string QRecibidos { get; set; }
        public string QValidos { get; set; }
        public string QErroneos { get; set; }
        public string QRepetidos { get; set; }
        public string QGestionManual { get; set; }
    }

    public class Tipificaciones
    {
        public string ResultadoLlamada { get; set; }
        public string MotivoLlamada { get; set; }
        public string ResultadoCampana { get; set; }
        public string MotivoCampana { get; set; }
        public string Reprogramacion { get; set; }
    }
    public class ReporteCargasTablas
    {
        public string TELEFONO_MOVIL { get; set; }
        public string TELEFONO_FIJO { get; set; }
        public string TELEFONO_DEL_TRABAJO { get; set; }
        public string TELEFONO_MOVIL_TRABAJO { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public string CORREO_ELECTRONICO_2 { get; set; }
        public string CODIGO_CAMPANA { get; set; }
        public string TIPO_DE_CAMPANA { get; set; }
        public string NOMBRE_CAMPANA { get; set; }
        public string CODIGO_SOCIO { get; set; }
        public string RUT { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string SEGMENTO { get; set; }
        public string COMUNA { get; set; }
        public string ACUERDO { get; set; }
        public string MONTO_APORTE { get; set; }
        public string DIVISA { get; set; }
        public string FUNDACION { get; set; }
        public string ESTADO_DEL_ACUERDO { get; set; }
        public string MECANISMO_RECAUDACION { get; set; }
        public string TIPO_MEDIO_DE_PAGO { get; set; }
        public string MEDIO_DE_PAGO { get; set; }
        public string OFICINA_DE_VENTA { get; set; }
        public string SEDE { get; set; }
        public string FECHA_DE_CREACION { get; set; }
        public string URL_CALL { get; set; }
        public string MONTO_BASE { get; set; }
        public string SEGMENTO_APORTE { get; set; }
        public string MONTO_PROPUESTO { get; set; }
        public string GENERICO1 { get; set; }
        public string GENERICO2 { get; set; }
        public string GENERICO3 { get; set; }
        public string GENERICO4 { get; set; }
    }

    public class DatosReporteria
    {
        public string NUMERO_LLAMADA { get; set; }
        public string FECHA_INGRESO { get; set; }
        public string CLIENTE { get; set; }
        public string CAMPANA { get; set; }
        public string CODIGO_SERVICIO { get; set; }
        public string CONNID { get; set; }
        public string SKILL { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string DV { get; set; }
        public string RUT_CLIENTE_DV { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string COMUNA { get; set; }
        public string TELEFONO_MOVIL { get; set; }
        public string TELEFONO_FIJO { get; set; }
        public string TELEFONO_DEL_TRABAJO { get; set; }
        public string TELEFONO_MOVIL_TRABAJO { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public string CORREO_ELECTRONICO_2 { get; set; }
        public string CODIGO_CAMPANA { get; set; }
        public string TIPO_DE_CAMPANA { get; set; }
        public string NOMBRE_CAMPANA { get; set; }
        public string CODIGO_SOCIO { get; set; }
        public string SEGMENTO { get; set; }
        public string MONTO_APORTE { get; set; }
        public string DIVISA { get; set; }
        public string FUNDACION { get; set; }
        public string ACUERDO { get; set; }
        public string ESTADO_DEL_ACUERDO { get; set; }
        public string MECANISMO_RECAUDACION { get; set; }
        public string TIPO_MEDIO_DE_PAGO { get; set; }
        public string MEDIO_DE_PAGO { get; set; }
        public string OFICINA_DE_VENTA { get; set; }
        public string SEDE { get; set; }
        public string MONTO_BASE { get; set; }
        public string SEGMENTO_APORTE { get; set; }
        public string FECHA_DE_CREACION { get; set; }
        public string MONTO_PROPUESTO { get; set; }
        public string URL_CALL { get; set; }
        public string OBSERVACIONES { get; set; }
        public string GENERICO1 { get; set; }
        public string GENERICO2 { get; set; }
        public string GENERICO3 { get; set; }
        public string GENERICO4 { get; set; }
        public string RESULTADO_LLAMADO { get; set; }
        public string MOTIVO_LLAMADO { get; set; }
        public string RESULTADO_CAMPANA { get; set; }
        public string MOTIVO_CAMPANA { get; set; }
        public string SEGMENTO_AGENTE { get; set; }
        public string FECHAHORA_APERTURAFORM { get; set; }
        public string FECHAHORA_GRABAFORM { get; set; }
        public string TIEMPO_HABLADO { get; set; }
        public string COD_TIPIFICACION1 { get; set; }
        public string COD_TIPIFICACION2 { get; set; }
        public string COD_TIPIFICACION3 { get; set; }
        public string AGENTE { get; set; }
        public string NOMBRE_AGENTE { get; set; }
        public string FECHA_ULTIMA_LLAMADA { get; set; }
        public string INTENTOS { get; set; }
        public string FECHA_AGENDAMIENTO { get; set; }
        public string FONO_CONTACTO { get; set; }
    }

    public class Gestion
    {
        public string ID { get; set; }
        public string ID_BASE_ENTRADA { get; set; }
        public string ID_ESTADO { get; set; }
        public string ID_ASIGNADO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string RUT { get; set; }
        public string CORREO { get; set; }
        public string DIRECCION { get; set; }
        public string REGION { get; set; }
        public string COMUNA { get; set; }
        public string TIPIFICACION_1 { get; set; }
        public string TIPIFICACION_2 { get; set; }
        public string TIPIFICACION_3 { get; set; }
        public string TIPIFICACION_4 { get; set; }
        public string FECHA_REAGENDA { get; set; }
    }
    public class Historial
    {
        public string ID { get; set; }
        public string FECHA { get; set; }
        public string TIPIFICACION_1 { get; set; }
        public string TIPIFICACION_2 { get; set; }
        public string TIPIFICACION_3 { get; set; }
        public string TIPIFICACION_4 { get; set; }
        public string ESTADO { get; set; }
        public string FECHA_REAGENDA { get; set; }
    }

    public class FiltroBandeja
    {
        public string CONNID { get; set; }
        public string SKILL { get; set; }
        public string FECHA { get; set; }
        public string ANI { get; set; }
        public string Cantidad { get; set; }
        public string Pagina { get; set; }
    }

    public class Bandeja
    {
        public string CONNID { get; set; }
        public string ASTERISKID { get; set; }
        public string SKILL { get; set; }
        public string FECHA { get; set; }
        public string AGENTE { get; set; }
        public string RESPUESTA1 { get; set; }
        public string RESPUESTA2 { get; set; }
        public string RESPUESTA3 { get; set; }
        public string MARCA_IVR { get; set; }
        public string AUDIO { get; set; }
        public string ANI { get; set; }
    }

    public class SMS
    {
        public string ID { get; set; }
        public string ANI { get; set; }
        public string NOMBRE { get; set; }
        public string URL { get; set; }
    }
    public class AccesoToken
    {
        public string token { get; set; }
        public string expiracion { get; set; }
    }

    public class FiltroTipificacion
    {
        public string NIVEL_1 { get; set; }
        public string NIVEL_2 { get; set; }
        public string NIVEL_3 { get; set; }
        public string NIVEL_4 { get; set; }
        public string REAGENDA { get; set; }
        public string ID_SERVICIO { get; set; }
        public string Cantidad { get; set; }
        public string Pagina { get; set; }
    }

    public class Tipificacion
    {
        public string ID_TIPIFICACION { get; set; }
        public string NIVEL_1 { get; set; }
        public string NIVEL_2 { get; set; }
        public string NIVEL_3 { get; set; }
        public string NIVEL_4 { get; set; }
        public string ID_CLIENTE { get; set; }
        public string CLIENTE { get; set; }
        public string ID_SERVICIO { get; set; }
        public string SERVICIO { get; set; }
        public string REAGENDA { get; set; }
        public string ACTIVO { get; set; }
        public string CODIGO_SERVICIO { get; set; }
    }

    public class ConfiguracionTipificacion
    {
        public string NIVEL_1 { get; set; }
        public string NIVEL_2 { get; set; }
        public string NIVEL_3 { get; set; }
        public string NIVEL_4 { get; set; }
    }

    public class ResumenValidacion
    {
        public string ID_CARGA { get; set; }
        public string OK_UNICO { get; set; }
        public string UNICO { get; set; }
        public string NUMERO_LINEA { get; set; }
    }

}