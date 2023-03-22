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
    public partial class SeguimientoAlmacenes : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static string idMantenimiento;
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarTablaMantenimiento();
        }
        private void LlenarTablaMantenimiento()
        {
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            DataTable tableMantenimiento = new DataTable();
            string IdUsuario = Convert.ToString(Session["IdUsuario"]);

            tableMantenimiento.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("IdMantenimiento", typeof(string)),
                new DataColumn("NombreEdificio", typeof(string)),
                new DataColumn("Fecha", typeof(string)),
                new DataColumn("FechaActualizacion", typeof(string)),
                new DataColumn("Consumo", typeof(string)),
                new DataColumn("Inventario", typeof(string)),
                new DataColumn("Concentracion", typeof(string)),
                new DataColumn("Promedio", typeof(string)),
                new DataColumn("Usuario", typeof(string))
            });

            if (tipoUsuario.Equals("Tecnico")) //Permite ver solo los mantenimientos del tecnico
            {
                string selectMantenimiento = @"select IdMantenimientoAlmacen,FORMAT(Fecha,'dd/MM/yyyy') as 'Fecha',FORMAT(FechaActualizacion,'dd/MM/yyyy') as 'FechaActualizacion',FORMAT(Consumo,'N','en-us') as 'Consumo',
                                               FORMAT(Inventario,'N','en-us') as 'Inventario',FORMAT(Concentracion,'N','en-us') as 'Concentracion',
                                               FORMAT(Promedio,'N','en-us') as 'Promedio', NombreEdificio from MantenimientosAlmacen inner join Edificios on MantenimientosAlmacen.IdEdificio = Edificios.IdEdificio where IdUsuario=@Id";
                SqlCommand command = new SqlCommand(selectMantenimiento, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Id", IdUsuario);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        tableMantenimiento.Rows.Add(
                            dataReader["IdMantenimientoAlmacen"].ToString(),
                            dataReader["NombreEdificio"].ToString(),
                            dataReader["Fecha"].ToString(),
                            dataReader["FechaActualizacion"].ToString(),
                            dataReader["Consumo"].ToString(),
                            dataReader["Inventario"].ToString(),
                            dataReader["Concentracion"].ToString(),
                            dataReader["Promedio"].ToString()
                            );
                    }
                }
                command.Connection.Close();
                GVMantenimientoAlmacenes.DataSource = tableMantenimiento;
                GVMantenimientoAlmacenes.DataBind();
            }
            else
            {

                string selectMantenimiento = @"select IdMantenimientoAlmacen,FORMAT(Fecha,'dd/MM/yyyy') as 'Fecha',FORMAT(FechaActualizacion,'dd/MM/yyyy') as 'FechaActualizacion',FORMAT(Consumo,'N','en-us') as 'Consumo',
                                               FORMAT(Inventario,'N','en-us') as 'Inventario',FORMAT(Concentracion,'N','en-us') as 'Concentracion',
                                               FORMAT(Promedio,'N','en-us') as 'Promedio', NombreEdificio, Nombres,Apellidos from MantenimientosAlmacen 
                                    inner join Edificios on MantenimientosAlmacen.IdEdificio = Edificios.IdEdificio inner join Usuarios
                                    on MantenimientosAlmacen.IdUsuario = Usuarios.IdUsuario";
                SqlCommand command = new SqlCommand(selectMantenimiento, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Id", IdUsuario);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        tableMantenimiento.Rows.Add(
                            dataReader["IdMantenimientoAlmacen"].ToString(),
                            dataReader["NombreEdificio"].ToString(),
                            dataReader["Fecha"].ToString(),
                             dataReader["FechaActualizacion"].ToString(),
                            dataReader["Consumo"].ToString(),
                            dataReader["Inventario"].ToString(),
                            dataReader["Concentracion"].ToString(),
                            dataReader["Promedio"].ToString(),
                            dataReader["Nombres"].ToString() + " " + dataReader["Apellidos"].ToString()
                            );
                    }
                }
                command.Connection.Close();
                GVMantenimientoAlmacenes.Columns[7].Visible = true; //Muestra columna de Usuarios
                GVMantenimientoAlmacenes.DataSource = tableMantenimiento;
                GVMantenimientoAlmacenes.DataBind();
            }

        }

        protected void GVMantenimientoAlmacenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (Convert.ToDouble(drv["Promedio"].ToString()) == 10.0)
                {

                    e.Row.BackColor = System.Drawing.Color.FromArgb(244, 255, 70);
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 137, 137);
                }
            }
        }

        protected void GVMantenimientoAlmacenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalles")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVMantenimientoAlmacenes.Rows[index];
                string idMantenimiento = row.Cells[0].Text;
                Response.Redirect("VerMantenimientoAlmacen.aspx?id=" + idMantenimiento, false);
            }
            else
            {
                if (e.CommandName == "Actualizar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GVMantenimientoAlmacenes.Rows[index];
                    string idMantenimiento = row.Cells[0].Text;
                    Response.Redirect("ActualizarAlmacen.aspx?id=" + idMantenimiento, false);
                }
            }
        }

        protected void GVMantenimientoAlmacenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVMantenimientoAlmacenes.PageIndex = e.NewPageIndex;
            LlenarTablaMantenimiento();
        }
    }
}