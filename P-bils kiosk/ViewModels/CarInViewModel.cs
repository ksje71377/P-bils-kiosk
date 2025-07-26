using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Media;
using System.Windows;
using System.Windows.Input;
using P_bils_kiosk.Helpers;
using P_bils_kiosk.Models;

namespace P_bils_kiosk.ViewModels
{
    public class CarInViewModel : IViewModelCommon
    {
        private readonly Window _window;

        public string ChaufførNummer { get; set; }
        public string ValgtBil { get; set; }
        public string Destination { get; set; }

        public ObservableCollection<string> IndDestinationer { get; set; }
        public ObservableCollection<string> IndBiler { get; set; }

        public ICommand BekræftCommand { get; }

        public CarInViewModel(Window window)
        {
            _window = window;
            BekræftCommand = new RelayCommand(GemOgLuk);
            IndBiler = new ObservableCollection<string>(ComboBoxLoader.LoadCars());
            IndDestinationer = new ObservableCollection<string>(ComboBoxLoader.LoadDestinations());
        }

        private void GemOgLuk()
        {
            var entry = new CarLogEntry
            {
                Tidspunkt = DateTime.Now,
                chaufførNummer = this.ChaufførNummer,
                valgtBil = this.ValgtBil,
                destination = this.Destination,
                isOutbound = false // Da det er "Car IN"
            };

            //Valideringsmetoden kaldes her

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
}

