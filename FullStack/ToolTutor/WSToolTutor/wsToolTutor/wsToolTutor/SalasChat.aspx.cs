using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using wsToolTutor.Models;
using System.Drawing.Printing;

namespace wsToolTutor
{
    public partial class SalasChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSalas();
            }
        }

        protected void rptSalas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AbrirSala")
            {
                string numeroSala = e.CommandArgument.ToString();
                Session["desUsuario"] = numeroSala; // Guarda la sala activa

                // Redirige al chat de la sala
                Response.Redirect("Chat.aspx"); // O la ruta que uses
            }
        }


        private async void CargarSalas()
        {
            string apiUrl = "https://localhost:44362/api/mensaje/splistasalas"; // O la URL real del servidor
            string cveUsuario = Session["cveUsuario"]?.ToString();

            if (string.IsNullOrEmpty(cveUsuario)) return;

            var client = new HttpClient();
            var parametros = new { rem_cve = cveUsuario };
            var content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(jsonResponse);

                if ((bool)obj["statusExec"])
                {
                    List<SalaChat> salas = obj["datos"]["salas"].ToObject<List<SalaChat>>();
                    rptSalas.DataSource = salas;
                    rptSalas.DataBind();
                }
            }
        }

    }
}