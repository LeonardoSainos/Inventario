using System;
using Inventario.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
 

namespace Inventario.Inventario.admin
{
    public partial class AddUser : System.Web.UI.UserControl
    {
        MySql AddUsers = new MySql();

        private string query = "", mens = "", alert="";
        private string[] ValEs, ValR, ValDe;
        private int[] idEs, idR, idDe;
        private int TotalEst = 0, TotalRol = 0, TotalDepa=0;

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
            get{ return query; }
            set { query = value; }
        }
        public string alerta
        {
            get { return alert; }
            set { alert = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            // GUARDAR NUEVO USUARIO 

            if (Request.Form["Gnombre"]!= null && Request.Form["Gapellidos1"]!= null && Request.Form["Gcorreo"]!= null) 
            {
                int SessionId = Convert.ToInt32(Session["id"]);
                string n = Functions.RequestPost(Request.Form["Gnombre"]);
                string a1 = Functions.RequestPost(Request.Form["Gapellidos1"]);
                string a2 = Functions.RequestPost(Request.Form["Gapellidos2"]);
                string GNcompleto = n + " " + a1 + " " + a2;
                string Gcorreo = Functions.RequestPost(Request.Form["Gcorreo"]);
                int Gdepartamento = Convert.ToInt32(Functions.RequestPost(Request.Form["Gdepartamento"]));
                int Grol = Convert.ToInt32(Functions.RequestPost(Request.Form["Grol"]));
                int Gestatus = Convert.ToInt32(Functions.RequestPost(Request.Form["Gestatus"]));
                string Gtelefono = Functions.RequestPost(Request.Form["Gtelefono"]);
                string Gusuario = Functions.RequestPost(Request.Form["Gusuario"]);
                consulta = "SELECT * FROM " + AddUsers.LinkedServer + " ... cliente WHERE email_cliente = ' " + Gcorreo + "' OR telefono_celular  = '" + Gtelefono + "'";
                Tuple<List<object[]>, int> verifica = AddUsers.Consulta(ref mens, consulta);
                if (verifica.Item2 <= 0)
                {
                    bool insertar=  AddUsers.Insertar("cliente", AddUsers.LinkedServer, "nombre_completo,nombre_usuario,email_cliente,id_departamento,id_rol,idEstatus,telefono_celular", "'" + GNcompleto + "','" + Gusuario + "','" + Gcorreo + "'," + Gdepartamento + "," + Grol + "," + Gestatus + "," + Gtelefono , ref mens);
                    if (insertar == true)
                    {
                        DateTime hoy = DateTime.Now;
                        string ahora = Convert.ToString(hoy);
                        alert = "<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>REGISTRO EXITOSO</h4><p class='text-center'>Cuenta creada exitosamente, resetea la contraseña del usuario para que posteriormente se le notifique que ya puede iniciar sesión.</p></div>";
                        AddUsers.ProcedimientoAlmacenado("registro_alteracionesCliente", "'" + AddUsers.LinkedServer+ "'",  + SessionId + ",\"Insertar\",\"" + ahora + "\"," + "\"cliente\"");
                        // Response.Redirect("./admin.aspx?view=admin");
                    }
                    else
                    {
                        alert = "<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'>  <button type = 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>OCURRIÓ UN ERROR</h4><p class='text-center'>Este correo o número de teléfono ya han sido registrados.</p></div>";
                    }
                }
            }
            query = "SELECT * FROM " + AddUsers.LinkedServer + " ... estatus WHERE ((idEstatus = 31448 OR idEstatus = 94573) OR (idEstatus=19231 OR idEstatus = 25542)) ORDER BY Nombre";
            Tuple<List<object[]>, int> estatus = AddUsers.Consulta(ref mens, query);
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
            query = "SELECT * FROM " + AddUsers.LinkedServer + " ... rol";
            Tuple<List<object[]>, int> rol = AddUsers.Consulta(ref mens, query);
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
            query = "SELECT idDepartamento,nombre FROM " + AddUsers.LinkedServer + " ... departamento";
            Tuple<List<object[]>, int> depa = AddUsers.Consulta(ref mens, query);
            //List<object[]> arrayDepa = depa.Item1;
            if (depa.Item2 >= 1)
            {
                TotalDepa = depa.Item2;
                idDe = new int[TotalDepa];
                ValDe = new string[TotalDepa];
                for(int k = 0; k < TotalDepa; k++)
                {
                    ValDe[k] = Convert.ToString(depa.Item1[k][1]);
                    idDe[k] = Convert.ToInt32(depa.Item1[k][0]);
                }
            }   
        }
    }
}