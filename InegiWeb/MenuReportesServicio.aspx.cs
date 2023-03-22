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
    public partial class MenuReportesServicio : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static string nombreEdificio;
        public static string idReport;
        public static string fechaReport;
        public static string accion;
        public static string idMantenimiento;
        static string fecha1;
        static string fecha2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LLenarDropDownList();
            }
        }
        private void LLenarDropDownList()
        {


            int currentYear = DateTime.Now.Year;

            for (int i = 2000; i <= currentYear; i++)
            {
                ddlseleccionarAño.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void LlenarTabla()
        {
            string IdUsuario = Convert.ToString(Session["IdUsuario"]);
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            DataTable tableReports = new DataTable();

            tableReports.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("IdReporte", typeof(string)),
                new DataColumn("NombreEdificio", typeof(string)),
                new DataColumn("FechaReporte", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("Validacion", typeof(string)),
                 new DataColumn("Usuarios", typeof(string))
            });
            if (tipoUsuario.Equals("Tecnico"))
            {
                string getReports = @"Select IdReporteServicios,NombreEdificio,FORMAT(FechaEvaluacion,'dd/MM/yyyy') as 'FechaReporte',
                                Arreglados, Validacion from ReportesServicios
                                inner join MantenimientoServicios on ReportesServicios.IdMantenimiento= MantenimientoServicios.IdMantenimientoServicio inner join Edificios ON MantenimientoServicios.IdEdificio = Edificios.IdEdificio where FechaEvaluacion between @Fecha1 and @Fecha2 and ReportesServicios.IdUsuario=@Id";
                SqlCommand command = new SqlCommand(getReports, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Fecha1", fecha1);
                command.Parameters.AddWithValue("@Fecha2", fecha2);
                command.Parameters.AddWithValue("@Id", IdUsuario);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    tableReports.Rows.Add(
                        dr["IdReporteServicios"].ToString(),
                        dr["NombreEdificio"].ToString(),
                        dr["FechaReporte"].ToString(),
                        dr["Arreglados"].ToString(),
                        dr["Validacion"].ToString()
                        );
                }
                command.Connection.Close();
                GVReportes.DataSource = tableReports;
                GVReportes.DataBind();
                GVReportes.Visible = true;
            }
            else
            {
                string getReports = @"Select Nombres,Apellidos,IdReporteServicios,NombreEdificio,FORMAT(FechaEvaluacion,'dd/MM/yyyy') as 'FechaReporte',
                                Arreglados, Validacion from ReportesServicios
                                inner join MantenimientoServicios on ReportesServicios.IdMantenimiento= MantenimientoServicios.IdMantenimientoServicio 
                                inner join Edificios ON MantenimientoServicios.IdEdificio = Edificios.IdEdificio 
                                inner join Usuarios on ReportesServicios.IdUsuario=Usuarios.IdUsuario where FechaEvaluacion between @Fecha1 and @Fecha2";
                SqlCommand command = new SqlCommand(getReports, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Fecha1", fecha1);
                command.Parameters.AddWithValue("@Fecha2", fecha2);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    tableReports.Rows.Add(
                        dr["IdReporteServicios"].ToString(),
                        dr["NombreEdificio"].ToString(),
                        dr["FechaReporte"].ToString(),
                        dr["Arreglados"].ToString(),
                        dr["Validacion"].ToString(),
                        dr["Nombres"].ToString() + " " + dr["Apellidos"].ToString()
                        ); ;
                }
                command.Connection.Close();
                GVReportes.Columns[5].Visible = true;
                GVReportes.DataSource = tableReports;
                GVReportes.DataBind();
                GVReportes.Visible = true;
            }

        }

        protected void GVReportes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Validacion"].ToString().Equals("Pendiente"))
                {

                    e.Row.BackColor = System.Drawing.Color.FromArgb(244, 255, 70);
                }
                else
                {
                    if (drv["Validacion"].ToString().Equals("Aprobado"))
                    {
                        e.Row.BackColor = System.Drawing.Color.FromArgb(129, 255, 101);
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.FromArgb(255, 137, 137);
                    }

                }
            }
        }

        protected void GVReportes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                accion = "Ver";
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVReportes.Rows[index];
                string getid = "select IdMantenimiento from ReportesServicios where IdReporteServicios=@id";
                SqlCommand comm = new SqlCommand(getid, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@id", row.Cells[0].Text);
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    idMantenimiento = dr["IdMantenimiento"].ToString();
                    comm.Connection.Close();
                }
                idReport = row.Cells[0].Text;
                fechaReport = row.Cells[2].Text;
                nombreEdificio = HttpUtility.HtmlDecode(row.Cells[1].Text);
                Response.Redirect("ValidacionReporteServicio.aspx", false);
            }
            else //Modificar
            {
                accion = "Modificar";
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVReportes.Rows[index];
                string getid = "select IdMantenimiento from ReportesServicios where IdReporteServicios=@id";
                SqlCommand comm = new SqlCommand(getid, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@id", row.Cells[0].Text);
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    idMantenimiento = dr["IdMantenimiento"].ToString();
                    comm.Connection.Close();
                }
                idReport = row.Cells[0].Text;
                fechaReport = row.Cells[2].Text;
                nombreEdificio = HttpUtility.HtmlDecode(row.Cells[1].Text);
                Response.Redirect("ValidacionReporteServicio.aspx", false);


            }
        }

        protected void GVReportes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVReportes.PageIndex = e.NewPageIndex;
            LlenarTabla();
        }

        protected void seleccionarBtn_Click(object sender, EventArgs e)
        {
            string trimestre = ddlTrimestre.Text;
            switch (trimestre)
            {
                case "E-M":
                    fecha1 = ddlseleccionarAño.Text + "0101"; //Asignar Rango de Fechas para String
                    fecha2 = ddlseleccionarAño.Text + "0331";
                    LlenarTabla();
                    break;

                case "A-J":
                    fecha1 = ddlseleccionarAño.Text + "0401"; //Asignar Rango de Fechas para String
                    fecha2 = ddlseleccionarAño.Text + "0630";
                    LlenarTabla();
                    break;
                case "J-S":
                    fecha1 = ddlseleccionarAño.Text + "0701"; //Asignar Rango de Fechas para String
                    fecha2 = ddlseleccionarAño.Text + "0930";
                    LlenarTabla();
                    break;
                case "O-D":
                    fecha1 = ddlseleccionarAño.Text + "1001"; //Asignar Rango de Fechas para String
                    fecha2 = ddlseleccionarAño.Text + "1231";
                    LlenarTabla();
                    break;
            }
        }
    }
}