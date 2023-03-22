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
    public partial class ActualizarAlmacen : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        DataTable tbConsumo = new DataTable();
        DataTable tbInventarios = new DataTable();
        DataTable tbConcentracion = new DataTable();
        DataTable tbConcentracion2 = new DataTable();
        DataTable tbConcentracion3 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaConsumo();
                LlenarTablaInventarios();
                LlenarTablaArea();
                LlenarTablaProcesos();
                LlenarTablaAcervo();
            }
        }
        private void LlenarTablaArea()
        {
            DataTable tableConsumo = new DataTable();
            tableConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Estado", typeof(string))
            });

            string getMantenimiento = "select CriteriosConcentracion.IdConcentracion, Criterio,Cumple,NoCumple,Observacion,Estado from DetallesConcentracion inner join CriteriosConcentracion on DetallesConcentracion.IdConcentracion =CriteriosConcentracion.IdConcentracion where IdMantenimiento=@id and Area ='Area de Acervo'";
            SqlCommand comm = new SqlCommand(getMantenimiento, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string estado;
                    if (dr["Estado"].Equals(true))
                    {
                        estado = "Aceptado";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                    tableConsumo.Rows.Add(
                    dr["IdConcentracion"].ToString(),
                    dr["Criterio"].ToString(),
                    dr["Cumple"].ToString(),
                    dr["NoCumple"].ToString(),
                    dr["Observacion"].ToString(),
                    estado
                     );
                }
            }
            comm.Connection.Close();
            GVArea.DataSource = tableConsumo;
            GVArea.DataBind();
        }

        private void LlenarTablaProcesos()
        {
            DataTable tableConsumo = new DataTable();
            tableConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Estado", typeof(string))
            });

            string getMantenimiento = "select CriteriosConcentracion.IdConcentracion, Criterio,Cumple,NoCumple,Observacion, Estado from DetallesConcentracion inner join CriteriosConcentracion on DetallesConcentracion.IdConcentracion =CriteriosConcentracion.IdConcentracion where IdMantenimiento=@id and Area ='Procesos Tecnicos'";
            SqlCommand comm = new SqlCommand(getMantenimiento, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string estado;
                    if (dr["Estado"].Equals(true))
                    {
                        estado = "Aceptado";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                    tableConsumo.Rows.Add(
                    dr["IdConcentracion"].ToString(),
                    dr["Criterio"].ToString(),
                    dr["Cumple"].ToString(),
                    dr["NoCumple"].ToString(),
                    dr["Observacion"].ToString(),
                    estado
                     );
                }
            }
            comm.Connection.Close();
            GVProcesos.DataSource = tableConsumo;
            GVProcesos.DataBind();
        }

        private void LlenarTablaAcervo()
        {
            DataTable tableConsumo = new DataTable();
            tableConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Estado", typeof(string))
            });

            string getMantenimiento = "select CriteriosConcentracion.IdConcentracion,Criterio,Cumple,NoCumple,Observacion,Estado from DetallesConcentracion inner join CriteriosConcentracion on DetallesConcentracion.IdConcentracion =CriteriosConcentracion.IdConcentracion where IdMantenimiento=@id and Area ='Procesos Tecnicos,Consulta y Acervo'";
            SqlCommand comm = new SqlCommand(getMantenimiento, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string estado;
                    if (dr["Estado"].Equals(true))
                    {
                        estado = "Aceptado";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                    tableConsumo.Rows.Add(
                    dr["IdConcentracion"].ToString(),
                    dr["Criterio"].ToString(),
                    dr["Cumple"].ToString(),
                    dr["NoCumple"].ToString(),
                    dr["Observacion"].ToString(),
                    estado
                     );
                }
            }
            comm.Connection.Close();
            GVAcervo.DataSource = tableConsumo;
            GVAcervo.DataBind();
        }

        private void LlenarTablaInventarios()
        {
            DataTable tableConsumo = new DataTable();
            tableConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdInventario", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Estado", typeof(string))
            });

            string getMantenimiento = "select CriteriosInventario.IdInventario,Criterio,Cumple,NoCumple,Observacion,Estado from DetallesInventario inner join CriteriosInventario on DetallesInventario.IdInventario =CriteriosInventario.IdInventario where IdMantenimiento=@id";
            SqlCommand comm = new SqlCommand(getMantenimiento, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string estado;
                    if (dr["Estado"].Equals(true))
                    {
                        estado = "Aceptado";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                    tableConsumo.Rows.Add(
                    dr["IdInventario"].ToString(),
                    dr["Criterio"].ToString(),
                    dr["Cumple"].ToString(),
                    dr["NoCumple"].ToString(),
                    dr["Observacion"].ToString(),
                    estado
                     );
                }
            }
            comm.Connection.Close();
            GVInventario.DataSource = tableConsumo;
            GVInventario.DataBind();
        }

        private void LlenarTablaConsumo()
        {
            DataTable tableConsumo = new DataTable();
            tableConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConsumo", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Estado", typeof(string))
            });

            string getMantenimiento = "select CriteriosConsumo.IdConsumo, Criterio,Cumple,NoCumple,Observacion,Estado from DetallesConsumo inner join CriteriosConsumo on DetallesConsumo.IdConsumo =CriteriosConsumo.IdConsumo where IdMantenimiento=@id";
            SqlCommand comm = new SqlCommand(getMantenimiento, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string estado;
                    if (dr["Estado"].Equals(true))
                    {
                        estado = "Aceptado";
                    }
                    else
                    {
                        estado = "Pendiente";
                    }
                    tableConsumo.Rows.Add(
                    dr["IdConsumo"].ToString(),
                    dr["Criterio"].ToString(),
                    dr["Cumple"].ToString(),
                    dr["NoCumple"].ToString(),
                    dr["Observacion"].ToString(),
                    estado
                     );
                }
            }
            comm.Connection.Close();
            GVConsumo.DataSource = tableConsumo;
            GVConsumo.DataBind();


        }

        public void ObtenerDatos()
        {
            String query = @"Select FORMAT(Fecha,'dd/MM/yyyy') as 'Fecha',FORMAT(FechaActualizacion,'dd/MM/yyyy') as 'FechaActualizacion',
                            NombreEdificio, Direccion,Posesion,Ocupacion,Superficie, Uso, Niveles, Cajones, Antiguedad, Terreno from MantenimientosAlmacen 
                            inner join Edificios on MantenimientosAlmacen.IdEdificio=Edificios.IdEdificio where IdMantenimientoAlmacen = @id";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtFecha1.Text = "Fecha de Realización:" + reader["Fecha"].ToString();
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            double consumo = ObtenerDatosConsumo();
            double inventario = ObtenerDatosInventario();
            double concentracion = ObtenerDatosConcentracion();

            ActualizarMantenimiento(consumo, inventario, concentracion);
            ActualizarDetalles();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se ha actualizado el mantenimiento');", true);
            Response.Redirect("SeguimientoAlmacenes.aspx", false);

        }
        private void ActualizarMantenimiento(double consumo, double inventario, double concentracion)
        {
            double promedioTotal = (concentracion + consumo + inventario) / 3.0;
            string insertMant = @"update MantenimientosAlmacen set FechaActualizacion=@fecha, Consumo=@consumo,Inventario=@inventario,Concentracion=@concentracion,Promedio=@promedio where IdMantenimientoAlmacen=@id";

            SqlCommand comm = new SqlCommand(insertMant, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@fecha", txbFecha2.Text);
            comm.Parameters.AddWithValue("@consumo", consumo);
            comm.Parameters.AddWithValue("@inventario", inventario);
            comm.Parameters.AddWithValue("@concentracion", concentracion);
            comm.Parameters.AddWithValue("@promedio", promedioTotal);
            comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
            comm.ExecuteNonQuery();
            comm.Connection.Close();
        }
        private double ObtenerDatosInventario()
        {
            int criteriosCumplen = 0;
            int i = 0;
            double promedioInventario;

            DataRow rowTable;
            tbInventarios.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdInventario", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Evidencia", typeof(byte[]))
            });
            
            foreach (GridViewRow gvr in GVInventario.Rows)
            {
                rowTable = tbInventarios.NewRow();
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
                Label label = (Label)gvr.FindControl("estado");
                string estado = label.Text;
                if (estado.Equals("Pendiente"))
                {

                    tbInventarios.Rows.Add(rowTable);
                }
                i++;
            }

            promedioInventario = (Convert.ToDouble(criteriosCumplen) * 10.0) / Convert.ToDouble(i);
            return promedioInventario;
        }

        private double ObtenerDatosConsumo()
        {
            int criteriosCumplen = 0;
            int i = 0;
            double promedioConsumo;

            DataRow rowTable;

            tbConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConsumo", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Evidencia", typeof(byte[]))
            });

            foreach (GridViewRow gvr in GVConsumo.Rows)
            {
                rowTable = tbConsumo.NewRow();
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
                Label label = (Label)gvr.FindControl("estado");
                string estado = label.Text;
               if (estado.Equals("Pendiente"))
                {
                    
                    tbConsumo.Rows.Add(rowTable);
                }
                i++;
            }

            promedioConsumo = (Convert.ToDouble(criteriosCumplen) * 10.0) / Convert.ToDouble(i);

            return promedioConsumo;
        }

        private double ObtenerDatosConcentracion()
        {
            double promedioConcentracion;
            int criteriosCumplen = 0;
            int i = 0;


            DataRow rowTable;
            tbConcentracion.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Evidencia", typeof(byte[]))
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
                Label label = (Label)gvr.FindControl("estado");
                string estado = label.Text;
                if (estado.Equals("Pendiente"))
                {

                    tbConcentracion.Rows.Add(rowTable);
                }
                i++;
            }
            /////////
            tbConcentracion2.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Evidencia", typeof(byte[]))
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
                Label label = (Label)gvr.FindControl("estado");
                string estado = label.Text;
                if (estado.Equals("Pendiente"))
                {

                    tbConcentracion2.Rows.Add(rowTable);
                }
                i++;
            }
            ///////////
            tbConcentracion3.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConcentracion", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Evidencia", typeof(byte[]))
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
                Label label = (Label)gvr.FindControl("estado");
                string estado = label.Text;
                if (estado.Equals("Pendiente"))
                {

                    tbConcentracion3.Rows.Add(rowTable);
                }
                i++;
            }

            promedioConcentracion = (Convert.ToDouble(criteriosCumplen) * 10.0) / Convert.ToDouble(i);
            return promedioConcentracion;
        }

        private void ActualizarDetalles()
        {
            for (int i = 0; i < tbConsumo.Rows.Count; i++)
            {
                string insertDetalles = "update DetallesConsumo set Cumple=@cumple,NoCumple=@nocumple,Observacion=@observacion, Evidencia=@evidencia where IdMantenimiento=@id and IdConsumo=@idconsumo";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", tbConsumo.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", tbConsumo.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@observacion", tbConsumo.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = tbConsumo.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.Parameters.AddWithValue("@idconsumo", tbConsumo.Rows[i]["IdConsumo"].ToString());
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            for (int i = 0; i < tbInventarios.Rows.Count; i++)
            {
                string insertDetalles = "update DetallesInventario set Cumple=@cumple,NoCumple=@nocumple,Observacion=@observacion, Evidencia=@evidencia where IdMantenimiento=@id and IdInventario=@idinventario";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", tbInventarios.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", tbInventarios.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@observacion", tbInventarios.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = tbInventarios.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@idinventario", tbInventarios.Rows[i]["IdInventario"].ToString());
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }

            for (int i = 0; i < tbConcentracion.Rows.Count; i++)
            {
                string insertDetalles = "update DetallesConcentracion set Cumple=@cumple,NoCumple=@nocumple,Observacion=@observacion, Evidencia=@evidencia where IdMantenimiento=@id and IdConcentracion=@idconcentracion";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", tbConcentracion.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", tbConcentracion.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@observacion", tbConcentracion.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = tbConcentracion.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@idconcentracion", tbConcentracion.Rows[i]["IdConcentracion"].ToString());
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }

            for (int i = 0; i < tbConcentracion2.Rows.Count; i++)
            {
                string insertDetalles = "update DetallesConcentracion set Cumple=@cumple,NoCumple=@nocumple,Observacion=@observacion, Evidencia=@evidencia where IdMantenimiento=@id and IdConcentracion=@idconcentracion";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", tbConcentracion2.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", tbConcentracion2.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@observacion", tbConcentracion2.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = tbConcentracion2.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@idconcentracion", tbConcentracion2.Rows[i]["IdConcentracion"].ToString());
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            for (int i = 0; i < tbConcentracion3.Rows.Count; i++)
            {
                string insertDetalles = "update DetallesConcentracion set Cumple=@cumple,NoCumple=@nocumple,Observacion=@observacion, Evidencia=@evidencia where IdMantenimiento=@id and IdConcentracion=@idconcentracion";
                SqlCommand comm = new SqlCommand(insertDetalles, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cumple", tbConcentracion3.Rows[i]["Cumple"].ToString());
                comm.Parameters.AddWithValue("@nocumple", tbConcentracion3.Rows[i]["NoCumple"].ToString());
                comm.Parameters.AddWithValue("@observacion", tbConcentracion3.Rows[i]["Observacion"].ToString());
                comm.Parameters.AddWithValue("@evidencia", SqlDbType.VarBinary).Value = tbConcentracion3.Rows[i]["Evidencia"];
                comm.Parameters.AddWithValue("@idconcentracion", tbConcentracion3.Rows[i]["IdConcentracion"].ToString());
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
        }
    }
}