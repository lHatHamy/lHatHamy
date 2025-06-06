using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// --------------------------------------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Web;
using apiRESTCtrlUsuario.Models;
using System.Reflection;
// -------------------------------------

namespace apiRESTCtrlUsuario.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("full/usuario/spinsusario")]
        public clsApiStatus spInsUsuario([FromBody] clsUsario modelo) 
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            try
            {
                clsUsario objUsuario = new clsUsario(modelo.cve,
                                                     modelo.nombre,
                                                     modelo.apellidoPaterno,
                                                     modelo.apellidoMaterno,
                                                     modelo.usuario,
                                                     modelo.contrasena,
                                                     modelo.ruta,
                                                     modelo.tipo);

                DataSet ds = new DataSet();
                // Ejecución del metodo del modelo (y recepción de los datos)
                ds = objUsuario.spInsUsuario();
                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Usuario registrado exitosamente !";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                jsonResp.Add("msgData", "Usuario registrado exitosamente !");
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex)
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Usuario NO registrado...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;

            }
            return objRespuesta;
        }

        [HttpPost]
        [Route("full/usuario/spvalidaracceso")]
        public clsApiStatus spValidarUsuario([FromBody] clsUsario modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            DataSet ds = new DataSet();

            try
            {
                clsUsario objUsuario = new clsUsario(modelo.usuario, 
                                                     modelo.contrasena);
                ds = objUsuario.spValidarAcceso();

                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                // Validación de la bandera recibida (0, 1)
                if (objRespuesta.ban == 1)
                {
                    objRespuesta.msg = "Usuario Validado Exitosamente! ";
                    jsonResp.Add("usu_nombre_completo", ds.Tables[0].Rows[0][1].ToString());
                    jsonResp.Add("usu_ruta", ds.Tables[0].Rows[0][2].ToString());
                    jsonResp.Add("usu_usuario", ds.Tables[0].Rows[0][3].ToString());
                    jsonResp.Add("tipo_descripcion", ds.Tables[0].Rows[0][4].ToString());
                }
                else 
                {
                    objRespuesta.msg = "Usuario no tiene permiso de acceso ¡ERROR!";
                    jsonResp.Add("msgData", "Usuario no tiene permiso de acceso");
                }

                objRespuesta.datos = jsonResp;


            }
            catch (Exception ex) 
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Fallo en la validación del acceso ...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }

            return objRespuesta;
        }

        [HttpGet]
        [Route("full/usuario/vwrptusuario")]
        public clsApiStatus vwRptUsuario([FromUri] string searchTerm = null)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------
    
            DataSet ds = new DataSet();

            try
            {
                clsUsario objUsuario = new clsUsario();
                ds = objUsuario.vwPrtUsuario(searchTerm);
                objRespuesta.statusExec = true;
                objRespuesta.ban = ds.Tables[0].Rows.Count;
                objRespuesta.msg = "Consulta de usuario realizada exitosamente";

                // Migración del ds(DataSet) al objeto Json
                string jsonString = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                jsonResp = JObject.Parse($"{{\"{ds.Tables[0].TableName}\": {jsonString}}}");
                // DatSet Migrado, se envpia clsPaiStatus
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex)
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Fallo en la consulta de reporte - Usuario...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }

            return objRespuesta;
        }

        [HttpPost]
        [Route("full/usuario/spupdusuarios")]
        public clsApiStatus spUpdUsuarios([FromBody] clsUsario modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------


            try
            {
                clsUsario objUsuario = new clsUsario(modelo.cve,
                                                     modelo.nombre,
                                                     modelo.apellidoPaterno,
                                                     modelo.apellidoMaterno,
                                                     modelo.usuario,
                                                     modelo.contrasena,
                                                     modelo.ruta,
                                                     modelo.tipo);

                DataSet ds = new DataSet();
                // Ejecución del metodo del modelo (y recepción de los datos)
                ds = objUsuario.spUpdUsuarios();
                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Usuario actualizado exitosamente !";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                jsonResp.Add("msgData", "Usuario actualizado exitosamente !");
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex) 
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Usuario NO registrado...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;

            }

            return objRespuesta;
        }

        [HttpPost]
        [Route("full/usuario/spdelusuario")]
        public clsApiStatus spDelUsuario([FromBody] clsUsario modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------


            try
            {
                clsUsario objUsuario = new clsUsario(modelo.cve);

                DataSet ds = new DataSet();
                // Ejecución del metodo del modelo (y recepción de los datos)
                ds = objUsuario.spDelUsuario();
                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Usuario eliminado exitosamente !";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                jsonResp.Add("msgData", "Usuario eliminado exitosamente !");
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex)
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Usuario NO eliminado...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;

            }

            return objRespuesta;
        }


        [HttpGet]
        [Route("full/usuario/vwtipousuario")]
        public clsApiStatus vwTipoUsuario()
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            DataSet ds = new DataSet();
            
            try
            {
                clsUsario objUsuario = new clsUsario();
                ds = objUsuario.vwTipoUsuario();
                objRespuesta.statusExec = true;
                objRespuesta.ban = ds.Tables[0].Rows.Count;
                objRespuesta.msg = "Consulta de tipo de usuario realizada exitosamente";

                // Migración del ds(DataSet) al objeto Json
                string jsonString = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                jsonResp = JObject.Parse($"{{\"{ds.Tables[0].TableName}\": {jsonString}}}");
                // DatSet Migrado, se envpia clsPaiStatus
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex)
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Fallo en la consulta de tipo de usuario - Usuario...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }

            return objRespuesta;
        }
    }
}
