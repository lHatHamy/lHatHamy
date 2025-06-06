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
    public class clsProfesor
    {
        public string p_cve { get; set; }
        public string p_nombre { get; set; }
        public string p_apellidoPaterno { get; set; }
        public string p_apellidoMaterno { get; set; }
        public string p_matricula { get; set; }
        public string p_contrasena { get; set; }
        public string p_usuario { get; set; }

        // DEFINICIÓN DE CADENAS DE CONEXIÓN
        private string cadConn = ConfigurationManager.
            ConnectionStrings["bdToolTutor"].
            ConnectionString;

        public clsProfesor() 
        {
        }

        public clsProfesor(string p_cve, string p_nombre, string p_apellidoPaterno, string p_apellidoMaterno,
                           string p_matricula, string p_contrasena)
        {
            this.p_cve = p_cve;
            this.p_nombre = p_nombre;
            this.p_apellidoPaterno = p_apellidoPaterno;
            this.p_apellidoMaterno = p_apellidoMaterno;
            this.p_matricula = p_matricula;
            this.p_contrasena = p_contrasena;
        }

        public clsProfesor(string p_matricula, string p_contrasena)
        {
            this.p_matricula = p_matricula;
            this.p_contrasena = p_contrasena;
        }


        public DataSet spInsProfesor()                           // METODO DE INSERCIÓN DE DATOS DE ESTUDIANTE A SQL
        {
            // Generación del comando SQL
            string cadSql = "CALL spInsProfesor('" + this.p_nombre + "', '"
                + this.p_apellidoPaterno + "', '"
                + this.p_apellidoMaterno + "', '"
                + this.p_matricula + "', '"
                + this.p_contrasena + "');"
                ;

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spInsProfesor");
            return ds;
        }

        public DataSet spValidarAccesoProfesor()                   // METODO DE VALIDACIÓN DE CREDENCIALES EN SQL
        {
            // Generación del comando SQL
            string cadSql = "CALL spValidarAccesoProfesor('" + this.p_matricula + "', '"
                                                     + this.p_contrasena + "');";

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spValidarAccesoProfesor");
            return ds;
        }
        public DataSet vwRptEstudiante(string gen = null, string name = null, string career = null, string semester = null)
        {
            // Consulta base
            string cadSql = "SELECT * FROM vw_reporte_estudiante";
            List<string> condiciones = new List<string>();

            // Dependiendo de las condiciones que se den, se agregan dinamicamente
            if (!string.IsNullOrEmpty(gen))
                condiciones.Add("GENERACION LIKE '%" + gen + "%'");

            if (!string.IsNullOrEmpty(name))
                condiciones.Add("NOMBRE_COMPLETO LIKE '%" + name + "%'");

            if (!string.IsNullOrEmpty(career))
                condiciones.Add("CARRERA LIKE '%" + career + "%'");

            if (!string.IsNullOrEmpty(semester))
                condiciones.Add("EST_SEMESTRE LIKE '%" + semester + "%'");

            // Si hay condiciones, se agregan a la consulta con WHERE
            if (condiciones.Count > 0)
            {
                cadSql += " WHERE " + string.Join(" AND ", condiciones);
            }

            // Ordena alfabéticamente por nombre
            cadSql += " ORDER BY NOMBRE_COMPLETO;";

            // Conexión y ejecución
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "vwRptEstudiante");
            return ds;
        }

    }




}