using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Web.Configuration;
using System.Configuration;

namespace Inventario.Inventario.lib
{
    public class Functions
    {
        public static string LimpiarCadena(string valor)
        {
            if (valor == null)
            {
                return string.Empty; // O cualquier otro valor predeterminado que quieras usar
            }

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
                .Replace("$", "")
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
        public static bool EnviarCorreo(string emailEnvia, string recuperar, string destino, string departamento, string subject, string body)
        {
            try
            {
                // Configuración del cliente SMTP
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(emailEnvia, recuperar);
                smtpClient.EnableSsl = true;

                // Crear el mensaje de correo
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress(emailEnvia);
                mensaje.To.Add(destino);
                mensaje.Subject = subject;

                // Cuerpo del correo en formato HTML
                mensaje.IsBodyHtml = true;
                mensaje.Body = body;
                // Configuración adicional
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
                mensaje.BodyEncoding = System.Text.Encoding.UTF8;

                // Enviar el correo
                smtpClient.Send(mensaje);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                return false;
            }
        }

        public static HttpCookie CrearCookie(string valor, string nombre, HttpResponse response)
        {
            HttpCookie cookie = new HttpCookie(nombre);

            cookie.Value = HttpUtility.UrlEncode(valor, Encoding.UTF8);
            cookie.Expires = DateTime.Now.AddDays(1);
            response.Cookies.Add(cookie);
            return cookie;
        }

        public static HttpCookie ObtenerCookie(string nombre)
        { 
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nombre];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                // Decodificar el valor de la cookie utilizando HTML
                cookie.Value = Decodificar(cookie.Value);
                return cookie;
            }
            return null; // La cookie no existe o no tiene valor
        }

        public static string Decodificar(string valor)
        {
            // Decodificar utilizando UTF-8
            string resultado = HttpUtility.UrlDecode(valor, Encoding.UTF8);
            return resultado;
        }

        public static void CrearPdf(string html, string fileName)
        {
            using (Document doc = new Document())
            {
                  HttpResponse response = HttpContext.Current.Response;
                    response.ContentType = "application/pdf";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + fileName  + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
                // Escribir el contenido del PDF en el Response
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();
                    StringReader stringReader = new StringReader(html);
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, stringReader);
                    doc.Close();
                    response.BinaryWrite(ms.ToArray());
                }
                response.End();
            }
        }
    }
}