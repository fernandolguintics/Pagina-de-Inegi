using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InegiWeb
{
    public partial class GuardarMantenimiento : System.Web.UI.Page
    {
        string idEdificio = "";
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarTabla();
            ObtenerDatos();
        }
        public void ObtenerDatos()
        {
            idEdificio = Menu.idInmueble;
            String query = "Select * from Edificios where IdEdificio = @idEdificio";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@idEdificio", idEdificio);
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
            txtFecha.Text = "Fecha:" + MantenimientoInfraestructura.fechaEvaluacion;
            command.Connection.Close();

        }
        private void LlenarTabla()
        {
            GVMaterialesInstalacion.DataSource = MantenimientoInfraestructura.tableInstalacion;
            GVMaterialesInstalacion.DataBind();
            GVMaterialesIlluminacion.DataSource = MantenimientoInfraestructura.tableIlluminacion;
            GVMaterialesIlluminacion.DataBind();
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            string IdUsuario = Convert.ToString(Session["IdUsuario"]);
            int totalElementos = MantenimientoInfraestructura.totalIlluminacionElementos + MantenimientoInfraestructura.totalInstalacionElementos;
            int malEstado = MantenimientoInfraestructura.totalMalEstadoInstalacion + MantenimientoInfraestructura.totalMalEstadoIlluminacion;
            double evaluacion = (MantenimientoInfraestructura.evaluacionIlluminacion + MantenimientoInfraestructura.evaluacionInstalacion) / 2.00;
            double costoTotal = MantenimientoInfraestructura.grandTotalIlluminacion + MantenimientoInfraestructura.grandTotalInstalacion;
            string queryInsert = @"Insert into Mantenimientos (FechaMantenimiento,IdEdificio, IdUsuario,
                                TotalElementosInstalados,Defectuosos,Evaluacion, CostoTotal) values(@fecha,@IdEdificio,
                                @IdUsuario,@TotalElementos,@Defectuosos,@Evaluacion,@CostoTotal)";

            SqlCommand cmd = new SqlCommand(queryInsert, sqlconn);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@fecha", MantenimientoInfraestructura.fechaEvaluacion);
            cmd.Parameters.AddWithValue("@IdEdificio", Menu.idInmueble);
            cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
            cmd.Parameters.AddWithValue("@TotalElementos", totalElementos);
            cmd.Parameters.AddWithValue("@Defectuosos", malEstado);
            cmd.Parameters.AddWithValue("@Evaluacion", evaluacion);
            cmd.Parameters.AddWithValue("@CostoTotal", costoTotal);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();

            string getIdMantenimiento = "Select Max(IdMantenimiento) as Id from Mantenimientos";
            SqlCommand command2 = new SqlCommand(getIdMantenimiento, sqlconn);
            command2.Connection.Open();
            SqlDataReader dr = command2.ExecuteReader();
            if (dr.Read())
            {
                int idMantenimiento = Convert.ToInt32(dr["Id"].ToString());
                command2.Connection.Close();
                GuardarMaterialesMantenimiento(idMantenimiento);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se ha guardado el mantenimiento');", true);
            Response.Redirect("Seguimiento.aspx", false);
        }

        private void GuardarMaterialesMantenimiento(int idMantenimiento)
        {
            int i;

            for (i = 0; i < MantenimientoInfraestructura.tableInstalacion.Rows.Count; i++)
            {
                if (MantenimientoInfraestructura.tableInstalacion.Rows[i]["IdMaterial"].ToString() == "")
                {
                    break;
                }
                else
                {
                    string idMaterial = MantenimientoInfraestructura.tableInstalacion.Rows[i]["IdMaterial"].ToString();
                    string piso = MantenimientoInfraestructura.tableInstalacion.Rows[i]["Piso"].ToString();
                    string cantidadTotal = MantenimientoInfraestructura.tableInstalacion.Rows[i]["Cantidad Total"].ToString();
                    string malEstado = MantenimientoInfraestructura.tableInstalacion.Rows[i]["Mal Estado"].ToString();
                    string subtotal = MantenimientoInfraestructura.tableInstalacion.Rows[i]["Subtotal"].ToString();
                    string insertMat = @"Insert into DetallesMaterialesMantenimiento (Piso,CantidadTotal,CantidadMalEstado,SubTotal,IdMantenimiento,IdMaterial)" +
                        "values (@Piso, @CantidadTotal, @CantidadMalEstado, @SubTotal, @IdMantenimiento, @IdMaterial)";
                    SqlCommand command = new SqlCommand(insertMat, sqlconn);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Piso", piso);
                    command.Parameters.AddWithValue("@CantidadTotal", cantidadTotal);
                    command.Parameters.AddWithValue("@CantidadMalEstado", malEstado);
                    command.Parameters.AddWithValue("@SubTotal", subtotal);
                    command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
                    command.Parameters.AddWithValue("@IdMaterial", idMaterial);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }

            }


            for (i = 0; i < MantenimientoInfraestructura.tableIlluminacion.Rows.Count; i++)
            {
                if (MantenimientoInfraestructura.tableIlluminacion.Rows[i]["IdMaterial"].ToString() == "")
                {
                    continue;
                }
                else
                {
                    string idMaterial = MantenimientoInfraestructura.tableIlluminacion.Rows[i]["IdMaterial"].ToString();
                    string piso = MantenimientoInfraestructura.tableIlluminacion.Rows[i]["Piso"].ToString();
                    string cantidadTotal = MantenimientoInfraestructura.tableIlluminacion.Rows[i]["Cantidad Total"].ToString();
                    string malEstado = MantenimientoInfraestructura.tableIlluminacion.Rows[i]["Mal Estado"].ToString();
                    string subtotal = MantenimientoInfraestructura.tableIlluminacion.Rows[i]["Subtotal"].ToString();

                    string insertMat = @"Insert into DetallesMaterialesMantenimiento (Piso,CantidadTotal,CantidadMalEstado,SubTotal,IdMantenimiento,IdMaterial)" +
                        "values (@Piso, @CantidadTotal, @CantidadMalEstado, @SubTotal, @IdMantenimiento, @IdMaterial)";
                    SqlCommand command = new SqlCommand(insertMat, sqlconn);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Piso", piso);
                    command.Parameters.AddWithValue("@CantidadTotal", cantidadTotal);
                    command.Parameters.AddWithValue("@CantidadMalEstado", malEstado);
                    command.Parameters.AddWithValue("@SubTotal", subtotal);
                    command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
                    command.Parameters.AddWithValue("@IdMaterial", idMaterial);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }

            }
            Guardar.Visible = false;
        }
    }
}