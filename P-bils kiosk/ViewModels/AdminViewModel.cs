using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using P_bils_kiosk;

namespace P_bils_kiosk.ViewModels
{
    class AdminViewModel
    {
        private readonly Window _window;

        public AdminViewModel(Window window)
        {
            _window = window;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public ICommand LoginCommand { get; }

        public string InputAccessKey { get; set; }
        private void OpenAdminLoggedInWindow()
        {
            var vindue = new AdminLoggedIn();
            vindue.Show();
        }

        private void ExecuteLogin()
        {
            if (InputAccessKey == "2650") // Erstat evt. med mere sikker tjek senere
            {
                OpenAdminLoggedInWindow();
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