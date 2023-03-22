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
    public partial class MantenimientoAlmacenConcentracion : System.Web.UI.Page
    {
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        string idEdificio = Menu.idInmueble;
        DataTable tbConcentracion = new DataTable();
        DataTable tbConcentracion2 = new DataTable();
        DataTable tbConcentracion3 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaConcentracion();
            }
        }
        private void LlenarTablaConcentracion()
        {
            DataTable tableAcervo = new DataTable();
            tableAcervo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
            });

            string querySelect = "Select * from CriteriosConcentracion where Area = 'Procesos Tecnicos,Consulta y Acervo'";

            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableAcervo.Rows.Add(
                        dr["IdConcentracion"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVAcervo.DataSource = tableAcervo;
            GVAcervo.DataBind();

            DataTable tableProcesos = new DataTable();
            tableProcesos.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
            });

            string querySelect2 = "Select * from CriteriosConcentracion where Area = 'Procesos Tecnicos'";

            SqlCommand commandSelect2 = new SqlCommand(querySelect2, sqlconn);
            commandSelect2.Connection.Open();
            SqlDataReader dr2 = commandSelect2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    tableProcesos.Rows.Add(
                        dr2["IdConcentracion"].ToString(),
                        dr2["Criterio"].ToString()
                    );
                }
            }
            commandSelect2.Connection.Close();
            GVProcesos.DataSource = tableProcesos;
            GVProcesos.DataBind();

            DataTable tableArea = new DataTable();
            tableArea.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
            });
            string querySelect3 = "Select * from CriteriosConcentracion where Area = 'Area de acervo'";

            SqlCommand commandSelect3 = new SqlCommand(querySelect3, sqlconn);
            commandSelect3.Connection.Open();
            SqlDataReader dr3 = commandSelect3.ExecuteReader();

            if (dr3.HasRows)
            {
                while (dr3.Read())
                {
                    tableArea.Rows.Add(
                        dr3["IdConcentracion"].ToString(),
                        dr3["Criterio"].ToString()
                    );
                }
            }
            commandSelect3.Connection.Close();
            GVArea.DataSource = tableArea;
            GVArea.DataBind();
        }

        public void ObtenerDatos()
        {

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
            command.Connection.Close();

        }

        protected void btnGuardarDatos_Click(object sender, EventArgs e)
        {

            double promedioConcentracion;
            int criteriosCumplen = 0;
            int i = 0, estado=0;

            DataRow rowTable;
            tbConcentracion.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Evidencia", typeof(byte[])),
                new DataColumn("Estado", typeof(string))
            });

            foreach (GridViewRow gvr in GVArea.Rows)
            {
                rowTable = tbConcentracion.NewRow();
                rowTable[0] = gvr.Cells[0].Text;
                if (((RadioButton)gvr.FindControl("rbCumple")).Checked)
                {
                    rowTable[1] = 1;
                    rowTable[2] = 0;
                    criteriosCumplen++;
                }
                else
                {
                    rowTable[1] = 0;
                    rowTable[2] = 1;
                }
                rowTable[3] = ((TextBox)gvr.FindControl("observacion")).Text;
                rowTable[4] = ((FileUpload)gvr.FindControl("evidencia")).FileBytes;
                rowTable[5] = estado;
                tbConcentracion.Rows.Add(rowTable);
                i++;
            }
            /////////
            tbConcentracion2.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Evidencia", typeof(byte[])),
                new DataColumn("Estado", typeof(string))
            });

            foreach (GridViewRow gvr in GVProcesos.Rows)
            {
                rowTable = tbConcentracion2.NewRow();
                rowTable[0] = gvr.Cells[0].Text;
                if (((RadioButton)gvr.FindControl("rbCumple")).Checked)
                {
                    rowTable[1] = 1;
                    rowTable[2] = 0;
                    criteriosCumplen++;
                }
                else
                {
                    rowTable[1] = 0;
                    rowTable[2] = 1;
                }
                rowTable[3] = ((TextBox)gvr.FindControl("observacion")).Text;
                rowTable[4] = ((FileUpload)gvr.FindControl("evidencia")).FileBytes;
                rowTable[5] = estado;
                tbConcentracion2.Rows.Add(rowTable);
                i++;
            }
            ///////////
            tbConcentracion3.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Evidencia", typeof(byte[])),
                new DataColumn("Estado", typeof(string))
            });

            foreach (GridViewRow gvr in GVAcervo.Rows)
            {
                rowTable = tbConcentracion3.NewRow();
                rowTable[0] = gvr.Cells[0].Text;
                if (((RadioButton)gvr.FindControl("rbCumple")).Checked)
                {
                    rowTable[1] = 1;
                    rowTable[2] = 0;
                    criteriosCumplen++;
                }
                else
                {
                    rowTable[1] = 0;
                    rowTable[2] = 1;
                }
                rowTable[3] = ((TextBox)gvr.FindControl("observacion")).Text;
                rowTable[4] = ((FileUpload)gvr.FindControl("evidencia")).FileBytes;
                rowTable[5] = estado;
                tbConcentracion3.Rows.Add(rowTable);
                i++;
            }

            promedioConcentracion = (Convert.ToDouble(criteriosCumplen) * 10.0) / Convert.ToDouble(i);

            GuardarMantenimiento(promedioConcentracion);
            mpeGuardarMantenimiento.Show();
            Response.Redirect("SeguimientoAlmacenes.aspx", false);
        }

        private void GuardarMantenimiento(double promedio)
        {
            string IdUsuario = Convert.ToString(Session["IdUsuario"]);
            double promedioTotal = (promedio + MantenimientoAlmacenConsumo.promedioConsumo + MantenimientoAlmacenInventario.promedioInventario) / 3.0;
            string insertMant = @"insert MantenimientosAlmacen(Fecha,Consumo,Inventario,Concentracion,Promedio,IdUsuario,IdEdificio) values(@fecha,@consumo,@inventario,@concentracion,@promedio,@idusuario,@edificio)";

            SqlCommand comm = new SqlCommand(insertMant, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@fecha", MantenimientoAlmacenConsumo.fecha);
            comm.Parameters.AddWithValue("@consumo", MantenimientoAlmacenConsumo.promedioConsumo);
            comm.Parameters.AddWithValue("@inventario", MantenimientoAlmacenInventario.promedioInventario);
            comm.Parameters.AddWithValue("@concentracion", promedio);
            comm.Parameters.AddWithValue("@promedio", promedioTotal);
            comm.Parameters.AddWithValue("@idusuario", IdUsuario);
            comm.Parameters.AddWithValue("@edificio", Menu.idInmueble);
            comm.ExecuteNonQuery();
            comm.Connection.Close();

            string selectId = "select MAX(IdMantenimientoAlmacen) as 'Id' from MantenimientosAlmacen";
            SqlCommand comm2 = new SqlCommand(selectId, sqlconn);
            comm2.Connection.Open();
            SqlDataReader dr = comm2.ExecuteReader();
            if (dr.Read())
            {
                string idMantenimiento = dr["Id"].ToString();
                comm2.Connection.Close();
                GuardarDetalles(idMantenimiento);
            }


        }

        private void GuardarDetalles(string id)
        {
            for (int i = 0; i < MantenimientoAlmacenConsumo.tbConsumo.Rows.Count; i++)
            {

                string insertDetalles = "insert DetallesConsumo(Cumple,NoCumple,Observacion,IdConsumo,IdMantenimiento,Evidencia,Estado) values(@cumple,@nocumple,@observacion,@idconsumo,@mantenimiento,@evidencia,@estado)";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", MantenimientoAlmacenConsumo.tbConsumo.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", MantenimientoAlmacenConsumo.tbConsumo.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@idconsumo", MantenimientoAlmacenConsumo.tbConsumo.Rows[i]["IdConsumo"].ToString());
                comm.Parameters.AddWithValue("@observacion", MantenimientoAlmacenConsumo.tbConsumo.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = MantenimientoAlmacenConsumo.tbConsumo.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@estado", MantenimientoAlmacenConsumo.tbConsumo.Rows[i]["Estado"].ToString());
                comm.Parameters.AddWithValue("@mantenimiento", id);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            for (int i = 0; i < MantenimientoAlmacenInventario.tbInventarios.Rows.Count; i++)
            {
                string insertDetalles = "insert DetallesInventario(Cumple,NoCumple,Observacion,IdInventario,IdMantenimiento,Evidencia,Estado) values(@cumple,@nocumple,@observacion,@idinventario,@mantenimiento,@evidencia,@estado)";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", MantenimientoAlmacenInventario.tbInventarios.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", MantenimientoAlmacenInventario.tbInventarios.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@idinventario", MantenimientoAlmacenInventario.tbInventarios.Rows[i]["IdInventario"].ToString());
                comm.Parameters.AddWithValue("@observacion", MantenimientoAlmacenInventario.tbInventarios.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = MantenimientoAlmacenInventario.tbInventarios.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@estado", MantenimientoAlmacenInventario.tbInventarios.Rows[i]["Estado"].ToString());
                comm.Parameters.AddWithValue("@mantenimiento", id);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }

            for (int i = 0; i < tbConcentracion.Rows.Count; i++)
            {
                string insertDetalles = "insert DetallesConcentracion(Cumple,NoCumple,Observacion,IdConcentracion,IdMantenimiento,Evidencia,Estado) values(@cumple,@nocumple,@observacion,@idconcentracion,@mantenimiento,@evidencia,@estado)";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", tbConcentracion.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", tbConcentracion.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@idconcentracion", tbConcentracion.Rows[i]["IdConcentracion"].ToString());
                comm.Parameters.AddWithValue("@observacion", tbConcentracion.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = tbConcentracion.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@estado", tbConcentracion.Rows[i]["Estado"].ToString());
                comm.Parameters.AddWithValue("@mantenimiento", id);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }

            for (int i = 0; i < tbConcentracion2.Rows.Count; i++)
            {
                string insertDetalles = "insert DetallesConcentracion(Cumple,NoCumple,Observacion,IdConcentracion,IdMantenimiento,Evidencia,Estado) values(@cumple,@nocumple,@observacion,@idconcentracion,@mantenimiento,@evidencia,@estado)";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", tbConcentracion2.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", tbConcentracion2.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@idconcentracion", tbConcentracion2.Rows[i]["IdConcentracion"].ToString());
                comm.Parameters.AddWithValue("@observacion", tbConcentracion2.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = tbConcentracion2.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@estado", tbConcentracion2.Rows[i]["Estado"].ToString());
                comm.Parameters.AddWithValue("@mantenimiento", id);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }

            for (int i = 0; i < tbConcentracion3.Rows.Count; i++)
            {
                string insertDetalles = "insert DetallesConcentracion(Cumple,NoCumple,Observacion,IdConcentracion,IdMantenimiento,Evidencia,Estado) values(@cumple,@nocumple,@observacion,@idconcentracion,@mantenimiento,@evidencia,@estado)";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", tbConcentracion3.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", tbConcentracion3.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@idconcentracion", tbConcentracion3.Rows[i]["IdConcentracion"].ToString());
                comm.Parameters.AddWithValue("@observacion", tbConcentracion3.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = tbConcentracion3.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@estado", tbConcentracion3.Rows[i]["Estado"].ToString());
                comm.Parameters.AddWithValue("@mantenimiento", id);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }

        }

    }
}