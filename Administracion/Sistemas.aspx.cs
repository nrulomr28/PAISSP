using AdmonUser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdmonUser.Administracion
{
    public partial class Sistemas : System.Web.UI.Page
    {
        UsuariosEntities ctx = new UsuariosEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnlinkSistemas_Command(object sender, CommandEventArgs e)
        {
            var nombreSistema = e.CommandArgument;
            var validarSistema = ctx.Applications.Where(a => a.ApplicationName == nombreSistema).FirstOrDefault();
            if (validarSistema != null)
            {
                Session["GuidSistema"] = validarSistema.ApplicationId;
                Session["Nombresistena"] = validarSistema.ApplicationName;
                Response.Redirect("~/Administracion/Usuarios.aspx");

            }

        }
    }
}