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
    public class clsEstudiante
    {
        public string e_cve { get; set; }
        public string e_nombre { get; set; }
        public string e_apellidoPaterno { get; set; }
        public string e_apellidoMaterno { get; set; }
        public string e_num_control { get; set; }
        public string e_contrasena { get; set; }
        public string e_semestre { get; set; }
        public string e_carrera { get; set; }
        public string e_usuario { get; set; }
        public string e_generacion { get; set; }

        // DEFINICIÓN DE CADENAS DE CONEXIÓN
        private string cadConn = ConfigurationManager.
            ConnectionStrings["bdToolTutor"].
            ConnectionString;



        // -- DEFINICIÓN DE LOS CONTRUCTORES DEL MODELO --

        public clsEstudiante() { 
            
        }        
        public clsEstudiante(string e_cve) { 
            this.e_cve = e_cve;
        }

        public clsEstudiante(string e_cve, string e_nombre, string e_apellidoPaterno, string e_apellidoMaterno,
        string e_num_control, string e_contrasena, string e_semestre, string e_carrera,
        string e_generacion)
        {
            this.e_cve = e_cve;
            this.e_nombre = e_nombre;
            this.e_apellidoPaterno = e_apellidoPaterno;
            this.e_apellidoMaterno = e_apellidoMaterno;
            this.e_num_control = e_num_control;
            this.e_contrasena = e_contrasena;
            this.e_semestre = e_semestre;
            this.e_carrera = e_carrera;
            this.e_generacion = e_generacion;
        }

        public clsEstudiante(string e_num_control, string e_contrasena)
        {
            this.e_num_control = e_num_control;
            this.e_contrasena = e_contrasena;
        }

        public DataSet spInsEstudiante()                           // METODO DE INSERCIÓN DE DATOS DE ESTUDIANTE A SQL
        {
            // Generación del comando SQL
            string cadSql = "CALL spInsEstudiante('" + this.e_nombre + "', '"
                + this.e_apellidoPaterno + "', '"
                + this.e_apellidoMaterno + "', '"
                + this.e_num_control + "', '"
                + this.e_contrasena + "', "
                + this.e_semestre + ", "
                + this.e_carrera + ", '"
                + this.e_generacion + "');"
                ;

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spInsEstudiante");
            return ds;
        }

        public DataSet spValidarAccesoEstudiante()                   // METODO DE VALIDACIÓN DE CREDENCIALES EN SQL
        {
            // Generación del comando SQL
            string cadSql = "CALL spValidarAccesoEstudiante('" + this.e_num_control + "', '"
                                                     + this.e_contrasena + "');";

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spValidarAccesoEstudiante");
            return ds;
        }

        public DataSet vwRptProfesor(string searchTerm = null)                        // METODO DE VALIDACIÓN DE VISTA DE USUARIOS EN SQL
        {
            // Generación del comando SQL
            string cadSql = "SELECT * FROM vw_reporte_profesor";

            // Si se proporciona un término de búsqueda, se añade a la consulta SQL
            if (!string.IsNullOrEmpty(searchTerm))
            {
                cadSql += " where nombre_completo like '%" + searchTerm + "%';";
            }
            else
            {
                cadSql += ";";
            }

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "vwRptProfesor");
            return ds;
        }

    }
}