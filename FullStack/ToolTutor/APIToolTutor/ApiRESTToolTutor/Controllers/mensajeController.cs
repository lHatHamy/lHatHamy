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
    public class mensajeController : ApiController
    {


        [HttpPost]
        [Route("api/mensaje/spenviarmensaje")]
        public clsApiStatus spEnviarMensaje([FromBody] clsMensaje modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------


            try
            {
                clsMensaje objMnesaje = new clsMensaje(modelo.rem_cve,
                                                     modelo.msg,
                                                     modelo.sala,
                                                     modelo.canal,
                                                     modelo.tipo);

                DataSet ds = new DataSet();
                // Ejecución del metodo del modelo (y recepción de los datos)
                ds = objMnesaje.spEnviarMensaje();
                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Mensaje enviado correctamente!";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                jsonResp.Add("msgData", "Mensaje enviado correctamente !");
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex)
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "mensaje NO enviado...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;

            }

            return objRespuesta;
        }



        [HttpPost]
        [Route("api/mensaje/spcrearsala")]
        public clsApiStatus spCrearSala([FromBody] clsMensaje modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------


            try
            {
                clsMensaje objMnesaje = new clsMensaje(modelo.rem_cve,
                                                       modelo.des_cve);

                DataSet ds = new DataSet();
                // Ejecución del metodo del modelo (y recepción de los datos)
                ds = objMnesaje.spCrearSala();
                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Sala creada correctamente!";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][1].ToString());
                jsonResp.Add("msgData", "Sala creada correctamente !");
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex)
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Sala NO creada";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;

            }

            return objRespuesta;
        }



        [HttpPost]
        [Route("api/mensaje/vwsalachat")]
        public clsApiStatus vwSalaChat([FromBody] clsMensaje modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            DataSet ds = new DataSet();



            try
            {
                clsMensaje objMnesaje = new clsMensaje(modelo.sala);

                // Ejecución del metodo del modelo (y recepción de los datos)
                ds = objMnesaje.vwSalaChat();
                // Configuración del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Sala mostrada correctamente!";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                JArray mensajes = new JArray();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    JObject mensaje = new JObject
                    {
                        ["msg_fecha"] = row["MSG_FECHA"].ToString(),
                        ["nombre_completo"] = row["NOMBRE_COMPLETO"].ToString(),
                        ["tipo_usuario"] = row["TIPO_USUARIO"].ToString(),
                        ["msg_mensaje"] = row["MSG_MENSAJE"].ToString(),
                        ["estado_mensaje"] = row["ESTADO_MENSAJE"].ToString(),
                        ["tipo_mensaje"] = row["TIPO_MENSAJE"].ToString()
                    };
                    mensajes.Add(mensaje);
                }

                objRespuesta.statusExec = true;
                objRespuesta.msg = "Sala mostrada correctamente!";
                objRespuesta.ban = ds.Tables[0].Rows.Count;
                jsonResp["mensajes"] = mensajes;
                objRespuesta.datos = jsonResp;

            }
            catch (Exception ex)
            {
                // Configuración del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Sala NO mostrada";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;

            }

            return objRespuesta;
        }

        // METODO POST PARA VALIDAR ACCESO DE ESTUDIANTE
        [HttpPost]
        [Route("api/mensaje/splistasalas")]
        public clsApiStatus spListaSalas([FromBody] clsMensaje modelo)
        {
            //-------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            //-------------------------------------

            DataSet ds = new DataSet();
            clsMensaje objMensaje = new clsMensaje(modelo.rem_cve, true);

            try
            {
                ds = objMensaje.spListaSalas();

                JArray salas = new JArray();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    JObject sala = new JObject
                    {
                        ["numero_sala"] = row["NUMERO_SALA"]?.ToString() ?? "",
                        ["fecha_sala"] = row["FECHA_SALA"]?.ToString() ?? "",
                        ["participantes"] = row["PARTICIPANTES"]?.ToString() ?? "",
                        ["total_mensajes"] = row["TOTAL_MENSAJES"]?.ToString() ?? "",
                        ["ultimo_mensaje"] = row["ULTIMO_MENSAJE"]?.ToString() ?? ""
                    };
                    salas.Add(sala);
                }

                objRespuesta.statusExec = true;
                objRespuesta.msg = "Sala mostrada correctamente!";
                objRespuesta.ban = ds.Tables[0].Rows.Count;
                jsonResp["salas"] = salas;
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex)
            {
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Fallo en la vista de salas ...";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }

            return objRespuesta;
        }
    }
}
