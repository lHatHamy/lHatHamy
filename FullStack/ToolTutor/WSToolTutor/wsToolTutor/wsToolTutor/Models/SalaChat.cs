using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsToolTutor.Models
{
    public class SalaChat
    {
        public string numero_sala { get; set; }
        public string fecha_sala { get; set; }
        public string participantes { get; set; }
        public string total_mensajes { get; set; }
        public string ultimo_mensaje { get; set; }
    }
}