using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace InegiWeb
{
    public partial class GuardarReporte : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        string idMantenimiento = Seguimiento.idMantenimiento;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ObtenerDatos();
            LlenarTablas();
        }
        private void ObtenerDatos()
        {

            String query = "select NombreEdificio,Direccion,Posesion,Ocupacion,Superficie,Uso,Niveles,Cajones,Antiguedad,Terreno from Edificios inner join Mantenimientos ON Edificios.IdEdificio = Mantenimientos.IdEdificio where IdMantenimiento = @IdMantenimiento";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtnombreInmueble.Text = reader["NombreEdificio"].ToString();
                txtDireccion.Text = reader["Direccion"].ToString();
                txtPosesion.Text = reader["Posesion"].ToString();
                txtOcupacion.Text = reader["Ocupacion"].ToString() + " años";
                txtSuperficie.Text = reader["Superficie"].ToString() + " mt2";
                txtUso.Text = reader["Uso"].ToString();
                txtNiveles.Text = reader["Niveles"].ToString();
                txtCajones.Text = reader["Cajones"].ToString();
                txtAntiguedad.Text = reader["Antiguedad"].ToString() + " años";
                txtTerreno.Text = reader["Terreno"].ToString() + " mt2";

            }
            command.Connection.Close();
        }

        private void LlenarTablas()
        {
            GVMaterialesInstalacion.DataSource = ReporteInfraestructura.tableInstalacion;
            GVMaterialesInstalacion.DataBind();
            GVMaterialIlluminacion.DataSource = ReporteInfraestructura.tableIlluminacion;
            GVMaterialIlluminacion.DataBind();
        }

        protected void btnGuardarDatos_Click(object sender, EventArgs e)
        {
            if (pdfEvidencias.HasFile && pdfOrdenCompra.HasFile)
            {
                if (detallesCompratxt.Equals(""))
                {
                    idMessage.Text = "Falta llenar detalles de compra";
                    headTextModal.Text = "Error";
                    mpeErrorCarga.Show();
                }
                else
                {
                    string insertOrden = "Insert into OrdenCompra(DetallesdeCompra,ImageInstalacion,TicketdeCompra) values(@DetallesdeCompra,@pdfImage,@pdfTicket)";
                    SqlCommand command = new SqlCommand(insertOrden, sqlconn);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@DetallesdeCompra", detallesDeCompra.Text);
                    command.Parameters.AddWithValue("@pdfImage", SqlDbType.VarBinary).Value = pdfEvidencias.FileBytes;
                    command.Parameters.AddWithValue("@pdfTicket", SqlDbType.VarBinary).Value = pdfOrdenCompra.FileBytes;
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                    string getValue = "select MAX(IdOrdenCompra) as 'IdOrden' from OrdenCompra";
                    SqlCommand command2 = new SqlCommand(getValue, sqlconn);
                    command2.Connection.Open();
                    SqlDataReader dr = command2.ExecuteReader();
                    if (dr.Read())
                    {
                        int idOrden = Convert.ToInt32(dr["IdOrden"].ToString());
                        command2.Connection.Close();
                        GuardarDetallesReporte(idOrden);
                    }

                }
            }
            else
            {
                idMessage.Text = "Hace falta cargar el archivo de orden de compra y/o evidencia.";
                headTextModal.Text = "Error";
                mpeErrorCarga.Show();
            }
        }

        private void GuardarDetallesReporte(int idOrden)
        {
            string IdUsuario = Convert.ToString(Session["IdUsuario"]);
            int totalArreglados = ReporteInfraestructura.totalArregladosInstalacion + ReporteInfraestructura.totalArregladosIlluminacion;
            double total = ReporteInfraestructura.grandTotalInstalacion + ReporteInfraestructura.grandTotalIlluminacion;
            string validacion = "Pendiente";
            string insertReport = @"insert into Reportes(FechaReporte,TotalArreglados,CostoTotal,Validacion,IdOrdenCompra,IdMantenimiento,IdUsuario)
                                    values (@Fecha,@Arreglados,@CostoTotal,@Validacion,@IdCompra,@IdMantenimiento,@IdUsuario)";
            SqlCommand command = new SqlCommand(insertReport, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@Fecha", txtFecha.Text);
            command.Parameters.AddWithValue("@Arreglados", totalArreglados);
            command.Parameters.AddWithValue("@CostoTotal", total);
            command.Parameters.AddWithValue("@Validacion", validacion);
            command.Parameters.AddWithValue("@IdCompra", idOrden);
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            command.Parameters.AddWithValue("@IdUsuario", IdUsuario);
            command.ExecuteNonQuery();
            command.Connection.Close();

            string getValue = "select MAX(IdReporte) as 'IdReporte' from Reportes";
            SqlCommand command2 = new SqlCommand(getValue, sqlconn);
            command2.Connection.Open();
            SqlDataReader dr = command2.ExecuteReader();
            if (dr.Read())
            {
                int idReporte = Convert.ToInt32(dr["IdReporte"].ToString());
                command2.Connection.Close();
                GuardarDetallesIlluminacion(idReporte);
                GuardarDetallesInstalacion(idReporte);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se guardo corractamente el reporte');", true);
            Response.Redirect("MenuReportes.aspx", false);
        }

        private void GuardarDetallesIlluminacion(int idReporte)
        {
            int i;

            for (i = 0; i < ReporteInfraestructura.tableIlluminacion.Rows.Count; i++)
            {
                if (ReporteInfraestructura.tableIlluminacion.Rows[i]["IdMaterial"].ToString() == "")
                {
                    continue;
                }
                else
                {
                    string idMaterial = ReporteInfraestructura.tableIlluminacion.Rows[i]["IdMaterial"].ToString();
                    string arreglados = ReporteInfraestructura.tableIlluminacion.Rows[i]["Arreglados"].ToString();
                    string subtotal = ReporteInfraestructura.tableIlluminacion.Rows[i]["Subtotal"].ToString();

                    string insertDet = @"Insert into DetallesReporte (Arreglados,Subtotal,IdMaterial,IdReporte) values (@Arreglados,@Subtotal,@IdMaterial,@IdReporte)";
                    SqlCommand command = new SqlCommand(insertDet, sqlconn);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Arreglados", arreglados);
                    command.Parameters.AddWithValue("@Subtotal", subtotal);
                    command.Parameters.AddWithValue("@IdMaterial", idMaterial);
                    command.Parameters.AddWithValue("@IdReporte", idReporte);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }

            }
        }

        private void GuardarDetallesInstalacion(int idReporte)
        {
            int i;

            for (i = 0; i < ReporteInfraestructura.tableInstalacion.Rows.Count; i++)
            {
                if (ReporteInfraestructura.tableInstalacion.Rows[i]["IdMaterial"].ToString() == "")
                {
                    break;
                }
                else
                {
                    string idMaterial = ReporteInfraestructura.tableInstalacion.Rows[i]["IdMaterial"].ToString();
                    string arreglados = ReporteInfraestructura.tableInstalacion.Rows[i]["Arreglados"].ToString();
                    string subtotal = ReporteInfraestructura.tableInstalacion.Rows[i]["Subtotal"].ToString();
                    string insertDet = @"Insert into DetallesReporte (Arreglados,Subtotal,IdMaterial,IdReporte) values (@Arreglados,@Subtotal,@IdMaterial,@IdReporte)";
                    SqlCommand command = new SqlCommand(insertDet, sqlconn);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Arreglados", arreglados);
                    command.Parameters.AddWithValue("@Subtotal", subtotal);
                    command.Parameters.AddWithValue("@IdMaterial", idMaterial);
                    command.Parameters.AddWithValue("@IdReporte", idReporte);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }

            }
        }
    }
}