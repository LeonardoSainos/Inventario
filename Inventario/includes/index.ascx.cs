using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 

namespace Inventario.includes
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        private string name = "", fullname = "", cont, res = "";
        private string[] viewd, whitel;
        public string lastVisitedUrl { get; set; }
        public HttpCookie userIdCookie { get; set; }


        public string content
        {
            set { cont = value; }
            get { return cont; }

        }
        public string result
        {
            set { res = value; }
            get { return res; }
        }
        public string nombre
        {
            set { name = value; }
            get { return name; }
        }
        public string completoName
        {
            set { fullname = value; }
            get { return fullname; }
        }
        public string[] ViewDiferent
        {
            set { viewd = value; }
            get { return viewd; }
        }
        public string[] WhiteList
        {
            set { whitel = value; }
            get { return whitel; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            userIdCookie = Request.Cookies["UserId"];
            HttpCookie rolIdCookie = Request.Cookies["RolId"];
            HttpCookie emailCookie = Request.Cookies["Email"];
            HttpCookie userCookie = Request.Cookies["UserName"];
            HttpCookie fullnameCookie = Request.Cookies["CompletoName"];

            if ((content == null || content == "") && (Session["rol"] != null | rolIdCookie != null))
            {
                content = Request.QueryString["view"] ?? "index";
            }

            nombre = Session["Nombre"] as string;
            completoName = Session["nombre_completo"] as string;
             ViewDiferent = new string[] { "searchTicket", "filterTicket" };
             WhiteList = new string [] { "index", "productos", "soporte", "ticket", "ticketcon", "registro", "configuracion", "ticketClient" };
        
        }
    }
}
