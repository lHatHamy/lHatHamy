using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//-------------------
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
// ------------------

namespace apiRESTCtrlUsuario.Models
{
    public class clsUsario
    {

        // Definición de atributos
        public string cve { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public string ruta { get; set; }
        public string tipo { get; set; }

        // DEFINICIÓN DE CADENAS DE CONEXIÓN
        private string cadConn = ConfigurationManager.
            ConnectionStrings["bdControlAcceso"].
            ConnectionString;

        // DEFINICIÓN DE CONTRUCCIÓN DEL MODELO
        public clsUsario()
        {
            // Codigo de actualización
        }
        public clsUsario(string cve)
        {
            this.cve = cve;

        }
        public clsUsario(string usuario, string contrasena)
        {
            this.usuario = usuario;
            this.contrasena = contrasena;

        }

        public clsUsario(string cve, string nombre, string apellidoPaterno, string apellidoMaterno,
            string usuario, string contrasena, string ruta, string tipo)
        {
            this.cve = cve;
            this.nombre = nombre;
            this.apellidoPaterno = apellidoPaterno;
            this.apellidoMaterno = apellidoMaterno;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.ruta = ruta;
            this.tipo = tipo;
        }
        // DEFINICIÓN DE METODOS DE PROCESO

        public DataSet spInsUsuario()                           // METODO DE INSERCIÓN DE DATOS A SQL
        {
            // Generación del comando SQL
            string cadSql = "CALL spInsUsuario('" + this.nombre + "', '"
                + this.apellidoPaterno + "', '"
                + this.apellidoMaterno + "', '"
                + this.usuario + "', '"
                + this.contrasena + "', '"
                + this.ruta + "', "
                + this.tipo + ");"
                ;

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spInsUsuario");
            return ds;
        }

        public DataSet spValidarAcceso()                        // METODO DE VALIDACIÓN DE CREDENCIALES EN SQL
        {
            // Generación del comando SQL
            string cadSql = "CALL spValidarAcceso('" + this.usuario + "', '"
                                                     + this.contrasena + "');";

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spValidarAcceso");
            return ds;
        }

        public DataSet vwPrtUsuario(string searchTerm = null)                        // METODO DE VALIDACIÓN DE VISTA DE USUARIOS EN SQL
        {
            // Generación del comando SQL
            string cadSql = "select * from vwRptUsuario";

            // Si se proporciona un término de búsqueda, se añade a la consulta SQL
            if (!string.IsNullOrEmpty(searchTerm))
            {
                cadSql += " where Nombre like '%" + searchTerm + "%';";
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
            da.Fill(ds, "vwPrtUsuario");
            return ds;
        }

        public DataSet vwTipoUsuario()                        // METODO DE VALIDACIÓN DE VISTA DE USUARIOS EN SQL
        {
            // Generación del comando SQL
            string cadSql = "select * from vwTipoUsuario;";

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "vwPrtUsuario");
            return ds;
        }

        public DataSet spUpdUsuarios() 
        {
            // Generación del comando SQL
            string cadSql = "CALL spUpdUsuario('" + this.cve + "', '"
                + this.nombre + "', '"
                + this.apellidoPaterno + "', '"
                + this.apellidoMaterno + "', '"
                + this.usuario + "', '"
                + this.contrasena + "', '"
                + this.ruta + "', "
                + this.tipo + ");"
                ;

            // Configuración de los objetos de conexión a MySQL
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            // Ejecución del adaptador de datos
            da.Fill(ds, "spUpdUsuarios");
            return ds;
        }

        public DataSet spDelUsuario()
        {
            if (string.IsNullOrEmpty(this.cve))
                throw new Exception("La clave (cve) no puede estar vacía.");

            int claveInt = int.Parse(this.cve);  // convertir a int

            string cadSql = "CALL spDelUsuario(" + claveInt + ");";
            MySqlConnection cnn = new MySqlConnection(cadConn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "spDelUsuario");
            return ds;
        }

    }
}