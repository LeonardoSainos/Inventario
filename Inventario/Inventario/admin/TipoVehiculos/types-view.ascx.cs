using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using Inventario.Scripts;
using System.Data;

namespace Inventario.Inventario.admin.TipoVehiculos
{
    public partial class tipos_view : System.Web.UI.UserControl
    {
        CONEXION TypesView = new CONEXION();
        MySql TypesViewMysql = new MySql();
        Functions Funciones = new Functions();
        private int numeropaginas = 0, paginaas = 0, r1 = 0, r2 = 0, r3 = 0, inicio = 0;
        private string aler = "", consulta = "", mens = "", rol = "", nombrepagina = "searchTypes";
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
        public int row2
        {
            set { r2 = value; }
            get { return r2; }
        }

        public int row3
        {
            set { r3 = value; }
            get { return r3; }
        }
        public string alerta
        {
            set { aler = value; }
            get { return aler; }
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
        public string TipoRol
        {
            set { rol = value; }
            get { return rol; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] orderby = { "nombre", "fecha_creacion" };
            string ordenamuestra = orderby[0];
            int tipoRol = 0;
            if((Request.QueryString["view"]!="" || Request.QueryString["view"]!=null) && Request.QueryString[rol] != null)
            {
                string orden = Request.QueryString[rol];
                orden = Functions.RequestGet(orden);
                switch (orden)
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
            else
            {
                ordenamuestra = orderby[0];
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


            if (!IsPostBack)
            {
                consulta = "SELECT COUNT(*) AS tipos FROM TIPO";
                Tuple<List<object[]>, int> resultado = TypesView.Consulta(ref mens, consulta);
                if (resultado.Item2 > 0)
                {
                    row1 = Convert.ToInt32(resultado.Item1[0][0]);
                }

                pagina = HttpContext.Current.Request.QueryString["pagina"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["pagina"]) : 1;
                int regpagina = 50, acaba = pagina * regpagina;
                inicio = (pagina * regpagina) - regpagina;
                string mensaje = "";
                consulta = "SELECT * FROM TIPO ORDER BY nombre OFFSET " + inicio + " ROWS FETCH NEXT " + acaba + " ROWS ONLY";
                Tuple<List<object[]>, int> res = TypesView.Consulta(ref mensaje, consulta);
                List<object[]> registros = res.Item1;
                int contador = row1;
             
                numeropaginas = (int)Math.Ceiling((double)contador / regpagina);
                tabla.DataBind();

                if(contador >= 1)
                {
                    TypesView.Mostrar(tabla, ref mensaje, consulta);
                }
                
            }



            if(Request.Form["id_deleT"]!=null || Request.Form["borrar_idT"] != null)
            {
                int idActivo = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
                (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
                string id = Functions.RequestPost(Request.Form["id_deleT"]);
                consulta = "SELECT * FROM TIPO WHERE id_tipo =" + id;
                Tuple<List<object[]>, int> drop = TypesView.Consulta(ref mens, consulta);
                List<object[]> arrayTypes = drop.Item1;
                try
                {
                  if(drop.Item2 >= 1)
                  {
                        consulta = "SELECT * FROM VEHICULO WHERE id_tipo=" +  id;
                        Tuple<List<object[]>, int> tipeVehic = TypesView.Consulta(ref mens, consulta);
                        if (tipeVehic.Item2 >= 1)
                        {
                            aler = @"<div class='alert alert-danger alert-dismissible fade in col -sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10;'> 
                                <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                    <h4 class='text-center'>OCURRIÓ UN ERROR</h4>
                                    <p class='text-center'>
                                        No hemos podido eliminar el tipo de vehiculo, ya que hay vehiculos activos con este. Eliminalos o actualiza los datos
                                    </p> </div>";
                        }
                        else if (tipeVehic.Item2 == 0)
                        {
                            if(TypesView.Eliminar("", "TIPO", "id_tipo=" + id))
                            {
                                DateTime fechaActual = DateTime.Now;
                                string ahora = fechaActual.ToString("yyyy-MM-dd HH:mm:ss");
                                TypesViewMysql.ProcedimientoAlmacenado("registro_alteracionesCliente", TypesViewMysql.LinkedServer, "" + idActivo + ",\"Eliminar\",\"" + ahora + "\"," + "\"Tipo\"");
                                id = null;
                                aler = @"<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z-index:10;'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><h4 class='text-center'>Registro eliminado</h4><p class='text-center'>El tipo de vehículo fue eliminado con éxito</p></div>";
                            }
                            else
                            {
                                aler = @"<div class='alert alert-danger alert-dismissible fade in col -sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10;'> 
                                <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                    <h4 class='text-center'>OCURRIÓ UN ERROR</h4>
                                    <p class='text-center'>
                                        No hemos podido actualizar el registro, revisa tu conexión o contactate con soporte técnico
                                    </p> </div>";
                            }
                        }
                  }
                }
                catch(Exception c)
                {
                    aler = @"<div class='alert alert-danger alert-dismissible fade in col -sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10;'> 
                                <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                    <h4 class='text-center'>OCURRIÓ UN ERROR</h4>
                                    <p class='text-center'>
                                     " + c + " </p> </div>";
                }
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
            string consulta = "SELECT * FROM TIPO ORDER BY nombre";
            Tuple<List<object[]>, int> exportPdf = TypesView.Consulta(ref mens, consulta);
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
            Functions.CrearPdf(html,"TipoVehiculos");
        }
    }
}