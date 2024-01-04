<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUser.ascx.cs" Inherits="Inventario.Inventario.admin.AddUser" %>

<!-- Modal para agregar usuario -->
<div class="container">
                            <div class="modal" tabindex="-1" id="modal1" >
                                <div class="modal-dialog modal-xlg  modal-dialog-centered">
                                    <div class="modal-content">
                                    <div class="modal-header" style="background: black; text-align:center;">
                                        <button class="close" data-dismiss="modal">&times;</button>
                                          <h1 style="color: white;">Agregar  nuevo usuario</h1>
                                        </div>
                                          <div class="modal-body">
                                            <form id="add" class="formu"  action= "" method="POST">
                                                
                                                
                                                <div class="form-group">
                                               <label class="col-sm-2 control-label">Nombre:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                             <input REQUIRED  class="formu form-control" type="text" name="Gnombre" placeholder="Nombre" maxlength="75">
                                                             <span class="input-group-addon"><i class="fa fa-user"></i></span>
                           
                                                        </div>
                                                      </div>
                                                </div> 
                                                <br/>
                                                <br/> <br/>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Apellidos:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                          <input required=""  class=" formu form-control" type="text" name="Gapellidos" placeholder="Apellidos" maxlength="70">
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
                                                           <input  REQUIRED  class="formu form-control" type="email" name="Gcorreo" placeholder="Correo electronico" maxlength="70">
                                                           <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                           
                                                        </div>
                                                      </div>
                                                   </div>
                                                <br>
                                                <br>

                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Usuario:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                           <input  REQUIRED  class="formu form-control" type="text" name="Gusuario" placeholder="Nombre de usuario" maxlength="70">
                                                           <span class="input-group-addon"><i class="fa fa-address-book"></i></span>
                           
                                                        </div>
                                                      </div>
                                                   </div>
                                                <br>
                                                <br>

                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Estatus:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                <?php $E=Mysql::consulta("SELECT * FROM estatus WHERE ((idEstatus = 31448 OR idEstatus = 94573 ) OR   (idEstatus = 19231 OR idEstatus =  25542 ) ) ORDER BY Nombre ");
                                                echo "
                                            
                                                
                                                <select REQUIRED  class='formu form-control'name='Gestatus'>";
                                                if ($E) {
                                                    while ($EST=mysqli_fetch_array($E,MYSQLI_ASSOC)) {
                                                        echo"  <option value=" .$EST['idEstatus']. ">" .$EST['Nombre']. "</option>";
                                                    }
                                                }
                                                
                                                
                                                 echo "</select>"
                                                ?>
                                                  <span class="input-group-addon"><i class="fa fa-info"></i></span>
                           
                                                        </div>
                                                      </div>
                                                   </div>
                                                <br><br>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Rol:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                <?php $E=Mysql::consulta("SELECT * FROM rol ");
                                                echo "
                                                   <select REQUIRED  class='formu form-control'name='Grol'>";
                                                if ($E) {
                                                    while ($EST=mysqli_fetch_array($E,MYSQLI_ASSOC)) {
                                                        echo"  <option value=" .$EST['idRol']. ">" .$EST['Nombre']. "</option>";
                                                    }
                                                }
                                                 echo "</select>"
                                                ?>
                                                          <span class="input-group-addon"><i class="fa fa-briefcase"></i></span>
                           
                                                        </div>
                                                      </div>
                                                </div>
                                                <br>
                                                <br>

                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Departamento: </label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                        
                                                <?php $E=Mysql::consulta("SELECT nombre,idDepartamento From departamento");
                                                echo "
                                                    
                                                <select REQUIRED  class='formu form-control'name='Gdepartamento'>";
                                                if ($E) {
                                                    while ($EST=mysqli_fetch_array($E,MYSQLI_ASSOC)) {
                                                        echo"  <option value=" .$EST['idDepartamento']. ">" .$EST['nombre']. "</option>";
                                                    }
                                                }
                                                 echo "</select>"
                                                ?>
                                                               <span class="input-group-addon"><i class="fa fa-users"></i></span>
                                                            </div>
                                                      </div>
                                                </div>
                                                      <br><br>
                                                   <div class="form-group">
                                                    <label class="col-sm-2 control-label">Teléfono:</label>
                                                      <div class='col-sm-10'>
                                                        <div class="input-group">
                                                              <input  REQUIRED  class="formu form-control" type="tel" name="Gtelefono" placeholder="Número de celular"  maxlength="10"/>
                                                             <span class="input-group-addon"><i class="fa fa-phone-square"></i></span>
                                                        </div>
                                                      </div>
                                                   </div><br><br>
                                                
                                                <!--id_usuario	id_estatus	id_rol	nombre	apellidos	id_departamento	contraseña	correo	id_zona	fecha_creacion	fecha_actualizacion	
  -->                                      <div class="modal-footer">

                                                    
                                                  <input class="btn btn-warning" type="submit" value="Crear usuario">
                                                <input class="btn btn-success"  onclick="funcion_reiniciar('add');"type="button" value="Restablecer">
                                                <button class="btn btn-danger" data-dismiss="modal">Cancelar </button>

                                          </div>
                                            </form>

                                        </div>
                                              
                                        </div>
                                    </div>
                                </div>
                                     <!--FIN DEL MODAL -->
                            </div>