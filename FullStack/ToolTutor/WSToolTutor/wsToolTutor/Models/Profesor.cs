using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


namespace wsToolTutor.Models
{
    public class Profesor
    {
        [JsonProperty("USU_CVE_USUARIO")]
        public int Usuario { get; set; }

        [JsonProperty("NOMBRE_COMPLETO")]
        public string NombreCompleto { get; set; }
    }
}