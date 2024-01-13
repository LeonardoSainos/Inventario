<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin-view.ascx.cs" Inherits="Inventario.Inventario.admin.WebUserControl1" %>
 <%@ Import Namespace="Inventario.Scripts" %>
<div id="contenido">
    <div class="container">
        <%Response.Write(alerta); %>
        <div class="row">
            <div class="col-sm-2">
                <img src="../Inventario/img/card_identy.png" alt="Animacion" class="img-responsive animated flipInY" />
            </div>
            <div class="col-sm-10">
                <p class="lead text-info">Bienvenido administrador, en esta página se muestran todos los <strong>Usuarios </strong> registrados en Inventario Alcomex, usted podrá eliminarlos si lo desea.</p>
            </div>
        </div>
    </div>
<br /><br />
<div class="container">
<div class='btn-group'>
    <button class='btn dropdown-toggle btn-warning' data-toggle='dropdown' value='Más'>
        Más
        <span class='caret'></span>
    </button>  <script src="https://code.jquery.com/jquery-2.1.0.min.js"></script>
    <ul class='dropdown-menu'>                                                        
        <li><span style='margin-left:22px' class='glyphicon glyphicon-user'></span> <button type="button" class="dropbtn btn btn-link" style='text-decoration:none;' data-toggle='modal' data-target='#modal1' >Nuevo usuario</button> </li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-trash'></span> <button  class='btn btn-link' form="acciones" style='text-decoration:none;' name="Eliminar">Eliminar</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-ban-circle'></span> <button class='btn btn-link' form="acciones" style='text-decoration:none;' name="Bloquear">Bloquear</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-refresh'></span> <button class='btn btn-link' form="acciones" style='text-decoration:none;' name="Desbloquear">Desbloquear</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-user'></span> <button class='btn btn-link' form="pdf" style='text-decoration:none;' name="Exportar">Exportar</button></li>
          <li ><a href='#' class='btn btn-link '> <span class='glyphicon glyphicon-log-in'></span><input  form="acciones" class='btn btn-link ' style='text-decoration:none;'  type="submit" value=" Resetear contraseña" name="Resetear"/> </a></li>  
    </ul>
</div> 
<div style="display:flex; float:right;">
 <input id="busqueda" style="width: 80%; float:left;" placeholder="Buscar administradores"   name="busqueda" class="form-control mr-sm-2 alin" type="text" />
<a id="mt" href="javascript:void()" style="float:right;" placeholder="Buscar" class="btn btn-warning" type="submit"><span class="glyphicon glyphicon-search"></span></a>
 <div class='btn-group' style="display:flex; float:left">
  <button class='btn dropdown-toggle btn-success' data-toggle='dropdown' value='Más'><span class='fa fa-reorder'></span></button>
      <ul class='dropdown-menu'>
        <li ><a  id="nombree" href='javascript:void()' class='btn btn-link ' type="submit" style='text-decoration:none;'>Nombre</a></li>  
        <li ><a id="correoo" href='javascript:void()' class='btn btn-link ' type="submit" style='text-decoration:none;'>Correo</a></li>  
        <li ><a  id="fechaa" href='javascript:void()' class='btn btn-link ' type="submit" style='text-decoration:none;'>Fecha</a></li>  
        <li ><a id="estatuss" href='javascript:void()' class='btn btn-link ' type="submit" style='text-decoration:none;'>Estatus</a></li>  
     </ul>
 </div>
</div>            
                                          <form id="pdf"   action="../TICKET/lib/users.php">
                                            <input type="hidden" name="Exportar" value="4046" />
                                           </form>
                                          <br/> <br/>
<%  
    %>
<%  %>
<div class="row">
  <div class="col-md-12 text-center">
   <ul class="nav nav-pills nav-justified">
     <li><a href="./admin.aspx?view=users"><i class="fa fa-users"></i>&nbsp;&nbsp;Mecanicos&nbsp;&nbsp;<span class="badge"> <%Response.Write(row3);%></span></a></li>
     <li><a href="./admin.aspx?view=admin"><i class="fa fa-male"></i>&nbsp;&nbsp;Administradores&nbsp;&nbsp;<span class="badge"> <% Response.Write(row1); %></span></a></li>
     <li><a href="./admin.aspx?view=tec"><i class="fa fa-male"></i>&nbsp;&nbsp;Almacenistas&nbsp;&nbsp;<span class="badge"><%Response.Write(row2); %></span></a></li>
   </ul>
   </div>
</div>
<br/>
<div class="row">
    <div class="col-md-12">
       <div class="table-responsive">
           <% %>
           <form id="acciones"  action="../TICKET/admin/acciones-view.php">                 
            <input type="hidden" name="nombre" value="<%= Session["nombre"] %>"/>
             <input type="hidden" name="rol"value="<%= Session["rol"] %>"/>
             <input type="hidden" name="id" value="<%= Session["id"] %>"/>
           </form>
