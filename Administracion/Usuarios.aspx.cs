using AdmonUser.Models;
using AdmonUser.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdmonUser.Administracion
{
    public partial class Usuarios : System.Web.UI.Page
    {
        UsuariosEntities ctx = new UsuariosEntities();
        dbSigaEntities ctxSiga = new dbSigaEntities();
        Guid APLICACION_ID;
        string nombre_sistema;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            if (Session["GuidSistema"] != null && Session["Nombresistena"] != null)
            {
                APLICACION_ID = Guid.Parse(Session["GuidSistema"].ToString());
                nombre_sistema = Convert.ToString(Session["Nombresistena"]);

                llenar_grid_user();
                conteuser();

            }
            else
            {
                Response.Redirect("~/Administracion/Sistemas.aspx");
            }
            //}
        }
        public void conteuser()
        {
            Guid app = Guid.Parse(Session["GuidSistema"].ToString());
            var totaluser = ctx.vw_MemberShip_datos.Where(a => a.ApplicationId == app).Count();
            alluser.InnerText = totaluser.ToString();

            var userbloque = ctx.vw_MemberShip_datos.Where(a => a.ApplicationId == app && a.IsApproved == false).Count();
            denagoUser.InnerText = userbloque.ToString();

            var useraprovado = ctx.vw_MemberShip_datos.Where(a => a.ApplicationId == app && a.IsApproved == true).Count();
            successUser.InnerText = useraprovado.ToString();


        }

        #region llenar grid y buscar usuarios ya registrados en el member
        public void llenar_grid_user()
        {
            string guidapp = Session["GuidSistema"].ToString();
            string busqueda = txtUser.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(busqueda))
            {
                busqueda = "";
            }
            else
            {
                busqueda = txtUser.Text;
            }
            var UsuariosMembership = ctx.sp_MemberShip_Datos(guidapp, busqueda).ToList();
            gvuser.DataSource = UsuariosMembership;
            gvuser.DataBind();
        }

        //protected void txtUser_TextChanged(object sender, EventArgs e)
        //{
        //    llenar_grid_user();
        //    txtUser.Text = null;
        //}
        protected void gvuser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvuser.PageIndex = e.NewPageIndex;
            this.llenar_grid_user();
        }
        #endregion

        #region llenartabal
        public void LlenarGridUserSiga(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                gvSigaUser.DataSource = null;
                gvSigaUser.DataBind();
                gvSigaUser.Visible = false;

            }
            else
            {
                var UserSIGA = ctxSiga.sp_busca_personal_plantilla_siga(user).ToList();
                gvSigaUser.DataSource = UserSIGA;
                gvSigaUser.DataBind();
            }

        }
        #endregion


        #region agregar usuario y buscar usuario de siga
        protected void btnagregarUser_Click(object sender, EventArgs e)
        {
            nombre_sistema = Convert.ToString(Session["Nombresistena"]);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalNuevoUser", "AbrirModalNuevoUser();", true);
            tituloModal.InnerText = "Agregar nuevo usuario al sistema: " + nombre_sistema;
            txtlogins.Text = null;
            chekHabilitado.Checked = false;
            chkBloqueado.Checked = false;
            txtBuscarUserMember.Text = null;
            LlenarGridUserSiga(null);
            btnGuardarUser.Visible = true;
            btnEditar.Visible = false;
            passwordmodulo.Visible = true;
            txtpassworrd.Text = Generador_contrasema();

        }
        protected void gvuser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarContraseña")
            {
                Guid numFila;
                if (Guid.TryParse(e.CommandArgument.ToString(), out numFila))
                {
                    Guid idusermember = Guid.Parse(numFila.ToString());
                    var login = ctx.vw_MemberShip_datos.Where(t => t.UserId == idusermember).FirstOrDefault();
                    txtuserPassword.Text = login.UserName;
                    userpassword.InnerText = "Cambio de contraseña al usuario: " + login.UserName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalContraseña", "AbrirModalContraseña();", true);
                }
            }

            if (e.CommandName == "Editar")
            {
                Guid numFila;
                if (Guid.TryParse(e.CommandArgument.ToString(), out numFila))
                {
                    Guid idusermember = Guid.Parse(numFila.ToString());
                    var login = ctx.vw_MemberShip_datos.Where(t => t.UserId == idusermember).FirstOrDefault();
                    tituloModal.InnerText = "Editar  usuario del sistema: " + login.ApplicationName;
                    txtlogins.Text = login.UserName;

                    txtnombre.Text = login.nombre;
                    txtapePaterno.Text = login.paterno;
                    txtapeMaterno.Text = login.materno;
                    Session["IdUserEdit"] = login.UserId;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalNuevoUser", "AbrirModalNuevoUser();", true);
                    btnGuardarUser.Visible = false;
                    btnEditar.Visible = true;
                    passwordmodulo.Visible = false;
                }
            }

            if (e.CommandName == "Roles")
            {
                Guid numFila;
                if (Guid.TryParse(e.CommandArgument.ToString(), out numFila))
                {
                    Guid idusermember = Guid.Parse(numFila.ToString());
                    var login = ctx.vw_MemberShip_datos.Where(t => t.UserId == idusermember).FirstOrDefault();
                    lbUsuario.Text = login.UserName;
                    Session["GuidSistema"] = login.ApplicationId;
                    BindRolesToList();
                    CheckRolesForSelectedUser();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalRoles", "AbrirModalRoles();", true);
                }
            }


            if (e.CommandName == "Permisos")
            {
                Guid numFila;
                if (Guid.TryParse(e.CommandArgument.ToString(), out numFila))
                {
                    Guid idusermember = Guid.Parse(numFila.ToString());
                    var login = ctx.vw_MemberShip_datos.Where(t => t.UserId == idusermember).FirstOrDefault();
                    txtPermisos.InnerText = "Administrar permiso para el usuario " + login.UserName + " de la aplicación " + login.ApplicationName;
                    lblUser.InnerText = login.UserName;
                    chekHabilitado.Checked = login.IsApproved ? true : false;
                    chkBloqueado.Checked = login.IsLockedOut ? true : false;
                    Session["IdUserPermi"] = login.UserId;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalPermi", "AbrirModalPermi();", true);
                }

            }
            if (e.CommandName == "print")
            {
                Guid numFila;
                if (Guid.TryParse(e.CommandArgument.ToString(), out numFila))
                {
                    Guid idusermember = Guid.Parse(numFila.ToString());
                    var login = ctx.vw_MemberShip_datos.Where(t => t.UserId == idusermember).FirstOrDefault();
                    txtPermisos.InnerText = "Administrar permiso para el usuario " + login.UserName + " de la aplicación " + login.ApplicationName;
                    lblUser.InnerText = login.UserName;

                    generaResguardo(login.UserName, Convert.ToString(login.UserId));
                }

            }


        }
        protected void txtBuscarUserMember_TextChanged(object sender, EventArgs e)
        {
            string user = txtBuscarUserMember.Text;
            LlenarGridUserSiga(user);
            gvSigaUser.Visible = true;
        }
        protected void gvSigaUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int numFila;
                if (int.TryParse(e.CommandArgument.ToString(), out numFila))
                {
                    int iduserSIGA = Convert.ToInt32(numFila.ToString());
                    var login = ctxSiga.vw_plantilla.Where(t => t.PersonalID == iduserSIGA).FirstOrDefault();
                    txtnombre.Text = login.nombre;
                    txtapePaterno.Text = login.paterno;
                    txtapeMaterno.Text = login.materno;
                    Session["idSIGA"] = login.PersonalID;
                    gvSigaUser.Visible = false;
                    txtBuscarUserMember.Text = "";
                }
            }
        }
        #endregion


        #region contraseña
        protected void btnlinkNewPassword_Click(object sender, EventArgs e)
        {
            bool valido = true;
            try
            {

                if (txtNuevacontraseña.Text.Length >= 8)
                {
                    if (string.IsNullOrEmpty(txtNuevacontraseña.Text))
                    {
                        valido = false;
                    }
                    if (!valido)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertaContraseña", "AlertaContraseña();", true);
                    }
                    else
                    {
                        nombre_sistema = Convert.ToString(Session["Nombresistena"]);
                        string username = txtuserPassword.Text;
                        string password = txtNuevacontraseña.Text;

                        System.Web.Security.Membership.ApplicationName = nombre_sistema;
                        //bool isValid = System.Web.Security.Membership.ValidateUser(username, password);
                        MembershipUser mu = Membership.GetUser(username);
                        //mu.LastLoginDate
                        if (mu != null)
                        {
                            mu.IsApproved = true;
                            if (mu.IsLockedOut)
                            {
                                mu.UnlockUser();
                            }
                            mu.ChangePassword(mu.ResetPassword(), password);
                            llenar_grid_user();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalContraseña", "CloseModalContraseña();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "success();", true);
                            CleanControl(this.Controls);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "caracterescontraseña", "caracterescontraseña();", true);

                    // la contraseña no tiene 8 campos
                }

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region roles

        private void BindRolesToList()
        {
            nombre_sistema = Convert.ToString(Session["Nombresistena"]);
            System.Web.Security.Membership.ApplicationName = nombre_sistema;
            APLICACION_ID = Guid.Parse(Session["GuidSistema"].ToString());
            // Get all of the roles 
            //string[] roles = System.Web.Security.Roles.GetAllRoles(APLICACION_ID);
            //var userRoleIds = ctx.Roles.Select(r => r.ApplicationId == APLICACION_ID);
            var roles = ctx.Roles.Where(r => r.ApplicationId == APLICACION_ID).Select(x => x.RoleName).ToArray();
            //string[] roles1 = roles.Select(X => X.RoleName).ToArray();     
            UsersRoleList.DataSource = roles;
            UsersRoleList.DataBind();
        }
        private void CheckRolesForSelectedUser()
        {
            // Determine what roles the selected user belongs to

            string selectedUserName = lbUsuario.Text;
            var selectedUsersRoles = ctx.vw_MemberShip_datos.Where(a => a.UserName == selectedUserName).ToArray();
            string[] r = selectedUsersRoles.Select(t => t.RoleName).ToArray();

            // Loop through the Repeater's Items and check or uncheck the checkbox as needed
            foreach (RepeaterItem ri in UsersRoleList.Items)
            {
                // Programmatically reference the CheckBox
                CheckBox RoleCheckBox = ri.FindControl("RoleCheckBox") as CheckBox;


                if (r.Contains(RoleCheckBox.Text))
                    RoleCheckBox.Checked = true;
                else
                    RoleCheckBox.Checked = false;
            }
        }
        protected void RoleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Reference the CheckBox that raised this event
                CheckBox RoleCheckBox = sender as CheckBox;

                // Get the currently selected user and role
                string selectedUserName = lbUsuario.Text;
                string roleName = RoleCheckBox.Text;
                Guid roleGuid;
                Guid userGuid;
                APLICACION_ID = Guid.Parse(Session["GuidSistema"].ToString());
                // Determine if we need to add or remove the user from this role
                if (RoleCheckBox.Checked)
                {
                    // Add the user to the role

                    var Validar_rol_existe = ctx.Roles.Where(x => x.RoleName == roleName && x.ApplicationId == APLICACION_ID).FirstOrDefault();
                    if (Validar_rol_existe != null)
                    {
                        roleGuid = Validar_rol_existe.RoleId;
                        var validar_user = ctx.vw_MemberShip_datos.Where(t => t.UserName == selectedUserName && t.ApplicationId == APLICACION_ID).FirstOrDefault();
                        if (validar_user != null)
                        {
                            userGuid = validar_user.UserId;
                            UsersInRoles agrgaruseryrol = new UsersInRoles();
                            agrgaruseryrol.RoleId = roleGuid;
                            agrgaruseryrol.UserId = userGuid;
                            ctx.UsersInRoles.Add(agrgaruseryrol);
                            ctx.SaveChanges();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Seagrego_usuario_al_rol", "Seagrego_usuario_al_rol();", true);
                            gvuser.DataBind();
                            llenar_grid_user();
                        }
                    }

                }
                else
                {

                    // remover el rol de la tabala de usuarios
                    var checarRol = ctx.vw_MemberShip_datos.Where(a => a.RoleName == roleName).FirstOrDefault();
                    if (checarRol != null)
                    {
                        roleGuid = (Guid)checarRol.RoleId;
                        userGuid = checarRol.UserId;

                        UsersInRoles customer = ctx.UsersInRoles.Where(t => t.RoleId == roleGuid && t.UserId == userGuid).FirstOrDefault();
                        if (customer != null)
                        {

                            ctx.UsersInRoles.Remove(customer);
                            ctx.SaveChanges();
                            llenar_grid_user();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Sequitoel_usuario_del_rol", "Sequitoel_usuario_del_rol();", true);

                        }
                    }


                }

            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        protected void btnGuardarUser_Click(object sender, EventArgs e)
        {
            bool valido = true;
            string textoValidacion = "<ul>";
            try
            {
                if (chkSiga.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtnombre.Text) || string.IsNullOrEmpty(txtnombre.Text) || string.IsNullOrEmpty(txtnombre.Text))
                    {
                        textoValidacion += "<li>Debe de seleccionar un personal de la plantilla del SIGA</li>";
                        valido = false;
                    }
                    if (Session["idSIGA"] == null)
                    {
                        textoValidacion += "<li>Debe de seleccionar un personal de la plantilla del SIGA</li>";
                        valido = false;
                    }
                }
                if (string.IsNullOrEmpty(txtlogins.Text))
                {
                    textoValidacion += "<li>Es obligatorio poner un usuario</li>";
                    valido = false;
                }
                if (string.IsNullOrEmpty(txtpassworrd.Text))
                {
                    textoValidacion += "<li>Es obligatorio poner la contraseña</li>";
                    valido = false;
                }
                if (txtpassworrd.Text.Length >= 8)
                { }
                else
                {
                    textoValidacion += "<li>La contraseña como minimo debe de tener 8 caracteres</li>";
                    valido = false;
                }


                if (!valido)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "registro_user", "registro_user('" + textoValidacion + "');", true);
                }
                else
                {
                    nombre_sistema = Convert.ToString(Session["Nombresistena"]);
                    System.Web.Security.Membership.ApplicationName = nombre_sistema;
                    int idPersonalSIGA = Convert.ToInt32(Session["idSIGA"]);
                    string password = txtpassworrd.Text;
                    string usuario = txtlogins.Text;
                    Guid idNuevoUsuario;
                    if (!Membership.ValidateUser(usuario, password) && (Membership.FindUsersByName(usuario) == null) || Membership.FindUsersByName(usuario).Count == 0)
                    {
                        tusuarios nuevouser = new tusuarios();
                        MembershipUser usuarioCreado = Membership.CreateUser(usuario, password);
                        string id = usuarioCreado.ProviderUserKey.ToString();
                        idNuevoUsuario = Guid.Parse(usuarioCreado.ProviderUserKey.ToString());
                        if (idPersonalSIGA != 0)
                        {
                            nuevouser.idtpersonal = idPersonalSIGA;
                            nuevouser.UserId = idNuevoUsuario;
                            nuevouser.Nombre = txtnombre.Text.Trim() + " " + txtapePaterno.Text.Trim() + " " + txtapeMaterno.Text.Trim();
                            ctx.tusuarios.Add(nuevouser);

                        }
                        ctx.SaveChanges();
                        gvuser.DataBind();
                        llenar_grid_user();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "success();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalNuevoUser", "CloseModalNuevoUser();", true);
                        CleanControl(this.Controls);
                        Session["idSIGA"] = null;
                        btnGuardarUser.Visible = true;
                        btnEditar.Visible = false;
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_gral", "error_gral();", true);

            }
        }

        protected void chkSiga_CheckedChanged(object sender, EventArgs e)
        {

            if (chkSiga.Checked == true)
            {
                campobusqeudasiga.Visible = true;
            }
            else
            {
                campobusqeudasiga.Visible = false;
                CleanControl(this.Controls);
                Session["idSIGA"] = null;
            }
        }

        public void CleanControl(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                    ((TextBox)control).Text = string.Empty;
                else if (control is DropDownList)
                    ((DropDownList)control).ClearSelection();
                else if (control is RadioButtonList)
                    ((RadioButtonList)control).ClearSelection();
                else if (control is CheckBoxList)
                    ((CheckBoxList)control).ClearSelection();
                else if (control is RadioButton)
                    ((RadioButton)control).Checked = false;
                else if (control is CheckBox)
                    ((CheckBox)control).Checked = false;
                else if (control.HasControls())


                    CleanControl(control.Controls);
            }
        }

        //protected void gvuser_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //if (e.Row.RowType == DataControlRowType.DataRow)
        //    //{
        //    //    try
        //    //    {
        //    //        CheckBox chkaprovado = (CheckBox)e.Row.FindControl("chkaprovado");
        //    //        if (chkaprovado.Checked == true)
        //    //        {
        //    //            //e.Row.Cells[5].Text = "<i class='far fa-check-square' style='font-size:1.6em; margin-top: 4px; color:Green;' aria-hidden='true'></i>";
        //    //            e.Row.Cells[8].Text = "<i class='fas fa-check-circle' style='font-size:1.6em; margin-top: 4px; color:Green;' aria-hidden='true'></i>";
        //    //        }
        //    //        if (chkaprovado.Checked == false)
        //    //        {
        //    //            e.Row.Cells[8].Text = "<i class='fas fa-times-circle' style='font-size:1.6em; margin-top: 4px; color:red;' aria-hidden='true'></i>";
        //    //        }


        //    //        CheckBox chkaIsLockedOut = (CheckBox)e.Row.FindControl("chkaIsLockedOut");
        //    //        if (chkaIsLockedOut.Checked == true)
        //    //        {
        //    //            e.Row.Cells[9].Text = "<i class='fas fa-times-circle' style='font-size:1.6em; margin-top: 4px; color:red;' aria-hidden='true'></i>";

        //    //            //e.Row.Cells[5].Text = "<i class='far fa-check-square' style='font-size:1.6em; margin-top: 4px; color:Green;' aria-hidden='true'></i>";
        //    //        }
        //    //        if (chkaIsLockedOut.Checked == false)
        //    //        {
        //    //            e.Row.Cells[9].Text = "<i class='fas fa-check-circle' style='font-size:1.6em; margin-top: 4px; color:Green;' aria-hidden='true'></i>";

        //    //        }
        //    //    }
        //    //    catch (Exception ex)
        //    //    {

        //    //    }
        //    //}
        //}

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            bool valido = true;
            string textoValidacion = "<ul>";
            try
            {
                if (chkSiga.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtnombre.Text) || string.IsNullOrEmpty(txtnombre.Text) || string.IsNullOrEmpty(txtnombre.Text))
                    {
                        textoValidacion += "<li>Debe de seleccionar un personal de la plantilla del SIGA</li>";
                        valido = false;
                    }
                    if (Session["idSIGA"] == null)
                    {
                        textoValidacion += "<li>Debe de seleccionar un personal de la plantilla del SIGA</li>";
                        valido = false;
                    }
                }
                if (string.IsNullOrEmpty(txtlogins.Text))
                {
                    textoValidacion += "<li>Es obligatorio poner un usuario</li>";
                    valido = false;
                }



                if (!valido)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "registro_user", "registro_user('" + textoValidacion + "');", true);
                }
                else
                {
                    Guid iduseredit = (Guid)Session["IdUserEdit"];
                    nombre_sistema = Convert.ToString(Session["Nombresistena"]);
                    int idsiga = Convert.ToInt32(Session["idSIGA"]);
                    System.Web.Security.Membership.ApplicationName = nombre_sistema;
                    var editUser = ctx.vw_MemberShip_datos.Where(q => q.UserId == iduseredit).FirstOrDefault();
                    if (editUser != null)
                    {
                        var edituser = ctx.Users.Where(t => t.UserId == iduseredit).FirstOrDefault();
                        editUser.UserName = txtlogins.Text;
                        string username = txtlogins.Text;


                        //MembershipUser usr = Membership.GetUser(username);
                        //usr.IsApproved = chekHabilitado.Checked;
                        //if (chkBloqueado.Checked == true)
                        //{
                        //    usr.UnlockUser();
                        //}
                        //Membership.UpdateUser(usr);
                        //var editarAuth=  ctx.Memberships.Where(q => q.UserId == iduseredit).FirstOrDefault();
                        //if(editarAuth != null)
                        //{
                        //    //if (chekHabilitado.Checked == true)
                        //    //{
                        //       editarAuth.IsApproved = chekHabilitado.Checked == true ? true: false;
                        //    //}
                        //    //else
                        //    //{
                        //    //    editarAuth.IsApproved = false;
                        //    //}
                        //    //editarAuth.IsLockedOut = chkBloqueado.Checked == true ? true : false;
                        //    ctx.SaveChanges();
                        //}


                        var personal = ctx.tusuarios.Where(m => m.UserId == iduseredit).FirstOrDefault();
                        if (personal != null)
                        {
                            personal.idtpersonal = idsiga;
                        }
                        else
                        {
                            if (idsiga != 0)
                            {
                                tusuarios tusuarios = new tusuarios();
                                tusuarios.idtpersonal = idsiga;
                                tusuarios.UserId = iduseredit;
                                ctx.tusuarios.Add(tusuarios);
                            }
                        }

                        ctx.SaveChanges();
                        llenar_grid_user();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "success();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalNuevoUser", "CloseModalNuevoUser();", true);
                        CleanControl(this.Controls);
                        gvuser.DataBind();
                        Session["IdUserEdit"] = null;
                        Session["idSIGA"] = null;
                        btnGuardarUser.Visible = true;
                        btnEditar.Visible = false;
                    }



                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_gral", "error_gral();", true);

            }
        }


        #region boton para cerrar modales
        protected void btncerrarModalUser_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalNuevoUser", "CloseModalNuevoUser();", true);
            CleanControl(this.Controls);
            llenar_grid_user();
        }
        protected void btnModalRoles_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalroles", "CloseModalroles();", true);
            CleanControl(this.Controls);
            llenar_grid_user();
        }
        protected void btnModalContraseña_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalContraseña", "CloseModalContraseña();", true);
            CleanControl(this.Controls);
            llenar_grid_user();
        }


        #endregion

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        protected void btnPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                string IdUserPermi = Convert.ToString(Session["IdUserPermi"]).ToString();

                bool habi = chekHabilitado.Checked == true ? true : false;
                bool bloque = chkBloqueado.Checked == true ? true : false;
                var permisos = ctx.upd_bloqueo_membership(IdUserPermi, bloque, habi);
                Session["IdUserPermi"] = null;
                llenar_grid_user();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalPermi", "CloseModalPermi();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "success();", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnPermisosSalir_Click(object sender, EventArgs e)
        {
            Session["IdUserPermi"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalPermi", "CloseModalPermi();", true);

        }

        public static string Generador_contrasema()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 10;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }

            return contraseniaAleatoria;
        }


        public void generaResguardo(string usures, string guid)
        {
            try
            {

            string nombreres = "";
            string pwdres = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var buscaruser = ctx.sel_res_usuarios(usures, guid).ToList();
            nombreres = usures;
            pwdres =  buscaruser[0].Password.ToString();
            MemoryStream ms = new MemoryStream();
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 50f, 40f);
                //PdfWriter pw = PdfWriter.GetInstance(doc, Response.OutputStream);
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("NombreDeTuArchivo.pdf", FileMode.Create, FileAccess.Write, FileShare.None));
                PdfWriter writer = PdfWriter.GetInstance(doc,
                     new FileStream(@"C:\PDF\Formato_electronico_FPOB_pdfa-3.pdf", FileMode.Create));



                doc.AddTitle("Resguardo de cuenta de Usuario");
            doc.AddCreator("Tecnologias de la Información SSP");

            doc.Open();

            //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:\Users\jayalab\Documents\Convivencia_Escudos-01.png");
            //imagen.BorderWidth = 0;
            //imagen.Alignment = Element.ALIGN_CENTER;
            //float percentage = 0.0f;
            //percentage = 500 / imagen.Width;
            //imagen.ScalePercent(percentage * 100);

            //doc.Add(imagen);

            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            Paragraph titulo = new Paragraph("RESGUARDO DE CUENTA DE USUARIO", _standardFont);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);

            Paragraph sistema = new Paragraph(ConfigurationManager.AppSettings["sistema"], _standardFont);
            sistema.Alignment = Element.ALIGN_CENTER;
            doc.Add(sistema);
            doc.Add(Chunk.NEWLINE);

            iTextSharp.text.Font cuerpo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font firmasfuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            Paragraph fecha = new Paragraph("FECHA: " + DateTime.Now.ToString(), cuerpo);
            fecha.Alignment = Element.ALIGN_RIGHT;
            doc.Add(fecha);
            doc.Add(Chunk.NEWLINE);
            // Se crean las tablas (en este caso 3)

            PdfPTable tblvalores = new PdfPTable(2);
            tblvalores.WidthPercentage = 100;
            var colWidthPercentages = new[] { 18f, 70f };
            tblvalores.SetWidths(colWidthPercentages);

            // Se configura el título de las columnas de la tabla
            PdfPCell nombre = new PdfPCell(new Phrase("Nombre", cuerpo));
            nombre.BorderWidth = 1;
            nombre.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell valornombre = new PdfPCell(new Phrase(nombreres, cuerpo));
            valornombre.BorderWidth = 1;
            valornombre.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell area = new PdfPCell(new Phrase("Area", cuerpo));
            area.BorderWidth = 1;
            area.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell valorarea = new PdfPCell(new Phrase(ConfigurationManager.AppSettings["arearesguardo"], cuerpo));
            valorarea.BorderWidth = 1;
            valorarea.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell usuario = new PdfPCell(new Phrase("Usuario", cuerpo));
            usuario.BorderWidth = 1;
            usuario.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell valorusuario = new PdfPCell(new Phrase(usures, cuerpo));
            valorusuario.BorderWidth = 1;
            valorusuario.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell contra = new PdfPCell(new Phrase("Contraseña", cuerpo));
            contra.BorderWidth = 1;
            contra.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell valorcontra = new PdfPCell(new Phrase(pwdres, cuerpo));
            valorcontra.BorderWidth = 1;
            valorcontra.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell liga = new PdfPCell(new Phrase("Liga para acceder", cuerpo));
            liga.BorderWidth = 1;
            liga.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell valorliga = new PdfPCell(new Phrase(ConfigurationManager.AppSettings["liga"], cuerpo));
            valorliga.BorderWidth = 1;
            valorliga.BorderColor = new BaseColor(205, 205, 205);

            // se agrega las celdas a la tabla
            tblvalores.AddCell(nombre);
            tblvalores.AddCell(valornombre);
            tblvalores.AddCell(area);
            tblvalores.AddCell(valorarea);
            tblvalores.AddCell(usuario);
            tblvalores.AddCell(valorusuario);
            tblvalores.AddCell(contra);
            tblvalores.AddCell(valorcontra);
            tblvalores.AddCell(liga);
            tblvalores.AddCell(valorliga);

            doc.Add(tblvalores);
            doc.Add(Chunk.NEWLINE);
            PdfPTable firmas = new PdfPTable(3);
            firmas.WidthPercentage = 100;
            var firmasPercentages = new[] { 40f, 20f, 40f };
            firmas.SetWidths(firmasPercentages);

            PdfPCell entrega = new PdfPCell(new Phrase("ENTREGA", firmasfuente));
            entrega.BorderWidth = 0;
            entrega.HorizontalAlignment = Element.ALIGN_CENTER;
            entrega.FixedHeight = 30f;

            PdfPCell espacio = new PdfPCell(new Phrase("", firmasfuente));
            espacio.BorderWidth = 0;
            espacio.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell recibe = new PdfPCell(new Phrase("RECIBE", firmasfuente));
            recibe.BorderWidth = 0;
            recibe.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell nombreentrega = new PdfPCell(new Phrase("L.I. RAÚL MESTIZO RAMÍREZ", cuerpo));
            nombreentrega.BorderWidth = 1;
            nombreentrega.Border = Rectangle.TOP_BORDER;
            nombreentrega.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell espacio2 = new PdfPCell(new Phrase("", firmasfuente));
            espacio2.BorderWidth = 0;
            espacio2.Border = Rectangle.TOP_BORDER;
            espacio2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell nombrerecibe = new PdfPCell(new Phrase(nombreres, cuerpo));
            nombrerecibe.BorderWidth = 1;
            nombrerecibe.Border = Rectangle.TOP_BORDER;
            nombrerecibe.HorizontalAlignment = Element.ALIGN_CENTER;
            firmas.AddCell(entrega);
            firmas.AddCell(espacio);
            firmas.AddCell(recibe);
            firmas.AddCell(nombreentrega);
            firmas.AddCell(espacio2);
            firmas.AddCell(nombrerecibe);

            doc.Add(firmas);
            //doc.Add(imagen);
            doc.Add(titulo);
            doc.Add(sistema);
            doc.Add(Chunk.NEWLINE);
            doc.Add(fecha);
            doc.Add(Chunk.NEWLINE);
            //

            tblvalores.DeleteRow(3);

            PdfPCell contran = new PdfPCell(new Phrase("Contraseña", cuerpo));
            contran.BorderWidth = 1;
            contran.BorderColor = new BaseColor(205, 205, 205);

            PdfPCell valorcontran = new PdfPCell(new Phrase("********", cuerpo));
            valorcontran.BorderWidth = 1;
            valorcontran.BorderColor = new BaseColor(205, 205, 205);
            tblvalores.AddCell(contran);
            tblvalores.AddCell(valorcontran);

            doc.Add(tblvalores);
            doc.Add(Chunk.NEWLINE);
            doc.Add(firmas);
            doc.Close();

            byte[] bystesStream = ms.ToArray();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Listado_de_Creditos.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(doc);
            Response.End();


            }
            catch (Exception ex)
            {

            }
        }


    }
}