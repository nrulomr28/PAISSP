using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAISSP
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {          
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
            FormsAuthentication.SignOut();
            //btnLogout.Visible = false;
            Response.Redirect("~/Inicio/Login.aspx");
        }
    }
}