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
        private string name = "", fullname = "", cont = "", res = "", last, urle = "";
        private string[] viewd, whitel;
        public string lastVisitedUrl  {   get { return last; } set { last = value; } }
        public string url { set { urle = value; }  get { return urle; } }
        public string content { set { cont = value; } get { return cont; }}
        public string result { set { res = value; }  get { return res; } }
        public string nombre{ set { name = value; }  get { return name; }}
        public string completoName { set { fullname = value; }get { return fullname; }}
        public string[] ViewDiferent { set { viewd = value; }get { return viewd; } }
        public string[] WhiteList { set { whitel = value; }get { return whitel; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            int idObject = Convert.ToInt32(Session["rol"]);
            HttpCookie userIdCookie = Functions.ObtenerCookie("UserId");
            HttpCookie rolIdCookie = Functions.ObtenerCookie("RolId");         
            nombre = Session["Nombre"] as string;
            completoName = Session["nombre_completo"] as string;
            WhiteList = new string[] { "ticketadmin", "interno", "ticketedit", "admin", "mecanico","almacenista","config", "depa", "depaedit", "userEdit", "acciones", "brands", "brandsEdit", "models", "modelEdit", "types", "typeEdit", "cars", "carEdit", "permissions" };
            ViewDiferent = new string[] { "searchUsers", "searchDepa", "searchTicket", "searchBrands", "searchModels", "searchTypes", "searchCars", "searchPermissions" };
           
            if (Request.QueryString["view"] != null && (Session["id"] != null || userIdCookie.Value != null)){
                LoadContent(); 
            }
            else if (idObject == 999999 && (Convert.ToString(rolIdCookie.Value) == "" || Convert.ToString(rolIdCookie.Value) == null)) {
                Response.Redirect("~/Inventario/process/logout.aspx"); 
            }
        }
        private void LoadContent()
        {
            string controlPath = string.Empty;
            content = Request.QueryString["view"];
            ValidarCookieUrl(content);
            if (content != null && WhiteList.Contains(result) && System.IO.File.Exists(Server.MapPath($"~/Inventario/admin/{lastVisitedUrl}-view.ascx")))
            {
                 controlPath = GetControlPath(result);
                 LoadPageWithNavbar(controlPath);
            }
            else if (content != null && ViewDiferent.Contains(result) && System.IO.File.Exists(Server.MapPath($"~/Inventario/{url}-view.ascx")))
            {
                controlPath = GetControlSearch(result);
                LoadPageWithoutNavBar(controlPath);
            }
            else
            {
                LoadErrorPage();
                Response.Redirect("~/Inventario/process/logout.aspx");
            }
        }
        private void LoadPageWithNavbar(string controlPath)
        {
            
            Control htmlContainer = new Control();
            
            htmlContainer.Controls.Add(new LiteralControl(@"<!DOCTYPE html>
                                                    <html>
                                                        <head>"));
      
            Control linksControl = LoadControl("~/Inventario/inc/links.ascx");
            htmlContainer.Controls.Add(linksControl);

            htmlContainer.Controls.Add(new LiteralControl(@"<title> Administración </title>
                                                    <link rel='icon' href='favicon.png'>
                                                        </head>
                                                    <body>
                                                        <div id='wrapper'>"));
            Control navbarControl = LoadControl("~/Inventario/inc/navbar.ascx");
            htmlContainer.Controls.Add(navbarControl);
            Control navbar2Control = LoadControl("~/Inventario/inc/navbar2.ascx");
            htmlContainer.Controls.Add(navbar2Control);
            htmlContainer.Controls.Add(new LiteralControl(@"<div id='page-wrapper'>
                                                        <div class='container'>
                                                            <div class='row'>
                                                                <div class='col-sm-12'>
                                                                    <div class='page-header'>
                                                                        <h1 class='animated lightSpeedIn'>Panel Administrativo<small></small></h1>
                                                                        <span class='label label-warning'>Transporte de logística S.A de C.V</span>
                                                                        <p class='pull-right text-primary'>"));
            Control timeZoneControl = LoadControl("~/Inventario/inc/timezone.ascx");
            htmlContainer.Controls.Add(timeZoneControl);
            htmlContainer.Controls.Add(new LiteralControl(@"</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='container'>"));
            PlaceHolder phContentPlaceholder = new PlaceHolder();
            phContentPlaceholder.ID = "phContentPlaceholder";
            htmlContainer.Controls.Add(phContentPlaceholder);
            htmlContainer.Controls.Add(new LiteralControl(@"</div>
                                                    </div>
                                                </div>"));
            Control footerControl = LoadControl("~/Inventario/inc/footer.ascx");
            htmlContainer.Controls.Add(footerControl);
            Control script2Control = LoadControl("~/Inventario/inc/linksJS.ascx");
            htmlContainer.Controls.Add(script2Control);
            htmlContainer.Controls.Add(new LiteralControl(@"</body></html>"));
            phContent.Controls.Add(htmlContainer);
 
            if (!string.IsNullOrEmpty(controlPath))
            {
                Control userControl = LoadControl(controlPath);
                var controlConTipoRol = userControl as dynamic;

                switch (result)
                {
                    case "admin":
                    case "brands":
                    case "models":
                    case "types":
                    case "cars":
                         if (controlConTipoRol != null)
                         {
                            controlConTipoRol.TipoRol = "admin";  // Asignar TipoRol
                         }
                        break;
                    case "almacenista":
                    case "mecanico":
                        if (controlConTipoRol != null)
                        {
                            controlConTipoRol.TipoRol = result;
                        }
                        break;
                }
                phContentPlaceholder.Controls.Add(userControl);
            }
        }
        private void LoadPageWithoutNavBar(string controlPath)
        {   Control htmlContainer = new Control();   
            htmlContainer.Controls.Add(new LiteralControl(@"<!DOCTYPE html>
                                                    <html>
                                                        <head>
                                                            <title>Administración</title>
                                                        </head>
                                                        <body>
                                                            <div class='container'>"));
                                                            PlaceHolder phContentPlaceholder = new PlaceHolder();
                                                            phContentPlaceholder.ID = "phContentPlaceholder";
                                                            htmlContainer.Controls.Add(phContentPlaceholder);
                                                           htmlContainer.Controls.Add(new LiteralControl(@"</div></body></html>"));
                                                           phContent.Controls.Add(htmlContainer);
                if (!string.IsNullOrEmpty(controlPath))
                {
                    Control userControl = LoadControl(controlPath);
                    switch (result)
                    {
                        case "SearchUsers":
                        case "SearchBrandsAdmin":
                        case "SearchModelsAdmin":
                        case "SearchTypesAdmin":
                        case "SearchCarsAdmin":
                            var controlConTipoRol = userControl as dynamic;
                            if (controlConTipoRol != null)
                            {
                                controlConTipoRol.NamePagina = content;   
                            }
                            break;
                    }
                    phContentPlaceholder.Controls.Add(userControl);
                }
        }
        private void LoadErrorPage()
        {
            // Mostrar un mensaje de error directamente en la página
            string errorHtml = @"
               <!DOCTYPE html>
                    <html>
                        <head>
                            <title>Administración</title>
                           <uc:Links runat='server' />
                        </head>
                        <body>
                < div id='wrapper'>
                    <uc:Navbar runat='server' />
                    <uc:NavBar2 runat='server' />
                    <div id='page-wrapper'>
                        <div class='container'>
                            <div class='row'>
                                <div class='col-sm-12'>
                                    <div class='page-header'>
                                        <h1 class='animated lightSpeedIn'>Lo sentimos<small></small></h1>
                                        <h3 class='text-center'>La opción que ha seleccionado no se encuentra disponible</h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <uc:Footer runat='server' />
                                  <uc:Script2 runat='server' />
                             </body>
                         </html>";
            phContent.Controls.Add(new LiteralControl(errorHtml));
        }
        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;
            foreach (Control child in root.Controls)
            {
                Control foundControl = FindControlRecursive(child, id);
                if (foundControl != null)
                    return foundControl;
            }
            return null;
        }
        private string GetControlPath(string content)
        {
            string controlPath = string.Empty;
            switch (content)
            {    
                case "index": { controlPath = $"~/includes/{content}.ascx"; break; }
                case "admin": { controlPath = $"~/Inventario/admin/{content}-view.ascx"; break; }
                case "almacenista": { controlPath = $"~/Inventario/admin/admin-view.ascx"; break; }
                case "mecanico": { controlPath = $"~/Inventario/admin/admin-view.ascx"; break; }
                case "userEdit": { controlPath = $"~/Inventario/admin/{content}-view.ascx"; break; }
                case "brands": { controlPath = $"~/Inventario/admin/Marcas/{content}-view.ascx"; break; }
                case "models": { controlPath = $"~/Inventario/admin/ModeloVehiculos/{content}-view.ascx"; break; }
                case "types": { controlPath = $"~/Inventario/admin/TipoVehiculos/{content}-view.ascx"; break; }
                case "permissions": { controlPath = $"~/Inventario/admin/Configuracion/{content}-view.ascx"; break; }
                case "cars": { controlPath = $"~/Inventario/admin/Vehiculos/{content}-view.ascx"; break; }
                case "typeEdit": { controlPath = $"~/Inventario/admin/TipoVehiculos/{content}-view.ascx"; break; }
                case "modelEdit": { controlPath = $"~/Inventario/admin/ModeloVehiculos/{content}-view.ascx"; break; }
                case "brandsEdit": { controlPath = $"~/Inventario/admin/Marcas/{content}-view.ascx"; break; }
                case "carEdit": { controlPath = $"~/Inventario/admin/Vehiculos/{content}-view.ascx"; break; }
                default:
                    Response.Redirect("~/Inventario/process/logout.aspx");
                    break;
            }
            return controlPath;
        }
        private string GetControlSearch(string content)
        {
            string controlPath = string.Empty;
            switch (content)
            {
                case "searchUsers": { controlPath = $"~/Inventario/admin/{content}-view.ascx"; break; }
                case "searchBrands": { controlPath = $"~/Inventario/admin/Marcas/{content}-view.ascx"; break; }
                case "searchModels": { controlPath = $"~/Inventario/admin/ModeloVehiculos/{content}-view.ascx"; break; }
                case "searchTypes": { controlPath = $"~/Inventario/admin/TipoVehiculos/{content}-view.ascx"; break; }
                case "searchCars": { controlPath = $"~/Inventario/admin/Vehiculos/{content}-view.ascx"; break; }
                   default:
                    Response.Redirect("~/Inventario/process/logout.aspx");
                    break;
            }
            return controlPath;
        }
        private void ValidarCookieUrl(string content)
        {
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

                case "searchUsers":
                    {
                        url = "admin/" + content;
                        break;
                    }
                case "searchCars":
                    {
                        url = "admin/Vehiculos/" + content;
                        break;
                    }
                case "searchPermissions":
                    {
                        url = "admin/Configuracion/" + content;
                        break;
                    }
                case "admin":
                    {
                        url = "admin/" + content;
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