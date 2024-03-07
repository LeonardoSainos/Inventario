using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using Inventario.Scripts;
namespace Inventario.Inventario.admin
{
    public partial class userEdit_view : System.Web.UI.UserControl
    {
      
        MySql UpdateUser = new MySql();
        private string query = "", mens = "", alert = "";
        private string[] ValEs, ValR, ValDe;
        private int[] idEs, idR, idDe;
        private int TotalEst = 0, TotalRol = 0, TotalDepa = 0;
        private string fechaText = "", nombreText = "", correoText = "", usuarioText = "", estadoText = "", depaText = "", rolText = "", telefonoText = "", anydeskText = "";
        private int idRolText = 0, idDepaText = 0, idEstatusText = 0, id_edit = 0;
        public int idEdit{ get;set;}
        public int txtIdRol
        {
            set { idRolText = value; }
            get { return idRolText; }
        }
        public int txtIdDepa
        {
            set { idDepaText = value; }
            get { return idDepaText; }
        }
        public int txtIdEstatus
        {
            set { idEstatusText = value; }
            get { return idEstatusText; }
        }
        public string txtTelefono
        {
            set { telefonoText = value; }
            get { return  telefonoText; }
        }
        public string txtAnydesk
        {
            set { anydeskText = value; }
            get { return anydeskText; }
        }
        public string txtFecha
        {
            set { fechaText = value; }
            get { return fechaText; }
        }
        public string txtNombre
        {
            set {  nombreText= value; }
            get { return nombreText; }
        } 
        public string txtCorreo
        {
            set { correoText = value; }
            get { return correoText; }
        }
        public string txtUsuario
        {
            set { usuarioText = value; }
            get { return usuarioText; }
        }
        public string txtEstado
        {
            set { estadoText = value; }
            get { return estadoText; }
        }
        public string txtDepa
        {
            set { depaText = value; }
            get { return depaText; }
        }
        public string txtRol
        {
            set { rolText = value; }
            get { return rolText;  }
        }

