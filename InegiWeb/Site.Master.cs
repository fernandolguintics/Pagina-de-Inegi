using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InegiWeb
{
    public partial class SiteMaster : MasterPage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string tipoUsuario = Convert.ToString(Session["TipoUsuario"]);
            if (tipoUsuario.Equals("Tecnico"))
            {
                menuUsuarios.Visible = false;
                menuConfAdmi.Visible = false;
                menuPresupuestos.Visible = false;
                presupuestoServicios.Visible = false;
                confAlm.Visible = false;
            }
        }
    }
}