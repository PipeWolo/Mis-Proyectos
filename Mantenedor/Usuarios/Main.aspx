<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Reportes_Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Usuarios</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv='cache-control' content='no-cache' />
    <meta http-equiv='expires' content='0' />
    <meta http-equiv='pragma' content='no-cache' />
    <link href="../css/local.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="section-header">
        <h1>Usuarios</h1>
    </div>
    <div class="section-body">
        <div class="row">
            <div class="col-md-12">
                <div class="card card-primary">
                    <div class="card-header">
                        <h4>Filtros</h4>
                        <div class="card-header-action">
                            <a class="btn btn-secondary" href="#" id="btnReestablecer"><i class="fas fa-sync-alt"></i>&nbsp;Reestablecer</a>
                            <a class="btn btn-success" href="#" id="btnNuevo"><i class="fas fa-plus"></i>&nbsp;Nuevo</a>
                            <a class="btn btn-primary" href="#" id="btnFiltrar"><i class="fas fa-filter"></i>&nbsp;Filtro</a>
                            <a class="btn btn-warning" href="#" id="btnCargaMasiva"><i class="fas fa-file-excel"></i>&nbsp;Carga Masiva</a>
                            <a data-collapse="#filtro-collapse" class="btn btn-icon btn-info" href="#" id="btnCerrarFiltro"><i class="fas fa-minus"></i></a>
                        </div>
                    </div>
                    <div class="collapse show" id="filtro-collapse" style="">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Usuario</label>
                                        <input type="text" id="txtUsuarioBuscar" class="form-control"  />
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Perfil</label>
                                        <select class="form-control" id="cboPerfilBuscar">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="table-responsive">
                    <table id="tblusuarios" class="table table-striped table-bordered no-footer" style="width:100%">
                        <thead>
                            <tr>
                                <th style="width:50px !important;"></th>
                                <th>Usuario</th>
                                <th>Nombre</th>
                                <th>Perfil</th>
                                <th>Fecha Creación</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="nuevo" tabindex="-1" role="dialog" style="background:rgba(0, 0, 0, 0.3);" aria-labelledby="nuevo" aria-hidden="true">
        <div class="modal-lg modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Usuario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row form-group">
                        <div class="col-sm-6 col-sm-6">
                            <input class="form-control" type="hidden" id="hidId" />
                            <label>Usuario</label>
                            <input class="form-control" type="text"  id="txtUsuario" name="txtUsuario" maxlength="30" data-validacion="Debe ingresar un usuario" />
                        </div>
                        <div class="col-sm-6 col-sm-6">
                            <label>Clave</label>
                            <input class="form-control" type="password"  id="txtPassword" name="txtPassword" maxlength="25" data-validacion="Debe seleccionar una clave valida" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-6 col-sm-6">
                            <label>Perfil</label>
                            <select class="form-control" id="cboPerfil" data-validacion="Debe seleccionar un perfil">
                            </select>
                        </div>
                        <div class="col-sm-6 col-sm-6">
                            <label>Nombre</label>
                            <input class="form-control" type="text"  id="txtNombre" name="txtNombre" maxlength="100" data-validacion="Debe ingresar un Nombre" />
                        </div>
                        
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-6 col-sm-6">
                            <label id="lblCorreo">Correo Eléctronico</label>
                            <input class="form-control" type="text"  id="txtCorreo" name="txtCorreo" maxlength="100" data-validacion="Debe ingresar un correo valido" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 mt-2 p-l-0 p-r-0">
                            <div class="notice notice-warning">
                                <strong>Notas:</strong><br />
                                <br />
                                <ul>
                                    <li><span>La contraseña debe tener al menos una mayúscula, un número, un carácter especial<strong>(. , - _ @ # $)</strong> y entre 14 y 25 caracteres.</span></li>
                                    <li><span>No puede utilizar una de sus últimas 3 contraseñas.</span></li>
                                    <li><span>Su contraseña expirara en 30 días.</span></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer bg-whitesmoke br">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button type="button" id="btnGrabar" class="btn btn-primary">Grabar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="mdlVerLog" tabindex="-1" role="dialog" style="background:rgba(0, 0, 0, 0.3);" aria-labelledby="nuevo" aria-hidden="true">
        <div class="modal-lg modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Usuario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive">
                                <table id="tblLog" class="table table-sm table-striped table-hover tableHorarios" style="width:100%">
                                    <thead>
                                        <tr>
                                            <th>Fecha</th>
                                            <th>Usuario</th>
                                            <th>Tipo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer bg-whitesmoke br">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="mdlCargaMasiva" tabindex="-1" role="dialog" style="background: rgba(0, 0, 0, 0.3);" aria-labelledby="nuevo" aria-hidden="true">
        <div class="modal-lg modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Carga Masiva de Usuarios</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="dvMantTipif">
                        <div id="masivaTipif" class="tab-pane">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Carga Masiva</h4>
                                    <div class="row">
                                        <div class="col-sm-8 col-md-8">
                                            <div class="fileinput fileinput-new row" data-provides="fileinput">
                                                <div class="col-sm-4 col-md-4">
                                                    <label class="text-info" style="width: 100%;">&nbsp;</label>
                                                    <span class="btn btn-default btn-file">
                                                        <span><i class="fa fa-upload"></i>&nbsp;Subir archivo</span>
                                                        <input type="file" id="fileUploadTipif" />
                                                    </span>
                                                </div>
                                                <div class="col-sm-5 col-md-5" style="margin-top: 10px;">
                                                    <div class="row">
                                                        <div class="col-sm-2 col-md-2">
                                                            <label class="text-info" style="width: 100%;">&nbsp;</label>
                                                            <span class="fileinput-filename"></span>
                                                        </div>
                                                        <div class="col-sm-10 col-md-10">
                                                            <label class="text-info" style="width: 100%;">&nbsp;</label>
                                                            <span class="fileinput-new">No hay archivo</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-md-4 " style="text-align: right;">
                                            <label class="text-info" style="width: 100%;">&nbsp;</label>
                                            <button class="btn btn-info" id="btnConfirmarArchTipif" type="button">Procesar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="dvResumenTipif">
                                <div class="col-md-12">
                                    <div class="card card-primary">
                                        <div class="card-header">
                                            <h4>Resumen</h4>
                                        </div>
                                        <div class="collapse show" id="filtro-collapse-Tipif" style="">
                                            <div class="card-body">
                                                <div class="col-md-12 col-sm-12">
                                                    <div class="table-responsive">
                                                        <table id="tablaResumenTipif" class="table table-striped table-bordered dataTable no-footer">
                                                            <thead>
                                                                <tr>
                                                                    <th>Línea</th>
                                                                    <th>Válido</th>
                                                                    <th>Mensaje</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="4" style="text-align: center;">Suba archivo para validar datos</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer bg-whitesmoke br">
                    <a id="btnDescargaPlantillaTipif" style="color: #fff !important;" class="btn btn-warning">Plantilla de Carga</a>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <iframe id="Iframe1" src="../KeepSessionAlive.aspx" scrolling="no" style="width: 16px; display: none; height: 8px" runat="server"></iframe>
    <input type="hidden" id="hidpag" value="0" />
    <input type="hidden" id="hdesde" />
    <input type="hidden" id="hhasta" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">

    <!--daterangepicker-->
    <script src="../assets/plugins/moment/moment-with-locales.min.js"></script>
    <link href="../assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../assets/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>

    <!-- Jasny-Bootstrap -->
    <link rel="stylesheet" href="../assets/plugins/jquery/ui/jquery-ui.css" />
    <script src="../assets/plugins/jquery/ui/jquery-ui.js"></script>
    <script src="../assets/plugins/jasny-bootstrap/jasny-bootstrap.js"></script>
    <link href="../assets/plugins/jasny-bootstrap/jasny-bootstrap.css" rel="stylesheet" />
    <script src="../assets/plugins/jasny-bootstrap/jquery.fileupload.js"></script>
    <script src="../assets/plugins/xls2json/js/xlsx.full.min.js"></script>

    <script type="text/javascript" src="../controllers/ctlr_util.js?v=1.3"></script>
    <script type="text/javascript" src="./controllers/ctlr_usuario.js?v=1.3"></script>
    <script type="text/javascript" src="./scripts/main.js?v=1.1"></script>
</asp:Content>