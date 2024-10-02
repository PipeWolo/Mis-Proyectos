<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteError.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="Entel empresas C2C" />
    <title>Click To Call</title>
	<!-- TAG MANAGER -->
    <meta name="google-site-verification" content="rH9aIfWsjZE6-6FLdxDu904e3H6Z_cWmTBPiB7Ep6xo" /><!-- Google Tag Manager -->
    <script>
		(function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':

                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],

                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =

                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);

        })(window, document, 'script', 'dataLayer', 'GTM-P45QN5R');
	</script>
    <!-- End Google Tag Manager -->
    <link href="css/bootstrap.min.css?v=3" rel="stylesheet" />
    <%--<link href='https://fonts.googleapis.com/css?family=Open+Sans:300,600' rel='stylesheet' type='text/css' />--%>
    <link href="https://fonts.googleapis.com/css?family=Barlow&display=swap" rel="stylesheet"> 
    <script src="js/jquery-1.10.2.min.js?v=3"></script>
    <script src="js/bootstrap.min.js?v=3"></script>
    <script src="proxys/proxy_util.min.js?v=3" type="text/javascript"></script>
    <script src="proxys/proxy.min.js?v=3" type="text/javascript"></script>
    <script src="js/main.min.js?v=3" type="text/javascript"></script>
    <link href="css/style.min.css?v=3" rel="stylesheet" />
    <!--[if lt IE 9]>
		<script src="js/html5shiv.js"></script>
		<script src="js/respond.min.js"></script>
	<![endif]-->
    <%--<link rel="shortcut icon" href="favicon-128.ico" />--%>
</head>
<body class="center-form" style="overflow-y:hidden">
    <!-- Google Tag Manager (noscript) -->
    <noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-P45QN5R"
    height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
    <!-- End Google Tag Manager (noscript) -->
    <main id="top" role="main">
        <div class="container">
            <div class="row" id="form-error" >
                <div class="col">
                    <div class="relative-block">
                        <div class="signup-block">
                            <div class="" style="text-align: right;">
                                <%--<a class="btn btn-cerrar float-right" style="line-height: 1em; font-size: 1.5em; color: #453db4; font-weight: bold;">
                                    <img src="./images/CLOSE.png" style="width: 1em;" />&nbsp;Cerrar</a>--%>
                            </div>
                            <div class="logo">
                                <img src="./assets/img/logo/ERROR.png" alt="readymade-logo" />
                            </div>
                            <br />
                            <h3 class="form-title has-margin-bottom-sm text-center h3signup-blockBlue" style="color: #10069F">Ocurrio un error.</h3>
                            <h3 class="form-title has-margin-bottom-sm text-center h3signup-blockGray" id="msg" runat="server">
                            </h3>  
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <script>
        $(function () {
            $('.scrollto, .gototop').bind('click', function (event) {
                var $anchor = $(this);
                $('html, body').stop().animate({
                    scrollTop: $($anchor.attr('href')).offset().top
                }, 1500, 'easeInOutExpo');
                event.preventDefault();
            });
        });

        $(".form").attr('disabled', false);
    </script>
</body>
</html>
