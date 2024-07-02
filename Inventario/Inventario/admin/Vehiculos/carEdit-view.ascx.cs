using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Inventario.lib;
using Inventario.Scripts;
namespace Inventario.Inventario.admin.Vehiculos
{
    public partial class VehiculosEdit_view : System.Web.UI.UserControl
    {
        CONEXION UpdateVehiculo = new CONEXION();
        MySql UpdateVehiculoMysql = new MySql();
        private string query = "", mens = "", alert = "";
        private int totalEst = 0, totalMarca = 0, totalTipo = 0, totalModelo = 0, totalGps = 0, totalProvGps = 0, totalOper = 0;
        private string asignadoOperador = "", vehiculo = "", placas = "", num_serie = "", poliza_seguro = "", vigencia = "", fechaCreado = "", fechaMecanica = "", fechaActualizado = "", estatusText = "", marcaText = "", tipoText = "", modeloText = "", gpstext = "", propietariogpsText = "";
        private int idOperador = 0, idEstatus = 0, idMarca = 0, idTipo = 0, idModelo = 0, idGps = 0, idPropietarioGps = 0;
        private string[] ValEs, ValM, ValTi, ValMod, ValG, ValPG, ValOper;
        private int[] idEs, idM, idTi, idMod, idG, idPG, idOper;
        public string mensaje { get { return mens; } set { mens = value; } }
        public string consulta { get { return query; } set { query = value; } }
        public string alerta { get { return alert; } set { alert = value; } }
        public int idEdit { get; set; }
        public string Vehiculo { get { return vehiculo; } set { vehiculo = value; ; } }
        public string Placas { get { return placas; } set { placas = value; ; } }
        public string NumeroSerie { get { return num_serie; } set { num_serie = value; ; } }
        public string PolizaSeguro { get { return poliza_seguro; } set { poliza_seguro = value; ; } }
        public string Vigencia { get { return vigencia; } set { vigencia = value; ; } }
        public string FechaMecanica { get { return fechaMecanica; } set { fechaMecanica = value; ; } }
        public string FechaActualizado { get { return fechaActualizado; } set { fechaActualizado = value; ; } }
        public string FechaCreado { get { return fechaCreado; } set { fechaCreado = value; ; } }
        public string AsignadoOperador { get { return asignadoOperador; } set { asignadoOperador = value; } }
        public int IDEstatus { get { return idEstatus; } set { idEstatus = value; ; } }
        public int IDMarca { get { return idMarca; } set { idMarca = value; } }
        public int IDTipo { get { return idTipo; } set { idTipo = value; ; } }
        public int IDModelo { get { return idModelo; } set { idModelo = value; ; } }
        public int IDGps { get { return idGps; } set { idGps = value; ; } }
        public int IDPropietario { get { return idPropietarioGps; } set { idPropietarioGps = value; ; } }
        public int IDOperador { get { return idOperador; } set { idOperador = value; ; } }
        public string txtEstatus { get { return estatusText; } set { estatusText = value; } }
        public string txtTipo { get { return tipoText; } set { tipoText = value; } }
        public string txtModelo { get { return modeloText; } set { modeloText = value; } }
        public string txtMarca { get { return marcaText; } set { marcaText = value; } }
        public string txtGPS { get { return gpstext; } set { gpstext = value; } }
        public string txtPropGps { get { return propietariogpsText; } set { propietariogpsText = value; } }
        public int TotalEstatus { get { return totalEst; } set { totalEst = value; } }
        public int TotalModelo { get { return totalModelo; } set { totalModelo = value; } }
        public int TotalMarca { get { return totalMarca; } set { totalMarca = value; } }
        public int TotalTipo { get { return totalTipo; } set { totalTipo = value; } }
        public int TotalGps { get { return totalGps; } set { totalGps = value; } }
        public int TotalPropietarioGps { get { return totalProvGps; } set { totalProvGps = value; } }
        public int TotalOperador { get { return totalOper; } set { totalOper = value; } }
        public int[] ideEstatus { get { return idEs; } set { idEs = value; } }
        public int[] ideMarca { get { return idM; } set { idM = value; } }
        public int[] ideTipo { get { return idTi; } set { idTi = value; } }
        public int[] ideModelo { get { return idMod; } set { idMod = value; } }
        public int[] ideGps { get { return idG; } set { idG = value; } }
        public int[] ideProvedorGps { get { return idPG; } set { idPG = value; } }
        public int[] ideOperador { get { return idOper; } set { idOper = value; } }
        public string[] validarEstatus { get { return ValEs; } set { ValEs = value; } }
        public string[] validarMarca { get { return ValM; } set { ValM = value; } }
        public string[] validarTipo { get { return ValTi; } set { ValTi = value; } }
        public string[] validarModelo { get { return ValMod; } set { ValMod = value; } }
        public string[] validarGPS { get { return ValG; } set { ValG = value; } }
        public string[] validarProvGP { get { return ValPG; } set { ValPG = value; } }
        public string[] validarOperador { get { return ValOper; } set { ValOper = value; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            string mensaje = "";
            int user = Session["id"] != null ? Convert.ToInt32(Session["id"]) : (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
            if (Request.QueryString["idC"] != null)
            {

                idEdit = Convert.ToInt32(Functions.RequestGet(Request.QueryString["idC"]));
                Tuple<List<object[]>, int> CarData = UpdateVehiculo.Consulta(ref mensaje, "SELECT v.id_vehiculo,v.nombre_vehiculo, v.fecha_actualizacion as actualizado ,v.fecha_creacion, c.nombre_completo AS asignado, v.placas, e.nombre as estatus, m.nombre as marca, t.nombre as tipo, v.Numero_serie,v.poliza_seguro, v.vigencia_poliza, mo.nombre as modelo, gp.nombre as GPS, pv.nombre as propietarioGps, v.fecha_revisionMecanica,e.idEstatus,m.id_marca,t.id_tipo,mo.id_modelo,gp.id_gps,pv.id_propietarioEmpresa,c.id_cliente FROM VEHICULO v INNER JOIN OPENQUERY(mysql_ticket, ' SELECT * FROM ESTATUS') e ON e.idEstatus = v.id_status INNER JOIN MARCA m ON m.id_marca = v.id_marca INNER JOIN TIPO t ON t.id_tipo = v.id_tipo INNER JOIN MODELO mo  ON mo.id_modelo = v.id_modelo INNER JOIN OPENQUERY(mysql_ticket, 'SELECT * FROM cliente') c ON  v.id_cliente = c.id_cliente  INNER JOIN GPS gp ON gp.id_gps = v.id_gps  INNER JOIN PROPIETARIO_VEHICULO pv ON pv.id_propietarioEmpresa = v.id_propietario WHERE v.id_vehiculo =" + idEdit);
                int contador = CarData.Item2;
                vehiculo = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][1])) ? Convert.ToString(CarData.Item1[0][1]) : vehiculo;
                fechaActualizado = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][2])) ? Convert.ToString(CarData.Item1[0][2]) : fechaActualizado;
                fechaCreado = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][3])) ? Convert.ToString(CarData.Item1[0][3]) : fechaCreado;
                asignadoOperador = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][4])) ? Convert.ToString(CarData.Item1[0][4]) : asignadoOperador;
                placas = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][5])) ? Convert.ToString(CarData.Item1[0][5]) : placas;
                estatusText = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][6])) ? Convert.ToString(CarData.Item1[0][6]) : estatusText;
                marcaText = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][7])) ? Convert.ToString(CarData.Item1[0][7]) : marcaText;
                tipoText = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][8])) ? Convert.ToString(CarData.Item1[0][8]) : tipoText;
                num_serie = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][9])) ? Convert.ToString(CarData.Item1[0][9]) : num_serie;
                poliza_seguro = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][10])) ? Convert.ToString(CarData.Item1[0][10]) : poliza_seguro;
                vigencia = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][11])) ? Convert.ToString(CarData.Item1[0][11]) : vigencia;
                modeloText = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][12])) ? Convert.ToString(CarData.Item1[0][12]) : modeloText;
                gpstext = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][13])) ? Convert.ToString(CarData.Item1[0][13]) : gpstext;
                propietariogpsText = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][14])) ? Convert.ToString(CarData.Item1[0][14]) : propietariogpsText;
                fechaMecanica = !string.IsNullOrEmpty(Convert.ToString(CarData.Item1[0][15])) ? Convert.ToString(CarData.Item1[0][15]) : fechaMecanica;
                // Para variables de tipo int
                idEstatus = int.TryParse(Convert.ToString(CarData.Item1[0][16]), out int tempIdEstatus) ? tempIdEstatus : idEstatus;
                idMarca = int.TryParse(Convert.ToString(CarData.Item1[0][17]), out int tempIdMarca) ? tempIdMarca : idMarca;
                idTipo = int.TryParse(Convert.ToString(CarData.Item1[0][18]), out int tempIdTipo) ? tempIdTipo : idTipo;
                idModelo = int.TryParse(Convert.ToString(CarData.Item1[0][19]), out int tempIdModelo) ? tempIdModelo : idModelo;
                idGps = int.TryParse(Convert.ToString(CarData.Item1[0][20]), out int tempIdGps) ? tempIdGps : idGps;
                idPropietarioGps = int.TryParse(Convert.ToString(CarData.Item1[0][21]), out int tempIdPropietarioGps) ? tempIdPropietarioGps : idPropietarioGps;
                idOperador = int.TryParse(Convert.ToString(CarData.Item1[0][22]), out int tempIdoperador) ? tempIdoperador : idOperador;
                if (!string.IsNullOrEmpty(fechaCreado))
                {
                    DateTime fechaHora = DateTime.Parse(fechaCreado);
                    fechaCreado = fechaHora.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                if (!string.IsNullOrEmpty(fechaActualizado))
                {
                    DateTime fechaHora = DateTime.Parse(fechaActualizado);
                    fechaActualizado = fechaHora.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                if (!string.IsNullOrEmpty(fechaMecanica))
                {
                    DateTime fechaHora = DateTime.Parse(fechaMecanica);
                    fechaMecanica = fechaHora.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                if (!string.IsNullOrEmpty(vigencia))
                {
                    DateTime fechaHora = DateTime.Parse(vigencia);
                    vigencia = fechaHora.ToString("yyyy-MM-dd");
                }
            }

          

                query = "SELECT * FROM TIPO WHERE id_tipo <>" + idTipo;
                Tuple<List<object[]>, int> tipo = UpdateVehiculo.Consulta(ref mens, query);
                if (tipo.Item2 >= 1)
                {
                    TotalTipo = tipo.Item2;
                    idTi = new int[tipo.Item2];
                    ValTi = new string[tipo.Item2];
                    for (int a = 0; a < tipo.Item2; a++)
                    {
                        validarTipo[a] = Convert.ToString(tipo.Item1[a][1]);
                        ideTipo[a] = Convert.ToInt32(tipo.Item1[a][0]);
                    }
                }
                query = "SELECT * FROM MARCA WHERE id_marca <>" + idMarca;
                Tuple<List<object[]>, int> marca = UpdateVehiculo.Consulta(ref mens, query);
                if (marca.Item2 >= 1)
                {
                    TotalMarca = marca.Item2;
                    idM = new int[marca.Item2];
                    ValM = new string[marca.Item2];
                    for (int b = 0; b < marca.Item2; b++)
                    {
                        validarMarca[b] = Convert.ToString(marca.Item1[b][1]);
                        ideMarca[b] = Convert.ToInt32(marca.Item1[b][0]);
                    }
                }
                query = "SELECT * FROM MODELO WHERE id_modelo <>" + idModelo;
                Tuple<List<object[]>, int> modelo = UpdateVehiculo.Consulta(ref mens, query);
                if (modelo.Item2 >= 1)
                {
                    TotalModelo = modelo.Item2;
                    idMod = new int[modelo.Item2];
                    ValMod = new string[modelo.Item2];
                    for (int c = 0; c < modelo.Item2; c++)
                    {
                        validarModelo[c] = Convert.ToString(modelo.Item1[c][1]);
                        ideModelo[c] = Convert.ToInt32(modelo.Item1[c][0]);
                    }
                }
                query = "SELECT * FROM GPS WHERE id_gps <>" + idGps;
                Tuple<List<object[]>, int> gps = UpdateVehiculo.Consulta(ref mens, query);
                if (gps.Item2 >= 1)
                {
                    TotalGps = gps.Item2;
                    idG = new int[gps.Item2];
                    ValG = new string[gps.Item2];
                    for (int d = 0; d < gps.Item2; d++)
                    {
                        validarGPS[d] = Convert.ToString(gps.Item1[d][1]);
                        ideGps[d] = Convert.ToInt32(gps.Item1[d][2]);
                    }
                }
                query = "SELECT * FROM PROPIETARIO_VEHICULO WHERE id_propietarioEmpresa <>" + idPropietarioGps;
                Tuple<List<object[]>, int> propietario = UpdateVehiculo.Consulta(ref mens, query);
                if (propietario.Item2 >= 1)
                {
                    TotalPropietarioGps = propietario.Item2;
                    idPG = new int[propietario.Item2];
                    ValPG = new string[propietario.Item2];
                    for (int f = 0; f < propietario.Item2; f++)
                    {
                        validarProvGP[f] = Convert.ToString(propietario.Item1[f][2]);
                        ideProvedorGps[f] = Convert.ToInt32(propietario.Item1[f][1]);
                    }
                }

                query = "SELECT * FROM " + UpdateVehiculoMysql.LinkedServer + "... estatus WHERE idEstatus <>" + idEstatus + " AND  idEstatus between 94577 AND 94583  ORDER BY Nombre ";
                Tuple<List<object[]>, int> estatus = UpdateVehiculo.Consulta(ref mens, query);
                if (estatus.Item2 >= 1)
                {
                    TotalEstatus = estatus.Item2;
                    idEs = new int[estatus.Item2];
                    ValEs = new string[estatus.Item2];
                    for (int g = 0; g < estatus.Item2; g++)
                    {
                        validarEstatus[g] = Convert.ToString(estatus.Item1[g][1]);
                        ideEstatus[g] = Convert.ToInt32(estatus.Item1[g][0]);
                    }
                }
                query = "SELECT id_cliente,nombre_completo FROM " + UpdateVehiculoMysql.LinkedServer + " ... cliente c WHERE(id_cliente NOT IN (SELECT id_cliente  FROM VEHICULO )  AND (((idEstatus=31448 OR idEstatus=94573 ) OR (idEstatus=19231 OR idEstatus=25542 )) AND id_rol= 4254)) ORDER BY c.nombre_completo";
                Tuple<List<object[]>, int> usuario = UpdateVehiculo.Consulta(ref mens, query);
                if (usuario.Item2 >= 1)
                {
                    TotalOperador = usuario.Item2;
                    idOper = new int[usuario.Item2];
                    ValOper = new string[usuario.Item2];
                    for (int h = 0; h < usuario.Item2; h++)
                    {
                        validarOperador[h] = Convert.ToString(usuario.Item1[h][1]);
                        ideOperador[h] = Convert.ToInt32(usuario.Item1[h][0]);
                    }
                }
            if (Request.Form["Ccar"] != null)
            {
                int ideeEstatus = Convert.ToInt32(Functions.RequestPost(Request.Form["CidEstatus"]));
                int ideeEMarca = Convert.ToInt32(Functions.RequestPost(Request.Form["CidMarca"]));
                int ideeTipo = Convert.ToInt32(Functions.RequestPost(Request.Form["CidTipo"]));
                int ideeModelo = Convert.ToInt32(Functions.RequestPost(Request.Form["CidModelo"]));
                int ideeGps = Convert.ToInt32(Functions.RequestPost(Request.Form["CidGps"]));
                int ideeProvGps = Convert.ToInt32(Functions.RequestPost(Request.Form["CidProv"]));
                int operador = Convert.ToInt32(Functions.RequestPost(Request.Form["Coperador"]));
                string vehiculo = Convert.ToString(Functions.RequestPost(Request.Form["Ccar"]));
                vehiculo = vehiculo.ToUpper();
                string placas = Convert.ToString(Functions.RequestPost(Request.Form["Cplacas"]));
                string serie = Convert.ToString(Functions.RequestPost(Request.Form["Cserie"]));
                string poliza = Convert.ToString(Functions.RequestPost(Request.Form["Cpoliza"]));
                string fechaCreacion = Convert.ToString(Functions.RequestPost(Request.Form["Cfecha"]));
                string fechaMecanica = Convert.ToString(Functions.RequestPost(Request.Form["Cmecanica"])); 

                if(fechaMecanica!= null || fechaMecanica!= "")
                {
                    DateTime fechaMecanicaDateTime = DateTime.Parse(fechaMecanica);
                    fechaMecanica = fechaMecanicaDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                }

                string vigencia = Convert.ToString(Functions.RequestPost(Request.Form["Cvigencia"]));
                if (vigencia != null || vigencia != "")
                {
                    DateTime vigenciaDateTime = !string.IsNullOrEmpty(vigencia) ? DateTime.Parse(vigencia) : DateTime.MinValue;
                    vigencia = vigenciaDateTime.ToString("yyyy/MM/dd");
                }


                query = "SELECT * FROM VEHICULO WHERE nombre_vehiculo LIKE '%" + vehiculo + "%' AND id_vehiculo <>" + idEdit;
                Tuple<List<object[]>, int> existente = UpdateVehiculo.Consulta(ref mens, query);
                if (existente.Item2 >= 1)
                {
                    alert = @"<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10; '> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                 <h4 class='text-center'>Ocurrió un error</h4>
                                   <p class='text-center'>
                                    El vehículo NO fue actualizado debido a que existe un vehículo con el mismo nombre o alguno similar, verifica tus datos.
                                    </p>
                                  </div>";
                }
                else
                {
                    try
                    {                   //  id_vehiculo,nombre_vehiculo,actualizado, fecha_creacion, operador asignado, placas, estatus, marca, tipo, num_Serie, poliza_seguro, vigencia_poliza, modelo, gps, provedorgps,fecharevision mecanica
                        bool actualizar = UpdateVehiculo.Actualizar("", "VEHICULO", $"nombre_vehiculo='{vehiculo}',id_cliente={operador},placas='{placas}',id_status={ideeEstatus},id_marca={ideeEMarca},id_tipo={ideeTipo},fecha_revisionMecanica='{fechaMecanica}',Numero_serie='{serie}',poliza_seguro='{poliza}',vigencia_poliza='{vigencia}',id_modelo={ideeModelo},id_propietario={ideeProvGps},id_gps={ideeGps}", "id_vehiculo=" + idEdit);
                        if (actualizar == true)
                        {
                            alerta = @"<div class='alert alert-info alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10; '> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                 <h4 class='text-center'>Vehículo actualizado</h4>
                                   <p class='text-center'>
                                    El vehículo fue actualizado con éxito
                                    </p>
                                  </div>";
                        }
                    }
                    catch (Exception c)
                    {
                        alert = @" < div class='alert alert-danger alert-dismissible fade in col -sm-3 animated bounceInDown' role='alert' style='position: fixed; top: 70px; right: 10px; z - index:10;'> 
                                <button type = 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>
                                    <h4 class='text-center'>OCURRIÓ UN ERROR</h4>
                                    <p class='text-center'>
                                        No hemos podido actualizar el usuario
                                    </p>
                                </div>";

                    }
                }
            }
        }
    }
}