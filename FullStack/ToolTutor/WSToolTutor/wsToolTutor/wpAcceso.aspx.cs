using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// -------------

using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using wsToolTutor.Models;

namespace wsToolTutor
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected async void btnEstudiante_Click(object sender, EventArgs e)
        {
            await cargaDatosApiEst();
        }

        protected async void btnProfesor_Click(object sender, EventArgs e)
        {
            await cargaDatosApiProf();
        }

        private async Task cargaDatosApiProf()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Contenido para enviarse al endpoint
                    String datos = @"{
                                    ""p_matricula"":""" + TextBox1.Text + "\"," +
                                    "\"p_contrasena\":\"" + TextBox2.Text + "\"" +
                                    "}";
                    // Configurar el envío del contenido
                    HttpContent contenido =
                            new StringContent(datos, Encoding.UTF8, "application/json");
                    string urlApi = "https://localhost:44362/api/profesor/spvalidaraccesoprofesor";
                    // Ejecución del endpoint
                    HttpResponseMessage respuesta =
                            await client.PostAsync(urlApi, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // Se debe importar el modelo de salida clsApiStatus!
                    // ---------------------------------------------------
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                        await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        if (objRespuesta.ban == 1)
                        {
                            //Response.write("Bienvenido!!")
                            Session["cveUsuario"] = objRespuesta.datos["clave_usuario"].ToString();
                            Session["nomUsuario"] = objRespuesta.datos["nombre_completo"].ToString();
                            Session["numUsuario"] = objRespuesta.datos["matricula"].ToString();
                            Session["tipUsuario"] = "profesor";
                            //Session["semUsuario"] = objRespuesta.datos["semestre"].ToString();
                            //Session["carUsuario"] = objRespuesta.datos["carrera"].ToString();
                            //Session["genUsuario"] = objRespuesta.datos["generacion"].ToString();
                            Response.Write("<script language ='javascript'> alert('Bienvenido: " + Session["nomUsuario"].ToString()
                                + "'); </script>");

                            Response.Write("<script language ='javascript'> " + "document.location.href = 'SalasChat.aspx';"
                                + "</script>");
                        }
                        else
                        {
                            limpiarSesiones();
                            Response.Write("<script language ='javascript'> alert('¡ERROR! ESTÁ MAL ... '); </script>");
                        }


                    }
                    else
                    {
                        Response.Write("<script language ='javascript'> alert('Error de conexión con el servicio'); </script>");
                    }

                }

            }
            catch (Exception ex)
            {
                Response.Write("<script language ='javascript'> alert('Algo sucedió internamente, contacte al administrador'); </script>");
            }

        }


        private async Task cargaDatosApiEst()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Contenido para enviarse al endpoint
                    String datos = @"{
                                    ""e_num_control"":""" + TextBox1.Text + "\"," +
                                    "\"e_contrasena\":\"" + TextBox2.Text + "\"" +
                                    "}";
                    // Configurar el envío del contenido
                    HttpContent contenido =
                            new StringContent(datos, Encoding.UTF8, "application/json");
                    string urlApi = "https://localhost:44362/api/estudiante/spvalidaraccesoestudiante";
                    // Ejecución del endpoint
                    HttpResponseMessage respuesta =
                            await client.PostAsync(urlApi, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // Se debe importar el modelo de salida clsApiStatus!
                    // ---------------------------------------------------
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                        await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        if (objRespuesta.ban == 1)
                        {
                            //Response.write("Bienvenido!!")
                            Session["cveUsuario"] = objRespuesta.datos["clave_usuario"].ToString();
                            Session["nomUsuario"] = objRespuesta.datos["nombre_completo"].ToString();
                            Session["numUsuario"] = objRespuesta.datos["matricula"].ToString();
                            Session["semUsuario"] = objRespuesta.datos["semestre"].ToString();
                            Session["carUsuario"] = objRespuesta.datos["carrera"].ToString();
                            Session["genUsuario"] = objRespuesta.datos["generacion"].ToString();
                            Session["tipUsuario"] = "estudiante";
                            Response.Write("<script language ='javascript'> alert('Bienvenido: " + Session["nomUsuario"].ToString()
                                + "'); </script>");

                            Response.Write("<script language ='javascript'> " + "document.location.href = 'Docentes.aspx';"
                                + "</script>");
                        }
                        else
                        {
                            limpiarSesiones();
                            Response.Write("<script language ='javascript'> alert('¡ERROR! ESTÁ MAL ... '); </script>");
                        }


                    }
                    else
                    {
                        Response.Write("<script language ='javascript'> alert('Error de conexión con el servicio'); </script>");
                    }

                }

            }
            catch (Exception ex)
            {
                Response.Write("<script language ='javascript'> alert('Algo sucedió internamente, contacte al administrador'); </script>");
            }

        }

        private void limpiarSesiones()
        {
            Session["nomUsuario"] = "";
            Session["tipUsuario"] = "";
            Session["numUsuario"] = "";
            Session["semUsuario"] = "";
            Session["carUsuario"] = "";
            Session["genUsuario"] = "";
        }

    }
}