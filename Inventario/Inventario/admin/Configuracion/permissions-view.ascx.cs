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
        private int numeropaginas = 0, paginaas = 0, r1 = 0, r2 = 0, r3 = 0, inicio = 0, totalModulo, totalSubdmodulo, totalSubsubmodulo, permissionUser = 0;
        private string aler = "", consulta = "", mens = "", rol = "", nombrepagina = "searchPermissions";
        private int[] idModuloUser, idSubmoduloUser, idSubsubmodulouser;
        private string[] valModuloUser, valSubmoduloUser, valSubsubmodulouser;
        private string[][] valAccion, moduloNombreAccion;
        private int[][] IdAccion;
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
        public int row3 { set { r3 = value; } get { return r3; } }
        public string alerta { set { aler = value; } get { return aler; } }
        public string query { set { consulta = value; } get { return consulta; } }
        public string mensaje { set { mens = value; } get { return mens; } }
        public string TipoRol { set { rol = value; } get { return rol; } }
        //Arreglos
        public int[] idModuloUserArray { set { idModuloUser = value; } get { return idModuloUser; } }
        public int[] idSubmoduloUserArray { set { idSubmoduloUser = value; } get { return idSubmoduloUser; } }
        public int[] idSubsubmoduloUserArray { set { idSubsubmodulouser = value; } get { return idSubsubmodulouser; } }
        public string[] valModuloUserArray { set { valModuloUser = value; } get { return valModuloUser; } }
        public string[] valSubmoduloUserArray { set { valSubmoduloUser = value; } get { return valSubmoduloUser; } }
        public string[] valSubsubmoduloUserArray { set { valSubsubmodulouser = value; } get { return valSubsubmodulouser; } }
        //Arreglos de jagged2 ( tipo Matrices)
        public string[][] valAccionUserArray { set { valAccion = value; } get { return valAccion; } }
        public int[][] idAccionUserArray { set { IdAccion = value; } get { return IdAccion; } }
        public string[][] moduloNombreAccionArray { set { moduloNombreAccion = value; } get { return moduloNombreAccion; } }
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
        private void BindModulosGrid() {
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
        private void BindSubmodulosGrid(GridView gridViewSubmodulos) // Este metodo se encarga de mostrar los nombres de modulos, permisos y el boton que mostrara submodulos en otro metodo
        { try
            { Tuple<List<object[]>, int> PermisosUser, Acciones, Extras;
                consulta = "SELECT DISTINCT m.id_modulo, m.nombre as Modulo FROM " + PermisosMysql.LinkedServer + " ...  modulo m INNER JOIN " + PermisosMysql.LinkedServer + "... permisos p ON p.id_modulo = m.id_modulo INNER JOIN " + PermisosMysql.LinkedServer + "... cliente c On p.id_usuario = c.id_cliente WHERE(p.id_app = 5470 AND m.id_modulo <> 99999) ORDER BY m.nombre";
                PermisosUser = PermisosMysql.Consulta(ref mens, consulta);
                if (PermisosUser.Item2 >= 1)
                { TM = PermisosUser.Item2;
                    // Valores de modulos en general
                    valModuloUserArray = new string[TM];
                    idModuloUserArray = new int[TM];
                    // Declaración de acciones usuario por modulo 
                    valAccionUserArray = new string[TM][];
                    idAccionUserArray = new int[TM][];
                    moduloNombreAccionArray = new string[TM][];
                    List<int> tempIdAcciones = new List<int>();
                    List<string> tempValAcciones = new List<string>();
                    for (int a = 0; a < TM; a++)
                    {  // Lleno arreglos de modulos general
                        valModuloUserArray[a] = Convert.ToString(PermisosUser.Item1[a][1]);
                        idModuloUserArray[a] = Convert.ToInt32(PermisosUser.Item1[a][0]);
                    }
                    for (int b = 0; b < idModuloUserArray.Length; b++)
                    {
                        consulta = "SELECT DISTINCT ac.id_accion, ac.nombre, m.id_modulo, m.nombre  FROM " + PermisosMysql.LinkedServer + " ...  AccionesPermiso ac INNER JOIN " + PermisosMysql.LinkedServer + " ... permisos p ON ac.id_accion = p.id_accionespermiso INNER JOIN " + PermisosMysql.LinkedServer + "  ... aplicacion ap on ap.id_app = p.id_app INNER JOIN " + PermisosMysql.LinkedServer + " ... cliente c on p.id_usuario = c.id_cliente  INNER JOIN " + PermisosMysql.LinkedServer + " ... modulo m ON m.id_modulo = p.id_modulo INNER JOIN " + PermisosMysql.LinkedServer + " ... submodulo s ON  s.id_submodulo = p.id_submodulo WHERE (c.id_cliente = " + PermissionsId + " AND ap.id_app = 5470)  AND (m.id_modulo = " + idModuloUserArray[b] + "  AND p.id_submodulo=99999) ORDER BY m.nombre";
                        Acciones = PermisosMysql.Consulta(ref mens, consulta);
                        if (Acciones.Item2 >= 1)
                        { // usuario con permisos existentes en modulos
                            moduloNombreAccionArray[b] = new string[Acciones.Item2];
                            valAccionUserArray[b] = new string[Acciones.Item2];
                            idAccionUserArray[b] = new int[Acciones.Item2];
                            for (int c = 0; c < Acciones.Item2; c++)
                            { // accionId, nombre de la accion y modulo nombre en caso de que si existan 
                                idAccionUserArray[b][c] = Convert.ToInt32(Acciones.Item1[c][0]);
                                valAccionUserArray[b][c] = Convert.ToString(Acciones.Item1[c][1]);
                                moduloNombreAccionArray[b][c] = (Acciones.Item1[c][3]).ToString();
                                consulta = "SELECT DISTINCT id_accion, nombre FROM " + PermisosMysql.LinkedServer + " ... AccionesPermiso WHERE id_accion <> " + idAccionUserArray[b][c] + " order by nombre desc";
                                Extras = PermisosMysql.Consulta(ref mens, consulta);
                                if (Extras.Item2 >= 1)
                                {
                                    for (int d = 0; d < 2; d++)
                                    {
                                        tempIdAcciones.Add(Convert.ToInt32(Extras.Item1[d][0]));
                                        tempValAcciones.Add(Convert.ToString(Extras.Item1[d][1]));
                                    }
                                }
                                idAccionUserArray[b] = idAccionUserArray[b].Concat(tempIdAcciones.ToArray()).ToArray();
                                valAccionUserArray[b] = valAccionUserArray[b].Concat(tempValAcciones.ToArray()).ToArray();
                                tempIdAcciones.Clear();
                                tempValAcciones.Clear();
                            }
                        }
                        else // usuario sin permiso alguno en modulos 
                        {
                            consulta = "SELECT DISTINCT id_accion, nombre FROM " + PermisosMysql.LinkedServer + " ... AccionesPermiso ORDER BY nombre DESC";
                            Acciones = PermisosMysql.Consulta(ref mens, consulta);
                            if (Acciones.Item2 >= 1)
                            {
                                valAccionUserArray[b] = new string[Acciones.Item2];
                                idAccionUserArray[b] = new int[Acciones.Item2];
                                for (int c = 0; c < Acciones.Item2; c++)
                                {
                                    idAccionUserArray[b][c] = Convert.ToInt32(Acciones.Item1[c][0]);
                                    valAccionUserArray[b][c] = Convert.ToString(Acciones.Item1[c][1]);
                                }
                            }
                        }
                    }
                    // Asignación de DataSource al GridView
                    gridViewSubmodulos.DataSource = PermisosUser.Item1.Select((x, index) => new
                    {
                        ModuloId = x[0], // id Modulo, por ejemplo, 8432
                        ModuloNombre = x[1], // Nombre Modulo, por ejemplo, Configuración
                        AccionNombre = (valAccionUserArray.Length > index && valAccionUserArray[index] != null) ? valAccionUserArray[index] : null, // Asegurarse de que el array no sea nulo
                        ModulosNombres = (valModuloUserArray.Length > index) ? valModuloUserArray[index] : null, // Asegurarse de que el array no sea nulo y verificar el índice
                        IdModulos = (idModuloUserArray.Length > index) ? idModuloUserArray[index] : 0, // Verificación del tamaño antes de acceder
                        IdAcciones = (idAccionUserArray.Length > index && idAccionUserArray[index] != null && idAccionUserArray[index].Length > 0)
                        ? idAccionUserArray[index][0] : 0 // Verificar que el array no sea nulo y que haya elementos en el array antes de acceder
                    });
                    // Vincular los datos al GridView
                    gridViewSubmodulos.DataBind();
                }
            }
            catch (Exception c) {
                string MensajeAlerta = "";
                MensajeAlerta = "ERROR:" + c.Message;
            }
        }
        private void BindSubSubmodulosGrid(GridView gridViewSubSubmodulos)
        {
            try
            {
                Tuple<List<object[]>, int> PermisosUser, Extras, Submodulos;

                consulta = "SELECT DISTINCT m.id_modulo, m.nombre as Modulo FROM " + PermisosMysql.LinkedServer + " ...  modulo m INNER JOIN " + PermisosMysql.LinkedServer + "... permisos p ON p.id_modulo = m.id_modulo INNER JOIN " + PermisosMysql.LinkedServer + "... cliente c On p.id_usuario = c.id_cliente WHERE(p.id_app = 5470 AND m.id_modulo <> 99999) ORDER BY m.nombre";
                PermisosUser = PermisosMysql.Consulta(ref mens, consulta);
                Submodulos = PermisosUser; // parche 
                if (PermisosUser.Item2 >= 1)
                {
                    TM = PermisosUser.Item2;
                    // Valores de modulos en general
                    valModuloUserArray = new string[TM];
                    idModuloUserArray = new int[TM];
                    // Declaración de acciones usuario por modulo 
                    valAccionUserArray = new string[TM][];
                    idAccionUserArray = new int[TM][];
                    moduloNombreAccionArray = new string[TM][];
                    List<int> tempIdAcciones = new List<int>();
                    List<string> tempValAcciones = new List<string>();
                    for (int a = 0; a < TM; a++)
                    {  // Lleno arreglos de modulos general
                        valModuloUserArray[a] = Convert.ToString(PermisosUser.Item1[a][1]);
                        idModuloUserArray[a] = Convert.ToInt32(PermisosUser.Item1[a][0]);
                        consulta = "SELECT DISTINCT ac.id_accion, ac.nombre, s.id_submodulo, s.nombre  FROM " + PermisosMysql.LinkedServer + " ...  AccionesPermiso ac INNER JOIN " + PermisosMysql.LinkedServer + " ... permisos p ON ac.id_accion = p.id_accionespermiso INNER JOIN " + PermisosMysql.LinkedServer + " ... aplicacion ap on ap.id_app = p.id_app INNER JOIN " + PermisosMysql.LinkedServer + " ... cliente c on p.id_usuario = c.id_cliente  INNER JOIN " + PermisosMysql.LinkedServer + " ... modulo m ON m.id_modulo = p.id_modulo INNER JOIN " + PermisosMysql.LinkedServer + " ... submodulo s ON  s.id_submodulo = p.id_submodulo WHERE (c.id_cliente = " + PermissionsId + " AND ap.id_app = 5470)  AND (m.id_modulo = " + idModuloUserArray[a] + "  AND p.id_submodulo<>99999) ORDER BY s.nombre";
                        Submodulos = PermisosMysql.Consulta(ref mens, consulta);
                        if (Submodulos.Item2 >= 1)
                        {
                            moduloNombreAccion[a] = new string[Submodulos.Item2];
                            valAccionUserArray[a] = new string[Submodulos.Item2];
                            idAccionUserArray[a] = new int[Submodulos.Item2];
                            for (int b = 0; b < Submodulos.Item2; b++)
                            {
                                moduloNombreAccionArray[a][b] = (Submodulos.Item1[b][3]).ToString();
                                valAccionUserArray[a][b] = Convert.ToString(Submodulos.Item1[b][1]);
                                idAccionUserArray[a][b] = Convert.ToInt32(Submodulos.Item1[b][0]);
                             
                                consulta = "SELECT DISTINCT id_accion, nombre FROM " + PermisosMysql.LinkedServer + " ... AccionesPermiso WHERE id_accion <> " + idAccionUserArray[a][b] + " order by nombre desc";
                                Extras = PermisosMysql.Consulta(ref mens, consulta);
                                if (Extras.Item2 >= 1)
                                {
                                    for (int c = 0; c < 2; c++)
                                    {
                                        tempIdAcciones.Add(Convert.ToInt32(Extras.Item1[c][0]));
                                        tempValAcciones.Add(Convert.ToString(Extras.Item1[c][1]));
                                    }
                                }
                                idAccionUserArray[a] = idAccionUserArray[a].Concat(tempIdAcciones.ToArray()).ToArray();
                                valAccionUserArray[a] = valAccionUserArray[a].Concat(tempValAcciones.ToArray()).ToArray();
                                tempIdAcciones.Clear();
                                tempValAcciones.Clear();
                            }
                        }

                    }
                    gridViewSubSubmodulos.DataSource =  Submodulos.Item1.Select((x, index) => new
                    {
                        SubModuloId = x[0], // id Modulo, por ejemplo, 8432
                        SubModuloNombre = x[3], // Nombre Modulo, por ejemplo, Configuración
                        AccionNombre = (valAccionUserArray.Length > index && valAccionUserArray[index] != null) ? valAccionUserArray[index] : null, // Asegurarse de que el array no sea nulo
                        SubModulosNombres = (valModuloUserArray.Length > index) ? valModuloUserArray[index] : null, // Asegurarse de que el array no sea nulo y verificar el índice
                        IdSubModulos = (idModuloUserArray.Length > index) ? idModuloUserArray[index] : 0, // Verificación del tamaño antes de acceder
                        IdAcciones = (idAccionUserArray.Length > index && idAccionUserArray[index] != null && idAccionUserArray[index].Length > 0)
                      ? idAccionUserArray[index][0] : 0 // Verificar que el array no sea nulo y que haya elementos en el array antes de acceder
                    });
                    // Vincular los datos al GridView
                    gridViewSubSubmodulos.DataBind();
                }
            }
            catch (Exception c)
            {
                alerta = "ERROR:" + c.Message;
            }
        }
        protected void GridViewModulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowSubmodulo"){
                string[] args = e.CommandArgument.ToString().Split(',');
                if (args.Length == 2){   
                    int index = Convert.ToInt32(args[0]);
                    int idUsuarioPermisos = Convert.ToInt32(args[1]);
                    PermissionsId = idUsuarioPermisos;
                    ViewState["PermissionsId"] = PermissionsId; // Guardarlo en ViewState.
                    ViewState["IndexModulos"] = index;
                    GridViewRow row = GridViewModulos.Rows[index];
                    Panel panelSubmodulo = (Panel)row.FindControl("PanelSubmodulo");
                    GridView gridViewSubmodulos = (GridView)row.FindControl("GridViewSubmodulos");
                    UpdatePanel updatePanelSubmodulo = (UpdatePanel)row.FindControl("UpdatePanelSubmodulo");
                    if (panelSubmodulo != null && gridViewSubmodulos != null && updatePanelSubmodulo != null)
                    {   BindSubmodulosGrid(gridViewSubmodulos);
                        if (panelSubmodulo.Style["display"] == "none" || string.IsNullOrEmpty(panelSubmodulo.Style["display"])){
                            panelSubmodulo.Style["display"] = "block";
                        }
                        else{       
                            panelSubmodulo.Style["display"] = "none";
                        }
                        updatePanelSubmodulo.Update();
                    }
                } 
            } 
        }
        protected void GridViewSubmodulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try {
                if (e.CommandName == "ShowSubsubmodulo")
                {
                    string[] args = e.CommandArgument.ToString().Split(',');
                    if (args.Length == 1)
                    {
                        int indexHijo = Convert.ToInt32(args[0]);
                        int indexPadre = (int)ViewState["IndexModulos"];
                        PermissionsId = (int)ViewState["PermissionsId"];
                        GridViewRow rowPadre = GridViewModulos.Rows[indexPadre];
                        GridView gridViewSubmodulos = (GridView)rowPadre.FindControl("GridViewSubModulos");
                        GridViewRow rowHijo = gridViewSubmodulos.Rows[indexHijo];
                        Panel panelSubsubmodulo = (Panel)rowHijo.FindControl("PanelSubSubmodulo");
                        GridView gridViewSubsubmodulos = (GridView)rowHijo.FindControl("GridViewSubSubmodulos");
                        UpdatePanel updatePanelSubsubmodulo = (UpdatePanel)rowHijo.FindControl("UpdatePanelSubSubmodulo");
                        if (panelSubsubmodulo!= null && gridViewSubsubmodulos!=null && updatePanelSubsubmodulo != null)
                        {
                            BindSubSubmodulosGrid(gridViewSubsubmodulos);
                            if (panelSubsubmodulo.Style["display"] == "none" || string.IsNullOrEmpty(panelSubsubmodulo.Style["display"])){
                                panelSubsubmodulo.Style["display"] = "block";

                            }else{
                                panelSubsubmodulo.Style["display"] = "none";
                            }
                            updatePanelSubsubmodulo.Update();
                        }
                    }
                }
            }
            catch(Exception c)
            {
                alerta = "ERROR:" + c.Message;
            }
        }
        protected void GridViewSubmodulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList SelectAccion = (DropDownList)e.Row.FindControl("SelectNombre");
                string Modulonombre = DataBinder.Eval(e.Row.DataItem, "ModuloNombre").ToString();
                int IdModulo = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ModuloId"));
                Button btnMostrarSubsubmodulo = (Button)e.Row.FindControl("btnMostrarSubsubmodulo");
                if (SelectAccion != null)
                {
                    SelectAccion.Items.Clear();
                    int rowIndex = e.Row.RowIndex; // Obtener el índice de la fila actual
                    if (valAccionUserArray.Length > rowIndex)
                    {
                        for (int i = 0; i < valAccionUserArray[rowIndex].Length; i++)
                        {
                            if (idAccionUserArray[rowIndex][0] == 567 || valAccionUserArray[rowIndex][0].Contains("NO PERMITIDO"))
                            {
                                btnMostrarSubsubmodulo.Visible = false;
                            }
                            else
                            {
                                btnMostrarSubsubmodulo.Visible = true;
                            }
                            string accionNombre = valAccionUserArray[rowIndex][i];
                            string accionId = idAccionUserArray[rowIndex][i].ToString();
                            SelectAccion.Items.Add(new ListItem(accionNombre, accionId));
                            SelectAccion.Attributes["data-nombreModulo"] = Modulonombre;
                            ViewState["idModulo"] = IdModulo;
                        }
                    }
                }
            }
        }
        protected void GridViewSubSubmodulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                DropDownList SelectActionSubsubmodulo = (DropDownList)e.Row.FindControl("SelectNombre2");
                string SubmoduloNombre = DataBinder.Eval(e.Row.DataItem, "SubModuloNombre").ToString();
                int IdSubmodulo = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem,"SubModuloId"));
                Button btnSubsubmodulo = (Button)e.Row.FindControl("btnMostrarSubsubmodulo");
                if (SelectActionSubsubmodulo != null)
                {
                    SelectActionSubsubmodulo.Items.Clear();
                    int rowIndex = e.Row.RowIndex;
                    if (valAccionUserArray.Length > rowIndex)
                    {
                       /* for (int i = 0; i < valAccionUserArray[rowIndex].Length; i++)
                        {
                            if (idAccionUserArray[rowIndex][0] == 567 || valAccionUserArray[rowIndex][0].Contains("NO PERMITIDO"))
                            {
                                btnSubsubmodulo.Visible = false;
                            }
                            else
                            {
                                btnSubsubmodulo.Visible = true;
                            }
                            string accionNombre = valAccionUserArray[rowIndex][i];
                            string accionId = idAccionUserArray[rowIndex][i].ToString();
                            SelectActionSubsubmodulo.Items.Add(new ListItem(accionNombre, accionId));
                            SelectActionSubsubmodulo.Attributes["data-nombreModulo"] = SubmoduloNombre;
                            ViewState["idSubModulo_" + rowIndex] = IdSubmodulo;
                        }*/
                    }
                }
            
            }
           

        }
        protected void SelectNombre_SelectedIndexChanged(object sender, EventArgs e)
        {    
            try
            {
                DropDownList SelectAction = (DropDownList)sender;  // Usa NamingContainer para obtener la fila actual del GridView        
                GridViewRow row = (GridViewRow)SelectAction.NamingContainer;
                Button ButtonSubsubmodulos = (Button)row.FindControl("btnMostrarSubsubmodulo");
                Control currentControl = row;

                int rowIndex = row.RowIndex;   // Obtener el índice de la fila actual
                int selectedValue = Convert.ToInt32(SelectAction.SelectedValue);
                string nombreM = SelectAction.Attributes["data-nombreModulo"];
                int idModulo = (int)ViewState["idModulo"];
                if (ViewState["PermissionsId"] != null){
                    PermissionsId = (int)ViewState["PermissionsId"];
                }
                consulta = "SELECT * FROM " + PermisosMysql.LinkedServer + " ... permisos WHERE id_usuario =  " + PermissionsId + " AND (id_modulo = " + idModulo + " AND id_submodulo= 99999)";
                Tuple<List<object[]>, int> UserModulo = PermisosMysql.Consulta(ref mens, consulta);
                if (UserModulo.Item2 == 0) {
                    if (PermisosMysql.Insertar("permisos", PermisosMysql.LinkedServer, "id_usuario,id_app,id_modulo,id_submodulo,id_subsubmodulo, id_accionespermiso", $"{PermissionsId},5470,{idModulo},99999,99999,{selectedValue}", ref mens))
                    {
                        ButtonSubsubmodulos.Visible = true;
                        while (currentControl != null && !(currentControl is UpdatePanel)){
                        currentControl = currentControl.Parent;
                        }
                        if (currentControl is UpdatePanel updatePanelSubmodulo){
                            // Actualizar el UpdatePanel
                            updatePanelSubmodulo.Update();
                        }
                    }       
                }
                else if (UserModulo.Item2 == 1){
                      int idPermiso = Convert.ToInt32(UserModulo.Item1[0][1]); 
                        consulta = "SELECT * FROM " + PermisosMysql.LinkedServer + " ... permisos WHERE id_usuario = " + PermissionsId  + " AND (id_modulo = " + idModulo    + " AND id_permiso = " + idPermiso +")";
                        Tuple<List<object[]>, int> PermisosInfo = PermisosMysql.Consulta(ref mens, consulta);
                        int Verifica1 = 0;
                        Verifica1 = Convert.ToInt32(PermisosInfo.Item1[0][1]);    
                        if (idPermiso == Verifica1 ) 
                        {
                           PermisosMysql.Actualizar(PermisosMysql.LinkedServer, "permisos", "id_accionespermiso=" + selectedValue, "id_permiso=" + PermisosInfo.Item1[0][1]);           
                           if(selectedValue == 567){
                                    ButtonSubsubmodulos.Visible = false;
                       
                                    while (currentControl != null && !(currentControl is UpdatePanel))
                                    {
                                        currentControl = currentControl.Parent;
                                    }
                                    if (currentControl is UpdatePanel updatePanelSubmodulo)
                                    {
                                        // Actualizar el UpdatePanel
                                        updatePanelSubmodulo.Update();
                                    }
                           }
                           else
                           {
                                ButtonSubsubmodulos.Visible = true;
                                while (currentControl != null && !(currentControl is UpdatePanel))
                                {
                                    currentControl = currentControl.Parent;
                                }
                                if (currentControl is UpdatePanel updatePanelSubmodulo)
                                {
                                    // Actualizar el UpdatePanel
                                    updatePanelSubmodulo.Update();
                                }
                           }

                    }
                }
            }
            catch (Exception c){ 
                alerta = "ERROR: " + c.Message;
            }
        }
        protected void SelectNombre2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}