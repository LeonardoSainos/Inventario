using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Scripts;
using Inventario.Inventario.lib;
namespace Inventario.Inventario.admin.TipoVehiculos
{
    public partial class searchTipos_view : System.Web.UI.UserControl
    {
        CONEXION searchTypesView = new CONEXION();
        Functions Funciones = new Functions();
        private int numeropaginas = 0, paginaas = 1, r1 = 0, r2 = 0, r3 = 0, encontrados = 0, inicio = 0;
        private string aler = "", consulta = "", mens = "", busqueda = "", rol = "", tipo = "", nombrepagina = "searchTypes";
        public string PaginaNombre
        {
            get { return nombrepagina; }
        }
        public int inicializacion
        {
            set { inicio = value; }
            get { return inicio; }
        }
        public int numPagina
        {
            set { numeropaginas = value; }
            get { return numeropaginas; }
        }
        public int pagina
        {
            set { paginaas = value; }
            get { return paginaas; }
        }
        public int row1
        {
            set { r1 = value; }
            get { return r1; }
        }
        
        public string alerta
        {
            set { aler = value; }
            get { return aler; }
        }
        public string tipoBusqueda
        {
            set { tipo = value; }
            get { return tipo; }
        }
        public string query
        {
            set { consulta = value; }
            get { return consulta; }
        }
        public string mensaje
        {
            set { mens = value; }
            get { return mens; }
        }
        public string Buscar
        {
            set { busqueda = value; }
            get { return busqueda; }
        }
        public int totalEncontrados
        {
            set { encontrados = value; }
            get { return encontrados; }
        }
        public string TipoRol
        {
            set { rol = value; }
            get { return rol; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["busqueda"] == null || Request.QueryString["busqueda"] == "")
            {
                busqueda = "";//Functions.RequestPost(Request.Form["admin"]);
            }
            else
            {
                busqueda = Functions.RequestGet(Request.QueryString["busqueda"]);
            }
            string[] orderby = { "nombre", "fecha_creacion","año"};
            string ordenamuestra = orderby[0];
            tipo = Request.QueryString["admin"] ?? Request.QueryString["mecanico"] ?? Request.QueryString["almacenista"] ?? "";
            string roles = Request.QueryString["admin"] != null ? "admin" : Request.QueryString["mecanico"] != null ? "mecanico" : Request.QueryString["almacenista"] != null ? "almacenista" : "";
            rol = roles;
            if (tipo != null && tipo != "")
            {
                tipo = Functions.RequestGet(tipo);
                switch (tipo)
                {
                    case "Nombre":
                        {
                            ordenamuestra = orderby[0];
                            break;
                        }
                    case "Fecha":
                        {
                            ordenamuestra = orderby[1];
                            break;
                        }
                }
            }
            encontrados = 0;
            int tipoRol = 0;
            switch (roles)
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
            if (!IsPostBack)
            {
                //***************************Codigo que cuenta tipos *************************************//
                consulta = "SELECT COUNT(*) AS tipos FROM TIPO";
                Tuple<List<object[]>, int> resultado = searchTypesView.Consulta(ref mens, consulta);
                if (resultado.Item2 > 0)
                {
                    row1 = Convert.ToInt32(resultado.Item1[0][0]);
                }
                //*********************************Codigo para mostrar*********************************// 
                pagina = HttpContext.Current.Request.QueryString["pagina"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["pagina"]) : 1;
                int regpagina = 50, acaba = pagina * regpagina;
                string mensaje = "";
                inicio = (pagina * regpagina) - regpagina;
                consulta = $"SELECT * FROM TIPO WHERE(id_tipo LIKE '%{busqueda}%' OR nombre LIKE '%{busqueda}%' OR descripcion LIKE '%{busqueda}%' OR fecha_creacion LIKE '%{busqueda}%')" + " ORDER BY " + ordenamuestra + " OFFSET " + inicio + " ROWS FETCH NEXT " + acaba + " ROWS ONLY";
                Tuple<List<object[]>, int> res = searchTypesView.Consulta(ref mensaje, consulta);
                List<object[]> registros = res.Item1;
                int contador = res.Item2;
                Tuple<List<object[]>, int> tr = searchTypesView.Consulta(ref mensaje, $"SELECT COUNT(*) as total_resultados FROM TIPO  WHERE(id_tipo LIKE '%{busqueda}%' OR nombre LIKE '%{busqueda}%' OR descripcion LIKE '%{busqueda}%' OR fecha_creacion LIKE '%{busqueda}%')" );
                List<object[]> totalregistros = tr.Item1;
                int total = 0;
                if (tr.Item2 > 0)
                {
                    total = Convert.ToInt32(tr.Item1[0][0]);
                    encontrados = total;
                }
                numeropaginas = (int)Math.Ceiling((double)total / regpagina);
                tabla.DataBind();
                searchTypesView.Mostrar(tabla, ref mensaje, consulta);
            }

        }
       
