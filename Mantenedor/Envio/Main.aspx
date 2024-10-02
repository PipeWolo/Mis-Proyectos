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
        <h1>Envio SMS</h1>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-3">
            <div class="form-group">
               <label>ID Registro</label>
            </div>
         </div>
         <div class="col-sm-12 col-md-6">
            <div class="form-group">
               <input type="text" id="txtIDRegistro" class="form-control" maxlength="10"  readonly="readonly" runat="server"/>
            </div>
         </div>
     </div>
     <div class="row">
        <div class="col-sm-12 col-md-3">
            <div class="form-group">
               <label>Fecha Ingreso</label>
            </div>
         </div>
         <div class="col-sm-12 col-md-6">
            <div class="form-group">
               <input type="text" id="txtFechaIngreso" class="form-control" maxlength="10"  readonly="readonly" runat="server"/>
            </div>
         </div>
      </div>
     <div class="row">
         <div class="col-sm-12 col-md-3">
            <div class="form-group">
               <label>Hora Ingreso</label>
            </div>
         </div>
         <div class="col-sm-12 col-md-6">
            <div class="form-group">
               <input type="text" id="txtHora" class="form-control" maxlength="10"  readonly="readonly" runat="server"/>
            </div>
         </div>
      </div>
     <div class="row">
          <div class="col-sm-12 col-md-3">
            <div class="form-group">
               <label>URL</label>
            </div>
         </div>
          <div class="col-sm-12 col-md-6">
            <div class="form-group">
               <input type="text" id="txtURL" class="form-control" maxlength="10"  readonly="readonly" runat="server"/>
            </div>
         </div>
      </div>
    <div class="row">
         <div class="col-sm-12 col-md-3">
            <div class="form-group">
               <label>ANI Cliente</label>
            </div>
         </div>
         <div class="col-sm-12 col-md-6">
            <div class="form-group">
               <input type="text" class="form-control" id="txtANI" maxlength="9" data-validacion="Debe ingresar ANI Cliente (largo 9)"/>    
             </div>
         </div>
      </div>
    <div class="row">
         <div class="col-sm-12 col-md-3">
            <div class="form-group">
               <label>Nombre Cliente</label>
            </div>
         </div>
         <div class="col-sm-12 col-md-6">
            <div class="form-group">
               <input type="text" id="txtNombreCliente" class="form-control" maxlength="60" data-validacion="Debe ingresar un Nombre Cliente"/>
            </div>
         </div>
      </div>
     <div class="row">
        <div class="col-md-9 col-sm-9 col-xs-12 col-md-offset-3">
            <button type="button" class="btn btn-secondary" id="btnCancelar">Cancelar</button>
            <button type="button" id="btnGrabar" class="btn btn-primary">Enviar SMS</button>
        </div>
     </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modal" runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">

    <!--daterangepicker-->
    <script src="../assets/plugins/moment/moment-with-locales.min.js"></script>
    <link href="../assets/plugins/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../assets/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>

    <link href="../assets/plugins/bootstrap-datepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../assets/plugins/bootstrap-datepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/plugins/bootstrap-datepicker/js/locales/bootstrap-datetimepicker.es.js"></script>

    <!-- Jasny-Bootstrap -->
    <link rel="stylesheet" href="../assets/plugins/jquery/ui/jquery-ui.css" />
    <script src="../assets/plugins/jquery/ui/jquery-ui.js"></script>
    <script src="../assets/plugins/jasny-bootstrap/jasny-bootstrap.js"></script>
    <link href="../assets/plugins/jasny-bootstrap/jasny-bootstrap.css" rel="stylesheet" />
    <script src="../assets/plugins/jasny-bootstrap/jquery.fileupload.js"></script>
    <script src="../assets/plugins/xls2json/js/xlsx.full.min.js"></script>

    <script src="../controllers/ctlr_util.js" type="text/javascript"></script>
    <script type="text/javascript" src="./controllers/ctlr_bandeja.js?v=5.6"></script>
    <script type="text/javascript" src="./scripts/main.js?v=1.4"></script>
</asp:Content>
