using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wsToolTutor.Models;

namespace wsToolTutor
{
    public partial class Chat : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await cargarDatosApi();
            }
        }

        private async Task enviarMensaje()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var mensaje = new
                    {
                        rem_cve = Convert.ToInt32(Session["cveUsuario"]),
                        msg = TextBox1.Text,
                        sala = Convert.ToInt32(Session["desUsuario"]),
                        canal = Convert.ToInt32(DropDownList1.SelectedValue),
                        tipo = Convert.ToInt32(DropDownList2.SelectedValue)
                    };

                    string json = JsonConvert.SerializeObject(mensaje);
                    HttpContent contenido = new StringContent(json, Encoding.UTF8, "application/json");

                    string apiUrl = "https://localhost:44362/api/mensaje/spenviarmensaje";

                    HttpResponseMessage respuesta = await client.PostAsync(apiUrl, contenido);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        clsApiStatus objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script>alert('Mensaje enviado correctamente!');</script>");
                            Response.Redirect("Chat.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('Mensaje NO enviado. Contacte con el administrador');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Error de conexión con el servicio');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                // DEBUG: Te recomiendo loguear el error mientras desarrollas
                Response.Write("<script>alert('Error de la aplicación: " + ex.Message.Replace("'", "") + "');</script>");
            }
        }


        private async Task cargarDatosApi()
        {
            try
            {
                if (Session["desUsuario"] == null)
                {
                    Response.Write("<script>alert('No se ha definido el destinatario.');</script>");
                    return;
                }

                int salaId = Convert.ToInt32(Session["desUsuario"]);  // Usamos el ID guardado previamente

                var payload = new
                {
                    sala = salaId
                };

                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://localhost:44362/api/mensaje/vwsalachat";

                    var json = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage respuesta = await client.PostAsync(apiUrl, content);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string result = await respuesta.Content.ReadAsStringAsync();
                        clsApiStatus apiStatus = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiStatus.datos != null && apiStatus.datos["mensajes"] != null)
                        {
                            var mensajes = apiStatus.datos["mensajes"].ToObject<List<Mensaje>>();
                            rptMensajes.DataSource = mensajes;
                            rptMensajes.DataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('No se encontraron mensajes.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al obtener los mensajes de la sala.');</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error inesperado al cargar la conversación.');</script>");
            }
        }

        protected async void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            await enviarMensaje();
        }
    }
}
