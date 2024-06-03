using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Scripts;
using Inventario.Inventario.lib;
using System.Data;


namespace Inventario.Inventario.admin.Vehiculos
{
    public partial class Vehiculos_view : System.Web.UI.UserControl
    {
        CONEXION CarsView = new CONEXION();
        MySql CarsViewMysql = new MySql();
        Functions Funciones = new Functions();
        private int numeropaginas = 0, paginaas = 0, r1 = 0, r2 = 0, r3 = 0, inicio = 0;
        private string aler = "", consulta = "", mens = "", rol = "", nombrepagina = "searchCars";
       
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
            if ((Request.QueryString["view"] != "" || Request.QueryString["view"] != null) && Request.QueryString[rol] != null)
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
                consulta = "SELECT  COUNT(*) AS carros FROM VEHICULO";
                Tuple<List<object[]>, int> resultado = CarsView.Consulta(ref mens, consulta);
                if (resultado.Item2 > 0)
                {
                    row1 = Convert.ToInt32(resultado.Item1[0][0]);
                }

                pagina = HttpContext.Current.Request.QueryString["pagina"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["pagina"]) : 1;
                int regpagina = 50, acaba = pagina * regpagina;
                inicio = (pagina * regpagina) - regpagina;
                string mensaje = "";
                consulta = $"SELECT v.id_vehiculo,v.nombre_vehiculo,c.nombre_completo as asignado , t.nombre as tipo,m.nombre as marca, v.placas,mo.nombre as modelo, e.nombre as estatus,v.Numero_serie,v.poliza_seguro, pv.nombre as propietarioGps, v.fecha_actualizacion as actualizado FROM VEHICULO v INNER JOIN OPENQUERY(mysql_ticket,'SELECT * FROM estatus') e ON e.idEstatus = v.id_status INNER JOIN MARCA m ON m.id_marca = v.id_marca INNER JOIN TIPO t ON t.id_tipo = v.id_tipo INNER JOIN MODELO mo  ON mo.id_modelo = v.id_modelo INNER JOIN OPENQUERY(mysql_ticket, 'SELECT * FROM cliente') c ON  v.id_cliente = c.id_cliente  INNER JOIN GPS gp ON gp.id_gps = v.id_gps  INNER JOIN PROPIETARIO_VEHICULO pv ON pv.id_propietarioEmpresa = v.id_propietario ORDER BY v.nombre_vehiculo OFFSET " + inicio + " ROWS FETCH NEXT " + acaba + " ROWS ONLY";
                Tuple<List<object[]>, int> res = CarsView.Consulta(ref mensaje, consulta);
                List<object[]> registros = res.Item1;
                int contador = row1;

                numeropaginas = (int)Math.Ceiling((double)contador / regpagina);
                tabla.DataBind();
                if (contador >= 1)
                {
                    CarsView.Mostrar(tabla, ref mensaje, consulta);
                }

            }
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

        protected void ckCar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox idCar = (CheckBox)sender;
            GridViewRow row = (GridViewRow)idCar.NamingContainer;
            int contadorSeleccionados = 0;
            foreach (GridViewRow rowe in tabla.Rows)
            {
                CheckBox chkCar = (CheckBox)rowe.FindControl("chkCar");

                if (chkCar.Checked)
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
                    CheckBox eliminar = (CheckBox)tabla.Rows[i].Cells[0].FindControl("chkCar");
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
            Response.Redirect("/Inventario/admin/Actions/ActionsCars.aspx");
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {/*
            string consulta = $"SELECT v.id_vehiculo,v.nombre_vehiculo,c.nombre_completo as asignado , t.nombre as tipo,m.nombre as marca, v.placas,mo.nombre as modelo, e.nombre as estatus,v.Numero_serie,v.poliza_seguro, pv.nombre as propietarioGps, v.fecha_actualizacion as actualizado FROM VEHICULO v INNER JOIN OPENQUERY(mysql_ticket,'SELECT * FROM estatus') e ON e.idEstatus = v.id_status INNER JOIN MARCA m ON m.id_marca = v.id_marca INNER JOIN TIPO t ON t.id_tipo = v.id_tipo INNER JOIN MODELO mo  ON mo.id_modelo = v.id_modelo INNER JOIN OPENQUERY(mysql_ticket, 'SELECT * FROM cliente') c ON  v.id_cliente = c.id_cliente  INNER JOIN GPS gp ON gp.id_gps = v.id_gps  INNER JOIN PROPIETARIO_VEHICULO pv ON pv.id_propietarioEmpresa = v.id_propietario ORDER BY v.nombre_vehiculo";
            Tuple<List<object[]>, int> exportPdf = CarsViewMysql.Consulta(ref mens, consulta);
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
            max-width: 800px; // Ancho máximo para una hoja tamaño carta 
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
            width: auto; // Ajustamos el ancho a automático 
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
                    <td>Creado</td>
 
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
                                        <td>{Convert.ToString(row[0])}</td>       
                                    </tr>";
                    i++;
                }
            }
            html += @"</tbody>
                            </table>  
                </div>
            </body>
            </html>";
            Functions.CrearPdf(html, "TipoVehiculos");*/

        }
    }
}