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
    public partial class Actions : System.Web.UI.Page
    {
        MySql Acciones = new MySql();
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

            Functions Funciones = new Functions();
            int SessionId = Convert.ToInt32(Session["id"]);
            string texto = "";
 
            string ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            int[] bloqueados = (int[])Session["Bloqueados"];
            int[] desbloqueados = (int[])Session["Desbloqueados"];
            int[] eliminados = (int[])Session["Eliminados"];
            int[] Reseteados = (int[])Session["Reseteados"];
            Session.Remove("Bloqueados"); Session.Remove("Desbloqueados"); Session.Remove("Eliminados"); Session.Remove("Reseteados");
           
            if (Session["nombre"] != null && Session["rol"].ToString() == "4046" && Session["id"] != null)
            {
               
                if (bloqueados!=null && bloqueados.Length!=0)
                {
                    foreach (int id in bloqueados)
                    {
                        try
                        {
                            if (Acciones.Actualizar(Acciones.LinkedServer, "cliente", "idEstatus=25542", "id_cliente = " + id))
                            {
                                Acciones.ProcedimientoAlmacenado("registro_alteracionesCliente", Acciones.LinkedServer, "" + SessionId + ",\"Actualizar\",\"" + ahora + "\"," + "\"cliente\"");
                                texto = "Usuario bloqueado exitosamente";
                                Response.Write("<script>alert('" + texto + "'); window.history.go(-1); </script>");
                            }
                        }
                        catch (Exception c)
                        {
                            texto = "ERROR: " + c.Message;
                            Response.Write("<script>alert('" + texto + "'); window.history.go(-1); </script>");
                        }
                       
                    }
                }
                else if (desbloqueados != null && desbloqueados.Length != 0 )
                {
                    foreach(int id in desbloqueados)
                    {
                        try
                        {

                            if (Acciones.Actualizar(Acciones.LinkedServer, "cliente", "idEstatus=31448", "id_cliente = " + id))
                            {
                                Acciones.ProcedimientoAlmacenado("registro_alteracionesCliente", Acciones.LinkedServer, "" + SessionId + ",\"Actualizar\",\"" + ahora + "\"," + "\"cliente\"");

                                texto = "Usuario desbloqueado exitosamente";
                                     Response.Write("<script>alert('" + texto + "'); window.history.go(-1); </script>");

                            }
                        }
                        catch (Exception c)
                        {
                            texto = "ERROR: " + c.Message;
                            Response.Write("<script>alert('" + texto + "'); window.history.go(-1); </script>");

                        }

                    }
                }
                else if (eliminados != null && eliminados.Length!=0)
                {
                    foreach (int ide in eliminados)
                    {
                        consulta = "SELECT * FROM OPENQUERY(" + Acciones.LinkedServer + ",'SELECT * FROM cliente WHERE id_cliente =" + ide + "')";
                        Tuple<List<object[]>, int> drop = Acciones.Consulta(ref mens, consulta);
                        List<object[]> arrayUser = drop.Item1;
                        if (drop.Item2 >= 1)
                        {
                            int cu = 0;
                            int departamento = Convert.ToInt32(drop.Item1[0][5]);
                            if (departamento == 2505)
                            {
                                cu = 1;
                            }
                            else
                            {
                                consulta = "SELECT * FROM OPENQUERY(" + Acciones.LinkedServer + ",'SELECT * FROM cliente WHERE ((id_departamento = " + departamento + " AND id_cliente <> " + ide + " ) AND id_departamento<> 2505)  AND (id_rol = 4046 OR id_rol = 5267)')";
                                Tuple<List<object[]>, int> tec = Acciones.Consulta(ref mens, consulta);
                                List<object[]> arrayTec = tec.Item1; cu = tec.Item2;
                            }
                            if (cu >= 1)
                            {
                                int eliminar = ide;
                                consulta = "SELECT * FROM " + Acciones.LinkedServer + " ... ticket WHERE idUsuario = " + eliminar;
                                Tuple<List<object[]>, int> cr = Acciones.Consulta(ref mens, consulta);
                                int creados = cr.Item2;

                                consulta = "SELECT * FROM " + Acciones.LinkedServer + " ... ticket WHERE id_Atiende = " + eliminar + " AND idStatus = 94576";
                                Tuple<List<object[]>, int> re = Acciones.Consulta(ref mens, consulta);
                                int resueltos = re.Item2;

                                consulta = "SELECT * FROM " + Acciones.LinkedServer + " ... ticket WHERE id_Atiende = " + eliminar + " AND idStatus = 94574";
                                Tuple<List<object[]>, int> pe = Acciones.Consulta(ref mens, consulta);
                                int pendientes = pe.Item2;

                                consulta = "SELECT * FROM " + Acciones.LinkedServer + " ... ticket WHERE id_Atiende = " + eliminar + " AND idStatus = 94575";
                                Tuple<List<object[]>, int> pro = Acciones.Consulta(ref mens, consulta);
                                int proceso = pro.Item2;



                                if (Acciones.ProcedimientoAlmacenado("EliminarUsuario", Acciones.LinkedServer, "" + eliminar + ",\"" + ahora + "\",\"" + ahora + "\"," + pendientes + "," + creados + "," + resueltos + "," + proceso))
                                {
                                    Acciones.ProcedimientoAlmacenado("registro_alteracionesCliente", Acciones.LinkedServer, "" + SessionId + ",\"EliminarU\",\"" + ahora + "\"," + "\"cliente\"");
                                    Acciones.ProcedimientoAlmacenado("registro_alteracionesCliente", Acciones.LinkedServer, "" + SessionId + ",\"EliminarU\",\"" + ahora + "\"," + "\"ticket\"");
                                    Acciones.ProcedimientoAlmacenado("registro_alteracionesCliente", Acciones.LinkedServer, "" + SessionId + ",\"EliminarU\",\"" + ahora + "\"," + "\"departamento\"");
                                    texto = "Usuario eliminado correctamente";
                                    Response.Write("<script>alert('" + texto + "');window.history.go(-1);</script>");
                                }
                                else
                                {
                                    texto = "ERROR: Ocurrió un error, vuelve a intentarlo";
                                    Response.Write("<script>alert('" + texto + "');window.history.go(-1);</script>");
                                }
                            }
                            else
                            {
                                texto = "Por el momento no es posible eliminar el usuario porque no hay más usuarios de su rol";
                                    Response.Write("<script>alert('" + texto + "');window.history.go(-1);</script>");
                            }
                        }
                    }
                }

                else if (Reseteados != null && Reseteados.Length!=0)
                {
                    foreach (int id in Reseteados)
                    {
                        bool actualizado = false;
                        string permittedChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        string cifrado = Functions.GenerarCadenaAleatoria(permittedChars, 16);
                        string NewPassword = Convert.ToString(Funciones.CalculateMD5(cifrado));
                        actualizado = Acciones.Actualizar(Acciones.LinkedServer, "cliente","clave='" + NewPassword + "'","id_cliente=" + id);
                        consulta = "SELECT * FROM OPENQUERY(" + Acciones.LinkedServer + ", 'SELECT c.email_cliente,c.nombre_completo, c.id_cliente, d.nombre as Depa FROM cliente c INNER JOIN departamento d ON c.id_departamento = d.idDepartamento WHERE c.id_cliente=" + id + "')";
                        Tuple<List<object[]>, int> arrayPassword = Acciones.Consulta(ref mens, consulta);
                        List<object[]> InfoUser = arrayPassword.Item1;
                        int cu = arrayPassword.Item2;
                        if (cu >= 1 && actualizado == true)
                        {
                            try
                            {

                                string correo = Convert.ToString(arrayPassword.Item1[0][0]);
                                string nombre_completo = Convert.ToString(arrayPassword.Item1[0][1]);
                                string departamento = Convert.ToString(arrayPassword.Item1[0][3]);

                                consulta = "SELECT * FROM OPENQUERY(" + Acciones.LinkedServer + ", 'SELECT cast(aes_decrypt(e.contraseña, \"AES\") as char) as RECUPERAR ,d.correo FROM enviocorreo e INNER JOIN departamento d ON e.correo = d.idDepartamento WHERE e.id = 2')";
                                Tuple<List<object[]>, int> ListCorreo = Acciones.Consulta(ref mens, consulta);
                                List<object[]> InfoCorreo = ListCorreo.Item1;
                                int registrosCorreo = ListCorreo.Item2;
                                string recuperar = Convert.ToString(ListCorreo.Item1[0][0]);
                                string envia = Convert.ToString(ListCorreo.Item1[0][1]);
                                Functions.EnviarCorreo(envia, recuperar, correo, departamento, "Reseteo de contraseña", "<p style='text-align:justify;'>Estimado usuario <strong>" + nombre_completo + "</strong>: <br> Hemos reseteado su contraseña como lo solicitó. <br> Su nueva contraseña para acceder a su usuario de Soporte Técnico es: <b><strong>" + NewPassword + "</strong></b><br><br><br>" + "<img width='250px' height='auto' src='https://i.pinimg.com/564x/bd/e3/f8/bde3f81141a064e60a231874c29ddd6e.jpg' />" + "<br><br><br><p style='text-align:justify;'>Atentamente Soporte Técnico Alcomex<br><hr>Esperamos haber atendido satisfactoriamente su problema.</p>");
                                texto = "RESETEO EXITOSO";
                                Response.Write("<script>alert('" + texto + "'); window.history.go(-1); </script>");

                            }
                            catch (Exception ex)
                            {
                                texto = "ERROR " + ex.Message;
                                Response.Write("<script>alert('" + texto + "'); window.history.go(-1); </script>");
                            }
                        }
                        else
                        {
                            texto = "ERROR: No fue posible actualizar, revisa tu conexión y vuelve a intentarlo ";  
                            Response.Write("<script>alert('" + texto + "'); window.history.go(-1); </script>");
                        }  
                    }
                }
                else
                {
                    texto = "No haz seleccionado ningún usuario";
                   Response.Write("<script> alert('" + texto + "'); window.history.go(-1); </script>" );
                }
            }
            }
        }
    }
