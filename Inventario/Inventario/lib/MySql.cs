using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Configuration;

namespace Inventario.Scripts
{
     class MySql
    {
      public System.Data.DataRowCollection Rows { get; }

        private string Base = ConfigurationManager.AppSettings["BD"];
        private string dirección = ConfigurationManager.AppSettings["SERVER"];         
        private string server = ConfigurationManager.AppSettings["LINKEDSERVER"];
        public string ServidorSQL {
            get { return dirección; }
        }
        public string BD {
            get { return Base; } 
        }
        public string LinkedServer {
      
         get { return server; }

        }
        public SqlConnection Conectar(ref String cad)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = "data source=" + ServidorSQL + "; Initial Catalog=" + BD + "; Integrated security=true";
            try{
                conexion.Open();
                cad = "Conexion abierta";
            }
            catch (Exception exep)
            {
                conexion = null;
                cad = "ERROR :" + exep.Message;
            }
            return conexion;
        }
        public Tuple <List <object[]>,int> Consulta(ref string mensaje, string sentenciaSQL)
        {
            string texto = "";
            int contador = 0;
            SqlConnection conexion = Conectar(ref texto);
            List<object[]> registros = new List<object[]>();
            try
            {
                using (conexion)
                {
                    SqlCommand query = new SqlCommand(sentenciaSQL, conexion);
                    SqlDataReader reader = query.ExecuteReader();
                    int columnCount = reader.FieldCount;
                    List<object> columnValues = new List<object>();
                    while (reader.Read())
                    {
                        contador++; // Incrementar el contador por cada registro
                        columnValues.Clear();
                        for (int i = 0; i < columnCount; i++)
                        {
                            object value = reader.GetValue(i);
                            columnValues.Add(value);
                        }
                        object[] registro = columnValues.ToArray();
                        registros.Add(registro);
                    }
                    reader.Close();
                }
                conexion.Close();
                mensaje = "Consulta realizada correctamente";
            }
            catch (Exception ex)
            {
                conexion = null;
                texto = "ERROR: " + ex.Message;
            }
            Tuple<List<object[]>, int> resultado = new Tuple<List<object[]>, int>(registros, contador);
            return resultado; // Devolver la lista de registros y el contador como una tupla
        }

        public SqlConnection Mostrar(System.Web.UI.WebControls.GridView grid, ref string mensaje, string sentenciaSQL)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand(); //mysqli_query
            SqlDataAdapter trailer_adaptador = new SqlDataAdapter(); //mysqli_query
            //SqlDataReader contenedor;
            DataSet resultado = new DataSet();
            try
            {
                using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandText = sentenciaSQL;  //  using (SqlCommand comando = new SqlCommand(sentenciaSQL, conexion))
                    trailer_adaptador.SelectCommand = query;
                    trailer_adaptador.Fill(resultado);
                    grid.DataSource = resultado.Tables[0];
                    grid.DataBind();
                    mensaje = "Consulta Realizada";
                }
                conexion.Close();
            }
            catch (Exception t)
            {
                conexion = null;
                mensaje = "ERROR: " + t.Message;
            }
            return conexion;
        }
        public bool Insertar(String tabla,string server, string campos, string valores, ref String mensaje)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
            try
            {
                using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandText = "INSERT INTO " + server + "... " + tabla + "(" + campos + " ) VALUES " + "(" + valores + ");";
                    query.ExecuteNonQuery();
                    mensaje = "Datos guardados";
                }
                conexion.Close();
                return true;
            }
            catch (Exception t)
            {
                conexion = null;
                mensaje = "ERROR: " + t.Message;
                return false;
            }
        }
        public bool Actualizar(string server, String tabla, string campos, string condicion)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
            try
            {
                using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandText = "UPDATE " + server + " ... " + tabla + " SET " + campos + " WHERE " + condicion;
                    query.ExecuteNonQuery();
                    texto = "Registros actualizados";
                }
                conexion.Close();
                return true;
            }
            catch (Exception t)
            {
                conexion = null;
                texto = "ERROR : " + t.Message;
                return false;
            }
        }
        public void Eliminar(string server, string tabla, string condicion)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
            try
            {
                using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandText = "DELETE FROM " + server + "... " + tabla + " WHERE " + condicion;
                    query.ExecuteNonQuery();
                    texto = "Registro eliminado";
                }
                conexion.Close();
            }
            catch (Exception t)
            {
                conexion = null;
                texto = "ERROR : " + t.Message;
            }
        }
        public bool ProcedimientoAlmacenado(string nombre, string server,string parametros)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
            try
            {
                using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandType = CommandType.Text;
                    query.CommandText = $"EXEC('CALL {nombre}( " + parametros + ")') AT " + server;
                    query.ExecuteNonQuery();
                }
                conexion.Close();
                texto = "Procedimiento ejecutado correctamente";
                return true;
            }
            catch (Exception t)
            {
                conexion = null;
                texto = "ERROR: " + t.Message;
                return false;
            }
        }
        
    }
}