<form runat="server">
<asp:GridView ID="tabla" OnPreRender="tabla_PreRender" runat="server" AutoGenerateColumns="False" class="table table-hover   table-bordered" Height="100%" AllowCustomPaging="True" AllowPaging="True" Width="100%" >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox name="Usuarios[]" ID="CheckBox1" runat="server" Text="" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="id_cliente" HeaderText="#" SortExpression="Id_cliente" />
        <asp:BoundField DataField="Fecha_creacion" HeaderText="Creado" SortExpression="Fecha_creacion" />
        <asp:BoundField DataField="nombre_completo" HeaderText="Nombre completo" SortExpression="nombre_completo" />
        <asp:BoundField DataField="nombre_usuario" HeaderText="Nombre de usuario" SortExpression="nombre_usuario" />
        <asp:BoundField DataField="email_cliente" HeaderText="Email" SortExpression="email_cliente" />
        <asp:BoundField DataField="Depa" HeaderText="Departamento" SortExpression="Depa" />
        <asp:BoundField DataField="Esta" HeaderText="Estatus" SortExpression="Esta" />
        <asp:BoundField DataField="celular" HeaderText="Teléfono" SortExpression="celular" />
        <asp:BoundField DataField="anydesk" HeaderText="Anydesk" SortExpression="anydesk" />
       <asp:TemplateField HeaderText="Opciones">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-sm btn-success"
                    NavigateUrl='<%# Eval("id_cliente", "admin.aspx?view=useredit&id={0}") %>'>
                    <i class="fa fa-pencil" aria-hidden="true"></i>
                </asp:HyperLink>
                 <button type="button" class="dropbtn btn btn-sm btn-danger" data-toggle="modal" data-target="#pregunta" onclick='document.getElementById("borrar_id").value = "<%# Eval("id_cliente") %>";'>
                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                </button>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate >
          <h2  class="text-center">No hay administradores registrados en el sistema</h2>
    </EmptyDataTemplate>
    <HeaderStyle BackColor="Black" ForeColor="White"  />
    <RowStyle />
    <SelectedRowStyle Font-Bold="True" ForeColor="Navy" />  
</asp:GridView>
</form>
                   </div>
     </div>
 </div>
<% if (numPagina >= 1) { %>
    <nav arial-label="Page navigation" class="text-center">
        <ul class="pagination">
            <%if (pagina == 1)
                { %>
               <li class="disabled">
                   <a aria-label="Previous">
                       <span aria-hidden="true">&laquo;</span>
                   </a>
               </li>
            <%}
              else {%>
              <li>
                  <a href="./admin.aspx?view=admin&pagina=<%Response.Write(pagina-1); %>" aria-label="Previous">
                      <span aria-hidden="true">&laquo</span>
                  </a>
              </li>
              <%}
                  for(int i=1; i<=numPagina; i++)
                  {
                      if (pagina == i){
                          Response.Write("<li class='active'><a href='./admin.aspx?view=admin&pagina=" + i + "'>" + i + "</a></li>");
                      }
                      else {
                          Response.Write("<li><a href='./admin.aspx?view=admin&pagina=" + i + "'>" + i + "</a></li>");
                      }
                  }
                  if(pagina == numPagina) {%>
                      <li class="disabled">
                           <a aria-label="Previous">
                               <span aria-hidden="true">&raquo;</span>
                            </a>
                      </li>
                     <%
                  }
                  else {%>
                    <li>
                        <a href="./admin.aspx?view=admin&pagina=<%Response.Write(pagina+1); %>" aria-label="Previous">
                            <span aria-hidden="true">&raquo;</span>
                        </a>
                    </li>
                  <%}
              %>
        </ul>
    </nav>
<%} %>
</div>     
    </div>
   <li><span style='margin-left:22px' class='glyphicon glyphicon-user'></span> <button type="button" class="dropbtn btn btn-link" style='text-decoration:none;' data-toggle='modal' data-target='#modal1' >Nuevo usuario</button> </li>
     
  <uc:DeleteUser runat="server" />
  <uc:InsertUser runat="server" />
     <!-- Modal para agregar usuario 
<div class="container">
                            <div class="modal" tabindex="-1" id="modal1" >
                                <div class="modal-dialog modal-xlg  modal-dialog-centered">
                                    <div class="modal-content">
                                    <div class="modal-header" style="background: black; text-align:center;">
                                        <button class="close" data-dismiss="modal">&times;</button>
                                          <h1 style="color: white;">Agregar  nuevo usuario</h1>
                                        </div>
                                          <div class="modal-body">
                                        </div>
                                        </div>
                                    </div>
                                </div>
                                 
                            </div> -->
 
      <script src="https://code.jquery.com/jquery-2.1.0.min.js"></script>
 

<script>
    $(document).ready(function () {
        $("#mt").click(BuscarUsuario);

        function BuscarUsuario() {
            var URL = "./admin.aspx?view=searchUsers&admin=" + $("#busqueda").val();
            $.get(URL, function (datos, estado) {
                $("#contenido").html(datos);
            });
        }

        $("#nombree").click(FiltroUsers);
        function FiltroUsers() {
            var URL = "./admin.aspx?view=filterUsers&admin=" + $("#nombre").val();
            $.get(URL, function (datos, estado) {
                $("#contenido").html(datos);
            });
        }

        $("#fechaa").click(FiltroFecha);
        function FiltroFecha() {
            var URL = "./admin.aspx?view=filterUsers&admin=" + $("#fecha").val();
            $.get(URL, function (datos, estado) {
                $("#contenido").html(datos);
            });
        }

        $("#correoo").click(FiltroCorreo);
        function FiltroCorreo() {
            var URL = "./admin.aspx?view=filterUsers&admin=" + $("#correo").val();
            $.get(URL, function (datos, estado) {
                $("#contenido").html(datos);
            });
        }

        $("#estatuss").click(FiltroEstatus);
        function FiltroEstatus() {
            var URL = "./admin.aspx?view=filterUsers&admin=" + $("#estatusss").val();
            $.get(URL, function (datos, estado) {
                $("#contenido").html(datos);
            });
        }
    });
</script>

