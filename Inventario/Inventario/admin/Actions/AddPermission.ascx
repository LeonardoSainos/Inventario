<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPermission.ascx.cs" Inherits="Inventario.Inventario.admin.Actions.AddPermission" %>
<% Response.Write(alerta); %>
<div class="container">
    <div class="modal" tabindex="-1" id="permisos">
        <div class="modal-dialog modal-xlg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header" style="background: black; text-align:center;">

                    <button class="close" data-dismiss="modal">&times;</button>
                    <h1 style="color:white;"> Añadir permisos</h1>
                </div> 
                <div class="modal-body">
                    <form id="add" class="formu"    action="" method="post">
                        <div>
                          

                        </div>






                                      <div class="modal-footer"> 
                                                    <input type="hidden" name="userPer" id="userPermisos" />
                                                  <input class="btn btn-warning" type="submit" value="Aplicar cambios"/>
                                                <input class="btn btn-success"  onclick="funcion_reiniciar('add');" type="button" value="Restablecer"/>
                                                <button class="btn btn-danger" data-dismiss="modal">Cancelar </button>
                                     </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>