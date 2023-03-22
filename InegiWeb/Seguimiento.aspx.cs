using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace InegiWeb
{
    public partial class Seguimiento : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static string idMantenimiento;
        static string deleteid;
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarTablaMantenimiento();
        }
        protected void GVMantenimientos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVMantenimientos.PageIndex = e.NewPageIndex;
            LlenarTablaMantenimiento();
        }

        private void LlenarTablaMantenimiento()
        {
            DataTable tableMantenimiento = new DataTable();
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            string IdUsuario = Convert.ToString(Session["IdUsuario"]);

            tableMantenimiento.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("IdMantenimiento", typeof(string)),
                new DataColumn("Inmueble", typeof(string)),
                new DataColumn("Fecha", typeof(string)),
                new DataColumn("TotalElementosInstalados", typeof(string)),
                new DataColumn("Defectuosos", typeof(string)),
                new DataColumn("Evaluacion", typeof(string)),
                new DataColumn("CostoTotal", typeof(string)),
                new DataColumn("Usuario", typeof(string))
            });

            if (tipoUsuario.Equals("Tecnico")) //Permite ver solo los mantenimientos del tecnico
            {
                string selectMantenimiento = @"select IdMantenimiento,NombreEdificio,FORMAT(FechaMantenimiento,'dd/MM/yyyy') as 'FechaMantenimiento',
                                    TotalElementosInstalados,Defectuosos,FORMAT(Evaluacion,'N','en-us') as 'Evaluacion',FORMAT(CostoTotal,'C','en-us') as 'CostoTotal' from Mantenimientos 
                                    inner join Edificios on Mantenimientos.IdEdificio = Edificios.IdEdificio where IdUsuario=@Id";
                SqlCommand command = new SqlCommand(selectMantenimiento, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Id", IdUsuario);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        tableMantenimiento.Rows.Add(
                            dataReader["IdMantenimiento"].ToString(),
                            dataReader["NombreEdificio"].ToString(),
                            dataReader["FechaMantenimiento"].ToString(),
                            dataReader["TotalElementosInstalados"].ToString(),
                            dataReader["Defectuosos"].ToString(),
                            dataReader["Evaluacion"].ToString() + "%",
                            dataReader["CostoTotal"].ToString()
                            );
                    }
                }
                command.Connection.Close();
                GVMantenimientos.Columns[9].Visible = false;
                GVMantenimientos.DataSource = tableMantenimiento;
                GVMantenimientos.DataBind();
            }
            else
            {
                string selectMantenimiento = @"select Nombres,Apellidos,IdMantenimiento,NombreEdificio,FORMAT(FechaMantenimiento,'dd/MM/yyyy') as 'FechaMantenimiento',
                                    TotalElementosInstalados,Defectuosos,FORMAT(Evaluacion,'N','en-us') as 'Evaluacion',FORMAT(CostoTotal,'C','en-us') as 'CostoTotal' from Mantenimientos 
                                    inner join Edificios on Mantenimientos.IdEdificio = Edificios.IdEdificio inner join Usuarios on
                                    Mantenimientos.IdUsuario = Usuarios.IdUsuario";
                SqlCommand command = new SqlCommand(selectMantenimiento, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Id", IdUsuario);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        tableMantenimiento.Rows.Add(
                            dataReader["IdMantenimiento"].ToString(),
                            dataReader["NombreEdificio"].ToString(),
                            dataReader["FechaMantenimiento"].ToString(),
                            dataReader["TotalElementosInstalados"].ToString(),
                            dataReader["Defectuosos"].ToString(),
                            dataReader["Evaluacion"].ToString() + "%",
                            dataReader["CostoTotal"].ToString(),
                             dataReader["Nombres"].ToString() + " " + dataReader["Apellidos"].ToString()
                            );
                    }
                }
                command.Connection.Close();
                GVMantenimientos.Columns[7].Visible = true;
                GVMantenimientos.DataSource = tableMantenimiento;
                GVMantenimientos.DataBind();
            }

        }

        protected void GVMantenimientos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalles")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVMantenimientos.Rows[index];
                idMantenimiento = row.Cells[0].Text;
                Response.Redirect("VerMantenimientoInfraestructura.aspx", false);
            }
            else
            {
                if (e.CommandName == "Borrar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GVMantenimientos.Rows[index];
                    deleteid = row.Cells[0].Text;
                    string searchReport = "select IdReporte from Reportes where IdMantenimiento = @Id";
                    SqlCommand cmd = new SqlCommand(searchReport, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@Id", deleteid);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede borrar el mantenimiento, ya tiene reportes');", true);
                    }
                    else
                    {
                        mpeBorrar.Show();
                        nombreMantenimientotxt.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);

                    }


                }
                else
                {
                    if (e.CommandName == "Reporte")
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = GVMantenimientos.Rows[index];
                        idMantenimiento = row.Cells[0].Text;
                        Response.Redirect("ReporteInfraestructura.aspx", false);
                    }
                }
            }
        }

        protected void GVMantenimientos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Defectuosos"].ToString().Equals("0"))
                {

                    e.Row.BackColor = System.Drawing.Color.FromArgb(244, 255, 70);
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 137, 137);
                }
            }
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {

            string sqlDelete = "delete from DetallesMaterialesMantenimiento where IdMantenimiento = @id";
            SqlCommand command = new SqlCommand(sqlDelete, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@id", deleteid);
            command.ExecuteNonQuery();
            command.Connection.Close();

            string sqlDeleteMantenimiento = "delete from Mantenimientos where IdMantenimiento = @id";
            SqlCommand command2 = new SqlCommand(sqlDeleteMantenimiento, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@id", deleteid);
            command2.ExecuteNonQuery();
            command2.Connection.Close();
            Response.Redirect("Seguimiento.aspx", false);
        }
    }
}