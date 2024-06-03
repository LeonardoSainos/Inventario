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
        private string query = "", mens = "", alert = "";
        private string[] ValEs, ValR, ValDe;
        private int[] idEs, idR, idDe;
        private int TotalEst = 0, TotalRol = 0, TotalDepa = 0;
        private string fechaText = "", nombreText = "", correoText = "", usuarioText = "", estadoText = "", depaText = "", rolText = "", telefonoText = "", anydeskText = "";
        private int idRolText = 0, idDepaText = 0, idEstatusText = 0, id_edit = 0;
        public int idEdit { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string mensaje = "";

            if (Request.QueryString["idV"] != null)
            {
                int user = Session["id"] != null ? Convert.ToInt32(Session["id"]) :
                (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
                idEdit = Convert.ToInt32(Functions.RequestGet(Request.QueryString["idV"])); 


                Tuple<List<object[]>, int> CarData = UpdateVehiculo.Consulta(ref mensaje, "SELECT v.id_vehiculo,v.nombre_vehiculo, v.fecha_actualizacion as actualizado ,v.fecha_creacion, c.nombre_completo AS asignado, v.placas, e.nombre as estatus, m.nombre as marca, t.nombre as tipo, v.Numero_serie,v.poliza_seguro, v.vigencia_poliza, mo.nombre as modelo, gp.nombre as GPS, pv.nombre as propietarioGps, v.fecha_revisionMecanica FROM VEHICULO v INNER JOIN OPENQUERY(mysql_ticket, 'SELECT * FROM estatus') e ON e.idEstatus = v.id_status INNER JOIN MARCA m ON m.id_marca = v.id_marca INNER JOIN TIPO t ON t.id_tipo = v.id_tipo INNER JOIN MODELO mo  ON mo.id_modelo = v.id_modelo INNER JOIN OPENQUERY(mysql_ticket, 'SELECT * FROM cliente') c ON  v.id_cliente = c.id_cliente  INNER JOIN GPS gp ON gp.id_gps = v.id_gps  INNER JOIN PROPIETARIO_VEHICULO pv ON pv.id_propietarioEmpresa = v.id_propietario WHERE v.id_vehiculo =" + id_edit);
                int contador = CarData.Item2;


            }
        }
    }
}