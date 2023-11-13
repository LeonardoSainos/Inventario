using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
 


namespace Inventario.Scripts
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        CONEXION obj = new CONEXION();
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string texto = ""; 
            obj.BD = "Inventario";
            obj.ServidorSQL = @"ALMPST2";
            obj.Conectar(ref texto);
            Label1.Text = texto;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            obj.BD = "Inventario";
            obj.ServidorSQL = @"ALMPST2";
            string mens = "";
            string setencia = "SELECT p.id_provedor, p.nombre as prove,e.nombre as estatus FROM proveedor p INNER JOIN OPENQUERY(mysql_ticket, 'SELECT  * FROM estatus') e ON e.idEstatus = p.id_status WHERE p.id_status = 31448;";
            obj.Mostrar(GridView1, ref mens, setencia);
        }
    }
}