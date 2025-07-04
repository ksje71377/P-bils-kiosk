using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Vml.Office;
using P_bils_kiosk.Models;

namespace P_bils_kiosk.Helpers
{
    class ExcelExporter
    {
        //

        public static void Export(CarLogEntry entry)
        {
            string dato = entry.Tidspunkt.ToString("dd-MM-yyyy");
            string filnavn = $"{dato}.xlsx";
            bool filEksisterer = File.Exists(filnavn);

            using var workbook = filEksisterer ? new XLWorkbook(filnavn) : new XLWorkbook();
            var sheet = filEksisterer ? workbook.Worksheets.First() : workbook.Worksheets.Add("Log");
            int næsteRække = sheet.LastRowUsed()?.RowNumber() + 1 ?? 1;

            if (næsteRække == 1)
            {
                sheet.Cell(næsteRække, 1).Value = "Tidspunkt";
                sheet.Cell(næsteRække, 2).Value = "Chaufførnummer";
                sheet.Cell(næsteRække, 3).Value = "Bil";
                sheet.Cell(næsteRække, 4).Value = "Destination";
                sheet.Cell(næsteRække, 5).Value = "Status";
                næsteRække++;
            }

            sheet.Cell(næsteRække, 1).Value = entry.Tidspunkt.ToString("dd-MM-yyyy HH:mm");
            sheet.Cell(næsteRække, 2).Value = entry.chaufførNummer;
            sheet.Cell(næsteRække, 3).Value = entry.valgtBil;
            sheet.Cell(næsteRække, 4).Value = entry.destination;
            sheet.Cell(næsteRække, 5).Value = entry.isOutbound ? "Udkørsel" : "Indkørsel";

            workbook.SaveAs(filnavn);
        }
    }

}
