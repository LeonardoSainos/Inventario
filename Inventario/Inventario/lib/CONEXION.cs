using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
namespace Inventario.Scripts
{
    public class CONEXION
    {
        public string ServidorSQL { get; set; }
        public string BD { set; get; }
        string nombre;
        public string Propiedad_nombre
        {
            set { nombre = value; }
            get { return nombre; }           
        }
        public System.Data.DataRowCollection Rows { get; }
        public SqlConnection Conectar(ref String cad)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand querys = new SqlCommand();
            conexion.ConnectionString = "data source=" + ServidorSQL + "; Initial Catalog=" + BD + "; Integrated security=true";
            try
            {
                conexion.Open();
                cad = "Conexion abierta";
            }
            catch(Exception exep)
            {
                conexion = null;
                cad = "ERROR :" + exep.Message;
            }
            return conexion;
        }
        public Tuple<List<object[]>, int> Consulta(ref string mensaje, string sentenciaSQL)
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
            SqlConnection conexion= Conectar(ref texto);
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
        public void Insertar(String tabla, string campos, string valores, ref String mensaje)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
             try
             { using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandText = "INSERT INTO " + tabla + "( " + campos + ") VALUES " + "(" + valores + ");";
                    query.ExecuteNonQuery();
                    mensaje = "Datos guardados";
                }
                conexion.Close();
             }
            catch (Exception t)
             {
                conexion = null;
                mensaje = "ERROR: " + t.Message;
            }
        }


        public void Actualizar(String tabla, string campos , string condicion)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
            try
            {
                using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandText = "UPDATE " + tabla + " SET " + campos + " WHERE " + condicion;
                    query.ExecuteNonQuery();
                    texto = "Registros actualizados";
                }
                conexion.Close();

            }
            catch (Exception t)
            {
                conexion = null;
                texto = "ERROR : " + t.Message;
            }
        }


        public void Eliminar(string tabla, string condicion)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
            try
            {
                using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandText = "DELETE FROM " + tabla + " WHERE " + condicion;
                    query.ExecuteNonQuery();
                    texto = "Registro eliminado";
                }
                conexion.Close();
            }
            catch(Exception t)
            {
                conexion = null;
                texto = "ERROR : " + t.Message;
            }
        }

        public void ProcedimientoAlmacenado (string nombre,  string [] parametros, String [] valores)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
            try
            {
                using (conexion)
                {
                    query.Connection = conexion;
                    query.CommandText = nombre;
                    query.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < parametros.Length; i++)
                    {
                         query.Parameters.AddWithValue("@" + parametros[i]  , valores[i]);
                    }
                    query.ExecuteScalar();
                }
                conexion.Close();
                texto = "Procedimiento ejecutado de forma correcta";
            }
            catch (Exception t)
            {
                conexion = null;
                texto = "ERROR : " + t.Message;
            }
        }

        public static string LimpiarCadena(string valor)
        {
            valor = valor.Replace("SELECT", "")
                .Replace("COPY", "")
                .Replace("UPDATE", "")
                .Replace("DELETE", "")
                .Replace("DROP", "")
                .Replace("DUMP", "")
                .Replace(" OR ", "")
                .Replace("%", "")
                .Replace("LIKE", "")
                .Replace("--", "")
                .Replace("^", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("\\", "")
                .Replace("!", "")
                .Replace("¡", "")
                .Replace("=", "")
                .Replace("&", "");
            return valor;
        }

        public static string RequestGet(string val)
        {
            string data = HttpContext.Current.Request.QueryString[val];
            string datos = LimpiarCadena(data);
            return datos;
        }

        public static string RequestPost(string val)
        {
            string data = HttpContext.Current.Request.Form[val];
            string datos = LimpiarCadena(data);
            return datos;
        }

    }
}