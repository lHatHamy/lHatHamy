using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System;
using wsToolTutor;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace wsToolTutor
{
    public partial class InsertarProfesor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnRegistrar_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:44362/api/profesor/spinsprofesor"; // Cambia el puerto si es necesario

            var profesorData = new
            {
                p_nombre = txtNombre.Text.Trim(),
                p_apellidoPaterno = txtApellidoPaterno.Text.Trim(),
                p_apellidoMaterno = txtApellidoMaterno.Text.Trim(),
                p_matricula = txtMatricula.Text.Trim(),
                p_contrasena = txtContrasena.Text.Trim()
            };

            using (var client = new HttpClient())
            {
                var json = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(profesorData), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(apiUrl, json);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        JObject jsonResp = JObject.Parse(result);
                        int ban = int.Parse(jsonResp["ban"].ToString());
                        string msg = jsonResp["msg"].ToString();

                        Response.Write($"<script>alert('{msg}');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al registrar profesor');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Excepción: {ex.Message}');</script>");
                }
            }
        }
    }
}