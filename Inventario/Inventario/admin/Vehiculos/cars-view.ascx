<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cars-view.ascx.cs" Inherits="Inventario.Inventario.admin.Vehiculos.Vehiculos_view" %>
 <%@ Import Namespace="Inventario.Scripts" %>
<div id="contenido">
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
                <%Response.Write(alerta); %>
                <img src="../Inventario/img/type.png" alt="Animacion" class="img-responsive animated flipInY" />
            </div>
            <div class="col-sm-10">
               <p class="lead text-info">Bienvenido administrador, en esta página se muestran los Vehiculos de Alcomex. Puedes eliminar, actualizar o agregar más registros en esta página</p>
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
                <li><span style='margin-left:22px' class='glyphicon glyphicon-road'></span><input class="btn btn-link" style="text-decoration:none;" onclick="ActivarBoton('Nuevo');" value="Agregar" type="button" /></li>
                <li><span style='margin-left:22px;' class='glyphicon glyphicon-trash'></span> <button type="submit" class='btn btn-link'  onclick="ActivarBoton('Eliminar');" style='text-decoration:none;' name="Eliminar">Eliminar</button></li>
                <li><span style='margin-left:22px;' class='glyphicon glyphicon-save'></span> <button type="button" class='btn btn-link' style='text-decoration:none;' name="ExportarPdf" onclick="ActivarBoton('Pdf');">Exportar PDF</button></li>
               </ul>
        </div>
         <div style="display:flex; float:right;">
              <input id="busqueda" style="width: 80%; float:left;" placeholder="Buscar Vehículos"   name="busqueda" class="form-control mr-sm-2 alin" type="text" />
             <a id="sv" href="javascript:void()" style="float:right;" placeholder="Buscar" class="btn btn-warning" type="submit"><span class="glyphicon glyphicon-search"></span></a>
         <div class='btn-group' style="display:flex; float:left">
          <button class='btn dropdown-toggle btn-success' data-toggle='dropdown' value='Más'><span class='fa fa-reorder'></span></button>
              <ul class='dropdown-menu'>
                <li><a id="Typenombree" href='javascript:void();' class='btn btn-link ' type="submit" style='text-decoration:none;'>Nombre</a></li>  
                  <li><a id="Typefechaa" href='javascript:void();' class='btn btn-link ' type="submit" style='text-decoration:none;'>Fecha</a></li>  
                 </ul>
         </div>        
      </div>            
         <input id="rol" type="hidden" value="<%=TipoRol%>" />
            <input id="NombrePaginaActual" type="hidden" value="<%=PaginaNombre%>" />
                <div class="row">
            <div class="col-md-12 text-center">
                <ul class="nav nav-pills nav-justified">
                    <li><a ><i class="fa fa-car"></i>&nbsp;&nbsp;Todos los vehiculos&nbsp;&nbsp; <span class="badge"><%Response.Write(row1); %></span></a></li>
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
                                        <asp:CheckBox ID="ckCar" runat="server" OnCheckedChanged="ckCar_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                                 <%#(Container.DataItemIndex+1)+ inicializacion %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id_vehiculo" HeaderText="ID_vehiculo" SortExpression="id_vehiculo" HeaderStyle-CssClass="hidden" >
                                    <HeaderStyle CssClass="hidden" > </HeaderStyle>
                                    <ItemStyle CssClass="hidden" > </ItemStyle>
                                    </asp:BoundField>
                                      <asp:BoundField  DataField="nombre_vehiculo" HeaderText="Vehículo" SortExpression="vehiculo" />
                                      <asp:BoundField  DataField="asignado" HeaderText="Chofer" SortExpression="asignado" />
                                     <asp:BoundField  DataField="tipo" HeaderText="Tipo de Vehiculo" SortExpression="tipo" />
                                     <asp:BoundField  DataField="marca" HeaderText="Marca" SortExpression="marca" />
                                     <asp:BoundField  DataField="placas" HeaderText="Placas" SortExpression="placas" />
                                     <asp:BoundField  DataField="modelo" HeaderText="Modelo" SortExpression="modelo" />
                                     <asp:BoundField  DataField="estatus" HeaderText="Estatus" SortExpression="estatus" />
                                     <asp:BoundField  DataField="Numero_serie" HeaderText="Numero de serie" SortExpression="Numero_serie" />
                                     <asp:BoundField  DataField="poliza_seguro" HeaderText="Poliza de seguro" SortExpression="poliza_seguro" />
                                     <asp:BoundField  DataField="propietarioGPS" HeaderText="GPS" SortExpression="propietarioGPS" />
                                     <asp:BoundField  DataField="actualizado" HeaderText="Ultima actualización" SortExpression="actualizado" />
                                  <asp:TemplateField HeaderText="Opciones">
                                 <ItemTemplate>
                                     <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-sm btn-success"
                                         NavigateUrl='<%# Eval("id_vehiculo","~/admin.aspx?view=carEdit&idC={0}") %>'>
                                         <i class="fa fa-pencil" aria-hidden="true"></i>
                                     </asp:HyperLink>
                                     <button type="button" class="dropbtn btn btn-sm btn-danger" data-toggle="modal" data-target="#pregunta" onclick='document.getElementById("borrar_idC").value = "<%# Eval("id_vehiculo")%>";'>
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
                    <a href="./admin.aspx?view=cars&pagina=<%Response.Write(pagina-1); %>" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                    </a>
                </li>
                <%}
                    for (int i = 1; i <= numPagina; i++)
                    {
                        if (pagina == i)
                        {
                            Response.Write("<li class='active'><a href='./admin.aspx?view=cars&pagina=" + i + "'>" + i + "</a></li>");
                        }
                        else
                        {
                            Response.Write("<li><a href='./admin.aspx?view=cars&pagina=" + i + "'>" + i + "</a></li>");
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
                            <a href="/admin.aspx?view=cars&pagina=<% Response.Write(pagina + 1);%>" aria-label="Previous">
                                <span aria-hidden="true">&raquo;</span>
                            </a>
                        </li>
                  <% }
                %>
            </ul>
        </nav>
        <%} %>
     </div>
<uc:InsertCar runat="server" />
<uc:DeleteCar runat="server" />
