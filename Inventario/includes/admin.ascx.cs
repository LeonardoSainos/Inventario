using Inventario.Inventario.lib;
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
        private string name = "", fullname = "", cont = "", res = "", last, urle="";
        private string[] viewd, whitel;
        public string lastVisitedUrl
        {
            get { return last; }
            set { last = value; }
        }
        public string url
        {
            set { urle = value; }
            get { return urle; }
        }
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
            int idObject = Convert.ToInt32(Session["rol"]);
            HttpCookie userIdCookie = Functions.ObtenerCookie("UserId");
            HttpCookie rolIdCookie = Functions.ObtenerCookie("RolId");
            HttpCookie emailCookie = Functions.ObtenerCookie("Email");
            HttpCookie userCookie = Functions.ObtenerCookie("UserName");
            HttpCookie fullnameCookie = Functions.ObtenerCookie("CompletoName");
            if (idObject == 999999 && (Convert.ToString(rolIdCookie.Value)=="" || Convert.ToString(rolIdCookie.Value)==null))
            {
                HttpContext.Current.Response.Redirect("~/Inventario/process/logout.aspx");
            }
            nombre = Session["Nombre"] as string;
            completoName = Session["nombre_completo"] as string;
            ViewDiferent = new string[] { "searchUsers", "searchDepa", "searchTicket", "searchBrands", "searchModels", "searchTypes", "searchCars" };
            WhiteList = new string[] { "ticketadmin", "interno", "ticketedit", "mecanico", "admin", "config", "almacenista", "depa", "depaedit", "userEdit", "acciones", "brands", "brandEdit", "models", "modelEdit", "types", "typeEdit", "cars", "carEdit" };

            


            if (Request.QueryString["view"] != null && (Session["id"] != null || userIdCookie != null))
            {
                content = Request.QueryString["view"];
                url = content;
                switch (content)
                {
                    case "searchTypes":
                        {
                            url = "admin/TipoVehiculos/" + content;
                            break;
                        }
                    case "searchModels":
                        {
                            url = "admin/ModeloVehiculos/" + content;
                            break;
                        }
                    case "searchBrands":
                        {
                            url = "admin/Marcas/" + content;
                            break;
                        }
                    
                }
                result = content.Substring(content.LastIndexOf('/') + 1);
                string i = Request.Cookies["LastVisitedURL"]?.Value;
                
                
                HttpCookie urlCookie = Functions.CrearCookie("", "LastVisitedURL", Response);   
                if (content != result)
                {
                    urlCookie.Value = content;
                    lastVisitedUrl = urlCookie.Value;
                }
                else if (content == result && !content.Contains("/") && content != i)
                {
                    if (content == result && !content.Contains("/") && i != null)
                    {
                        urlCookie.Value = i;
                        lastVisitedUrl = i;
                    }
                    else
                    {
                        urlCookie.Value = content;
                        lastVisitedUrl = urlCookie.Value;
                       urlCookie.Expires = DateTime.MinValue;
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