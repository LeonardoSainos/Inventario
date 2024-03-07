<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin.ascx.cs" Inherits="Inventario.includes.admin" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web"%>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%  int idObject= Convert.ToInt32(Session["rol"]);
    if (idObject!=4046)
    {
            HttpContext.Current.Response.Redirect("~/Inventario/process/logout.aspx");
    }   
    string nombre = Session["Nombre"] as string;
    string completoName = Session["nombre_completo"] as string;
    string[] ViewDiferent = {"searchUsers","searchDepa","searchTicket","searchBrands","searchModels","searchTypes","searchCars"};
    string[] WhiteList = {"ticketadmin" ,"interno","ticketedit","mecanico","admin","config","almacenista","depa","depaedit","userEdit","acciones", "brands","brandsEdit","models","modelsEdit","types","typeEdit","cars","carEdit"};
    if (Request.QueryString["view"] != null && Session["id"] != null)
    {
        string content = Request.QueryString["view"];
        string result = content.Substring(content.LastIndexOf('/') + 1);
        if ((content != null && WhiteList.Contains(result)) && System.IO.File.Exists(Server.MapPath("~/Inventario/admin/" + content + "-view.ascx"))) {
               %>
                 <!DOCTYPE html>
                                <html>
                                <head>
                                    <uc:Links runat="server"/>
                                   
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
                                        <% switch (result) {
                                                        case "index": { %>  <uc:IndexView runat="server" /> <%
                                                            break;
                                                                    }
                                                        case "admin": { %>  <uc:AdminView TipoRol="admin" runat="server" />  <%      
                                                             break;
                                                             }
                                                        case "almacenista": { %>  <uc:AdminView  TipoRol="almacenista" runat="server" />  <%      
                                                             break;
                                                             }
                                                        case "mecanico": { %><uc:AdminView TipoRol="mecanico" runat="server" />  <%      
                                                              break;  }
                                                        case "userEdit": { %> <uc:UserEditAdmin runat="server" /> <% 
                                                            break; }
                                                        case "brands" : { %> <uc:AdminBrandView runat="server" /> <%
                                                            break; }
                                                        case "models": { %> <uc:AdminModelView runat="server" /> <%
                                                            break; }
                                                        case "types": { %> <uc:AdminTypeView runat="server" /> <% 
                                                            break; }
                                                        case "cars": { %> <uc:AdminCarView runat="server" /> <% 
                                                               break;      }
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
                       else if ((content != null && ViewDiferent.Contains(result)) &&  System.IO.File.Exists(Server.MapPath("~/Inventario/admin/" + content + "-view.ascx"))){
                       %>
                     <!DOCTYPE html>
                                <html>
                                <head>
                                    <title>Administración</title>
                                 </head>
                                <body>
                                    <div class="container">
                                           <% 
                                               switch(result){
                                                  
                                                     case "searchUsers": { %> <uc:SearchUsers  runat="server" />  <%
                                                        break;
                                                     }
                                                     case "searchBrands": { %> <uc:SearchBrandsAdmin runat="server" /> <% 
                                                        break;
                                                     }
                                                     case "searchModels": { %> <uc:SearchModelsAdmin runat="server" /> <% 
                                                       break;
                                                     }
                                                     case "searchTypes": { %> <uc:SearchTypesAdmin runat="server" /> <%
                                                       break;
                                                     }
                                                     case "searchCars": { %> <uc:SearchCarsAdmin runat="server" /> <%
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
%>                                                