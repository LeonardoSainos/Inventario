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
    public partial class AddTypes : System.Web.UI.UserControl
    {
      CONEXION Addtypes = new CONEXION();

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
             if(Request.Form["Gtype"]!=null && Request.Form["Gdescription"] != null)
             {
                int idActivo = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
                     (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
                string nameTipo = Functions.RequestPost(Request.Form["Gtype"]);
                string description = Functions.RequestPost(Request.Form["Gdescription"]);
                consulta = "SELECT * FROM TIPO WHERE nombre LIKE '" + nameTipo + "%' OR descripcion LIKE '%" + description + "%'";
                Tuple<List<object[]>, int> verifica = Addtypes.Consulta(ref mens, consulta);
                if (verifica.Item2 <= 0)
                {
                    try
                    {
                        bool insertar = Addtypes.Insertar("TIPO", "", "nombre,descripcion", "'" + nameTipo + "','" + description + "'", ref mens);
                        if (insertar == true)
                        {
                            DateTime hoy = DateTime.Now;
                            string ahora = Convert.ToString(hoy);
                            alert = "<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>REGISTRO EXITOSO</h4><p class='text-center'>Registro creado exitosamente,consulta recargando la página</p></div>";

                            //PENDIENTE
                            //     Addtypes.ProcedimientoAlmacenado("registro_alteracionesCliente", "''", +SessionId + ",\"Insertar\",\"" + ahora + "\"," + "\"cliente\"");
                        }
                        else
                        {
                            alert = "<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'>  <button type = 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>OCURRIÓ UN ERROR</h4><p class='text-center'>Este registro ya ha sido registrado, si el error persiste comunicate con Soporte TI</p></div>";

                        }
                    }
                    catch(Exception c)
                    {
                        alerta = "ERROR : " + c.Message;
                    }
                  
                }

             }
        }
    }
}