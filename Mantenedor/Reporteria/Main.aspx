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
        <h1>Reportería en línea</h1>
    </div>
    <div class="section-body">
        <div class="row">
            <div class="col-md-12">
                <div class="card card-primary">
                    <div class="card-header">
                        <h4>Filtros</h4>
                        <div class="card-header-action">
                            <a class="btn btn-primary" href="#" id="btnFiltrar"><i class="fas fa-filter"></i>&nbsp;Filtro</a>
                            <a class="btn btn-primary" href="#" id="btnExportar"><i class="fas fa-file-excel"></i>&nbsp;Exportar</a>
                            <a data-collapse="#filtro-collapse" class="btn btn-icon btn-info" href="#" id="btnCerrarFiltro"><i class="fas fa-minus"></i></a>
                        </div>
                    </div>
                    <div class="collapse show" id="filtro-collapse" style="">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Campaña</label>
                                        <select id="cboCampanaBusqueda" class="form-control"></select>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Segmento Agente</label>
                                        <select id="cboSegmentoBusqueda" class="form-control">
                                            <option value="0">Seleccione ...</option>
                                            <option value="SEGMENTO1">SEGMENTO1</option>
                                            <option value="SEGMENTO2">SEGMENTO2</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Fecha Desde/Hasta</label>
                                        <input type="text" id="txtDtCierre" class="form-control" readonly="readonly"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Resultado Llamada</label>
                                        <select id="cboResultadoLlamada" class="form-control">
                                            <option value="0">Todas ...</option>
                                            <option value="Contactado">Contactado</option>
                                            <option value="Descartado">Descartado</option>
                                            <option value="Pendiente">Pendiente</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Motivo Llamada</label>
                                        <select id="cboMotivoLlamada" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Resultado Campaña</label>
                                        <select id="cboResultadoCampana" class="form-control"></select>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Motivo Campaña</label>
                                        <select id="cboMotivoCampana" class="form-control">
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
                    <table id="tblllamadas" class="table table-striped table-hover small" style="width:100%">
                        <thead>
                            <tr>
                                <th>Numero Llamada</th>
                                <th>Resultado Llamada</th>
                                <th>Motivo Llamada</th>
                                <th>Resultado Campaña</th>
                                <th>Motivo Campaña</th>
                                <th>Agente</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
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

    <!--select2-->
    <link href="./assets/plugins/select2/dist/css/select2.min.css" rel="stylesheet" />
    <script src="./assets/plugins/select2/dist/js/select2.min.js"></script>
    <script src="./assets/plugins/select2/dist/js/i18n/es.js"></script>

    <script type="text/javascript" src="../controllers/ctlr_util.js?v=1.1"></script>
    <script type="text/javascript" src="controllers/ctlr_reporteria.js?v=1.1"></script>
    <script type="text/javascript" src="controllers/ctrl_tipificacion.js?v=1.5"></script>
    <script type="text/javascript" src="scripts/main.js?v=1.1"></script>
</asp:Content>