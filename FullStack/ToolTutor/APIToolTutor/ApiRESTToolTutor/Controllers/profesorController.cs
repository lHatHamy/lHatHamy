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
    public class profesorController : ApiController
    {

        // METODO POST PARA VALIDAR ACCESO DE ESTUDIANTE
        [HttpPost]
        [Route("api/profesor/spvalidaraccesoprofesor")]
        public clsApiStatus spValidarProfesor([FromBody] clsProfesor modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            DataSet ds = new DataSet();

            try
            {
                clsProfesor objProfesor = new clsProfesor(modelo.p_matricula,
                                                     modelo.p_contrasena);
                ds = objProfesor.spValidarAccesoProfesor();

                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                // Validación de la bandera recibida (0, 1)
                if (objRespuesta.ban == 1)
                {
                    objRespuesta.msg = "Profesor Validado Exitosamente! ";
                    jsonResp.Add("clave_usuario", ds.Tables[0].Rows[0][1].ToString());
                    jsonResp.Add("nombre_completo", ds.Tables[0].Rows[0][2].ToString());
                    jsonResp.Add("matricula", ds.Tables[0].Rows[0][3].ToString());
                    jsonResp.Add("tipo_usuario", ds.Tables[0].Rows[0][4].ToString());
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
        [Route("api/profesor/vwrptestudiante")]
        public clsApiStatus vwRptEstudiante([FromUri] string gen = null, string name = null, string career = null, string semester = null)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            DataSet ds = new DataSet();

            try
            {
                clsProfesor objProfesor = new clsProfesor();
                ds = objProfesor.vwRptEstudiante(gen, name, career, semester);
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
                objRespuesta.msg = "Fallo en la consulta de estudiantes ...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }

            return objRespuesta;
        }


        [HttpPost]
        [Route("api/profesor/spinsprofesor")]
        public clsApiStatus PostProfesor([FromBody] clsProfesor modelo)
        {
            var resp = new clsApiStatus();
            var json = new JObject();

            try
            {
                DataSet ds = modelo.spInsProfesor();
                int ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                resp.statusExec = true;
                resp.ban = ban;
                resp.msg = MapeaMensajeProfesor(ban);
                json.Add("msgData", resp.msg);
            }
            catch (Exception ex)
            {
                resp.statusExec = false;
                resp.ban = -1;
                resp.msg = "Error al registrar profesor";
                json.Add("msgData", ex.Message);
            }

            resp.datos = json;
            return resp;
        }

        private string MapeaMensajeProfesor(int ban)
        {
            switch (ban)
            {
                case 0: return "Profesor registrado correctamente";
                case 1: return "Nombre completo ya registrado";
                case 2: return "Matrícula ya registrada";
                case 3: return "Tipo de usuario no existe";
                default: return "Código desconocido";
            }
        }
    }
}
