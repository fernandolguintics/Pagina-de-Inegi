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
    public partial class MantenimientoServicios : System.Web.UI.Page
    {
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        string idEdificio = Menu.idInmueble;
        public static string fechaEvaluacion;
        static string idMantenimiento;
        static double costoTotal = 0.0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
                LlenarTablaMaterialesCarpinteria();
                LlenarTablaMaterialesHerreria();
                LLenarTablaPintura();
                LlenarTablaHidrosanitarias();
                LlenarTablaPisosyPlafones();
                LlenarTablaOtros();
                LLenarCblMateriales();
            }
        }
        private void LLenarCblMateriales()
        {
            DataTable dt = new DataTable();
            //string selectMateriales = "select IdMaterialesServicio, CONCAT(NombreMaterial,'',UnidadMedida,' $', CONVERT(varchar(100),Precio)) as 'NombreMaterial' from MaterialesServicio";
            string selectMateriales = "select IdMaterialesServicio, CONCAT(NombreMaterial,'',Descripcion,'',UnidadMedida) as 'NombreMaterial' from MaterialesServicio";
            SqlDataAdapter sda = new SqlDataAdapter(selectMateriales, sqlconn);

            sda.Fill(dt);

            cblMateriales.DataSource = dt;
            cblMateriales.DataTextField = "NombreMaterial";
            cblMateriales.DataValueField = "IdMaterialesServicio";
            cblMateriales.DataBind();


        }

        private void LlenarTablaOtros()
        {
            DataTable tableOtros = new DataTable();
            tableOtros.Columns.AddRange(new DataColumn[]{
                new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
            });

            string querySelect = "Select * from CriteriosServicio where Categoria='Otros'";
            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableOtros.Rows.Add(
                         dr["IdCriterioServicio"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVOtros.DataSource = tableOtros;
            GVOtros.DataBind();
        }

        private void LlenarTablaPisosyPlafones()
        {
            DataTable tablePisos = new DataTable();
            tablePisos.Columns.AddRange(new DataColumn[]{
                  new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),

            });

            string querySelect = "Select * from CriteriosServicio where Categoria='Piso,Plafones y Techo'";
            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tablePisos.Rows.Add(
                        dr["IdCriterioServicio"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVPisos.DataSource = tablePisos;
            GVPisos.DataBind();
        }

        private void LlenarTablaHidrosanitarias()
        {
            DataTable tableHidro = new DataTable();
            tableHidro.Columns.AddRange(new DataColumn[]{
                  new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),

            });

            string querySelect = "Select * from CriteriosServicio where Categoria='Instalaciones Hidrosanitarias'";
            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableHidro.Rows.Add(
                        dr["IdCriterioServicio"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVHidrosanitarias.DataSource = tableHidro;
            GVHidrosanitarias.DataBind();
        }

        private void LLenarTablaPintura()
        {
            DataTable tablePintura = new DataTable();
            tablePintura.Columns.AddRange(new DataColumn[]{
               new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),

            });

            string querySelect = "Select * from CriteriosServicio where Categoria='Pintura'";
            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tablePintura.Rows.Add(
                        dr["IdCriterioServicio"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVPintura.DataSource = tablePintura;
            GVPintura.DataBind();
        }

        private void LlenarTablaMaterialesHerreria()
        {
            DataTable tableHerr = new DataTable();
            tableHerr.Columns.AddRange(new DataColumn[]{
                 new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),

            });

            string querySelect = "Select * from CriteriosServicio where Categoria='Herrería'";
            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableHerr.Rows.Add(
                          dr["IdCriterioServicio"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVHerreria.DataSource = tableHerr;
            GVHerreria.DataBind();
        }

        private void LlenarTablaMaterialesCarpinteria()
        {
            DataTable tableCarp = new DataTable();
            tableCarp.Columns.AddRange(new DataColumn[]{
                 new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Criterio", typeof(string)),

            });

            string querySelect = "Select * from CriteriosServicio where Categoria='Carpintería y Cristales'";
            SqlCommand commandSelect = new SqlCommand(querySelect, sqlconn);
            commandSelect.Connection.Open();
            SqlDataReader dr = commandSelect.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableCarp.Rows.Add(
                       dr["IdCriterioServicio"].ToString(),
                        dr["Criterio"].ToString()
                    );
                }
            }
            commandSelect.Connection.Close();
            GVCarpinteria.DataSource = tableCarp;
            GVCarpinteria.DataBind();
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            costoTotal = 0.0;
            GuardarListaMateriales();
            mpeMostrar.Show();


        }

        private void GuardarListaMateriales()
        {
            DataTable tb = new DataTable();
            tb.Columns.AddRange(new DataColumn[]{
               new DataColumn("Id", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Precio", typeof(string)),

            });

            DataRow rowTable;

            for (int i = 0; i < cblMateriales.Items.Count; i++)
            {
                if (cblMateriales.Items[i].Selected == true)
                {
                    string precio = "select Precio from MaterialesServicio where IdMaterialesServicio=@id";
                    SqlCommand comm = new SqlCommand(precio, sqlconn);
                    comm.Connection.Open();
                    comm.Parameters.AddWithValue("@id", cblMateriales.Items[i].Value.ToString());
                    SqlDataReader dr = comm.ExecuteReader();
                    if (dr.Read())
                    {
                        rowTable = tb.NewRow();
                        rowTable[0] = cblMateriales.Items[i].Value.ToString();
                        rowTable[1] = cblMateriales.Items[i].Text;
                        rowTable[2] = dr["Precio"].ToString();
                        tb.Rows.Add(rowTable);
                    }
                    comm.Connection.Close();

                }
            }

            GVMateriales.DataSource = tb;
            GVMateriales.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            LlenarTablaTotalCompra();
            mpeTotal.Show();

        }

        private void LlenarTablaTotalCompra()
        {

            DataTable tb = new DataTable();
            tb.Columns.AddRange(new DataColumn[]{
               new DataColumn("Id", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("Subtotal", typeof(string)),

            });

            DataRow rowTable;
            foreach (GridViewRow gvr in GVMateriales.Rows)
            {
                var cantidad = ((TextBox)gvr.FindControl("txbCantidad")).Text;
                string precio = gvr.Cells[2].Text;
                double subtotal = Convert.ToDouble(precio) * Convert.ToDouble(cantidad);
                costoTotal = costoTotal + subtotal;
                rowTable = tb.NewRow();
                rowTable[0] = gvr.Cells[0].Text;
                rowTable[1] = HttpUtility.HtmlDecode(gvr.Cells[1].Text);
                rowTable[2] = cantidad;
                rowTable[3] = subtotal;
                tb.Rows.Add(rowTable);
            }
            txtTotal.Text = "Total:" + costoTotal.ToString();
            GVTotal.DataSource = tb;
            GVTotal.DataBind();
        }

        protected void btnGuardarMantenimiento_Click(object sender, EventArgs e)
        {
            string IdUsuario = Convert.ToString(Session["IdUsuario"]);
            int cantidadTotal = 0;
            int malestadoTotal = 0;
            DataRow rowTable;
            DataTable tbCarpinteria = new DataTable();
            DataTable tbHerreria = new DataTable();
            DataTable tbPintura = new DataTable();
            DataTable tbHidro = new DataTable();
            DataTable tbPisos = new DataTable();
            DataTable tbOtros = new DataTable();

            tbCarpinteria.Columns.AddRange(new DataColumn[]{
               new DataColumn("IdCriterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            tbHerreria = tbCarpinteria.Clone();
            tbPintura = tbCarpinteria.Clone();
            tbHidro = tbCarpinteria.Clone();
            tbPisos = tbCarpinteria.Clone();
            tbOtros = tbCarpinteria.Clone();

            foreach (GridViewRow gvr in GVCarpinteria.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var cantidad = ((TextBox)gvr.FindControl("cantidadTotal")).Text;
                var malEstado = ((TextBox)gvr.FindControl("malEstado")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (cantidad.Equals("") && malEstado.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar cantidas y mal estado vacio');", true);
                    return;
                }
                else
                {
                    cantidadTotal += Convert.ToInt32(cantidad);
                    malestadoTotal += Convert.ToInt32(malEstado);
                    rowTable = tbCarpinteria.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = cantidad;
                    rowTable[3] = malEstado;
                    rowTable[4] = normativa;
                    rowTable[5] = observacion;
                    tbCarpinteria.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVHerreria.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var cantidad = ((TextBox)gvr.FindControl("cantidadTotal")).Text;
                var malEstado = ((TextBox)gvr.FindControl("malEstado")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (cantidad.Equals("") && malEstado.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar cantidas y mal estado vacio');", true);
                    return;
                }
                else
                {
                    cantidadTotal += Convert.ToInt32(cantidad);
                    malestadoTotal += Convert.ToInt32(malEstado);
                    rowTable = tbHerreria.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = cantidad;
                    rowTable[3] = malEstado;
                    rowTable[4] = normativa;
                    rowTable[5] = observacion;
                    tbHerreria.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVPintura.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var cantidad = ((TextBox)gvr.FindControl("cantidadTotal")).Text;
                var malEstado = ((TextBox)gvr.FindControl("malEstado")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (cantidad.Equals("") && malEstado.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar cantidas y mal estado vacio');", true);
                    return;
                }
                else
                {
                    cantidadTotal += Convert.ToInt32(cantidad);
                    malestadoTotal += Convert.ToInt32(malEstado);
                    rowTable = tbPintura.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = cantidad;
                    rowTable[3] = malEstado;
                    rowTable[4] = normativa;
                    rowTable[5] = observacion;
                    tbPintura.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVHidrosanitarias.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var cantidad = ((TextBox)gvr.FindControl("cantidadTotal")).Text;
                var malEstado = ((TextBox)gvr.FindControl("malEstado")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (cantidad.Equals("") && malEstado.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar cantidas y mal estado vacio');", true);
                    return;
                }
                else
                {
                    cantidadTotal += Convert.ToInt32(cantidad);
                    malestadoTotal += Convert.ToInt32(malEstado);
                    rowTable = tbHidro.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = cantidad;
                    rowTable[3] = malEstado;
                    rowTable[4] = normativa;
                    rowTable[5] = observacion;
                    tbHidro.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVPisos.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var cantidad = ((TextBox)gvr.FindControl("cantidadTotal")).Text;
                var malEstado = ((TextBox)gvr.FindControl("malEstado")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (cantidad.Equals("") && malEstado.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar cantidas y mal estado vacio');", true);
                    return;
                }
                else
                {
                    cantidadTotal += Convert.ToInt32(cantidad);
                    malestadoTotal += Convert.ToInt32(malEstado);
                    rowTable = tbPisos.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = cantidad;
                    rowTable[3] = malEstado;
                    rowTable[4] = normativa;
                    rowTable[5] = observacion;
                    tbPisos.Rows.Add(rowTable);
                }

            }

            foreach (GridViewRow gvr in GVOtros.Rows)
            {
                var piso = ((TextBox)gvr.FindControl("piso")).Text;
                var cantidad = ((TextBox)gvr.FindControl("cantidadTotal")).Text;
                var malEstado = ((TextBox)gvr.FindControl("malEstado")).Text;
                var normativa = ((TextBox)gvr.FindControl("cambioNormativa")).Text;
                var observacion = ((TextBox)gvr.FindControl("observaciones")).Text;
                if (cantidad.Equals("") && malEstado.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se puede dejar cantidas y mal estado vacio');", true);
                    return;
                }
                else
                {
                    cantidadTotal += Convert.ToInt32(cantidad);
                    malestadoTotal += Convert.ToInt32(malEstado);
                    rowTable = tbOtros.NewRow();
                    rowTable[0] = gvr.Cells[0].Text;
                    rowTable[1] = piso;
                    rowTable[2] = cantidad;
                    rowTable[3] = malEstado;
                    rowTable[4] = normativa;
                    rowTable[5] = observacion;
                    tbOtros.Rows.Add(rowTable);
                }

            }

            double evaluacion = (1.00 - (Convert.ToDouble(malestadoTotal) / Convert.ToDouble(cantidadTotal))) * 100.00;
            string insertMantenimiento = "insert MantenimientoServicios(EvaluacionFinal,CostoTotal,IdUsuario,IdEdificio,Fecha)values(@evaluacion,@costo,@idusuario,@idedificio,@fecha)";
            SqlCommand comm = new SqlCommand(insertMantenimiento, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@evaluacion", evaluacion);
            comm.Parameters.AddWithValue("@costo", costoTotal);
            comm.Parameters.AddWithValue("@idusuario", IdUsuario);
            comm.Parameters.AddWithValue("@idedificio", idEdificio);
            comm.Parameters.AddWithValue("@fecha", txtFecha.Text);
            comm.ExecuteNonQuery();
            comm.Connection.Close();


            string selectMant = "select Max(IdMantenimientoServicio) as 'Id' from MantenimientoServicios";
            SqlCommand comm2 = new SqlCommand(selectMant, sqlconn);
            comm2.Connection.Open();
            SqlDataReader dr = comm2.ExecuteReader();
            if (dr.Read())
            {
                idMantenimiento = dr["Id"].ToString();
                comm2.Connection.Close();
            }
            string insert = @"insert DetallesMantenimientoServicio(Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion,IdMantenimiento,IdCriterio)
                            values (@piso,@cantidad,@malestado,@cambio,@observacion,@idmantenimiento,@idcriterio)";

            for (int i = 0; i < tbCarpinteria.Rows.Count; i++)
            {

                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@piso", tbCarpinteria.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@cantidad", tbCarpinteria.Rows[i]["Cantidad"].ToString());
                comm3.Parameters.AddWithValue("@malestado", tbCarpinteria.Rows[i]["MalEstado"].ToString());
                comm3.Parameters.AddWithValue("@cambio", tbCarpinteria.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbCarpinteria.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idmantenimiento", idMantenimiento);
                comm3.Parameters.AddWithValue("@idCriterio", tbCarpinteria.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }
            for (int i = 0; i < tbHerreria.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@piso", tbHerreria.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@cantidad", tbHerreria.Rows[i]["Cantidad"].ToString());
                comm3.Parameters.AddWithValue("@malestado", tbHerreria.Rows[i]["MalEstado"].ToString());
                comm3.Parameters.AddWithValue("@cambio", tbHerreria.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbHerreria.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idmantenimiento", idMantenimiento);
                comm3.Parameters.AddWithValue("@idCriterio", tbHerreria.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }
            for (int i = 0; i < tbPintura.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@piso", tbPintura.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@cantidad", tbPintura.Rows[i]["Cantidad"].ToString());
                comm3.Parameters.AddWithValue("@malestado", tbPintura.Rows[i]["MalEstado"].ToString());
                comm3.Parameters.AddWithValue("@cambio", tbPintura.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbPintura.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idmantenimiento", idMantenimiento);
                comm3.Parameters.AddWithValue("@idCriterio", tbPintura.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }

            for (int i = 0; i < tbHidro.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@piso", tbHidro.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@cantidad", tbHidro.Rows[i]["Cantidad"].ToString());
                comm3.Parameters.AddWithValue("@malestado", tbHidro.Rows[i]["MalEstado"].ToString());
                comm3.Parameters.AddWithValue("@cambio", tbHidro.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbHidro.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idmantenimiento", idMantenimiento);
                comm3.Parameters.AddWithValue("@idCriterio", tbHidro.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }

            for (int i = 0; i < tbPisos.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@piso", tbPisos.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@cantidad", tbPisos.Rows[i]["Cantidad"].ToString());
                comm3.Parameters.AddWithValue("@malestado", tbPisos.Rows[i]["MalEstado"].ToString());
                comm3.Parameters.AddWithValue("@cambio", tbPisos.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbPisos.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idmantenimiento", idMantenimiento);
                comm3.Parameters.AddWithValue("@idCriterio", tbPisos.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }

            for (int i = 0; i < tbOtros.Rows.Count; i++)
            {
                SqlCommand comm3 = new SqlCommand(insert, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@piso", tbOtros.Rows[i]["Piso"].ToString());
                comm3.Parameters.AddWithValue("@cantidad", tbOtros.Rows[i]["Cantidad"].ToString());
                comm3.Parameters.AddWithValue("@malestado", tbOtros.Rows[i]["MalEstado"].ToString());
                comm3.Parameters.AddWithValue("@cambio", tbOtros.Rows[i]["CambioNormativa"].ToString());
                comm3.Parameters.AddWithValue("@observacion", tbOtros.Rows[i]["Observacion"].ToString());
                comm3.Parameters.AddWithValue("@idmantenimiento", idMantenimiento);
                comm3.Parameters.AddWithValue("@idCriterio", tbOtros.Rows[i]["IdCriterio"].ToString());
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();
            }

            GuardarMateriales();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se guardo el mantenimiento con exito');", true);
            Response.Redirect("SeguimientoServicios.aspx", false);

        }

        private void GuardarMateriales()
        {

            foreach (GridViewRow gvr in GVTotal.Rows)
            {
                string insertMaterial = "insert ListaMateriales(Cantidad,Subtotal,IdMaterial,IdMantenimiento,FechaPedido) values (@cantidad,@subtotal,@idmat,@idmant,@fecha)";
                SqlCommand comm = new SqlCommand(insertMaterial, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cantidad", gvr.Cells[2].Text);
                comm.Parameters.AddWithValue("@subtotal", gvr.Cells[3].Text);
                comm.Parameters.AddWithValue("@idmat", gvr.Cells[0].Text);
                comm.Parameters.AddWithValue("@idmant", idMantenimiento);
                comm.Parameters.AddWithValue("@fecha", txtFecha.Text);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
        }
    }
}