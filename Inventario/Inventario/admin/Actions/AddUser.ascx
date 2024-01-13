<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUser.ascx.cs" Inherits="Inventario.Inventario.admin.AddUser" %>
 <%Response.Write(alerta); %>
<div class="container">
                            <div class="modal" tabindex="-1" id="modal1" >
                                <div class="modal-dialog modal-xlg  modal-dialog-centered">
                                    <div class="modal-content">
                                    <div class="modal-header" style="background: black; text-align:center;">
                                        <button class="close" data-dismiss="modal">&times;</button>
                                          <h1 style="color: white;">Agregar  nuevo usuario</h1>
                                        </div>
                                          <div class="modal-body">
                                            <form id="add" class="formu"  action="" method="post">
                                                
                                                
                                                <div class="form-group">
                                               <label class="col-sm-2 control-label">Nombre:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                             <input required=""  class="formu form-control" type="text" name="Gnombre" placeholder="Nombre" maxlength="75">
                                                             <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                                        </div>
                                                      </div>
                                                </div> 
                                                <br/>
                                                <br/> <br/>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Primer Apellido:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                          <input required=""  class=" formu form-control" type="text" name="Gapellidos1" placeholder="Primer apellido" maxlength="70">
                                                          <span class="input-group-addon"><i class="fa fa-user-o"></i></span>
                                                        </div>
                                                      </div>
                                                </div>
                                                    <br/>
                                                   <br/>
                                                   <div class="form-group">
                                                    <label class="col-sm-2 control-label">Segundo Apellido:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                          <input required=""  class=" formu form-control" type="text" name="Gapellidos2" placeholder="Segundo apellido" maxlength="70">
                                                          <span class="input-group-addon"><i class="fa fa-user-o"></i></span>
                                                        </div>
                                                      </div>
                                                </div>
                                                    <br/>
                                                   <br/>
                                                   <div class="form-group">
                                                    <label class="col-sm-2 control-label">Correo:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                           <input  required=""  class="formu form-control" type="email" name="Gcorreo" placeholder="Correo electronico" maxlength="70">
                                                           <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                                        </div>
                                                      </div>
                                                   </div>
                                                <br/>
                                                <br/>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Usuario:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                           <input  required=""  class="formu form-control" type="text" name="Gusuario" placeholder="Nombre de usuario" maxlength="70">
                                                           <span class="input-group-addon"><i class="fa fa-address-book"></i></span>
                                                        </div>
                                                      </div>
                                                   </div>
                                                <br/>
                                                <br/>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Estatus:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                          <%  Response.Write("<select REQUIRED  class='formu form-control' name='Gestatus'>");  
                                                                for(int j=0; j< Totest; j++) 
                                                                {
                                                                     Response.Write("<option value='" + ideEstatus[j] +  "'>" + valorEst[j]  + "</option>");   
                                                                }
                                                            Response.Write("</select>"); %>
                                                      <span class="input-group-addon"><i class="fa fa-info"></i></span>
                                                        </div>
                                                      </div>
                                                   </div>
                                                <br/><br/>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Rol:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                             <%  Response.Write("<select REQUIRED  class='formu form-control' name='Grol'>");  
                                                                for(int j=0; j< TotR; j++) 
                                                                {
                                                                     Response.Write("<option value='" + ideRol[j] +  "'>" + valorRol[j]  + "</option>");   
                                                                }
                                                            Response.Write("</select>"); %>
                                                          <span class="input-group-addon"><i class="fa fa-briefcase"></i></span>
                                                        </div>
                                                      </div>
                                                </div>
                                                <br/>
                                                <br/>

                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Departamento: </label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                             <%  Response.Write("<select REQUIRED  class='formu form-control' name='Gdepartamento'>");  
                                                                for(int j=0; j< TotalD; j++) 
                                                                {
                                                                     Response.Write("<option value='" + ideDepa[j] +  "'>" + valorDepa[j]  + "</option>");   
                                                                }
                                                                Response.Write("</select>"); %>
                                               
                                                               <span class="input-group-addon"><i class="fa fa-users"></i></span>
                                                            </div>
                                                      </div>
                                                </div>
                                                      <br/><br/>
                                                   <div class="form-group">
                                                    <label class="col-sm-2 control-label">Teléfono:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                              <input  required=""  class="formu form-control" type="tel" name="Gtelefono" placeholder="Número de celular"  maxlength="10"/>
                                                             <span class="input-group-addon"><i class="fa fa-phone-square"></i></span>
                                                        </div>
                                                      </div>
                                                   </div><br/><br/>
                                                
                                                <!--id_usuario	id_estatus	id_rol	nombre	apellidos	id_departamento	contraseña	correo	id_zona	fecha_creacion	fecha_actualizacion	
  -->                                      <div class="modal-footer">

                                                   
                                                  <input class="btn btn-warning" type="submit" value="Crear usuario">
                                                <input class="btn btn-success"  onclick="funcion_reiniciar('add');" type="button" value="Restablecer">
                                                <button class="btn btn-danger" data-dismiss="modal">Cancelar </button>

                                          </div>
                                            </form>

                                        </div>
                                              
                                        </div>
                                    </div>
                                </div>
                                     <!--FIN DEL MODAL -->
                            </div>