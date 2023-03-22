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
    public partial class ValidacionReporteServicio : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        string nombreEdificio = MenuReportesServicio.nombreEdificio;
        string idReporte = MenuReportesServicio.idReport;
        string accion = MenuReportesServicio.accion;
        string validacion;
        static DataTable tableCarp;
        static DataTable tableHerr;
        static DataTable tableOtros;
        static DataTable tablePisos;
        static DataTable tableHidro;
        static DataTable tablePintura;
        protected void Page_Load(object sender, EventArgs e)
        {
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaCarpinteria();
                LlenarTablaHerreria();
                LLenarTablaPintura();
                LLenarTablaHidrosanitarias();
                LLenarTablaPisos();
                LLenarTablaOtros();
                urlRequision.NavigateUrl = "VerPDFOrdenCompraServicio.aspx?id=" + idReporte;
                urlEvidencias.NavigateUrl = "VerPDFEvidenciasServicio.aspx?id=" + idReporte;
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
                    btnActualizar.Visible = false;

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
        private void LLenarTablaOtros()
        {
            tableOtros = new DataTable();
            tableOtros.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
            });

            string select = @"select IdCriterioServicio,Criterio,DetallesReporteServicio.Piso,CantidadTotal,MalEstado,Arreglados, DetallesMantenimientoServicio.CambioNormativa,DetallesMantenimientoServicio.Observacion from DetallesReporteServicio inner join
                                                    CriteriosServicio on DetallesReporteServicio.IdCriterio = CriteriosServicio.IdCriterioServicio inner join
                                                    DetallesMantenimientoServicio on CriteriosServicio.IdCriterioServicio = DetallesMantenimientoServicio.IdCriterio
                                                    where IdReporte = @idreporte and IdMantenimiento = @idmant and Categoria = 'Otros'";
            SqlCommand command2 = new SqlCommand(select, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@idreporte", idReporte);
            command2.Parameters.AddWithValue("@idmant", MenuReportesServicio.idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableOtros.Rows.Add(
                        dr2["IdCriterioServicio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["Arreglados"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();
            if (accion == "Modificar")
            {
                GVOtros.Visible = false;
                GVOtros.Visible = true;
                GVOtros2.DataSource = tableCarp;
                GVOtros2.DataBind();
            }
            else
            {
                GVOtros.DataSource = tableCarp;
                GVOtros.DataBind();
            }
        }

        private void LLenarTablaPisos()
        {
            tablePisos = new DataTable();
            tablePisos.Columns.AddRange(new DataColumn[]{
              new DataColumn("IdCriterio", typeof(string)),
              new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
            });

            string select = @"select IdCriterioServicio,Criterio,DetallesReporteServicio.Piso,CantidadTotal,MalEstado,Arreglados, DetallesMantenimientoServicio.CambioNormativa,DetallesMantenimientoServicio.Observacion from DetallesReporteServicio inner join
                                                    CriteriosServicio on DetallesReporteServicio.IdCriterio = CriteriosServicio.IdCriterioServicio inner join
                                                    DetallesMantenimientoServicio on CriteriosServicio.IdCriterioServicio = DetallesMantenimientoServicio.IdCriterio
                                                    where IdReporte = @idreporte and IdMantenimiento = @idmant and Categoria = 'Piso,Plafones y Techo'";
            SqlCommand command2 = new SqlCommand(select, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@idreporte", idReporte);
            command2.Parameters.AddWithValue("@idmant", MenuReportesServicio.idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tablePisos.Rows.Add(
                        dr2["IdCriterioServicio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["Arreglados"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            if (accion == "Modificar")
            {
                GVPisos.Visible = false;
                GVPisos2.Visible = true;
                GVPisos2.DataSource = tableCarp;
                GVPisos2.DataBind();
            }
            else
            {
                GVPisos.DataSource = tableCarp;
                GVPisos.DataBind();
            }
        }

        private void LLenarTablaHidrosanitarias()
        {
            tableHidro = new DataTable();
            tableHidro.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
            });

            string select = @"select IdCriterioServicio,Criterio,DetallesReporteServicio.Piso,CantidadTotal,MalEstado,Arreglados, DetallesMantenimientoServicio.CambioNormativa,DetallesMantenimientoServicio.Observacion from DetallesReporteServicio inner join
                                                    CriteriosServicio on DetallesReporteServicio.IdCriterio = CriteriosServicio.IdCriterioServicio inner join
                                                    DetallesMantenimientoServicio on CriteriosServicio.IdCriterioServicio = DetallesMantenimientoServicio.IdCriterio
                                                    where IdReporte = @idreporte and IdMantenimiento = @idmant and Categoria = 'Instalaciones Hidrosanitarias'";
            SqlCommand command2 = new SqlCommand(select, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@idreporte", idReporte);
            command2.Parameters.AddWithValue("@idmant", MenuReportesServicio.idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableHidro.Rows.Add(
                        dr2["IdCriterioServicio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["Arreglados"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            if (accion == "Modificar")
            {
                GVHidrosanitarias.Visible = false;
                GVHidrosanitarias2.Visible = true;
                GVHidrosanitarias2.DataSource = tableCarp;
                GVHidrosanitarias2.DataBind();
            }
            else
            {
                GVHidrosanitarias.DataSource = tableCarp;
                GVHidrosanitarias.DataBind();
            }
        }

        private void LLenarTablaPintura()
        {
            tablePintura = new DataTable();
            tablePintura.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
                 new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
            });

            string select = @"select IdCriterioServicio,Criterio,DetallesReporteServicio.Piso,CantidadTotal,MalEstado,Arreglados, DetallesMantenimientoServicio.CambioNormativa,DetallesMantenimientoServicio.Observacion from DetallesReporteServicio inner join
                                                    CriteriosServicio on DetallesReporteServicio.IdCriterio = CriteriosServicio.IdCriterioServicio inner join
                                                    DetallesMantenimientoServicio on CriteriosServicio.IdCriterioServicio = DetallesMantenimientoServicio.IdCriterio
                                                    where IdReporte = @idreporte and IdMantenimiento = @idmant and Categoria = 'Pintura'";
            SqlCommand command2 = new SqlCommand(select, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@idreporte", idReporte);
            command2.Parameters.AddWithValue("@idmant", MenuReportesServicio.idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tablePintura.Rows.Add(
                        dr2["IdCriterioServicio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["Arreglados"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            if (accion == "Modificar")
            {
                GVPintura.Visible = false;
                GVPintura2.Visible = true;
                GVPintura2.DataSource = tableCarp;
                GVPintura2.DataBind();
            }
            else
            {
                GVPintura.DataSource = tableCarp;
                GVPintura.DataBind();
            }
        }

        private void LlenarTablaHerreria()
        {
            tableHerr = new DataTable();
            tableHerr.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
                 new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
            });

            string select = @"select IdCriterioServicio,Criterio,DetallesReporteServicio.Piso,CantidadTotal,MalEstado,Arreglados, DetallesMantenimientoServicio.CambioNormativa,DetallesMantenimientoServicio.Observacion from DetallesReporteServicio inner join
                                                    CriteriosServicio on DetallesReporteServicio.IdCriterio = CriteriosServicio.IdCriterioServicio inner join
                                                    DetallesMantenimientoServicio on CriteriosServicio.IdCriterioServicio = DetallesMantenimientoServicio.IdCriterio
                                                    where IdReporte = @idreporte and IdMantenimiento = @idmant and Categoria = 'Herrería'";
            SqlCommand command2 = new SqlCommand(select, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@idreporte", idReporte);
            command2.Parameters.AddWithValue("@idmant", MenuReportesServicio.idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableHerr.Rows.Add(
                        dr2["IdCriterioServicio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["Arreglados"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            if (accion == "Modificar")
            {
                GVHerreria.Visible = false;
                GVHerreria2.Visible = true;
                GVHerreria2.DataSource = tableHerr;
                GVHerreria2.DataBind();
            }
            else
            {
                GVHerreria.DataSource = tableHerr;
                GVHerreria.DataBind();
            }
        }

        private void LlenarTablaCarpinteria()
        {
            tableCarp = new DataTable();
            tableCarp.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
            });

            string select = @"select IdCriterioServicio,Criterio,DetallesReporteServicio.Piso,CantidadTotal,MalEstado,Arreglados, DetallesMantenimientoServicio.CambioNormativa,DetallesMantenimientoServicio.Observacion from DetallesReporteServicio inner join
                                                    CriteriosServicio on DetallesReporteServicio.IdCriterio = CriteriosServicio.IdCriterioServicio inner join
                                                    DetallesMantenimientoServicio on CriteriosServicio.IdCriterioServicio = DetallesMantenimientoServicio.IdCriterio
                                                    where IdReporte = @idreporte and IdMantenimiento = @idmant and Categoria = 'Carpintería y Cristales'";
            SqlCommand command2 = new SqlCommand(select, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@idreporte", idReporte);
            command2.Parameters.AddWithValue("@idmant", MenuReportesServicio.idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableCarp.Rows.Add(
                         dr2["IdCriterioServicio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["Arreglados"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            if (accion == "Modificar")
            {
                GVCarpinteria.Visible = false;
                GVCarpinteria2.Visible = true;
                GVCarpinteria2.DataSource = tableCarp;
                GVCarpinteria2.DataBind();
            }
            else
            {
                GVCarpinteria.DataSource = tableCarp;
                GVCarpinteria.DataBind();
            }

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
            tituloFecha.Text = "Fecha:" + MenuReportesServicio.fechaReport;
            command.Connection.Close();

            string getDetalles = "select DetallesdeCompra from ReportesServicios inner join OrdenesCompraServicios on ReportesServicios.IdOrdenCompra = OrdenesCompraServicios.IdOrdenCompraServicio where IdReporteServicios =@IdReporte ";
            SqlCommand command2 = new SqlCommand(getDetalles, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdReporte", idReporte);
            SqlDataReader dr = command2.ExecuteReader();
            while (dr.Read())
            {
                detallesCompratxt.Text = dr["DetallesdeCompra"].ToString();
            }
            command2.Connection.Close();

            string getValidacion = "select Validacion,Observaciones from ReportesServicios where IdReporteServicios =@Id";
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
            string updateValidation = "update ReportesServicios set Validacion = @Validacion, @Observacion=@observacion where IdReporteServicios =@IdReporte";
            SqlCommand command = new SqlCommand(updateValidation, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@Validacion", validacionDDL.Text);
            command.Parameters.AddWithValue("@Observacion", txbObservaciones.Text);
            command.Parameters.AddWithValue("@IdReporte", idReporte);
            command.ExecuteNonQuery();

            command.Connection.Close();
            if (validacionDDL.Text == "Aprobado")
            {
                ActualizarMantenimiento();
            }
            else
            {
                Response.Redirect("MenuReportesServicio.aspx", false);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se han guardado los cambios');", true);
            Response.Redirect("MenuReportesServicio.aspx", false);
        }

        private void ActualizarMantenimiento()
        {

            string getIdMant = "select IdMantenimiento from ReportesServicios where IdReporteServicios=@Id";
            SqlCommand comm = new SqlCommand(getIdMant, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@Id", idReporte);
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                string idMantenimiento = dr["IdMantenimiento"].ToString();
                comm.Connection.Close();
                ActualizarDetalles(idMantenimiento);
            }

        }

        private void ActualizarDetalles(string id)
        {
            int total = 0;
            int totalMalEstado = 0;
            DataTable tableMantenimiento = new DataTable();
            tableMantenimiento.Columns.AddRange(new DataColumn[]{

                new DataColumn("IdDetalle", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Normativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Criterio", typeof(string)),

            });

            string getMaterialesMant = "Select * from DetallesMantenimientoServicio where IdMantenimiento =@IdMantenimiento";
            SqlCommand command2 = new SqlCommand(getMaterialesMant, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", id);
            SqlDataReader dr2 = command2.ExecuteReader();
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableMantenimiento.Rows.Add(
                       dr2["IdDetalleMantenimiento"].ToString(),
                       dr2["Piso"].ToString(),
                       dr2["CantidadTotal"].ToString(),
                       dr2["MalEstado"].ToString(),
                       dr2["CambioNormativa"].ToString(),
                       dr2["Observacion"].ToString(),
                       dr2["IdCriterio"].ToString()
                   );
                }
            }
            command2.Connection.Close();
            //Ciclo que valora las tablas
            for (int i = 0; i < tableMantenimiento.Rows.Count; i++)
            {
                for (int j = 0; j < tableCarp.Rows.Count; j++)
                {
                    if (tableMantenimiento.Rows[i][6].ToString() == tableCarp.Rows[j][0].ToString())
                    {
                        total += Convert.ToInt32(tableMantenimiento.Rows[i]["Cantidad"].ToString());
                        int restaEstado = Convert.ToInt32(tableMantenimiento.Rows[i]["MalEstado"].ToString()) - Convert.ToInt32(tableCarp.Rows[j]["Arreglados"].ToString());
                        tableMantenimiento.Rows[i]["Piso"] = tableCarp.Rows[j]["Piso"].ToString();
                        tableMantenimiento.Rows[i]["MalEstado"] = restaEstado;
                        tableMantenimiento.Rows[i]["Normativa"] = tableCarp.Rows[j]["CambioNormativa"].ToString();
                        tableMantenimiento.Rows[i]["Observacion"] = tableCarp.Rows[j]["Observacion"].ToString();
                        totalMalEstado += restaEstado;

                    }

                }

                for (int j = 0; j < tableHerr.Rows.Count; j++)
                {
                    if (tableMantenimiento.Rows[i][6].ToString() == tableHerr.Rows[j][0].ToString())
                    {
                        total += Convert.ToInt32(tableMantenimiento.Rows[i]["Cantidad"].ToString());
                        int restaEstado = Convert.ToInt32(tableMantenimiento.Rows[i]["MalEstado"].ToString()) - Convert.ToInt32(tableHerr.Rows[j]["Arreglados"].ToString());
                        tableMantenimiento.Rows[i]["Piso"] = tableHerr.Rows[j]["Piso"].ToString();
                        tableMantenimiento.Rows[i]["MalEstado"] = restaEstado;
                        tableMantenimiento.Rows[i]["Normativa"] = tableHerr.Rows[j]["CambioNormativa"].ToString();
                        tableMantenimiento.Rows[i]["Observacion"] = tableHerr.Rows[j]["Observacion"].ToString();
                        totalMalEstado += restaEstado;

                    }

                }

                for (int j = 0; j < tablePintura.Rows.Count; j++)
                {
                    if (tableMantenimiento.Rows[i][6].ToString() == tablePintura.Rows[j][0].ToString())
                    {
                        total += Convert.ToInt32(tableMantenimiento.Rows[i]["Cantidad"].ToString());
                        int restaEstado = Convert.ToInt32(tableMantenimiento.Rows[i]["MalEstado"].ToString()) - Convert.ToInt32(tablePintura.Rows[j]["Arreglados"].ToString());
                        tableMantenimiento.Rows[i]["Piso"] = tablePintura.Rows[j]["Piso"].ToString();
                        tableMantenimiento.Rows[i]["MalEstado"] = restaEstado;
                        tableMantenimiento.Rows[i]["Normativa"] = tablePintura.Rows[j]["CambioNormativa"].ToString();
                        tableMantenimiento.Rows[i]["Observacion"] = tablePintura.Rows[j]["Observacion"].ToString();
                        totalMalEstado += restaEstado;

                    }

                }

                for (int j = 0; j < tableHidro.Rows.Count; j++)
                {
                    if (tableMantenimiento.Rows[i][6].ToString() == tableHidro.Rows[j][0].ToString())
                    {
                        total += Convert.ToInt32(tableMantenimiento.Rows[i]["Cantidad"].ToString());
                        int restaEstado = Convert.ToInt32(tableMantenimiento.Rows[i]["MalEstado"].ToString()) - Convert.ToInt32(tableHidro.Rows[j]["Arreglados"].ToString());
                        tableMantenimiento.Rows[i]["Piso"] = tableHidro.Rows[j]["Piso"].ToString();
                        tableMantenimiento.Rows[i]["MalEstado"] = restaEstado;
                        tableMantenimiento.Rows[i]["Normativa"] = tableHidro.Rows[j]["CambioNormativa"].ToString();
                        tableMantenimiento.Rows[i]["Observacion"] = tableHidro.Rows[j]["Observacion"].ToString();
                        totalMalEstado += restaEstado;

                    }

                }

                for (int j = 0; j < tablePisos.Rows.Count; j++)
                {
                    if (tableMantenimiento.Rows[i][6].ToString() == tablePisos.Rows[j][0].ToString())
                    {
                        total += Convert.ToInt32(tableMantenimiento.Rows[i]["Cantidad"].ToString());
                        int restaEstado = Convert.ToInt32(tableMantenimiento.Rows[i]["MalEstado"].ToString()) - Convert.ToInt32(tablePisos.Rows[j]["Arreglados"].ToString());
                        tableMantenimiento.Rows[i]["Piso"] = tablePisos.Rows[j]["Piso"].ToString();
                        tableMantenimiento.Rows[i]["MalEstado"] = restaEstado;
                        tableMantenimiento.Rows[i]["Normativa"] = tablePisos.Rows[j]["CambioNormativa"].ToString();
                        tableMantenimiento.Rows[i]["Observacion"] = tablePisos.Rows[j]["Observacion"].ToString();
                        totalMalEstado += restaEstado;

                    }

                }

                for (int j = 0; j < tableOtros.Rows.Count; j++)
                {
                    if (tableMantenimiento.Rows[i][6].ToString() == tableOtros.Rows[j][0].ToString())
                    {
                        total += Convert.ToInt32(tableMantenimiento.Rows[i]["Cantidad"].ToString());
                        int restaEstado = Convert.ToInt32(tableMantenimiento.Rows[i]["MalEstado"].ToString()) - Convert.ToInt32(tableOtros.Rows[j]["Arreglados"].ToString());
                        tableMantenimiento.Rows[i]["Piso"] = tableOtros.Rows[j]["Piso"].ToString();
                        tableMantenimiento.Rows[i]["MalEstado"] = restaEstado;
                        tableMantenimiento.Rows[i]["Normativa"] = tableOtros.Rows[j]["CambioNormativa"].ToString();
                        tableMantenimiento.Rows[i]["Observacion"] = tableOtros.Rows[j]["Observacion"].ToString();
                        totalMalEstado += restaEstado;

                    }
                }
            }
            //Guarda los detalles
            for (int i = 0; i < tableMantenimiento.Rows.Count; i++)
            {
                string updateDetalles = @"update DetallesMantenimientoServicio set Piso=@piso, MalEstado=@mal, 
                CambioNormativa=@cambio,Observacion=@observacion where IdDetalleMantenimiento = @Id";
                SqlCommand command3 = new SqlCommand(updateDetalles, sqlconn);
                command3.Connection.Open();
                command3.Parameters.AddWithValue("@Piso", tableMantenimiento.Rows[i][1].ToString());
                command3.Parameters.AddWithValue("mal", tableMantenimiento.Rows[i][3].ToString());
                command3.Parameters.AddWithValue("@cambio", tableMantenimiento.Rows[i][4].ToString());
                command3.Parameters.AddWithValue("@observacion", tableMantenimiento.Rows[i][5].ToString());
                command3.Parameters.AddWithValue("@Id", tableMantenimiento.Rows[i][0].ToString());
                command3.ExecuteNonQuery();
                command3.Connection.Close();
            }
            double evaluacion = (1.00 - (Convert.ToDouble(totalMalEstado) / Convert.ToDouble(total))) * 100.00;
            string updateDetalles2 = "update MantenimientoServicios set EvaluacionFinal=@evaluacion where IdMantenimientoServicio=@id";
            SqlCommand comm = new SqlCommand(updateDetalles2, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@evaluacion", evaluacion);
            comm.Parameters.AddWithValue("@id", id);
            comm.ExecuteNonQuery();
            comm.Connection.Close();

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            if (tipoUsuario.Equals("Administrador"))
            {
                string updateObservaciones = "Update ReportesServicios set Observaciones = @Observaciones where IdReporteServicios = @Id";
                SqlCommand comm = new SqlCommand(updateObservaciones, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@Observaciones", txbObservaciones.Text);
                comm.Parameters.AddWithValue("@Id", idReporte);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
                Response.Redirect("MenuReportesServicio.aspx", false);
            }
            else
            {
                if (pdfEvidencias.HasFile && pdfRequisicionCompra.HasFiles)
                {
                    string getIdCompra = "select IdOrdenCompra from ReportesServicios where IdReporteServicios =@Id";
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
                    ActualizarReporte();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se han guardado los cambios');", true);
                    Response.Redirect("MenuReportesServicio.aspx", false);
                }
                else
                {
                    mpeErrorCarga.Show();
                }
            }
        }

        private void ActualizarReporte()
        {
            int arregladosTotal = 0;

            DataRow rowTable;
            DataTable tbCarpinteria = new DataTable();
            DataTable tbHerreria = new DataTable();
            DataTable tbPintura = new DataTable();
            DataTable tbHidro = new DataTable();
            DataTable tbPisos = new DataTable();
            DataTable tbOtros = new DataTable();

            tbCarpinteria.Columns.AddRange(new DataColumn[]{
               new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            tbHerreria = tbCarpinteria.Clone();
            tbPintura = tbCarpinteria.Clone();
            tbHidro = tbCarpinteria.Clone();
            tbPisos = tbCarpinteria.Clone();
            tbOtros = tbCarpinteria.Clone();

            foreach (GridViewRow gvr in GVCarpinteria2.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var arreglados = ((TextBox)gvr.FindControl("arreglados")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;

                if (arreglados.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar espacios en blanco en arreglados');", true);
                    return;
                }
                else
                {
                    arregladosTotal += Convert.ToInt32(arreglados);
                    rowTable = tbCarpinteria.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = arreglados;
                    rowTable[3] = normativa;
                    rowTable[4] = observacion;
                    tbCarpinteria.Rows.Add(rowTable);

                }

            }

            foreach (GridViewRow gvr in GVHerreria2.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var arreglados = ((TextBox)gvr.FindControl("arreglados")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (arreglados.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar espacios en blanco en arreglados');", true);
                    return;
                }
                else
                {
                    arregladosTotal += Convert.ToInt32(arreglados);
                    rowTable = tbHerreria.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = arreglados;
                    rowTable[3] = normativa;
                    rowTable[4] = observacion;
                    tbHerreria.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVPintura2.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var arreglados = ((TextBox)gvr.FindControl("arreglados")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (arreglados.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar espacios en blanco en arreglados');", true);
                    return;
                }
                else
                {
                    arregladosTotal += Convert.ToInt32(arreglados);
                    rowTable = tbPintura.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = arreglados;
                    rowTable[3] = normativa;
                    rowTable[4] = observacion;
                    tbPintura.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVHidrosanitarias2.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var arreglados = ((TextBox)gvr.FindControl("arreglados")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (arreglados.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar espacios en blanco en arreglados');", true);
                    return;
                }
                else
                {
                    arregladosTotal += Convert.ToInt32(arreglados);
                    rowTable = tbHidro.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = arreglados;
                    rowTable[3] = normativa;
                    rowTable[4] = observacion;
                    tbHidro.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVPisos2.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var arreglados = ((TextBox)gvr.FindControl("arreglados")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (arreglados.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar espacios en blanco en arreglados');", true);
                    return;
                }
                else
                {
                    arregladosTotal += Convert.ToInt32(arreglados);
                    rowTable = tbPisos.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = arreglados;
                    rowTable[3] = normativa;
                    rowTable[4] = observacion;
                    tbPisos.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVOtros2.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var arreglados = ((TextBox)gvr.FindControl("arreglados")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (arreglados.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar espacios en blanco en arreglados');", true);
                    return;
                }
                else
                {
                    arregladosTotal += Convert.ToInt32(arreglados);
                    rowTable = tbOtros.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = arreglados;
                    rowTable[3] = normativa;
                    rowTable[4] = observacion;
                    tbOtros.Rows.Add(rowTable);
                }

            }


            string updateReporte = @"update ReportesServicios set Arreglados=@arreglados where IdReporteServicios=@Id)";

            SqlCommand comm = new SqlCommand(updateReporte, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@Id", idReporte);
            comm.Parameters.AddWithValue("@arreglados", arregladosTotal);

            comm.ExecuteNonQuery();
            comm.Connection.Close();

            string insert = @"update DetallesReporteServicio set CambioNormativa=@cambio,Observacion=@observacion,
                            Piso=@piso,Arreglados=@arreglados where IdReporte=@idreporte and IdCriterio=@idcriterio";

            for (int i = 0; i < tbCarpinteria.Rows.Count; i++)
            {

                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@cambio", tbCarpinteria.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbCarpinteria.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idreporte", idReporte);
                comm3.Parameters.AddWithValue("@piso", tbCarpinteria.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@arreglados", tbCarpinteria.Rows[i]["Arreglados"].ToString());
                comm3.Parameters.AddWithValue("@idcriterio", tbCarpinteria.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }
            for (int i = 0; i < tbHerreria.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@cambio", tbHerreria.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbHerreria.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idreporte", idReporte);
                comm3.Parameters.AddWithValue("@piso", tbHerreria.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@arreglados", tbHerreria.Rows[i]["Arreglados"].ToString());
                comm3.Parameters.AddWithValue("@idcriterio", tbHerreria.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }
            for (int i = 0; i < tbPintura.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@cambio", tbPintura.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbPintura.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idreporte", idReporte);
                comm3.Parameters.AddWithValue("@piso", tbPintura.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@arreglados", tbPintura.Rows[i]["Arreglados"].ToString());
                comm3.Parameters.AddWithValue("@idcriterio", tbPintura.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }

            for (int i = 0; i < tbHidro.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@cambio", tbHidro.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbHidro.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idreporte", idReporte);
                comm3.Parameters.AddWithValue("@piso", tbHidro.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@arreglados", tbHidro.Rows[i]["Arreglados"].ToString());
                comm3.Parameters.AddWithValue("@idcriterio", tbHidro.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }

            for (int i = 0; i < tbPisos.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@cambio", tbPisos.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbPisos.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idreporte", idReporte);
                comm3.Parameters.AddWithValue("@piso", tbPisos.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@arreglados", tbPisos.Rows[i]["Arreglados"].ToString());
                comm3.Parameters.AddWithValue("@idcriterio", tbPisos.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }

            for (int i = 0; i < tbOtros.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@cambio", tbOtros.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbOtros.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idreporte", idReporte);
                comm3.Parameters.AddWithValue("@piso", tbOtros.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@arreglados", tbOtros.Rows[i]["Arreglados"].ToString());
                comm3.Parameters.AddWithValue("@idcriterio", tbOtros.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }
        }

        private void ActualizarOrdenCompra(string id)
        {
            string updateOrden = "update OrdenesCompraServicios set DetallesdeCompra=@Detalles, ImageInstalacion=@Image, TicketdeCompra =@Ticket where IdOrdenCompraServicio=@Id";
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