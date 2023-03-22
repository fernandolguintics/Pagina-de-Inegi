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
    public partial class ConfAlmacen : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static string idConsumo;
        public static string idConcentracion;
        public static string idInventario;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GVConsumo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVConsumo.PageIndex = e.NewPageIndex;
            LLenarTablaConsumo();
        }

        protected void GVConsumo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                mpeConsumo.Show();
                btnActualizarConsu.Visible = true;
                btnConsumo.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVConsumo.Rows[index];
                idConsumo = row.Cells[0].Text;
                string select = "select Criterio from CriteriosConsumo where IdConsumo =@id";
                SqlCommand cmd = new SqlCommand(select, sqlconn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id", idConsumo);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txbCriterio.Text = dr["Criterio"].ToString();
                    cmd.Connection.Close();
                }
            }
            else
            {
                if (e.CommandName == "Borrar")
                {
                    mpeBorrar.Show();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GVConsumo.Rows[index];
                    idConsumo = row.Cells[0].Text;
                    string select = "select Criterio from CriteriosConsumo where IdConsumo =@id";
                    SqlCommand cmd = new SqlCommand(select, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", idConsumo);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        tipoCriteriotxt.Text = dr["Criterio"].ToString();
                        cmd.Connection.Close();
                    }
                }
            }
        }
        protected void mostrar_Click(object sender, EventArgs e)
        {
            switch (seleccionarTabla.SelectedValue)
            {
                case "Consumo":
                    GVConsumo.Visible = true;
                    btnAgregarConsumo.Visible = true;
                    GVInventario.Visible = false;
                    btnAgregarInventario.Visible = false;
                    GVConcentracion.Visible = false;
                    btnAgregarConcentracion.Visible = false;
                    areatxt.Visible = false;
                    seleccionarArea.Visible = false;
                    LLenarTablaConsumo();
                    break;
                case "Inventarios":
                    GVInventario.Visible = true;
                    btnAgregarInventario.Visible = true;
                    GVConsumo.Visible = false;
                    btnAgregarConsumo.Visible = false;
                    GVConcentracion.Visible = false;
                    btnAgregarConcentracion.Visible = false;
                    areatxt.Visible = false;
                    seleccionarArea.Visible = false;
                    LLenarTablaInventario();
                    break;
                case "Concentracion":
                    GVConcentracion.Visible = true;
                    btnAgregarConcentracion.Visible = true;
                    GVInventario.Visible = false;
                    btnAgregarInventario.Visible = false;
                    GVConsumo.Visible = false;
                    btnAgregarConsumo.Visible = false;
                    areatxt.Visible = true;
                    seleccionarArea.Visible = true;
                    LlenarTablaConcentracion();
                    break;
            }
        }

        private void LlenarTablaConcentracion()
        {
            DataTable tableConcentracion = new DataTable();
            tableConcentracion.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Area", typeof(string)),
            });
            SqlCommand command = new SqlCommand("select * from CriteriosConcentracion", sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableConcentracion.Rows.Add(
                        dr["IdConcentracion"].ToString(),
                        dr["Criterio"].ToString(),
                        dr["Area"]

                    );
                }
            }
            command.Connection.Close();
            GVConcentracion.Columns[3].Visible = false;
            GVConcentracion.DataSource = tableConcentracion;
            GVConcentracion.DataBind();
        }

        private void LLenarTablaInventario()
        {
            DataTable tableInventario = new DataTable();
            tableInventario.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
            });
            SqlCommand command = new SqlCommand("select IdInventario, Criterio from CriteriosInventario", sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableInventario.Rows.Add(
                        dr["IdInventario"].ToString(),
                        dr["Criterio"].ToString()

                    );
                }
            }
            command.Connection.Close();
            GVInventario.DataSource = tableInventario;
            GVInventario.DataBind();
        }

        private void LLenarTablaConsumo()
        {
            DataTable tableConsumo = new DataTable();
            tableConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
            });
            SqlCommand command = new SqlCommand("select IdConsumo, Criterio from CriteriosConsumo", sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableConsumo.Rows.Add(
                        dr["IdConsumo"].ToString(),
                        dr["Criterio"].ToString()

                    );
                }
            }
            command.Connection.Close();
            GVConsumo.DataSource = tableConsumo;
            GVConsumo.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            switch (seleccionarTabla.SelectedValue)
            {
                case "Consumo":
                    string insertCriterio = "insert into CriteriosConsumo(Criterio) values (@criterio)";
                    SqlCommand comm1 = new SqlCommand(insertCriterio, sqlconn);
                    comm1.Connection.Open();
                    comm1.Parameters.AddWithValue("@criterio", txbCriterio.Text);
                    comm1.ExecuteNonQuery();
                    comm1.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
                case "Inventarios":
                    string insertInventario = "insert into CriteriosInventario(Criterio) values (@criterio)";
                    SqlCommand comm = new SqlCommand(insertInventario, sqlconn);
                    comm.Connection.Open();
                    comm.Parameters.AddWithValue("@criterio", criterioInventario.Text);
                    comm.ExecuteNonQuery();
                    comm.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
                case "Concentracion":
                    string insertCon = "insert into CriteriosConcentracion(Criterio,Area) values (@criterio,@area)";
                    SqlCommand comm2 = new SqlCommand(insertCon, sqlconn);
                    comm2.Connection.Open();
                    comm2.Parameters.AddWithValue("@criterio", criterioConcentracion.Text);
                    comm2.Parameters.AddWithValue("@area", seleccionarArea.SelectedValue);
                    comm2.ExecuteNonQuery();
                    comm2.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
            }
        }


        protected void GVInventario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVInventario.PageIndex = e.NewPageIndex;
            LLenarTablaInventario();
        }

        protected void GVInventario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                mpeInventario.Show();
                btnActualizarInv.Visible = true;
                btnInventario.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVInventario.Rows[index];
                idInventario = row.Cells[0].Text;
                string select = "select Criterio from CriteriosInventario where IdInventario =@id";
                SqlCommand cmd = new SqlCommand(select, sqlconn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id", idInventario);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    criterioInventario.Text = dr["Criterio"].ToString();
                    cmd.Connection.Close();
                }

            }
            else
            {
                if (e.CommandName == "Borrar")
                {
                    mpeBorrar.Show();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GVInventario.Rows[index];
                    idInventario = row.Cells[0].Text;
                    string select = "select Criterio from CriteriosInventario where IdInventario =@id";
                    SqlCommand cmd = new SqlCommand(select, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", idInventario);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        tipoCriteriotxt.Text = dr["Criterio"].ToString();
                        cmd.Connection.Close();
                    }
                }
            }
        }

        protected void GVConcentracion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                mpeConcentra.Show();
                btnActualizarCon.Visible = true;
                btnConcentracion.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVConcentracion.Rows[index];
                idConcentracion = row.Cells[0].Text;
                string select = "select Criterio from CriteriosConcentracion where IdConcentracion =@id";
                SqlCommand cmd = new SqlCommand(select, sqlconn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id", idConcentracion);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    criterioConcentracion.Text = dr["Criterio"].ToString();
                    cmd.Connection.Close();
                }
            }
            else
            {
                if (e.CommandName == "Borrar")
                {
                    mpeBorrar.Show();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GVConcentracion.Rows[index];
                    idConcentracion = row.Cells[0].Text;
                    string select = "select Criterio from CriteriosConcentracion where IdConcentracion =@id";
                    SqlCommand cmd = new SqlCommand(select, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", idConcentracion);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        tipoCriteriotxt.Text = dr["Criterio"].ToString();
                        cmd.Connection.Close();
                    }
                }
            }
        }

        protected void GVConcentracion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVConcentracion.PageIndex = e.NewPageIndex;
            LlenarTablaConcentracion();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            switch (seleccionarTabla.SelectedValue)
            {
                case "Consumo":
                    string insertCriterio = "update CriteriosConsumo set Criterio=@criterio where IdConsumo=@id";
                    SqlCommand comm1 = new SqlCommand(insertCriterio, sqlconn);
                    comm1.Connection.Open();
                    comm1.Parameters.AddWithValue("@criterio", txbCriterio.Text);
                    comm1.Parameters.AddWithValue("@id", idConsumo);
                    comm1.ExecuteNonQuery();
                    comm1.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
                case "Inventarios":
                    string insertInventario = "update CriteriosInventarios set Criterio=@criterio where IdInventario=@id";
                    SqlCommand comm = new SqlCommand(insertInventario, sqlconn);
                    comm.Connection.Open();
                    comm.Parameters.AddWithValue("@criterio", criterioInventario.Text);
                    comm.Parameters.AddWithValue("@id", idInventario);
                    comm.ExecuteNonQuery();
                    comm.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
                case "Concentracion":
                    string insertCon = "update CriteriosConcentracion set Criterio=@criterio, Area=@area where IdConcentracion=@id";
                    SqlCommand comm2 = new SqlCommand(insertCon, sqlconn);
                    comm2.Connection.Open();
                    comm2.Parameters.AddWithValue("@criterio", criterioConcentracion.Text);
                    comm2.Parameters.AddWithValue("@area", seleccionarArea.SelectedValue);
                    comm2.Parameters.AddWithValue("@id", idConcentracion);
                    comm2.ExecuteNonQuery();
                    comm2.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
            }
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            switch (seleccionarTabla.SelectedValue)
            {
                case "Consumo":
                    string insertCriterio = "delete from CriteriosConsumo where IdConsumo=@id";
                    SqlCommand comm1 = new SqlCommand(insertCriterio, sqlconn);
                    comm1.Connection.Open();
                    comm1.Parameters.AddWithValue("@id", idConsumo);
                    comm1.ExecuteNonQuery();
                    comm1.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
                case "Inventarios":
                    string insertInventario = "delete from CriteriosInventarios where IdInventario=@id";
                    SqlCommand comm = new SqlCommand(insertInventario, sqlconn);
                    comm.Connection.Open();
                    comm.Parameters.AddWithValue("@id", idInventario);
                    comm.ExecuteNonQuery();
                    comm.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
                case "Concentracion":
                    string insertCon = "delete from CriteriosConcentracion where IdConcentracion=@id";
                    SqlCommand comm2 = new SqlCommand(insertCon, sqlconn);
                    comm2.Connection.Open();
                    comm2.Parameters.AddWithValue("@id", idConcentracion);
                    comm2.ExecuteNonQuery();
                    comm2.Connection.Close();
                    Response.Redirect("ConfAlmacen.aspx", false);
                    break;
            }
        }

        protected void btnAgregarConcentracion_Click(object sender, EventArgs e)
        {
            btnActualizarCon.Visible = false;
            btnConcentracion.Visible = true;
            criterioConcentracion.Text = "";
        }

        protected void btnAgregarConsumo_Click(object sender, EventArgs e)
        {
            btnActualizarConsu.Visible = false;
            btnConsumo.Visible = true;
            txbCriterio.Text = "";
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnActualizarInv.Visible = false;
            btnActualizarCon.Visible = false;
            btnActualizarInv.Visible = false;
            btnInventario.Visible = true;
            btnConcentracion.Visible = true;
            btnConsumo.Visible = true;
            criterioInventario.Text = "";
            txbCriterio.Text = "";
            criterioConcentracion.Text = "";
        }
    }
}