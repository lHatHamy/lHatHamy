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
using wsCtrlUsuario.Models;

namespace wsCtrlUsuario
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            await cargaDatosApi();
            GridView1.PageIndexChanging += GridView1_PageIndexChanging;
        }

        private void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e) 
        { 
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        // Método Asincrono para ejecutar el reporte de Usuarios

        private async Task cargaDatosApi()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Ejecución de la petición de un endpoint a una webApi
                    string apiUrl = "https://localhost:44348/full/usuario/vwrptusuario?searchTerm=" + TextBox1.Text;
                    HttpResponseMessage respuesta =
                                    await client.GetAsync(apiUrl);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();

                    // Validación de estado de ejecución
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);
                        // ------------------------------------------
                        JArray jsonArray = (JArray)objRespuesta.datos["vwPrtUsuario"];
                        // Convertir JArray a DataTable
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                        // -------------------------------------------
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Error de conexión con webapi');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('Error inesperado ...');</script>");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            cargaDatosApi();
        }
    }
}