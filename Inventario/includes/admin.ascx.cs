using Inventario.Inventario.lib;
using System;
using System.Web;
using System.Web.UI;

namespace Inventario.includes
{
    public partial class admin : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string content = Request.QueryString["view"];
                if (Session["id"] != null || Functions.ObtenerCookie("UserId") != null)
                {
                    content = content?.ToLower();

                    if (IsValidContent(content))
                    {
                        string controlPath = GetControlPath(content);
                        if (!string.IsNullOrEmpty(controlPath))
                        {
                            Control userControl = LoadControl(controlPath);
                            phContent.Controls.Add(userControl);
                        }
                        else
                        {
                            Response.Redirect("~/Inventario/process/logout.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Inventario/process/logout.aspx");
                }
            }
        }

        private bool IsValidContent(string content)
        {
            string[] whiteList = { "admin", "almacenista", "mecanico", "useredit", "brands", "models", "types", "permissions", "cars", "typeedit", "modeledit", "brandedit", "caredit" };
            string[] viewDiferent = { "searchusers", "searchdepa", "searchticket", "searchbrands", "searchmodels", "searchtypes", "searchcars", "searchpermissions" };

            return Array.Exists(whiteList, element => element == content) ||
                   Array.Exists(viewDiferent, element => element == content);
        }

        private string GetControlPath(string content)
        {
            string controlPath = string.Empty;

            switch (content)
            {
                case "admin":
                case "almacenista":
                case "mecanico":
                    controlPath = $"~/Inventario/admin/{content}-view.ascx";
                    break;
                case "useredit":
                case "brands":
                case "models":
                case "types":
                case "permissions":
                case "cars":
                case "typeedit":
                case "modeledit":
                case "brandedit":
                case "caredit":
                    controlPath = $"~/Inventario/admin/{content}-view.ascx";
                    break;
                case "searchusers":
                case "searchbrands":
                case "searchmodels":
                case "searchtypes":
                case "searchcars":
                case "searchpermissions":
                    controlPath = $"~/Inventario/admin/{content}.ascx";
                    break;
                default:
                    Response.Redirect("~/Inventario/process/logout.aspx");
                    break;
            }

            return controlPath;
        }
    }
}
