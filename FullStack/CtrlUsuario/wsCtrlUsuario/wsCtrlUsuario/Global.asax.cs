using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace wsCtrlUsuario
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["nomEmpresa"] = "e-Commerce ITP App";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["nomUsuario"] = "";
            Session["usuUsuario"] = "";
            Session["urlUsuario"] = "";
            Session["rolUsuario"] = "";

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}