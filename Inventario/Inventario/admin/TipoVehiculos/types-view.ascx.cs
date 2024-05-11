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
        MySql TypesView = new MySql();
        Functions Funciones = new Functions();
        private int numeropaginas = 0, paginaas = 0, r1 = 0, r2 = 0, r3 = 0, inicio = 0;
        string aler = "", consulta = "", mens = "", rol = "";
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
                consulta = "SELECT * FROM TIPO ORDER BY fecha_creacion OFFSET " + inicio + " ROWS FETCH NEXT " + acaba + " ROWS ONLY";
                Tuple<List<object[]>, int> res = TypesView.Consulta(ref mensaje, consulta);
                List<object[]> registros = res.Item1;
                int contador = resultado.Item2;
             
                numeropaginas = (int)Math.Ceiling((double)contador / regpagina);
                tabla.DataBind();

                if(contador >= 1)
                {
                    TypesView.Mostrar(tabla, ref mensaje, consulta);
                }
                
            }


        }
        protected void chkTipo_CheckedChanged(object sender, EventArgs e)
        {
            
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
    }
}