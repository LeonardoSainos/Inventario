<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin.ascx.cs" Inherits="Inventario.includes.admin" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web"%>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%  object idObject = Session["rol"];
    if (idObject != null && idObject is int)
    {
        int id = (int)idObject;
        if (id != 4046)
        {
            HttpContext.Current.Response.Redirect("~/Inventario/process/logout.aspx");
        }
    }
    string nombre = Session["Nombre"] as string;
    string completoName = Session["nombre_completo"] as string;
    string[] ViewDiferent = {"searchUsers","searchDepa","searchTicket","filterDepa","filterTicket","filterUsers" };
    string[] WhiteList = {"ticketadmin" ,"interno","ticketedit","users","admin","config","tec","depa","depaedit","useredit","acciones" };
            if (Request.QueryString["view"]!= null && Session["id"]!= null)
            {
                 string content = Request.QueryString["view"];
               if ((Request.QueryString["view"] != null && WhiteList.Contains(Request.QueryString["view"])) &&   System.IO.File.Exists(Server.MapPath("~/Inventario/admin/" + content + "-view.ascx"))){
               %>
                 <!DOCTYPE html>
                                <html>
                                <head>
                                    <uc:Links runat="server"/>
                                    <meta charset="UTF8" />
                                    <title>Administración</title>
                                    <link rel="icon" href="favicon.png">              
                                 </head>
                                <body>
                                     <div id="wrapper">
                                      <uc:Navbar runat="server"/> 
                                        <uc:NavBar2 runat="server" />
                                    <div id="page-wrapper">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="page-header">
                                                    <h1 class="animated lightSpeedIn">Panel Administrativo<small></small></h1>
                                                    <span class="label label-warning">Transporte de logística S.A de C.V</span>
                                                    <p class="pull-right text-primary">
                                                        <strong>
                                                 <uc:TimeZone runat="server"/>
                                                        </strong>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>                          
                                    </div>
                                        <div class="container">
                                        <% switch(content){
                                                case "index": { %>  <uc:IndexView runat="server" /> <%
                                                       break;
                                                }
                                                case "admin": { %>  <uc:AdminView runat="server" />  <%      
                                                       break;
                                                }
                                                default:  %> <script> window.history.go(-1); </script> <% 
                                                       break;
                                                }
                                            %>
                                            </div>
                              </div>
                                         </div>
                                  <uc:Footer runat="server" />
                                  <uc:Script2 runat="server" />
                                </body>
                                </html>
               <%
                }
                else if((Request.QueryString["view"]!=null && ViewDiferent.Contains(Request.QueryString["view"])) &&  System.IO.File.Exists(Server.MapPath("~/Inventario/admin/" + content + "-view.ascx"))){
                       %>
                     <!DOCTYPE html>
                                <html>
                                <head>
                                    <meta charset="UTF8" />
                                    <title>Administración</title>
                                 </head>
                                <body>
                                  
                                    <div class="container">
                                           <% switch(content){
                                                case "index": { %>  <uc:IndexView runat="server" /> <%
                                                        break;
                                                }
                                                default:
                                                %> <script> window.history.go(-1);</script> <% 
                                                        break;
                                                }
                                            %>
                                    </div>
                                    </body>
                                    </html>
                <%
                }
            else { %>
           <!DOCTYPE html>
                    <html>
                        <head>
                            <title>Administración</title>
                           <uc:Links runat="server" />
                        </head>
                        <body>   
                            <div id="wrapper">
                            <uc:Navbar runat="server" />
                                <uc:Navbar2 runat="server" />
                                <div id="page-wrapper">
                            <div class="container">
                            <div class="row">
                                <div class="col-sm-12">
                                              <div class="page-header">
                                                    <h1 class="animated lightSpeedIn">Alcomex <small>México</small></h1>
                                                    <span class="label label-warning">Transporte de logística S.A de C.V</span>
                                                    <p class="pull-right text-primary">
                                                        <strong>
                                                 <uc:TimeZone runat="server"/>
                                                        </strong>
                                                    </p>
                                                </div>
                                </div>
                                <h1 class="text-center">Lo sentimos,la opción que ha seleccionado no se encuentra disponible</h1>
                            </div>
                            </div>
                        </div>
                    </div>
                                  <uc:Footer runat="server" />
                                  <uc:Script2 runat="server" />
                            </body>
                        </html>
         <% }
    }
%>                                     <script>
                                         $(document).ready(function () {
                                             $("#input_user").keyup(function () {
                                                 $.ajax({
                                                     url: "./process/val_admin.php?id=" + $(this).val(),
                                                     success: function (data) {
                                                         $("#com_form").html(data);
                                                     }
                                                 });
                                             });
                                             $("#input_user2").keyup(function () {
                                                 $.ajax({
                                                     url: "./process/val_admin.aspx?id=" + $(this).val(),
                                                     success: function (data) {
                                                         $("#com_form2").html(data);
                                                     }
                                                 });
                                             });
                                         });
                                    </script>                    