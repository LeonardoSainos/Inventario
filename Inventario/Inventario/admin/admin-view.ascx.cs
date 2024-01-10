﻿using System;
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
        string query = "";
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
        public string consulta
        {
            set { query = value; }
            get { return query; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MySql AdminView = new MySql();

            //***************************Codigo que cuenta usuarios *************************************//
            string consulta = "SELECT COUNT(*) AS contador FROM mysql_ticket ...cliente WHERE id_rol = 4046", mens = "";
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
            MySql AdminViews = new MySql();
            string mensaje = "";
            consulta = "SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT SQL_CALC_FOUND_ROWS cliente.id_cliente,cliente.telefono_celular AS celular, cliente.nombre_completo,cliente.nombre_usuario,cliente.email_cliente,departamento.nombre AS Depa,estatus.Nombre AS Esta,cliente.Fecha_creacion,cliente.anydesk FROM cliente INNER JOIN departamento ON cliente.id_departamento = departamento.idDepartamento INNER JOIN estatus ON estatus.idEstatus = cliente.idEstatus WHERE cliente.id_rol = 4046 ORDER BY " + ordenamuestra + " LIMIT " + inicio + "," + regpagina + "')";
            Tuple<List<object[]>, int> res = AdminViews.Consulta(ref mensaje, consulta);
            List<object[]> registros = res.Item1;
            int contador = resultado.Item2;
            Tuple<List<object[]>, int> tr = AdminViews.Consulta(ref mensaje, "SELECT * FROM OPENQUERY(mysql_ticket, 'SELECT count(*) FROM CLIENTE')");
            List<object[]> totalregistros = tr.Item1;
            int total = 0;

            if (tr.Item2 > 0)
            {
                total = Convert.ToInt32(tr.Item1[0][0]);
            }
            numeropaginas = (int)Math.Ceiling((double)total / regpagina);
            tabla.DataBind();
            if (contador >= 1) {
                AdminViews.Mostrar(tabla, ref mensaje, consulta);
            }

            // ****************************Codigo que recibe el id de usuario para eliminar ***************************************** //
            if (Request.Form["id_dele"] != null || Request.Form["borrar_id"] != null)
            {
                int id = Convert.ToInt32(Request.Form["id_dele"]);
               
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