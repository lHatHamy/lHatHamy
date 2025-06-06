using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wsCtrlUsuario
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validación de usuario en sesión (OK)
            if (Session["nomUsuario"].ToString() == "" &&
                Session["usuUsuario"].ToString() == "" &&
                Session["urlUsuario"].ToString() == "" &&
                Session["rolUsuario"].ToString() == "") 
            {   
                Response.Write("<script language ='javascript'>" + "alert('Acceso no autorizado ... inicie sesión'); " +
                "</script>");

                Response.Write("<script language ='javascript'> " + "document.location.href = 'wpAcceso.aspx';"
                    + "</script>");

            }

            // Configuración de la app y la sesión
            Label1.Text = Application["nomEmpresa"].ToString();
            Label6.Text = Session["nomUsuario"].ToString() + "(" +
                Session["usuUsuario"].ToString() + ")" +
                Session["rolUsuario"].ToString();

            // Actualización de imagen de foto del usuario
            Image2.ImageUrl = Session["urlUsuario"].ToString();
            Image2.CssClass = "circular-image"; // Se aplica el nuevo estilo


        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            Session["nomUsuario"] = "";
            Session["usuUsuario"] = "";
            Session["urlUsuario"] = "";
            Session["rolUsuario"] = "";

            Response.Write("<script language ='javascript'>" + "alert('Sesión cerrada exitosamente'); " +
            "</script>");

            Response.Write("<script language ='javascript'> " + "document.location.href = 'wpAcceso.aspx';"
                + "</script>");
        }
    }
}