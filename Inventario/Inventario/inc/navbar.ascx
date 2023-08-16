 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="navbar.ascx.cs" Inherits="Inventario.Inventario.inc.navbar" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
 
<% if(Request.Form["nombre_login"] != null && Request.Form["contrasena_login"] != null) { %>
    <%@ Import Namespace="System.Web" %>

    <%// Server.Execute("~/process/login.aspx"); %><% } %>
<div id="wrapper"> 
<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <div class="container-fluid">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span> 
            </button>
            <a class="navbar-brand" href="../index.aspx">
                <img class="img-responsive" src="../Inventario/img/Transp_ALCOMEX.png" alt="logo" width="50" height="50" />&nbsp;&nbsp; Alcomex
            </a>
        </div>
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <% if(Session["rol"] != null && Session["nombre"] != null) { %>
            <ul class="nav navbar-nav navbar-right">
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <span class="glyphicon glyphicon-user"></span> &nbsp; <%= Session["nombre"] %><b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <!-- usuarios -->
                        <% if(Session["rol"].ToString() == "2736") { %>
                        <!--<li>
                            <a  href="#!"><span class="glyphicon glyphicon-comment"></span>&nbsp;&nbsp;Mensajes</a>
                        </li> -->
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
                        <li>
                            <a href="admin.php?view=ticketadmin"><span class="glyphicon glyphicon-envelope"></span> &nbsp; Administrar Tickets</a>
                        </li>
                        <li>
                            <a href="admin.php?view=users"><span class="glyphicon glyphicon-user"></span> &nbsp;Administrar Usuarios</a>
                        </li>
                        <li>
                            <a href="admin.php?view=depa"><span class="glyphicon glyphicon-briefcase"></span> &nbsp;Administrar Departamentos</a>
                        </li>
                        <li>
                            <a href="admin.php?view=config"><i class="fa fa-cogs"></i> &nbsp; Configuración</a>
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
                <!--   <li>
                    <a  class="hoverr" href="./index.php?view=productos"><span class="glyphicon glyphicon-shopping-cart"></span> &nbsp; Productos</a>
                </li> -->
                <li> 
                    <a href="./index.php?view=soporte"><span class="glyphicon glyphicon-earphone"></span>&nbsp;&nbsp;Soporte técnico</a>
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
                            Response.Write("<script> window.open(\"/TicketAlcomex/TICKET/index.aspx?view=soporte\");</script>");
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
</div>

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
