using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InegiWeb
{
    public partial class VerPDFEvidencias : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarPdf();
        }
        private void MostrarPdf()
        {
            string getPdf = "select ImageInstalacion from Reportes inner join OrdenCompra on Reportes.IdOrdenCompra = OrdenCompra.IdOrdenCompra where IdReporte=@Id";
            SqlCommand command = new SqlCommand(getPdf, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Request.QueryString["idEvi"];
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                byte[] imagen = (byte[])dataReader["ImageInstalacion"];
                SendDocument(imagen);
            }
        }

        private void SendDocument(byte[] buf)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "Application/pdf";

            HttpContext.Current.Response.AppendHeader("Content-Length", buf.Length.ToString());
            HttpContext.Current.Response.BinaryWrite(buf);

            HttpContext.Current.Response.End();
        }
    }
}