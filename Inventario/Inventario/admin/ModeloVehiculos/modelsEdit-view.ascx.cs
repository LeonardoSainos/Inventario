using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using Inventario.Scripts;
namespace Inventario.Inventario.admin.ModeloVehiculos
{
    public partial class modelsEdit_view : System.Web.UI.UserControl
    {
        CONEXION UpdateModel = new CONEXION();
        private string query = "", mens = "", alert = "";
        private string fechaText = "", nombreText = "", descripcionText = "";
        private int  id_edit = 0, añoText=0;
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
        public int txtAño
        {
            set { añoText = value; }
            get { return añoText; }
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
            if (Request.QueryString["idM"] != null)
            {
                int ModelId = Convert.ToInt32(Functions.RequestGet(Request.QueryString["idM"]));
                id_edit = ModelId;
                consulta = "SELECT * FROM MODELO WHERE id_modelo =" + id_edit;
                Tuple<List<object[]>, int> ModelData = UpdateModel.Consulta(ref mens, consulta);
                int contador = ModelData.Item2;

                //  descripcionText = Convert.ToString(ModelData.Item1[0][2])
                nombreText = !string.IsNullOrEmpty(Convert.ToString(ModelData?.Item1?[0][1])) ? Convert.ToString(ModelData.Item1[0][1]) : nombreText;
                descripcionText = !string.IsNullOrEmpty(Convert.ToString(ModelData?.Item1?[0][2])) ? Convert.ToString(ModelData.Item1[0][2]) : descripcionText;
                object valor = ModelData.Item1[0][3];  
                añoText = valor != DBNull.Value ? Convert.ToInt32(valor) : 0;

                //variable = condición ? valor_si_verdadero : valor_si_falso;


                fechaText = !string.IsNullOrEmpty(Convert.ToString(ModelData?.Item1[0][4])) ? Convert.ToString(ModelData.Item1[0][4]) : fechaText;
                
                if(!string.IsNullOrEmpty(fechaText))
                {
                    DateTime fechaHora = DateTime.Parse(fechaText);
                    fechaText = fechaHora.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
   

                if (Request.Form["id_edit"] != null & Request.Form["Mnombre"] != null)
                {
                    int idActivo = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
                    (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);

                    string nombreL = Functions.RequestPost(Request.Form["Mnombre"]);
                    string descripcionL = Functions.RequestPost(Request.Form["Mdescripcion"]);
                    string fechaL = Functions.RequestPost(Request.Form["Mfecha"]);
                    DateTime fechaL2 = DateTime.Parse(fechaL);
                    fechaL = fechaL2.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    int añoL = Convert.ToInt32(Functions.RequestPost(Request.Form["Maño"]));
                    try
                    {
                        if (UpdateModel.Actualizar("", "MODELO", "nombre='" + nombreL + "',descripcion='" + descripcionL + "',fecha_creacion='" + fechaL + "',año=" + añoL +""  , "id_modelo=" + id_edit))
                        {
                            string fechaConvertida = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            //UpdateModel.ProcedimientoAlmacenado("registro_alteracionesCliente", UpdateUser.LinkedServer, SessionId + ",\"Actualizar\",\"" + fecha + "\"," + "\"cliente\"");

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
                    catch (Exception c)
                    {

                        alerta = "ERROR : " + c.Message;
                    }
                }

            }

        }
    }
}