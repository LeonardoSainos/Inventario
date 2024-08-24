using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using System.Data;
using Inventario.Scripts;
namespace Inventario.Inventario.admin.Configuracion
{
    public partial class permissions_view : System.Web.UI.UserControl
    {
        MySql PermisosMysql = new MySql();
        CONEXION Permisos = new CONEXION();
        private int numeropaginas = 0, paginaas = 0, r1 = 0, r2 = 0, r3 = 0, inicio = 0, totalModulo, totalSubdmodulo, totalSubsubmodulo, permissionUser=0;
        private string aler = "", consulta = "", mens = "", rol = "", nombrepagina = "searchPermissions";
        private int[] idModuloUser, idSubmoduloUser, idSubsubmodulouser;
        private string[] valModuloUser, valSubmoduloUser, valSubsubmodulouser;
        public string PaginaNombre { get { return nombrepagina; } }
        public int PermissionsId { get { return permissionUser; } set { permissionUser = value; } }
        public int inicializacion { set { inicio = value; } get { return inicio; } }
        public int numPagina { set { numeropaginas = value; } get { return numeropaginas; } }
        public int pagina { set { paginaas = value; } get { return paginaas; } }
        public int row1 { set { r1 = value; } get { return r1; } }
        public int row2 { set { r2 = value; } get { return r2; } }      
        public int TM { set { totalModulo = value; } get { return totalModulo; } }
        public int TS { set { totalSubdmodulo = value; } get { return totalSubdmodulo; } }
        public int TSS { set { totalSubsubmodulo = value; } get { return totalSubsubmodulo; } }
        public int[] idModuloUserArray { set { idModuloUser = value; } get { return idModuloUser; } }
        public int[] idSubmoduloUserArray { set { idSubmoduloUser = value; } get { return idSubmoduloUser; } }
        public int[] idSubsubmoduloUserArray { set { idSubsubmodulouser = value; } get { return idSubsubmodulouser; } }
        public string[] valdModuloUserArray { set { valModuloUser = value; } get { return valModuloUser; } }
        public string[] valSubmoduloUserArray { set { valSubmoduloUser = value; } get { return valSubmoduloUser; } }
        public string[] valSubsubmoduloUserArray { set { valSubsubmodulouser = value; } get { return valSubsubmodulouser; } }
        public int row3 { set { r3 = value; } get { return r3; } }
        public string alerta { set { aler = value; } get { return aler; } }
        public string query { set { consulta = value; } get { return consulta; } }
        public string mensaje { set { mens = value; } get { return mens; } }
        public string TipoRol { set { rol = value; } get { return rol; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            consulta = "SELECT COUNT(*) AS contador FROM " + PermisosMysql.LinkedServer + " ... cliente";
            Tuple<List<object[]>, int> usuarios = PermisosMysql.Consulta(ref mens, consulta);
            if (usuarios.Item2 > 0)
            {
                row1 = Convert.ToInt32(usuarios.Item1[0][0]);
            }
            if (!IsPostBack)
            { BindModulosGrid();
            }
        }
        private void BindModulosGrid()
        {
            if (!String.IsNullOrEmpty(Request.Form["id_clientePermission"]))
            {
                PermissionsId = Convert.ToInt32(Functions.RequestPost(Request.Form["id_clientePermission"]));
            }

            string[] orderby = { "c.nombre_completo", "c.email_cliente" };
            string ordenamuestra = orderby[0];
            int tipoRol = 0;
            if (Request.QueryString["view"] != "" || Request.QueryString["view"] != null && Request.QueryString[rol] != null)
            {
                string orden = Functions.RequestGet(Request.QueryString[rol]);
                switch (orden)
                {
                    case "Nombre":
                        {
                            ordenamuestra = orderby[0];
                            break;
                        }
                    case "Correo":
                        {
                            ordenamuestra = orderby[1];
                            break;
                        }
                }
                switch (rol)
                {
                    case "admin":
                        {
                            tipoRol = 4046;
                            break;
                        }
                    case "mecanico":
                        {
                            tipoRol = 2736;
                            break;
                        }
                    case "almacenista":
                        {
                            tipoRol = 7845;
                            break;
                        }
                }
            }
            ///// MOSTRAR RESULTADOS
            pagina = HttpContext.Current.Request.QueryString["pagina"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["pagina"]) : 1;
            int regpagina = 50, acaba = pagina * regpagina;
            inicio = (pagina * regpagina) - regpagina;
            string mensaje = "";
            consulta = "SELECT * FROM OPENQUERY(" + PermisosMysql.LinkedServer + ",' SELECT c.id_cliente, c.nombre_completo, c.email_cliente FROM cliente c WHERE c.id_cliente <>999999 ORDER BY " + ordenamuestra + " LIMIT " + inicio + "," + acaba + "')";
            Tuple<List<object[]>, int> res = PermisosMysql.Consulta(ref mensaje, consulta);
            int contador = res.Item2;
            numeropaginas = (int)Math.Ceiling((double)contador / regpagina);
            if (contador >= 1)
            {
                PermisosMysql.Mostrar(GridViewModulos, ref mens, consulta);
            }
        }
        protected void GridViewModulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowSubmodulo")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewModulos.Rows[index];
                Panel panelSubmodulo = (Panel)row.FindControl("PanelSubmodulo");
                GridView gridViewSubmodulos = (GridView)row.FindControl("GridViewSubmodulos");
                UpdatePanel updatePanelSubmodulo = (UpdatePanel)row.FindControl("UpdatePanelSubmodulo");
                Button idUser = (Button)row.FindControl("btnMostrarSubmodulo");
              
                if (panelSubmodulo != null && gridViewSubmodulos != null && updatePanelSubmodulo != null)
                {
                    BindSubmodulosGrid(gridViewSubmodulos, index);

                    if (panelSubmodulo.Style["display"] == "none" || string.IsNullOrEmpty(panelSubmodulo.Style["display"]))
                    {
                        panelSubmodulo.Style["display"] = "block";
                    }
                    else
                    {
                        panelSubmodulo.Style["display"] = "none";
                    }

                    // Esto asegura que solo el UpdatePanel se actualiza sin causar un postback completo.
                    updatePanelSubmodulo.Update();
                }
            }
        }
        private void BindSubmodulosGrid(GridView gridViewSubmodulos, int moduloId)
        {
       
            consulta = "SELECT DISTINCT m.id_modulo, m.nombre, a.nombre  FROM " + PermisosMysql.LinkedServer + " ... cliente c INNER JOIN " + PermisosMysql.LinkedServer + "... permisos p ON c.id_cliente = p.id_usuario INNER JOIN " + PermisosMysql.LinkedServer + "... modulo m ON m.id_modulo = p.id_modulo INNER JOIN " + PermisosMysql.LinkedServer + " ... AccionesPermiso a ON a.id_accion = p.id_accionespermiso WHERE  m.id_modulo <> 99999  AND c.id_cliente=" + PermissionsId;
            Tuple<List<object[]>, int> PermisosUser = PermisosMysql.Consulta(ref mens, consulta);
            TM = PermisosUser.Item2;
            valdModuloUserArray = new string[TM];
            idModuloUserArray = new int[TM];
            for (int a = 0; a < TM; a++)
            {
                valModuloUser[a] = Convert.ToString(PermisosUser.Item1[a][1]);
                idModuloUserArray[a] = Convert.ToInt32(PermisosUser.Item1[a][0]);
            }

            // GridView gridViewSubmodulos = (GridView)panelSubmodulo.FindControl("GridViewSubmodulos");
            gridViewSubmodulos.DataSource = PermisosUser.Item1.Select(x => new { ModuloId = x[0], ModuloNombre = x[1], AccionNombre = x[2] });
            gridViewSubmodulos.DataBind();
        }
        protected void GridViewSubmodulos_RowCommand(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList SelectAccion = (DropDownList)e.Row.FindControl("SelectNombre");
                if (SelectAccion != null)
                {
                    string AccionNombre = DataBinder.Eval(e.Row.DataItem, "AccionNombre").ToString();
                    SelectAccion.Items.Add(new ListItem(AccionNombre));

                }
            }
        }
    }
}