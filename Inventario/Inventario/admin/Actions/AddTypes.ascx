<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTypes.ascx.cs" Inherits="Inventario.Inventario.admin.Actions.AddTypes" %>
<% Response.Write(alerta); %>
<div class="container">
    <div class="modal" tabindex="-1" id="modal1">
        <div class="modal-dialog modal-xlg  modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header" style="background:black; text-align:center";>
                    <button class="close" data-dismiss="modal">&times;</button>
                    <h1 style="color:white;">Agregar tipo de vehículo</h1>
                </div>
                <div class="modal-body">
                    <form id="add" class="formu" action="" method="post">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Tipo:</label>
                             <div class='col-sm-10'>
                                  <div class="input-group">
                                    <input required=""  class="formu form-control" type="text" name="Gtype" placeholder="Tipo de Vehiculo" maxlength="75"/>
                                     <span class="input-group-addon"><i class="fa fa-car"></i></span>
                                  </div>
                             </div>
                        </div>  <br/><br/> <br/>
                      <div class="form-group">
                             <label class="col-sm-2 control-label">Descripción:</label>
                              <div class='col-sm-10'>
                                   <div class="input-group">
                                       <input required=""  class=" formu form-control" type="text" name="Gdescription" placeholder="Añade una descripción" maxlength="500"/>
                                        <span class="input-group-addon"><i class="fa fa-pencil"></i></span>
                                   </div>
                              </div>
                     </div> <br/>       
                              <div class="modal-footer">       
                                                  <input class="btn btn-warning" type="submit" value="Crear tipo"/>
                                                <input class="btn btn-success"  onclick="funcion_reiniciar('add');" type="button" value="Restablecer"/>
                                                <button class="btn btn-danger" data-dismiss="modal">Cancelar </button>

                      </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>