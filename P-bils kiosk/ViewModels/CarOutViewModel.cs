using System;
using System.Windows;
using System.Windows.Input;
using P_bils_kiosk.Helpers;
using P_bils_kiosk.Models;

namespace P_bils_kiosk
{
    public class CarOutViewModel
    {
        private readonly Window _window;

        public string ChaufførNummer { get; set; }
        public string ValgtBil { get; set; }
        public string Destination { get; set; }

        public ICommand BekræftCommand { get; }

        public CarOutViewModel(Window window)
        {
            _window = window;
            BekræftCommand = new RelayCommand(GemOgLuk);
        }

        private void GemOgLuk()
        {
            var entry = new CarLogEntry
            {
                Tidspunkt = DateTime.Now,
                chaufførNummer = this.ChaufførNummer,
                valgtBil = this.ValgtBil,
                destination = this.Destination,
                isOutbound = true
            };

            try
            {
                ExcelExporter.Export(entry);
                MessageBox.Show("Udkørsel registreret og gemt i Excel.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                _window.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved gemning: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}