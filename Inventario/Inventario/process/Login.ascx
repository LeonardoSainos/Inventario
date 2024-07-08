<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Inventario.Inventario.inc.Login" %>
  <!-- /. PAGE WRAPPER  -->
<div class="modal fade" tabindex="-1" role="dialog" id="modalLog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header" style="background:#FB5D14;">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 style="color:white;" class="modal-title text-center text-primary" id="myModalLabel">Inventario Alcomex</h4>
            </div>
            <form  id="login" runat="server" style="margin: 20px;">
                <div class="form-group">
                    <label><span class="glyphicon glyphicon-user"></span>&nbsp;Usuario</label>
                    <asp:TextBox class="form-control" minlength="8" name="nombre_login" runat="server"  placeholder="Escribe tu nombre de usuario o correo electrónico" required="" ID="txtUsuario" ></asp:TextBox>
                </div>
                <div class="form-group">
                    <label><span class="glyphicon glyphicon-lock"></span>&nbsp;Contraseña</label>
                    <asp:TextBox runat="server" type="password" minlength="8" class="form-control" name="contrasena_login" placeholder="Escribe tu contraseña" required="" ID="txtPassword" ></asp:TextBox>
                </div>
                <!--
                <p>¿Cómo iniciarás sesión?</p>
                   <asp:RadioButtonList   ID="rblLogin" runat="server" Height="20px" Width="94px">
                      <asp:ListItem  class="radio" Text="Mecanico" Value="1"  Selected="True"></asp:ListItem>                
                       <asp:ListItem  class="radio" Text="Almacenista" Value="2"></asp:ListItem>                                                         
                        <asp:ListItem class="radio" Text="Administrador" Value="3"></asp:ListItem> 
                   </asp:RadioButtonList>-->
                <div class="modal-footer">
                    <asp:button runat="server"   Text="Iniciar sesión" class="btn btn-warning btn-sm" OnClick="Iniciar"></asp:button>
                    <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancelar</button>
                </div>
            </form>
        </div>
    </div>
</div>