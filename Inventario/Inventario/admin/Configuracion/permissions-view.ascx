<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="permissions-view.ascx.cs" Inherits="Inventario.Inventario.admin.Configuracion.permissions_view" %>
<%@ Import Namespace="Inventario.Scripts" %>

<div id="contenido">
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
                <%Response.Write(alerta); %>
                <img src="../Inventario/img/permisos.png" alt="animación" class="img-responsive animated flipInY" />
            </div>
            <div class="col-sm-10">
                <p class="lead text-info">
                    Bienvenido administrador, en esta página se muestran todos los usuarios de Inventario Alcomex. Puedes agregar o quitar permisos.
                </p>
            </div>
        </div>
    </div>
    <br /> <br />

    <div class="container">
        <div class="btn-group">
            <button class="btn dropdown-toggle btn-warning" data-toggle="dropdown" value="Más">Más<span class="caret"></span></button>
            <ul class="dropdown-menu">
                <li>
                    <span style='margin-left:22px' class='glyphicon gly1phicon-user'></span>
                    <input class="btn btn-link" style='text-decoration:none;' onclick="ActivarBoton('Nuevo');" value="Nuevo usuario" type="button" />
                </li>
            </ul>
        </div>
        <input id="NombrePaginaActual" type="hidden" value="<%=PaginaNombre%>" />
        <input id="rol" type="hidden" value="<%=TipoRol%>" />

        <div style="display:flex; float:right;">
            <input id="busqueda" style="width: 80%; float:left;" placeholder="Buscar usuarios" name="busqueda" class="form-control mr-sm-2 alin" type="text" />
            <a id="mt" href="javascript:void()" style="float:right;" placeholder="Buscar" class="btn btn-warning" type="submit">
                <span class="glyphicon glyphicon-search"></span>
            </a>
            <div class='btn-group' style="display:flex; float:left">
                <button class='btn dropdown-toggle btn-success' data-toggle='dropdown' value='Más'>
                    <span class='fa fa-reorder'></span>
                </button>
                <ul class='dropdown-menu'>
                    <li><a id="nombree" href='javascript:void();' class='btn btn-link ' type="submit" style='text-decoration:none;'>Nombre</a></li>
                    <li><a id="correoo" href='javascript:void();' class='btn btn-link ' type="submit" style='text-decoration:none;'>Correo</a></li>
                </ul>
            </div>
        </div>
        <br /><br />

        <div class="row">
            <div class="col-md-12 text-center">
                <ul class="nav nav-pills nav-justified">
                    <li><a><i class="fa fa-male"></i>&nbsp;&nbsp;Todos los usuarios&nbsp;&nbsp;<span class="badge"> <% Response.Write(row1); %></span></a></li>
                </ul>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                    <form runat="server" id="mostrar">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePartialRendering="true" />
                           <div>
                               <asp:GridView   ID="GridViewModulos"  runat="server"    class="table  table-bordered"    AutoGenerateColumns="false"    OnRowCommand="GridViewModulos_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# (Container.DataItemIndex + 1) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_cliente" HeaderText="IDCLIENTE" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="nombre_completo" HeaderText="Nombre" />
                                    <asp:BoundField DataField="email_cliente" HeaderText="Correo" />
                                    <asp:TemplateField HeaderText="Seguridad">
                                        <ItemTemplate>
                                            <asp:Button  CssClass="btnMostrarSubmoduloClient btn btn-warning" ID="btnMostrarSubmodulo" runat="server" Text="+"  CommandName="ShowSubmodulo" CommandArgument='<%#Container.DataItemIndex + "," + Eval("id_cliente") %>' />
                                            <asp:UpdatePanel ID="UpdatePanelSubmodulo" runat="server"   UpdateMode="Conditional" >
                                              <ContentTemplate>  
                                                <asp:Panel ID="PanelSubmodulo" runat="server" CssClass="details" >
                                                    <asp:GridView class="table table-bordered" OnRowDataBound="GridViewSubmodulos_RowCommand" ID="GridViewSubmodulos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" >
                                                         <Columns>
                                                             <asp:BoundField DataField="ModuloId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <%# (Container.DataItemIndex + 1) %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Submódulo">
                                                                        <ItemTemplate >
                                                                            <%# Eval("ModuloNombre") %> 
                                                                        </ItemTemplate>

                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo de Acceso">
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanelSelectNombre" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="SelectNombre" AutoPostBack="true" OnSelectedIndexChanged="SelectNombre_SelectedIndexChanged"    runat="server" />
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="SelectNombre" EventName="SelectedIndexChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                               <asp:TemplateField HeaderText="Submodulos">
                                                                   <ItemTemplate>
                                                                       <asp:Button CssClass="btn btn-warning" ID="btnMostrarSubsubmodulo" runat="server" Text="+"  CommandName="ShowSubsubmodulo" CommandArgument='<%#Container.DataItemIndex %>'/>
                                                                        <asp:Panel ID="PanelSubsubmodulo" runat="server" CssClass="details">
                                                                        </asp:Panel>
                                                                   </ItemTemplate>
                                                               </asp:TemplateField>
                                                   
                                                        </Columns>  <HeaderStyle BackColor="Black" ForeColor="White" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>   
                                             <Triggers>
                                                 <asp:AsyncPostBackTrigger ControlID="btnMostrarSubmodulo" EventName="Click" />
                         
                                             </Triggers> 
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                              </Columns>
                                <EmptyDataTemplate>
                                    <h2 class="text-center">No hay USUARIOS registrados en el sistema</h2>
                                </EmptyDataTemplate>
                              <HeaderStyle BackColor="Black" ForeColor="White" />
                            </asp:GridView>
               </div>
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
                    <a href="./admin.aspx?view=permissions&pagina=<%Response.Write(pagina-1); %>" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                    </a>
                </li>
                <%}
                for(int i=1; i<=numPagina; i++)
                {
                    if (pagina == i){
                        Response.Write("<li class='active'><a href='./admin.aspx?view=permissions&pagina=" + i + "'>" + i + "</a></li>");
                    }
                    else {
                        Response.Write("<li><a href='./admin.aspx?view=permissions&pagina=" + i + "'>" + i + "</a></li>");
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
                    <a href="./admin.aspx?view=permissions&pagina=<%Response.Write(pagina+1); %>" aria-label="Previous">
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

<uc:InsertUser runat="server" />
