using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
namespace Inventario.Scripts
{
    public class CONEXION : MySql
    { 
        public CONEXION() : base()
        {
        }
        public override bool Insertar(String tabla, string server, string campos, string valores, ref String mensaje)
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
                return true;
             }
            catch (Exception t)
             {
                conexion = null;
                mensaje = "ERROR: " + t.Message;
                return false;
            }
        }


        public override bool Actualizar(string server,String tabla, string campos , string condicion)
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
                return true;
            }
            catch (Exception t)
            {
                conexion = null;
                texto = "ERROR : " + t.Message;
                return false;
            }
        }


        public override void Eliminar(string server, string tabla, string condicion)
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

        public override bool ProcedimientoAlmacenado (string nombre,  string server, string parametros)
        {
            string texto = "";
            SqlConnection conexion = Conectar(ref texto);
            SqlCommand query = new SqlCommand();
            try
            {
                using (conexion)
                { // PENDIENTE DE REVISAR FUNCIONAMIENTO EN CUANTO AL FOREACH, FECHA DE CREACION 1-03-2024
                    query.Connection = conexion;
                    query.CommandType = CommandType.StoredProcedure; // Cambia el tipo de comando a StoredProcedure
                    query.CommandText = nombre; // Asigna el nombre del procedimiento almacenado
                                                // Agrega los parámetros a la consulta
                    foreach (var parametro in parametros.Split(','))
                    {
                        query.Parameters.AddWithValue("@Parametro", parametro.Trim());
                    }
                    query.ExecuteNonQuery();


                }
                conexion.Close();
                texto = "Procedimiento ejecutado de forma correcta";
                return true;
            }
            catch (Exception t)
            {
                conexion = null;
                texto = "ERROR : " + t.Message;
                return false;
            }
        }
    }
}
