<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PAISSP.Inicio.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="v"></asp:ScriptManager>
        <div class="container">
            <div class="row">
                <div class="wrapper fadeInDown">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Login ID="Login1" runat="server" OnAuthenticate="Login_Authenticate">
                                <LayoutTemplate>
                                    <div id="formContent">
                                        <br />
                                        <!-- Tabs Titles -->
                                        <!-- Icon -->
                                        <div class="fadeIn first">
                                            <img runat="server" src="~/Content/img/sistemas/Administración_de_usuarios.png" style="display: block; width: 50%; margin: 10px auto;" />

                                        </div>
                                        <br />


                                        <!-- Login Form -->
                                        <asp:TextBox runat="server" ID="UserName" class="fadeIn second" placeholder=" Usuario" required autocomplete="off"> </asp:TextBox>
                                        <br />
                                        <asp:TextBox runat="server" ID="Password" CssClass="fadeIn second" placeholder=" Contraseña" TextMode="Password" required autocomplete="off"></asp:TextBox>
                                        <br />
                                        <asp:Button ID="LoginButton" CommandName="Login" runat="server" class="login100-form-btn btn-success" ValidationGroup="Login1" Text="Inicio de sesi&#243;n" />
                                        <!-- Remind Passowrd -->
                                        <div id="formFooter">
                                            <img class="avatar" src="../Content/img/Convivencia_Escudos.png" width="100%" />
                                        </div>
                                    </div>
                                </LayoutTemplate>
                            </asp:Login>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <br />
        <footer class="footer p-2  card" style="background-color: #AA983F;">
            <div class="container-fluid float-md-start">
                <div class="row">
                    <div class="col-sm-12 col-md-12" style="color: white">
                        <p class="align-self-center text-center">Se prohíbe la reproducción total o parcial contenida en este sistema informático sin el consentimiento expreso y por escrito de la Secretaría de Seguridad Pública de Estado de Veracruz. Esta plataforma digital deberá ser utilizada únicamente por el personal autorizado y debidamente certificado de esta Secretaría. Cualquier violación de la integridad del sistema será castigado severamente. </p>
                        <p class="align-self-center text-center">
                            &copy; <%: DateTime.Now.Year %> - GOBIERNO DEL ESTADO DE VERACRUZ / SECRETARÍA DE SEGURIDAD PÚBLICA/ DERECHOS RESERVADOS.
                        </p>
                        <p class="align-self-center text-center">
                            VERACRUZ.GOB.MX
                        </p>
                    </div>
                </div>
            </div>
        </footer>
    </form>
    <script src="<%= ResolveUrl("~/Scripts/sweetalert.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        function error() {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'El usuario y la contraseña no son correctos',
            })
        }

    </script>
</body>
</html>
