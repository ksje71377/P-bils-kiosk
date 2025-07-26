using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P_bils_kiosk.ViewModels
{
    class AdminViewModel
    {
        public AdminViewModel(Window window)
        {
            _window = window;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }
        private readonly Window _window;

        public ICommand LoginCommand { get; }

        public string InputAccessKey { get; set; }

        private void ExecuteLogin()
        {
            if (InputAccessKey == "2650") // Erstat evt. med mere sikker tjek senere
            {
                MessageBox.Show("Login godkendt", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                _window.Close(); // Eller åbn næste vindue her
            }
            else
            {
                SoundPlayer player = new SoundPlayer("Sounds\\error.wav");
                player.Play();
                MessageBox.Show("Forkert adgangskode", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}