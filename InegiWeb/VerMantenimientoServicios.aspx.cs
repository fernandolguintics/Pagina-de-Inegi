using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Reporting.WebForms;

namespace InegiWeb
{
    public partial class VerMantenimientoServicios : System.Web.UI.Page
    {
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        string idMantenimiento = SeguimientoServicios.idMantenimiento;
        static double costoTotal = 0.0;
        static int malEstado = 0;
        static DataTable tb;
        static string deleteid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LLenarCblMateriales();
                ObtenerDatos();
                LlenarTablaCarpinteria();
                LlenarTablaHerreria();
                LLenarTablaPintura();
                LLenarTablaHidrosanitarias();
                LLenarTablaPisos();
                LLenarTablaOtros();
                LlenaTablaMateriales();
            }
        }
        private void LlenaTablaMateriales()
        {
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            tb = new DataTable();
            double total = 0.0;
            tb.Columns.AddRange(new DataColumn[]{
               new DataColumn("Id", typeof(string)),
               new DataColumn("NombreMaterial", typeof(string)),
               new DataColumn("UnidadMedida", typeof(string)),
               new DataColumn("Responsable", typeof(string)),
               new DataColumn("Fecha", typeof(string)),
               new DataColumn("Precio", typeof(string)),
               new DataColumn("Cantidad", typeof(string)),
               new DataColumn("Subtotal", typeof(string)),
               new DataColumn("Subtotal2", typeof(string))

            });

            string materiales = "select IdListaMaterial,CONCAT(NombreMaterial,'',Descripcion) as 'NombreMaterial',UnidadMedida,Responsable,FORMAT(FechaPedido,'dd/MM/yyyy') as 'FechaPedido',FORMAT(Precio,'C','en-us') as 'Precio',  Cantidad,FORMAT(Subtotal,'C','en-us') as 'SubTotal',Subtotal from ListaMateriales inner join MaterialesServicio on ListaMateriales.IdMaterial = MaterialesServicio.IdMaterialesServicio where IdMantenimiento=@id";
            SqlCommand comm = new SqlCommand(materiales, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@id", idMantenimiento);
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    total += Convert.ToDouble(dr["Subtotal"].ToString());
                    tb.Rows.Add(
                       dr["IdListaMaterial"].ToString(),
                       dr["NombreMaterial"].ToString(),
                       dr["UnidadMedida"].ToString(),
                       dr["Responsable"].ToString(),
                       dr["FechaPedido"].ToString(),
                       dr["Precio"].ToString(),
                       dr["Cantidad"].ToString(),
                       dr["SubTotal"].ToString(),
                       dr["Subtotal"].ToString()
                   );
                }
            }
            if (tipoUsuario.Equals("Administrador"))
            {

                GVTotal.DataSource = tb;
                GVTotal.DataBind();
            }
            else
            {

                GVTotal.Columns[9].Visible = false;
                GVTotal.Columns[0].Visible = false;
                GVTotal.DataSource = tb;
                GVTotal.DataBind();
            }
            txtTotal.Text ="Total:"+ total.ToString("C2");

        }

        private void LLenarTablaOtros()
        {
            DataTable tableOtros = new DataTable();
            tableOtros.Columns.AddRange(new DataColumn[]{
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Otros'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    malEstado += Convert.ToInt32(dr2["MalEstado"].ToString());

                    tableOtros.Rows.Add(
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVOtros.DataSource = tableOtros;
            GVOtros.DataBind();
        }

        private void LLenarTablaPisos()
        {
            DataTable tablePisos = new DataTable();
            tablePisos.Columns.AddRange(new DataColumn[]{
                 new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Piso,Plafones y Techo'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    malEstado += Convert.ToInt32(dr2["MalEstado"].ToString());
                    tablePisos.Rows.Add(
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVPisos.DataSource = tablePisos;
            GVPisos.DataBind();
        }

        private void LLenarTablaHidrosanitarias()
        {
            DataTable tableHidro = new DataTable();
            tableHidro.Columns.AddRange(new DataColumn[]{
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Instalaciones Hidrosanitarias'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    malEstado += Convert.ToInt32(dr2["MalEstado"].ToString());
                    tableHidro.Rows.Add(
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVHidrosanitarias.DataSource = tableHidro;
            GVHidrosanitarias.DataBind();
        }

        private void LLenarTablaPintura()
        {
            DataTable tablePintura = new DataTable();
            tablePintura.Columns.AddRange(new DataColumn[]{
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Pintura'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    malEstado += Convert.ToInt32(dr2["MalEstado"].ToString());
                    tablePintura.Rows.Add(
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVPintura.DataSource = tablePintura;
            GVPintura.DataBind();
        }

        private void LlenarTablaHerreria()
        {
            DataTable tableHerr = new DataTable();
            tableHerr.Columns.AddRange(new DataColumn[]{
               new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Herrería'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    malEstado += Convert.ToInt32(dr2["MalEstado"].ToString());
                    tableHerr.Rows.Add(
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVHerreria.DataSource = tableHerr;
            GVHerreria.DataBind();
        }

        private void LlenarTablaCarpinteria()
        {
            DataTable tableCarp = new DataTable();
            tableCarp.Columns.AddRange(new DataColumn[]{
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Piso", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("MalEstado", typeof(string)),
                new DataColumn("CambioNormativa", typeof(string)),
                new DataColumn("Observacion", typeof(string)),

            });

            string selectMateriales = @"select Criterio,Piso,CantidadTotal,MalEstado,CambioNormativa,Observacion from DetallesMantenimientoServicio inner join CriteriosServicio on 
                                            DetallesMantenimientoServicio.IdCriterio = CriteriosServicio.IdCriterioServicio where IdMantenimiento =@IdMantenimiento and Categoria = 'Carpintería y Cristales'";
            SqlCommand command2 = new SqlCommand(selectMateriales, sqlconn);
            command2.Connection.Open();
            command2.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader dr2 = command2.ExecuteReader();

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    malEstado += Convert.ToInt32(dr2["MalEstado"].ToString());
                    tableCarp.Rows.Add(
                        dr2["Criterio"].ToString(),
                        dr2["Piso"].ToString(),
                        dr2["CantidadTotal"].ToString(),
                        dr2["MalEstado"].ToString(),
                        dr2["CambioNormativa"].ToString(),
                        dr2["Observacion"].ToString()

                    );
                }
            }
            command2.Connection.Close();

            GVCarpinteria.DataSource = tableCarp;
            GVCarpinteria.DataBind();
        }

        private void ObtenerDatos()
        {
            String query = @"select FORMAT(CostoTotal,'C','en-us') as 'CostoTotal',Format(Fecha,'dd/MM/yyyy') as 'FechaMantenimiento',NombreEdificio,Direccion,Posesion,Ocupacion,Superficie,Uso,Niveles,Cajones,Antiguedad,Terreno from MantenimientoServicios inner join 
                                        Edificios on MantenimientoServicios.IdEdificio = Edificios.IdEdificio where IdMantenimientoServicio = @IdMantenimiento";
            SqlCommand command = new SqlCommand(query, sqlconn);
            command.Connection.Open();
            command.Parameters.AddWithValue("@IdMantenimiento", idMantenimiento);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //txtTotal.Text = "Total:" + reader["CostoTotal"].ToString();
                txtFecha.Text = "Fecha de Evaluación:" + reader["FechaMantenimiento"].ToString();
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

        protected void btnOrdenar_Click(object sender, EventArgs e)
        {
            mpeMostar.Show();
            GuardarListaMateriales();
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
                if (cantidad.Equals(""))
                {
                    System.Windows.Forms.MessageBox.Show("No se puede dejar cantidades en blanco");
                    return;
                }
                else
                {
                    string precio = gvr.Cells[2].Text;
                    double subtotal = Convert.ToDouble(precio) * Convert.ToDouble(cantidad);
                    costoTotal = costoTotal + subtotal;
                    rowTable = tb.NewRow();
                    rowTable[0] = HttpUtility.HtmlDecode(gvr.Cells[0].Text);
                    rowTable[1] = gvr.Cells[1].Text;
                    rowTable[2] = cantidad;
                    rowTable[3] = subtotal;
                    tb.Rows.Add(rowTable);
                }

            }

            txtTotal2.Text = "Total:" + costoTotal.ToString();
            GVTotal2.DataSource = tb;
            GVTotal2.DataBind();
            mpeTotal2.Show();
        }

        protected void btnAtras2_Click(object sender, EventArgs e)
        {
            costoTotal = 0.0;
            mpeMostar.Show();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            foreach (GridViewRow gvr in GVTotal2.Rows)
            {
                string insertMaterial = "insert ListaMateriales(Cantidad,Subtotal,IdMaterial,IdMantenimiento,FechaPedido) values (@cantidad,@subtotal,@idmat,@idmant,@fecha)";
                SqlCommand comm = new SqlCommand(insertMaterial, sqlconn);
                comm.Connection.Open();
                comm.Parameters.AddWithValue("@cantidad", gvr.Cells[2].Text);
                comm.Parameters.AddWithValue("@subtotal", gvr.Cells[3].Text);
                comm.Parameters.AddWithValue("@idmat", gvr.Cells[0].Text);
                comm.Parameters.AddWithValue("@idmant", idMantenimiento);
                comm.Parameters.AddWithValue("@fecha", date);
                comm.ExecuteNonQuery();
                comm.Connection.Close();
            }

            string updateCosto = "select SUM(Subtotal) as 'Subtotal' from ListaMateriales where IdMantenimiento=@idMant";
            SqlCommand comm2 = new SqlCommand(updateCosto, sqlconn);
            comm2.Connection.Open();
            comm2.Parameters.AddWithValue("@idMant", idMantenimiento);
            SqlDataReader dr = comm2.ExecuteReader();
            if (dr.Read())
            {
                string total = dr["Subtotal"].ToString();
                comm2.Connection.Close();

                string update = "update MantenimientoServicios set CostoTotal =@costo where IdMantenimientoServicio=@id";
                SqlCommand comm3 = new SqlCommand(update, sqlconn);
                comm3.Connection.Open();
                comm3.Parameters.AddWithValue("@costo", total);
                comm3.Parameters.AddWithValue("@id", idMantenimiento);
                comm3.ExecuteNonQuery();
                comm3.Connection.Close();

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se han agregado los materiales a la lista');", true);
            Response.Redirect("SeguimientoServicios.aspx", false);
        }

        protected void GVTotal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVTotal.Rows[index];
                deleteid = row.Cells[0].Text;
                mpeBorrar.Show();
                nombreMaterialtxt.Text = row.Cells[1].Text;
            }
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            string updateCosto = "select SUM(Subtotal) as 'Subtotal' from ListaMateriales where IdMantenimiento=@idMant";
            SqlCommand comm2 = new SqlCommand(updateCosto, sqlconn);
            comm2.Connection.Open();
            comm2.Parameters.AddWithValue("@idMant", idMantenimiento);
            SqlDataReader dr = comm2.ExecuteReader();
            if (dr.Read())
            {
                string total = dr["Subtotal"].ToString();
                comm2.Connection.Close();

                string select = "select Subtotal from ListaMateriales where IdListaMaterial = @id";
                SqlCommand comm1 = new SqlCommand(select, sqlconn);
                comm1.Connection.Open();
                comm1.Parameters.AddWithValue("@id", deleteid);
                SqlDataReader dr2 = comm1.ExecuteReader();
                if (dr2.Read())
                {
                    string subtotal = dr2["Subtotal"].ToString();
                    comm1.Connection.Close();

                    double precioActualizado = Convert.ToDouble(total) - Convert.ToDouble(subtotal);

                    string update = "update MantenimientoServicios set CostoTotal =@costo where IdMantenimientoServicio=@id";
                    SqlCommand comm3 = new SqlCommand(update, sqlconn);
                    comm3.Connection.Open();
                    comm3.Parameters.AddWithValue("@costo", precioActualizado);
                    comm3.Parameters.AddWithValue("@id", idMantenimiento);
                    comm3.ExecuteNonQuery();
                    comm3.Connection.Close();

                }
            }
            string borrarMaterial = "delete from ListaMateriales where IdListaMaterial = @id";
            SqlCommand comm = new SqlCommand(borrarMaterial, sqlconn);
            comm.Connection.Open();
            comm.Parameters.AddWithValue("@id", deleteid);
            comm.ExecuteNonQuery();
            comm.Connection.Close();
            Response.Redirect("SeguimientoServicios.aspx", false);
        }

      
    }
}