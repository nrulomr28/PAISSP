<%@ Page Title="Administrador de sistemas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PortalAdministracion.aspx.cs" Inherits="AdmonUser.Administracion.Sistemas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-3">
        <div class="row">
            <h3 class="text-center titulos">Administrador de sistemas</h3>
            <div class="linea-roja"></div>
        </div>
        <div class="row mt-4">
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnlinkSiicop" CommandArgument="SIICOP" OnCommand="btnlinkSistemas_Command">
                <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                    <img runat="server" src="~/Content/img/sistemas/SIICOP.png" class="card-img-top" alt="...">
                </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnlinkLexus" CommandArgument="LEXUZ" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/sistemas/LEXUS.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="LinkButton3" CommandArgument="ConsultaTransporte" OnCommand="btnlinkSistemas_Command">
                <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                    <img runat="server" src="~/Content/img/sistemas/Conces_Transportes.png" class="card-img-top" alt="...">
                </div>
                </asp:LinkButton>
            </div>

        </div>
        <div class="row mt-3">
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnlinkSADA" CommandArgument="Abastecimiento" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/sistemas/SADA.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnlinkRevi" CommandArgument="SISREVI" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/sistemas/ReVi.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnlinkVarafact" CommandArgument="Facturas" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/sistemas/VAREFACT.jpg" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>

        </div>
        <div class="row mt-3">


            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="LinkButton1" CommandArgument="SIGARH.NET" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="http://repositorio.veracruz.gob.mx/seguridad/wp-content/uploads/sites/16/2022/05/SIGA.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>

            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="LinkButton2" CommandArgument="VIGIA" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded text-center">
                        <img runat="server" src="http://repositorio.veracruz.gob.mx/seguridad/wp-content/uploads/sites/16/2022/12/VIGIA.png"  style="width:48%; align-items:center !important" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>

            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="btnlkAdministracionUser" CommandArgument="LOC" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="http://repositorio.veracruz.gob.mx/seguridad/wp-content/uploads/sites/16/2022/05/LicOfColectiva.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>

        </div>

        <div class="row mt-3">

            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="LinkButton4" CommandArgument="CI3" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded text-center">
                        <img runat="server" src="http://repositorio.veracruz.gob.mx/seguridad/wp-content/uploads/sites/16/2022/12/Logos_centinela.png"  style="width:48%; align-items:center !important" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>

            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="LinkButton6" CommandArgument="AdmonUser" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/sistemas/Administración_de_usuarios.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>

            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="LinkButton5" CommandArgument="SISCAP" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/SISCAP_horizontal.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>
        </div>

        
        <div class="row mt-3">
            <div class="col-md-4">
                <asp:LinkButton runat="server" ID="LinkButton9" CommandArgument="Mapas" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/sistemas/MAlerta_SinFondo.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>
                     <div class="col-md-4">
                <asp:LinkButton runat="server" ID="LinkButton7" CommandArgument="Convocatorias" OnCommand="btnlinkSistemas_Command">
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/sistemas/Convocatorias_Policiales.png" class="card-img-top" alt="...">
                    </div>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
