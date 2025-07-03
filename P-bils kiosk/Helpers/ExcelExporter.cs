using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Vml.Office;
using P_bils_kiosk.Models;

namespace P_bils_kiosk.Helpers
{
    class ExcelExporter
    {
        string dato = entry.Tidspunkt.ToString("dd-MM-yyyy");
        string filnavn = $"{dato}.xlsx";
        bool filEksisterer = File.Exists(filnavn);
    }
}
