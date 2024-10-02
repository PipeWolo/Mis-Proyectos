<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Paris BO</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <script src="jquery/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="bootstrap/bootstrap.min.js" type="text/javascript"></script>
    <script src="scripts/exit.js" type="text/javascript"></script>
    <!--HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
        <script src="bootstrap/trunk/html5.js"></script>
        <script src="bootstrap/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        function logout() {
            var url = "Login.aspx";
            window.top.location.href = url;
        }
    </script>
</head>
<body>
    <div class="container" style="margin-top:40px">
		<div class="row">
			<div class="col-sm-6 col-md-4 col-md-offset-4">
				<div class="panel panel-default">
					<div class="panel-heading">
						<strong>Paris BO</strong>
					</div>
					<div class="panel-body">
                        <div class="row">
							<div class="center-block">
								<img src="images/logo.jpg" class="img-responsive center-block" alt="Paris" />
                                <br />
							</div>
						</div>
                        <div class="alert alert-dismissable alert-info">
                            <strong>Su sesión a expirado.</strong>Por su seguridad su sesión de usuario ha sido cerrada. Para volver a iniciar sesión clic en botón Login. Para finalizar solo cierre el navegador.
                        </div>
						<form role="form" runat="server" id="login">
							<fieldset>
								<div class="row">
									<div class="col-sm-12 col-md-10  col-md-offset-1 ">
										<div class="form-group">
                                            <input type="button" class="btn btn-lg btn-primary btn-block" value="Login" onclick="logout();"/>
										</div>
									</div>
								</div>
							</fieldset>
						</form>
					</div>
                </div>
			</div>
		</div>
	</div>
    <iframe ID="KeepAliveFrame" src="KeepSessionAlive.aspx" scrolling="no" style="width: 16px; display: none; height: 8px" runat="server"></iframe>
</body>
</html>