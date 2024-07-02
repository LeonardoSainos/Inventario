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
                            <label class="col-sm-2 control-label">Creado:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                    <input class="form-control" type="datetime-local" readonly="" name="Cfecha" value="<%=FechaCreado%>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                            </div>
                        </div>

                     <div class="form-group">
                            <label class="col-sm-2 control-label">Vehiculo:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                  <input class="form-control" type="text" name="Ccar"  value="<%=Vehiculo %>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Asignado:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                               <select class="form-control" name="Coperador">
                                     <option value="<%=IDOperador %>"><%=AsignadoOperador %> </option>
                                     <% for(int i=0; i < TotalOperador; i++)
                                         {
                                             Response.Write("<option value='" + ideOperador[i] + "'>" + validarOperador[i] + "</option>");
                                         }
                                     %>
                                 </select>
                                   <span class="input-group-addon"><i class="fa fa-info"></i></span>
                        
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Tipo:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                 <select class="form-control" name="CidTipo">
                                     <option value="<%=IDTipo %>"><%=txtTipo %> </option>
                                     <% for(int i=0; i < TotalTipo; i++)
                                         {
                                             Response.Write("<option value='" + ideTipo[i] + "'>" + validarTipo[i] + "</option>");
                                         }
                                     %>
                                 </select>
                                   <span class="input-group-addon"><i class="fa fa-info"></i></span>
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Modelo:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                         <select class="form-control" name="CidModelo">
                                     <option value="<%=IDModelo %>"><%=txtModelo %> </option>
                                     <% for(int i=0; i < TotalModelo; i++)
                                         {
                                             Response.Write("<option value='" + ideModelo[i] + "'>" + validarModelo[i] + "</option>");
                                         }
                                     %>
                                 </select>
                                   <span class="input-group-addon"><i class="fa fa-info"></i></span>
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Marca:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                  <select class="form-control" name="CidMarca">
                                     <option value="<%=IDMarca %>"><%=txtMarca %> </option>
                                     <% for(int i=0; i < TotalMarca; i++)
                                         {
                                             Response.Write("<option value='" + ideMarca[i] + "'>" + validarMarca[i] + "</option>");
                                         }
                                     %>
                                 </select>
                                   <span class="input-group-addon"><i class="fa fa-info"></i></span>
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Estatus:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                     <select class="form-control" name="CidEstatus">
                                     <option value="<%=IDEstatus %>"><%=txtEstatus %> </option>
                                     <% for(int i=0; i < TotalEstatus; i++)
                                         {
                                             Response.Write("<option value='" + ideEstatus[i] + "'>" + validarEstatus[i] + "</option>");
                                         }
                                     %>
                                 </select>
                                   <span class="input-group-addon"><i class="fa fa-info"></i></span>
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Placas:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                         <input class="form-control" type="text" name="Cplacas"  value="<%=Placas %>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                        
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Número de serie:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                  <input class="form-control" type="text" name="Cserie"  value="<%=NumeroSerie %>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                        
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Póliza:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                  <input class="form-control" type="text" name="Cpoliza"  value="<%=PolizaSeguro %>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                        
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Vigencia póliza:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                  <input class="form-control" type="date" name="Cvigencia"  value="<%=Vigencia %>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                        
                                </div>
                            </div>
                        </div>
                      
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Propietario vehículo:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                    <select class="form-control" name="CidProv">
                                     <option value="<%=IDPropietario %>"><%=txtPropGps %> </option>
                                     <% for (int i = 0; i < TotalPropietarioGps; i++)
                                             {
                                                 Response.Write("<option value='" + ideProvedorGps[i] + "'>" + validarProvGP[i] + "</option>");
                                         }
                                     %>
                                 </select>
                                   <span class="input-group-addon"><i class="fa fa-info"></i></span>
                                </div>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">GPS:</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                     <select class="form-control" name="CidGps">
                                     <option value="<%=IDGps %>"><%=txtGPS %> </option>
                                     <% for(int i=0; i < TotalGps; i++)
                                         {
                                             Response.Write("<option value='" + ideGps[i] + "'>" + validarGPS[i] + "</option>");
                                         }
                                     %>
                                 </select>
                                   <span class="input-group-addon"><i class="fa fa-info"></i></span>
                                </div>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Revisión mecánica :</label>
                            <div class='col-sm-10'>
                                <div class="input-group">
                                  <input class="form-control" type="datetime-local" name="Cmecanica"  value="<%=FechaMecanica %>" />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                        
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