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
using Microsoft.Reporting.WebForms;
using Microsoft.Ajax.Utilities;

namespace InegiWeb
{
    public partial class SeguimientoServicios : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static string idMantenimiento;
        static string deleteid;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                new DataColumn("Evaluacion", typeof(string)),
               new DataColumn("Costo", typeof(string)),
                new DataColumn("Usuario", typeof(string))
            });

            if (tipoUsuario.Equals("Tecnico")) //Permite ver solo los mantenimientos del tecnico
            {
                string selectMantenimiento = @"select IdMantenimientoServicio,NombreEdificio,FORMAT(EvaluacionFinal,'N','en-us') as 'EvaluacionFinal',FORMAT(Fecha,'dd/MM/yyyy') as 'FechaEvaluacion',FORMAT(CostoTotal,'C','en-us') as 'CostoTotal' from MantenimientoServicios
                                    inner join Edificios on MantenimientoServicios.IdEdificio = Edificios.IdEdificio where IdUsuario=@Id";
                SqlCommand command = new SqlCommand(selectMantenimiento, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Id", IdUsuario);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        tableMantenimiento.Rows.Add(
                            dataReader["IdMantenimientoServicio"].ToString(),
                            dataReader["NombreEdificio"].ToString(),
                            dataReader["FechaEvaluacion"].ToString(),
                            dataReader["EvaluacionFinal"].ToString() + "%",
                            dataReader["CostoTotal"].ToString()
                            );
                    }
                }
                command.Connection.Close();
                GVMantenimientoServicios.Columns[7].Visible = false;
                GVMantenimientoServicios.DataSource = tableMantenimiento;
                GVMantenimientoServicios.DataBind();
            }
            else
            {
                string selectMantenimiento = @"select Nombres,Apellidos, IdMantenimientoServicio,NombreEdificio,FORMAT(EvaluacionFinal,'N','en-us') as 'EvaluacionFinal',FORMAT(Fecha,'dd/MM/yyyy') as 'FechaEvaluacion', FORMAT(CostoTotal,'C','en-us') as 'CostoTotal' from MantenimientoServicios
                                    inner join Edificios on MantenimientoServicios.IdEdificio = Edificios.IdEdificio inner join Usuarios on MantenimientoServicios.IdUsuario=Usuarios.IdUsuario";
                SqlCommand command = new SqlCommand(selectMantenimiento, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Id", IdUsuario);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        tableMantenimiento.Rows.Add(
                           dataReader["IdMantenimientoServicio"].ToString(),
                           dataReader["NombreEdificio"].ToString(),
                           dataReader["FechaEvaluacion"].ToString(),
                           dataReader["EvaluacionFinal"].ToString() + "%",
                           dataReader["CostoTotal"].ToString(),
                           dataReader["Nombres"].ToString() + " " + dataReader["Apellidos"].ToString()
                           );
                    }
                }
                command.Connection.Close();
                GVMantenimientoServicios.Columns[5].Visible = true;
                GVMantenimientoServicios.DataSource = tableMantenimiento;
                GVMantenimientoServicios.DataBind();
            }

        }

        protected void GVMantenimientoServicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Evaluacion"].ToString().Equals("100%"))
                {

                    e.Row.BackColor = System.Drawing.Color.FromArgb(244, 255, 70);
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 137, 137);
                }
            }
        }

        protected void GVMantenimientoServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalles")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVMantenimientoServicios.Rows[index];
                idMantenimiento = row.Cells[0].Text;
                
                Response.Redirect("VerMantenimientoServicios.aspx", false);
            }
            else
            {
                if (e.CommandName == "Borrar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GVMantenimientoServicios.Rows[index];
                    deleteid = row.Cells[0].Text;
                    string searchReport = "select IdReporteServicios from ReportesServicios where IdMantenimiento = @Id";
                    SqlCommand cmd = new SqlCommand(searchReport, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@Id", deleteid);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede borrar el mantenimiento ya tiene reportes');", true);

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
                        GridViewRow row = GVMantenimientoServicios.Rows[index];
                        idMantenimiento = row.Cells[0].Text;
                        Response.Redirect("ReporteServicios.aspx", false);
                    }
                    else
                    {
                        if (e.CommandName == "Imprimir")
                        {
                            int index = Convert.ToInt32(e.CommandArgument);
                            GridViewRow row = GVMantenimientoServicios.Rows[index];
                            idMantenimiento = row.Cells[0].Text;
                            string nombreInmueble = row.Cells[1].Text;
                            string fecha = row.Cells[2].Text;
                            string evaluacion = row.Cells[3].Text;
                            string selectMalEstado = "select COUNT(MalEstado) as 'Mal Estado' from DetallesMantenimientoServicio where IdMantenimiento=@id";
                            SqlCommand comm = new SqlCommand(selectMalEstado, sqlconn);
                            comm.Connection.Open();
                            comm.Parameters.AddWithValue("@id", idMantenimiento);
                            SqlDataReader dr = comm.ExecuteReader();
                            if (dr.Read())
                            {
                                string malEstado = dr["Mal Estado"].ToString();
                                comm.Connection.Close();
                                string queryDireccion = @"select Direccion from MantenimientoServicios inner join 
                                        Edificios on MantenimientoServicios.IdEdificio = Edificios.IdEdificio where IdMantenimientoServicio = @IdMantenimiento";
                                SqlCommand comm2 = new SqlCommand(queryDireccion, sqlconn);
                                comm2.Connection.Open();
                                comm2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
                                SqlDataReader dr2 = comm2.ExecuteReader();
                                if (dr2.Read())
                                {
                                    string direccion = dr2["Direccion"].ToString();
                                    Imprimir(idMantenimiento, evaluacion, nombreInmueble, fecha, malEstado,direccion);
                                }
                               
                            }
                            
                        }
                    }
                }
            }
        }

        protected void GVMantenimientoServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVMantenimientoServicios.PageIndex = e.NewPageIndex;
            LlenarTablaMantenimiento();
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            string sqlDelete = "delete from DetallesMantenimientoServicio where IdMantenimiento = @id";
            SqlCommand command = new SqlCommand(sqlDelete, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@id", deleteid);
            command.ExecuteNonQuery();
            command.Connection.Close();

            string sqlDeleteMantenimiento = "delete from MantenimientoServicios where IdMantenimiento = @id";
            SqlCommand command2 = new SqlCommand(sqlDeleteMantenimiento, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@id", deleteid);
            command2.ExecuteNonQuery();
            command2.Connection.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se borro el mantenimiento con exito');", true);
            Response.Redirect("SeguimientoServicios.aspx", false);
        }
        protected void Imprimir(string id, string evaluacion, string nombreInmueble, string fecha, string malEstado,string direccion)
        {
           
            string select = @"select ROW_NUMBER() OVER (ORDER BY nombreMaterial ASC) as 'No',CONCAT(NombreMaterial, '', Descripcion) as 'nombreMaterial',UnidadMedida,
                        FORMAT(Precio, 'C', 'en-us') as 'Precio',  Cantidad,FORMAT(Subtotal, 'C', 'en-us') as 'Subtotal2',Subtotal from ListaMateriales inner join 
                        MaterialesServicio on ListaMateriales.IdMaterial = MaterialesServicio.IdMaterialesServicio where IdMantenimiento = @id and MaterialesServicio.Responsable = 'INEGI'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlconn);
            da.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rp = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rp);

            string select2 = @"select ROW_NUMBER() OVER (ORDER BY nombreMaterial ASC) as 'No',CONCAT(NombreMaterial, '', Descripcion) as 'nombreMaterial',UnidadMedida,
                        FORMAT(Precio, 'C', 'en-us') as 'Precio',  Cantidad,FORMAT(Subtotal, 'C', 'en-us') as 'Subtotal2',Subtotal from ListaMateriales inner join 
                        MaterialesServicio on ListaMateriales.IdMaterial = MaterialesServicio.IdMaterialesServicio where IdMantenimiento = @id and MaterialesServicio.Responsable = 'Contrato'";
            SqlDataAdapter da2 = new SqlDataAdapter(select2, sqlconn);
            da2.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            ReportDataSource rp2 = new ReportDataSource("DataSet2", dt2);
            ReportViewer1.LocalReport.DataSources.Add(rp2);
            mpeImprimir.Show();
            ReportParameter[] reportParameters = new ReportParameter[5];
            reportParameters[0] = new ReportParameter("Inmueble", nombreInmueble.ToUpper());
            reportParameters[1] = new ReportParameter("Direccion", direccion.ToUpper());
            reportParameters[2] = new ReportParameter("Fecha", fecha.ToUpper());
            reportParameters[3] = new ReportParameter("Malestado", malEstado.ToUpper());
            reportParameters[4] = new ReportParameter("Evaluacion", evaluacion);
            ReportViewer1.LocalReport.SetParameters(reportParameters);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}