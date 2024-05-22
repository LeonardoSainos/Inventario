using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Scripts;
using Inventario.Inventario.lib;


namespace Inventario.Inventario.admin.Actions
{
    public partial class ActionsBrands : System.Web.UI.Page
    {
        CONEXION Actionsbrands = new CONEXION();
        MySql ActionsbrandsMysql = new MySql();
        string aler = "", consulta = "", mens = "";
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
            string texto = "";
            string ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int [] eliminados = (int[]) Session["Eliminados"];
            Session.Remove("Eliminados");
            int idActivo = Session["id"] != null ? Convert.ToInt32(Request.Form["id"]) : (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
            HttpCookie rolIdCookie = Functions.ObtenerCookie("RolId");
            if ((Session["nombre"] != null && Session["rol"].ToString() == "4046" && Session["id"] != null) || (idActivo != 0 && Convert.ToString(rolIdCookie.Value) == "4046"))
            {
                if (eliminados != null && eliminados.Length != 0)
                {
                    int[] error = new int[eliminados.Length];
                    bool asegura = false;
                    int i = 0;
                    foreach (int ide in eliminados)
                    {
                        consulta = "SELECT * FROM MARCA WHERE id_marca =" + ide;
                        Tuple<List<object[]>, int> drop = Actionsbrands.Consulta(ref mens, consulta);
                        List<object[]> arrayBrands = drop.Item1;
                        try
                        {
                            if (drop.Item2 >= 1)
                            {
                                int eliminar = ide;
                                consulta = "SELECT * FROM VEHICULO WHERE id_marca =" + ide;
                                Tuple<List<object[]>, int> brandCar = Actionsbrands.Consulta(ref mens, consulta);
                                if (brandCar.Item2 >= 1)
                                {
                                    texto = "No hemos podido eliminar el tipo de vehiculo, ya que hay vehiculos activos con este. Eliminalos o actualiza los datos";
                                }
                                else if (brandCar.Item2 == 0)
                                {
                                    if (Actionsbrands.Eliminar("", "MARCA", "id_marca=" + ide))
                                    {
                                        ActionsbrandsMysql.ProcedimientoAlmacenado("registro_alteracionesCliente", ActionsbrandsMysql.LinkedServer, "" + idActivo + ",\"Eliminar\",\"" + ahora + "\"," + "\"Marca\"");
                                        texto = "Registros eliminados correctamente";
                                        aler = null;
                                        asegura = true;
                                    }
                                    else
                                    {
                                        texto = "ERROR: Ocurrió un error, vuelve a intentarlo. ID de error" + eliminar;
                                        asegura = false;
                                        aler = null;
                                        i++;
                                        error[i] = eliminar;
                                    }
                                }

                            }

                        }
                        catch (Exception c)
                        {
                            texto = "ERROR :" + c;

                        }

                    }

                    if (i > 1 && asegura == false)
                    {
                        Response.Write("<script>alert('Ocurrió un error con los tipos de Vehículos que tienen los ID" + eliminados + "');window.history.go(-1);</script>");

                    }
                    else
                    {
                        Response.Write("<script>alert('" + texto + "');window.history.go(-1);</script>");
                    }
                }            
            }

        }
    }
}