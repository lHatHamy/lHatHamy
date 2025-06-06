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
using ApiRESTToolTutor.Models;
using System.Reflection;
// -------------------------------------

namespace ApiRESTToolTutor.Controllers
{
    public class estudianteController : ApiController
    {
        // METODO POST PARA VALIDAR ACCESO DE ESTUDIANTE
        [HttpPost]
        [Route("api/estudiante/spvalidaraccesoestudiante")]
        public clsApiStatus spValidarEstudiante([FromBody] clsEstudiante modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            DataSet ds = new DataSet();

            try
            {
                clsEstudiante objEstudiante = new clsEstudiante(modelo.e_num_control,
                                                     modelo.e_contrasena);
                ds = objEstudiante.spValidarAccesoEstudiante();

                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                // Validación de la bandera recibida (0, 1)
                if (objRespuesta.ban == 1)
                {
                    objRespuesta.msg = "Estudiante Validado Exitosamente! ";
                    jsonResp.Add("clave_usuario", ds.Tables[0].Rows[0][1].ToString());
                    jsonResp.Add("nombre_completo", ds.Tables[0].Rows[0][2].ToString());
                    jsonResp.Add("matricula", ds.Tables[0].Rows[0][3].ToString());
                    jsonResp.Add("semestre", ds.Tables[0].Rows[0][4].ToString());
                    jsonResp.Add("carrera", ds.Tables[0].Rows[0][5].ToString());
                    jsonResp.Add("generacion", ds.Tables[0].Rows[0][6].ToString());
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
        [Route("api/estudiante/vwrptprofesor")]
        public clsApiStatus vwRptProfesor([FromUri] string searchTerm = null)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            DataSet ds = new DataSet();

            try
            {
                clsEstudiante objEstudiante = new clsEstudiante();
                ds = objEstudiante.vwRptProfesor(searchTerm);
                objRespuesta.statusExec = true;
                objRespuesta.ban = ds.Tables[0].Rows.Count;
                objRespuesta.msg = "Consulta de profesor realizada exitosamente";

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
                objRespuesta.msg = "Fallo en la consulta de profesores ...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }

            return objRespuesta;
        }

        [HttpPost]
        [Route("api/estudiante/spinsestudiante")]
        public clsApiStatus PostEstudiante([FromBody] clsEstudiante modelo)
        {
            var resp = new clsApiStatus();
            var json = new JObject();

            try
            {
                DataSet ds = modelo.spInsEstudiante();
                int ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                resp.statusExec = true;
                resp.ban = ban;
                resp.msg = MapeaMensajeEstudiante(ban);
                json.Add("msgData", resp.msg);
            }
            catch (Exception ex)
            {
                resp.statusExec = false;
                resp.ban = -1;
                resp.msg = "Error al registrar estudiante";
                json.Add("msgData", ex.Message);
            }

            resp.datos = json;
            return resp;
        }

        /* ====== Helpers de mapeo ====== */

        private string MapeaMensajeEstudiante(int ban)
        {
            switch (ban)
            {
                case 0: return "Estudiante registrado correctamente";
                case 1: return "Nombre completo ya registrado";
                case 2: return "Número de control ya registrado";
                case 3: return "Tipo de usuario no existe";
                case 4: return "Carrera no existe";
                default: return "Código desconocido";
            }
        }
    }
}
