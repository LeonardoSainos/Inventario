<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="Inventario.includes.WebUserControl1" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web"%>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register Src="~/Inventario/inc/links.ascx" TagName="Links" TagPrefix="uc" %>
<%@ Register Src="~/Inventario/inc/navbar.ascx" TagName="NavBar" TagPrefix="uc" %>
<%@ Register Src="~/Inventario/inc/timezone.ascx" TagName="TimeZone" TagPrefix="uc" %>
<%@ Register Src="~/Inventario/inc/linksJS.ascx" TagName="Script2" TagPrefix ="uc" %>
<%@ Register Src="~/Inventario/inc/linksJS2.ascx" TagName="Script" TagPrefix="uc" %>

 
<!-- En el lugar donde deseas incluir el control -->
        <%
          // Guardar un valor en la sesión
                // Recuperar un valor de la sesión
                string nombre = Session["Nombre"] as string;
                string[] ViewDiferent = { "searchTicket", "filterTicket" };
                string[] WhiteList = { "index", "productos", "soporte", "ticket", "ticketcon", "registro", "configuracion", "ticketClient", "" };
              if (Request.QueryString["view"] != null)
              {
                        string content = Request.QueryString["view"];
                if (Request.QueryString["view"] != null && WhiteList.Contains(Request.QueryString["view"]) && System.IO.File.Exists(Server.MapPath("~/user/" + Request.QueryString["view"] + "-view.aspx")))
                {  
                %>
                <!DOCTYPE html>
                <html>
                <head>
                    <uc:Links runat="server"/>
                    <meta charset="UTF8" />
                    <title>Alcomex soporte técnico</title>
                    <link rel="icon" href="favicon.png">
              
                 </head>
                <body>
                    <div class="container">
                        <uc:Navbar runat="server"/>
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
                    <uc:Script2 runat="server" />
                </body>
                </html>
                <%
                        /* include "./user/" + Request.QueryString["view"] + "-view.aspx";
                         include "./inc/footer.php";*/
                    }
                    else if (Request.QueryString["view"] != null && ViewDiferent.Contains(Request.QueryString["view"]) && System.IO.File.Exists(Server.MapPath("~/user/" + Request.QueryString["view"] + "-view.aspx")))
                    {
                %>
                <!DOCTYPE html>
                <html>
                <head>
                    <uc:Links runat="server" />
                    <title>Alcomex soporte técnico</title>
                </head>
                <body>
                   <uc:Navbar runat="server"/>
                    <div class="container">
                    </div>
                      <uc:Script2 runat="server" />
                </body>
                </html>
                <%
                        /*  include "./user/" + Request.QueryString["view"] + "-view.aspx";*/
                    }
                    else
                    {
                %>
                <!DOCTYPE html>
                <html>
                <head>
                    <uc:Links runat="server"/>
                    <title>Alcomex soporte técnico</title>
                </head>
                <body>
                  <uc:Navbar runat="server"/>
                    <div class="container">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="page-header">
                                    <h1 class="animated lightSpeedIn">Panel técnico</h1>
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
                    <br />      
                      <uc:Script2 runat="server" />
                </body>
                </html>
                <%
                   }
              }
              else
              {
                            %>
                        <!DOCTYPE html>
                        <html>
                            <head>
                                <uc:Links runat="server"/>
                                 <meta charset="UTF8"/>
                                 <title>Alcomex soporte técnico</title>
                                <link rel="icon" href="favicon.png">
                            </head>
                            <body>   
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
                                  <uc:Script2 runat="server" />
                                </body>
                              </html> 
                                  <script>
                                    if('serviceWorker' in navigator){
                                        navigator.serviceWorker.register('./sw/sw.js')
                                        .then(
                                            function(registration){
                                                    console.log('Reg. satisfactorio del sw en el ámbito: ', registration.scope);
                                            }
                                        ).catch(
                                            function(err){
                                                   console.log('El SW no se registró', err);
                                            }
                                        );
                                    }
                                  </script>
                     <%      
                               /* include "./user/index-view.php";*/       
                            %>
       <%       } %>