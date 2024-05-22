using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventario.Inventario.admin.Vehiculos
{
    public partial class searchVehiculo_view : System.Web.UI.UserControl
    {
        private string  nombrepagina="";
        public string PaginaNombre
        {
            set { nombrepagina = value; }
            get { return nombrepagina; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}