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
    public partial class ReporteInfraestructura : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        string idMantenimiento = Seguimiento.idMantenimiento;
        public static DataTable tableIlluminacion;
        public static DataTable tableInstalacion;
        public static int totalArregladosInstalacion = 0, totalArregladosIlluminacion = 0;
        public static double grandTotalInstalacion = 0.0, grandTotalIlluminacion = 0.0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaIlluminacion();
                LLenarTablaInstalacion();

            }
        }
        private void LlenarTablaIlluminacion()
        {
            tableIlluminacion = new DataTable();
            tableIlluminacion.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdMaterial", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("CantidadTotal", typeof(string)),
                new DataColumn("malEstadoIlluminacion", typeof(string)),

            });
            SqlConnection sqlconn = new SqlConnection(conectar);
            string getMateriales = "Select DetallesMaterialesMantenimiento.idMaterial,nombreMaterial, precio, Piso, CantidadTotal, CantidadMalEstado from DetallesMaterialesMantenimiento inner join Materiales on DetallesMaterialesMantenimiento.IdMaterial = Materiales.idMaterial where IdMantenimiento= @IdMantenimiento and idCategoria =1";
            SqlCommand command = new SqlCommand(getMateriales, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableIlluminacion.Rows.Add(
                        dr["idMaterial"].ToString(),
                        dr["nombreMaterial"].ToString(),
                        dr["precio"].ToString(),
                        dr["Piso"].ToString(),
                        dr["CantidadTotal"].ToString(),
                        dr["CantidadMalEstado"].ToString()
                    );
                }
            }
            command.Connection.Close();

            GVMaterialesIlluminacion.DataSource = tableIlluminacion;
            GVMaterialesIlluminacion.DataBind();
        }

        private void ObtenerDatos()
        {

            String query = "select NombreEdificio,Direccion,Posesion,Ocupacion,Superficie,Uso,Niveles,Cajones,Antiguedad,Terreno from Edificios inner join Mantenimientos ON Edificios.IdEdificio = Mantenimientos.IdEdificio where IdMantenimiento = @IdMantenimiento";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
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
        private void LLenarTablaInstalacion()
        {

            tableInstalacion = new DataTable();

            tableInstalacion.Columns.AddRange(new DataColumn[]{

                new DataColumn("IdMaterial", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("CantidadTotal", typeof(string)),
                new DataColumn("malEstadoInstalacion", typeof(string)),
                new DataColumn("Subtotal", typeof(string))

            });
            SqlConnection sqlconn = new SqlConnection(conectar);
            string getMateriales = "Select DetallesMaterialesMantenimiento.idMaterial, nombreMaterial, precio, Piso, CantidadTotal, CantidadMalEstado, SubTotal from DetallesMaterialesMantenimiento inner join Materiales on DetallesMaterialesMantenimiento.IdMaterial = Materiales.IdMaterial where IdMantenimiento= @IdMantenimiento and idCategoria =1003";
            SqlCommand command = new SqlCommand(getMateriales, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableInstalacion.Rows.Add(
                        dr["idMaterial"].ToString(),
                        dr["nombreMaterial"].ToString(),
                        dr["precio"].ToString(),
                        dr["Piso"].ToString(),
                        dr["CantidadTotal"].ToString(),
                        dr["CantidadMalEstado"].ToString(),
                        dr["SubTotal"].ToString()
                    );
                }
            }
            command.Connection.Close();

            GVMaterialesInstalacion.DataSource = tableInstalacion;
            GVMaterialesInstalacion.DataBind();
        }

        protected void GuardarDatos_Click(object sender, EventArgs e)
        {
            DataTable tableInstalacion2 = new DataTable();
            DataTable tableIlluminacion2 = new DataTable();
            DataRow rowTable;



            tableInstalacion2.Columns.AddRange(new DataColumn[]{
               new DataColumn("IdMaterial", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("CantidadTotal", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("Subtotal", typeof(string))
            });

            tableIlluminacion2.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdMaterial", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("CantidadTotal", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("Arreglados", typeof(string)),
                new DataColumn("Subtotal", typeof(string))
            });

            foreach (GridViewRow gvr in GVMaterialesIlluminacion.Rows)
            {
                var arreglados = ((TextBox)gvr.FindControl("arreglosIlluminacion")).Text;
                if (arreglados.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar espacios en blanco en arreglados');", true);
                    return;
                }
                else
                {
                    totalArregladosIlluminacion = totalArregladosIlluminacion + Convert.ToInt32(arreglados);
                    double subtotal = Convert.ToDouble(gvr.Cells[2].Text) * Convert.ToDouble(arreglados);
                    grandTotalIlluminacion = grandTotalIlluminacion + subtotal;
                    rowTable = tableIlluminacion2.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = gvr.Cells[1].Text;
                    rowTable[2] = gvr.Cells[2].Text;
                    rowTable[3] = gvr.Cells[3].Text;
                    rowTable[4] = gvr.Cells[4].Text;
                    rowTable[5] = gvr.Cells[5].Text;
                    rowTable[6] = arreglados;
                    rowTable[7] = subtotal;
                    tableIlluminacion2.Rows.Add(rowTable);
                }

            }
            rowTable = tableIlluminacion2.NewRow();
            rowTable[6] = totalArregladosIlluminacion;
            rowTable[7] = grandTotalIlluminacion;
            tableIlluminacion2.Rows.Add(rowTable);

            foreach (GridViewRow gvr in GVMaterialesInstalacion.Rows)
            {
                var arreglados = ((TextBox)gvr.FindControl("arreglosInstalacion")).Text;
                if (arreglados.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar espacios en blanco en arreglados');", true);
                    return;
                }
                else
                {
                    totalArregladosInstalacion = totalArregladosInstalacion + Convert.ToInt32(arreglados);
                    double subtotal = Convert.ToDouble(gvr.Cells[2].Text) * Convert.ToDouble(arreglados);
                    grandTotalInstalacion = grandTotalInstalacion + subtotal;
                    rowTable = tableInstalacion2.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = gvr.Cells[1].Text;
                    rowTable[2] = gvr.Cells[2].Text;
                    rowTable[3] = gvr.Cells[3].Text;
                    rowTable[4] = gvr.Cells[4].Text;
                    rowTable[5] = gvr.Cells[5].Text;
                    rowTable[6] = arreglados;
                    rowTable[7] = subtotal;
                    tableInstalacion2.Rows.Add(rowTable);
                }

            }

            rowTable = tableInstalacion2.NewRow();
            rowTable[6] = totalArregladosInstalacion;
            rowTable[7] = grandTotalInstalacion;
            tableInstalacion2.Rows.Add(rowTable);

            tableInstalacion = tableInstalacion2.Copy();
            tableIlluminacion = tableIlluminacion2.Copy();

            Response.Redirect("GuardarReporte.aspx", false);
        }
    }
}