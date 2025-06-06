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
using wsCtrlUsuario.Models;

namespace wsCtrlUsuario
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private async Task cargaDatosApi()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Contenido para enviarse al endpoint
                    String datos = @"{
                                    ""usuario"":""" + TextBox1.Text + "\"," +
                                    "\"contrasena\":\"" + TextBox2.Text + "\"" +
                                    "}";
                    // Configurar el envío del contenido
                    HttpContent contenido =
                            new StringContent(datos, Encoding.UTF8, "application/json");
                    string urlApi = "https://localhost:44348/full/usuario/spvalidaracceso";
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
                            Session["nomUsuario"] = objRespuesta.datos["usu_nombre_completo"].ToString();
                            Session["usuUsuario"] = objRespuesta.datos["usu_usuario"].ToString();
                            Session["urlUsuario"] = objRespuesta.datos["usu_ruta"].ToString();
                            Session["rolUsuario"] = objRespuesta.datos["tipo_descripcion"].ToString();
                            Response.Write("<script language ='javascript'> alert('Bienvenido: " +  Session["nomUsuario"].ToString()
                                + "'); </script>");

                            Response.Write("<script language ='javascript'> " + "document.location.href = 'Formulario web1.aspx';"
                                + "</script>");
                        }
                        else
                        {
                            Session["nomUsuario"] = "";
                            Session["usuUsuario"] = "";
                            Session["urlUsuario"] = "";
                            Session["rolUsuario"] = "";
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

        // Evento click del botodn acceder
        protected async void Button1_Click(object sender, EventArgs e)
        {
             // Ejecución asincrona del metodo 
            await cargaDatosApi();
        }
    }
}