        protected void chkTipo_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox idType = (CheckBox)sender;
            GridViewRow row = (GridViewRow)idType.NamingContainer;
            int contadorSeleccionados = 0;
            foreach (GridViewRow rowe in tabla.Rows)
            {
                CheckBox chkType = (CheckBox)rowe.FindControl("chkTipo");

                if (chkType.Checked)
                {
                    contadorSeleccionados++;
                }
            }
            Session["ContadorSeleccionados"] = contadorSeleccionados;
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int contador = (int)(Session["ContadorSeleccionados"] ?? 0);
            Session.Remove("ContadorSeleccionados");
            int[] eliminados = new int[contador];
            int j = 0; // Variable para llevar la cuenta de los elementos válidos
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                if (tabla.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    CheckBox eliminar = (CheckBox)tabla.Rows[i].Cells[0].FindControl("chkTipo");
                    if (eliminar.Checked && tabla.Rows[i].RowType != 0)
                    {
                        int id = Convert.ToInt32(tabla.Rows[i].Cells[2].Text);
                        eliminados[j] = id;
                        j++;
                    }
                    else if (contador == 0 || contador < 1)
                    {
                        eliminados = null;
                        break;
                    }
                }
            }
            int[] filtrados = new int[j];
            while (eliminados != null)
            {
                Array.Copy(eliminados, filtrados, j);
                break;
            }
            Session["Eliminados"] = filtrados;
            Response.Redirect("/Inventario/admin/Actions/ActionsTypes.aspx");
        }
        protected void tabla_PreRender(object sender, EventArgs e)
        {
            if (tabla.Rows.Count > 0)
            {
                GridViewRow newRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.ColumnSpan = tabla.Columns.Count;
                cell.CssClass = "text-center";
                cell.Text = "Seleccionar :<input onclick=\"MarcarCheckBox(this);\" type=\"checkbox\" /> Todos | Ninguno";
                newRow.Cells.Add(cell);
                tabla.Controls[0].Controls.Add(newRow);
            }
        }
        protected void btnPdf_Click(object sender, EventArgs e)
        {
            string consulta = $"SELECT * FROM TIPO WHERE(id_tipo LIKE '%{busqueda}%' OR nombre LIKE '%{busqueda}%' OR descripcion LIKE '%{busqueda}%' OR fecha_creacion LIKE '%{busqueda}%') ORDER BY nombre";
            Tuple<List<object[]>, int> exportPdf = searchTypesView.Consulta(ref mens, consulta);
            List<object[]> totalExport = exportPdf.Item1;
            string html = $@"<!DOCTYPE html>  
<html lang='es'> 
<head>      
    <meta charset='UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />              
    <title>Usuarios</title>  
    <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css'/>                 
    <style> ";
            html += @" .container {
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 10px;
        }
        .table {
            border-collapse: collapse;
            text-align: center;
            border: 1px solid #000;
            width: 100%;
            max-width: 800px; /* Ancho máximo para una hoja tamaño carta */
            margin-bottom: 20px;
        }
        hr {
            color: black;
        }
        .table thead {
            border: 1px solid #000;
            font-weight: bold;
            font-size: 16px;
        }
        .table td {
            border: 1px solid #000;
            padding: 10px;
            font-size: 8px;
            font-family: Arial;
            width: auto; /* Ajustamos el ancho a automático */
        }
        .table tr {
            background: #fff;
        }
        p {
            font-size: 10x;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <img style='float:right; padding:0;' src='https://i.pinimg.com/originals/1b/16/1f/1b161fa87cacc2f1bca21de412dbdfc1.png' width='50%' />
        <br/>
        <div>
            <h2 style='text-align:center;'>Tipos de Vehículos</h2>
            <br/>
        </div>
        <table class='table'>
            <thead style='border: 1px solid #000;'>
                <tr>
                    <td>#</td>
                    <td>Tipo</td>
                    <td>Descripción</td>
 
                </tr>
            </thead>
            <tbody> ";
            if (totalExport.Count > 0)
            {
                int i = 1;
                foreach (object[] row in totalExport)
                {
                    html += $@"
                                    <tr>
                                        <td>{i}</td>
                                        <td>{row[1]}</td>
                                        <td>{row[2]}</td>
                                    </tr>";
                    i++;
                }
            }
            html += @"</tbody>
                            </table>  
                </div>
            </body>
            </html>";
            Functions.CrearPdf(html, "TipoVehiculos");
        }
    }
}