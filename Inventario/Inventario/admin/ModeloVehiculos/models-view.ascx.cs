using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using Inventario.Scripts;
using System.Data;
namespace Inventario.Inventario.admin.ModeloVehiculos
{
    public partial class models_view : System.Web.UI.UserControl
    {
        CONEXION ModelsView = new CONEXION();
        MySql ModelsViewMysql = new MySql();
        Functions Funciones = new Functions();
        private int numeropaginas = 0, paginaas = 0, r1 = 0,  inicio = 0;
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

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {

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
            string[] orderby = { "nombre", "fecha_creacion", "año" };
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
                    case "Año":
                        {
                            ordenamuestra = orderby[2];
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
                consulta = "SELECT COUNT(*) AS modelos FROM MODELO";
                Tuple<List<object[]>, int> resultado = ModelsView.Consulta(ref mens, consulta);
                if (resultado.Item2 > 0)
                {
                    row1 = Convert.ToInt32(resultado.Item1[0][0]);
                }

                pagina = HttpContext.Current.Request.QueryString["pagina"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["pagina"]) : 1;
                int regpagina = 50, acaba = pagina * regpagina;
                inicio = (pagina * regpagina) - regpagina;
                string mensaje = "";
                consulta = "SELECT * FROM MODELO ORDER BY fecha_creacion OFFSET " + inicio + " ROWS FETCH NEXT " + acaba + " ROWS ONLY";
                Tuple<List<object[]>, int> res = ModelsView.Consulta(ref mensaje, consulta);
                List<object[]> registros = res.Item1;
                int contador = resultado.Item2;

                numeropaginas = (int)Math.Ceiling((double)contador / regpagina);
                tabla.DataBind();

                if (contador >= 1)
                {
                    ModelsView.Mostrar(tabla, ref mensaje, consulta);
                }
            }

            if (Request.Form["id_deleM"]!= null || Request.Form["borrar_idM"]!= null)
            {
                int idActivo = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
               (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
                string id = Functions.RequestPost(Request.Form["id_deleM"]);
                consulta = "SELECT * FROM MODELO WHERE id_modelo =" + id;
                Tuple<List<object[]>, int> drop = ModelsView.Consulta(ref mens, consulta);
                List<object[]> arrayModel = drop.Item1;
                try
                {
                    if (drop.Item2 >= 1)
                    {
                        consulta = "SELECT * FROM VEHICULO WHERE id_modelo = " + id;
                        Tuple<List<object[]>, int> modelVehic = ModelsView.Consulta(ref mens, consulta);
                        if (modelVehic.Item2 >= 1)
                        {
                            aler = @"<div class='alert alert-danger alert-dismissible fade in col -sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10;'> 
                                <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                    <h4 class='text-center'>OCURRIÓ UN ERROR</h4>
                                    <p class='text-center'>
                                        No hemos podido eliminar el modelo, ya que hay vehiculos activos con el modelo. Eliminalos o actualiza los datos
                                    </p> </div>";
                        }
                        else if (modelVehic.Item2 == 0)
                        {
                            if (ModelsView.Eliminar("", "MODELO", "id_modelo=" + id))
                            {
                                DateTime fechaActual = DateTime.Now;
                                string ahora = fechaActual.ToString("yyyy-MM-dd HH:mm:ss");
                                ModelsViewMysql.ProcedimientoAlmacenado("registro_alteracionesCLiente", ModelsViewMysql.LinkedServer, "" + idActivo + ",\"Eliminar\",\"" + ahora + "\"," + "\"Modelo\"");

                                id = null;
                                aler = @"<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z-index:10;'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><h4 class='text-center'>Registro eliminado</h4><p class='text-center'>El modelo fue eliminado con éxito</p></div>";

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
                catch (Exception c)
                {
                    aler = @"<div class='alert alert-danger alert-dismissible fade in col -sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10;'> 
                                <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                    <h4 class='text-center'>OCURRIÓ UN ERROR</h4>
                                    <p class='text-center'>
                                     " + c + " </p> </div>";
                }

            }
        }


        protected void btnPdf_Click(object sender, EventArgs e)
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
                    CheckBox eliminar = (CheckBox)tabla.Rows[i].Cells[0].FindControl("chkModelo");
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
            Response.Redirect("/Inventario/admin/Actions/ActionsModels.aspx");
        }

        protected void chkModelo_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox idType = (CheckBox)sender;
            GridViewRow row = (GridViewRow)idType.NamingContainer;
            int contadorSeleccionados = 0;
            foreach (GridViewRow rowe in tabla.Rows)
            {
                CheckBox chkType = (CheckBox)rowe.FindControl("chkModelo");

                if (chkType.Checked)
                {
                    contadorSeleccionados++;
                }
            }
            Session["ContadorSeleccionados"] = contadorSeleccionados;
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