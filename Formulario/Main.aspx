<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<%@ Import Namespace="Navigator.Clases" %>
<%@ Import Namespace="System.Collections.Generic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="./assets/css/nuevo_ticket.css" rel="stylesheet" />

     <script>
        sessionStorage.setItem('DatosCampanas', '<%=((List<KVP>)Session["DatosCampana"])%>');
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="section-body">
        <div class="card">
            <div class="card-body" id="body-formulario">
                <asp:Panel ID="panError" Visible="false" runat="server" CssClass="error">
                    <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
                </asp:Panel>
                <div class="row" id="pnlAsistido" runat="server">
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <h6><i class="fas fa-phone-volume" style="font-size: 1em;"></i>&nbsp;&nbsp;Contacto Socio
                            </h6>
                            <hr />
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="row">
                            <div class="col-sm-12 col-md-6">
                                <div class="form-group">
                                    <label>Campaña a Trabajar</label>
                                    <select id="cboCampanas" class="form-control" runat="server"></select>
                                    <%--<input type="text" class="form-control" id="txtNombreCampana" maxlength="100" placeholder="Habilidad" disabled="disabled" runat="server" />--%>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-6">
                                <div class="form-group">
                                    <label>Agente</label>
                                    <input type="text" class="form-control" id="txtNombreAgente" maxlength="100" placeholder="Habilidad" disabled="disabled" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="row">
                            <div class="col-md-3">
                                 <div class="form-group">
                                     <label>&nbsp;</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnPedirRegistro">Pedir</button>                 
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <button type="button" class="btn btn-primary btn-lg btn-block" id="btnBuscarRegistro">Buscar</button>                        
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                     <label>&nbsp;</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="pnlBuscarRegistro" runat="server" style="display:none">
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <h6><i class="fas fa-search" style="font-size: 1em;"></i>&nbsp;&nbsp;Búsqueda
                            </h6>
                            <hr />
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Rut Socio</label>
                                    <input type="text" class="form-control" id="txtRutClienteBusqueda" maxlength="8" onkeypress="return event.charCode >= 48 && event.charCode <= 57"/>
                                </div>
                            </div>
                            <%--<div class="col-md-4">
                                <div class="form-group">
                                    <label>Campaña<span class="text-danger obligatorio">(*)</span></label>
                                    <select id="cboCampañaBusqueda" class="form-control" data-validacion="Debe seleccionar habilidad"></select>
                                </div>
                            </div>--%>
                            <div class="col-md-4 dv" style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <button type="button" id="btnBuscar" class="btn btn-primary btn-lg btn-block">Buscar</button>
                                </div>
                            </div>
                            <div class="col-md-4 dv" style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <button type="button" id="btnCancelarBusqueda" class="btn btn-warning btn-lg btn-block">Cancelar</button>
                                </div>
                            </div>
                        </div>
                    </div> 
                    <div class="col-sm-12 col-md-12">
                        <div class="row">
                            <div class="col-sm-12 col-md-12 table-responsive">
                                <table id="tblBusqueda" class="table table-striped table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Numero Llamada</th>
                                            <th>Fecha Llamada</th>
                                            <th>Resultado Llamada</th>
                                            <th>Motivo Resultado Llamada</th>
                                            <th>Resultado Campaña</th>
                                            <th>Motivo Resultado Campaña</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="pnlLlamada" style="display:none">
                    <div class="row">
                        <div class="col-sm-12 col-md-12">
                            <div class="form-group">
                                <h6><i class="fas fa-phone-volume" style="font-size: 1em;"></i>&nbsp;&nbsp;Datos de Llamada
                                    <span class="right" style="font-weight: 100">Los campos con <span class="text-danger obligatorio">(*)</span> son obligatorios.</span>
                                </h6>
                                <hr />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 col-md-3">
                            <div class="form-group">
                                <label>Nº Llamada</label>
                                <input type="text" class="form-control" id="txtNLlamada" maxlength="20" disabled="disabled" runat="server" />
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <div class="form-group">
                                <label>Código Servicio</label>
                                <input type="text" class="form-control" id="txtCodigoServicio" maxlength="10" placeholder="Código del servicio" data-validacion="Debe ingresar un código servicio válido" disabled="disabled" runat="server" />
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <div class="form-group">
                                <label>Campaña</label>
                                <input type="text" class="form-control" id="txtCampana" maxlength="100" disabled="disabled" />
                            </div>
                        </div>
                        
                        <div class="col-sm-12 col-md-3">
                            <div class="form-group">
                                <label>ConnID</label>
                                <input type="text" class="form-control" id="txtConnID" maxlength="15" disabled="disabled" runat="server" />
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-4">
                            <div class="form-group">
                                <label>Skill</label>
                                <input type="text" class="form-control" id="txtSkillCampana" maxlength="10" placeholder="ANI" data-validacion="Debe ingresar un ANI válido" disabled="disabled" runat="server" />
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-4">
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Fono Contacto</label>
                                        <input type="text" class="form-control" id="txtFonoContacto" maxlength="9" runat="server" onkeypress="return event.charCode >= 48 && event.charCode <= 57"/>
                                    </div>
                                </div>
                                <div class="col-md-4 dv" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <button type="button" id="btnDiscarFono" class="btn btn-primary btn-lg btn-block">Discar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-4">
                            <div class="form-group">
                                <label>Agente</label>
                                <input type="text" class="form-control" id="txtAgente" maxlength="10" placeholder="Código del servicio" data-validacion="Debe ingresar un código servicio válido" disabled="disabled" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div style="position: absolute; right: 2em;">
                        <a href="#tablist" class="btn btn-primary" id="btnTransfiereEPA" >Transfiere EPA</a>
                        <a href="#tablist" class="btn btn-warning right" id="btnGrabar">Guardar</a>
                    </div>
                    <!-- Tabs -->
                    <ul class="nav nav-tabs" id="myTab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="cliente-tab" data-toggle="tab" href="#cliente" role="tab" aria-controls="cliente" aria-selected="true"><i class="fas fa-user"></i>&nbsp;&nbsp;Datos Socio</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="producto-tab" data-toggle="tab" href="#producto" role="tab" aria-controls="producto" aria-selected="false"><i class="fas fa-bars"></i>&nbsp;&nbsp;Producto</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="tipificacion-tab" data-toggle="tab" href="#tipificacion" role="tab" aria-controls="tipificacion" aria-selected="false"><i class="fas fa-clone"></i>&nbsp;&nbsp;Tipificación</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="historial-tab" data-toggle="tab" href="#historial" role="tab" aria-controls="historial" aria-selected="false"><i class="fas fa-table"></i>&nbsp;&nbsp;Historial</a>
                        </li>
                    </ul>
                    <div class="tab-content" id="myTabContent">
                        <div class="tab-pane fade show active" id="cliente" role="tabpanel" aria-labelledby="cliente-tab">
                            <!-- Datos Cliente -->
                            <div class="row">
                                <div class="col-sm-12 col-md-4">
                                    <div class="row">
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <label>RUT</label>
                                                <input type="text" class="form-control" id="txtClienteRUT" readonly="readonly"/>
                                            </div>
                                        </div>
                                        <div class="col-md-3 dv" style="padding-left: 0px;">
                                            <div class="form-group">
                                                <label>DV</label>
                                                <input type="text" id="txtClienteDV" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Nombre Socio</label>
                                        <input type="text" class="form-control" id="txtNombreCliente" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Código Socio</label>
                                        <input type="text" class="form-control" id="txtCodigoSocio" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Comuna</label>
                                        <input type="text" class="form-control" id="txtComuna" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Teléfono Movil</label>
                                                <input type="text" class="form-control" id="txtTelefonoMovil" readonly="readonly"  />
                                            </div>
                                        </div>
                                        <div class="col-md-4 dv" style="padding-left: 0px;">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <button type="button" id="btnDiscarTelefonoMovil" class="btn btn-primary btn-lg btn-block">Discar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Teléfono Fijo</label>
                                                <input type="text" class="form-control" id="txtTelefonoFijo" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-md-4 dv" style="padding-left: 0px;">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <button type="button" id="btnDiscarTelefonoFijo" class="btn btn-primary btn-lg btn-block">Discar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Telefono Trabajo</label>
                                                <input type="text" class="form-control" id="txtTelefonoTrabajo" readonly="readonly"/>
                                            </div>
                                        </div>
                                        <div class="col-md-4 dv" style="padding-left: 0px;">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <button type="button" id="btnDiscarTelefonoTrabajo" class="btn btn-primary btn-lg btn-block">Discar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Telefono Movil Trabajo</label>
                                                <input type="text" class="form-control" id="txtTelefonoMovilTrabajo" readonly="readonly"/>
                                            </div>
                                        </div>
                                        <div class="col-md-4 dv" style="padding-left: 0px;">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <button type="button" id="btnDiscarTelefonoMovilTrabajo" class="btn btn-primary btn-lg btn-block">Discar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Correo Electrónico 1  </label>
                                        <input type="text" class="form-control" id="txtCorreoElectronico1" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Correo Electrónico 2  </label>
                                        <input type="text" class="form-control" id="txtCorreoElectronico2" readonly="readonly"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade show" id="producto" role="tabpanel" aria-labelledby="producto-tab">
                            <!-- Datos Cliente -->
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Monto Aporte</label>
                                        <input type="text" class="form-control" id="txtMontoAporte" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Divisa</label>
                                        <input type="text" class="form-control" id="txtDivisa" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Fundación </label>
                                        <input type="text" class="form-control" id="txtFundacion" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <div class="form-group">
                                        <label>Acuerdo </label>
                                        <input type="text" class="form-control" id="txtAcuerdo" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Tipo Medio Pago</label>
                                        <input type="text" class="form-control" id="txtTipoMedioPago" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Medio Pago </label>
                                        <input type="text" class="form-control" id="txtMedioPago" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Oficina Venta</label>
                                        <input type="text" class="form-control" id="txtOficinaVenta" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Sede  </label>
                                        <input type="text" class="form-control" id="txtSede" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Monto Base </label>
                                        <input type="text" class="form-control" id="txtMontoBase" readonly="readonly"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <div class="form-group">
                                        <label>Monto Propuesto</label>
                                        <input type="text" class="form-control" id="txtMontoPropuesto" readonly="readonly"/>
                                    </div>
                                </div>
                                 <div class="col-sm-12 col-md-12">
                                    <div class="row">
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <label>URL Call</label>
                                                <input type="text" class="form-control" id="txtURLCall" readonly="readonly"/>
                                            </div>
                                        </div>
                                        <div class="col-md-3 dv" style="padding-left: 0px;">
                                            <div class="form-group">
                                                <label>&nbsp;&nbsp;</label>
                                                <button type="button" id="btnLink" class="btn btn-primary btn-lg btn-block" style="background-color:#009da6;">Abrir</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-12">
                                    <div class="form-group">
                                        <label>Observaciones</label>
                                        <textarea class="form-control" id="txtObservacion" style="height: 10em!important;" data-validacion="Debe ingresar una observación" maxlength="1000" onkeypress="return ctlr_util.ContadorCaracteres(this,1000,'#contador')" onchange="return ctlr_util.ContadorCaracteres(this,1000,'#contador')"></textarea>
                                        <small class="form-text text-muted float-right"><span id="contador">0</span> de 1.000 caracteres</small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="tipificacion" role="tabpanel" aria-labelledby="tipificacion-tab">
                            <div class="row" id="pnlReprogramacion" style="display:none;">
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Fecha/Hora Reprogramación<span class="text-danger obligatorio">(*)</span></label>
                                        <input type="text" id="txtFechaRepro" class="form-control" readonly="readonly"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Resultado Llamada <span class="text-danger obligatorio">(*)</span></label>
                                        <select id="cboResultadoLlamada" class="form-control" data-validacion="Debe seleccionar habilidad">
                                            <option value="0">Seleccione ...</option>
                                            <option value="Contactado">Contactado</option>
                                            <option value="Descartado">Descartado</option>
                                            <option value="Pendiente">Pendiente</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Motivo Llamada <span class="text-danger obligatorio">(*)</span></label>
                                        <select id="cboMotivoLlamada" class="form-control" data-validacion="Debe seleccionar operacion" disabled="disabled"></select>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Resultado Camapaña <span class="text-danger obligatorio">(*)</span></label>
                                        <select id="cboResultadoCampana" class="form-control" data-validacion="Debe seleccionar suboperacion" disabled="disabled"></select>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                    <div class="form-group">
                                        <label>Motivo Camapaña <span class="text-danger obligatorio">(*)</span></label>
                                        <select id="cboMotivoCampana" class="form-control" data-validacion="Debe seleccionar tipo" disabled="disabled"></select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="historial" role="tabpanel" aria-labelledby="historial-tab">
                            <div class="row">
                                <div class="col-sm-12 col-md-12 table-responsive">
                                    <table id="tblHistorial" class="table table-striped table-bordered" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>Numero Llamada</th>
                                                <th>Fecha Llamada</th>
                                                <th>Resultado Llamada</th>
                                                <th>Motivo Resultado Llamada</th>
                                                <th>Resultado Campaña</th>
                                                <th>Motivo Resultado Campaña</th>
                                            </tr>
                                        </thead>
                                        <tbody>
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
    <div>
        <input type="hidden" runat="server" id="hAgente" />
        <input type="hidden" runat="server" id="hNombreLista" />
        <input type="hidden" runat="server" id="hCodigoServicio" />
        <input type="hidden" runat="server" id="hConnIdDec" value="0" />
        <input type="hidden" runat="server" id="hConnIdHex" value="0" />
        <input type="hidden" runat="server" id="hIdentificadorCliente" value="0" />
        <input type="hidden" runat="server" id="hKeySP" />
        <input type="hidden" runat="server" id="hRUT" />
        <input type="hidden" runat="server" id="hMensajeError" value="0"  />
        <input type="hidden" id="hPrefijo" />
        <input type="hidden" id="hOpcionDiscado" />
        <input type="hidden" id="hModoDiscado" />
        <input type="hidden" runat="server" id="hidmaquina" />
        <input type="hidden" runat="server" id="hiderror" />
        <input type="hidden" runat="server" id="hidprefijoEPA" value="" />
        <input type="hidden" runat="server" id="hidvdnEPA" value="" />
        <input type="hidden" id="hIDCS" value="" />
        <input type="hidden" id="hRecordID" value="" runat="server" />
        <iframe style="width: 16px; display: none; height: 8px" src="" name="ir_pie" scrolling="no"></iframe>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <!--- Plugins --->

    <!--ButtonLoader-->
    <link href="./assets/plugins/button-loader/buttonLoader.min.css" rel="stylesheet" />
    <script src="./assets/plugins/button-loader/jquery.buttonLoader.min.js"></script>

    <!--select2-->
    <link href="./assets/plugins/select2/dist/css/select2.min.css" rel="stylesheet" />
    <script src="./assets/plugins/select2/dist/js/select2.min.js"></script>
    <script src="./assets/plugins/select2/dist/js/i18n/es.js"></script>

    <!--moment-->
    <script src="./assets/plugins/moment/moment-with-locales.min.js"></script>
    <script src="./assets/plugins/moment/momentjs-business.js"></script>

    <!--busy-load-->
    <link href="./assets/plugins/busy-load/busy-load.min.css" rel="stylesheet" />
    <script src="./assets/plugins/busy-load/busy-load.min.js"></script>

    <!--numeral-->
    <script src="./assets/plugins/numeral/numeral.min.js"></script>
    <script src="./assets/plugins/numeral/numeral.es.js"></script>

    <!--datatables-->
    <link href="./assets/plugins/datatables/1.10.19/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="./assets/plugins/datatables/1.10.19/jquery.dataTables.min.js"></script>
    <script src="./assets/plugins/datatables/1.10.19/dataTables.bootstrap4.min.js"></script>

    <!--autocomplete-->
    <link href="./assets/plugins/jquery-ui-autocomplete/jquery-ui.min.css" rel="stylesheet" />
    <script src="./assets/plugins/jquery-ui-autocomplete/jquery-ui.min.js"></script>

    <!--bootstrap-datepicker-->
    <script src="./assets/plugins/moment/moment-with-locales.min.js"></script>
    <link href="./assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="./assets/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>

    <link href="./assets/plugins/bootstrap-datepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="./assets/plugins/bootstrap-datepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="./assets/plugins/bootstrap-datepicker/js/locales/bootstrap-datetimepicker.es.js"></script>
    
    <script src="<%=ResolveUrl("~/assets/plugins/sweetalert/sweetalert.min.js")%>"></script>

    <!--- Custom --->
    <script src="./navigator/clases.js"></script>
    <script type="text/javascript" src="./controller/ctlr_login.js"></script>
    <script src="./controller/ctlr_main.js?v=3.3"></script>
    <script src="./controller/ctrl_tipificacion.js?v=1.5"></script>
    <script src="./scripts/main.js?v=2"></script>

</asp:Content>

