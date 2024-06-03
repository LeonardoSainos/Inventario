<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="carEdit-view.ascx.cs" Inherits="Inventario.Inventario.admin.Vehiculos.VehiculosEdit_view" %>
<div class="container">
    <%Response.Write(alerta); %>
    <div class="row">
             <div class="col-sm-3">
                <img src="../Inventario/img/Edit.png" alt="Image" class="img-responsive animated tada" />
            </div>
            <div class="col-sm-9">
                <a href="./admin.aspx?view=cars" class="btn btn-warning btn-sm pull-right"><i class="fa fa-reply"></i>&nbsp;&nbsp;Volver administrar vehículos</a>
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
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Nombre:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 
                                </div>
                            </div>
                        </div>

                      <div class="form-group">
                          <div class="col-sm-offset-2 col-sm-10 text-center">
                              <button type="submit" class="btn btn-warning">Actualizar vehículo</button>
                            </div>
                        </div>
                      </form>
            </div><!--col-md-12-->
          </div><!--container-->