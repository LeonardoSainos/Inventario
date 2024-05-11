using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Scripts;
using Inventario.Inventario.lib;
namespace Inventario.Inventario.inc
{
    public partial class navbar2 : System.Web.UI.UserControl
    {
        public HttpCookie fullNameCook, userIdCook, rolCook, emailCook, userCook;
        public HttpCookie fullNameCookie
        {
            get { return fullNameCook; }
            set { fullNameCook = value; }
        }
        public HttpCookie userIdCookie
        {
            get { return userIdCook; }
            set { userIdCook = value; }
        }
        public HttpCookie rolCookie
        {
            get { return rolCook; }
            set { rolCook = value; }
        }
        public HttpCookie emailCookie
        {
            get { return emailCook; }
            set { emailCook = value; }
        }
        public HttpCookie userCookie
        {
            get { return userCook; }
            set { userCook = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie userIdCookie = Functions.ObtenerCookie("UserId");
            rolCookie = Functions.ObtenerCookie("RolId");
            emailCookie = Functions.ObtenerCookie("Email");
            userCookie = Functions.ObtenerCookie("UserName");
            fullNameCookie = Functions.ObtenerCookie("CompletoName"); 
        }
    }
}
   
