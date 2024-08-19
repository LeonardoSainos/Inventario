using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Scripts;
using Inventario.Inventario.lib;

namespace Inventario.Inventario.admin.Actions
{
    public partial class AddPermission : System.Web.UI.UserControl
    {
        CONEXION AddPermissions = new CONEXION();
        MySql AddPermissionsMysql = new MySql();
        private int UserPermision = 0;
        private string query = "", mens = "", alert = ""; 
        private string [] arrayPermisionsClientModul;
        public string mensaje { get { return mens; } set { mens = value; }}
        public string consulta { get { return query; } set { query = value; }}
        public string alerta{ get { return alert; }set { alert = value; }}
        public int UserP { get { return UserPermision; } set { UserPermision = value; } }
        public string[] PermissionModul { get { return arrayPermisionsClientModul; } set { arrayPermisionsClientModul = value; } }
    
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.Form["id_clientePermission"] != null)
            {
                int IduserPermission = Convert.ToInt32(Functions.RequestPost(Request.Form["id_clientePermission"]));
                // string correoPermission = Functions.RequestPost(Request.Form[""]);

                consulta = "SELECT DISTINCT m.nombre FROM " + AddPermissionsMysql.LinkedServer + " ... cliente c INNER JOIN " + AddPermissionsMysql.LinkedServer + " ... permisos p ON c.id_cliente = p.id_usuario INNER JOIN " + AddPermissionsMysql.LinkedServer + " ... modulo m ON m.id_modulo = p.id_modulo WHERE c.id_cliente = " + IduserPermission + " AND m.id_modulo <> 99999";
                Tuple<List<object[]>, int> permisos = AddPermissionsMysql.Consulta(ref mens, consulta);
            }





        }
    }
}