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
    public partial class CatalogoInfraestructura : System.Web.UI.Page
    {
        public static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static string idMaterialActualizar;
        public static string idMaterialEliminar;
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarTablaMateriales();
            if (!IsPostBack)
            {
                LlenarDropdownList();
            }
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
            SqlCommand command = new SqlCommand("select idMaterial, nombreMaterial, FORMAT(precio,'C','en-us') as 'precio', nombreCategoria from Materiales inner join Categorias on Materiales.idCategoria=Categorias.idCategoria", sqlconn);
            command.Connection.Open();
            SqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableMateriales.Rows.Add(
                        dr["idMaterial"].ToString(),
                        dr["nombreMaterial"].ToString(),
                        dr["nombreCategoria"].ToString(),
                        dr["Precio"].ToString()
                    );
                }
            }
            command.Connection.Close();

            GVMateriales.DataSource = tableMateriales;
            GVMateriales.DataBind();


        }

        protected void btnAgregarResponsable_Click(object sender, EventArgs e)
        {
            String query = "Insert into Responsables (nombreResponsable) values(@nombreResponsable)";
            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand comand = new SqlCommand(query, sqlconn);
                comand.Connection.Open();
                comand.Parameters.AddWithValue("@nombreResponsable", txbResponsable.Text);
                comand.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo agregar el material');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo agregar el material');", true);
                Console.WriteLine(ex.ToString());
            }
        }

        protected void LlenarDropdownList()
        {

            try
            {
                //Dropdownlist Responsables
                String query = "Select idResponsable,nombreResponsable from Responsables";
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                DropDownListResponsables.DataSource = dt;
                DropDownListResponsables.DataTextField = "nombreResponsable";
                DropDownListResponsables.DataValueField = "idResponsable";
                DropDownListResponsables.DataBind();
                DropDownListResponsables.Items.Insert(0, new ListItem("<Seleccionar Responsable>", "0"));

                //Dropdownlist Responsables
                String query2 = "Select idCategoria,nombreCategoria from Categorias";
                SqlDataAdapter sda2 = new SqlDataAdapter(query2, sqlconn);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);

                DropDownListCategorias.DataSource = dt2;
                DropDownListCategorias.DataTextField = "nombreCategoria";
                DropDownListCategorias.DataValueField = "idCategoria";
                DropDownListCategorias.DataBind();
                DropDownListCategorias.Items.Insert(0, new ListItem("<Seleccionar Categoria>", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo agregar el material');", true);
                Console.WriteLine(ex.ToString());
            }


        }

        protected void btnAgregarCat_Click(object sender, EventArgs e)
        {
            String query = "Insert into Categorias (nombreCategoria) values(@nombreCategoria)";
            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand comand = new SqlCommand(query, sqlconn);
                comand.Connection.Open();
                comand.Parameters.AddWithValue("@nombreCategoria", txbCategoria.Text);
                comand.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se ha insertado la categoria');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo agregar el material');", true);
                Console.WriteLine(ex.ToString());
            }
        }

        protected void BtnAgregarMaterial_Click(object sender, EventArgs e)
        {


            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                string insertQuery = "insert Materiales(nombreMaterial,unidadMedida,precio,idResponsable,idCategoria,Descripcion) values(@nombreMaterial, @unidadMedida, @pre, @idResponsable, @idCategoria, @descripcion)";
                SqlCommand command = new SqlCommand(insertQuery, sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@nombreMaterial", txbNombreMat.Text);
                command.Parameters.AddWithValue("@unidadMedida", txbUnidadMedida.Text);
                command.Parameters.AddWithValue("@pre", txbPrecio.Text);
                command.Parameters.AddWithValue("@idResponsable", DropDownListResponsables.Text);
                command.Parameters.AddWithValue("@idCategoria", DropDownListCategorias.Text);
                command.Parameters.AddWithValue("@descripcion", txbDescripcion.Text);
                command.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se agrego el material');", true);
                command.Connection.Close();
                Response.Redirect("CatalogoInfraestructura.aspx", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo agregar el material');", true);
                Console.WriteLine(ex.ToString());
            }


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
                string querySelect = @"select nombreMaterial, precio,unidadMedida, nombreCategoria,nombreResponsable,Descripcion from Materiales inner join Categorias on Materiales.idCategoria=Categorias.idCategoria inner join Responsables on Materiales.idResponsable = Responsables.idResponsable where idMaterial=@IdMaterial";
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@IdMaterial", idMaterial);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txbNombreMat.Text = dr["nombreMaterial"].ToString();
                    txbPrecio.Text = dr["precio"].ToString();
                    txbUnidadMedida.Text = dr["unidadMedida"].ToString();
                    DropDownListCategorias.SelectedItem.Text = dr["nombreCategoria"].ToString();
                    DropDownListResponsables.SelectedItem.Text = dr["nombreResponsable"].ToString();
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
                    string querySelect = @"select nombreMaterial, precio,unidadMedida, Descripcion from Materiales e where idMaterial=@IdMaterial";
                    SqlConnection sqlconn = new SqlConnection(conectar);
                    SqlCommand cmd = new SqlCommand(querySelect, sqlconn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@IdMaterial", idMaterialActualizar);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txbNombreMat.Text = dr["nombreMaterial"].ToString();
                        txbPrecio.Text = dr["precio"].ToString();
                        txbUnidadMedida.Text = dr["unidadMedida"].ToString();
                        txbDescripcion.Text = dr["Descripcion"].ToString();

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

        protected void DeshabilitarCasillas()
        {
            txbNombreMat.Enabled = false;
            txbPrecio.Enabled = false;
            txbUnidadMedida.Enabled = false;
            DropDownListCategorias.Enabled = false;
            DropDownListResponsables.Enabled = false;
            txbDescripcion.Enabled = false;
        }
        protected void VaciarCasillas()
        {
            txbNombreMat.Text = "";
            txbPrecio.Text = "";
            txbUnidadMedida.Text = "";
            txbDescripcion.Text = "";


        }

        protected void HabilitarCasillas()
        {
            txbNombreMat.Enabled = true;
            txbPrecio.Enabled = true;
            txbUnidadMedida.Enabled = true;
            DropDownListCategorias.Enabled = true;
            DropDownListResponsables.Enabled = true;
            txbDescripcion.Enabled = true;

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(conectar);
                SqlCommand command = new SqlCommand("update Materiales set nombreMaterial=@nombreMaterial, unidadMedida=@unidadMedida, precio=@precio, idResponsable=@idResponsable, idCategoria=@idCategoria, Descripcion=@descripcion where idMaterial=@idMaterial", sqlconn);
                command.Connection.Open();
                command.Parameters.AddWithValue("@nombreMaterial", txbNombreMat.Text);
                command.Parameters.AddWithValue("@unidadMedida", txbUnidadMedida.Text);
                command.Parameters.AddWithValue("@precio", txbPrecio.Text);
                command.Parameters.AddWithValue("@idResponsable", DropDownListResponsables.Text);
                command.Parameters.AddWithValue("@idCategoria", DropDownListCategorias.Text);
                command.Parameters.AddWithValue("@descripcion", txbDescripcion.Text);
                command.Parameters.AddWithValue("@idMaterial", idMaterialActualizar);
                command.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo actualizar el material');", true);
                command.Connection.Close();
                Response.Redirect("CatalogoInfraestructura.aspx", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo actualizar el material');", true);
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
            LlenarDropdownList();
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            string queryDelete = "delete from Materiales where idMaterial = @IdMaterial";
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand(queryDelete, sqlconn);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@IdMaterial", idMaterialEliminar);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Se ha borrado el material');", true);
            Response.Redirect("CatalogoInfraestructura.aspx", false);
        }

        protected void GVMateriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GVMateriales.PageIndex = e.NewPageIndex;
            LlenarTablaMateriales();
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            DataTable tableMateriales = new DataTable();
            tableMateriales.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id", typeof(string)),
                new DataColumn("Material", typeof(string)),
                new DataColumn("Categoria", typeof(string)),
                new DataColumn("Precio", typeof(string)),
            });

            string buscar = txbBuscar.Text;
            string queryBuscar = "select idMaterial, nombreMaterial, precio, nombreCategoria from Materiales inner join Categorias on Materiales.idCategoria=Categorias.idCategoria where nombreMaterial=@nombreMaterial";
            SqlConnection sqlconn = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand(queryBuscar, sqlconn);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@nombreMaterial", buscar);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tableMateriales.Rows.Add(
                       dr["idMaterial"].ToString(),
                       dr["nombreMaterial"].ToString(),
                       dr["nombreCategoria"].ToString(),
                       dr["Precio"].ToString()
                   );
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo agregar el material');", true);
            }
        }
    }
}