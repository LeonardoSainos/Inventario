<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchUsers-view.ascx.cs" Inherits="Inventario.Inventario.admin.Actions.searchUsers_view" %>

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
<div class="btn-group">
    <button class="btn dropdown-toggle btn-warning" data-toggle="dropdown" value="Más">
        Más
        <span class="caret"></span>
    </button>   
 
    <ul class="dropdown-menu">                                                        
       <li><span style='margin-left:22px' class='glyphicon glyphicon-user'></span><input class="btn btn-link" style='text-decoration:none;' onclick="ActivarBoton('Nuevo');" value="Nuevo usuario" type="button"/></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-trash'></span> <button type="submit" class='btn btn-link'  onclick="ActivarBoton('Eliminar');" style='text-decoration:none;' name="Eliminar">Eliminar</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-ban-circle'></span><button type="button" class="btn btn-link" onclick="ActivarBoton('Bloquear');" id="btnExterno" style="text-decoration:none;" name="Bloquear">Bloquear</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-refresh'></span> <button type="button" class='btn btn-link'  onclick="ActivarBoton('Desbloquear');" style='text-decoration:none;' name="Desbloquear">Desbloquear</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-user'></span> <button type="button" class='btn btn-link' style='text-decoration:none;' name="ExportarPdf" onclick="ActivarBoton('Pdf');">Exportar PDF</button></li>
                <li><span style='margin-left:22px;' class='glyphicon glyphicon-user'></span> <button type="button" class='btn btn-link' style='text-decoration:none;' name="ExportarExcel" onclick="ActivarBoton('Excel');" >Exportar EXCEL</button></li>
          <li ><a href='#' class='btn btn-link'> <span class='glyphicon glyphicon-log-in'></span><input  onclick="ActivarBoton('Resetear');"  class='btn btn-link ' style='text-decoration:none;'  type="button" value=" Resetear contraseña" name="Resetear"/> </a></li>     
        </ul>
</div> 
       <input id="rol" type="hidden" value="<%=TipoRol %>" />
             

<div style="display:flex; float:right;">
 <input id="busqueda" style="width: 80%; float:left;" placeholder="Buscar administradores"  value="<%=Buscar %>"  name="busqueda" class="form-control mr-sm-2 alin" type="text" />
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
                                          
                                          <br/> <br/>
   
<div class="row">
  <div class="col-md-12 text-center">
   <ul class="nav nav-pills nav-justified">
     <li><a href="./admin.aspx?view=mecanico"><i class="fa fa-users"></i>&nbsp;&nbsp;Mecanicos&nbsp;&nbsp;<span class="badge"> <%Response.Write(row3);%></span></a></li>
     <li><a href="./admin.aspx?view=admin"><i class="fa fa-male"></i>&nbsp;&nbsp;Administradores&nbsp;&nbsp;<span class="badge"> <% Response.Write(row1); %></span></a></li>
     <li><a href="./admin.aspx?view=almacenista"><i class="fa fa-male"></i>&nbsp;&nbsp;Almacenistas&nbsp;&nbsp;<span class="badge"><%Response.Write(row2); %></span></a></li>
   </ul>
   </div>
</div>
<br/>
     
<div class="row">
    <div class="col-md-12">
       <div class="table-responsive">
          
        <% if (totalEncontrados > 0 && (Buscar!=null && Buscar !="")) { %>
    <div class="col-sm-10">
        <p class="lead text-info"><strong><%= totalEncontrados %></strong> registros coinciden con tu búsqueda</p>
    </div>
<% } %>
          
         
<form runat="server" id="mostrar">
<asp:GridView ID="tabla"    OnPreRender="tabla_PreRender" runat="server" AutoGenerateColumns="False" class="table table-hover   table-bordered" Height="100%" AllowCustomPaging="True" AllowPaging="True" Width="100%"   >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
       <asp:CheckBox ID="chkUsuario" runat="server"  OnCheckedChanged="chkUsuario_CheckedChanged"  />
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
                    NavigateUrl='<%# Eval("id_cliente", "~/admin.aspx?view=userEdit&id={0}") %>'>
                    <i class="fa fa-pencil" aria-hidden="true"></i>
                </asp:HyperLink>
                 <button type="button" class="dropbtn btn btn-sm btn-danger" data-toggle="modal" data-target="#pregunta" onclick='document.getElementById("borrar_id").value = "<%# Eval("id_cliente") %>";'>
                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                </button>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate >
          <h2  class="text-center">No hay administradores que coincidan con tu busqueda</h2>
    </EmptyDataTemplate>
    <HeaderStyle BackColor="Black" ForeColor="White"  />
    <RowStyle />
    <SelectedRowStyle Font-Bold="True" ForeColor="Navy" />  
</asp:GridView>
    <asp:Button ID="btnBloquear" runat="server" form="mostrar" Style="display:none;" OnClick="btnBloquear_Click" />
     <asp:Button ID="btnDesbloquear" runat="server"  form="mostrar" Style="display:none;" OnClick="btnDesbloquear_Click" />
     <asp:Button ID="btnResetear" runat="server"  form="mostrar" Style="display:none;" OnClick="btnResetear_Click" />
      <asp:Button ID="btnEliminar" runat="server"  form="mostrar" Style="display:none;" OnClick="btnEliminar_Click" />
       <asp:Button ID="btnPdf" runat="server"  form="mostrar" Style="display:none;" Onclick="btnPdf_Click" />
       <asp:Button ID="btnExcel" runat="server"  form="mostrar" Style="display:none;" OnClick="btnExcel_Click"  />
        <button id="btnNuevo" type="button" style="display:none;" data-toggle='modal' data-target='#modal1'></button>
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
  <uc:DeleteUser runat="server" />
  <uc:InsertUser runat="server" />
<script>
   
    function ActivarBoton(opcion) {
        // Activar el botón dentro del formulario
        switch (opcion) {
            case "Bloquear": {
                var boton = document.getElementById("<%= btnBloquear.ClientID %>");
                if (boton) {
                    boton.click();
                }
                break;
            }
            case "Desbloquear": {
                var boton = document.getElementById("<%= btnDesbloquear.ClientID %>");
                if (boton) {
                    boton.click();
                }
                break;
            }
            case "Resetear": {
                var boton = document.getElementById("<%= btnResetear.ClientID %>");
                if (boton) {
                    boton.click();
                }
                break;
            }
            case "Eliminar": {
                var boton = document.getElementById("<%= btnEliminar.ClientID %>");
                if (boton) {
                    boton.click();
                }
                break;
            }
            case "Nuevo": {
                var boton = document.getElementById("btnNuevo");
                if (boton) {
                    boton.click();
                }
                break;
            }
            case "Pdf": {
                var boton = document.getElementById("<%= btnPdf.ClientID %>");
                  if (boton) {
                    boton.click();
                }
                break;
            }
            case "Excel": {
                 var boton = document.getElementById("<%= btnExcel.ClientID %>");
                 if (boton) {
                     boton.click();
                 }
                 break;
             }
             default: "Ninguno";
         }
     }
    $(document).ready(function () {
        $("#mt").click(BuscarUsuario);
        $("#nombree").click(FiltroUsers);
        $("#fechaa").click(FiltroFecha);
        $("#correoo").click(FiltroCorreo);
        $("#estatuss").click(FiltroEstatus);

    });
</script>
