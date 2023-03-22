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
    public partial class Menu : System.Web.UI.Page
    {
        static string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(conectar);
        public static String idInmueble;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarDropDownList();

            }
            txtnombre.Text = Convert.ToString(Session["Nombres"]) + " " + Convert.ToString(Session["Apellidos"]);
        }

        public void LlenarDropDownList()
        {
            //Dropdownlist Inmuebles
            String query = "Select IdEdificio,NombreEdificio from Edificios";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            DropDownListInmuebles.DataSource = dt;
            DropDownListInmuebles.DataTextField = "NombreEdificio";
            DropDownListInmuebles.DataValueField = "IdEdificio";
            DropDownListInmuebles.DataBind();
            DropDownListInmuebles.Items.Insert(0, new ListItem("<Seleccionar Inmueble>", "0"));

            DropDownListInmuebles2.DataSource = dt;
            DropDownListInmuebles2.DataTextField = "NombreEdificio";
            DropDownListInmuebles2.DataValueField = "IdEdificio";
            DropDownListInmuebles2.DataBind();
            DropDownListInmuebles2.Items.Insert(0, new ListItem("<Seleccionar Inmueble>", "0"));

            DropDownListInmuebles3.DataSource = dt;
            DropDownListInmuebles3.DataTextField = "NombreEdificio";
            DropDownListInmuebles3.DataValueField = "IdEdificio";
            DropDownListInmuebles3.DataBind();
            DropDownListInmuebles3.Items.Insert(0, new ListItem("<Seleccionar Inmueble>", "0"));
        }
        protected void Crear_Click(object sender, EventArgs e)
        {



            idInmueble = DropDownListInmuebles.SelectedValue.ToString();

            if (Response.IsClientConnected)
            {
                Response.Redirect("MantenimientoInfraestructura.aspx", false);
                //string _open = "window.open('Mantenimiento.aspx', '_blank');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
            else
            {
                Response.End();
            }

        }

        protected void btnCrearServicio_Click(object sender, EventArgs e)
        {
            idInmueble = DropDownListInmuebles2.SelectedValue.ToString();

            if (Response.IsClientConnected)
            {
                Response.Redirect("MantenimientoServicios.aspx", false);
                //string _open = "window.open('MantenimientoServicios.aspx', '_blank');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
            else
            {
                Response.End();
            }

        }

        protected void btnMantenimientoAlmacen_Click(object sender, EventArgs e)
        {
            idInmueble = DropDownListInmuebles3.SelectedValue.ToString();

            if (Response.IsClientConnected)
            {

                Response.Redirect("MantenimientoAlmacenConsumo.aspx", false);
            }
            else
            {
                Response.End();
            }
        }
    }
}