using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
namespace Inventario.Inventario.lib
{
    public class Functions
    {
        public static string LimpiarCadena(string valor)
        {
            valor = valor.Replace("SELECT", "")
                .Replace("''", "")
                .Replace("COPY", "")
                .Replace("*", "")
                .Replace("FROM", "")
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
            string datos = LimpiarCadena(val);
            return datos;
        }

        public static string RequestPost(string val)
        {
            string datos = LimpiarCadena(val);
            return datos;
        }
        public string CalculateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static string GenerarCadenaAleatoria(string caracteresPermitidos, int longitud)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder(longitud);

            for (int i = 0; i < longitud; i++)
            {
                int index = random.Next(caracteresPermitidos.Length);
                sb.Append(caracteresPermitidos[index]);
            }

            return sb.ToString();
        }
    }
}