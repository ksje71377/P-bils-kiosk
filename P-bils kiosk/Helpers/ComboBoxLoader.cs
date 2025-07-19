// ✅ ComboBoxLoader.cs (refaktoreret og samlet)
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace P_bils_kiosk.Helpers
{
    public static class ComboBoxLoader
    {
        private static string GetFilePath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Databases", fileName);
        }

        public static List<string> LoadCars()
        {
            return LoadListFromJson("CarsComboBox.json");
        }

        public static List<string> LoadDestinations()
        {
            return LoadListFromJson("DestinationComboBox.json");
        }

        private static List<string> LoadListFromJson(string fileName)
        {
            try
            {
                string path = GetFilePath(fileName);
                if (!File.Exists(path))
                {
                    Console.WriteLine($"❌ Filen '{fileName}' blev ikke fundet i mappen 'Databases'.");
                    return new List<string>();
                }

                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<string>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved indlæsning af {fileName}: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
