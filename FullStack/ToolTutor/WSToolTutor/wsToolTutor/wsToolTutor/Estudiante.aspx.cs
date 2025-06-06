using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wsToolTutor.Models;

namespace wsToolTutor
{
    public partial class Estudiantes : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await CargarEstudiantes();
            }
        }

        private async Task CargarEstudiantes(string searchTerm = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:44362/api/profesor/vwrptestudiante?";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string resultado = await response.Content.ReadAsStringAsync();

                        clsApiStatus objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        if (objRespuesta.datos != null && objRespuesta.datos["vwRptEstudiante"] != null)
                        {
                            JArray jsonArray = (JArray)objRespuesta.datos["vwRptEstudiante"];
                            List<Estudiante> lista = jsonArray.ToObject<List<Estudiante>>();

                            rptEstudiantes.DataSource = lista;
                            rptEstudiantes.DataBind();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al conectar con la API de estudiantes');</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error inesperado al cargar estudiantes');</script>");
            }
        }

        protected async void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            string termino = TextBox1.Text.Trim();
            await CargarEstudiantes(termino);
        }

        protected async void rptEstudiantes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerPerfil")
            {
                string usuario = e.CommandArgument.ToString();
                Response.Redirect($"PerfilEstudiante.aspx?usuario={usuario}");
            }

            if (e.CommandName == "IniciarChat")
            {
                // 1. Obtener rem_cve desde sesión
                if (Session["cveUsuario"] == null)
                {
                    Response.Write("<script>alert('Debe iniciar sesión primero.');</script>");
                    return;
                }
                int remCve;
                if (!int.TryParse(Session["cveUsuario"].ToString(), out remCve))
                {
                    Response.Write("<script>alert('Error en identificación de usuario.');</script>");
                    return;
                }

                // 2. Obtener des_cve del CommandArgument
                int desCve;
                if (!int.TryParse(e.CommandArgument.ToString(), out desCve))
                {
                    Response.Write("<script>alert('Error en identificación del docente.');</script>");
                    return;
                }

                Session["desUsuario"] = desCve;

                // 3. Preparar payload
                var payload = new
                {
                    rem_cve = remCve,
                    des_cve = desCve
                };
                string jsonPayload = JsonConvert.SerializeObject(payload);

                // 4. Llamada al API
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                        string urlApi = "https://localhost:44362/api/mensaje/spcrearsala";

                        HttpResponseMessage resp = await client.PostAsync(urlApi, content);
                        if (resp.IsSuccessStatusCode)
                        {
                            string respJson = await resp.Content.ReadAsStringAsync();
                            // Opcional: puedes parsear un objeto de respuesta si el API devuelve algo
                            Response.Write("<script>alert('Sala creada correctamente.');</script>");
                            Response.Redirect("Chat.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('Error al crear sala: " + resp.StatusCode + "');</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error interno al crear sala.');</script>");
                }
            }

        }
    }
}