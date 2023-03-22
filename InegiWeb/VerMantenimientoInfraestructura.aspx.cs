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

namespace InegiWeb
{
    public partial class VerMantenimientoInfraestructura : System.Web.UI.Page
    {
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        string idMantenimiento = Seguimiento.idMantenimiento;
        int totalMalEstado = 0, totalMalEstado2 = 0;
        double evaluacion, evaluacion2;
        static DataTable tableInstalacion;
        static DataTable tableIlluminacion;
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarTablaInstalacion();
            LlenarTablaIlluminacion();
            ObtenerDatos();
        }
        private void LlenarTablaInstalacion()
        {
            int totalElementos = 0;
            double total = 0.00;
            tableInstalacion = new DataTable();
            DataRow row;

            tableInstalacion.Columns.AddRange(new DataColumn[]{
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Responsable", typeof(string)),
                new DataColumn("Unidad", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad Total", typeof(string)),
                new DataColumn("Mal Estado", typeof(string)),
                new DataColumn("Subtotal1", typeof(string)),
                new DataColumn("Subtotal2", typeof(string)),
            });

            string selectMaterialesInstalacion = @"select nombreMaterial,FORMAT(precio, 'C', 'en-us') as 'precio',unidadMedida,nombreResponsable, Piso, CantidadTotal, CantidadMalEstado, FORMAT(SubTotal, 'C','en-us') as 'SubTotal1', Subtotal from DetallesMaterialesMantenimiento inner join Materiales on 
                                            DetallesMaterialesMantenimiento.IdMaterial = Materiales.idMaterial inner join Responsables on Materiales.idResponsable = Responsables.idResponsable where IdMantenimiento =@IdMantenimiento and idCategoria = 1003";
            SqlCommand command2 = new SqlCommand(selectMaterialesInstalacion, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    totalElementos += Convert.ToInt32(dr2["CantidadTotal"].ToString());
                    totalMalEstado2 = totalMalEstado2 + Convert.ToInt32(dr2["CantidadMalEstado"].ToString());
                    total += Convert.ToDouble(dr2["SubTotal"].ToString());

                    tableInstalacion.Rows.Add(
                        dr2["nombreMaterial"].ToString(),
                        dr2["precio"].ToString(),
                        dr2["nombreResponsable"].ToString(),
                        dr2["unidadMedida"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["CantidadMalEstado"].ToString(),
                        dr2["SubTotal1"].ToString(),
                        dr2["Subtotal"].ToString()

                    );
                }
            }
            command2.Connection.Close();
            evaluacion2 = (1.00 - (Convert.ToDouble(totalMalEstado2) / Convert.ToDouble(totalElementos))) * 100.00;
            row = tableInstalacion.NewRow();
            row[0] = "Evaluacion " + evaluacion2 + "%";
            row[4] = "Totales";
            row[5] = totalElementos;
            row[6] = totalMalEstado2;
            row[7] = total.ToString("C2");
            tableInstalacion.Rows.Add(row);


            GVMaterialesInstalacion.DataSource = tableInstalacion;
            GVMaterialesInstalacion.DataBind();
        }

        private void LlenarTablaIlluminacion()
        {
            tableIlluminacion = new DataTable();
            DataRow row;
            int totalElementos = 0;
            double total = 0.00;
            tableIlluminacion.Columns.AddRange(new DataColumn[]{
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Responsable", typeof(string)),
                new DataColumn("Unidad", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad Total", typeof(string)),
                new DataColumn("Mal Estado", typeof(string)),
                new DataColumn("Subtotal1", typeof(string)),
                new DataColumn("Subtotal2", typeof(string))
            });

            string selectMaterialesIlluminacion = @"select nombreMaterial,FORMAT(precio, 'C', 'en-us') as 'precio',nombreResponsable,unidadMedida, Piso, CantidadTotal, CantidadMalEstado, FORMAT(SubTotal, 'C','en-us') as 'SubTotal1', Subtotal from DetallesMaterialesMantenimiento inner join Materiales on 
                                            DetallesMaterialesMantenimiento.IdMaterial = Materiales.idMaterial inner join Responsables on Materiales.idResponsable = Responsables.idResponsable where IdMantenimiento =@IdMantenimiento and idCategoria = 1";
            SqlCommand command = new SqlCommand(selectMaterialesIlluminacion, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    totalElementos = totalElementos + Convert.ToInt32(dr["CantidadTotal"].ToString());
                    totalMalEstado = totalMalEstado + Convert.ToInt32(dr["CantidadMalEstado"].ToString());
                    total += Convert.ToDouble(dr["Subtotal"].ToString());

                    tableIlluminacion.Rows.Add(
                        dr["nombreMaterial"].ToString(),
                        dr["precio"].ToString(),
                        dr["nombreResponsable"].ToString(),
                        dr["unidadMedida"].ToString(),
                        dr["Piso"].ToString(),
                        dr["CantidadTotal"].ToString(),
                        dr["CantidadMalEstado"].ToString(),
                        dr["SubTotal1"].ToString(),
                        dr["Subtotal"].ToString()

                    );
                }
            }
            command.Connection.Close();
            evaluacion = (1.00 - (Convert.ToDouble(totalMalEstado) / Convert.ToDouble(totalElementos))) * 100.00;
            row = tableIlluminacion.NewRow();
            row[0] = "Evaluacion " + evaluacion + "%";
            row[4] = "Totales";
            row[5] = totalElementos;
            row[6] = totalMalEstado;
            row[7] = total.ToString("C2");
            tableIlluminacion.Rows.Add(row);
            GVMaterialesIlluminacion.DataSource = tableIlluminacion;
            GVMaterialesIlluminacion.DataBind();

        }
        private void ObtenerDatos()
        {
            String query = @"select Format(FechaMantenimiento,'dd/MM/yyyy') as 'FechaMantenimiento',NombreEdificio,Direccion,Posesion,Ocupacion,Superficie,Uso,Niveles,Cajones,Antiguedad,Terreno from Mantenimientos inner join 
                                        Edificios on Mantenimientos.IdEdificio = Edificios.IdEdificio where IdMantenimiento = @IdMantenimiento";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtFecha.Text = "FECHA:" + reader["FechaMantenimiento"].ToString();
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

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            string select = @"select ROW_NUMBER() OVER(ORDER BY nombreMaterial ASC) as 'No', nombreMaterial,unidadMedida,CantidadTotal,CantidadMalEstado,FORMAT(precio, 'C', 'en-us') as 'precio', 
                                FORMAT(SubTotal, 'C','en-us') as 'SubTotal', Subtotal as 'Subtotal2' from DetallesMaterialesMantenimiento inner join Materiales on 
                                DetallesMaterialesMantenimiento.IdMaterial = Materiales.idMaterial inner join Responsables on Materiales.idResponsable = Responsables.idResponsable 
                                where IdMantenimiento =@id and idCategoria = 1 AND nombreResponsable='INEGI'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlconn);
            da.SelectCommand.Parameters.AddWithValue("@id", idMantenimiento);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rp = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rp);

