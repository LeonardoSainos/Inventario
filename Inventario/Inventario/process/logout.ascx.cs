using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Inventario.process
{
    public partial class logout : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {  // Iterar sobre todas las cookies y eliminarlas una por una
            foreach (string cookieName in Request.Cookies.AllKeys)
            {
                HttpCookie cookie = Request.Cookies[cookieName];
                cookie.Expires = DateTime.Now.AddDays(-1); // Establecer la fecha de expiración en el pasado
                Response.Cookies.Add(cookie); // Agregar la cookie al Response para que se borre en el cliente
            }
            Session.Clear();
           Response.Redirect(".././../index.aspx");
        }
    }
}