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
    public partial class ConfAdmi : System.Web.UI.Page
    {
        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        public static string idEdificioActualizar;
        public static string idEdificioEliminar;
        protected void Page_Load(object sender, EventArgs e)
        {
            LLenarTablaEdificios();
        }
        protected void DeshabilitarCasillas()
        {
            txbNomInmueble.Enabled = false;
            txbDireccion.Enabled = false;
            txbAntiguedad.Enabled = false;
            txbCajones.Enabled = false;
            txbNiveles.Enabled = false;
            txbOcupacion.Enabled = false;
            txbPosesion.Enabled = false;
            txbSuperficie.Enabled = false;
            txbTerreno.Enabled = false;
            txbUso.Enabled = false;
        }
        protected void VaciarCasillas()
        {
            txbNomInmueble.Text = "";
            txbDireccion.Text = "";
            txbAntiguedad.Text = "";
            txbCajones.Text = "";
            txbNiveles.Text = "";
            txbOcupacion.Text = "";
            txbPosesion.Text = "";
            txbSuperficie.Text = "";
            txbTerreno.Text = "";
            txbUso.Text = "";
        }

        protected void HabilitarCasillas()
        {
            txbNomInmueble.Enabled = true;
            txbDireccion.Enabled = true;
            txbAntiguedad.Enabled = true;
            txbCajones.Enabled = true;
            txbNiveles.Enabled = true;
            txbOcupacion.Enabled = true;
            txbPosesion.Enabled = true;
            txbSuperficie.Enabled = true;
            txbTerreno.Enabled = true;
            txbUso.Enabled = true;
        }
        private void LLenarTablaEdificios()
        {
            DataTable tableEdificios = new DataTable();
            tableEdificios.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Edificio", typeof(string)),
            });
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand command = new SqlCommand("select IdEdificio, NombreEdificio from Edificios;", sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableEdificios.Rows.Add(
                        dr["IdEdificio"].ToString(),
                        dr["NombreEdificio"].ToString()

                    );
                }
            }
            command.Connection.Close();
            GVEdificios.Columns[4].Visible = false;
            GVEdificios.DataSource = tableEdificios;
            GVEdificios.DataBind();
        }

        protected void btnAgregarInmueble_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand command = new SqlCommand("SPIngresarEdificio", sqlconn);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@NombreEdificio", txbNomInmueble.Text);
                command.Parameters.AddWithValue("@Direccion", txbDireccion.Text);
                command.Parameters.AddWithValue("@TipoPosesion", txbPosesion.Text);
                command.Parameters.AddWithValue("@Ocupacion", txbOcupacion.Text);
                command.Parameters.AddWithValue("@Superficie", txbSuperficie.Text);
                command.Parameters.AddWithValue("@Uso", txbUso.Text);
                command.Parameters.AddWithValue("@Niveles", txbNiveles.Text);
                command.Parameters.AddWithValue("@Cajones", txbCajones.Text);
                command.Parameters.AddWithValue("@Antiguedad", txbAntiguedad.Text);
                command.Parameters.AddWithValue("@Terreno", txbTerreno.Text);
                command.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Edificio agregado');", true);
                command.Connection.Close();
                VaciarCasillas();
                Response.Redirect("ConfAdmi.aspx", false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void GVEdificios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                nombreTituloModal.Text = "Datos del Edificio";
                DeshabilitarCasillas();
                btnActualizar.Visible = false;
                btnAgregarInmueble.Visible = false;

                mpeMostrar.Show();

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVEdificios.Rows[index];
                idEdificioActualizar = row.Cells[0].Text;
                string querySelect = "select * from Edificios where IdEdificio = @IdEdificio";
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@IdEdificio", idEdificioActualizar);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txbNomInmueble.Text = dr["NombreEdificio"].ToString();
                    txbDireccion.Text = dr["Direccion"].ToString();
                    txbSuperficie.Text = dr["Superficie"].ToString();
                    txbNiveles.Text = dr["Niveles"].ToString();
                    txbAntiguedad.Text = dr["Antiguedad"].ToString();
                    txbPosesion.Text = dr["Posesion"].ToString();
                    txbOcupacion.Text = dr["Ocupacion"].ToString();
                    txbUso.Text = dr["Uso"].ToString();
                    txbCajones.Text = dr["Cajones"].ToString();
                    txbTerreno.Text = dr["Terreno"].ToString();

                }
            }
            else
            {
                if (e.CommandName == "Editar")
                {
                    mpeMostrar.Show();
                    HabilitarCasillas();
                    nombreTituloModal.Text = "Editar Datos";
                    btnAgregarInmueble.Visible = false;
                    btnActualizar.Visible = true;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GVEdificios.Rows[index];
                    idEdificioActualizar = row.Cells[0].Text;
                    string querySelect = "select * from Edificios where IdEdificio = @IdEdificio";
                    SqlConnection sqlconn = new SqlConnection(conectar);
                    SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@IdEdificio", idEdificioActualizar);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {

                        txbNomInmueble.Text = dr["NombreEdificio"].ToString();
                        txbDireccion.Text = dr["Direccion"].ToString();
                        txbSuperficie.Text = dr["Superficie"].ToString();
                        txbNiveles.Text = dr["Niveles"].ToString();
                        txbAntiguedad.Text = dr["Antiguedad"].ToString();
                        txbPosesion.Text = dr["Posesion"].ToString();
                        txbOcupacion.Text = dr["Ocupacion"].ToString();
                        txbUso.Text = dr["Uso"].ToString();
                        txbCajones.Text = dr["Cajones"].ToString();
                        txbTerreno.Text = dr["Terreno"].ToString();

                    }

                }
                else
                {
                    if (e.CommandName == "Borrar")
                    {


                        mpeBorrarEdificio.Show();
                        int index = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = GVEdificios.Rows[index];
                        idEdificioEliminar = row.Cells[0].Text;
                        string nombreEdificio = row.Cells[1].Text;
                        nombreEdificiotxt.Text = nombreEdificio;


                    }
                }
            }

        }



        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            String queryUpdate = @"Update Edificios Set NombreEdificio = @Nombre, Direccion = @Direccion, Posesion = @Posesion, Ocupacion = @Ocupacion,
                       Superficie = @Superficie, Uso=@Uso, Niveles=@Niveles, Cajones=@Cajones, Antiguedad=@Antiguedad, Terreno=@Terreno where IdEdificio= @IdEdificio";
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand command = new SqlCommand(queryUpdate, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@Nombre", txbNomInmueble.Text);
            command.Parameters.AddWithValue("@Direccion", txbDireccion.Text);
            command.Parameters.AddWithValue("@Posesion", txbPosesion.Text);
            command.Parameters.AddWithValue("@Ocupacion", txbOcupacion.Text);
            command.Parameters.AddWithValue("@Superficie", txbSuperficie.Text);
            command.Parameters.AddWithValue("@Uso", txbUso.Text);
            command.Parameters.AddWithValue("@Niveles", txbNiveles.Text);
            command.Parameters.AddWithValue("@Cajones", txbCajones.Text);
            command.Parameters.AddWithValue("@Antiguedad", txbAntiguedad.Text);
            command.Parameters.AddWithValue("@Terreno", txbTerreno.Text);
            command.Parameters.AddWithValue("@IdEdificio", idEdificioActualizar);
            command.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se actualizo con exito');", true);
            VaciarCasillas();
            Response.Redirect("ConfAdmi.aspx", false);
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            string queryDelete = "delete from Edificios where IdEdificio = @IdEdificio";
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand(queryDelete, sqlconn);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@idUsuario", idEdificioEliminar);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se ha borrado con exito');", true);
            Response.Redirect("ConfAdmi.aspx", false);
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnActualizar.Visible = false;
            btnAgregarInmueble.Visible = true;
            HabilitarCasillas();
            nombreTituloModal.Text = "Ingresar Datos";
            VaciarCasillas();


        }

        protected void GVEdificios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVEdificios.PageIndex = e.NewPageIndex;
            LLenarTablaEdificios();
        }
    }
}