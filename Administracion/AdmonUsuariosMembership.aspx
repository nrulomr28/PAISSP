<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmonUsuariosMembership.aspx.cs" Inherits="PAISSP.Administracion.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-3">
        <div class="row">
            <h3 class="text-center titulos">Administración de usuarios</h3>
            <div class="linea-roja"></div>
        </div>


    </div>
    <div class="container-fluid">
        <div class=" mt-4 shadow-lg p-3 mb-5 bg-body rounded">
            <div class="row">
                <div class="d-flex bd-highlight pt-3">
                    <div class="p-2 flex-grow-1 bd-highlight">
                        <%--    <div class="input-wrapper">
                            <asp:TextBox runat="server" ID="txtUser" CssClass="form-control input form-control-1" placeholder="Usuario" OnTextChanged="txtUser_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <svg xmlns="http://www.w3.org/2000/svg" class="input-icon" viewBox="0 0 20 20" fill="currentColor">
                                <path fill-rule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z" clip-rule="evenodd" />
                            </svg>

                        </div>--%>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                            <ContentTemplate>
                                <div class="input-group mb-3">
                                    <asp:TextBox runat="server" ID="txtUser" CssClass="form-control input form-control-1" placeholder="Usuario"></asp:TextBox>
                                    <asp:LinkButton runat="server" ID="btnBuscar" OnClick="btnBuscar_Click"><svg xmlns="http://www.w3.org/2000/svg" class="input-icon" viewBox="0 0 20 20" fill="currentColor">
                                <path fill-rule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z" clip-rule="evenodd" />
                            </svg></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="p-2 bd-highlight">
                        <asp:UpdatePanel runat="server" ID="btnagregar">
                            <ContentTemplate>
                                <asp:LinkButton runat="server" ID="btnagregarUser" CssClass="btn btn-marron" OnClick="BtnagregarUser_Click">Agregar usuario<i class="fa-solid fa-user-plus"></i></asp:LinkButton>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="p-2 bd-highlight">
                        <h4><i class="fas fa-user"></i><span class="badge bg-secondary" runat="server" id="alluser">0</span></h4>
                    </div>
                    <div class="p-2 bd-highlight">
                        <h4><i class=" fas fa-user-check" style="color: forestgreen"></i><span class="badge bg-success" runat="server" id="successUser">0</span></h4>
                    </div>
                    <div class="p-2 bd-highlight">
                        <h4><i class="fas fa-user-times" style="color: red"></i><span class="badge bg-danger" runat="server" id="denagoUser">0</span></h4>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive-md">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <asp:GridView runat="server" ID="gvuser" AutoGenerateColumns="False"
                                CssClass="table table-sm table-borderless table-bordered table-hover"
                                DataKeyNames="ApplicationId,UserId"
                                PagerSettings-PageButtonCount="5" PageSize="20"
                                OnPageIndexChanging="gvuser_PageIndexChanging"
                                CellPadding="3" AllowPaging="True"
                                OnRowCommand="gvuser_RowCommand">

                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Num" HeaderStyle-CssClass="TableHeader" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ApplicationName" HeaderText="Aplicación" ReadOnly="True" HeaderStyle-CssClass="TableHeader"></asp:BoundField>
                                    <asp:BoundField DataField="nombrecompleto" HeaderText="Personal" ReadOnly="True" HeaderStyle-CssClass="TableHeader"></asp:BoundField>
                                    <asp:BoundField DataField="ct_ubicacion" HeaderText="Ubicacón" ReadOnly="True" HeaderStyle-CssClass="TableHeader"></asp:BoundField>
                                    <asp:BoundField DataField="ub_fisicaactual" HeaderText="Área" ReadOnly="True" HeaderStyle-CssClass="TableHeader"></asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="Usuario" ReadOnly="True" HeaderStyle-CssClass="TableHeader"></asp:BoundField>
                                    <asp:BoundField DataField="RoleName" HeaderText="Rol" ReadOnly="True" HeaderStyle-CssClass="TableHeader"></asp:BoundField>
                                    <asp:BoundField DataField="Email" HeaderText="Correo" HeaderStyle-CssClass="TableHeader"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Aprobado" SortExpression="IsApproved">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" Checked='<%# Bind("IsApproved") %>' ID="chkaprovado" Enabled="false"></asp:CheckBox>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="TableHeader"></HeaderStyle>
                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bloqueado pw" SortExpression="IsLockedOut">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" Checked='<%# Bind("IsLockedOut") %>' ID="chkaIsLockedOut" Enabled="false"></asp:CheckBox>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="TableHeader"></HeaderStyle>
                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                    </asp:TemplateField>
                                    <%-- <asp:CheckBoxField DataField="IsApproved" HeaderText="Approved?" />
                                    <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Locked Out?" />--%>
                                    <asp:TemplateField ShowHeader="False" HeaderStyle-CssClass="TableHeader text-center" HeaderText="Acciones">
                                        <ItemTemplate>
                                            <div class="row justify-content-center p-2 mx-auto">
                                                <div class="col">
                                                    <asp:LinkButton runat="server" Text="Editar" CssClass="btn btn-warning btn-sm px-2" CommandName="EditarContraseña" CausesValidation="False" ID="LinkButton1" ToolTip="Editar Contraseña" CommandArgument='<%# Eval("UserId") %>'>
                                    <i class="fas fa-key" style="color:white"></i>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col">
                                                    <asp:LinkButton runat="server" Text="Roles" CssClass="btn btn-info btn-sm" CommandName="Roles" CausesValidation="False" ID="LinkButton2" ToolTip="Roles" CommandArgument='<%# Eval("UserId") %>'>
                                  <i class="fas fa-network-wired"></i>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col">
                                                    <asp:LinkButton runat="server" Text="Editar" CssClass="btn btn-primary btn-sm" CommandName="Editar" CausesValidation="False" ID="lkbEditar" ToolTip="Editar" CommandArgument='<%# Eval("UserId") %>'>
                                    <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col">
                                                    <asp:LinkButton runat="server" Text="Permisos" CssClass="btn btn-danger btn-sm" CommandName="Permisos" CausesValidation="False" ID="LinkButton3" ToolTip="Permisos" CommandArgument='<%# Eval("UserId") %>'>
                                    <i class="fas fa-user-lock"></i>
                                                    </asp:LinkButton>
                                                </div>


                                                <div class="col">
                                                    <asp:LinkButton runat="server" Text="print" CssClass="btn btn-success btn-sm" CommandName="print" CausesValidation="False" ID="LinkButton4" ToolTip="Imprimir" CommandArgument='<%# Eval("UserId") %>'>
                                   <i class="fas fa-print"></i>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>




    <%-- modal de crear usuarios --%>
    <!-- Modal -->
    <div class="modal fade" id="ModalNuevoUser" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel runat="server" ID="dd">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title text-center titulos" id="tituloModal" runat="server">N/A
                            </h5>
                        </div>
                        <div class="modal-body">
                            <div class="row g-2 P-2">
                                <div class="col-md">
                                    <div class="form-check">
                                        <asp:CheckBox runat="server" ID="chkSiga" class="form-check-input" OnCheckedChanged="chkSiga_CheckedChanged" AutoPostBack="true" />
                                        <label class="form-check-label" for="flexCheckIndeterminate">
                                            Asignar usuario del SIGA
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row g-2" visible="false" runat="server" id="campobusqeudasiga">
                                <div class="d-flex bd-highlight pt-3">
                                    <div class="p-2 flex-grow-1 bd-highlight">
                                        <div class="input-wrapper">
                                            <asp:TextBox runat="server" ID="txtBuscarUserMember" CssClass="form-control input form-control-1" placeholder="bucar usuario"
                                                OnTextChanged="txtBuscarUserMember_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <svg xmlns="http://www.w3.org/2000/svg" class="input-icon" viewBox="0 0 20 20" fill="currentColor">
                                                <path fill-rule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z" clip-rule="evenodd" />
                                            </svg>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView runat="server" ID="gvSigaUser" AutoGenerateColumns="False" DataKeyNames="PersonalID"
                                    Visible="false" CssClass="table table-small table-bordered"
                                    OnRowCommand="gvSigaUser_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nombre" HeaderStyle-CssClass="TableHeader text-center">

                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Bind("nombre") %>' ID="Label2"></asp:Label>
                                                <asp:Label runat="server" Text='<%# Bind("paterno") %>' ID="Label1"></asp:Label>
                                                <asp:Label runat="server" Text='<%# Bind("materno") %>' ID="Label3"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ct_ubicacion" HeaderText="Ubicación" ReadOnly="True" HeaderStyle-CssClass="TableHeader text-center"></asp:BoundField>
                                        <asp:BoundField DataField="ub_fisicaactual" HeaderText="Departamento" ReadOnly="True" HeaderStyle-CssClass="TableHeader text-center"></asp:BoundField>
                                        <asp:TemplateField ShowHeader="False" HeaderStyle-CssClass="TableHeader text-center" HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" Text="Select" CssClass="btn btn-vino-o btn-sm" CommandName="Select" CausesValidation="False" ID="lkbSelect" ToolTip="Select" CommandArgument='<%# Eval("PersonalID") %>'>
                                  <i class="fas fa-hand-pointer"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row g-2 pt-2">
                                <div class="col-md">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="txtnombre" class="form-control" placeholder="Nombre "></asp:TextBox>
                                        <label for="floatingInputGrid">Nombre <i class="fas fa-user"></i></label>
                                    </div>
                                </div>
                                <div class="col-md">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="txtapePaterno" class="form-control" placeholder="Apellido paterno "></asp:TextBox>
                                        <label for="floatingInputGrid">Apellido paterno <i class="fas fa-user"></i></label>
                                    </div>
                                </div>
                                <div class="col-md">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="txtapeMaterno" class="form-control" placeholder="Apellido materno "></asp:TextBox>
                                        <label for="floatingInputGrid">Apellido materno <i class="fas fa-user"></i></label>
                                    </div>
                                </div>
                            </div>
                            <div class="row g-2 pt-1">
                                <div class="col-md">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="txtlogins" class="form-control" placeholder="Contraseña "></asp:TextBox>
                                        <label for="floatingInputGrid">Usuario <i class="fas fa-fingerprint"></i></label>
                                    </div>
                                </div>
                                <div class="col-md" runat="server" id="passwordmodulo" visible="false">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="txtpassworrd" class="form-control" placeholder="Contraseña "></asp:TextBox>
                                        <label for="floatingInputGrid">Contraseña <i class="fas fa-lock"></i></label>
                                    </div>
                                </div>


                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btncerrarModalUser" CssClass="btn btn-danger btn-small" OnClick="btncerrarModalUser_Click" Text="Cerrar" Visible="true" />
                            <asp:Button runat="server" ID="btnGuardarUser" CssClass="btn btn-success btn-small" OnClick="btnGuardarUser_Click" Text="Guardar" Visible="false" />
                            <asp:Button runat="server" ID="btnEditar" CssClass="btn btn-warning btn-small" OnClick="btnEditar_Click" Text="Editar" Visible="false" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <%--Modal roles   --%>
    <div class="modal fade" id="ModalRoles" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">

            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="pt-1 text-center titulos">Tipo de roles</h5>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                        <ContentTemplate>
                            <div class="row g-2 pt-2">
                                <h5 class="titulos">Usuario: 
                                        <asp:Label runat="server" ID="lbUsuario"></asp:Label></h5>
                                <asp:Repeater ID="UsersRoleList" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="true" Text='<%# Container.DataItem %>'
                                                OnCheckedChanged="RoleCheckBox_CheckedChanged" />
                                            <br />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnModalRoles" CssClass="btn btn-danger btn-small" OnClick="btnModalRoles_Click" Text="Cerrar" Visible="true" />
                </div>
            </div>


        </div>
    </div>


    <%-- modal contraseña --%>
    <div class="modal fade" id="ModalContraseña" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h6 id="userpassword" class="text-center titulos" runat="server">N/A</h6>
                        </div>
                        <div class="modal-body">
                            <div class="row g-2">
                                <div class="d-flex bd-highlight pt-3">
                                    <div class="p-2 flex-grow-1 bd-highlight">
                                        <asp:TextBox runat="server" ID="txtuserPassword" CssClass="form-control" placeholder="Usuario " Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row g-2">
                                <div class="d-flex bd-highlight pt-3">
                                    <div class="p-2 flex-grow-1 bd-highlight">
                                        <asp:TextBox runat="server" ID="txtNuevacontraseña" CssClass="form-control" placeholder="Contraseña "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnModalContraseña" CssClass="btn btn-danger btn-small" OnClick="btnModalContraseña_Click" Text="Cerrar" Visible="true" />
                            <asp:LinkButton runat="server" ID="btnlinkNewPassword" OnClick="btnlinkNewPassword_Click" CssClass="btn btn-warning">Editar </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>


    <%-- modal contraseña --%>
    <div class="modal fade" id="ModalPermisos" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h6 id="txtPermisos" class="text-center titulos" runat="server">N/A</h6>
                        </div>
                        <div class="modal-body">
                            <div class="row g-2">
                                <div class="d-flex bd-highlight pt-3">
                                    <div class="p-2 flex-grow-1 bd-highlight">
                                        <h6 id="lblUser" class="text-center titulos" runat="server">N/A</h6>
                                    </div>
                                </div>
                            </div>
                            <div class="row g-2">
                                <div class="col-md">

                                    <div class="row g-2 pt-2">
                                        <div class="col-md">
                                            <div class="form-check">
                                                <asp:CheckBox runat="server" ID="chekHabilitado" class="form-check-input" />
                                                <label class="form-check-label" for="flexCheckIndeterminate">
                                                    Habilitado
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md">
                                            <div class="form-check">
                                                <asp:CheckBox runat="server" ID="chkBloqueado" class="form-check-input" />
                                                <label class="form-check-label" for="flexCheckIndeterminate">
                                                    Bloqueado
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnPermisosSalir" CssClass="btn btn-danger btn-small" OnClick="btnPermisosSalir_Click" Text="Salir" />
                            <asp:LinkButton runat="server" ID="lkbtnpPermisos" OnClick="btnPermisos_Click" CssClass="btn btn-success">Guardar </asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <script type="text/javascript">
        function AbrirModalNuevoUser() {
            var myModal = new bootstrap.Modal(document.getElementById('ModalNuevoUser'), {
                keyboard: false
            })
            myModal.show();
        };


        function AbrirModalContraseña() {
            var myModal = new bootstrap.Modal(document.getElementById('ModalContraseña'), {
                keyboard: false
            })
            myModal.show();
        };

        function AbrirModalRoles() {
            var myModal = new bootstrap.Modal(document.getElementById('ModalRoles'), {
                keyboard: false
            })
            myModal.show();
        };

        function AbrirModalPermi() {
            var myModal = new bootstrap.Modal(document.getElementById('ModalPermisos'), {
                keyboard: false
            })
            myModal.show();
        };

        function CloseModalPermi() {
            $('#ModalPermisos').modal('hide');

        };

        function CloseModalroles() {
            $('#ModalRoles').modal('hide');
            //var myModalclose = document.getElementById('#ModalContraseña');
            //myModalclose.hide();
        };
        function CloseModalContraseña() {
            $('#ModalContraseña').modal('hide');

        };

        function CloseModalNuevoUser() {
            $('#ModalNuevoUser').modal('hide');

        };
    </script>
</asp:Content>
