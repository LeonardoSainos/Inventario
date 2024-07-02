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
    public partial class AddCar : System.Web.UI.UserControl
    { MySql AddCarsMysql = new MySql();
        CONEXION AddCars = new CONEXION();
        private string query = "", mens = "", alert = "";
        private string[] ValEs, ValR, ValDe, ValTi, ValMa, ValMo, ValOper, ValPROGPS, valGps;
        private int[] idEs, idR, idDe, idTi, idMa, idMod, idOper, idPROGPS, idGPS;
        private int TotalEst = 0, TotalRol = 0, TotalDepa = 0, TotalType = 0, TotalMarca = 0, TotalModelo = 0, TotalOperador = 0, TotalPROGPS=0, TotalGps=0;
         
        
        public int TotalGP
        {
            set { TotalGps = value; }
            get { return TotalGps; }
        }

        public int TotalOper
        {
            set { TotalOperador = value; }
            get { return TotalOperador; }
        }
        public int TotalPROGps
        {
            set { TotalPROGPS = value; }
            get { return TotalPROGPS; }
        }
        public int Totest
        {
            set { TotalEst = value; }
            get { return TotalEst; }
        }

        public int TotTy
        {
            set { TotalType = value; }
            get { return TotalType; }
        }
        public int TotalMode
        {
            set { TotalModelo = value; }
            get { return TotalModelo; }
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
        public int TotalMa
        {
            set { TotalMarca = value; }
            get { return TotalMarca; }
        }
        public int[] idprogps
        {
            set { idPROGPS = value; }
            get { return idPROGPS; }
        }
        public int [] idGPSS
        {
            get { return idGPS; }
            set { idGPS = value; }
        }
        public int[] idOperador
        {
            get { return idOper; }
            set { idOper = value; }
        }
        public int[] idModelo
        { get { return idMod; }
            set { idMod = value; }
        }
        public int[] idMarcas
        {
            get { return idMa; }
            set { idMa = value; }
        }
        public int[] idTipos
        {
            get { return idTi; }
            set { idTi = value; }
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
        public string [] valiGps
        {
            get { return valGps; }
            set { valGps = value; }
        }
        public string[] valprogps
        {
            get { return ValPROGPS; }
            set { ValPROGPS = value; }
        }
        public string[] valOper
        {
            get { return ValOper; }
            set { ValOper = value; }
        }
       
        public string[] valModelo
        {
            get { return ValMo; }
            set { ValMo = value; }
        }
        public int[] ideDepa
        {
            get { return idDe; }
            set { idDe = value; }
        }
        public string[] valorTi
        {
            get { return ValTi; }
            set { ValTi = value; }
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
        public string[] valorMa
        {
            get { return ValMa; }
            set { ValMa = value; }
         
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
            if (Request.Form["Geconomico"] != null || Request.Form["Gtypee"] != null)
            {
                int idActivo = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
                (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
                string Necnonomico = Convert.ToString(Functions.RequestPost(Request.Form["Geconomico"]));
                int type = Convert.ToInt32(Functions.RequestPost(Request.Form["Gtypee"]));
                int brand = Convert.ToInt32(Functions.RequestPost(Request.Form["Gmarca"]));
                int model = Convert.ToInt32(Functions.RequestPost(Request.Form["Gmodelo"]));
                string placas = Convert.ToString(Functions.RequestPost(Request.Form["Gplacas"]));
                string Nserie = Convert.ToString(Functions.RequestPost(Request.Form["Gserie"]));
                string poliza = Convert.ToString(Functions.RequestPost(Request.Form["Gpoliza"]));
                string vigencia = Convert.ToString(Functions.RequestPost(Request.Form["Gvigencia"]));
                if (DateTime.TryParse(vigencia, out DateTime parsedVigencia) | vigencia!=null)
                {                    vigencia = parsedVigencia.ToString("yyyy/MM/dd");
                }
                int estatusId = Convert.ToInt32(Functions.RequestPost(Request.Form["Gestatus"]));
                int provedorGps = Convert.ToInt32(Functions.RequestPost(Request.Form["Gprovedorgps"]));
                int GPS = Convert.ToInt32(Functions.RequestPost(Request.Form["Ggps"]));
                int Useroperador=999999;
                if (Request.Form["Goperador"]!= "N/A")
                {
                    Useroperador = Convert.ToInt32(Functions.RequestPost(Request.Form["Goperador"]));
                }
                consulta = $"SELECT * FROM VEHICULO WHERE (nombre_vehiculo='{Necnonomico}' AND (nombre_vehiculo  IS NOT NULL AND nombre_vehiculo<>'')) OR (placas='{placas}' AND (placas IS NOT NULL AND placas <>'')) OR ((poliza_seguro='{poliza}' AND (poliza_seguro IS NOT NULL AND poliza_seguro<>'' )) OR (Numero_Serie='{Nserie}' AND (Numero_Serie IS NOT NULL AND Numero_serie<>''))) ORDER BY nombre_vehiculo";
                Tuple<List<object[]>, int> Vehiculos = AddCars.Consulta(ref mens, consulta);
                try
                {
                    if (Vehiculos.Item2 < 1)
                    {                       // nombre, idtipo, id marca,idmodelo,placas,numeroserie,poliza,vigencia,idestatus,propietario,gps,cliente
                        bool insertar = AddCars.Insertar("VEHICULO", "", "nombre_vehiculo,id_tipo,id_marca,id_modelo,placas,Numero_serie,poliza_seguro,vigencia_poliza,id_status,id_propietario,id_gps,id_cliente", "'" + Necnonomico + "'," + type + "," + brand + "," + model + ",'" + placas + "','" + Nserie + "','" + poliza + "','" + vigencia + "'," + estatusId + "," + provedorGps + "," + GPS + "," + Useroperador, ref mens);
                        if (insertar == true)
                        {
                            string ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            alert = "<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>REGISTRO EXITOSO</h4><p class='text-center'>Vehículo creado de forma correcta.</p></div>";
                            AddCarsMysql.ProcedimientoAlmacenado("registro_alteracionesCliente", "'" + AddCarsMysql.LinkedServer + "'", +idActivo + ",\"Insertar\",\"" + ahora + "\"," + "\"cliente\"");
                        }
                    }
                    else
                    {
                        alert = "<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'>  <button type = 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <h4 class='text-center'>OCURRIÓ UN ERROR</h4><p class='text-center'>Este Vehiculo ya ha sido registrado.</p></div>";
                    }
                }
                catch(Exception c)
                {
                    alert = "ERROR: " + c.Message;
                }
            }
           
            query = "SELECT id_tipo, nombre FROM TIPO ORDER BY nombre";
            Tuple<List<object[]>, int> tipo = AddCars.Consulta(ref mens, query);
            if (tipo.Item2 >= 1)
            {
                TotalType = tipo.Item2;
                idTi = new int[TotalType];
                ValTi = new string[TotalType];
                for(int k=0; k<TotalType; k++)
                {
                    ValTi[k] = Convert.ToString(tipo.Item1[k][1]);
                    idTi[k] = Convert.ToInt32(tipo.Item1[k][0]);
                }
            }

            query = "SELECT id_marca, nombre FROM MARCA ORDER BY nombre";
            Tuple<List<object[]>, int> marca = AddCars.Consulta(ref mens, query);
            if (marca.Item2 >= 1)
            {
                TotalMarca = marca.Item2;
                idMa = new int[TotalMarca];
                ValMa = new string[TotalMarca];
                for (int i=0; i< TotalMarca; i++)
                {
                    ValMa[i] = Convert.ToString(marca.Item1[i][1]);
                    idMa[i] = Convert.ToInt32(marca.Item1[i][0]);
                }
            }

            query = "SELECT id_modelo,nombre FROM MODELO ORDER BY nombre";
            Tuple<List<object[]>, int> modelo = AddCars.Consulta(ref mens, query);
            if (modelo.Item2 >= 1)
            {
                TotalModelo = modelo.Item2;
                idMod = new int[TotalModelo];
                ValMo = new string[TotalModelo];
                for(int j = 0; j < TotalModelo; j++)
                {
                    ValMo[j] = Convert.ToString(modelo.Item1[j][1]);
                    idMod[j]= Convert.ToInt32(modelo.Item1[j][0]);
                }

            }

            query = "SELECT * FROM " + AddCarsMysql.LinkedServer + "... estatus WHERE idEstatus between 94577 AND 94583  ORDER BY Nombre ";
            Tuple<List<object[]>, int> estatus = AddCarsMysql.Consulta(ref mens, consulta);
            if (estatus.Item2 >= 1)
            {
                TotalEst = estatus.Item2;
                idEs = new int[TotalEst];
                ValEs = new string[TotalEst];
                for (int z = 0; z < TotalEst; z++)
                {
                    ValEs[z] = Convert.ToString(estatus.Item1[z][1]);
                    idEs[z] = Convert.ToInt32(estatus.Item1[z][0]);
                }


            }
            query = "SELECT id_cliente,nombre_completo FROM " + AddCarsMysql.LinkedServer + " ... cliente c WHERE(id_cliente NOT IN (SELECT id_cliente FROM VEHICULO)  AND (((idEstatus=31448 OR idEstatus=94573 ) OR (idEstatus=19231 OR idEstatus=25542 )) AND id_rol= 4254)) ORDER BY c.nombre_completo";
            Tuple<List<object[]>, int> operador = AddCarsMysql.Consulta(ref mens, query);
            if (operador.Item2 >= 1)
            {
                TotalOper = operador.Item2;
                idOper = new int[TotalOper];
                valOper = new string[TotalOper];
                for (int y=0; y<TotalOper; y++)
                {
                    valOper[y] = Convert.ToString(operador.Item1[y][1]);
                    idOper[y] = Convert.ToInt32(operador.Item1[y][0]);
                }
            }

            query = "SELECT id_propietarioEmpresa,nombre FROM PROPIETARIO_VEHICULO ORDER BY nombre";
            Tuple<List<object[]>,int> propCar = AddCars.Consulta(ref mens, consulta);
            if (propCar.Item2 >= 1)
            {
                TotalPROGPS = propCar.Item2;
                idPROGPS = new int[TotalPROGPS];
                ValPROGPS = new string[TotalPROGPS];
                for (int x = 0; x< TotalPROGPS; x++)
                {
                    ValPROGPS[x] = Convert.ToString(propCar.Item1[x][1]);
                    idPROGPS[x] = Convert.ToInt32(propCar.Item1[x][0]);
                }
            }

            query = "SELECT id_gps,nombre FROM GPS ORDER BY nombre";
            Tuple<List<object[]>, int> gps = AddCars.Consulta(ref mens, consulta);
            if (gps.Item2 >= 1)
            {
                TotalGps = gps.Item2;
                idGPS = new int[TotalGps];
                valGps = new string[TotalGps];
                for(int a =0; a< TotalGps; a++)
                {
                    valGps[a] = Convert.ToString(gps.Item1[a][1]);
                    idGPS[a] = Convert.ToInt32(gps.Item1[a][0]);
                }
            }

        }
    }
}