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
    public partial class searchUsers_view : System.Web.UI.UserControl
    {
        MySql AdminView = new MySql();
        Functions Funciones = new Functions();
        private int numeropaginas = 0, paginaas = 1, r1 = 0, r2 = 0, r3 = 0, encontrados =0;
        string aler = "", consulta = "", mens = "";
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
        public int totalEncontrados
        {
            set { encontrados = value; }
            get { return encontrados; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            encontrados = 0;
            string tipo = "admin";
            string busqueda = "";
            if (Request.QueryString["admin"] == null || Request.QueryString["admin"] == "")
            {
                busqueda = "漢字";//Functions.RequestPost(Request.Form["admin"]);
            }
            else
            {
                busqueda = Functions.RequestGet(Request.QueryString["admin"]);
            }
            int tipoRol = 0;
            switch (tipo)
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
                default:
                    Response.Write("Opcion no valida");
                    break;
            }

            // ****************************Codigo que recibe el id de usuario para eliminar ***************************************** //
            if (Request.Form["id_dele"] != null || Request.Form["borrar_id"] != null)
            {
                int SessionId = Convert.ToInt32(Session["id"]);
                string id = Functions.RequestPost(Request.Form["id_dele"]);
                consulta = "SELECT * FROM OPENQUERY(mysql_ticket,'SELECT * FROM cliente WHERE id_cliente =" + id + "')";
                Tuple<List<object[]>, int> drop = AdminView.Consulta(ref mens, consulta);
                List<object[]> arrayUser = drop.Item1;
                if (drop.Item2 >= 1)
                {
                    int cu = 0;
                    int departamento = Convert.ToInt32(drop.Item1[0][5]);
                    if (departamento == 2505)
                    {
                        cu = 1;
                    }
                    else
                    {
                        consulta = "SELECT * FROM OPENQUERY(mysql_ticket,'SELECT * FROM cliente WHERE ((id_departamento = " + departamento + " AND id_cliente <> " + id + " ) AND id_departamento<> 2505)  AND (id_rol = 4046 OR id_rol = 5267)')";
                        Tuple<List<object[]>, int> tec = AdminView.Consulta(ref mens, consulta);
                        List<object[]> arrayTec = tec.Item1; cu = tec.Item2;
                    }
                    if (cu >= 1)
                    {
                        string eliminar = id;
                        consulta = "SELECT * FROM mysql_ticket ... ticket WHERE idUsuario = " + eliminar;
                        Tuple<List<object[]>, int> cr = AdminView.Consulta(ref mens, consulta);
                        int creados = cr.Item2;

                        consulta = "SELECT * FROM mysql_ticket ... ticket WHERE id_Atiende = " + eliminar + " AND idStatus = 94576";
                        Tuple<List<object[]>, int> re = AdminView.Consulta(ref mens, consulta);
                        int resueltos = re.Item2;

                        consulta = "SELECT * FROM mysql_ticket ... ticket WHERE id_Atiende = " + eliminar + " AND idStatus = 94574";
                        Tuple<List<object[]>, int> pe = AdminView.Consulta(ref mens, consulta);
                        int pendientes = pe.Item2;

                        consulta = "SELECT * FROM mysql_ticket ... ticket WHERE id_Atiende = " + eliminar + " AND idStatus = 94575";
                        Tuple<List<object[]>, int> pro = AdminView.Consulta(ref mens, consulta);
                        int proceso = pro.Item2;

                        DateTime fechaActual = DateTime.Now;
                        string ahora = fechaActual.ToString("yyyy-MM-dd HH:mm:ss");

                        if (AdminView.ProcedimientoAlmacenado("EliminarUsuario", "mysql_ticket", "" + eliminar + ",\"" + ahora + "\",\"" + ahora + "\"," + pendientes + "," + creados + "," + resueltos + "," + proceso))
                        {
                            AdminView.ProcedimientoAlmacenado("registro_alteracionesCliente", "mysql_ticket", "" + SessionId + ",\"EliminarU\",\"" + ahora + "\"," + "\"cliente\"");
                            AdminView.ProcedimientoAlmacenado("registro_alteracionesCliente", "mysql_ticket", "" + SessionId + ",\"EliminarU\",\"" + ahora + "\"," + "\"ticket\"");
                            AdminView.ProcedimientoAlmacenado("registro_alteracionesCliente", "mysql_ticket", "" + SessionId + ",\"EliminarU\",\"" + ahora + "\"," + "\"departamento\"");
                            aler = "<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><h4 class='text-center'>ADMINISTRADOR ELIMINADO</h4><p class='text-center'>El administrador fue eliminado del sistema con éxito</p></div>";
                            id = "";
                        }
                        else
                        {
                            aler = "<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>OCURRIÓ UN ERROR</h4> <p class='text-center'> No hemos podido eliminar el administrador </p> </div>";
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('Por el momento no es posible eliminar el usuario porque no hay más técnicos'); window.history.go(-1);</ script > ");
                }
            }
            if (!IsPostBack)
            {
              
                //***************************Codigo que cuenta usuarios *************************************//
                consulta = "SELECT COUNT(*) AS contador FROM mysql_ticket ...cliente WHERE id_rol = 4046";
                Tuple<List<object[]>, int> resultado = AdminView.Consulta(ref mens, consulta);
                if (resultado.Item2 > 0)
                {
                    row1 = Convert.ToInt32(resultado.Item1[0][0]);
                }
                consulta = "SELECT COUNT(*) AS contador FROM mysql_ticket ...cliente WHERE id_rol = 7845 ";
                Tuple<List<object[]>, int> resultado2 = AdminView.Consulta(ref mens, consulta);
                List<object[]> registros2 = resultado2.Item1;

                if (resultado2.Item2 > 0)
                {
                    row2 = Convert.ToInt32(resultado2.Item1[0][0]);
                }
                consulta = "SELECT COUNT(*) AS contador FROM mysql_ticket ...cliente WHERE id_rol = 2736";
                Tuple<List<object[]>, int> resultado3 = AdminView.Consulta(ref mens, consulta);
                List<object[]> registros3 = resultado3.Item1;

                if (resultado3.Item2 > 0)
                {
                    row3 = Convert.ToInt32(resultado3.Item1[0][0]);
                }
                //*********************************Codigo para mostrar*********************************// 
                pagina = HttpContext.Current.Request.QueryString["pagina"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["pagina"]) : 1;
                string[] orderby = { "c.nombre_completo", "c.email_cliente", "c.Fecha_creacion", "e.Nombre" };
                string ordenamuestra = orderby[0];
                int inicio = 0, regpagina = 50;
                string mensaje = "";

               consulta = $"SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT SQL_CALC_FOUND_ROWS c.id_cliente,c.nombre_completo, c.nombre_usuario, c.email_cliente, d.nombre as Depa, r.Nombre, c.telefono_celular as celular, c.Fecha_creacion, e.Nombre as Esta,c.anydesk FROM cliente c INNER JOIN departamento d ON c.id_departamento = d.idDepartamento INNER JOIN estatus e ON e.idEstatus = c.idEstatus INNER JOIN rol r ON c.id_rol = r.idRol WHERE(c.id_cliente LIKE \"%{busqueda}%\" OR c.nombre_usuario LIKE \"%{busqueda}%\" OR c.nombre_completo LIKE \"%{busqueda}%\" OR c.email_cliente LIKE \"%{busqueda}%\" OR c.telefono_celular LIKE \"%{busqueda}%\" OR c.Fecha_creacion LIKE \"%{busqueda}%\" OR d.nombre LIKE \"%{busqueda}%\" OR r.Nombre LIKE \"%{busqueda}%\" OR e.Nombre LIKE \"%{busqueda}%\" OR c.anydesk LIKE \"%{busqueda}%\") AND c.id_rol = " + tipoRol + " ORDER BY " + ordenamuestra +" LIMIT " + inicio + "," + regpagina + "')";      
                Tuple<List<object[]>, int> res = AdminView.Consulta(ref mensaje, consulta);
                List<object[]> registros = res.Item1;
                int contador = res.Item2;
                encontrados = contador;
                Tuple<List<object[]>, int> tr = AdminView.Consulta(ref mensaje, "SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT count(*) FROM CLIENTE')");
                List<object[]> totalregistros = tr.Item1;
                int total = 0;
                if (tr.Item2 > 0)
                {
                    total = Convert.ToInt32(tr.Item1[0][0]);
                }
                numeropaginas = (int)Math.Ceiling((double)total / regpagina);
                tabla.DataBind();
                
                 AdminView.Mostrar(tabla, ref mensaje, consulta);
               
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
        protected void btnBloquear_Click(object sender, EventArgs e)
        {
            int contador = (int)(Session["ContadorSeleccionados"] ?? 0);
            Session.Remove("ContadorSeleccionados");

            int[] bloqueados = new int[contador];
            int j = 0; // Variable para llevar la cuenta de los elementos válidos
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                if (tabla.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    CheckBox bloquear = (CheckBox)tabla.Rows[i].Cells[0].FindControl("chkUsuario");
                    if (bloquear.Checked && tabla.Rows[i].RowType != 0)
                    {
                        int id = Convert.ToInt32(tabla.Rows[i].Cells[1].Text);
                        bloqueados[j] = id;
                        j++;
                    }
                    else if (contador == 0 || contador < 1)
                    {
                        bloqueados = null;
                        break;
                    }
                }
            }
            int[] filtrados = new int[j];
            while (bloqueados != null)
            {
                Array.Copy(bloqueados, filtrados, j);
                break;
            }
            Session["Bloqueados"] = filtrados;
            Response.Redirect("/Inventario/admin/Actions/Actions.aspx");
        }
        protected void btnDesbloquear_Click(object sender, EventArgs e)
        {
            int contador = (int)(Session["ContadorSeleccionados"] ?? 0);
            Session.Remove("ContadorSeleccionados");
            int[] desbloqueados = new int[contador];
            int j = 0; // Variable para llevar la cuenta de los elementos válidos
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                if (tabla.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    CheckBox desbloquear = (CheckBox)tabla.Rows[i].Cells[0].FindControl("chkUsuario");
                    if (desbloquear.Checked && tabla.Rows[i].RowType != 0)
                    {
                        int id = Convert.ToInt32(tabla.Rows[i].Cells[1].Text);
                        desbloqueados[j] = id;
                        j++;
                    }
                    else if (contador == 0 || contador < 1)
                    {
                        desbloqueados = null;
                        break;
                    }
                }
            }
            int[] filtrados = new int[j];
            while (desbloqueados != null)
            {
                Array.Copy(desbloqueados, filtrados, j);
                break;
            }
            Session["Desbloqueados"] = filtrados;
            Response.Redirect("/Inventario/admin/Actions/Actions.aspx");
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
                    CheckBox eliminar = (CheckBox)tabla.Rows[i].Cells[0].FindControl("chkUsuario");
                    if (eliminar.Checked && tabla.Rows[i].RowType != 0)
                    {
                        int id = Convert.ToInt32(tabla.Rows[i].Cells[1].Text);
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
            Response.Redirect("/Inventario/admin/Actions/Actions.aspx");
        }
        protected void btnResetear_Click(object sender, EventArgs e)
        {
            int contador = (int)(Session["ContadorSeleccionados"] ?? 0);
            Session.Remove("ContadorSeleccionados");
            int[] reseteados = new int[contador];
            int j = 0; // Variable para llevar la cuenta de los elementos válidos
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                if (tabla.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    CheckBox resetear = (CheckBox)tabla.Rows[i].Cells[0].FindControl("chkUsuario");
                    if (resetear.Checked && tabla.Rows[i].RowType != 0)
                    {
                        int id = Convert.ToInt32(tabla.Rows[i].Cells[1].Text);
                        reseteados[j] = id;
                        j++;
                    }
                    else if (contador == 0 || contador < 1)
                    {
                        reseteados = null;
                        break;
                    }
                }
            }
            int[] filtrados = new int[j];
            while (reseteados != null)
            {
                Array.Copy(reseteados, filtrados, j);
                break;
            }
            Session["Reseteados"] = filtrados;
            Response.Redirect("/Inventario/admin/Actions/Actions.aspx");
        }
        protected void chkUsuario_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox idUser = (CheckBox)sender;
            GridViewRow row = (GridViewRow)idUser.NamingContainer;
            int contadorSeleccionados = 0;
            foreach (GridViewRow rowe in tabla.Rows)
            {
                CheckBox chkUsuario = (CheckBox)rowe.FindControl("chkUsuario");

                if (chkUsuario.Checked)
                {
                    contadorSeleccionados++;
                }
            }
            Session["ContadorSeleccionados"] = contadorSeleccionados;
        }
        protected void btnPdf_Click(object sender, EventArgs e)
        {
            int rol = 4046;
            string consulta = $" SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT c.Anydesk,c.id_cliente, c.fecha_creacion, c.nombre_completo, c.email_cliente, c.telefono_celular, c.nombre_usuario, d.nombre as Depa,r.Nombre as Rol, e.Nombre as Esta FROM cliente c INNER JOIN departamento d ON d.idDepartamento = c.id_departamento INNER JOIN estatus e ON e.idEstatus = c.idEstatus INNER JOIN rol r on c.id_rol = r.idRol WHERE c.id_rol = {rol} ORDER BY c.nombre_completo')";
            Tuple<List<object[]>, int> exportPdf = AdminView.Consulta(ref mens, consulta);
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
            max-width: 800px;
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
            width: auto; 
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
            <h2 style='text-align:center;'>Usuarios</h2>
            <br/>
        </div>
        <table class='table'>
            <thead style='border: 1px solid #000;'>
                <tr>
                    <td>#</td>
                    <td>Creado</td>
                    <td>Nombre completo</td>
                    <td>Usuario</td>
                    <td>Email</td>
                    <td>Teléfono</td>
                    <td>Departamento</td>
                    <td>Rol</td>
                    <td>Estatus</td>
                  
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
                                        <td>{Convert.ToString(row[2])}</td>
                                        <td>{row[3]}</td>
                                        <td>{row[6]}</td>
                                        <td>{row[4]}</td>
                                        <td>{row[5]}</td>
                                        <td>{row[7]}</td>
                                        <td>{row[8]}</td>
                                        <td>{row[9]}</td>
                                
                                    </tr>";
                    i++;
                }
            }
            html += @"</tbody>
                            </table>  
                </div>
            </body>
            </html>";
            Functions.CrearPdf(html);
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
        }
    }
   
}