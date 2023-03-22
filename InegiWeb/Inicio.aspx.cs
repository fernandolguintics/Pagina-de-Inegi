using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace InegiWeb
{
    public partial class Inicio : System.Web.UI.Page
    {
      
      
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void AccederPagina(object sender, EventArgs e)
        {

            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand command = new SqlCommand("SP_ValidarUsuario", sqlconn);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                command.Connection.Open();
                command.Parameters.AddWithValue("@Usuario", tbUsuario.Text);
                command.Parameters.AddWithValue("@Contraseña", tbContraseña.Text);
                command.Parameters.AddWithValue("@IdUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@Nombres", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                command.Parameters.Add("@Apellidos", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                command.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                command.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();

                Session["IdUsuario"] = Convert.ToInt32(command.Parameters["@IdUsuario"].Value.ToString());
                Session["Nombres"] = command.Parameters["@Nombres"].Value.ToString();
                Session["Apellidos"] = command.Parameters["@Apellidos"].Value.ToString();
                Session["TipoUsuario"] = command.Parameters["@TipoUsuario"].Value.ToString();
                string estado = command.Parameters["@Estado"].Value.ToString();

                if (estado.Equals("Activo"))
                {

                    Response.Redirect("Menu.aspx", false);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('El usuario y/o contraseña no coinciden');", true);

            }

            command.Connection.Close();
        }
    }
}