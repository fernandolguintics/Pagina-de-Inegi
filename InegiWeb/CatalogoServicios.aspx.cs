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
    public partial class CatalogoServicios : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static string idMaterialActualizar;
        public static string idMaterialEliminar;
        public static string idCriterioActualizar;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void DeshabilitarCasillas()
        {
            txbNombreMat.Enabled = false;
            txbUnidadMedida.Enabled = false;
            DropDownListCategorias.Enabled = false;
            DropDownListResponsables.Enabled = false;
            txbDescripcion.Enabled = false;
            txbPrecio.Enabled = false;
        }
        protected void VaciarCasillas()
        {
            txbNombreMat.Text = "";
            txbUnidadMedida.Text = "";
            txbDescripcion.Text = "";
            txbPrecio.Text = "";
        }

        protected void HabilitarCasillas()
        {
            txbNombreMat.Enabled = true;
            txbUnidadMedida.Enabled = true;
            DropDownListCategorias.Enabled = true;
            DropDownListResponsables.Enabled = true;
            txbDescripcion.Enabled = true;
            txbPrecio.Enabled = true;

        }
        private void LlenarTablaMateriales()
        {
            DataTable tableMateriales = new DataTable();
            tableMateriales.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Categoria", typeof(string)),
                new DataColumn("Precio", typeof(string)),
            });
            SqlCommand command = new SqlCommand("select IdMaterialesServicio, NombreMaterial, FORMAT(Precio,'C','en-us') as 'Precio', Categoria from MaterialesServicio", sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableMateriales.Rows.Add(
                        dr["IdMaterialesServicio"].ToString(),
                        dr["NombreMaterial"].ToString(),
                        dr["Categoria"].ToString(),
                        dr["Precio"].ToString()
                    );
                }
            }
            command.Connection.Close();

            GVMateriales.DataSource = tableMateriales;
            GVMateriales.DataBind();
        }


        protected void GVMateriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVMateriales.PageIndex = e.NewPageIndex;
            LlenarTablaMateriales();
        }

        protected void GVMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                btnAgregarMaterial.Visible = false;
                btnActualizar.Visible = false;
                mpeMaterial.Show();
                txtTituloModal.Text = "Datos del Material";
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectRow = GVMateriales.Rows[index];
                string idMaterial = selectRow.Cells[0].Text;
                string querySelect = @"select NombreMaterial,UnidadMedida, Categoria,Responsable,Descripcion,Precio from MaterialesServicio where IdMaterialesServicio=@Id";
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@Id", idMaterial);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txbNombreMat.Text = dr["NombreMaterial"].ToString();
                    txbUnidadMedida.Text = dr["UnidadMedida"].ToString();
                    txbPrecio.Text = dr["Precio"].ToString();
                    DropDownListCategorias.SelectedItem.Text = dr["Categoria"].ToString();
                    DropDownListResponsables.SelectedItem.Text = dr["Responsable"].ToString();
                    txbDescripcion.Text = dr["Descripcion"].ToString();

                }
                DeshabilitarCasillas();
                cmd.Connection.Close();
            }
            else
            {
                if (e.CommandName == "Editar")
                {
                    HabilitarCasillas();
                    btnActualizar.Visible = true;
                    btnAgregarMaterial.Visible = false;
                    txtTituloModal.Text = "Actualizar Material";
                    mpeMaterial.Show();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow selectRow = GVMateriales.Rows[index];
                    idMaterialActualizar = selectRow.Cells[0].Text;
                    string querySelect = @"select NombreMaterial,UnidadMedida, Descripcion,Precio from MaterialesServicio where IdMaterialesServicio=@IdMaterial";
                    SqlConnection sqlconn = new SqlConnection(conectar);
                    SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@IdMaterial", idMaterialActualizar);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txbNombreMat.Text = dr["NombreMaterial"].ToString();
                        txbUnidadMedida.Text = dr["UnidadMedida"].ToString();
                        txbDescripcion.Text = dr["Descripcion"].ToString();
                        txbPrecio.Text = dr["Precio"].ToString();

                    }
                    cmd.Connection.Close();
                }
                else
                {
                    if (e.CommandName == "Borrar")
                    {
                        mpeBorrarMaterial.Show();
                        int index = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = GVMateriales.Rows[index];
                        idMaterialEliminar = row.Cells[0].Text;
                        string nombreUsuario = row.Cells[1].Text;
                        nombrePresupuestotxt.Text = nombreUsuario;
                    }

                }
            }
        }


        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand command = new SqlCommand("update MaterialesServicio set NombreMaterial=@nombreMaterial, UnidadMedida=@unidadMedida, Precio=@precio, Responsable=@Responsable, Categoria=@Categoria, Descripcion=@descripcion where IdMaterialesServicio=@idMaterial", sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@nombreMaterial", txbNombreMat.Text);
                command.Parameters.AddWithValue("@unidadMedida", txbUnidadMedida.Text);
                command.Parameters.AddWithValue("@Responsable", DropDownListResponsables.Text);
                command.Parameters.AddWithValue("@Categoria", DropDownListCategorias.Text);
                command.Parameters.AddWithValue("@precio", txbPrecio.Text);
                command.Parameters.AddWithValue("@descripcion", txbDescripcion.Text);
                command.Parameters.AddWithValue("@idMaterial", idMaterialActualizar);
                command.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se actualizo el material');", true);
                command.Connection.Close();
                Response.Redirect("CatalogoServicios.aspx", false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void btnAgregarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                string insertQuery = "insert into MaterialesServicio(NombreMaterial,UnidadMedida,Responsable,Categoria,Precio,Descripcion) values(@nombreMaterial, @unidadMedida,@Responsable, @Categoria,@Precio, @descripcion)";
                SqlCommand command = new SqlCommand(insertQuery, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@nombreMaterial", txbNombreMat.Text);
                command.Parameters.AddWithValue("@unidadMedida", txbUnidadMedida.Text);
                command.Parameters.AddWithValue("@Responsable", DropDownListResponsables.Text);
                command.Parameters.AddWithValue("@Categoria", DropDownListCategorias.Text);
                command.Parameters.AddWithValue("@Precio", txbPrecio.Text);
                command.Parameters.AddWithValue("@descripcion", txbDescripcion.Text);
                command.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se agrego el material con exito');", true);
                command.Connection.Close();
                Response.Redirect("CatalogoServicios.aspx", false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        protected void btnCancelarMaterial_Click(object sender, EventArgs e)
        {
            VaciarCasillas();
            HabilitarCasillas();
            txtTituloModal.Text = "Agregar Material";
            btnActualizar.Visible = false;
            btnAgregarMaterial.Visible = true;
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            string queryDelete = "delete from MaterialesServicio where IdMaterialesServicio = @IdMaterial";
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand(queryDelete, sqlconn);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@IdMaterial", idMaterialEliminar);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se borro el material con exito');", true);
            Response.Redirect("CatalogoServicios.aspx", false);
        }

        protected void mostrar_Click(object sender, EventArgs e)
        {
            switch (seleccionarTabla.SelectedValue)
            {
                case "Materiales":
                    btnIngresarMaterial.Visible = true;
                    GVMateriales.Visible = true;
                    GVCriterios.Visible = false;
                    btnAgregarCriterio.Visible = false;
                    LlenarTablaMateriales();
                    break;
                case "Criterios":
                    GVMateriales.Visible = false;
                    btnIngresarMaterial.Visible = false;
                    GVCriterios.Visible = true;
                    btnAgregarCriterio.Visible = true;
                    LlenarTablaCriterios();
                    break;

            }
        }

        private void LlenarTablaCriterios()
        {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Criterio", typeof(string)),
                new DataColumn("Categoria", typeof(string)),
            });
            SqlCommand command = new SqlCommand("select IdCriterioServicio, Criterio, Categoria from CriteriosServicio", sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    table.Rows.Add(
                        dr["IdCriterioServicio"].ToString(),
                        dr["Criterio"].ToString(),
                        dr["Categoria"].ToString()
                    );
                }
            }
            command.Connection.Close();

            GVCriterios.DataSource = table;
            GVCriterios.DataBind();
        }

        protected void GVCriterios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVCriterios.PageIndex = e.NewPageIndex;
            LlenarTablaCriterios();
        }

        protected void GVCriterios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Editar")
            {
                HabilitarCasillas();
                actualizarCriterio.Visible = true;
                agregarCriterio.Visible = false;
                txtTituloModal2.Text = "Actualizar Criterio";
                mpeCriterio.Show();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectRow = GVCriterios.Rows[index];
                idCriterioActualizar = selectRow.Cells[0].Text;
                string querySelect = @"select * from CriteriosServicio where IdCriterioServicio=@Id";
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@Id", idCriterioActualizar);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txbCriterio.Text = dr["Criterio"].ToString();
                    DropDownList2.SelectedItem.Text = dr["Categoria"].ToString();

                }
                cmd.Connection.Close();


            }
        }

        protected void agregarCriterio_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                string insertQuery = "insert into CriteriosServicio(Criterio,Categoria) values(@Criterio, @Categoria)";
                SqlCommand command = new SqlCommand(insertQuery, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Criterio", txbCriterio.Text);
                command.Parameters.AddWithValue("@Categoria", DropDownList2.Text);
                command.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se agrego el criterio con exito');", true);
                command.Connection.Close();
                Response.Redirect("CatalogoServicios.aspx", false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected void actualizarCriterio_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand command = new SqlCommand("update CriteriosServicio set Criterio=@Criterio, Categoria=@Categoria where IdCriterioServicio=@id", sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@Criterio", txbCriterio.Text);
                command.Parameters.AddWithValue("@Categoria", DropDownList2.Text);
                command.Parameters.AddWithValue("@id", idCriterioActualizar);
                command.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se actualizo el criterio con exito');", true);
                command.Connection.Close();
                LlenarTablaCriterios();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}