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
    public partial class MenuReportes : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static string nombreEdificio;
        public static string idReport;
        public static string fechaReport;
        public static string accion;
        static string fecha1;
        static string fecha2; //Rango de Fechas
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
                new DataColumn("CostoTotal", typeof(string)),
                new DataColumn("Validacion", typeof(string)),
                new DataColumn("Usuarios", typeof(string))
            });
            if (tipoUsuario.Equals("Tecnico"))
            {
                string getReports = @"Select IdReporte,NombreEdificio,FORMAT(FechaReporte,'dd/MM/yyyy') as 'FechaReporte',
                                FORMAT(Reportes.CostoTotal,'C','en-us') as 'CostoTotal', TotalArreglados, Validacion from Reportes 
                                inner join Mantenimientos on Reportes.IdMantenimiento= Mantenimientos.IdMantenimiento inner join Edificios ON Mantenimientos.IdEdificio = Edificios.IdEdificio where FechaReporte between @Fecha1 and @Fecha2 and Reportes.IdUsuario=@Id";
                SqlCommand command = new SqlCommand(getReports, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Fecha1", fecha1);
                command.Parameters.AddWithValue("@Fecha2", fecha2);
                command.Parameters.AddWithValue("@Id", IdUsuario);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    tableReports.Rows.Add(
                        dr["IdReporte"].ToString(),
                        dr["NombreEdificio"].ToString(),
                        dr["FechaReporte"].ToString(),
                        dr["TotalArreglados"].ToString(),
                        dr["CostoTotal"].ToString(),
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
                string getReports = @"Select Nombres,Apellidos,IdReporte,NombreEdificio,FORMAT(FechaReporte,'dd/MM/yyyy') as 'FechaReporte',
                                FORMAT(Reportes.CostoTotal,'C','en-us') as 'CostoTotal', TotalArreglados, Validacion from Reportes 
                                inner join Mantenimientos on Reportes.IdMantenimiento= Mantenimientos.IdMantenimiento inner join Edificios ON Mantenimientos.IdEdificio = Edificios.IdEdificio
                                inner join Usuarios on Mantenimientos.IdUsuario = Usuarios.IdUsuario where FechaReporte between @Fecha1 and @Fecha2";
                SqlCommand command = new SqlCommand(getReports, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Fecha1", fecha1);
                command.Parameters.AddWithValue("@Fecha2", fecha2);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    tableReports.Rows.Add(
                        dr["IdReporte"].ToString(),
                        dr["NombreEdificio"].ToString(),
                        dr["FechaReporte"].ToString(),
                        dr["TotalArreglados"].ToString(),
                        dr["CostoTotal"].ToString(),
                        dr["Validacion"].ToString(),
                        dr["Nombres"].ToString() + " " + dr["Apellidos"].ToString()
                        );
                }
                command.Connection.Close();
                GVReportes.Columns[6].Visible = true;
                GVReportes.DataSource = tableReports;
                GVReportes.DataBind();
                GVReportes.Visible = true;
            }

        }



        protected void GVReportes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVReportes.PageIndex = e.NewPageIndex;
            LlenarTabla();
        }

        protected void GVReportes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                accion = "Ver";
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVReportes.Rows[index];
                idReport = row.Cells[0].Text;
                fechaReport = row.Cells[2].Text;
                nombreEdificio = HttpUtility.HtmlDecode(row.Cells[1].Text);
                Response.Redirect("ValidacionReportes.aspx", false);
            }
            else //Modificar
            {
                accion = "Modificar";
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVReportes.Rows[index];
                idReport = row.Cells[0].Text;
                fechaReport = row.Cells[2].Text;
                nombreEdificio = HttpUtility.HtmlDecode(row.Cells[1].Text);
                Response.Redirect("ValidacionReportes", false);


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