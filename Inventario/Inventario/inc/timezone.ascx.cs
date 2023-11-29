using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
namespace Inventario.Inventario.inc
{
    public partial class timezone : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           /* DateTime now = DateTime.Now;
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"); // Ajusta el ID de la zona horaria según tu ubicación
            DateTime mexicoCityTime = TimeZoneInfo.ConvertTime(now, timeZone);
            CultureInfo culture = new CultureInfo("es-MX");
            string fecha = mexicoCityTime.ToString("dddd d 'de' MMMM 'del' yyyy", culture);
          */ //    FechaLiteral.Text = fecha;

        }
    }
}