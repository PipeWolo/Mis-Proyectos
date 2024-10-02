<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Reportes_Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Campanas</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv='cache-control' content='no-cache' />
    <meta http-equiv='expires' content='0' />
    <meta http-equiv='pragma' content='no-cache' />
    <link href="../css/local.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="section-header">
        <h1>Campanas</h1>
    </div>
    <div class="section-body">
        <%--<div class="row">
            <div class="col-md-12">
                <div class="card card-primary">
                    <div class="card-header">
                        <h4>Filtros</h4>
                        <div class="card-header-action">
                            <a class="btn btn-secondary" href="#" id="btnReestablecer"><i class="fas fa-sync-alt"></i>&nbsp;Reestablecer</a>
                            <a class="btn btn-success" href="#" id="btnNuevo"><i class="fas fa-plus"></i>&nbsp;Nuevo Campana</a>
                            <a class="btn btn-primary" href="#" id="btnFiltrar"><i class="fas fa-filter"></i>&nbsp;Filtro</a>
                            <a data-collapse="#filtro-collapse" class="btn btn-icon btn-info" href="#" id="btnCerrarFiltro"><i class="fas fa-minus"></i></a>
                        </div>
                    </div>
                    <div class="collapse show" id="filtro-collapse" style="">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    <div class="form-group">
                                        <label>Campaña</label>
                                        <select class="form-control" id="cboCampanaBuscar"></select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="table-responsive">
                    <table id="tblcampanas" class="table table-striped table-bordered no-footer" style="width:100%">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Codigo Servicio</th>
                                <th>Nombre Campaña</th>
                                <th>Modo Discado</th>
                                <th>Prefijo</th>
                                <th>Reciclado</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    
    <div class="modal" id="nuevoCampana" tabindex="-1" role="dialog" style="background:rgba(0, 0, 0, 0.3);" aria-labelledby="nuevo" aria-hidden="true">
        <div class="modal-lg modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Campanas</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row form-group">
                        <div class="col-sm-12 col-sm-6">
                            <input class="form-control" type="hidden" id="hidId" />
                            <label>Codigo Servicio</label>
                            <input class="form-control" type="text"  id="txtCodigoServicio" disabled="disabled" />
                        </div>
                        <div class="col-sm-12 col-sm-6">
                            <label>Nombre Campaña</label>
                            <input class="form-control" type="text"  id="txtNombreCampana" name="txtRut" maxlength="60" data-validacion="Debe ingresar un Nombre Campaña" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-12 col-sm-6">
                            <label>Skill 1</label>
                            <input class="form-control" type="text"  id="txtSkill1" name="txtRut" maxlength="30" data-validacion="Debe ingresar un Skill 1" />
                        </div>
                        <div class="col-sm-12 col-sm-6">
                            <label>Skill 2</label>
                            <input class="form-control" type="text"  id="txtSkill2" name="txtRut" maxlength="30" data-validacion="Debe ingresar un Skill 2" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-12 col-sm-6">
                            <label>Modo Discado</label>
                            <select class="form-control" id="cboModoDiscado">
                                <option value="0">Seleccione ...</option>
                                <option value="Asistido">Asistido</option>
                                <option value="Mixto">Mixto</option>
                                <option value="Predictivo">Predictivo</option>
                            </select>
                        </div>
                        <div class="col-sm-12 col-sm-6">
                            <label>Prefijo</label>
                            <input class="form-control" type="text"  id="txtPrefijo" name="txtRut" maxlength="30" data-validacion="Debe ingresar un Prefijo" />
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

    <div class="modal" id="nuevoReciclado" tabindex="-1" role="dialog" style="background:rgba(0, 0, 0, 0.3);" aria-labelledby="nuevo" aria-hidden="true">
        <div class="modal-lg modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Recicla Registros</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row form-group">
                        <div class="col-sm-12 col-sm-6">
                            <input class="form-control" type="hidden" id="hiCodigoServicio" />
                            <input class="form-control" type="hidden" id="hiCampana" />
                            <label>Segmento</label>
                            <select class="form-control" id="cboSegmento">
                                <option value="0">Seleccione ...</option>
                                <option value="SEGMENTO1">SEGMENTO1</option>
                                <option value="SEGMENTO2">SEGMENTO2</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="modal-footer bg-whitesmoke br">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button type="button" id="btnReciclar" class="btn btn-primary">Reciclar</button>
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


    <script type="text/javascript" src="./controllers/ctlr_campanas.js?v=1.1"></script>
    <script type="text/javascript" src="./scripts/main.js?v=9"></script>
</asp:Content>