using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using Inventario.Scripts;
namespace Inventario.Inventario.admin.Actions
{
    public partial class AddModels : System.Web.UI.UserControl
    {
        CONEXION Addmodels = new CONEXION();
        MySql AddmodelsMysql = new MySql();

        private string query = "", mens = "", alert = "";
        public string mensaje
        {
            get { return mens; }
            set { mens = value; }
        }
        public string consulta
        {
            get { return query; }
            set { query = value; }
        }
        public string alerta
        {
            get { return alert; }
            set { alert = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["Gmodel"] != null && Request.Form["Gdescription"] != null && Request.Form["Gaño"] != null)
            {
                int idActivo = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
                 (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
                string nameModel = Functions.RequestPost(Request.Form["Gmodel"]).ToUpper();
                string description = Functions.RequestPost(Request.Form["Gdescription"]);
                int año=Convert.ToInt32( Functions.RequestPost(Request.Form["Gaño"]));
                consulta = "SELECT * FROM MODELO WHERE (nombre LIKE '" + nameModel + "%' AND descripcion LIKE '%" + description + "%') AND año =" + año + "";
                Tuple<List<object[]>, int> verifica = Addmodels.Consulta(ref mens, consulta);
                if (verifica.Item2 <= 0)
                {
                    bool insertar = Addmodels.Insertar("MODELO", "", "nombre,descripcion,año", "'" + nameModel + "','" + description + "'," + año + "", ref mens);
                    if (insertar == true)
                    {
                        DateTime hoy = DateTime.Now;
                        string ahora = Convert.ToString(hoy);
                        alert = "<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>REGISTRO EXITOSO</h4><p class='text-center'>Registro creado exitosamente,consulta recargando la página</p></div>";
                     // pendiente
                       AddmodelsMysql.ProcedimientoAlmacenado("registro_alteracionesCliente", "''", + idActivo + ",\"Insertar\",\"" + ahora + "\"," + "\"modelo\"");
                    }
                    else
                    {
                        alert = "<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'>  <button type = 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>OCURRIÓ UN ERROR</h4><p class='text-center'>Este registro ya ha sido registrado, si el error persiste comunicate con Soporte TI" +  mensaje +  "</p></div>";
                    }
                }
            }

        }
    }
}