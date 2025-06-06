using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wsCtrlUsuario.Models;

namespace wsCtrlUsuario
{
    public partial class wpUpload : System.Web.UI.Page
    {
        protected global::System.Web.UI.HtmlControls.HtmlInputFile oFile;
        protected global::System.Web.UI.WebControls.Label lblUploadResult;
        protected global::System.Web.UI.WebControls.Panel frmConfirmation;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string strFileName;
            string strFilePath;
            string strFolder;
            strFolder = Server.MapPath("imagenes/uploads/");
            // Carga el nombre del archivo que será enviado al server
            strFileName = oFile.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);
            if (oFile.Value != "")
            {
                // Crea el folder si no existe --> imagenes
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }
                // Guarda el archivo uploaded al servidor
                strFilePath = strFolder + strFileName;
                if (File.Exists(strFilePath))
                {
                    lblUploadResult.Text = strFileName + " ya existe en el servidor web!";
                }
                else
                {
                    oFile.PostedFile.SaveAs(strFilePath);
                    lblUploadResult.Text = strFileName + " ha sido exitosamente cargado en el servidor web.";
                }
            }
            else
            {
                lblUploadResult.Text = "Click 'Examinar/Browse' para seleccionar el archivo a cargar...";
            }
            // Despliega el estatus del upload en el panel
            frmConfirmation.Visible = true;



        }
    }
}