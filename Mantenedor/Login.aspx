<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="PaginaLogin" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="./assets/img/logo/favicon.png">

    <!-- General CSS Files -->
    <link rel="stylesheet" href="./assets/plugins/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="./assets/plugins/fontawesome-free/5.10.2/css/all.min.css">

    <!-- Template CSS -->
    <link rel="stylesheet" href="./assets/css/style.css?v=1">
    <link rel="stylesheet" href="./assets/css/components.css">
</head>
<body>
    <div id="app">
        <section class="section">
            <div class="container mt-5">
                <div class="row">
                    <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4">
                        <div class="login-brand">
                            <img style="width: 50%;" src="./assets/img/logo/logo.png" alt="logo">
                        </div>

                        <div class="card card-primary">
                            <div class="card-header">
                                <h4>Login</h4>
                            </div>
                            <asp:Panel ID="panError" Visible="false" runat="server" CssClass="error">
                                <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
                            </asp:Panel>

                            <div class="card-body">
                                <form runat="server">
                                    <div class="form-group">
                                        <label for="txtUsuario">Usuario</label>
                                        <input id="txtUsuario" type="text" class="form-control" name="txtUsuario" tabindex="1" autofocus placeholder="Ejemplo: nusuario" runat="server" />
                                    </div>

                                    <div class="form-group">
                                        <div class="d-block">
                                            <label for="txtContrasena" class="control-label">Clave</label>
                                        </div>
                                        <input id="txtPassword" type="password" class="form-control" name="txtPassword" tabindex="2" runat="server" />
                                    </div>

                                    <div class="form-group">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
                                        </asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton CssClass="btn btn-primary btn-lg btn-block" ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login <i class='ti-arrow-right pull-right' style='float: right;margin-top: 13px;margin-right: 10px;'></i>" TabIndex="4"></asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="text-center p-t-30">
                                        <span class="txt1">Olvide mi <a class="txt2" href="./Cuenta/ForgotPassword.aspx">Clave</a></span>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <!-- General JS Scripts -->
    <script src="./assets/plugins/jquery/jquery.min.js"></script>
    <!-- JS Libraies -->
    <script src="assets/plugins/sweetalert2/sweetalert2.all.min.js"></script>
    <!-- Page Specific JS File -->
    <script type="text/javascript" src="controllers/ctlr_login.js"></script>
    <script type="text/javascript" src="scripts/login.js"></script>
    <iframe id="KeepAliveFrame" src="KeepSessionAlive.aspx" scrolling="no" style="width: 16px; display: none; height: 8px" runat="server"></iframe>
</body>
</html>
