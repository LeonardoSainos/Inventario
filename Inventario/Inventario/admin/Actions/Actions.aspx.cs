using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Scripts;
namespace Inventario.Inventario.admin.Actions
{
    public partial class Actions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MySql Acciones = new MySql();
            int SessionId = Convert.ToInt32(Session["id"]);
            int bloquear = Convert.ToInt32(MySql.RequestPost(Request.Form["Bloquear"]));
            int desbloquear = Convert.ToInt32(MySql.RequestPost(Request.Form["Desbloquear"]));
            int eliminar = Convert.ToInt32(MySql.RequestPost(Request.Form["Eliminar"]));
            int resetear = Convert.ToInt32(MySql.RequestPost(Request.Form["Resetear"]));
            DateTime hoy = DateTime.Now;
            string ahora = Convert.ToString(hoy);


            if (Session["nombre"] != null && Session["id_rol"].ToString() != "404" && Session["id"] != null)
            {
                int userDelete = Convert.ToInt32(MySql.RequestPost(Request.Form["id"]));
                if (Request.Form["Bloquear"] != null)
                {
                    if (Request.Form["Usuarios"] != null)
                    {


                        foreach (int Usuario in HttpContext.Current.Request.Form["Usuarios"])
                        {
                            Acciones.Actualizar("mysql_ticket", "cliente", "idEstatus=25542", "id_cliente = " + Usuario);
                            Acciones.ProcedimientoAlmacenado("registro_alteracionesCliente", "mysql_ticket", "" + SessionId + ",\'Actualizar\',\'" + ahora + "\'," + "\'cliente\'");

                        }
                        Response.Write("<script>alert('Usuario bloqueado'); window.history.go(-1); </script>");
                    }
                    else if (HttpContext.Current.Request.Form["Usuarios"] != null)
                    {
                        Response.Write("<script> alert('No haz seleccionado ningún usuario'); window.history.go(-1);  </ script > ");
                    }


                }


            }
        }
    }
}