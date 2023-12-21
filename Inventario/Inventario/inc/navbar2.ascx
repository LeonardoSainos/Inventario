<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="navbar2.ascx.cs" Inherits="Inventario.Inventario.inc.navbar2" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
 
<% if(Request.Form["nombre_login"] != null && Request.Form["contrasena_login"] != null) { %>
    <%@ Import Namespace="System.Web" %>

    <%// Server.Execute("~/process/login.aspx"); %><% }

  string completoName = Session["nombre_completo"] as string;
                                                       %> 

                                    
    <nav id="mobile-only" class="navbar-nav navbar-default navbar-side" role="navigation">
                                    <div class="collapse in sidebar-collapse" id="bd-example-navbar-collapse-1">

                                        <ul class="nav" id="main-menu">
                                            <li>
                                                <div class="user-img-div">
                                                    <img src="assets/img/user.png" class="img-thumbnail" />
                                                    <div class="inner-text">    
                                                  <% Response.Write(completoName);
%>         
                                                    </div>
                                                </div>
                                            </li>
                                            <%
                                            if (Session["rol"].ToString() == "4046") { %>
                                         
                                            <li class="dropdown">
                                                 <a href="#"><i class="fa fa-cog"  aria-hidden="true"> </i> <span class="fa arrow" ></span> Configuración </a>
                                            </li>



                                            <li class="dropdown">
                                                <a href="#"><i class="fa fa-database" aria-hidden="true"></i> <span class="fa arrow"></span>Inventario</a>
                                                 <ul class="nav nav-second-level">
                                                     <li><a href="social.html"><i class="fa fa-wpforms" aria-hidden="true"></i>  <span class="fa arrow"></span> Inventario General</a> 
                                                         <ul class="nav nav-second-level">
                                                              <li><a href="#"><i class="fa fa-arrows-h" aria-hidden="true"></i>Tipo de entrada </a></li>
                                                             <li><a href="#"><i class="fa fa-arrow-left" aria-hidden="true"></i>Entrada de material</a></li>
                                                             <li><a href="#"><i class="fa fa-arrow-right" aria-hidden="true"></i>Salida de material</a></li>
                                                         </ul>
                                                     </li>
                                                     <li><a href="social.html"><i class="fa fa-wpforms" aria-hidden="true"></i> <span class="fa arrow"></span> B2 Taller</a>
                                                         <ul class="nav nav-second-level">
                                                             <li><a href="#"><i class="fa fa-arrow-left" aria-hidden="true"></i>Entrada de material</a></li>
                                                               <li><a href="#"><i class="fa fa-arrow-right" aria-hidden="true"></i>Salida de material </a></li>
                                                         </ul>
                                                     </li>
                                                      <li><a href="social.html"><i class="fa fa-wpforms" aria-hidden="true"></i> <span class="fa arrow"></span> B3 Encierro</a>
                                                         <ul class="nav nav-second-level">
                                                             <li><a href="#"><i class="fa fa-arrow-left" aria-hidden="true"></i>Entrada de material</a></li>
                                                               <li><a href="#"><i class="fa fa-arrow-right" aria-hidden="true"></i>Salida de material </a></li>
                                                         </ul>
                                                     </li>
                                                 </ul>

                                            </li>
                                             <li class="dropdown">
                                                 <a href="#"><i class="fa fa-wrench"  aria-hidden="true"> </i> <span class="fa arrow" ></span> Material  </a>
                                                 <ul class="nav nav-second-level">
                                                     <li><a href="#"><i class="fa fa-arrows-alt" aria-hidden="true"></i>Unidades de medida</a></li>
                                                       <li><a href="#"><i class="fa fa-archive" aria-hidden="true"></i>Productos</a></li>
                                                 </ul>
                                            </li>
                                            <li>
                                                 <a href="#"><i class="fa fa-tags"  aria-hidden="true"> </i> <span class="fa arrow" ></span> Familia de material </a>
                                            </li>
                                            <li>
                                                 <a href="#"><i class="fa fa-users" aria-hidden="true"></i> <span class="fa arrow" ></span> Usuarios </a>
                                            </li>
                                            <li>
                                                 <a href="#"><i class="fa fa-handshake-o" aria-hidden="true"></i> <span class="fa arrow" ></span> Provedores </a>
                                            </li>
                                            <li>
                                                 <a href="#"><i class="fa fa-bus" aria-hidden="true"></i>  <span class="fa arrow" ></span> Vehiculos </a>
                                                 <ul class="nav nav-second-level">
                                                     <li><a href="#"><i class="fa fa-globe" aria-hidden="true"></i>Todos los vehiculos</a></li>
                                                     <li><a href="#"><i class="fa fa-car" aria-hidden="true"></i>Modelo de vehiculos</a></li>
                                                     <li><a href="#"><i class="fa fa-taxi" aria-hidden="true"></i>Tipo de vehiculos</a></li>
                                                     <li><a href="#"><i class="fa fa-ambulance" aria-hidden="true"></i>Marcas</a></li>
                                                 </ul>
                                            </li>
                                         
                                            <%
                                             }
                                             else if (Session["rol"].ToString()=="7845"){
                                             
                                             }
                                            else if (Session["rol"].ToString() =="2736")
                                            {
                                            }   
                                             %>
                                            
                                           
                                             <li>
                                                <a href="#"><i class="fa fa-sitemap"></i>Tikets <span class="fa arrow"></span></a>
                                                 <ul class="nav nav-second-level">
                                                    <li>
                                                        <a href="invoice.html"><i class="fa fa-send"></i>Mis Tikest</a>
                                                    </li>
                                                    <li>
                                                        <a href="pricing.html"><i class="fa fa-plus "></i>Solicitar Tiket</a>
                                                    </li>
                                                    <li>
                                                        <a href="component.html"><i class="fa fa-square-o"></i>Gestionar Tikets</a>
                                                    </li>
                                                     <li>
                                                        <a href="social.html"><i class="fa fa-exclamation-circle "></i>Tikets Terminados</a>
                                                    </li>      
                                                     </ul>
                                            </li>      
                                        </ul>          
                                        </div>
                                </nav>

 
                                           