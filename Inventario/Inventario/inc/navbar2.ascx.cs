using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventario.Scripts;
using Inventario.Inventario.lib;
namespace Inventario.Inventario.inc
{
    public partial class navbar2 : System.Web.UI.UserControl
    {
        MySql MysqlNavBar2 = new MySql();
        private HttpCookie fullNameCook, rolCook, emailCook, userCook;
        private string query = "", mensaje = "";
        private int userIdCook = 0, totalModulo=0, totalSubmodulo = 0, totalSubsubmodulo=0;
        private int[] id_modulo, id_submodulo,id_subsubmodulo;
        private string[] nombreModulo,nombreSubModulo,nombreSubsubmodulo, rutaModulo, rutaSubModulo,rutaSubsubmodulo, iconModulo,iconSubmodulo,iconSubsubmodulo, spanModulo, spanSubmodulo, spanSubsubmodulo;
        public HttpCookie fullNameCookie  { get { return fullNameCook; } set { fullNameCook = value; } }
        public HttpCookie rolCookie { get { return rolCook; } set { rolCook = value; } }
        public HttpCookie emailCookie  { get { return emailCook; } set { emailCook = value; } }
        public HttpCookie userCookie { get { return userCook; } set { userCook = value; }}
        public int userIdCookie { get { return userIdCook; } set { userIdCook = value; } }
        public string consulta { get { return query; }set { query = value; } }
        public string mens { get { return mensaje; } set { mensaje = value; } }
        public int TotalModulos { get { return totalModulo; } set { totalModulo = value; } }
        public int TotalSubModulos { get { return totalSubmodulo; } set { totalSubmodulo = value; } }
        public int TotalSubSubModulos { get { return totalSubsubmodulo; } set { totalSubsubmodulo = value; } }



        public List<List<int>> idSubModulosList = new List<List<int>>();
        public List<List<string>> NombreSubModulosList = new List<List<string>>();
        public List<List<string>> RutaSubModulosList = new List<List<string>>();
        public List<List<string>> IconSubModulosList = new List<List<string>>();
        public List<List<string>> SpanSubModulosList = new List<List<string>>();

        public List<List<List<int>>> idSubSubModulosList = new List<List<List<int>>>();
        public List<List<List<string>>> NombreSubSubModulosList = new List<List<List<string>>>();
        public List<List<List<string>>> RutaSubSubModulosList = new List<List<List<string>>>();
        public List<List<List<string>>> IconSubSubModulosList = new List<List<List<string>>>();
        public List<List<List<string>>> SpanSubSubModulosList = new List<List<List<string>>>();



