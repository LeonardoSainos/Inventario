
<%@ Page Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register Src="~/includes/WebUserControl1.ascx" TagName="Index" TagPrefix="uc" %> 
<%@ Register Src="~/Inventario/mecanico/index-view.ascx" TagName="IndexView" TagPrefix="uc" %>
<%@ Register Src="~/Inventario/inc/footer.ascx" TagName="Footer" TagPrefix="uc" %>
<uc:Index runat="server" />
<uc:IndexView runat="server" />
<uc:Footer runat="server" />
