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
        <h1>Reportería Carga</h1>
    </div>
    <div class="section-body">
        <div class="row">
            <div class="col-md-12">
                <div class="card card-primary">
                    <div class="card-header">
                        <h4>Filtros</h4>
                        <div class="card-header-action">
                            <a class="btn btn-primary" href="#" id="btnFiltrar"><i class="fas fa-filter"></i>&nbsp;Filtro</a>
                            <%--<a class="btn btn-primary" href="#" id="btnExportar"><i class="fas fa-file-excel"></i>&nbsp;Exportar</a>--%>
                            <a data-collapse="#filtro-collapse" class="btn btn-icon btn-info" href="#" id="btnCerrarFiltro"><i class="fas fa-minus"></i></a>
                        </div>
                    </div>
                    <div class="collapse show" id="filtro-collapse" style="">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Campaña</label>
                                        <select id="cboCampanaBusqueda" class="form-control"></select>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Segmento Agente</label>
                                        <select id="cboSegmentoBusqueda" class="form-control">
                                            <option value="0">Seleccione ...</option>
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
                 <div class="row">
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <h6><i class="fas fa-layer-group" style="font-size: 1em;"></i>&nbsp;&nbsp;Reporte de Carga
                            </h6>
                            <hr />
                        </div>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-3 col-sm-6">
                        <label>Fecha Carga</label>
                        <input class="form-control" type="text"  id="txtFecha" disabled="disabled"/>
                    </div>
                    <div class="col-sm-3 col-sm-6">
                        <label>Hora Carga</label>
                        <input class="form-control" type="text"  id="txtHora" disabled="disabled"/>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-sm-3 col-sm-6">
                        <label>Registros Cargados</label>
                        <input class="form-control" type="text"  id="txtCargados" disabled="disabled"/>
                    </div>
                    <div class="col-sm-3 col-sm-6">
                        <label>Registros OK</label>
                        <input class="form-control" type="text"  id="txtOk" disabled="disabled"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 col-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Registros Repetidos</label>
                                        <input type="text" class="form-control" id="txtRepetidos" disabled="disabled"/>
                                    </div>
                                </div>
                                <div class="col-md-2 dv" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button type="button" id="btnRepetidos" class="btn btn-primary btn-lg btn-block">Descargar</button>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Registros Erroneos</label>
                                        <input type="text" class="form-control" id="txtErrores" disabled="disabled"/>
                                    </div>
                                </div>
                                <div class="col-md-2 dv" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button type="button" id="btnErrores" class="btn btn-primary btn-lg btn-block">Descargar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-3 col-sm-6">
                            <label>Registros Pendiente/Gestión Manual</label>
                            <input class="form-control" type="text"  id="txtGestionManual" disabled="disabled"/>
                        </div>
                        <div class="col-sm-3 col-sm-6">
                            
                        </div>
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

    <iframe id="Iframe1" src="../KeepSessionAlive.aspx" scrolling="no" style="width: 16px; display: none; height: 8px" runat="server"></iframe>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <!--daterangepicker-->
    <script src="../assets/plugins/moment/moment-with-locales.min.js"></script>
    <link href="../assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../assets/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script type="text/javascript" src="../controllers/ctlr_util.js?v=1.1"></script>
    <script type="text/javascript" src="controllers/ctlr_carga.js?v=1.1"></script>
    <script type="text/javascript" src="scripts/main.js?v=1.1"></script>
</asp:Content>