        //MODULO PROPIEDADES
        public int [] idModulo { get { return id_modulo; } set { id_modulo = value; } }
        public string [] NombreModulo { get { return nombreModulo; } set { nombreModulo = value; } }
        public string[] RutaM { get { return rutaModulo; } set { rutaModulo = value; } }
        public string[] IconM { get { return iconModulo; } set { iconModulo = value; } }
        public string[] SpanM { get { return spanModulo; } set { spanModulo = value; } }
        ///SUBMODULO PROPIEDADES
        public int[] idSubModulo { get { return id_submodulo; } set { id_submodulo = value; } }
        public string[] NombreSubModulo { get { return nombreSubModulo; } set { nombreSubModulo = value; } }
        public string[] RutaSM { get { return  rutaSubModulo; } set { rutaSubModulo = value; } }
        public string[] IconSM { get { return iconSubmodulo; } set { iconSubmodulo = value; } }
        public string[] SpanSM { get { return spanSubmodulo; } set { spanSubmodulo = value; } }
        // SUBSUBMODULO PROPIEDADES
        public int[] idSubSubModulo { get { return id_subsubmodulo; } set { id_subsubmodulo = value; } }
        public string[] NombreSubSubModulo { get { return nombreSubsubmodulo; } set { nombreSubsubmodulo = value; } }
        public string[] RutaSSM { get { return rutaSubsubmodulo; } set { rutaSubsubmodulo = value; } }
        public string[] IconSSM { get { return iconSubsubmodulo; } set { iconSubsubmodulo = value; } }
        public string[] SpanSSM { get { return spanSubsubmodulo; } set { spanSubsubmodulo = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Tuple<List<object[]>, int> Modulos, Submodulos, Subsubmodulos;

            int userIdCookie = Session["id"] != null ? Convert.ToInt32(Session["id"]) : (Request.Cookies["UserId"] != null ? Convert.ToInt32(Request.Cookies["UserId"].Value) : 0);
            rolCookie = Functions.ObtenerCookie("RolId");
            emailCookie = Functions.ObtenerCookie("Email");
            userCookie = Functions.ObtenerCookie("UserName");
            fullNameCookie = Functions.ObtenerCookie("CompletoName");

            // MODULOS
            consulta = "SELECT DISTINCT m.id_modulo, m.nombre as Modulo, m.ruta, m.iconModulo, m.spanModulo FROM " + MysqlNavBar2.LinkedServer + " ...  modulo m INNER JOIN " + MysqlNavBar2.LinkedServer + " ... permisos p ON p.id_modulo = m.id_modulo INNER JOIN " + MysqlNavBar2.LinkedServer + "... cliente c On p.id_usuario = c.id_cliente WHERE (p.id_app = 5470 AND m.id_modulo <>99999) AND p.id_usuario =" + userIdCookie + " ORDER BY m.nombre";
            Modulos = MysqlNavBar2.Consulta(ref mensaje, consulta);

            if (Modulos.Item2 >= 1)
            {
                TotalModulos = Modulos.Item2;
                idModulo = new int[TotalModulos];
                NombreModulo = new string[TotalModulos];
                RutaM = new string[TotalModulos];
                IconM = new string[TotalModulos];
                SpanM = new string[TotalModulos];

                for (int i = 0; i < TotalModulos; i++)
                {
                    idModulo[i] = Convert.ToInt32(Modulos.Item1[i][0]);
                    NombreModulo[i] = Convert.ToString(Modulos.Item1[i][1]);
                    RutaM[i] = Convert.ToString(Modulos.Item1[i][2]);
                    IconM[i] = Convert.ToString(Modulos.Item1[i][3]);
                    SpanM[i] = Convert.ToString(Modulos.Item1[i][4]);

                    // SUBMODULOS
                    consulta = "SELECT DISTINCT s.id_submodulo, s.nombre as Submodulo, s.ruta, s.iconSubmodulo, s.spanSubmodulo FROM " + MysqlNavBar2.LinkedServer + " ... submodulo s INNER JOIN " + MysqlNavBar2.LinkedServer + " ... permisos p ON p.id_submodulo = s.id_submodulo INNER JOIN " + MysqlNavBar2.LinkedServer + " ... cliente c ON p.id_usuario = c.id_cliente WHERE (p.id_modulo = " + idModulo[i] + " AND s.id_submodulo <>99999) AND c.id_cliente =" + userIdCookie + " ORDER BY s.nombre";
                    Submodulos = MysqlNavBar2.Consulta(ref mensaje, consulta);

                    if (Submodulos.Item2 >= 1)
                    {
                        TotalSubModulos = Submodulos.Item2;
                        List<int> idSubModulo = new List<int>();
                        List<string> NombreSubModulo = new List<string>();
                        List<string> RutaSM = new List<string>();
                        List<string> IconSM = new List<string>();
                        List<string> SpanSM = new List<string>();

                        List<List<int>> idSubSubModuloList = new List<List<int>>();
                        List<List<string>> NombreSubSubModuloList = new List<List<string>>();
                        List<List<string>> RutaSubSubModuloList = new List<List<string>>();
                        List<List<string>> IconSubSubModuloList = new List<List<string>>();
                        List<List<string>> SpanSubSubModuloList = new List<List<string>>();

                        for (int j = 0; j < TotalSubModulos; j++)
                        {
                            idSubModulo.Add(Convert.ToInt32(Submodulos.Item1[j][0]));
                            NombreSubModulo.Add(Convert.ToString(Submodulos.Item1[j][1]));
                            RutaSM.Add(Convert.ToString(Submodulos.Item1[j][2]));
                            IconSM.Add(Convert.ToString(Submodulos.Item1[j][3]));
                            SpanSM.Add(Convert.ToString(Submodulos.Item1[j][4]));

                            // SUBSUBMODULOS
                            consulta = "SELECT DISTINCT ss.id_subsubmodulo, ss.nombre as Subsubmodulo, ss.ruta, ss.iconSubsubmodulo, ss.spansubsubmodulo FROM " + MysqlNavBar2.LinkedServer + " ... subsubmodulo ss INNER JOIN " + MysqlNavBar2.LinkedServer + " ... permisos p ON p.id_subsubmodulo = ss.id_subsubmodulo INNER JOIN " + MysqlNavBar2.LinkedServer + " ... cliente c ON p.id_usuario = c.id_cliente WHERE p.id_submodulo = " + idSubModulo[j] + " AND c.id_cliente =" + userIdCookie;
                            Subsubmodulos = MysqlNavBar2.Consulta(ref mensaje, consulta);

                            if (Subsubmodulos.Item2 >= 1)
                            {
                                TotalSubSubModulos = Subsubmodulos.Item2;
                                List<int> idSubSubModulo = new List<int>();
                                List<string> NombreSubSubModulo = new List<string>();
                                List<string> RutaSSM = new List<string>();
                                List<string> IconSSM = new List<string>();
                                List<string> SpanSSM = new List<string>();

                                for (int k = 0; k < TotalSubSubModulos; k++)
                                {
                                    idSubSubModulo.Add(Convert.ToInt32(Subsubmodulos.Item1[k][0]));
                                    NombreSubSubModulo.Add(Convert.ToString(Subsubmodulos.Item1[k][1]));
                                    RutaSSM.Add(Convert.ToString(Subsubmodulos.Item1[k][2]));
                                    IconSSM.Add(Convert.ToString(Subsubmodulos.Item1[k][3]));
                                    SpanSSM.Add(Convert.ToString(Subsubmodulos.Item1[k][4]));
                                }

                                idSubSubModuloList.Add(idSubSubModulo);
                                NombreSubSubModuloList.Add(NombreSubSubModulo);
                                RutaSubSubModuloList.Add(RutaSSM);
                                IconSubSubModuloList.Add(IconSSM);
                                SpanSubSubModuloList.Add(SpanSSM);
                            }
                        }

                        idSubModulosList.Add(idSubModulo);
                        NombreSubModulosList.Add(NombreSubModulo);
                        RutaSubModulosList.Add(RutaSM);
                        IconSubModulosList.Add(IconSM);
                        SpanSubModulosList.Add(SpanSM);

                        idSubSubModulosList.Add(idSubSubModuloList);
                        NombreSubSubModulosList.Add(NombreSubSubModuloList);
                        RutaSubSubModulosList.Add(RutaSubSubModuloList);
                        IconSubSubModulosList.Add(IconSubSubModuloList);
                        SpanSubSubModulosList.Add(SpanSubSubModuloList);
                    }
                }
            }
        }
    }
}
