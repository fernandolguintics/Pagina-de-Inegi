using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;

namespace InegiWeb
{
    public partial class MantenimientoAlmacenConsumo : System.Web.UI.Page
    {
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static DataTable tbConsumo;
        string idEdificio = Menu.idInmueble;
        public static double promedioConsumo;
        public static string fecha;
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
            DataTable tableConsumo = new DataTable();
            tableConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConsumo", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
            });

            string querySelect = "Select * from CriteriosConsumo";

            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableConsumo.Rows.Add(
                        dr["IdConsumo"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVConsumo.DataSource = tableConsumo;
            GVConsumo.DataBind();
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
            tbConsumo = new DataTable();
            tbConsumo.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdConsumo", typeof(string)),
                new DataColumn("Cumple", typeof(string)),
                new DataColumn("NoCumple", typeof(string)),
                new DataColumn("Observacion", typeof(string)),
                new DataColumn("Evidencia", typeof(byte[])),
                 new DataColumn("Estado", typeof(string))
            });

            foreach (GridViewRow gvr in GVConsumo.Rows)
            {
                rowTable = tbConsumo.NewRow();
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
                tbConsumo.Rows.Add(rowTable);
                i++;
            }

            promedioConsumo = (Convert.ToDouble(criteriosCumplen) * 10.0) / Convert.ToDouble(i);
            fecha = txtFecha.Text;
            Response.Redirect("MantenimientoAlmacenInventario.aspx", false);
        }
    }
}