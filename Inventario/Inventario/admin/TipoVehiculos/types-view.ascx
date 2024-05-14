<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="types-view.ascx.cs" Inherits="Inventario.Inventario.admin.TipoVehiculos.tipos_view" %>
 <%@ Import Namespace="Inventario.Scripts" %>
<div id="contenido">
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
                <%Response.Write(alerta); %>
                <img src="../Inventario/img/type_car.png" alt="Animacion" class="img-responsive animated flipInY" />
            </div>
            <div class="col-sm-10">
               <p class="lead text-info">Bienvenido administrador, en esta página se muestran los tipos de Vehiculos que tienes disponibles. Puedes eliminar, actualizar o agregar más registros en esta página</p>
            </div>
        </div>
    </div>
    <br /> <br />
    <div class="container">
        <div class="btn-group">
            <button class="btn dropdown-toggle btn-warning" data-toggle="dropdown" value="Más">
               Más
                <span class="caret"></span>
            </button>
            <ul class="dropdown-menu">
                <li><span style='margin-left:22px' class='glyphicon glyphicon-road'></span><input class="btn btn-link" style="text-decoration:none;" onclick="ActivarBoton('Nuevo');" value="Nuevo tipo" type="button" /></li>
                <li><span style='margin-left:22px;' class='glyphicon glyphicon-trash'></span> <button type="submit" class='btn btn-link'  onclick="ActivarBoton('Eliminar');" style='text-decoration:none;' name="Eliminar">Eliminar</button></li>
                <li><span style='margin-left:22px;' class='glyphicon glyphicon-save'></span> <button type="button" class='btn btn-link' style='text-decoration:none;' name="ExportarPdf" onclick="ActivarBoton('Pdf');">Exportar PDF</button></li>
               </ul>
        </div>
        <div style="display:flex; float:right;">
              <input id="busqueda" style="width: 80%; float:left;" placeholder="Buscar tipos de Vehículos"   name="busqueda" class="form-control mr-sm-2 alin" type="text" />
             <a id="mt" href="javascript:void()" style="float:right;" placeholder="Buscar" class="btn btn-warning" type="submit"><span class="glyphicon glyphicon-search"></span></a>
         <div class='btn-group' style="display:flex; float:left">
          <button class='btn dropdown-toggle btn-success' data-toggle='dropdown' value='Más'><span class='fa fa-reorder'></span></button>
              <ul class='dropdown-menu'>
                <li><a id="nombree" href='javascript:void();' class='btn btn-link ' type="submit" style='text-decoration:none;'>Nombre</a></li>  
                  <li><a id="fechaa" href='javascript:void();' class='btn btn-link ' type="submit" style='text-decoration:none;'>Fecha</a></li>  
                 </ul>
         </div>        
      </div>            
         <input id="rol" type="hidden" value="<%=TipoRol%>" />

        <div class="row">
            <div class="col-md-12 text-center">
                <ul class="nav nav-pills nav-justified">
                    <li><a ><i class="fa fa-car"></i>&nbsp;&nbsp;Tipos de vehiculos&nbsp;&nbsp; <span class="badge"><%Response.Write(row1); %></span></a></li>
                </ul>
            </div>
        </div>

       <br/>
     <div class="row">
         <div class="col-md-12">
             <div class="table-responsive">
                 <form runat="server" id="mostrar">
                     <asp:GridView ID="tabla" OnPreRender="tabla_PreRender" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" Height="100" AllowCustomPaging="true" AllowPaging="true" Width="100%" PageSize="50"  PageIndex="5">
                         <Columns>
                             <asp:TemplateField>
                                 <ItemTemplate>
                                     <asp:CheckBox ID="chkTipo" runat="server" OnCheckedChanged="chkTipo_CheckedChanged" />
                                 </ItemTemplate>
                             </asp:TemplateField>

                             <asp:TemplateField HeaderText="#">
                                 <ItemTemplate>
                                     <%#(Container.DataItemIndex + 1) + inicializacion %>
                                 </ItemTemplate>
                             </asp:TemplateField>
                            <asp:BoundField DataField="id_tipo" HeaderText="ID tipo" SortExpression="id_tipo" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                <ItemStyle CssClass="hidden"></ItemStyle>
                            </asp:BoundField>
                                <asp:BoundField  DataField="nombre" HeaderText="Tipo" SortExpression="nombre" />
                             <asp:BoundField  DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                                <asp:BoundField  DataField="Fecha_creacion" HeaderText="Creado" SortExpression="Fecha_creacion" />
                       
                             <asp:TemplateField HeaderText="Opciones">
                                 <ItemTemplate>
                                     <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-sm btn-success"
                                         NavigateUrl='<%# Eval("id_tipo","~/admin.aspx?view=typeEdit&idT={0}") %>'>
                                         <i class="fa fa-pencil" aria-hidden="true"></i>
                                     </asp:HyperLink>
                                     <button type="button" class="dropbtn btn btn-sm btn-danger" data-toggle="modal" data-target="#pregunta" onclick='document.getElementById("borrar_idT").value = "<%# Eval("id_tipo")%>";'>
                                         <i class="fa fa-trash-o" aria-hidden="true"></i>
                                     </button>
                                 </ItemTemplate>
                             </asp:TemplateField>
                           </Columns>
                            <EmptyDataTemplate>
                                <h2 class="text-center"> No hay registros de tipos en el sistema</h2>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="Black" ForeColor="White" />
                            <RowStyle />
                            <SelectedRowStyle Font-Bold="true" ForeColor="Navy" />
                     </asp:GridView>
                      <asp:Button   runat="server"  form="mostrar" Style="display:none;" OnClick="btnEliminar_Click" CssClass="btnEliminarClass" />
                     <asp:Button   runat="server"  form="mostrar" Style="display:none;" Onclick="btnPdf_Click"  CssClass="btnPdfClass" />
                      <button id="btnNuevo" type="button" style="display:none;" data-toggle='modal' data-target='#modal1'></button>
                 </form>
             </div>
         </div>
     </div>
        <% if (numPagina >= 1)
            { %>
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
                    else
                    {%>
                <li>
                    <a href="./admin.aspx?view=types&pagina=<%Response.Write(pagina-1); %>" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                    </a>
                </li>
                <%}
                    for (int i = 1; i <= numPagina; i++)
                    {
                        if (pagina == i)
                        {
                            Response.Write("<li class='active'><a href='./admin.aspx?view=types&pagina=" + i + "'>" + i + "</a></li>");
                        }
                        else
                        {
                            Response.Write("<li><a href='./admin.aspx?view=types&pagina=" + i + ">'" + i + "</a></li>");
                        }
                    }
                    if (pagina == numPagina)
                    {%>
                       <li class="disabled">
                           <a aria-label="Previous">
                               <span aria-hidden="true">&raquo;</span>
                           </a>
                       </li>
                        <%
                            }
                            else
                            { %>
                        <li>
                            <a href="/admin.aspx?view=types&pagina=<% Response.Write(pagina + 1);%>" aria-label="Previous">
                                <span aria-hidden="true">&raquo;</span>
                            </a>
                        </li>
                  <% }
                %>
            </ul>
        </nav>
        <%} %>

    </div>
</div>

<uc:DeleteType runat="server" />
<uc:InsertType runat="server" />