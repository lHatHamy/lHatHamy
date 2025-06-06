using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using wsToolTutor.Models;
using System.Net.Http.Headers;
using System.Text;

namespace wsToolTutor
{
    public partial class Docentes : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await cargaDatosApi();
            }
        }

        private async Task cargaDatosApi(string searchTerm = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:44362/api/estudiante/vwrptprofesor?searchTerm={searchTerm}";
                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();

                        clsApiStatus objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Verifica que el campo "datos" contenga el array esperado
                        if (objRespuesta.datos != null && objRespuesta.datos["vwRptProfesor"] != null)
                        {
                            JArray jsonArray = (JArray)objRespuesta.datos["vwRptProfesor"];

                            List<Profesor> listaProfesores = jsonArray.ToObject<List<Profesor>>();

                            rptDocentes.DataSource = listaProfesores;
                            rptDocentes.DataBind();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al conectar con la API');</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error inesperado al cargar docentes');</script>");
            }
        }


        protected async void rptDocentes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
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
            else if (e.CommandName == "VerPerfil")
            {
                // Maneja VerPerfil, por ejemplo:
                int desCve = int.Parse(e.CommandArgument.ToString());
                Response.Redirect($"PerfilDocente.aspx?usuario={desCve}");
            }
        }

        protected async void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {

            await cargaDatosApi(TextBox1.Text);
        }

        protected async void TextBox1_TextChanged(object sender, EventArgs e)
        {
            await cargaDatosApi(TextBox1.Text);
        }

    }
}