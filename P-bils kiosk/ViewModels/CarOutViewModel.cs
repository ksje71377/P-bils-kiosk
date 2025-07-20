using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DocumentFormat.OpenXml.Wordprocessing;
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
        public ObservableCollection<string> UdDestinationer { get; set; }
        public ObservableCollection<string> UdBiler { get; set; }

        public ICommand BekræftCommand { get; }

        public CarOutViewModel(Window window)
        {
            _window = window;
            BekræftCommand = new RelayCommand(GemOgLuk);
            UdBiler = new ObservableCollection<string>(ComboBoxLoader.LoadCars());
            UdDestinationer = new ObservableCollection<string>(ComboBoxLoader.LoadDestinations());
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

            if (string.IsNullOrWhiteSpace(entry.valgtBil))
            {
                MessageBox.Show("Bil kan ikke være tomt.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrWhiteSpace(entry.destination))
            {
                MessageBox.Show("Destination kan ikke være tomt.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrWhiteSpace(entry.chaufførNummer))
            {
                MessageBox.Show("Chaufførnummer kan ikke være tomt.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(entry.chaufførNummer, @"^\d{4}$"))
            {
                MessageBox.Show("Chaufførnummer skal bestå af præcis 4 cifre.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                ExcelExporter.Export(entry);
                MessageBox.Show("Tak fordi du registrerede dine oplysninger. Rigtig god tur, kør forsigtigt.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                _window.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved gemning: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}