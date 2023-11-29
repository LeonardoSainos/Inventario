<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="navbar.ascx.cs" Inherits="Inventario.Inventario.inc.navbar" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<% if(Request.Form["nombre_login"] != null && Request.Form["contrasena_login"] != null) { %>
    <%@ Import Namespace="System.Web" %>

    <%// Server.Execute("~/process/login.aspx"); %><% } %> 


<style>

  @media (min-width: 768px) {
    .notdisplay {
        display: none;
    }
}

</style>
<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <div class="container-fluid">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span> 
            </button>
       <a class="navbar-brand" href="#">
    <img class="img-responsive" src="../Inventario/img/Transp_ALCOMEX.png" alt="logo" width="50" height="50" />&nbsp;&nbsp; Alcomex
</a>
        </div>
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1"  >
            <% if(Session["rol"] != null && Session["nombre"] != null) { %>
            <ul class="nav navbar-nav navbar-right" >
                <li class="dropdown"  >
                    <a id="expanded" href="#"  class="dropdown-toggle principal" data-toggle="dropdown">
                        <span class="glyphicon glyphicon-user"></span> &nbsp; <%= Session["nombre"] %><b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <!-- usuarios -->
                        <% if(Session["rol"].ToString() == "2736") { %>
                        <li>
                            <a href="./index.php?view=configuracion"><i class="fa fa-cogs"></i>&nbsp;&nbsp;Configuración</a>
                        </li> 
                        <li>
                            <a href="./index.php?view=ticketClient"><span class="glyphicon glyphicon-envelope"></span> &nbsp; Tus Tickets</a>
                        </li>
                            <% } %>
                        <!-- tecnico -->
                            <% if(Session["rol"].ToString() == "7845") { %>
                        <li>
                            <a href="tecni.php?view=ticketTecni"><span class="glyphicon glyphicon-envelope"></span> &nbsp; Tus Tickets</a>
                        </li>
                        <li>
                            <a href="tecni.php?view=users"><span class="glyphicon glyphicon-user"></span> &nbsp;Usuarios</a>
                        </li>
                        <li>
                            <a href="tecni.php?view=config"><i class="fa fa-cogs"></i> &nbsp; Configuración</a>
                        </li>
                            <% } %>
                         <!-- admins -->
                    <% if(Session["rol"].ToString() == "4046") { %>
                 
                                    <!-- Configuracion  -->
                                    <li class="dropdown notdisplay" >
                                        <a href="#" class="dropdown-toggle configuracion-link" data-toggle="dropdown">
                                            &nbsp;&nbsp;<i class="fa fa-cog" aria-hidden="true"></i>&nbsp;Configuración<b class="caret"></b>
                                        </a>
                                        <ul class="dropdown-menu">
                                            <li>
                                                <a class="data" data-toggle="dropdown" href="ruta_a_configuracion">
                                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-cogs"></i>&nbsp;Configuración
                                                </a>
                                            </li>
                                            <li>
                                                <a class="data" data-toggle="dropdown" href="tecni.php?view=ticketTecni">
                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="glyphicon glyphicon-envelope"></span>&nbsp;Tus Tickets
                                                </a>
                                            </li>
                                        </ul>
                                    </li>




                                     <!--Inventario -->
                                    <li  class="dropdown notdisplay">
                                        <a href="#" class="dropdown-toggle configuracion-link" data-toggle="dropdown">
                                           &nbsp;&nbsp;<i class="fa fa-database" aria-hidden="true"></i>&nbsp;Inventario <b class="caret"></b>
                                        </a>
                                        <ul class="dropdown-menu">
                                                   <li class="dropdown notdisplay">
                                                    <a id="otro" href="#" class="dropdown-toggle configuracion-link" data-toggle="dropdown">
                                                        &nbsp;&nbsp;<i class="fa fa-database" aria-hidden="true"></i>&nbsp;Inventario general <b class="caret"></b>
                                                    </a>
                                                    <ul class="dropdown-menu" id="inventarioSubMenu">
                                                        <li><a class="data" data-toggle="dropdown" href="ruta_a_configuracion">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-arrows-h" aria-hidden="true"></i>Tipo de entrada </a></li>
                                                        <li><a class="data" data-toggle="dropdown" href="ruta_a_configuracion">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-arrow-left" aria-hidden="true"></i>Entrada de material</a></li>
                                                        <li><a class="data" data-toggle="dropdown" href="ruta_a_configuracion">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-arrow-right" aria-hidden="true"></i>Salida de material</a></li>
                                                    </ul>
                                                  </li>

                                                <script>
                                                    $(document).ready(function () {
                                                        $("#inventarioSubMenu").hide(); // Oculta el submenú inicialmente

                                                        $("#otro").click(function (e) {
                                                            e.preventDefault();
                                                            $("#inventarioSubMenu").toggle(); // Muestra u oculta el submenú al hacer clic
                                                        });
                                                    });
                                                </script>
                                        </ul>
                                    </li>




                                   <!-- Material -->
                                    <li  class="dropdown notdisplay">
                                      <a href="#" class="dropdown-toggle configuracion-link" data-toggle="dropdown">
                                             &nbsp;&nbsp;&nbsp;&nbsp;<span class="glyphicon glyphicon-user"></span>&nbsp;Material<b class="caret"></b>
                                       </a>
                                    </li>
                                   <!-- Familia de material -->
                                    <li  class="dropdown notdisplay">
                                          <a href="#" class="dropdown-toggle configuracion-link" data-toggle="dropdown">
                                            &nbsp;&nbsp;<span class="glyphicon glyphicon-user"></span>&nbsp;Familia<b class="caret"></b>
                                        </a>
                                    </li>
                                   <!--  Usuarios -->
                                    <li  class="dropdown notdisplay">
                                          <a href="#" class="dropdown-toggle configuracion-link" data-toggle="dropdown">
                                             &nbsp;&nbsp;<span class="glyphicon glyphicon-user"></span>&nbsp;Usuarios<b class="caret"></b>
                                        </a>
                                    </li>
                                    <!--  Proveedores -->
                                     <li  class="dropdown notdisplay">
                                           <a href="#" class="dropdown-toggle configuracion-link" data-toggle="dropdown">
                                             &nbsp;&nbsp;<span class="glyphicon glyphicon-user"></span>&nbsp;Proveedores<b class="caret"></b>
                                        </a>
                                     </li>
                                     <!-- Vehiculos -->
                                     <li  class="dropdown notdisplay">
                                           <a href="#" class="dropdown-toggle configuracion-link" data-toggle="dropdown">
                                             &nbsp;&nbsp;<span class="glyphicon glyphicon-user"></span>&nbsp;Vehiculos<b class="caret"></b>
                                        </a>
                                     </li>


                                
                                 

    

 
</li>

<% } %>
                        <li class="divider"></li>
                        <li>
                            <a href="./Inventario/process/logout.aspx"><i class="fa fa-power-off"></i>&nbsp;&nbsp;Cerrar sesión</a>
                        </li>
                    </ul>
                 </li>
            </ul>
           
            <% } %>
            <ul class="nav navbar-nav navbar-right">
                <li>
                    <a href="./index.aspx"><span class="glyphicon glyphicon-home"></span> &nbsp; Inicio</a>
                </li>
               
                <li> 
                    <a href="http://192.168.11.5:8888/TicketAlcomex/Ticket/index.php?view=ticket"><span class="glyphicon glyphicon-earphone"></span>&nbsp;&nbsp;Soporte técnico</a>
                </li>
                    <% if(Session["rol"] == null || Session["nombre"] == null) { %>
                <li>
                    <a href="./index.php?view=registro"><i class="glyphicon glyphicon-user"></i>&nbsp;&nbsp;Registro</a>
                </li>
                <li>
                    <a href="#!" data-toggle="modal" data-target="#modalLog"><span class="glyphicon glyphicon-user"></span>&nbsp;&nbsp;Login</a>
                </li>
                    <% } %>
            </ul>
            <form   class="navbar-form navbar-right hidden-xs" role="search" >
                <div class="form-group">
                 
                </div>

                <button type="submit" class="btn btn-warning">Buscar</button>
                <% if(Request.Form["buscar"] != null) {
                    string buscar = Request.Form["buscar"].ToString();
                    switch(buscar.ToLower()) {
                        case "ticket":
                            Response.Write("<script> window.open(\"/http://192.168.11.5:8888/TicketAlcomex/Ticket/index.php?view=ticket\");</script>");
                            break;
                        case "nuevo":
                            Response.Write("<script> window.open(\"/TicketAlcomex/TICKET/index.php?view=ticket\");</script>");
                            break;
                    }
                } %>
            </form>
        </div>
    </div>
  </nav>
            <!-- /. PAGE WRAPPER  -->
<div class="modal fade" tabindex="-1" role="dialog" id="modalLog" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title text-center text-primary" id="myModalLabel">Inventario Alcomex</h4>
            </div>
            <form  runat="server" style="margin: 20px;">
                <div class="form-group">
                    <label><span class="glyphicon glyphicon-user"></span>&nbsp;Usuario</label>
                    <asp:TextBox class="form-control" name="nombre_login" runat="server" placeholder="Escribe tu nombre de usuario o correo electrónico" required="" ID="txtUsuario" ></asp:TextBox>
                </div>
                <div class="form-group">
                    <label><span class="glyphicon glyphicon-lock"></span>&nbsp;Contraseña</label>
                    <asp:TextBox runat="server" type="password" class="form-control" name="contrasena_login" placeholder="Escribe tu contraseña" required="" ID="txtPassword" ></asp:TextBox>
                </div>
                <p>¿Cómo iniciarás sesión?</p>
                   <asp:RadioButtonList   ID="rblLogin" runat="server" Height="20px" Width="94px">
                
                      <asp:ListItem  class="radio" Text="Mecanico" Value="1"  Selected="True"></asp:ListItem>                
                       <asp:ListItem  class="radio" Text="Almacenista" Value="2"></asp:ListItem>                                                         
                        <asp:ListItem class="radio" Text="Administrador" Value="3"></asp:ListItem> 
                   </asp:RadioButtonList>
                <div class="modal-footer">
                    <asp:button runat="server"   Text="Iniciar sesión" class="btn btn-warning btn-sm" OnClick="Iniciar"></asp:button>
                    <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancelar</button>
                </div>
            </form>
        </div>
    </div>
</div>

     <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
                 <!--  <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script> -->

                    <script>
                        $(document).ready(function () {
                            // Evento clic para abrir/cerrar el menú desplegable
                            $(".configuracion-link").click(function (e) {
                                e.preventDefault(); // Evita la acción predeterminada del enlace
                                var dropdownMenu = $(this).closest(".dropdown").find(".dropdown-menu");
                                dropdownMenu.toggleClass("show");
                            });
                            // Evento clic para cerrar el menú desplegable al hacer clic fuera de él
                          
                            $(".principal").click(function (e) {
                              e.preventDefault(); // Evita la acción predeterminada del enlace
                            //    var dropdownMenu = $(this).closest(".dropdown").find(".dropdown-menu");
                            dropdownMenu.toggleClass("open");
                            });


                            // Evitar que se cierre el menú al hacer clic en un enlace interno
                            $(".dropdown-menu").on("click", function (e) {
                                e.stopPropagation();
                            });

                          
                        });</script>