﻿using Inventario.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
namespace Inventario.Inventario.inc
{
    public partial class navbar : System.Web.UI.UserControl
    {
        MySql obj = new MySql();
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Iniciar(object sender, EventArgs e)
        {
            string mens = "";
            obj.BD = "Inventario";
            obj.ServidorSQL = @"ALCOMPC02";
            string clave = Convert.ToString(obj.CalculateMD5(txtPassword.Text));
            string consulta = "";
            string texto = rblLogin.SelectedItem.Text; 
            string valor = rblLogin.SelectedItem.Value;
            int indice = rblLogin.SelectedIndex;

            if ((clave != null || txtUsuario.Text != null) && valor != null)
            {
                switch (valor)
                {
                    case "1":
                        {
                            consulta = "SELECT * FROM mysql_ticket... cliente WHERE((email_cliente = '" + txtUsuario.Text + "' OR nombre_usuario = '" + txtUsuario.Text + "') AND clave = '" + clave + "')  AND (id_rol = 2736   AND idEstatus <> 25542 )";
                            Tuple<List<object[]>, int> resultado = obj.Consulta(ref mens, consulta);
                            List<object[]> registros = resultado.Item1;
                            int contador = resultado.Item2;
                            int row1 = 0, row6 = 0, row7 = 0, row10 = 0, row11 = 0;
                            string row2 = "", row3 = "", row4 = "", row5 = "", row8 = "";
                            DateTime row9 = DateTime.Now;
                            foreach (object[] registro in registros)
                            {
                                // Suponiendo que las columnas son de los tipos conocidos (por ejemplo, string, int)
                                row1 = (int)registro[0];
                                row2 = (string)registro[1];
                                row3 = (string)registro[2];
                                row4 = (string)registro[3];
                                row5 = (string)registro[4];
                                row6 = (int)registro[5];
                                row7 = (int)registro[6];
                                row8 = (string)registro[7];
                                row9 = (DateTime)registro[8];
                                row10 = (int)registro[9];
                                row11 = (int)registro[10];
                                // Utiliza los valores de las columnas como desees
                                Console.WriteLine($"Valor de columna1: {row1}");
                            }
                              if (contador > 0)
                              {
                                Session["id"] = row1;
                                Session["nombre_completo"] = row2;
                                Session["nombre"] = row3;
                                Session["email"] = row4;
                                Session["clave"] = row5;
                                Session["departamento"] = row6;
                                Session["rol"] = row7;
                                Session["telefono"] = row8;
                                Session["fecha"] = row9;
                                Session["estatus"] = row10;
                                Session["anydesk"] = row11;
                                // Paral impiar radiobutton se usa rblLogin.SelectedIndex = -1;
                
                                Response.Redirect("index.aspx?view=index");
                            }
                            else
                            {
                                Response.Write("<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type= 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><h4 class='text-center'>OCURRIÓ UN ERROR</h4><p class='text-center'> Nombre de usuario o contraseña incorrectos</p> </div>");
                            }
                                break;
                        }
                    case "2":
                        {
                            consulta = "SELECT * FROM mysql_ticket... cliente WHERE((email_cliente = '" + txtUsuario.Text + "' OR nombre_usuario = '" + txtUsuario.Text + "') AND clave = '" + clave + "')  AND (id_rol = 7845   AND idEstatus <> 25542 )";
                            Tuple<List<object[]>, int> resultado = obj.Consulta(ref mens, consulta);

                            List<object[]> registros = resultado.Item1;
                            int contador = resultado.Item2;

                            int row1 = 0, row6 = 0, row7 = 0, row10 = 0, row11 = 0;
                            string row2 = "", row3 = "", row4 = "", row5 = "", row8 = "";
                            DateTime row9 = DateTime.Now;
                            foreach (object[] registro in registros)
                            {
                                // Suponiendo que las columnas son de los tipos conocidos (por ejemplo, string, int)
                                row1 = (int)registro[0];
                                row2 = (string)registro[1];
                                row3 = (string)registro[2];
                                row4 = (string)registro[3];
                                row5 = (string)registro[4];
                                row6 = (int)registro[5];
                                row7 = (int)registro[6];
                                row8 = (string)registro[7];
                                row9 = (DateTime)registro[8];
                                row10 = (int)registro[9];
                                row11 = (int)registro[10];
                                // Utiliza los valores de las columnas como desees
                                Console.WriteLine($"Valor de columna1: {row1}");
                            }
                            Console.WriteLine($"Número de registros: {contador}");
                            if (contador > 0)
                            {
                                Session["id"] = row1;
                                Session["nombre_completo"] = row2;
                                Session["nombre"] = row3;
                                Session["email"] = row4;
                                Session["clave"] = row5;
                                Session["departamento"] = row6;
                                Session["rol"] = row7;
                                Session["telefono"] = row8;
                                Session["fecha"] = row9;
                                Session["estatus"] = row10;
                                Session["anydesk"] = row11;
                                // Paral impiar radiobutton se usa rblLogin.SelectedIndex = -1;
                                Response.Write(@"<script> alert('Usuario valido');</script>");
                                Response.Redirect("index.aspx?view=index");
                            }
                            else
                            {
                                Response.Write("<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type= 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><h4 class='text-center'>OCURRIÓ UN ERROR</h4><p class='text-center'> Nombre de usuario o contraseña incorrectos</p> </div>");
                            }
                            break;
                        }
                    case "3":
                        {
                            consulta = "SELECT * FROM mysql_ticket... cliente WHERE((email_cliente = '" + txtUsuario.Text + "' OR nombre_usuario = '" + txtUsuario.Text + "') AND clave = '" + clave + "')  AND (id_rol = 4046   AND idEstatus <> 25542 )";
                            Tuple<List<object[]>, int> resultado = obj.Consulta(ref mens, consulta);
                            List<object[]> registros = resultado.Item1;
                            int contador = resultado.Item2;
                            int row1 = 0, row6 = 0, row7 = 0, row10 = 0, row11 = 0;
                            string row2 = "", row3 = "", row4 = "", row5 = "", row8 = "";
                            DateTime row9 = DateTime.Now;
                            foreach (object[] registro in registros)
                            {
                                // Suponiendo que las columnas son de los tipos conocidos (por ejemplo, string, int)
                                row1 = (int)registro[0];
                                row2 = (string)registro[1];
                                row3 = (string)registro[2];
                                row4 = (string)registro[3];
                                row5 = (string)registro[4];
                                row6 = (int)registro[5];
                                row7 = (int)registro[6];
                                row8 = (string)registro[7];
                                row9 = (DateTime)registro[8];
                                row10 = (int)registro[9];
                                row11 = (int)registro[10];
                                // Utiliza los valores de las columnas como desees
                            }
                            if (contador > 0)
                            {
                                Session["id"] = row1;
                                Session["nombre_completo"] = row2;
                                Session["nombre"] = row3;
                                Session["email"] = row4;
                                Session["clave"] = row5;
                                Session["departamento"] = row6;
                                Session["rol"] = row7;
                                Session["telefono"] = row8;
                                Session["fecha"] = row9;
                                Session["estatus"] = row10;
                                Session["anydesk"] = row11;
                                // Paral impiar radiobutton se usa rblLogin.SelectedIndex = -1;
                                Response.Write(@"<script> alert('Usuario valido');</script>");
                                Response.Redirect("index.aspx?view=index");
                            }
                            else
                            {
                                Response.Write("<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'> <button type= 'button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><h4 class='text-center'>OCURRIÓ UN ERROR</h4><p class='text-center'> Nombre de usuario o contraseña incorrectos</p> </div>");
                            }
                            break;
                        }
                }
            }
            else
            {
                Response.Write(@"<div class='alert alert-danger alert-dismissible fade in col-sm-3 animated bounceInDown' role='alert' style='position:fixed; top:70px; right:10px; z-index:10;'>" +
                    "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
                    "<h4 class='text-center'>OCURRIÓ UN ERROR</h4>" +
                    "<p class='text-center'> No puedes dejar ningún campo vacío </p></div>");
            }
        }
    }
}