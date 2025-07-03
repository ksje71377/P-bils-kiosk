using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P_bils_kiosk
{
    /// <summary>
    /// ViewModel til hovedvinduet. Indeholder kommandoer til at åbne ind- og ud-vinduer.
    /// </summary>
    public class MainViewModel
    {
        public ICommand OpenOutCommand { get; }
        public ICommand OpenInCommand { get; }

        public MainViewModel()
        {
            OpenOutCommand = new RelayCommand(OpenOutWindow);
            OpenInCommand = new RelayCommand(OpenInWindow);
        }

        /// <summary>
        /// Åbner vinduet hvor chaufføren tager en bil ud af garagen.
        /// </summary>
        private void OpenOutWindow()
        {
            var vindue = new carOut();
            vindue.Show();
        }

        /// <summary>
        /// Åbner vinduet hvor chaufføren afleverer en bil til garagen.
        /// </summary>
        private void OpenInWindow()
        {
            var vindue = new CarIn();
            vindue.Show();
        }
    }
}