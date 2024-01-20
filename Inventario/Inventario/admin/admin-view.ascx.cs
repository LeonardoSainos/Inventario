using System;
using Inventario.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Inventario.Inventario.admin
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        private int numeropaginas=0, paginaas=1, r1=0, r2=0, r3=0;
        string aler = "",consulta="",mens="";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            MySql AdminView = new MySql();
           
            // ****************************Codigo que recibe el id de usuario para eliminar ***************************************** //
            if (Request.Form["id_dele"] != null || Request.Form["borrar_id"] != null)
            {
                int SessionId = Convert.ToInt32(Session["id"]);
                string id = MySql.RequestPost(Request.Form["id_dele"]);


                consulta = "SELECT * FROM OPENQUERY(mysql_ticket,'SELECT * FROM cliente WHERE id_cliente =" + id + "')";
                Tuple<List<object[]>, int> drop = AdminView.Consulta(ref mens, consulta);
                List<object[]> arrayUser = drop.Item1;
                if (drop.Item2 >= 1)
                {
                    int cu = 0;
                    int departamento = Convert.ToInt32(drop.Item1[0][5]);
                    if(departamento == 2505)
                    {
                        cu = 1;
                    }
                    else
                    {
                        consulta = "SELECT * FROM OPENQUERY(mysql_ticket,'SELECT * FROM cliente WHERE ((id_departamento = " + departamento + " AND id_cliente <> " + id + " ) AND id_departamento<> 2505)  AND (id_rol = 4046 OR id_rol = 5267)')";
                        Tuple<List<object[]>, int> tec = AdminView.Consulta(ref mens, consulta);
                        List<object[]> arrayTec = tec.Item1;  cu = tec.Item2;
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
            string[] orderby=  { "cliente.nombre_completo", "cliente.email_cliente", "cliente.Fecha_creacion", "estatus.Nombre"};
            string ordenamuestra = orderby[0];
            int inicio = 0, regpagina=50;
 
            string mensaje = "";
            consulta = "SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT SQL_CALC_FOUND_ROWS cliente.id_cliente,cliente.telefono_celular AS celular, cliente.nombre_completo,cliente.nombre_usuario,cliente.email_cliente,departamento.nombre AS Depa,estatus.Nombre AS Esta,cliente.Fecha_creacion,cliente.anydesk FROM cliente INNER JOIN departamento ON cliente.id_departamento = departamento.idDepartamento INNER JOIN estatus ON estatus.idEstatus = cliente.idEstatus WHERE cliente.id_rol = 4046 ORDER BY " + ordenamuestra + " LIMIT " + inicio + "," + regpagina + "')";
            Tuple<List<object[]>, int> res = AdminView.Consulta(ref mensaje, consulta);
            List<object[]> registros = res.Item1;
            int contador = resultado.Item2;
            Tuple<List<object[]>, int> tr = AdminView.Consulta(ref mensaje, "SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT count(*) FROM CLIENTE')");
            List<object[]> totalregistros = tr.Item1;
            int total = 0;
            if (tr.Item2 > 0)
            {
                total = Convert.ToInt32(tr.Item1[0][0]);
            }
            numeropaginas = (int)Math.Ceiling((double)total / regpagina);
            tabla.DataBind();
            if (contador >= 1) {
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
            foreach (GridViewRow row in tabla.Rows)
            {
                CheckBox chkUsuario = row.FindControl("chkUsuario") as CheckBox;

                if (chkUsuario != null)
                {
                    Console.WriteLine($"ID de Cliente: {chkUsuario.Text}");
                    Console.WriteLine($"Checked: {chkUsuario.Checked}");
                }
            }
        }


    }
}