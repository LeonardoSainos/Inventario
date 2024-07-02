<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCar.ascx.cs" Inherits="Inventario.Inventario.admin.Actions.AddCar" %>
 <%Response.Write(alerta); %>
<div class="container">
    <div class="container">
        <div class="modal" tabindex="-1" id="modal1">
            <div class="modal-dialog modal-xlg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header" style="background:black; text-align:center;">
                        <button class="close" data-dismiss="modal">&times;</button>
                        <h1 style="color:white;"> Agregar nuevo vehículo </h1>
                    </div> 
                    <div class="modal-body">
                        <form id="add" class="formu"   method="post">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo:</label>
                                  <div class='col-sm-10'>
                                      <div class="input-group">
                                          <%Response.Write("<select   required='' class='formu form-control' name='Gtypee'>");
                                              for (int i = 0;i<TotTy; i++)
                                              {
                                                  Response.Write("<option value='" + idTipos[i] + "'>" + valorTi[i] + "</option>");
                                              }
                                              Response.Write("</select>");
                                          %>
                                      <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                      </div>
                                  </div>
                            </div>
                            <br />
                            <br /> 
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Marca:</label>
                                <div class="col-sm-10">
                                    <div class="input-group">
                                          <%Response.Write("<select required='' class='formu form-control' name='Gmarca'>");
                                              for (int j = 0;j<TotalMa; j++)
                                              {
                                                  Response.Write("<option value='" + idMarcas[j] + "'>" + valorMa[j] + "</option>");
                                              }
                                              Response.Write("</select>");
                                          %>
                                       <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                    </div>
                                </div>
                            </div>
                            <br /><br /> 
                                <div class="form-group">
                                <label class="col-sm-2 control-label">Modelo:</label>
                                <div class="col-sm-10">
                                    <div class="input-group">
                                          <%Response.Write("<select required='' class='formu form-control' name='Gmodelo'>");
                                              for (int k = 0; k< TotalMode; k++)
                                              {
                                                  Response.Write("<option value='" + idModelo[k] + "'>" + valModelo[k] + "</option>");
                                              }
                                              Response.Write("</select>");
                                          %>
                                       <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                    </div>
                                </div>
                            </div>
                              <br /><br /> 
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Número económico:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                               <input id="economico" value="<%  %>" required=""  class="formu form-control" type="text" name="Geconomico" maxlength="70"/>
                                              <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div>
                            <br /><br /> 

                            <div class="form-group">
                                <label class="col-sm-2 control-label">Placas:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                               <input  placeholder="Placas" class="formu form-control" type="text" name="Gplacas" maxlength="70"/>
                                              <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div> <br /> <br />
                           <div class="form-group">
                                <label class="col-sm-2 control-label">Serie:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                               <input  placeholder="Número de serie" class="formu form-control" type="text" name="Gserie" maxlength="70"/>
                                              <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div>
                            <br /><br/>
                              <div class="form-group">
                                <label class="col-sm-2 control-label">Póliza:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                               <input  placeholder="Póliza de seguro" class="formu form-control" type="text" name="Gpoliza" maxlength="70"/>
                                              <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div><br /><br/>
                           <div class="form-group">
                                <label class="col-sm-2 control-label">Vigencia:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                               <input   class="formu form-control" type="date" name="Gvigencia" maxlength="70"/>
                                              <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div><br /><br /> 
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Estatus:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                                <%Response.Write("<select required='' class='formu form-control' name='Gestatus'>");
                                              for (int j = 0; j < Totest; j++)
                                              {
                                                  Response.Write("<option value='" +  ideEstatus[j] + "'>" + valorEst[j] + "</option>");
                                              }
                                              Response.Write("</select>");
                                          %>
                                      
                                             <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div><br /><br />
                              <div class="form-group">
                                <label class="col-sm-2 control-label">Provedor GPS:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                                <%Response.Write("<select required='' class='formu form-control' name='Gprovedorgps'>");
                                              for (int j = 0; j < TotalPROGps; j++)
                                              {
                                                  Response.Write("<option value='" +  idprogps[j] + "'>" + valprogps[j] + "</option>");
                                              }
                                              Response.Write("</select>");
                                          %>
                                      
                                             <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div><br /><br />
                             <div class="form-group">
                                <label class="col-sm-2 control-label">GPS:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                                <%Response.Write("<select required='' class='formu form-control' name='Ggps'>");
                                              for (int j = 0; j < TotalGP; j++)
                                              {
                                                  Response.Write("<option value='" +  idGPSS[j] + "'>" + valiGps[j] + "</option>");
                                              }
                                              Response.Write("</select>");
                                          %>
                                      
                                             <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div><br /><br />
                              <div class="form-group">
                                <label class="col-sm-2 control-label">Asignar a:</label>
                                 <div class='col-sm-10'>
                                  <div class="input-group">
                                                <%Response.Write("<select  class='formu form-control' name='Goperador'> <option value='N/A' >NO ASIGNAR</option>");
                                                 

                                              for (int j = 0; j < TotalOper; j++)
                                              {
                                                  Response.Write("<option value='" +  idOperador[j] + "'>" + valOper[j] + "</option>");
                                              }
                                              Response.Write("</select>");
                                          %>
                                      
                                             <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                        </div>
                                 </div>
                            </div><br /><br />

                    
                            <div class="modal-footer">
                                <input class="btn btn-warning" type="submit" value="Crear Vehículo" />
                                <input class="btn btn-success" onclick="funcion_reiniciar('add');" type="button" value="Restablecer" />
                                <button class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                            </div>

                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>