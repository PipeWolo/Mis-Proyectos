function RetornoAjax() {
    this.ret = '';
    this.msg = '';
    this.debug = '';
    this.values = [];
}

function FiltroUsuario() {
    this.Usuario = '';
    this.Nombre = '';
    this.Cantidad = '';
    this.Pagina = '';
}

function Usuario() {
    this.ID_USUARIO = '';
    this.USUARIO = '';
    this.NOMBRE = '';
    this.ID_PERFIL = '';
    this.PERFIL = '';
    this.PASSWORD = '';
    this.CORREO = '';
    this.ACTIVO = '';
    this.FECHA_CREACION = '';
    this.SERVICIO = '';
}



function FiltroCampana() {
    this.Campana = '';
    this.Cantidad = '';
    this.Pagina = '';
}

function Campana() {
    this.ID_CAMPANA = '';
    this.CODIGO_SERVICIO = '';
    this.NOMBRE_CAMPANA = '';
    this.SKILL_1 = '';
    this.SKILL_2 = '';
    this.MODO_DISCADO = '';
    this.PREFIJO = '';
}

function SMS() {
    this.ID = '';
    this.ANI = '';
    this.NOMBRE = '';
    this.URL = '';
}

function FiltroServicio() {
    this.SERVICIO = '';
    this.ID_CLIENTE = '';
    this.Cantidad = '';
    this.Pagina = '';
}

function FiltroBandeja() {
    this.CONNID = '';
    this.SKILL = '';
    this.FECHA = '';
    this.ANI = '';
    this.Cantidad = '';
    this.Pagina = '';
}

function Gestion() {
    this.FECHA_REAGENDA = '0';
    this.ID = '';
    this.ID_BASE_ENTRADA = '0';
    this.ID_ESTADO = '1';
    this.ID_ASIGNADO = '0';
    this.NOMBRE = '';
    this.APELLIDO = '';
    this.RUT = '';
    this.CORREO = '';
    this.DIRECCION = '';
    this.REGION = '';
    this.COMUNA = '';
    this.TIPIFICACION_1 = '';
    this.TIPIFICACION_2 = '';
    this.TIPIFICACION_3 = '';
    this.TIPIFICACION_4 = '';
}

function Tipificacion() {
    this.ID_TIPIFICACION = '';
    this.NIVEL_1 = '';
    this.NIVEL_2 = '';
    this.NIVEL_3 = '';
    this.NIVEL_4 = '';
    this.ID_CLIENTE = '';
    this.CLIENTE = '';
    this.ID_SERVICIO = '';
    this.SERVICIO = '';
    this.ACTIVO = '';
    this.CODIGO_SERVICIO = '';
    this.REAGENDA = '';
}

function ValidacionCarga() {
    this.N_LINEA = '';
    this.VALIDO = '';
    this.MENSAJE = '';
};