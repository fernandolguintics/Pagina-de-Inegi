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
    public partial class VerMantenimientoAlmacen : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            if (tipoUsuario.Equals("Tecnico"))
            {
                btnGuardar.Visible = false;
                GVAcervo.Columns[6].Visible = false;
                GVArea.Columns[6].Visible = false;
                GVConsumo.Columns[6].Visible = false;
                GVInventario.Columns[6].Visible = false;
                GVProcesos.Columns[6].Visible = false;
                GVAcervo.Columns[7].Visible = true;
                GVArea.Columns[7].Visible = true;
                GVConsumo.Columns[7].Visible = true;
                GVInventario.Columns[7].Visible = true;
                GVProcesos.Columns[7].Visible = true;
            }

            if (tipoUsuario.Equals("Administrador"))
            {
                btnGuardar.Visible = true;
                GVAcervo.Columns[6].Visible = true;
                GVArea.Columns[6].Visible = true;
                GVConsumo.Columns[6].Visible = true;
                GVInventario.Columns[6].Visible = true;
                GVProcesos.Columns[6].Visible = true;
                GVAcervo.Columns[7].Visible = false;
                GVArea.Columns[7].Visible = false;
                GVConsumo.Columns[7].Visible = false;
                GVInventario.Columns[7].Visible = false;
                GVProcesos.Columns[7].Visible = false;
            }
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
                 new DataColumn("IdCriterio", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Estado", typeof(string)),
                  new DataColumn("Estado2", typeof(string))
            });

            string getMantenimiento = "select Criterio,Cumple,NoCumple,Observacion,Estado, DetallesConcentracion.IdConcentracion from DetallesConcentracion inner join CriteriosConcentracion on DetallesConcentracion.IdConcentracion =CriteriosConcentracion.IdConcentracion where IdMantenimiento=@id and Area ='Area de Acervo'";
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
                    dr["Estado"].ToString(),
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
                new DataColumn("IdCriterio", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Estado", typeof(string)),
                new DataColumn("Estado2", typeof(string))
            });

            string getMantenimiento = "select Criterio,Cumple,NoCumple,Observacion,Estado, DetallesConcentracion.IdConcentracion from DetallesConcentracion inner join CriteriosConcentracion on DetallesConcentracion.IdConcentracion =CriteriosConcentracion.IdConcentracion where IdMantenimiento=@id and Area ='Procesos Tecnicos'";
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
                     dr["Estado"].ToString(),
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
                new DataColumn("IdCriterio", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                 new DataColumn("Estado", typeof(string)),
                 new DataColumn("Estado2", typeof(string))
            });

            string getMantenimiento = "select Criterio,Cumple,NoCumple,Observacion,Estado, DetallesConcentracion.IdConcentracion from DetallesConcentracion inner join CriteriosConcentracion on DetallesConcentracion.IdConcentracion =CriteriosConcentracion.IdConcentracion where IdMantenimiento=@id and Area ='Procesos Tecnicos,Consulta y Acervo'";
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
                    dr["Estado"].ToString(),
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
                new DataColumn("IdCriterio", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Estado", typeof(string)),
                new DataColumn("Estado2", typeof(string))
            });

            string getMantenimiento = "select Criterio,Cumple,NoCumple,Observacion,Estado, DetallesInventario.IdInventario from DetallesInventario inner join CriteriosInventario on DetallesInventario.IdInventario =CriteriosInventario.IdInventario where IdMantenimiento=@id";
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
                    dr["Estado"].ToString(),
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
            new DataColumn("IdCriterio", typeof(string)),
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                 new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Estado", typeof(string)),
                new DataColumn("Estado2", typeof(string))
            });

            string getMantenimiento = "select Criterio,Cumple,NoCumple,Observacion, DetallesConsumo.IdConsumo,Estado from DetallesConsumo inner join CriteriosConsumo on DetallesConsumo.IdConsumo =CriteriosConsumo.IdConsumo where IdMantenimiento=@id";
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
                    dr["Estado"].ToString(),
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
            NombreEdificio, Direccion,Posesion,Ocupacion,Superficie, Uso, Niveles, Cajones, Antiguedad, Terreno from MantenimientosAlmacen inner join Edificios on MantenimientosAlmacen.IdEdificio=Edificios.IdEdificio where IdMantenimientoAlmacen = @id";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtFecha1.Text = "Fecha de Realización:" + reader["Fecha"].ToString();
                txtFecha2.Text = "Ultima Fecha de Actualización:" + reader["FechaActualizacion"].ToString();
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
           
            foreach(GridViewRow gvr in GVConsumo.Rows)
            {
                string actualizar = "update DetallesConsumo set Estado = @estado where IdConsumo = @idConsumo and IdMantenimiento = @id";
                int estado;
                if (((CheckBox)gvr.FindControl("estado")).Checked)
                {
                    estado = 1;
                }
                else
                {
                    estado = 0;
                }
                SqlCommand comm = new SqlCommand(actualizar, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@estado", estado);
                comm.Parameters.AddWithValue("@idConsumo", gvr.Cells[0].Text);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }

            foreach (GridViewRow gvr in GVAcervo.Rows)
            {
                string actualizar = "update DetallesConcentracion set Estado = @estado where IdConcentracion = @idConcentracion and IdMantenimiento = @id";
                int estado;
                if (((CheckBox)gvr.FindControl("estado")).Checked)
                {
                    estado = 1;
                }
                else
                {
                    estado = 0;
                }
                SqlCommand comm = new SqlCommand(actualizar, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@estado", estado);
                comm.Parameters.AddWithValue("@idConcentracion", gvr.Cells[0].Text);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            foreach (GridViewRow gvr in GVArea.Rows)
            {
                string actualizar = "update DetallesConcentracion set Estado = @estado where IdConcentracion = @idConcentracion and IdMantenimiento = @id";
                int estado;
                if (((CheckBox)gvr.FindControl("estado")).Checked)
                {
                    estado = 1;
                }
                else
                {
                    estado = 0;
                }
                SqlCommand comm = new SqlCommand(actualizar, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@estado", estado);
                comm.Parameters.AddWithValue("@idConcentracion", gvr.Cells[0].Text);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            foreach (GridViewRow gvr in GVInventario.Rows)
            {
                string actualizar = "update DetallesInventario set Estado = @estado where IdInventario = @idInventario and IdMantenimiento = @id";
                int estado;
                if (((CheckBox)gvr.FindControl("estado")).Checked)
                {
                    estado = 1;
                }
                else
                {
                    estado = 0;
                }
                SqlCommand comm = new SqlCommand(actualizar, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@estado", estado);
                comm.Parameters.AddWithValue("@idInventario", gvr.Cells[0].Text);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            foreach (GridViewRow gvr in GVProcesos.Rows)
            {
                string actualizar = "update DetallesConcentracion set Estado = @estado where IdConcentracion = @idConcentracion and IdMantenimiento = @id";
                int estado;
                if (((CheckBox)gvr.FindControl("estado")).Checked)
                {
                    estado = 1;
                }
                else
                {
                    estado = 0;
                }
                SqlCommand comm = new SqlCommand(actualizar, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@estado", estado);
                comm.Parameters.AddWithValue("@idConcentracion", gvr.Cells[0].Text);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            Response.Redirect("SeguimientoAlmacenes.aspx", false);
        }

        protected void GVConsumo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVConsumo.Rows[index];
                mpeMostrar.Show();
                string id = row.Cells[0].Text;
                string selectImage = "select Evidencia from DetallesConsumo where IdConsumo =@idConsumo and IdMantenimiento=@id";
                SqlCommand comm = new SqlCommand(selectImage, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@idConsumo", id);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                SqlDataReader dataReader = comm.ExecuteReader();
                if (dataReader.Read())
                {
                    byte[] imagen = (byte[])dataReader["Evidencia"];
                    if (imagen != null && imagen.Length > 0)
                    {
                        mostrarEvidencia.ImageUrl = "data:image;base64," + Convert.ToBase64String(imagen);
                    }
                }
            }
        }

        protected void GVInventario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVConsumo.Rows[index];
                mpeMostrar.Show();
                string id = row.Cells[0].Text;
                string selectImage = "select Evidencia from DetallesInventario where IdInventario =@idInventario and IdMantenimiento=@id";
                SqlCommand comm = new SqlCommand(selectImage, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@idInventario", id);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                SqlDataReader dataReader = comm.ExecuteReader();
                if (dataReader.Read())
                {
                    byte[] imagen = (byte[])dataReader["Evidencia"];
                    if (imagen != null && imagen.Length > 0)
                    {
                        mostrarEvidencia.ImageUrl = "data:image;base64," + Convert.ToBase64String(imagen);
                    }
                }
            }
        }

        protected void GVArea_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVConsumo.Rows[index];
                mpeMostrar.Show();
                string id = row.Cells[0].Text;
                string selectImage = "select Evidencia from DetallesConcentracion where IdConcentracion =@idConcentracion and IdMantenimiento=@id";
                SqlCommand comm = new SqlCommand(selectImage, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@idConcentracion", id);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                SqlDataReader dataReader = comm.ExecuteReader();
                if (dataReader.Read())
                {
                    byte[] imagen = (byte[])dataReader["Evidencia"];
                    if (imagen != null && imagen.Length > 0)
                    {
                        mostrarEvidencia.ImageUrl = "data:image;base64," + Convert.ToBase64String(imagen);
                    }
                }
            }
        }

        protected void GVProcesos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVConsumo.Rows[index];
                mpeMostrar.Show();
                string id = row.Cells[0].Text;
                string selectImage = "select Evidencia from DetallesConcentracion where IdConcentracion =@idConcentracion and IdMantenimiento=@id";
                SqlCommand comm = new SqlCommand(selectImage, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@idConcentracion", id);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                SqlDataReader dataReader = comm.ExecuteReader();
                if (dataReader.Read())
                {
                    byte[] imagen = (byte[])dataReader["Evidencia"];
                    if (imagen != null && imagen.Length > 0)
                    {
                        mostrarEvidencia.ImageUrl = "data:image;base64," + Convert.ToBase64String(imagen);
                    }
                }
            }
        }

        protected void GVAcervo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVConsumo.Rows[index];
                mpeMostrar.Show();
                string id = row.Cells[0].Text;
                string selectImage = "select Evidencia from DetallesConcentracion where IdConcentracion =@idConcentracion and IdMantenimiento=@id";
                SqlCommand comm = new SqlCommand(selectImage, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@idConcentracion", id);
                comm.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                SqlDataReader dataReader = comm.ExecuteReader();
                if (dataReader.Read())
                {
                    byte[] imagen = (byte[])dataReader["Evidencia"];
                    if (imagen != null && imagen.Length > 0)
                    {
                        mostrarEvidencia.ImageUrl = "data:image;base64," + Convert.ToBase64String(imagen);
                    }
                }
            }
        }
    }
}