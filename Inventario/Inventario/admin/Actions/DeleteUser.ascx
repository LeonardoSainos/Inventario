<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteUser.ascx.cs" Inherits="Inventario.Inventario.admin.DeleteUser" %>
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
                             <form  id="formulario" style="display: inline-block;" method="post" >                  
                                  <input  type="hidden" name="id_dele"   id="borrar_id" />       
                                    <button  type="submit"   class="btn btn-success">SI</button>
                                   <button type="button" class="btn btn-danger" data-dismiss="modal">CANCELAR</button>
                             </form>                        
                      </center>
                  </div>
             </div>
       </div>    
</div>