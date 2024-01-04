﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin-view.ascx.cs" Inherits="Inventario.Inventario.admin.WebUserControl1" %>
 <%@ Import Namespace="Inventario.Scripts" %>

<% MySql AdminView = new MySql(); %>
<div id="contenido">
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
                <img src="../img/card_identy.png" alt="Animacion" class="img-responsive animated flipInY" />
            </div>
            <div class="col-sm-10">
                <p class="lead text-info">Bienvenido administrador, en esta página se muestran todos los <strong>Usuarios </strong> registrados en soporte técnico Alcomex, usted podrá eliminarlos si lo desea.</p>
            </div>
        </div>
    </div>
</div>
<br /><br />
<div class='btn-group'>
    <button class='btn dropdown-toggle btn-warning' data-toggle='dropdown' value='Más'>
        Más
        <span class='caret'></span>
    </button>
    <ul class='dropdown-menu'>
        <li><span style='margin-left:22px' class='glyphicon glyphicon-user'></span> <button   class="btn btn-link" style='text-decoration:none;' data-toggle='modal' data-target='#modal1'>Nuevo usuario</button> </li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-trash'></span> <button  class='btn btn-link' style='text-decoration:none;' name="Eliminar">Eliminar</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-ban-circle'></span> <button class='btn btn-link ' style='text-decoration:none;' name="Bloquear">Bloquear</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-refresh'></span> <button class='btn btn-link ' style='text-decoration:none;' name="Desbloquear">Desbloquear</button></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-user'></span> <button class='btn btn-link ' style='text-decoration:none;' name="Exportar">Exportar</button></li>
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


<%      string consulta = "SELECT COUNT(*) AS contador FROM mysql_ticket ...cliente WHERE id_rol = 4046", mens = "";
    Tuple<List<object[]>, int> resultado = AdminView.Consulta(ref mens, consulta);
    int row1 = 0;
    if (resultado.Item2 > 0)
    {
        // Accede directamente al valor del recuento
        row1 = Convert.ToInt32(resultado.Item1[0][0]);
    }

    consulta = "SELECT COUNT(*) AS contador FROM mysql_ticket ...cliente WHERE id_rol = 7845 ";
    Tuple<List<object[]>, int> resultado2 = AdminView.Consulta(ref mens, consulta);
    List<object[]> registros2 = resultado2.Item1;
    int row2 = 0;
    foreach (object[] registro in registros2) {
        row2 = (int)registro[0];
    }
    consulta = "SELECT COUNT(*) AS contador FROM mysql_ticket ...cliente WHERE id_rol = 2736";
    Tuple<List<object[]>, int> resultado3 = AdminView.Consulta(ref mens, consulta);
    List<object[]> registros3 = resultado3.Item1;
    int row3 = 0;
    foreach (object[] registro in registros3) {
        row3 = (int)registro[0];
    }
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
                    NavigateUrl='<%# Eval("id_cliente", "admin.aspx?view=useredit&id=1") %>'>
                    <i class="fa fa-pencil" aria-hidden="true"></i>
                </asp:HyperLink>
                <button type="button" class="dropbtn btn btn-sm btn-danger" data-toggle="modal" data-target="#pregunta"
                    onclick='<%# "AbrirModalPregunta(" + Eval("id_cliente") + "); return false;" %>'>
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
<div class="modal fade" id="pregunta" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
       <div class="modal-dialog" role="document">
           <div class="modal-content">
                <div style="text-align:center; background: #fb5d14; color:white;" class="modal-header">
                     <h3 class="modal-title" id="exampleModalLabel">¿Estás seguro de que deseas elminar al usuario (Todo lo que este relacionado a él se eliminara de forma permanente)?</h3>                                                           
                 </div>
            <div class="modal-body">                                              
       </div>
       <div style="align-items:center; justify-content:center;"class="modal-footer">
                    <center> 
                             <form id="formulario"     style="display: inline-block;">                  
                                   <input type="hidden" value="admin" name="view" />
                                    <input  type="hidden" name="id_dele"  id="borrar_id" />       
                                    <button   name="ide" type="submit"  class="btn btn-success">SI</button>
                                   <button type="button" class="btn btn-danger" data-dismiss="modal">CANCELAR</button>
                             </form>                        
                      </center>
                  </div>
             </div>
       </div>    
    </div>