            string select2 = @"select ROW_NUMBER() OVER(ORDER BY nombreMaterial ASC) as 'No', nombreMaterial,unidadMedida,CantidadTotal,CantidadMalEstado,FORMAT(precio, 'C', 'en-us') as 'precio', 
                                FORMAT(SubTotal, 'C','en-us') as 'SubTotal',Subtotal as 'Subtotal2' from DetallesMaterialesMantenimiento inner join Materiales on 
                                DetallesMaterialesMantenimiento.IdMaterial = Materiales.idMaterial inner join Responsables on Materiales.idResponsable = Responsables.idResponsable 
                                where IdMantenimiento =@id and idCategoria = 1 AND nombreResponsable='Contrato'";
            SqlDataAdapter da2 = new SqlDataAdapter(select2, sqlconn);
            da2.SelectCommand.Parameters.AddWithValue("@id", idMantenimiento);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            ReportDataSource rp2 = new ReportDataSource("DataSet2", dt2);
            ReportViewer1.LocalReport.DataSources.Add(rp2);
            string totalevaluacion = Convert.ToString((evaluacion + evaluacion2) / 2.0) + "%";
            mpeImprimir.Show();
            ReportParameter[] reportParameters = new ReportParameter[4];
            reportParameters[0] = new ReportParameter("Inmueble", txtnombreInmueble.Text.ToUpper());
            reportParameters[1] = new ReportParameter("Direccion", txtDireccion.Text.ToUpper());
            reportParameters[2] = new ReportParameter("Fecha", txtFecha.Text.ToUpper());
            reportParameters[3] = new ReportParameter("Evaluacion", totalevaluacion);
            ReportViewer1.LocalReport.SetParameters(reportParameters);
            ReportViewer1.LocalReport.Refresh();
        }


        protected void GenerarReporte_Click(object sender, EventArgs e)
        {
            string select = @"select ROW_NUMBER() OVER(ORDER BY nombreMaterial ASC) as 'No', nombreMaterial,unidadMedida,CantidadTotal,CantidadMalEstado,FORMAT(precio, 'C', 'en-us') as 'precio', 
                                FORMAT(SubTotal, 'C','en-us') as 'SubTotal', Subtotal as 'Subtotal2' from DetallesMaterialesMantenimiento inner join Materiales on 
                                DetallesMaterialesMantenimiento.IdMaterial = Materiales.idMaterial inner join Responsables on Materiales.idResponsable = Responsables.idResponsable 
                                where IdMantenimiento =@id and idCategoria = 1003 AND nombreResponsable='INEGI'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlconn);
            da.SelectCommand.Parameters.AddWithValue("@id", idMantenimiento);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportDataSource rp = new ReportDataSource("DataSet1", dt);
            ReportViewer2.LocalReport.DataSources.Add(rp);

            string select2 = @"select ROW_NUMBER() OVER(ORDER BY nombreMaterial ASC) as 'No', nombreMaterial,unidadMedida,CantidadTotal,CantidadMalEstado,FORMAT(precio, 'C', 'en-us') as 'precio', 
                                FORMAT(SubTotal, 'C','en-us') as 'SubTotal',Subtotal as 'Subtotal2' from DetallesMaterialesMantenimiento inner join Materiales on 
                                DetallesMaterialesMantenimiento.IdMaterial = Materiales.idMaterial inner join Responsables on Materiales.idResponsable = Responsables.idResponsable 
                                where IdMantenimiento =@id and idCategoria = 1003 AND nombreResponsable='Contrato'";
            SqlDataAdapter da2 = new SqlDataAdapter(select2, sqlconn);
            da2.SelectCommand.Parameters.AddWithValue("@id", idMantenimiento);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            ReportDataSource rp2 = new ReportDataSource("DataSet2", dt2);
            ReportViewer2.LocalReport.DataSources.Add(rp2);
            string totalevaluacion = Convert.ToString((evaluacion + evaluacion2) / 2.0) + "%";
            mpeImprimir2.Show();
            ReportParameter[] reportParameters = new ReportParameter[4];
            reportParameters[0] = new ReportParameter("Inmueble", txtnombreInmueble.Text.ToUpper());
            reportParameters[1] = new ReportParameter("Direccion", txtDireccion.Text.ToUpper());
            reportParameters[2] = new ReportParameter("Fecha", txtFecha.Text.ToUpper());
            reportParameters[3] = new ReportParameter("Evaluacion", totalevaluacion);
            ReportViewer2.LocalReport.SetParameters(reportParameters);
            ReportViewer2.LocalReport.Refresh();
        }
    }
}