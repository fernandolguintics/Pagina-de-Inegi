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
    public partial class MantenimientoInfraestructura : System.Web.UI.Page
    {
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static DataTable tableInstalacion = new DataTable();//Tabla que se llena cuando el usuario ingresa todos los datos, se activa con la funcion guardar
        public static DataTable tableIlluminacion = new DataTable();
        public static double grandTotalInstalacion = 0, grandTotalIlluminacion = 0;
        public static int totalIlluminacionElementos = 0, totalInstalacionElementos = 0;
        public static int totalMalEstadoInstalacion = 0, totalMalEstadoIlluminacion = 0;
        public static double evaluacionInstalacion, evaluacionIlluminacion;
        String idEdificio;
        public static string fechaEvaluacion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaMateriales();
                LlenarTablaMaterialesInstalacion();
            }
        }
        private void LlenarTablaMateriales()
        {
            DataTable tableMatIlluminacion = new DataTable();
            tableMatIlluminacion.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdMaterial", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Unidad", typeof(string)),
                new DataColumn("Responsable", typeof(string)),
                new DataColumn("Precio", typeof(string)),

            });

            string querySelect = @"Select idMaterial,nombreMaterial,precio,unidadMedida, nombreResponsable from Materiales inner join 
            Responsables on Materiales.idResponsable = Responsables.idResponsable where idCategoria= 1";
            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableMatIlluminacion.Rows.Add(
                        dr["idMaterial"].ToString(),
                        dr["nombreMaterial"].ToString(),
                        dr["unidadMedida"].ToString(),
                        dr["nombreResponsable"].ToString(),
                        dr["precio"].ToString()

                    );
                }
            }
            commandSelect.Connection.Close();
            GVMaterialIlluminacion.DataSource = tableMatIlluminacion;
            GVMaterialIlluminacion.DataBind();


        }
        private void LlenarTablaMaterialesInstalacion()
        {
            DataTable tableMatInstalacion = new DataTable();
            tableMatInstalacion.Columns.AddRange(new DataColumn[]{

                new DataColumn("IdMaterial", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Unidad", typeof(string)),
                new DataColumn("Responsable", typeof(string)),
                new DataColumn("Precio", typeof(string)),

            });
            SqlConnection sqlconn = new SqlConnection(conectar);
            string querySelect = @"Select idMaterial, nombreMaterial,precio,unidadMedida, nombreResponsable from Materiales inner join 
            Responsables on Materiales.idResponsable = Responsables.idResponsable where idCategoria= 1003";
            SqlCommand command = new SqlCommand(querySelect, sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableMatInstalacion.Rows.Add(
                        dr["idMaterial"].ToString(),
                        dr["nombreMaterial"].ToString(),
                        dr["unidadMedida"].ToString(),
                        dr["nombreResponsable"].ToString(),
                        dr["precio"].ToString()

                    );
                }
            }
            command.Connection.Close();

            GVMaterialesInstalacion.DataSource = tableMatInstalacion;
            GVMaterialesInstalacion.DataBind();


        }
        public void ObtenerDatos()
        {
            idEdificio = Menu.idInmueble;
            String query = "Select * from Edificios where IdEdificio = @idEdificio";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@idEdificio", idEdificio);
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



        protected void GuardarDatos_Click(object sender, EventArgs e)
        {
            DataTable tableInstalacion2 = new DataTable();
            DataTable tableIlluminacion2 = new DataTable();
            DataRow rowTable;


            fechaEvaluacion = txtFecha.Text;
            tableInstalacion2.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdMaterial", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Unidad", typeof(string)),
                new DataColumn("Responsable", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad Total", typeof(string)),
                new DataColumn("Mal Estado", typeof(string)),
                new DataColumn("Subtotal", typeof(string))
            });

            tableIlluminacion2.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdMaterial", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Unidad", typeof(string)),
                new DataColumn("Responsable", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad Total", typeof(string)),
                new DataColumn("Mal Estado", typeof(string)),
                new DataColumn("Subtotal", typeof(string))
            });


            foreach (GridViewRow gvr in GVMaterialIlluminacion.Rows)
            {

                var piso = ((TextBox)gvr.FindControl("pisoIlluminacion")).Text;
                var totalIlluminacion = ((TextBox)gvr.FindControl("totalIlluminacion")).Text;
                var txt = ((TextBox)gvr.FindControl("malEstadoIlluminacion")).Text;
                if (txt.Equals("") && totalIlluminacion.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar cantidas y mal estado vacio');", true);
                    return;
                }
                else
                {
                    totalIlluminacionElementos = totalIlluminacionElementos + Convert.ToInt32(totalIlluminacion);
                    totalMalEstadoIlluminacion = totalMalEstadoIlluminacion + Convert.ToInt32(txt);
                    double malEstado = Convert.ToDouble(txt);
                    double suma = Convert.ToDouble(gvr.Cells[4].Text) * malEstado;
                    grandTotalIlluminacion = grandTotalIlluminacion + suma;
                    rowTable = tableIlluminacion2.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = HttpUtility.HtmlDecode(gvr.Cells[1].Text);
                    rowTable[2] = gvr.Cells[2].Text;
                    rowTable[3] = gvr.Cells[3].Text;
                    rowTable[4] = gvr.Cells[4].Text;
                    rowTable[5] = piso;
                    rowTable[6] = totalIlluminacion;
                    rowTable[7] = malEstado;
                    rowTable[8] = suma;
                    tableIlluminacion2.Rows.Add(rowTable);
                }

            }
            evaluacionIlluminacion = (1.00 - (Convert.ToDouble(totalMalEstadoIlluminacion) / Convert.ToDouble(totalIlluminacionElementos))) * 100.00;
            rowTable = tableIlluminacion2.NewRow();
            rowTable[1] = "Evaluacion";
            rowTable[2] = evaluacionIlluminacion + "%";
            rowTable[5] = "Totales";
            rowTable[6] = totalIlluminacionElementos;
            rowTable[7] = totalMalEstadoIlluminacion;
            rowTable[8] = grandTotalIlluminacion;
            tableIlluminacion2.Rows.Add(rowTable);



            foreach (GridViewRow gvr in GVMaterialesInstalacion.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("pisoInstalacion")).Text;

                var totalInstalacion = ((TextBox)gvr.FindControl("totalInstalacion")).Text;
                var txt = ((TextBox)gvr.FindControl("malEstadoInstalacion")).Text;
                if (txt.Equals("") && totalInstalacion.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar cantidas y mal estado vacio');", true);
                    return;
                }
                else
                {
                    totalInstalacionElementos = totalInstalacionElementos + Convert.ToInt32(totalInstalacion);

                    totalMalEstadoInstalacion = totalMalEstadoInstalacion + Convert.ToInt32(txt);
                    double malEstado = Convert.ToDouble(txt);
                    double suma = Convert.ToDouble(gvr.Cells[4].Text) * malEstado;
                    grandTotalInstalacion = grandTotalInstalacion + suma;
                    rowTable = tableInstalacion2.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = HttpUtility.HtmlDecode(gvr.Cells[1].Text);
                    rowTable[2] = gvr.Cells[2].Text;
                    rowTable[3] = gvr.Cells[3].Text;
                    rowTable[4] = gvr.Cells[4].Text;
                    rowTable[5] = piso;
                    rowTable[6] = totalInstalacion;
                    rowTable[7] = malEstado;
                    rowTable[8] = suma;
                    tableInstalacion2.Rows.Add(rowTable);
                }


            }
            evaluacionInstalacion = (1.00 - (Convert.ToDouble(totalMalEstadoInstalacion) / Convert.ToDouble(totalInstalacionElementos))) * 100.00;
            rowTable = tableInstalacion2.NewRow();
            rowTable[1] = "Evaluacion";
            rowTable[2] = evaluacionInstalacion + "%";
            rowTable[5] = "Totales";
            rowTable[6] = totalInstalacionElementos;
            rowTable[7] = totalMalEstadoInstalacion;
            rowTable[8] = grandTotalInstalacion;
            tableInstalacion2.Rows.Add(rowTable);


            tableIlluminacion = tableIlluminacion2.Copy();
            tableInstalacion = tableInstalacion2.Copy();

            Response.Redirect("GuardarMantenimiento.aspx");
        }
    }
}