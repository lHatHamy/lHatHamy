﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wsCtrlUsuario.Models;

namespace wsCtrlUsuario
{
    public partial class Formulario_web2 : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            await cargaDatosTipoUsuario();
        }

        // Creación del método asíncrono para ejecutar el
        // endpoint vwTipoUsuario
        private async Task cargaDatosTipoUsuario()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración de la peticion HTTP
                    string apiUrl = "https://localhost:44348/full/usuario/vwtipousuario";
                    // Ejecución del endpoint
                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();

                    // Validación del estatus OK
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);
                        // ------------------------------------------
                        JArray jsonArray = (JArray)objRespuesta.datos["vwPrtUsuario"];
                        // Convertir JArray a DataTable
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                        // -------------------------------------------
                        // Visualización de los datos formateados DropDownList
                        DropDownList1.DataSource = dt;
                        DropDownList1.DataTextField = "descripcion";
                        DropDownList1.DataValueField = "clave";
                        DropDownList1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }


        // Creación del método asíncrono para ejecutar el
        // endpoint spInsUsuario
        private async Task cargaDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                                  ""nombre"":""" + TextBox2.Text + "\"," +
                                  "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                                  "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasena\":\"" + TextBox6.Text + "\"," +
                                  "\"ruta\":\"" + TextBox7.Text + "\"," +
                                  "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44348/full/usuario/spinsusario";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario registrado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 1)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El nombre de usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 2)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 3)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El tipo de usuario no existe');" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }
        private async Task eliminarDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    String data = @"{
                ""cve"":""" + TextBox1.Text + @"""}";

                    HttpContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    string apiUrl = "https://localhost:44348/full/usuario/spdelusuario";

                    HttpResponseMessage respuesta = await client.PostAsync(apiUrl, contenido);
                    clsApiStatus objRespuesta = new clsApiStatus();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script>alert('Usuario eliminado exitosamente');</script>");
                            Response.Write("<script>document.location.href='Formulario web2.aspx';</script>");
                        }
                        else if (objRespuesta.ban == 1)
                        {
                            Response.Write("<script>alert('La clave de usuario no existe');</script>");
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
                Response.Write("<script>alert('Error de la aplicación, intentar nuevamente');</script>");
            }
        }

        private async Task actualizarDatos() 
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{

                                  ""cve"":""" + TextBox1.Text + "\"," +
                                  "\"nombre\":\"" + TextBox2.Text + "\"," +
                                  "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                                  "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasena\":\"" + TextBox6.Text + "\"," +
                                  "\"ruta\":\"" + TextBox7.Text + "\"," +
                                  "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44348/full/usuario/spupdusuarios";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario actualizado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 2)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El nombre " + TextBox2.Text +  " ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 3)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario " + TextBox5.Text + " ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 4)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El tipo de usuario no existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 1)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('La clave de usuario no existe');" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            // ejecución del metodo de carga de datos
            if (TextBox2.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                                  "alert('El nombre no puede quedar vacío');" +
                                  "</script>");

            }
            else
            {

                if (TextBox3.Text == "")
                {
                    Response.Write("<script language='javascript'>" +
                                      "alert('El apellido paterno no puede quedar vacío');" +
                                      "</script>");

                }
                else
                {
                    if (TextBox4.Text == "")
                    {
                        Response.Write("<script language='javascript'>" +
                                          "alert('El apellido materno no puede quedar vacío');" +
                                          "</script>");

                    }
                    else
                    {
                        if (TextBox5.Text == "")
                        {
                            Response.Write("<script language='javascript'>" +
                                              "alert('El Usuario no puede quedar vacío');" +
                                              "</script>");

                        }
                        else
                        {
                            if (TextBox6.Text == "")
                            {
                                Response.Write("<script language='javascript'>" +
                                                  "alert('La Contraseña no puede quedar vacío');" +
                                                  "</script>");

                            }
                            else
                            {
                                if (TextBox7.Text == "")
                                {
                                    Response.Write("<script language='javascript'>" +
                                                      "alert('La Ruta de foto no puede quedar vacía');" +
                                                      "</script>");

                                }
                                else
                                {
                                    //Ejecucion del metodo de Insercion
                                    await cargaDatos();

                                }

                            }

                        }

                    }

                }

            }
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                                  "alert('El nombre no puede quedar vacío');" +
                                  "</script>");

            }
            else
            {

                if (TextBox3.Text == "")
                {
                    Response.Write("<script language='javascript'>" +
                                      "alert('El apellido paterno no puede quedar vacío');" +
                                      "</script>");

                }
                else
                {
                    if (TextBox4.Text == "")
                    {
                        Response.Write("<script language='javascript'>" +
                                          "alert('El apellido materno no puede quedar vacío');" +
                                          "</script>");

                    }
                    else
                    {
                        if (TextBox5.Text == "")
                        {
                            Response.Write("<script language='javascript'>" +
                                              "alert('El Usuario no puede quedar vacío');" +
                                              "</script>");

                        }
                        else
                        {
                            if (TextBox6.Text == "")
                            {
                                Response.Write("<script language='javascript'>" +
                                                  "alert('La Contraseña no puede quedar vacío');" +
                                                  "</script>");

                            }
                            else
                            {
                                if (TextBox7.Text == "")
                                {
                                    Response.Write("<script language='javascript'>" +
                                                      "alert('La Ruta de foto no puede quedar vacía');" +
                                                      "</script>");

                                }
                                else
                                {
                                    //Ejecucion del metodo de actualización
                                    await actualizarDatos();

                                }

                            }

                        }

                    }

                }

            }
        }

        protected async void Button3_Click(object sender, EventArgs e)
        {
            await eliminarDatos();
        }
    }
}