using System;
using System.Collections.Generic;

namespace Navigator.Clases
{
    //Esta clase siempre va y es la clase comodin * ideal para combos, o datos de 2 campos ...
    public class KVP
    {
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public string KeyValue2 { get; set; }
        public string KeyValue3 { get; set; }
        public string KeyValue4 { get; set; }
        public string KeyValue5 { get; set; }
        public string KeyValue6 { get; set; }
        public string KeyValue7 { get; set; }
        public string KeyValue8 { get; set; }

        public KVP(string keyvalue, string keyvalue2, string keyname)
        {
            this.KeyValue = keyvalue;
            this.KeyValue2 = keyvalue2;
            this.KeyName = keyname;
        }

        public KVP(string keyvalue, string keyname)
        {
            this.KeyValue = keyvalue;
            this.KeyName = keyname;
        }

        public KVP()
        {

        }
    }

    public class ComboBox
    {
        public string id { get; set; }
        public string desc { get; set; }
    }

    public class ComboItem
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class ResultDatatable
    {
        public List<object> data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
    }

    public class DataTableColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public string searchable { get; set; }
        public string orderable { get; set; }
        public string searchValue { get; set; }
    }

    public class FiltroGeneral
    {
        public string campo1 { get; set; }
        public string campo2 { get; set; }
        public string campo3 { get; set; }
        public string campo4 { get; set; }
        public string campo5 { get; set; }
        public string campo6 { get; set; }
        public string campo7 { get; set; }
        public string campo8 { get; set; }
        public string campo9 { get; set; }
        public string campo10 { get; set; }
        public string campo11 { get; set; }
        public string campo12 { get; set; }
        public string campo13 { get; set; }
        public string campo14 { get; set; }
        public string campo15 { get; set; }
        public string campo16 { get; set; }
        public string campo17 { get; set; }
        public string campo18 { get; set; }
        public string campo19 { get; set; }
        public string campo20 { get; set; }
        public string start { get; set; }
        public string length { get; set; }
        public string column { get; set; }
        public string direction { get; set; }
    }

    public class Campana
    {
        //Cabecera
        public string CodigoServicio { get; set; }
        public string NombreCampana { get; set; }
        public string Skill1 { get; set; }
        public string Skill2 { get; set; }
        public string ModoDiscado { get; set; }
        public string Prefijo { get; set; }
    }

    public class GenesysInfo
    {
        public string Identificador { get; set; }
        public string SegundosGenesys { get; set; }
        public string CodigoServivio { get; set; }
        public string Error { get; set; }
    }

    public class RegistrosGenesys
    {
        public string RecordID { get; set; }
        public string ListaGenesys { get; set; }
    }

    public class Llamada
    {
        //Cabecera
        public string NumeroLlamada { get; set; }
        public string Agente { get; set; }
        public string NombreCampana { get; set; }
        public string RutAgente { get; set; }
        public string NombreLista { get; set; }
        public string ConnIdDec { get; set; }
        public string ConnIdHex { get; set; }
        public string CodigoServicio { get; set; }
        public string Skill { get; set; }
        public string Numero_Cliente { get; set; }
        public string IdentificadorCliente { get; set; }
        public string KeySP { get; set; }
        public string RUT { get; set; }

        //Datos Cliente
        public string RutCliente { get; set; }
        public string DV { get; set; }
        public string NombreCliente { get; set; }
        public string Comuna { get; set; }
        public string TelefonoMovil { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoTrabajo { get; set; }
        public string TelefonoMovilTrabajo { get; set; }
        public string CorreoElectrónico1  { get; set; }
        public string CorreoElectrónico2 { get; set; }
        public string CodigoSocio { get; set; }
        public string MontoAporte { get; set; }
        public string Divisa { get; set; }
        public string Fundacion { get; set; }
        public string TipoMedioPago { get; set; }
        public string MedioPago { get; set; }
        public string OficinaVenta { get; set; }
        public string Sede { get; set; }
        public string MontoBase { get; set; }
        public string MontoPropuesto { get; set; }
        public string URLCall { get; set; }
        public string Observaciones { get; set; }
        public string Acuerdo { get; set; }

        //Tipificaciones
        public string ResultadoLlamado { get; set; }
        public string MotivoLlamado { get; set; }
        public string ResultadoCampana { get; set; }
        public string MotivoCampana { get; set; }
        public string FonoContacto { get; set; }
        public string FechaReprogramacion { get; set; }

        public string ID_CS { get; set; }
        public string RecordID { get; set; }
    }

    public class Agentes
    {
        public string Agente { get; set; }
        public string NombreAgente { get; set; }
    }

    public class Tipificaciones
    {
        public string ResultadoLlamada { get; set; }
        public string MotivoLlamada { get; set; }
        public string ResultadoCampana { get; set; }
        public string MotivoCampana { get; set; }
        public string Reprogramacion { get; set; }
    }

    public class RequestAPI
    {
        public int tipoIdTransaccional { get; set; }
        public string idTransaccional { get; set; }
        public string rutCliente { get; set; }
        public string dvCliente { get; set; }
        public string idCategoria { get; set; }
        public string idSubcategoria { get; set; }
        public string idSubcategoria2 { get; set; }
        public string origen { get; set; }
        public string canal { get; set; }
        public bool cerrarCaso { get; set; }
        public string motivoCierre { get; set; }
        public string claseSistemaExterno { get; set; }
        public string codigoSistemaExterno { get; set; }
        public string glosaSistemaExterno { get; set; }
        public string MAD { get; set; }
        public string MAR { get; set; }
        public List<ListCamposIniciales> listCamposIniciales { get; set; }
        public int tipoDocumentoContacto { get; set; }
        public string rutContacto { get; set; }
        public string dvContacto { get; set; }
        public string nombreContacto { get; set; }
        public string apellidoPaternoContacto { get; set; }
        public string apellidoMaternoContacto { get; set; }
        public string telefonoContactoMovil { get; set; }
        public string telefonoContactoFijo { get; set; }
        public string telefonoContactoInter { get; set; }
        public string correoContacto { get; set; }
    }

    public class ListCamposIniciales
    {
        public string nombreEsquema { get; set; }
        public string tipoCampoTexto { get; set; }
        public string tipoCampoValor { get; set; }
        public string valor { get; set; }
        public string entidad { get; set; }
    }

    public class ResponseAPI
    {
        public CasoModel casoModel { get; set; }
        public object[] listArticuloModel { get; set; }
        public ModelRespuesta modelRespuesta { get; set; }
    }

    public class CasoModel
    {
        public string idCaso { get; set; }
        public string numeroCaso { get; set; }
        public string statecode { get; set; }
        public string statuscode { get; set; }
        public string state { get; set; }
        public string status { get; set; }
        public string titulo { get; set; }
        public string idCategoria { get; set; }
        public string idSubcategoria { get; set; }
        public string idSubcategoria2 { get; set; }
        public string categoria { get; set; }
        public string subcategoria { get; set; }
        public string subcategoria2 { get; set; }
        public string fechaUltimaActualizacion { get; set; }
    }

    public class ModelRespuesta
    {
        public bool resultado { get; set; }
        public string respuesta { get; set; }
    }

    public class Historial
    {
        public string NumeroLlamada { get; set; }
        public string FechaUltimaLlamada { get; set; }
        public string Agente { get; set; }
        public string RutCliente { get; set; }
        public string DV { get; set; }
        public string ResultadoLlamado { get; set; }
        public string MotivoLlamado { get; set; }
        public string ResultadoCampana { get; set; }
        public string MotivoCampana { get; set; }
        public string FonoContacto { get; set; }
    }

    public class ConsultaDatosTracking
    {
        public DatosOTTracking DatosOT { get; set; }
        public List<ListTracking> ListTracking { get; set; }
        public Respuesta Respuesta { get; set; }
    }

    public class DatosOTTracking
    {
        public string fechaCreacion { get; set; }
        public string cod_producto { get; set; }
        public string gls_producto { get; set; }
        public string cod_servicio { get; set; }
        public string gls_servicio { get; set; }
        public string cod_estado { get; set; }
        public string gls_estado { get; set; }
        public string referencia { get; set; }
        public string peso { get; set; }
        public string largo { get; set; }
        public string alto { get; set; }
        public string ancho { get; set; }
        public string DireccionCalle { get; set; }
        public string DirNumero { get; set; }
        public string DirComplemento { get; set; }
        public string cod_cobertura { get; set; }
        public string gls_cobertura { get; set; }
        public string gls_tiempo_estimado { get; set; }
    }

    public class ListTracking
    {
        public string nro_ot { get; set; }
        public int cod_tipo_evento { get; set; }
        public string cod_tipo_ot { get; set; }
        public string gls_tipo_ot { get; set; }
        public string nro_ot_padre { get; set; }
        public string referencia { get; set; }
        public string cod_of_origen { get; set; }
        public string cod_of_destino { get; set; }
        public string cod_destino { get; set; }
        public string rut_recibe { get; set; }
        public string gls_recibe { get; set; }
        public string gls_observacion { get; set; }
        public string cod_motivo { get; set; }
        public string gls_motivo { get; set; }
        public string fec_track { get; set; }
        public string fecha_recibe { get; set; }
        public string gls_tracking { get; set; }
        public string gls_tipoOT { get; set; }
        public string gls_Destino { get; set; }
        public string gls_nombre { get; set; }
        public string tipo_envio { get; set; }
        public string tipo_producto { get; set; }
        public string gls_servicio { get; set; }
        public string gls_destinatario { get; set; }
        public string gls_consulta { get; set; }
        public string ind_url_firma { get; set; }
        public string ind_url_pod { get; set; }
        public string gls_url_firma { get; set; }
        public string gls_url_pod { get; set; }
        public string indice { get; set; }
        public string fec_admision { get; set; }
    }

    public class Respuesta
    {
        public bool resultado { get; set; }
        public string respuesta { get; set; }
    }

    public class RetornoEstado
    {
        public int cod_tipo_evento { get; set; }
        public int cod_devolucion { get; set; }
    }

    public class ConsultaDatosOT
    {
        public List<DatosOTConsulta> DatosOT { get; set; }
        public Respuesta Respuesta { get; set; }
    }

    public class DatosOTConsulta
    {
        public string codigo_producto { get; set; }
        public string desc_producto { get; set; }
        public string codigo_servicio { get; set; }
        public string desc_servicio { get; set; }
        public string direccion_entrega { get; set; }
        public string fecha_estimada_entrega { get; set; }
        public string desc_referencia { get; set; }
        public string alto { get; set; }
        public string ancho { get; set; }
        public string largo { get; set; }
        public string peso { get; set; }
        public string fecha_creacion { get; set; }
        public string desc_cobertura { get; set; }
        public string Manipulacion { get; set; }
        public string fecha_admision { get; set; }
        public string nombre_receptor { get; set; }
        public string rut_remitente { get; set; }
        public string nombre_remitente { get; set; }
        public string telefono_remitente { get; set; }
        public string email_remitente { get; set; }
        public string rut_destintario { get; set; }
        public string nombre_destinatario { get; set; }
        public string telefono_destinatario { get; set; }
        public string email_destinatario { get; set; }
        public string cod_oficina_redireccion { get; set; }
        public string nombre_oficina_redireccion { get; set; }
        public string servicios_contratados { get; set; }
        public int cod_devolucion { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string ind_hash { get; set; }
    }
}