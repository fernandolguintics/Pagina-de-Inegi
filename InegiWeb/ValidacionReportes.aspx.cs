using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InegiWeb
{
    public partial class ValidacionReportes : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        string nombreEdificio = MenuReportes.nombreEdificio;
        string idReporte = MenuReportes.idReport;
        string accion = MenuReportes.accion;
        static DataTable tableIlluminacion;
        static DataTable tableInstalacion;
        string validacion;
        protected void Page_Load(object sender, EventArgs e)
        {
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaIlluminacion();
                LlenarTablaInstalacion();
                urlRequision.NavigateUrl = "VerPDFOrdenCompra.aspx?id=" + idReporte;
                urlEvidencias.NavigateUrl = "VerPDFEvidencias.aspx?idEvi=" + idReporte;
                if (accion.Equals("Modificar"))
                {
                    if (tipoUsuario.Equals("Tecnico"))
                    {
                        pdfEvidencias.Visible = true;
                        urlEvidencias.Visible = false;
                        pdfRequisicionCompra.Visible = true;
                        urlRequision.Visible = false;
                        validacionDDL.Visible = false;
                        txtValidar.Text = "Validacion:" + validacion;
                        txbDetalles.Visible = true;
                        detallesCompratxt.Visible = false;
                        btnGuardarDatos.Visible = false;
                        btnActualizar.Visible = true;
                        txbObservaciones.Enabled = false;
                    }
                    else
                    {
                        btnGuardarDatos.Visible = false;
                        btnActualizar.Visible = true;
                        pdfEvidencias.Visible = true;
                        urlEvidencias.Visible = false;
                        pdfRequisicionCompra.Visible = true;
                        urlRequision.Visible = false;
                        validacionDDL.Visible = false;
                        txtValidar.Text = "Validacion:" + validacion;
                        detallesCompratxt.Visible = false;
                        txbDetalles.Visible = true;
                        txbObservaciones.Enabled = true;
                    }


                }
                if (validacion.Equals("Aprobado"))
                {

                    validacionDDL.Enabled = false;
                    btnGuardarDatos.Visible = false;
                    txbObservaciones.Enabled = false;
                }


                if (accion.Equals("Ver"))
                {
                    if (tipoUsuario.Equals("Tecnico"))
                    {
                        validacionDDL.Visible = false;
                        btnGuardarDatos.Visible = false;
                        txtValidar.Text = "Validacion:" + validacion;
                        txbObservaciones.Enabled = false;
                    }
                }
            }
        }
        private void LlenarTablaInstalacion()
        {
            tableInstalacion = new DataTable();

            tableInstalacion.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("Subtotal", typeof(string)),
                new DataColumn("Subtotal2", typeof(string))

            });
            string getMateriales = "select DetallesReporte.IdMaterial, nombreMaterial,FORMAT( precio,'C','en-us') as 'precio',Arreglados,FORMAT( Subtotal,'C','en-us') as 'subtotal',Subtotal from DetallesReporte inner join Materiales on DetallesReporte.IdMaterial = Materiales.IdMaterial where IdReporte = @IdReporte and idCategoria = 1003";
            SqlCommand command = new SqlCommand(getMateriales, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdReporte", idReporte);
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableInstalacion.Rows.Add(
                        dr["IdMaterial"].ToString(),
                        dr["nombreMaterial"].ToString(),
                        dr["precio"].ToString(),
                        dr["Arreglados"].ToString(),
                        dr["subtotal"].ToString(),
                        dr["Subtotal"].ToString()
                    );
                }
            }
            command.Connection.Close();
            GVMaterialesInstalacion.DataSource = tableInstalacion;
            GVMaterialesInstalacion.DataBind();



        }

        private void LlenarTablaIlluminacion()
        {
            tableIlluminacion = new DataTable();

            tableIlluminacion.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("Subtotal", typeof(string)),
                new DataColumn("Subtotal2", typeof(string)),
            });

            string getMateriales = " select DetallesReporte.IdMaterial,nombreMaterial,FORMAT( precio,'C','en-us') as 'precio',Arreglados,FORMAT( Subtotal,'C','en-us') as 'subtotal', Subtotal from DetallesReporte inner join Materiales on DetallesReporte.IdMaterial = Materiales.IdMaterial where IdReporte = @IdReporte and idCategoria = 1";
            SqlCommand command = new SqlCommand(getMateriales, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdReporte", idReporte);
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableIlluminacion.Rows.Add(
                       dr["IdMaterial"].ToString(),
                       dr["nombreMaterial"].ToString(),
                       dr["precio"].ToString(),
                       dr["Arreglados"].ToString(),
                       dr["subtotal"].ToString(),
                       dr["Subtotal"].ToString()
                   );
                }
            }
            command.Connection.Close();
            GVMaterialIlluminacion.DataSource = tableIlluminacion;
            GVMaterialIlluminacion.DataBind();
        }

        private void ObtenerDatos()
        {

            String query = "select NombreEdificio,Direccion,Posesion,Ocupacion,Superficie,Uso,Niveles,Cajones,Antiguedad,Terreno from Edificios where NombreEdificio = @NombreEdificio";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@NombreEdificio", nombreEdificio);
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
            tituloFecha.Text = "Fecha:" + MenuReportes.fechaReport;
            command.Connection.Close();

            string getDetalles = "select DetallesdeCompra from Reportes inner join OrdenCompra on Reportes.IdOrdenCompra = OrdenCompra.IdOrdenCompra where IdReporte =@IdReporte ";
            SqlCommand command2 = new SqlCommand(getDetalles, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdReporte", idReporte);
            SqlDataReader dr = command2.ExecuteReader();
            while (dr.Read())
            {
                detallesCompratxt.Text = dr["DetallesdeCompra"].ToString();
            }
            command2.Connection.Close();

            string getValidacion = "select Validacion,Observaciones from Reportes where IdReporte =@Id";
            SqlCommand command3 = new SqlCommand(getValidacion, sqlconn);
            command3.Connection.Open();
            command3.Parameters.AddWithValue("@Id", idReporte);
            SqlDataReader dataReader = command3.ExecuteReader();
            if (dataReader.Read())
            {
                validacion = dataReader["Validacion"].ToString();
                validacionDDL.SelectedItem.Text = dataReader["Validacion"].ToString();
                txbObservaciones.Text = dataReader["Observaciones"].ToString();

            }

            command3.Connection.Close();
        }

        protected void btnGuardarDatos_Click(object sender, EventArgs e)
        {
            string updateValidation = "update Reportes set Validacion = @Validacion where IdReporte =@IdReporte";
            SqlCommand command = new SqlCommand(updateValidation, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@Validacion", validacionDDL.Text);
            command.Parameters.AddWithValue("@IdReporte", idReporte);
            command.ExecuteNonQuery();

            command.Connection.Close();
            if (validacionDDL.Text == "Aprobado")
            {
                ObtenerDatosMantenimiento();
            }
            else
            {
                Response.Redirect("MenuReportes.aspx", false);
            }

        }

        private void ObtenerDatosMantenimiento()
        {
            string getIdMantenimiento = "select IdMantenimiento from Reportes where IdReporte = @IdReporte";
            string idMantenimiento = "";
            SqlCommand command = new SqlCommand(getIdMantenimiento, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdReporte", idReporte);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                idMantenimiento = dr["IdMantenimiento"].ToString();
            }
            command.Connection.Close();

            DataTable tableMaterialesMantenimiento = new DataTable();
            tableMaterialesMantenimiento.Columns.AddRange(new DataColumn[]{

                new DataColumn("IdDetallesMaterialesMantenimiento", typeof(string)),
                new DataColumn("CantidadTotal", typeof(string)),
                new DataColumn("CantidadMalEstado", typeof(string)),
                new DataColumn("Subtotal", typeof(string)),
                new DataColumn("IdMaterial", typeof(string)),

            });

            string getMaterialesMant = "Select * from DetallesMaterialesMantenimiento where IdMantenimiento =@IdMantenimiento";
            SqlCommand command2 = new SqlCommand(getMaterialesMant, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableMaterialesMantenimiento.Rows.Add(

                       dr2["IdDetallesMaterialesMantenimiento"].ToString(),
                       dr2["CantidadTotal"].ToString(),
                       dr2["CantidadMalEstado"].ToString(),
                       dr2["Subtotal"].ToString(),
                       dr2["IdMaterial"].ToString()
                   );
                }
            }
            command2.Connection.Close();

            int totalMalEstadoInstalacion = 0, totalMalEstadoIlluminacion = 0, costoTotal = 0, totalInstalacion = 0, totalIlluminacion = 0; //Variables para almacenar totales

            for (int i = 0; i < tableMaterialesMantenimiento.Rows.Count; i++)
            {
                for (int j = 0; j < tableIlluminacion.Rows.Count; j++)
                {
                    if (tableMaterialesMantenimiento.Rows[i]["IdMaterial"].ToString() == tableIlluminacion.Rows[j]["Id"].ToString())
                    {
                        totalIlluminacion += Convert.ToInt32(tableMaterialesMantenimiento.Rows[i]["CantidadTotal"].ToString());
                        int restaEstado = Convert.ToInt32(tableMaterialesMantenimiento.Rows[i]["CantidadMalEstado"].ToString()) - Convert.ToInt32(tableIlluminacion.Rows[j]["Arreglados"].ToString());
                        int restaSubtotal = Convert.ToInt32(tableMaterialesMantenimiento.Rows[i]["Subtotal"].ToString()) - Convert.ToInt32(tableIlluminacion.Rows[j]["Subtotal2"].ToString());
                        tableMaterialesMantenimiento.Rows[i]["CantidadMalEstado"] = restaEstado;
                        tableMaterialesMantenimiento.Rows[i]["Subtotal"] = restaSubtotal;
                        totalMalEstadoIlluminacion += restaEstado;
                        costoTotal += restaSubtotal;
                    }

                }

                for (int j = 0; j < tableInstalacion.Rows.Count; j++)
                {
                    if (tableMaterialesMantenimiento.Rows[i]["IdMaterial"].ToString() == tableInstalacion.Rows[j]["Id"].ToString())
                    {
                        totalInstalacion += Convert.ToInt32(tableMaterialesMantenimiento.Rows[i]["CantidadTotal"].ToString());
                        int restaEstado = Convert.ToInt32(tableMaterialesMantenimiento.Rows[i]["CantidadMalEstado"].ToString()) - Convert.ToInt32(tableInstalacion.Rows[j]["Arreglados"].ToString());
                        int restaSubtotal = Convert.ToInt32(tableMaterialesMantenimiento.Rows[i]["Subtotal"].ToString()) - Convert.ToInt32(tableInstalacion.Rows[j]["Subtotal2"].ToString());
                        tableMaterialesMantenimiento.Rows[i]["CantidadMalEstado"] = restaEstado;
                        tableMaterialesMantenimiento.Rows[i]["Subtotal"] = restaSubtotal;
                        totalMalEstadoInstalacion += restaEstado;
                        costoTotal += restaSubtotal;
                    }

                }
            }

            double evaluacionInstalacion = (1.00 - (Convert.ToDouble(totalMalEstadoInstalacion) / Convert.ToDouble(totalInstalacion))) * 100.00;
            double evaluacionIlluminacion = (1.00 - (Convert.ToDouble(totalMalEstadoIlluminacion) / Convert.ToDouble(totalIlluminacion))) * 100.00;
            for (int i = 0; i < tableMaterialesMantenimiento.Rows.Count; i++)
            {
                string idDetalles = tableMaterialesMantenimiento.Rows[i]["IdDetallesMaterialesMantenimiento"].ToString();
                string malEstado = tableMaterialesMantenimiento.Rows[i]["CantidadMalEstado"].ToString();
                string subtotal = tableMaterialesMantenimiento.Rows[i]["Subtotal"].ToString();

                string updateDetalles = "update DetallesMaterialesMantenimiento set CantidadMalEstado =@Cantidad, Subtotal=@Subtotal where IdDetallesMaterialesMantenimiento = @Id";
                SqlCommand command3 = new SqlCommand(updateDetalles, sqlconn);
                command3.Connection.Open();
                command3.Parameters.AddWithValue("@Cantidad", malEstado);
                command3.Parameters.AddWithValue("@Subtotal", subtotal);
                command3.Parameters.AddWithValue("@ID", idDetalles);
                command3.ExecuteNonQuery();
                command3.Connection.Close();
            }

            string updateMantenimiento = "update Mantenimientos set Defectuosos = @Defectuosos, Evaluacion =@Evaluacion, CostoTotal=@Costototal where IdMantenimiento=@Id";
            SqlCommand command4 = new SqlCommand(updateMantenimiento, sqlconn);
            command4.Connection.Open();
            command4.Parameters.AddWithValue("@Defectuosos", totalMalEstadoInstalacion + totalMalEstadoIlluminacion);
            command4.Parameters.AddWithValue("@Evaluacion", (evaluacionInstalacion + evaluacionIlluminacion) / 2.00);
            command4.Parameters.AddWithValue("@CostoTotal", costoTotal);
            command4.Parameters.AddWithValue("@Id", idMantenimiento);
            command4.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se han guardado los cambios');", true);
            Response.Redirect("MenuReportes.aspx", false);
            command4.Connection.Close();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            if (tipoUsuario.Equals("Administrador"))
            {
                string updateObservaciones = "Update Reportes set Observaciones = @Observaciones where IdReporte = @Id";
                SqlCommand comm = new SqlCommand(updateObservaciones, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@Observaciones", txbObservaciones.Text);
                comm.Parameters.AddWithValue("@Id", idReporte);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
                Response.Redirect("MenuReportes.aspx", false);
            }
            else
            {
                if (pdfEvidencias.HasFile && pdfRequisicionCompra.HasFiles)
                {
                    if (txbDetalles.Equals(""))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pueden dejar en blanco los detalles');", true);
                        return;
                    }
                    else
                    {
                        string getIdCompra = "select IdOrdenCompra from Reportes where IdReporte =@Id";
                        SqlCommand command = new SqlCommand(getIdCompra, sqlconn);
                        command.Connection.Open();
                        command.Parameters.AddWithValue("@Id", idReporte);
                        SqlDataReader dr = command.ExecuteReader();
                        if (dr.Read())
                        {
                            string idCompra = dr["IdOrdenCompra"].ToString();
                            command.Connection.Close();
                            ActualizarOrdenCompra(idCompra);
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se guardaron los cambios con exito');", true);
                        Response.Redirect("MenuReportes.aspx", false);

                    }
                }
                else
                {
                    mpeErrorCarga.Show();
                }
            }


        }

        private void ActualizarOrdenCompra(string id)
        {
            string updateOrden = "update OrdenCompra set DetallesdeCompra=@Detalles, ImageInstalacion=@Image, TicketdeCompra =@Ticket where IdOrdenCompra=@Id";
            SqlCommand command2 = new SqlCommand(updateOrden, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@Detalles", txbDetalles.Text);
            command2.Parameters.AddWithValue("@Image", SqlDbType.VarBinary).Value = pdfEvidencias.FileBytes;
            command2.Parameters.AddWithValue("@Ticket", SqlDbType.VarBinary).Value = pdfRequisicionCompra.FileBytes;
            command2.Parameters.AddWithValue("@Id", id);
            command2.ExecuteNonQuery();
            command2.Connection.Close();
        }
    }
}