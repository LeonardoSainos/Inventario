<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin-view.ascx.cs" Inherits="Inventario.Inventario.admin.WebUserControl1" %>
 <form runat="server">
<div id="contenido">
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
                <img src="../img/card_identy.png" alt="Animacion" class="img-responsive animated flipInY" />
            </div>
            <div class="col-sm-10">
                <p class="lead text-info">Bienvenido administrador, en esta página se muestran todos los <strong>Usuarios </strong> registrados en soporte técnico Alcomex, usted podrá eliminarlos si lo desea.</p>
            </div>
        </div>
    </div>
</div>
<br /><br />
<div class='btn-group'>
    <button class='btn dropdown-toggle btn-warning' data-toggle='dropdown' value='Más'>
        Más
        <span class='caret'></span>
    </button>
    <ul class='dropdown-menu'>
        <li><span style='margin-left:22px' class='glyphicon glyphicon-user'></span> <button runat="server" class="btn btn-link" style='text-decoration:none;' data-toggle='modal' data-target='#modal1'>Nuevo usuario</button> </li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-trash'></span> <asp:TextBox runat="server" class='btn btn-link' style='text-decoration:none;' name="Eliminar">Eliminar</asp:TextBox></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-ban-circle'></span> <asp:LinkButton runat="server" class='btn btn-link ' style='text-decoration:none;' name="Bloquear">Bloquear</asp:LinkButton></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-refresh'></span> <asp:LinkButton runat="server" class='btn btn-link ' style='text-decoration:none;' name="Desbloquear">Desbloquear</asp:LinkButton></li>
        <li><span style='margin-left:22px;' class='glyphicon glyphicon-user'></span> <asp:LinkButton runat="server" class='btn btn-link ' style='text-decoration:none;' name="Exportar">Exportar</asp:LinkButton></li>
        <li><asp:LinkButton runat="server" class='btn btn-link ' style='text-decoration:none;' name="Resetear" >Resetear contraseña</asp:LinkButton></li>
    </ul>
</div>

     </form>
 