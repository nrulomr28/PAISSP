<%@ Page Title="Administrador de cuentas de usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PAISSP._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 
    <div class="container">
        <div class="row pt-4">
            <h3 class="text-center titulos pt-4 fw-bold">Administrador de cuentas de usuario</h3>
            <div class="linea-roja"></div>
        </div>

        <div class="row pt-5">
            <div class="col-md-6">
                <a runat="server" ID="alinknet" href="~/Administracion/Sistemas.aspx" >
                <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                    <img runat="server" src="~/Content/img/NET.png" class="card-img-top" alt="..."  style="width:50%;   margin:10px auto; display:block;">
                </div>
                </a>
            </div>
            <div class="col-md-6">
               <a runat="server" ID="a1" href="~/Administracion/usuarios_genexus.aspx" >
                    <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
                        <img runat="server" src="~/Content/img/geneux.png" class="card-img-top" alt="..." style="width:68%; margin:10px auto; display:block;">
                    </div>
                </a>
            </div>

        </div>
    </div>


</asp:Content>
