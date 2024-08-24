<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchBrands-view.ascx.cs" Inherits="Inventario.Inventario.admin.Marcas.searchBrands_view" %>
 <%@ Import Namespace="Inventario.Scripts" %>
<div id="contenido">
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
                <%Response.Write(alerta); %>
                <img src="../Inventario/img/brand.png" alt="Animación" class="img-responsive animated flipInY" />
            </div>
            <div class="col-sm-10">
                <p class="lead text-info">Bienvenido administrador, en esta página se muestran todas las marcas de Vehículos existentes en inventario Alcomex, usted podrá eliminar, actualizar y agregar nuevos registros. </p>
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
            <li><span style='margin-left:22px' class='glyphicon glyphicon-road'></span><input class="btn btn-link" style='text-decoration:none;' onclick="ActivarBoton('Nuevo');" value="Nueva marca" type="button"/></li>
            <li><span style='margin-left:22px;' class='glyphicon glyphicon-trash'></span> <button type="submit" class='btn btn-link'  onclick="ActivarBoton('Eliminar');" style='text-decoration:none;' name="Eliminar">Eliminar</button></li>
            <li><span style='margin-left:22px;' class='glyphicon glyphicon-save'></span> <button type="button" class='btn btn-link' style='text-decoration:none;' name="ExportarPdf" onclick="ActivarBoton('Pdf');">Exportar PDF</button></li>
        </ul>
    </div>
         <div style="display:flex; float:right;">
        <input id="busqueda" style="width: 80%; float:left;" placeholder="Buscar marcas" value="<%=Buscar %>"  name="busqueda" class="form-control mr-sm-2 alin" type="text" />
        <a id="sb" href="javascript:void()" style="float:right;" placeholder="Buscar" class="btn btn-warning" type="submit"><span class="glyphicon glyphicon-search"></span></a>
        <div class='btn-group' style="display:flex; float:left">
                <button class='btn dropdown-toggle btn-success' data-toggle='dropdown' value='Más'><span class='fa fa-reorder'></span></button>
              <ul class='dropdown-menu'>
                <li><a id="Brandnombree" href='javascript:void();' class='btn btn-link ' type="submit" style='text-decoration:none;'>Nombre</a></li>  
                <li><a id="Brandfechaa" href='javascript:void();' class='btn btn-link ' type="submit" style='text-decoration:none;'>Fecha</a></li>  
                  
              </ul>
       </div>
    </div>
    <input id="NombrePaginaActual" type="hidden" value="<%=PaginaNombre%>" />
            <input id="rol" type="hidden" value="<%=TipoRol%>" />

     <div class="row">
      <div class="col-md-12 text-center">
           <ul class="nav nav-pills nav-justified">
             <li><a href="./admin.aspx?view=brands"><i class="fa fa-car"></i>&nbsp;&nbsp;Todas las marcas &nbsp;&nbsp;<span class="badge"> <%Response.Write(row1);%></span></a></li>
         </ul>
       </div>
  </div>
            <br />
         <div class="row">
        <div class="col-md-12">
            <div class="talbe-responsive">
                                   <% if (totalEncontrados > 0 && (Buscar!=null && Buscar !="")) { %>
    <div class="col-sm-10">
        <p class="lead text-info"><strong><%= totalEncontrados %></strong> registros coinciden con tu búsqueda</p>
    </div>
<% } %>   


                  <form runat="server" id="mostrar">
                    <asp:GridView ID="tabla" OnPreRender="tabla_PreRender" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" Height="100%" AllowCustomPaging="true" AllowPaging="true" Width="100%" PageSize="50" PageIndex="5">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBrand" runat="server" OnCheckedChanged="chkBrand_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#(Container.DataItemIndex +1)+ inicializacion %>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:BoundField  DataField="id_marca" HeaderText="ID marca" SortExpression="id_marca" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                               <ItemStyle CssClass="hidden"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="nombre" HeaderText="Marca" SortExpression="nombre"/> 
                              <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                            <asp:BoundField  DataField="fecha_creacion" HeaderText="Creado" SortExpression="fecha_creacion"/>  
                            <asp:TemplateField HeaderText="Opciones">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-sm btn-success"
                                        NavigateUrl='<%#Eval("id_marca","~/admin.aspx?view=brandsEdit&idB={0}") %>'>
                                        <i class="fa fa-pencil" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                   <button type="button" class="dropbtn btn btn-sm btn-danger" data-toggle="modal" data-target="#pregunta" onclick='document.getElementById("borrar_idB").value="<%# Eval("id_marca")%>";'>
                                       <i class="fa fa-trash-o" aria-hidden="true"></i>
                                   </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                             </Columns>
                        <EmptyDataTemplate>
                            <h2 class="text-center"> No hay registros de marcas en el sistema</h2>
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="Black" ForeColor="White" />
                        <RowStyle />
                        <SelectedRowStyle Font-Bold="true" ForeColor="Navy"/>
                    </asp:GridView>
                   <asp:Button   runat="server"  form="mostrar" Style="display:none;" OnClick="btnEliminar_Click" CssClass="btnEliminarClass" />
                 <asp:Button   runat="server"  form="mostrar" Style="display:none;" OnClick="btnPdf_Click"  CssClass="btnPdfClass" />
                  <button id="btnNuevo" type="button" style="display:none;" data-toggle='modal' data-target='#modal1'></button>
                </form>
            </div>
        </div>
    </div>

        
    <%if (numPagina >= 1)
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
                { %>
             <li>
                  <% Response.Write("<a href='javascript:void()' id='retrocede' type='submit'  aria-label='Previous'> "); %>
                        <span aria-hidden="true">&laquo</span>
                      <% Response.Write("</a>"); %>
             </li>
               <%}
                   for (int i = 1; i <= numPagina; i++)
                   {
                       if (pagina == i)
                       {
                        Response.Write("<li class='active'><a id='paginador" + i + "' href='javascript:void()' type='submit'>" + i + "</a></li>");
                       }
                       else
                       {
                        Response.Write("<li><a id='paginador" + i +  "'  href='javascript:void();' type='submit'>" + i + "</a></li>");  
                   
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
                  {%>
                 <li>
                         <% Response.Write("<a href='javascript:void()' id='incremento' type='submit'  aria-label='Previous'>");  %>
                            <span aria-hidden="true">&raquo;</span>
                        <% Response.Write("</a>"); %>
                    </li>
                <%} %>
        </ul>
    </nav>
    <%} %>

            </div>
</div> 
<input type="hidden" value ="<%=tipoBusqueda %>" id="tipoBusqueda" />
<input type="hidden" value ="<%=pagina %>" id="paginaActual" />
<uc:DeleteBrand runat="server"/>
<uc:InsertBrand runat="server"/>