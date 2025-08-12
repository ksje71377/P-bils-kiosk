using System;
using System.Collections.ObjectModel;
using System.Media;
using System.Windows;
using System.Windows.Input;
using DocumentFormat.OpenXml.Wordprocessing;
using P_bils_kiosk.Helpers;
using P_bils_kiosk.Models;
using P_bils_kiosk.ViewModels;

namespace P_bils_kiosk
{
    public class CarOutViewModel : IViewModelCommon
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

            var validator = new ValidationService();
            bool inputIsValid = validator.ControlUserInput(this);

            if (inputIsValid == false)

            {
                // Brugeren tastede noget forkert, fejl er allerede vist i metoden.
                return;
            }

            else

            {

                try

                {
                    ExcelExporter.Export(entry);
                    _window.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Fejl ved gemning: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}