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
    public partial class ReporteServicios : System.Web.UI.Page
    {
        static string conn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conn);
        string idMantenimiento = SeguimientoServicios.idMantenimiento;
        public static string fechaEvaluacion;
        static string idReporte;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaCarpinteria();
                LlenarTablaHerreria();
                LlenarTablaPintura();
                LlenarTablaHidrosanitarias();
                LlenarTablaPisos();
                LlenarTablaOtros();
            }
        }
        private void LlenarTablaCarpinteria()
        {
            DataTable tableCarp = new DataTable();
            tableCarp.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select IdCriterio,Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Carpintería y Cristales'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableCarp.Rows.Add(
                         dr2["IdCriterio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVCarpinteria.DataSource = tableCarp;
            GVCarpinteria.DataBind();
        }

        private void LlenarTablaHerreria()
        {
            DataTable tableHerr = new DataTable();
            tableHerr.Columns.AddRange(new DataColumn[]{
                 new DataColumn("IdCriterio", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select IdCriterio,Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Herrería'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableHerr.Rows.Add(
                         dr2["IdCriterio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVHerreria.DataSource = tableHerr;
            GVHerreria.DataBind();
        }
        private void LlenarTablaPintura()
        {
            DataTable tablePintura = new DataTable();
            tablePintura.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select IdCriterio,Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Pintura'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tablePintura.Rows.Add(
                         dr2["IdCriterio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVPintura.DataSource = tablePintura;
            GVPintura.DataBind();
        }

        private void LlenarTablaHidrosanitarias()
        {
            DataTable tableHidro = new DataTable();
            tableHidro.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select IdCriterio,Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Instalaciones Hidrosanitarias'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableHidro.Rows.Add(
                         dr2["IdCriterio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVHidrosanitarias.DataSource = tableHidro;
            GVHidrosanitarias.DataBind();
        }
        private void LlenarTablaPisos()
        {
            DataTable tablePisos = new DataTable();
            tablePisos.Columns.AddRange(new DataColumn[]{
                 new DataColumn("IdCriterio", typeof(string)),
                 new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select IdCriterio,Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Piso,Plafones y Techo'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tablePisos.Rows.Add(
                        dr2["IdCriterio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVPisos.DataSource = tablePisos;
            GVPisos.DataBind();
        }
        private void LlenarTablaOtros()
        {
            DataTable tableOtros = new DataTable();
            tableOtros.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select IdCriterio,Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Otros'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableOtros.Rows.Add(
                        dr2["IdCriterio"].ToString(),
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVOtros.DataSource = tableOtros;
            GVOtros.DataBind();
        }

        public void ObtenerDatos()
        {

            String query = @"select Format(Fecha,'dd/MM/yyyy') as 'FechaMantenimiento',NombreEdificio,Direccion,Posesion,Ocupacion,Superficie,Uso,Niveles,Cajones,Antiguedad,Terreno from MantenimientoServicios inner join 
                                        Edificios on MantenimientoServicios.IdEdificio = Edificios.IdEdificio where IdMantenimientoServicio = @IdMantenimiento";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                txtFecha.Text = "Fecha de Evaluación:" + reader["FechaMantenimiento"].ToString();
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if (pdfEvidencias.HasFile && pdfOrdenCompra.HasFiles)
            {
                string insertReport = "insert into OrdenesCompraServicios(DetallesdeCompra,ImageInstalacion,TicketdeCompra) values(@detalles,@instalacion,@ticket)";
                SqlCommand command = new SqlCommand(insertReport, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@detalles", detallesDeCompra.Text);
                command.Parameters.AddWithValue("@instalacion", SqlDbType.VarBinary).Value = pdfEvidencias.FileBytes;
                command.Parameters.AddWithValue("@ticket", SqlDbType.VarBinary).Value = pdfOrdenCompra.FileBytes;
                command.ExecuteNonQuery();
                command.Connection.Close();

                string selectId = "select MAX(IdOrdenCompraServicio) as 'Id' from OrdenesCompraServicios";
                SqlCommand comm2 = new SqlCommand(selectId, sqlconn);
                comm2.Connection.Open();
                SqlDataReader dr = comm2.ExecuteReader();
                if (dr.Read())
                {
                    string id = dr["Id"].ToString();
                    comm2.Connection.Close();
                    GuardarReporte(id);

                }
            }
            else
            {
                mpeErrorCarga.Show();
            }
        }

        private void GuardarReporte(string id)
        {
            int arregladosTotal = 0;
            string IdUsuario = Convert.ToString(Session["IdUsuario"]);
            string validacion = "Pendiente";

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

            foreach (GridViewRow gvr in GVCarpinteria.Rows)
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

            foreach (GridViewRow gvr in GVHerreria.Rows)
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

            foreach (GridViewRow gvr in GVPintura.Rows)
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

            foreach (GridViewRow gvr in GVHidrosanitarias.Rows)
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

            foreach (GridViewRow gvr in GVPisos.Rows)
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

            foreach (GridViewRow gvr in GVOtros.Rows)
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

            string insertReporte = @"insert ReportesServicios(FechaEvaluacion,IdUsuario,IdMantenimiento,IdOrdenCompra,Validacion,Arreglados)
                                    values(@fecha,@idusuario,@idmant,@idorden,@validacion,@arreglados)";
            SqlCommand comm = new SqlCommand(insertReporte, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@fecha", txtFecha.Text);
            comm.Parameters.AddWithValue("@idusuario", IdUsuario);
            comm.Parameters.AddWithValue("@idmant", idMantenimiento);
            comm.Parameters.AddWithValue("@idorden", id);
            comm.Parameters.AddWithValue("@validacion", validacion);
            comm.Parameters.AddWithValue("@arreglados", arregladosTotal);

            comm.ExecuteNonQuery();
            comm.Connection.Close();


            string selectMant = "select Max(IdReporteServicios) as 'Id' from ReportesServicios";
            SqlCommand comm2 = new SqlCommand(selectMant, sqlconn);
            comm2.Connection.Open();
            SqlDataReader dr = comm2.ExecuteReader();
            if (dr.Read())
            {
                idReporte = dr["Id"].ToString();
                comm2.Connection.Close();

            }
            string insert = @"insert DetallesReporteServicio(CambioNormativa,Observacion,IdReporte,Piso,Arreglados,IdCriterio)
                            values (@cambio,@observacion,@idreporte,@piso,@arreglados,@idcriterio)";

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

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se ha guardado el reporte con exito');", true);
            Response.Redirect("MenuReportesServicio.aspx", false);
        }
    }
}