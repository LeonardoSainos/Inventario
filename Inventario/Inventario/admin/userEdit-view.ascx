<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="userEdit-view.ascx.cs" Inherits="Inventario.Inventario.admin.userEdit_view" %>
 <div class="container">
     <% Response.Write(alerta); %>
          <div class="row">
            <div class="col-sm-3">
                <img src="../Inventario/img/Edit.png" alt="Image" class="img-responsive animated tada" />
            </div>
            <div class="col-sm-9">
                <a href="./admin.aspx?view=admin" class="btn btn-warning btn-sm pull-right"><i class="fa fa-reply"></i>&nbsp;&nbsp;Volver administrar usuarios</a>
            </div>
          </div>
        </div>
          <div class="container">
            <div class="col-sm-12">
                <form class="form-horizontal" role="form" action="" method="post">
                		<input type="hidden" name="id_edit" value="<%=idEdit %>" />
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Fecha:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                    <input class="form-control"   type="text" name="fecha" readonly="" value="<%=txtFecha %>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                            </div>
                        </div>
                    
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                    <input class="form-control"  readonly="" type="text" name="nombre_completo"   value="<%=txtNombre %>" />
                                    <span class="input-group-addon"><i class="fa fa-barcode"></i></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Correo:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                    <input class="form-control"   type="text" name="email_cliente"    value="<%=txtCorreo %>" />
                                    <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre de usuario:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                    <input class="form-control" readonly=""   type="text" name="nombre"   value="<%=txtUsuario %>" />
                                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Estado:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">                            
                                    <select class="form-control" name="estado_cliente">
                                        <option  value="<%=txtIdEstatus%>"> <%=txtEstado%>   </option> 
                                        <%
                                            for(int j=0; j<Totest; j++)
                                            {
                                                Response.Write("<option value='" + ideEstatus[j] +  "'>" + valorEst[j] +  "</option>");
                                            }   
                                        %>
                                     </select>
                                    <span class="input-group-addon"><i class="fa fa-info"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Departamento:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">                            
                                    <select class="form-control" name="departamento_cliente">
                                           <option  value="<%=txtIdDepa %>"> <%=txtDepa%>   </option> 
                                      <%
                                            for(int j=0; j< TotalD; j++)
                                            {
                                                Response.Write("<option value='" + ideDepa[j] + "'>" + valorDepa[j] +  "</option>");

                                            }   
                                        %>
                                      </select>
                                    <span class="input-group-addon"><i class="fa fa-users"></i></span>
                                </div>
                            </div>
                        </div>



                        <div class="form-group">
                            <label class="col-sm-2 control-label">Rol:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">                            
                                    <select class="form-control" name="rol_cliente">
                                         <option  value="<%=txtIdRol %>"> <%=txtRol%>   </option> 
                                      <%
                                            for(int j=0; j< TotR; j++)
                                            {
                                                Response.Write("<option value='" + ideRol[j] + "'>" + valorRol[j] +  "</option>");

                                            }   
                                        %>
                                      </select>
                                    <span class="input-group-addon"><i class="fa fa-briefcase"></i></span>
                                </div>
                            </div>
                        </div>

                     <div class="form-group">
                          <label for="inputEmail3" class="col-sm-2 control-label">Teléfono:</label>
                          <div class="col-sm-10">
                              <div class='input-group'>
                                  <input type="tel" class="form-control"  name="telefono"   value="<%=txtTelefono %>" />
                                <span class="input-group-addon"><i class="fa fa-phone-square"></i></span>
                              </div> 
                          </div>
                        </div>
                        <div class="form-group">
                          <label for="inputEmail3" class="col-sm-2 control-label">Anydesk:</label>
                          <div class="col-sm-10">
                              <div class='input-group'>
                                  <input type="text" required="" maxlength="10" pattern="^[0-9]+$" class="form-control"  name="anydesk"   value="<%=txtAnydesk %>" />
                                <span class="input-group-addon"><i class="fa fa-desktop"></i></span>
                              </div> 
                          </div>
                        </div>
                        
                    <br/>
                        <div class="form-group">
                          <div class="col-sm-offset-2 col-sm-10 text-center">
           
                              <button type="submit" class="btn btn-warning">Actualizar usuario</button>
                         
                            </div>
                        </div>
                      </form>
            </div><!--col-md-12-->
          </div><!--container-->