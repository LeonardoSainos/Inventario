using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using Inventario.Scripts;

namespace Inventario.Inventario.admin.TipoVehiculos
{
    public partial class tipoEdit : System.Web.UI.UserControl
    {
        CONEXION UpdateType = new CONEXION();
        MySql UpdateTypeMysql = new MySql();
        private string query = "", mens = "", alert = "";
 
        private string fechaText = "", nombreText = "", descripcionText="";
        private int  id_edit = 0;
        public int idEdit { get; set; }
         
        public string txtFecha
        {
            set { fechaText = value; }
            get { return fechaText; }
        }
        public string txtNombre
        {
            set { nombreText = value; }
            get { return nombreText; }
        }
        public string txtDescripcion
        {
            set { descripcionText = value; }
            get { return descripcionText; }
        }
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
            if (Request.QueryString["idT"]!=null) {
                int typeId = Convert.ToInt32(Functions.RequestGet(Request.QueryString["idT"]));
                id_edit = typeId;
                consulta = "SELECT * FROM TIPO WHERE id_tipo =" + id_edit;
                Tuple<List<object[]>, int> TypeData = UpdateType.Consulta(ref mens, consulta);
                int contador = TypeData.Item2;

                nombreText= !string.IsNullOrEmpty(Convert.ToString(TypeData?.Item1?[0][1])) ? Convert.ToString(TypeData.Item1[0][1]) :nombreText;
                descripcionText = !string.IsNullOrEmpty(Convert.ToString(TypeData?.Item1?[0][2])) ? Convert.ToString(TypeData.Item1[0][2]) : descripcionText; fechaText = Convert.ToString(TypeData.Item1[0][3]);
                fechaText = !string.IsNullOrEmpty(Convert.ToString(TypeData?.Item1[0][3])) ? Convert.ToString(TypeData.Item1[0][3]) : fechaText;
                if (!string.IsNullOrEmpty(fechaText))
                {
                    DateTime fechaHora = DateTime.Parse(fechaText);
                    fechaText = fechaHora.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                if (Request.Form["id_edit"]!= null & Request.Form["Tnombre"] != null)
                {
                    int idActivo = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
                    (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);



                    string nombreL = Functions.RequestPost(Request.Form["Tnombre"]).ToUpper();
                    string descripcionL = Functions.RequestPost(Request.Form["Tdescripcion"]);
                    string fechaL = Functions.RequestPost(Request.Form["Tfecha"]);
                    if(!string.IsNullOrEmpty(fechaL))
                    {
                        DateTime fechaL2 = DateTime.Parse(fechaL);
                        fechaL = fechaL2.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    }
              
                    try
                    {
                        if (UpdateType.Actualizar("", "TIPO", "nombre='" + nombreL + "',descripcion='" + descripcionL + "',fecha_creacion='" + fechaL + "'", "id_tipo=" + id_edit))
                        {
                            string fechaConvertida = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            //PENDIENTE
                          UpdateTypeMysql.ProcedimientoAlmacenado("registro_alteracionesCliente", UpdateType.LinkedServer, idActivo + ",\"Actualizar\",\"" + fechaConvertida + "\"," + "\"tipo\"");
                            alerta = @"<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10; '> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                 <h4 class='text-center'>Registro Actualizado</h4>
                                   <p class='text-center'>
                                    El tipo de vehiculo fue actualizado con éxito
                                    </p>
                                  </div>";
                        }
                        else
                        {
                            alerta = @"<div class='alert alert-danger alert-dismissible fade in col -sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10;'> 
                                <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                <h4 class='text-center'>OCURRIÓ UN ERROR</h4>
                                <p class='text-center'>
                                    No hemos podido actualizar el registro
                                </p>
                                 </div>";
                        }

                    }
                    catch (Exception c) {

                        alerta = "ERROR : " + c.Message; 
                    }
                }

            }

        }
    }
}