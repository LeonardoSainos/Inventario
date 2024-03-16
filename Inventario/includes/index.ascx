<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="index.ascx.cs" Inherits="Inventario.includes.WebUserControl1" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web"%>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<% /* Links, NavBar,TimeZone,Script2*/ %> 
        <% 
          

            if (content != null || (Session["id"]!= null || userIdCookie!=null )){
            

                if (content != null && WhiteList.Contains(content) &&   System.IO.File.Exists(Server.MapPath("~/Inventario/mecanico/" + content + "-view.ascx"))){%>
                <!DOCTYPE html>
                <html>
                <head>
                    <uc:Links runat="server"/>
    
                    <title>Alcomex soporte técnico</title>
                    <link rel="icon" href="favicon.png">              
                 </head>
                <body>
                     <div id="wrapper">
                      <uc:Navbar runat="server"/> 
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
                        </div>                          
                    </div>
                        <div class="container">
                               <% switch(content){
                                case "index": {

                                        %>  <uc:IndexView runat="server" />
                                       <%
                                        break;
                                }
                                default:
                                              %> <script> window.history.go(-1);</script> <% 
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
                        /* include "./user/" + Request.QueryString["view"] + "-view.aspx";
                         include "./inc/footer.php";*/
                    }
               else if (content!= null && ViewDiferent.Contains(content) && System.IO.File.Exists(Server.MapPath("~/Inventario/mecanico/" + content + "-view.ascx"))){                %>
                <!DOCTYPE html>
                <html>
                <head>
                    <uc:Links runat="server" />
                    <title>Alcomex soporte técnico</title>
                </head>
                <body>
                     <div id="wrapper">
                   <uc:Navbar runat="server"/>
                            <uc:Navbar2 runat="server" />
                      <div id="page-wrapper">
                    <div class="container">
                         <uc:IndexView runat="server"/> 
                    </div>
                          </div>
                         </div>
                      <uc:Footer runat="server" />
                              
                </body>
                </html>
                <%
                     //    include "./user/" + Request.QueryString["view"] + "-view.aspx";
                    }
                    else{ %>
                <!DOCTYPE html>
                <html>
                <head>
                    <uc:Links runat="server"/>
                    <title>Alcomex soporte técnico</title>
                </head>
                <body>
                                 <uc:Navbar runat="server"/>
                             <uc:Navbar2 runat="server" />
                        <div id="wrapper">
                      <div id="page-wrapper">
                    <div class="container">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="page-header">
                                    <h1 class="animated lightSpeedIn">Panel General</h1>
                                    <span class="label label-warning">Alcomex S.A de C.V</span>
                                    <p class="pull-right text-primary">
                                        <strong>
                                              <uc:TimeZone runat="server"/>
                                        </strong>
                                    </p>
                                </div>
                            </div>
                            <h1 class="text-center">Lo sentimos, la opción que ha seleccionado no se encuentra disponible</h1>
                        </div>
                    </div>
                          </div><br/>      
                 
                         <uc:Footer runat="server" />
                         <uc:Script2 runat="server" />
                </body>
                </html>
                <%
                   }
              }
              else{ %>
                        <!DOCTYPE html>
                        <html>
                            <head>
                                <uc:Links runat="server"/>
                               
                                 <title>Alcomex soporte técnico</title>
                                <link rel="icon" href="favicon.png">
                            </head>
                            <body>   
                                <% if(Session["id"]!=null || userIdCookie!=null){
                                        %>
                             <div id="wrapper">
                              <uc:Navbar runat="server"/>
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
                                  </div>
                                </div>
                                            <div class="container">
                                           <uc:IndexView runat="server" />
                                                </div>
                                     <uc:Footer runat="server" />
                                  <uc:Script2 runat="server" />
                 
                                 </div>
                                 </div>
                                <% }
                                    else{%>
                              <uc:Navbar runat="server"/>
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
                                  </div>
                                </div>
                                            <div class="container">
                                           <uc:IndexView runat="server" />
                                                </div>
                                <uc:Footer runat="server" />
                                  <uc:Script2 runat="server" />

                                <%} %>
                                </body>
                              </html> 
                            <% } %>