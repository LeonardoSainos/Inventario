using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using Inventario.Scripts;
namespace Inventario.Inventario.admin.Marcas
{
    public partial class brandsEdit_view : System.Web.UI.UserControl
    {
        CONEXION UpdateBrand = new CONEXION();
        MySql UpdateBrandMysql = new MySql();
        private string query = "", mens = "", alert = "";
        private string fechaText = "", nombreText = "", descripcionText = "";
        private int id_edit = 0;
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
            if (Request.QueryString["idB"] != null)
            {
                int brandId = Convert.ToInt32(Functions.RequestGet(Request.QueryString["idB"]));
                id_edit = brandId;
                consulta = "SELECT * FROM MARCA WHERE id_marca =" + id_edit;
                Tuple<List<object[]>, int> BrandData = UpdateBrand.Consulta(ref mens, consulta);
                int contador = BrandData.Item2;

                nombreText = !string.IsNullOrEmpty(Convert.ToString(BrandData?.Item1?[0][1])) ? Convert.ToString(BrandData.Item1[0][1]) : nombreText;
                descripcionText = !string.IsNullOrEmpty(Convert.ToString(BrandData?.Item1?[0][2])) ? Convert.ToString(BrandData.Item1[0][2]) : descripcionText; fechaText = Convert.ToString(BrandData.Item1[0][3]);
                fechaText = !string.IsNullOrEmpty(Convert.ToString(BrandData?.Item1[0][3])) ? Convert.ToString(BrandData.Item1[0][3]) : fechaText;
                if (!string.IsNullOrEmpty(fechaText))
                {
                    DateTime fechaHora = DateTime.Parse(fechaText);
                    fechaText = fechaHora.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                if (Request.Form["id_edit"] != null & Request.Form["Bnombre"] != null)
                {
                    int idActivo = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
                   (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);



                    string nombreL = Functions.RequestPost(Request.Form["Bnombre"]);
                    string descripcionL = Functions.RequestPost(Request.Form["Bdescripcion"]);
                    string fechaL = Functions.RequestPost(Request.Form["Bfecha"]);
                    DateTime fechaL2 = DateTime.Parse(fechaL);
                    fechaL = fechaL2.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    try
                    {
                        if (UpdateBrand.Actualizar("", "MARCA", "nombre='" + nombreL + "',descripcion='" + descripcionL + "',fecha_creacion='" + fechaL + "'", "id_marca=" + id_edit))
                        {
                            string fechaConvertida = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            //PENDIENTE
                            UpdateBrandMysql.ProcedimientoAlmacenado("registro_alteracionesCliente", UpdateBrandMysql.LinkedServer, idActivo + ",\"Actualizar\",\"" + fechaConvertida + "\"," + "\"marca\"");
                            alerta = @"<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10; '> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                 <h4 class='text-center'>Registro Actualizado</h4>
                                   <p class='text-center'>
                                    La marca de vehiculo fue actualizado con éxito
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
   