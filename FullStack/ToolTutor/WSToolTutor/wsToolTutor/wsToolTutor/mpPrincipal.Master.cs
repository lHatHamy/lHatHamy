using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wsToolTutor
{
    public partial class mpPrincipal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if  (Session["cveUsuario"].ToString() == "" &&
                Session["nomUsuario"].ToString() == "" &&
                Session["numUsuario"].ToString() == "" &&
                Session["tipUsuario"].ToString() =="" )
            {
                Response.Write("<script language ='javascript'>" + "alert('Acceso no autorizado ... inicie sesión'); " +
                                "</script>");

                Response.Write("<script language ='javascript'> " + "document.location.href = 'wpAcceso.aspx';"
                                + "</script>");
            }

            if (Session["tipUsuario"] == "estudiante")
            {
                Label6.Text = Session["nomUsuario"] + " - " +
                              Session["numUsuario"] + " (" +
                              Session["carUsuario"] + ")";
            }
            else
            {
                Label6.Text = "Asesor/a: " + Session["nomUsuario"] + " - " +
                                             Session["numUsuario"];
            }

            if (!IsPostBack)
            {
                string tipoUsuario = Session["tipUsuario"]?.ToString();

                if (tipoUsuario == "estudiante")
                {
                    lnkDocentes.Text = "Docentes";
                    lnkDocentes.PostBackUrl = "~/Docentes.aspx";
                }
                else if (tipoUsuario == "profesor")
                {
                    lnkDocentes.Text = "Alumnos";
                    lnkDocentes.PostBackUrl = "~/Estudiante.aspx";
                }
            }
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            Session["cveUsuario"] = "";
            Session["nomUsuario"] = "";
            Session["tipUsuario"] = "";
            Session["numUsuario"] = "";
            Session["semUsuario"] = "";
            Session["carUsuario"] = "";
            Session["genUsuario"] = "";
            Session["DesUsuario"] = "";

            Response.Write("<script language ='javascript'>" + "alert('Sesión cerrada exitosamente'); " +
                    "</script>");

            Response.Write("<script language ='javascript'> " + "document.location.href = 'wpAcceso.aspx';"
                + "</script>");
        }
    }
}