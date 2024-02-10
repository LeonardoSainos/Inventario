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
        MySql obj = new MySql();
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string texto = ""; 
           
            obj.Conectar(ref texto);
            Label1.Text = texto;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            
            string mens = "";
            string setencia = "SELECT p.id_provedor, p.nombre as prove,e.nombre as estatus FROM proveedor p INNER JOIN OPENQUERY(" + obj.LinkedServer + ", 'SELECT  * FROM estatus') e ON e.idEstatus = p.id_status WHERE p.id_status = 31448;";
            obj.Mostrar(GridView1, ref mens, setencia);
        }
    }
}