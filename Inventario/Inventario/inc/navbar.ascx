<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="navbar.ascx.cs" Inherits="Inventario.Inventario.inc.navbar" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%
     string nombreCompleto = Session["nombre"] as string;

 
       nombreCompleto = (!string.IsNullOrEmpty(nombreCompleto)) ? nombreCompleto : 
                        (userCookie!= null && !string.IsNullOrEmpty(userCookie.Value)) ? userCookie.Value : 
                        string.Empty;
%>
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
            <% if((Session["rol"] != null || rolCookie!=null) && (Session["nombre"] != null || userCookie!=null)) { %>
            <ul class="nav navbar-nav navbar-right" >
                <li class="dropdown" id="main">
                    <a id="expanded" href="#"  class="dropdown-toggle nombre" data-toggle="dropdown">
                        <span class="glyphicon glyphicon-user"></span> &nbsp; <%= nombreCompleto %><b class="caret"></b>
                    </a>
                 <ul class="dropdown-menu">
    <% 
        string rolC = Convert.ToString(rolCookie.Value); 
        if (Session["rol"]?.ToString() != "99999" || rolC != null) {
            for (int a = 0; a < TotalModulos; a++) { 
    %>        <li class="dropdown notdisplay">
                    <a href="<%= RutaM[a] %>" class="<%=Class_M[a]%>" data-toggle="dropdown">
                        &nbsp;&nbsp;<i class="<%= IconM[a] %>" aria-hidden="true"></i>&nbsp;<%= NombreModulo[a] %><b class="caret"></b>
                    </a>
                    <% if (idSubModulo[a] != null) { %>
                        <ul class="dropdown-menu">
                            <% for (int b = 0; b < idSubModulo[a].Length; b++) { %>
                                <li class="dropdown notdisplay">
                                    <a href="<%= RutaSM[a][b] %>" class="<%=Class_SM[a][b] %>" data-toggle="dropdown">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="<%= IconSM[a][b] %>"></i>&nbsp;<%= NombreSubModulo[a][b] %>
                                    </a>
                                    <% if (idSubSubModulo[a][b] != null) { %>
                                        <ul class="dropdown-menu <%=ClasesCSS[b] %>">
                                            <% for (int c = 0; c < idSubSubModulo[a][b].Length; c++) { %>
                                                <li>
                                                    <a class="data" href="<%= RutaSSM[a][b][c] %>"  data-toggle="dropdown">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="<%= IconSSM[a][b][c] %>" aria-hidden="true"></i> <%= NombreSubSubModulo[a][b][c] %>
                                                    </a>
                                                </li>
                                            <% } %>
                                        </ul>
                                    <% } %>
                                </li>
                            <% } %>
                        </ul>
                    <% } %>
                </li>
    <%}
    %>           
                      
<% } %>
                        <li class="divider"></li>
                        <li>
                            <a href="./Inventario/process/logout.aspx"><i class="fa fa-power-off"></i>&nbsp;&nbsp;Cerrar sesión</a>
                        </li>
                    </ul>
            <% } %>
                    </li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li>
                    <a href="./index.aspx"><span class="glyphicon glyphicon-home"></span> &nbsp; Inicio</a>
                </li>
                <li> 
                    <a href="http://192.168.11.5:8888/TicketAlcomex/Ticket/index.php?view=ticket"><span class="glyphicon glyphicon-earphone"></span>&nbsp;&nbsp;Soporte técnico</a>
                </li>
                    <% if((Session["rol"] == null || Session["nombre"] == null) && (rolCookie== null || userCookie==null) ) { %>
                <li>
                    <a href="./index.php?view=registro"><i class="glyphicon glyphicon-user"></i>&nbsp;&nbsp;Registro</a>
                </li>
                <li>
                    <a href="#!" data-toggle="modal" data-target="#modalLog"><span class="glyphicon glyphicon-user"></span>&nbsp;&nbsp;Login</a>
                </li>
                    <% } %>
            </ul>
        </div>
    </div>
  </nav>
          <% if ((Session["rol"] == null || Session["nombre"] == null) && (rolCookie==null || userCookie==null)){ %>
                    <uc:Login runat="server" /><%}
          %>