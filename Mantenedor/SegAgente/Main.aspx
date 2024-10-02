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
        <h1>Segmentos x Agente</h1>
    </div>
    <div class="section-body">
        <div class="row">
            <div class="col-md-12">
                <div class="card card-primary">
                    <div class="card-header">
                        <h4>Filtros</h4>
                        <div class="card-header-action">
                            <a class="btn btn-secondary" href="#" id="btnReestablecer"><i class="fas fa-sync-alt"></i>&nbsp;Restablecer</a>
                            <a class="btn btn-success" href="#" id="btnNuevo"><i class="fas fa-plus"></i>&nbsp;Nuevo</a>
                            <a class="btn btn-primary" href="#" id="btnFiltrar"><i class="fas fa-filter"></i>&nbsp;Filtro</a>
                            <a class="btn btn-warning" href="#" id="btnCargaMasiva"><i class="fas fa-file-excel"></i>&nbsp;Carga Masiva</a>
                            <a data-collapse="#filtro-collapse" class="btn btn-icon btn-info" href="#" id="btnCerrarFiltro"><i class="fas fa-minus"></i></a>
                        </div>
                    </div>
                    <div class="collapse show" id="filtro-collapse" style="">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Agente</label>
                                        <input type="text" id="txtAgenteBuscar" class="form-control"  onkeypress="return event.charCode >= 48 && event.charCode <= 57" maxlength="8"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Codigo Servicio</label>
                                        <input type="text" id="txtCodigoServicioBuscar" class="form-control" maxlength="10"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Segmento Agente</label>
                                        <select id="cboSegmentoBusqueda" class="form-control">
                                            <option value="0">Todos ...</option>
                                            <option value="SEGMENTO1">SEGMENTO1</option>
                                            <option value="SEGMENTO2">SEGMENTO2</option>
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
                    <table id="tblservicios" class="table table-striped table-hover small" style="width:100%">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Agente</th>
                                <th>Nombre Agente</th>
                                <th>Segmento Agente</th>
                                <th>Campaña</th>
                                <th>Codigo Servicio</th>
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
                    <h5 class="modal-title">Agente x Segmento</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row form-group">
                        <div class="col-sm-3 col-sm-6">
                            <input class="form-control" type="hidden" id="hid" />
                            <label>Agente</label>
                            <input class="form-control" type="text"  id="txtAgente" maxlength="8" onkeypress="return event.charCode >= 48 && event.charCode <= 57"/>
                        </div>
                        <div class="col-sm-3 col-sm-6">
                            <label>Nombre Agente</label>
                            <input class="form-control" type="text"  id="txtNombreAgente" disabled="disabled"/>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-3 col-sm-6">
                            <label>Campaña</label>
                            <select id="cboCampana" class="form-control"></select>
                        </div>
                        <div class="col-sm-3 col-sm-6">
                            <label>Codigo Servicio</label>
                            <input class="form-control" type="text"  id="txtCodigoServicio" disabled="disabled"/>
                        </div>
                    </div>
                     <div class="row form-group">
                         <div class="col-sm-3 col-sm-6">
                            <label>Segmento</label>
                            <select id="cboSegmento" class="form-control">
                                <option value="0">Seleccione ...</option>
                                <option value="SEGMENTO1">SEGMENTO1</option>
                                <option value="SEGMENTO2">SEGMENTO2</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="modal-footer bg-whitesmoke br">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" id="btnGrabar" class="btn btn-primary">Grabar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="mdlCargaMasiva" tabindex="-1" role="dialog" style="background: rgba(0, 0, 0, 0.3);" aria-labelledby="nuevo" aria-hidden="true">
        <div class="modal-lg modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Carga Masiva de Agentes x Segmento</h5>
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

    <script type="text/javascript" src="../controllers/ctlr_util.js?v=1.1"></script>
    <script type="text/javascript" src="controllers/ctlr_segagente.js?v=1.1"></script>
    <script type="text/javascript" src="scripts/main.js?v=1.1"></script>
</asp:Content>