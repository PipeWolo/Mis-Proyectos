<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambiar Contraseña</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    
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
                                        <h4>Cambiar Contraseña</h4>
                                    </div>
                                    <asp:Panel ID="panError" Visible="false" runat="server" CssClass="error">
                                        <asp:Literal ID="litMensaje" runat="server"></asp:Literal>
                                    </asp:Panel>
                                    <div class="card-body">
                                        <div class="form-group">
                                            <label for="txtConfiguracionCuentaContrasenaNueva">Ingrese su nueva Contraseña</label>
                                            <input class="form-control" type="password" placeholder="Ingrese su nueva Contraseña" id="txtConfiguracionCuentaContrasenaNueva" runat="server" maxlength="100" />
                                        </div>

                                        <div class="form-group">
                                            <div class="d-block">
                                                <label for="txtConfiguracionCuentaContrasenaNuevaConfirma" class="control-label">Confirme su nueva contraseña</label>
                                            </div>
                                            <input class="form-control" type="password" placeholder="Confirme su nueva contraseña" id="txtConfiguracionCuentaContrasenaNuevaConfirma" runat="server" maxlength="100" />
                                        </div>

                                        <div class="form-group">
                                            <asp:ScriptManager ID="ScriptManager3" runat="server">
                                            </asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton CssClass="btn btn-primary btn-lg btn-block" ID="btnConfiguracionCuentaCambiar" runat="server" OnClick="btnConfiguracionCuentaCambiar_Click" Text="Cambiar contraseña" OnClientClick="ctrl_change_password.StartLoader()"></asp:LinkButton>
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
            </form>
        </section>
    </div>
    <!-- General JS Scripts -->
                <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <!-- JS Libraies -->
                <script src="../assets/plugins/sweetalert2/sweetalert2.all.min.js"></script>
    <script src="../assets/plugins/tilt/tilt.jquery.min.js"></script>
    <script src="../assets/plugins/button-loader/jquery.buttonLoader.min.js"></script>
    <script src="./controller/ctlr_change_password.js?v=3"></script>
    <script src="./scripts/change_password.js"></script>
    
</body>
</html>
