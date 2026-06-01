using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAISSP.Inicio
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                if (System.Web.Security.Membership.ValidateUser(Login1.UserName, Login1.Password))
                {
                    FormsAuthentication.SetAuthCookie(Login1.UserName, true);
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "error()", true);

            }
        }
    }
}