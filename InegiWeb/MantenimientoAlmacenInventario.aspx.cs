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
    public partial class MantenimientoAlmacenInventario : System.Web.UI.Page
    {
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static DataTable tbInventarios;
        string idEdificio = Menu.idInmueble;
        public static double promedioInventario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaConsumo();
            }
        }
        private void LlenarTablaConsumo()
        {
            DataTable tableInventario = new DataTable();
            tableInventario.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdInventario", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
            });

            string querySelect = "Select * from CriteriosInventario";

            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableInventario.Rows.Add(
                        dr["IdInventario"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVInventario.DataSource = tableInventario;
            GVInventario.DataBind();
        }

        public void ObtenerDatos()
        {

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

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            int criteriosCumplen = 0;
            int i = 0;
            int estado = 0;

            DataRow rowTable;
            tbInventarios = new DataTable();
            tbInventarios.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdInventario", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Evidencia", typeof(byte[])),
                new DataColumn("Estado", typeof(string))
            });

            foreach (GridViewRow gvr in GVInventario.Rows)
            {
                rowTable = tbInventarios.NewRow();
                rowTable[0] = gvr.Cells[0].Text;
                if (((RadioButton)gvr.FindControl("rbCumple")).Checked)
                {
                    rowTable[1] = 1;
                    rowTable[2] = 0;
                    criteriosCumplen++;
                }
                else
                {
                    rowTable[1] = 0;
                    rowTable[2] = 1;
                }
                rowTable[3] = ((TextBox)gvr.FindControl("observacion")).Text;
                rowTable[4] = ((FileUpload)gvr.FindControl("evidencia")).FileBytes;
                rowTable[5] = estado;
                tbInventarios.Rows.Add(rowTable);
                i++;
            }

            promedioInventario = (Convert.ToDouble(criteriosCumplen) * 10.0) / Convert.ToDouble(i);

            Response.Redirect("MantenimientoAlmacenConcentracion.aspx", false);
        }
    }
}