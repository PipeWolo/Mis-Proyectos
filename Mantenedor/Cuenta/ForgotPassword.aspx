<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="NewPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Olvide mi contraseña</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link rel="stylesheet" type="text/css" href="../assets/plugins/animate/animate.css" />
    <link rel="stylesheet" type="text/css" href="../assets/plugins/css-hamburgers/hamburgers.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/plugins/button-loader/buttonLoader.min.css" />

    <!-- General CSS Files -->
    <link rel="stylesheet" href="../assets/plugins/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/plugins/fontawesome-free/5.10.2/css/all.min.css" />

    <!-- Template CSS -->
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/components.css" />
</head>
<body>
    <div id="app">
        <section class="section">
            <form runat="server">
                <div class="limiter" id="recovery" runat="server">
                    <div class="container mt-5">
                        <div class="row">
                            <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4">
                                <div class="login-brand">
                                    <img style="width: 50%;" src="../assets/img/logo/logoLogin.png" alt="logo" />
                                </div>
                            </div>
                            <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4">
                                <div class="card card-primary">
                                    <div class="card-header">
                                        <h4>Olvide mi contraseña</h4>
                                    </div>
                                    <asp:Panel ID="panError" Visible="false" runat="server" CssClass="error">
                                        <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
                                    </asp:Panel>

                                    <div class="card-body">
                                        <div class="form-group">
                                            <label for="txtConfiguracionCuentaContrasenaNueva">Nueva contraseña</label>
                                            <input class="form-control" type="password" placeholder="Ingrese su nueva contraseña" id="txtConfiguracionCuentaContrasenaNueva" runat="server" maxlength="100" />
                                        </div>

                                        <div class="form-group">
                                            <div class="d-block">
                                                <label for="txtConfiguracionCuentaContrasenaNuevaConfirma" class="control-label">Confirme su contraseña</label>
                                            </div>
                                            <input class="form-control" type="password" placeholder="Confirme su nueva contraseña" id="txtConfiguracionCuentaContrasenaNuevaConfirma" runat="server" maxlength="100" />
                                        </div>

                                        <div class="form-group">
                                            <asp:ScriptManager ID="ScriptManager3" runat="server">
                                            </asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton CssClass="btn btn-primary btn-lg btn-block" ID="LinkButton1" runat="server" OnClick="btnConfiguracionCuentaCambiar_Click" Text="Cambiar Contraseña" OnClientClick="ctrl_cuenta.StartLoader()"></asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
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
                            </div>
                        </div>
                    </div>
                </div>
                <div class="limiter" id="firstStep" runat="server">
                    <div class="container mt-5">
                        <div class="row">
                            <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4">
                                <div class="login-brand">
                                    <img style="width: 50%;" src="../assets/img/logo/logoLogin.png" alt="logo" />
                                </div>
                            </div>
                            <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4">

                                <div class="card card-primary">
                                    <div class="card-header">
                                        <h4>Olvidé mi Contraseña</h4>
                                    </div>
                                    <asp:Panel ID="Panel1" Visible="false" runat="server" CssClass="error">
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </asp:Panel>

                                    <div class="card-body">
                                        <div class="form-group">
                                            <label for="txtRecuperarUsuario">Login</label>
                                            <input class="form-control" type="text" placeholder="Ingrese su login" id="txtRecuperarUsuario" runat="server" maxlength="100" />
                                        </div>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <a class="btn btn-primary btn-lg btn-block" id="btnRecuperar" href="#">Recuperar contraseña</a>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-12 mt-2 p-l-0 p-r-0">
                                            <div class="notice notice-warning">
                                                <strong>Notas:</strong><br />
                                                <br />
                                                <ul>
                                                    <li><span>Un link sera enviado a su email para recuperar su contraseña.</span></li>
                                                    <li><span>El link expirará en 60 minutos.</span></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- General JS Scripts -->
                <script src="../assets/plugins/jquery/jquery.min.js"></script>
                <!-- JS Libraies -->
                <script src="../assets/plugins/sweetalert2/sweetalert2.all.min.js"></script>
                <script src="../assets/plugins/tilt/tilt.jquery.min.js"></script>
                <script src="../assets/plugins/button-loader/jquery.buttonLoader.min.js"></script>
                <script src="./controller/ctrl_cuenta.js?v=3"></script>
                <script src="./controller/ctrl_forgot_password.js?v=3"></script>
                <script src="./scripts/forgot_password.js"></script>

            </form>

        </section>
    </div>
</body>
</html>
