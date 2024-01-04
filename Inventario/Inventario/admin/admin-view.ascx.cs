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
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] orderby=  { "cliente.nombre_completo", "cliente.email_cliente", "cliente.Fecha_creacion", "estatus.Nombre"};
            string ordenamuestra = orderby[0];
            int inicio = 0, regpagina=50;
            MySql AdminViews = new MySql();
            string mensaje = "", consulta = "SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT SQL_CALC_FOUND_ROWS cliente.id_cliente,cliente.telefono_celular AS celular, cliente.nombre_completo,cliente.nombre_usuario,cliente.email_cliente,departamento.nombre AS Depa,estatus.Nombre AS Esta,cliente.Fecha_creacion,cliente.anydesk FROM cliente INNER JOIN departamento ON cliente.id_departamento = departamento.idDepartamento INNER JOIN estatus ON estatus.idEstatus = cliente.idEstatus WHERE cliente.id_rol = 4046 ORDER BY " + ordenamuestra + " LIMIT " + inicio + "," + regpagina + "')";
            Tuple<List<object[]>, int> resultado = AdminViews.Consulta(ref mensaje, consulta);
            List<object[]> registros = resultado.Item1;
            int contador = resultado.Item2;


            Tuple<List<object[]>, int> tr = AdminViews.Consulta(ref mensaje, "SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT count(*) FROM CLIENTE')");
            List<object[]> totalregistros = tr.Item1;
            int total = 0;
            if (tr.Item2 > 0)
            {
                total = Convert.ToInt32(tr.Item1[0][0]);
            }
              int numeropaginas = (int)Math.Ceiling((double)total / regpagina);


            tabla.DataBind();
            if (contador >= 1) {
                AdminViews.Mostrar(tabla, ref mensaje, consulta);
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
    }
}