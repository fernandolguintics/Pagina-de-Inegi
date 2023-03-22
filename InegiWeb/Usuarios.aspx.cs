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
    public partial class Usuarios : System.Web.UI.Page
    {
        string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        public static string idUsuarioActualizar;
        public static string idUsuarioEliminar;
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarTablaUsuarios();
        }
        protected void HabilitarCasillas()
        {
            txbNombres.Enabled = true;
            txbApellidos.Enabled = true;
            txbUsuario.Enabled = true;
            txbContraseña.Enabled = true;
            txbContraseña.Enabled = true;
            Estado.Enabled = true;
            TipoUsuario.Enabled = true;
            txbConfContraseña.Enabled = true;
        }

        protected void VaciarCasillas()
        {
            txbNombres.Text = "";
            txbApellidos.Text = "";
            txbUsuario.Text = "";
            txbContraseña.Text = "";
            txbConfContraseña.Text = "";
        }

        protected void DeshabilitarCasillas()
        {
            txbNombres.Enabled = false;
            txbApellidos.Enabled = false;
            txbUsuario.Enabled = false;
            txbContraseña.Enabled = false;
            Estado.Enabled = false;
            TipoUsuario.Enabled = false;
            txbConfContraseña.Enabled = false;
            btnAgregar.Visible = false;
        }

        private void LlenarTablaUsuarios()
        {
            DataTable tableUser = new DataTable();
            tableUser.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Nombres", typeof(string)),
                new DataColumn("Apellidos", typeof(string)),
                new DataColumn("Usuario", typeof(string)),
                new DataColumn("Estado", typeof(string)),
            });
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand command = new SqlCommand("Select IdUsuario, Nombres, Apellidos, Usuario, Estado from Usuarios", sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableUser.Rows.Add(
                        dr["IdUsuario"].ToString(),
                        dr["Nombres"].ToString(),
                        dr["Apellidos"].ToString(),
                        dr["Usuario"].ToString(),
                        dr["Estado"].ToString()
                    );
                }
            }
            command.Connection.Close();

            GVUsuario.DataSource = tableUser;
            GVUsuario.DataBind();

        }

        protected void btnAgregarUsuario(object sender, EventArgs e)
        {

            if (txbContraseña.Text.Equals(txbConfContraseña.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se ha guardado el usuario');", true);
                String fecha = DateTime.Now.ToString("yyyy-MM-dd");
                String query = @"Insert into Usuarios(Nombres,Apellidos,Usuario,TipoUsuario,Contraseña,FechaInicio,FechaModi,Estado) 
                            values(@nombres,@apellidos,@usuario,@tipoUsuario,@contraseña,@fechaInicio, @fechaModi,@estado);";
                try
                {
                    SqlConnection sqlconn = new SqlConnection(conectar);
                    SqlCommand command = new SqlCommand(query, sqlconn);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@nombres", txbNombres.Text);
                    command.Parameters.AddWithValue("@apellidos", txbApellidos.Text);
                    command.Parameters.AddWithValue("@usuario", txbUsuario.Text);
                    command.Parameters.AddWithValue("@tipoUsuario", TipoUsuario.Text);
                    command.Parameters.AddWithValue("@contraseña", txbContraseña.Text);
                    command.Parameters.AddWithValue("@fechaInicio", fecha);
                    command.Parameters.AddWithValue("@fechaModi", fecha);
                    command.Parameters.AddWithValue("@estado", Estado.Text);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                    VaciarCasillas();
                    Response.Redirect("Usuarios.aspx");


                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Hubo un error al guardar el usuario');", true);
                    Console.WriteLine(ex.ToString());
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Las contraseñas no coinciden');", true);
            }


        }



        protected void GVUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // comando utilizado
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectRow = GVUsuario.Rows[index];
                string idUsuario = selectRow.Cells[0].Text;
                string querySelect = "select * from Usuarios where IdUsuario = @IdUsuario";
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    mpeMostrar.Show();
                    modalTitleText.Text = "Datos del Usuario";
                    txbNombres.Text = dr["Nombres"].ToString();
                    txbApellidos.Text = dr["Apellidos"].ToString();
                    txbUsuario.Text = dr["Usuario"].ToString();
                    txbContraseña.Text = dr["Contraseña"].ToString();
                    txbConfContraseña.TextMode = TextBoxMode.SingleLine;
                    txbConfContraseña.Text = dr["Contraseña"].ToString();
                    TipoUsuario.Text = dr["TipoUsuario"].ToString();
                    Estado.Text = dr["Estado"].ToString();
                    DeshabilitarCasillas();

                }
                cmd.Connection.Close();
            }
            else
            {
                if (e.CommandName == "Editar")
                {
                    mpeMostrar.Show();
                    modalTitleText.Text = "Actualizar Usuario";
                    btnAgregar.Visible = false;
                    btnActualizar.Visible = true;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GVUsuario.Rows[index];
                    idUsuarioActualizar = row.Cells[0].Text;
                    string querySelect = "select * from Usuarios where IdUsuario = @IdUsuario";
                    SqlConnection sqlconn = new SqlConnection(conectar);
                    SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuarioActualizar);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {

                        txbNombres.Text = dr["Nombres"].ToString();
                        txbApellidos.Text = dr["Apellidos"].ToString();
                        txbUsuario.Text = dr["Usuario"].ToString();
                        txbContraseña.Text = dr["Contraseña"].ToString();
                        txbConfContraseña.TextMode = TextBoxMode.SingleLine;
                        txbConfContraseña.Text = dr["Contraseña"].ToString();
                        TipoUsuario.Text = dr["TipoUsuario"].ToString();
                        Estado.Text = dr["Estado"].ToString();

                        HabilitarCasillas();
                    }

                }
                else
                {
                    if (e.CommandName == "Borrar")
                    {


                        mpeBorrarUsuario.Show();
                        int index = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = GVUsuario.Rows[index];
                        idUsuarioEliminar = row.Cells[0].Text;
                        string nombreUsuario = row.Cells[1].Text + " " + row.Cells[2].Text;
                        nombreUsuariotxt.Text = nombreUsuario;


                    }
                }
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            VaciarCasillas();
            HabilitarCasillas();
            btnActualizar.Visible = false;
            btnAgregar.Visible = true;
            modalTitleText.Text = "Ingresar Usuario";
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txbContraseña.Text.Equals(txbConfContraseña.Text))
            {

                String fecha = DateTime.Now.ToString("yyyy-MM-dd");
                String update = @"Update Usuarios Set Nombres = @Nombres, Apellidos = @Apellidos, Usuario = @Usuario, TipoUsuario = @TipoUsuario,
                       Contraseña = @Contraseña, FechaModi=@FechaModi, Estado = @Estado where IdUsuario= @IdUsuario";
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand command = new SqlCommand(update, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Nombres", txbNombres.Text);
                command.Parameters.AddWithValue("@Apellidos", txbApellidos.Text);
                command.Parameters.AddWithValue("@Usuario", txbUsuario.Text);
                command.Parameters.AddWithValue("@TipoUsuario", TipoUsuario.Text);
                command.Parameters.AddWithValue("@Contraseña", txbContraseña.Text);
                command.Parameters.AddWithValue("@FechaModi", fecha);
                command.Parameters.AddWithValue("@Estado", Estado.Text);
                command.Parameters.AddWithValue("@IdUsuario", idUsuarioActualizar);
                command.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se actualizo con exito');", true);
                VaciarCasillas();
                Response.Redirect("Usuarios.aspx", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Las contraseñas no coinciden');", true);
            }

        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            string queryDelete = "delete from Usuarios where IdUsuario = @IdUsuario";
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand(queryDelete, sqlconn);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@idUsuario", idUsuarioEliminar);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se ha borrado el registro con exito');", true);
            Response.Redirect("Usuarios.aspx", false);
        }

        protected void GVUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVUsuario.PageIndex = e.NewPageIndex;
            LlenarTablaUsuarios();
        }
    }
}