        public int Totest
        {
            set { TotalEst = value; }
            get { return TotalEst; }
        }
        public int TotR
        {
            set { TotalRol = value; }
            get { return TotalRol; }
        }
        public int TotalD
        {
            set { TotalDepa = value; }
            get { return TotalDepa; }
        }
        public int[] ideEstatus
        {
            get { return idEs; }
            set { idEs = value; }
        }
        public int[] ideRol
        {
            get { return idR; }
            set { idR = value; }
        }
        public int[] ideDepa
        {
            get { return idDe; }
            set { idDe = value; }
        }
        public string[] valorEst
        {
            get { return ValEs; }
            set { ValEs = value; }
        }
        public string[] valorDepa
        {
            get { return ValDe; }
            set { ValDe = value; }
        }
        public string[] valorRol
        {
            get { return ValR; }
            set { ValR = value; }
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
        { string mensaje = "";

            if (Request.QueryString["id"] != null)
            {
                int user = Convert.ToInt32(Functions.RequestGet(Request.QueryString["id"]));
                id_edit = user;
                Tuple<List<object[]>, int> UserData = UpdateUser.Consulta(ref mensaje, $"SELECT * FROM OPENQUERY(" + UpdateUser.LinkedServer + ",'SELECT DISTINCT r.idRol,r.Nombre as Nrol, c.telefono_celular,c.Fecha_creacion,c.id_cliente,c.nombre_completo,c.email_cliente,c.nombre_usuario,e.Nombre as NombreE, e.idEstatus, d.nombre, d.idDepartamento, c.anydesk FROM cliente c INNER JOIN estatus e ON c.idEstatus = e.idEstatus INNER JOIN departamento d ON d.idDepartamento = c.id_departamento INNER JOIN rol r ON c.id_rol = r.idRol WHERE c.id_cliente = " + user + "')");
                int contador = UserData.Item2;

                idRolText = Convert.ToInt32(UserData.Item1[0][0]);
                rolText = Convert.ToString(UserData.Item1[0][1]);
                telefonoText = Convert.ToString(UserData.Item1[0][2]);
                fechaText = Convert.ToString(UserData.Item1[0][3]);
               nombreText = Convert.ToString(UserData.Item1[0][5]);
                correoText= Convert.ToString(UserData.Item1[0][6]);
                usuarioText = Convert.ToString(UserData.Item1[0][7]);
                estadoText = Convert.ToString(UserData.Item1[0][8]);
                idEstatusText = Convert.ToInt32(UserData.Item1[0][9]);
                depaText = Convert.ToString(UserData.Item1[0][10]);
                idDepaText = Convert.ToInt32(UserData.Item1[0][11]);
               anydeskText = Convert.ToString(UserData.Item1[0][12]);
            }  
            if (Request.Form["id_edit"] != null && Request.Form["nombre_completo"] != null && Request.Form["email_cliente"] != null)
            {
                int SessionId = Convert.ToInt32(Session["id"]);
                //int id_edit = Convert.ToInt32(Functions.RequestPost(Request.Form["id_edit"]));
                int estado = Convert.ToInt32(Functions.RequestPost(Request.Form["estado_cliente"]));
                int departamento = Convert.ToInt32(Functions.RequestPost(Request.Form["departamento_cliente"]));
                int role = Convert.ToInt32(Functions.RequestPost(Request.Form["rol_cliente"]));
                string correo = Functions.RequestPost(Request.Form["email_cliente"]);
                string telefono = Functions.RequestPost(Request.Form["telefono"]);
                int anydesk = Convert.ToInt32(Functions.RequestPost(Request.Form["anydesk"]));
                string nombre_completo = Functions.RequestPost(Request.Form["nombre_completo"]);
                nombre_completo = nombre_completo.ToUpper();

                   try
                {


                    if (UpdateUser.Actualizar(UpdateUser.LinkedServer, "cliente", "anydesk=" + anydesk + ", nombre_completo='" + nombre_completo + "', telefono_celular=" + telefono + ", email_cliente='" + correo + "',id_departamento=" + departamento + ", id_rol=" + role + ",idEstatus =" + estado, "id_cliente = " + id_edit))
                    {

                        string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        UpdateUser.ProcedimientoAlmacenado("registro_alteracionesCliente", UpdateUser.LinkedServer, SessionId + ",\"Actualizar\",\"" + fecha + "\"," + "\"cliente\"");
                        
                            alerta = @"<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10; '> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                 <h4 class='text-center'>Usuario Actualizado</h4>
                       <p class='text-center'>
                        El usuario fue actualizado con éxito
                        </p>
                      </div>";
                        
                      
                    }
                    else
                    {
                        alerta = @"<div class='alert alert-danger alert-dismissible fade in col -sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10;'> 
                                <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                    <h4 class='text-center'>OCURRIÓ UN ERROR</h4>
                    <p class='text-center'>
                        No hemos podido actualizar el usuario
                    </p>
                </div>";
                    }
                }
                catch(Exception c)
                {
                  alerta = "ERROR :" +  c.Message;
                }


            }
            query = "SELECT * FROM " + UpdateUser.LinkedServer + " ... estatus WHERE (( Nombre <> '" +  txtEstado +  "' ) AND ((idEstatus = 31448 OR idEstatus = 94573) OR (idEstatus=19231 OR idEstatus = 25542))) ORDER BY Nombre";
            Tuple<List<object[]>, int> estatus = UpdateUser.Consulta(ref mens, query);
            // List<object[]> arrayEstatus = estatus.Item1;
            if (estatus.Item2 >= 1)
            {
                TotalEst = estatus.Item2;
                idEs = new int[estatus.Item2];
                ValEs = new string[estatus.Item2];
                for (int i = 0; i < estatus.Item2; i++)
                {
                    ValEs[i] = Convert.ToString(estatus.Item1[i][1]);
                    idEs[i] = Convert.ToInt32(estatus.Item1[i][0]);
                }
            }
            query = "SELECT * FROM " + UpdateUser.LinkedServer + " ... rol where Nombre<>'" + txtRol +"'";
            Tuple<List<object[]>, int> rol =  UpdateUser.Consulta(ref mens, query);
            //List<object[]> arrayRol = rol.Item1;
            if (rol.Item2 >= 1)
            {
                TotalRol = rol.Item2;
                idR = new int[TotalRol];
                ValR = new string[TotalRol];
                for (int j = 0; j < rol.Item2; j++)
                {
                    ValR[j] = Convert.ToString(rol.Item1[j][1]);
                    idR[j] = Convert.ToInt32(rol.Item1[j][0]);
                }
            }
            query = "SELECT idDepartamento,nombre FROM " + UpdateUser.LinkedServer + " ... departamento where nombre <> '" + txtDepa + "'";
            Tuple<List<object[]>, int> depa = UpdateUser.Consulta(ref mens, query);
            //List<object[]> arrayDepa = depa.Item1;
            if (depa.Item2 >= 1)
            {
                TotalDepa = depa.Item2;
                idDe = new int[TotalDepa];
                ValDe = new string[TotalDepa];
                for (int k = 0; k < TotalDepa; k++)
                {
                    ValDe[k] = Convert.ToString(depa.Item1[k][1]);
                    idDe[k] = Convert.ToInt32(depa.Item1[k][0]);
                }
            }

        }
    }
}