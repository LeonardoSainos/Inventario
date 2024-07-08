using Inventario.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Inventario.Inventario.lib;
using System.Text;
namespace Inventario.Inventario.inc
{
    public class ApplicationUser
    {
        public string Username { get; set; }
        public string PwdName { get; set; }
    }

    public partial class Login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public static T GetValueOrDefault<T>(object value)
        {
            return value == DBNull.Value ? default(T) : (T)value;
        }


        protected void Iniciar(object sender, EventArgs e)
        {
           ApplicationUser UserLogin = new ApplicationUser();
            MySql obj = new MySql();
            Functions Funciones = new Functions();
            string mens = "";       
            UserLogin.PwdName= Convert.ToString(Functions.RequestPost(Funciones.CalculateMD5(txtPassword.Text)));
            UserLogin.Username=  Convert.ToString(Functions.RequestPost(txtUsuario.Text));
            string usuario = UserLogin.Username;
            string clave = UserLogin.PwdName;
            //string texto = rblLogin.SelectedItem.Text;
            //string valor = rblLogin.SelectedItem.Value;
            //int indice = rblLogin.SelectedIndex;
            if ((clave != null && usuario!= null))
            {
                
                            string  consulta = "SELECT * FROM " + obj.LinkedServer + " ... cliente WHERE((email_cliente = '" + usuario + "' OR nombre_usuario = '" + usuario + "') AND clave = '" + clave + "')   AND idEstatus <> 25542";
                            Tuple<List<object[]>, int> resultado = obj.Consulta(ref mens, consulta);
                            List<object[]> registros = resultado.Item1;
                            int contador = resultado.Item2;
                            int row1 = 0, row6 = 0, row7 = 0, row10 = 0, row11 = 0, row12=0;
                            string row2 = "", row3 = "", row4 = "", row5 = "", row8 = "";
                            DateTime row9 = DateTime.Now;
                            foreach (object[] registro in registros)
                            {
                               // Suponiendo que las columnas son de los tipos conocidos (por ejemplo, string, int)
                                row1 = GetValueOrDefault<int>(registro[0]);
                                row2 = GetValueOrDefault<string>(registro[1]);
                                row3 = GetValueOrDefault<string>(registro[2]);
                                row4 = GetValueOrDefault<string>(registro[3]);
                                row5 = GetValueOrDefault<string>(registro[4]);
                                row6 = GetValueOrDefault<int>(registro[5]);
                                row7 = GetValueOrDefault<int>(registro[6]);
                                row8 = GetValueOrDefault<string>(registro[7]);
                                row9 = GetValueOrDefault<DateTime>(registro[8]);
                                row10 = GetValueOrDefault<int>(registro[9]);
                                row11 = GetValueOrDefault<int>(registro[10]);
                                row12 = GetValueOrDefault<int>(registro[11]);
                              // Utiliza los valores de las columnas como desees
                            }
                            if (contador > 0)
                            {
                                Session["id"] = row1;
                                Session["nombre_completo"] = row2;
                                Session["nombre"] = row3;
                                Session["email"] = row4;
                                Session["clave"] = row5;
                                Session["departamento"] = row6;
                                Session["rol"] = row7;
                                Session["telefono"] = row8;
                                Session["fecha"] = row9;
                                Session["estatus"] = row10;
                                Session["anydesk"] = row11;
                                Session["area"] = row12;

                                // Obtener el ID del usuario de la base de datos u otra fuente de datos
     
                                Functions.CrearCookie(Convert.ToString(Session["id"]),"UserId",Response);                      
                                Functions.CrearCookie(Convert.ToString(Session["rol"]), "RolId", Response);
                                Functions.CrearCookie(Convert.ToString(Session["nombre_completo"]), "CompletoName", Response);
                                Functions.CrearCookie(Convert.ToString(Session["nombre"]), "UserName", Response);
                                Functions.CrearCookie(Convert.ToString(Session["email"]), "Email", Response);
                                // Paral impiar radiobutton se usa rblLogin.SelectedIndex = -1;
                                Response.Redirect("index.aspx?view=index");
                            }
                            else
                            {
                                Response.Write("<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type= 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><h4 class='text-center'>OCURRIÓ UN ERROR</h4><p class='text-center'> Nombre de usuario o contraseña incorrectos</p> </div>");
                            }               
            }
            else
            {
                Response.Write(@"<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'>" +
                    "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
                    "<h4 class='text-center'>OCURRIÓ UN ERROR</h4>" +
                    "<p class='text-center'> No puedes dejar ningún campo vacío </p></div>");
            }
        }
    }
}
