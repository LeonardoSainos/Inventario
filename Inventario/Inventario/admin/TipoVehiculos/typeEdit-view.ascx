<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="typeEdit-view.ascx.cs" Inherits="Inventario.Inventario.admin.TipoVehiculos.tipoEdit" %>
<div class="container">
    <%Response.Write(alerta); %>
    <div class="row">
        <div class="col-sm-3">
            <img src="../Inventario/img/Edit.png" alt="Imagen edit" class="img-responsive animated tada" />
        </div>
         <div class="col-sm-9">
                <a href="./admin.aspx?view=types" class="btn btn-warning btn-sm pull-right"><i class="fa fa-reply"></i>&nbsp;&nbsp;Volver a tipos de vehículos</a>
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
                                    <input class="form-control" required="" type="datetime-local" name="Tfecha"  value="<%=txtFecha %>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                            </div>
                        </div>
                    
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                    <input class="form-control" required="" type="text" name="Tnombre"   value="<%=txtNombre %>" />
                                    <span class="input-group-addon"><i class="fa fa-barcode"></i></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Descripción:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                    <input class="form-control" required="" type="text" name="Tdescripcion" value="<%=txtDescripcion %>" />
                                    <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                </div>
                            </div>
                        </div>
              <br/>
                        <div class="form-group">
                          <div class="col-sm-offset-2 col-sm-10 text-center">
                              <button type="submit" class="btn btn-warning">Actualizar datos</button>
                            </div>
                        </div>
        </form>
     </div>
</div>