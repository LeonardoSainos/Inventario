using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Inventario.includes
{
    public partial class admin : System.Web.UI.UserControl
    {
        private string name = "", fullname = "", cont="", res="";
        private string[] viewd, whitel;
        public string lastVisitedUrl { get; set; }

        public string content
        {
            set { cont= value; }
            get { return cont; }

        }
        public string result
        {
            set {res = value; }
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
        public string [] ViewDiferent
        {
            set { viewd = value; }
            get { return viewd; }
        }
        public string [] WhiteList
        {
            set { whitel = value; }
            get { return whitel; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            int idObject = Convert.ToInt32(Session["rol"]);


            HttpCookie userIdCookie = Request.Cookies["UserId"];
            HttpCookie rolIdCookie = Request.Cookies["RolId"];
            HttpCookie emailCookie = Request.Cookies["Email"];
            HttpCookie userCookie = Request.Cookies["UserName"];
            HttpCookie fullnameCookie = Request.Cookies["CompletoName"];

            if (idObject!=4046 && Convert.ToString(rolIdCookie)!="4046")
            {
                HttpContext.Current.Response.Redirect("~/Inventario/process/logout.aspx");
            }
            nombre = Session["Nombre"] as string;
            completoName = Session["nombre_completo"] as string;
            ViewDiferent = new string[] { "searchUsers", "searchDepa", "searchTicket", "searchBrands", "searchModels", "searchTypes", "searchCars" };
            WhiteList = new string[] { "ticketadmin", "interno", "ticketedit", "mecanico", "admin", "config", "almacenista", "depa", "depaedit", "userEdit", "acciones", "brands", "brandsEdit", "models", "modelsEdit", "types", "typeEdit", "cars", "carEdit" };
            if (Request.QueryString["view"] != null && Session["id"] != null)
            {
                content = Request.QueryString["view"];
                result = content.Substring(content.LastIndexOf('/') + 1);
               string i = Request.Cookies["LastVisitedURL"]?.Value;
                HttpCookie urlCookie = new HttpCookie("LastVisitedURL");
                if (content != result)
                {
                    urlCookie.Value = content;
                    lastVisitedUrl = urlCookie.Value;
                    // urlCookie.Expires = DateTime.Now.AddDays(1); // La cookie expirará en 1 día
                    urlCookie.Expires = DateTime.MinValue;
                    Response.Cookies.Add(urlCookie);
                }
                else if(content == result && !content.Contains("/") && content!=i )
                {
                    if (content == result && !content.Contains("/") && i!=null)
                    {
                        urlCookie.Value = content;
                        lastVisitedUrl = Request.Cookies["LastVisitedURL"]?.Value;
                    }
                    else
                    {
                        urlCookie.Value = content;
                        lastVisitedUrl = urlCookie.Value;
                        // urlCookie.Expires = DateTime.Now.AddDays(1); // La cookie expirará en 1 día
                        urlCookie.Expires = DateTime.MinValue;
                        Response.Cookies.Add(urlCookie);
                    } 
                }              
                else
                {
                    urlCookie.Value = content;
                    lastVisitedUrl = Request.Cookies["LastVisitedURL"]?.Value;
                }
            }
        }
    }
}