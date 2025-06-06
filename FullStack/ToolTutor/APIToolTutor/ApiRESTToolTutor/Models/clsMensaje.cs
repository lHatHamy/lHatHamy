using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//-------------------
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
// ------------------


namespace ApiRESTToolTutor.Models
{
    public class clsMensaje
    {
        public string rem_cve { get; set; }
        public string des_cve { get; set; }
        public string msg { get; set; }
        public string sala { get; set; }
        public string canal { get; set; }
        public string tipo { get; set; }


        // DEFINICIÓN DE CADENAS DE CONEXIÓN
        private string cadConn = ConfigurationManager.
            ConnectionStrings["bdToolTutor"].
            ConnectionString;

        public clsMensaje() 
        { 
        }

        public clsMensaje(string parametro, bool esRemCve = false)
        {
            if (esRemCve)
            {
                this.rem_cve = parametro;
            }
            else
            {
                this.sala = parametro;
            }
        }


        // PARA MANEJAR LA CREACIÓN DE SALAS
        public clsMensaje(string rem_cve, string des_cve)
        {
            this.rem_cve = rem_cve;
            this.des_cve = des_cve;
        }


        //PARA ENVIAR EL MENSAJE A LA SALA
        public clsMensaje(string rem_cve, string msg, string sala, string canal, string tipo)
        {
            this.rem_cve = rem_cve;
            this.msg = msg;
            this.sala = sala;
            this.canal = canal;
            this.tipo = tipo;
        }

        // SOLICITUD DE CADENAS SQL

        public DataSet spCrearSala()                           // METODO DE INSERCIÓN DE DATOS DE ESTUDIANTE A SQL
        {
            // Generación del comando SQL
            string cadSql = "CALL spCrearSala(" + this.rem_cve + ", "
                + this.des_cve + ");"
                ;

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spInsEstudiante");
            return ds;
        }

        public DataSet spEnviarMensaje()                           // METODO DE INSERCIÓN DE DATOS DE ESTUDIANTE A SQL
        {
            // Generación del comando SQL
            string cadSql = "CALL spEnviarMensaje(" + this.rem_cve + ", '"
                + this.msg + "', "
                + this.sala + ", "
                + this.canal + ", "
                + this.tipo + ");"
                ;

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spEnviarMensaje");
            return ds;
        }

        public DataSet vwSalaChat()                           // VISTA PARA VER LA CONVERSACIÓN
        {
            // Generación del comando SQL
            string cadSql = "SELECT * FROM vw_sala_chat WHERE RM_SALA = " + this.sala + ";"
                ;

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spInsEstudiante");
            return ds;
        }

        public DataSet spListaSalas()
        {
            string cadSql = "CALL spListaSalas (@rem_cve);";

            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlCommand cmd = new MySqlCommand(cadSql, cnn);
            cmd.Parameters.AddWithValue("@rem_cve", this.rem_cve);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "spListaSalas");
            return ds;
        }
    }
}