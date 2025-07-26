using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using P_bils_kiosk.ViewModels;
using P_bils_kiosk.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using P_bils_kiosk.Helpers;
using System.Media;

namespace P_bils_kiosk.Helpers
{
    public class ValidationService
    {

        public bool ControlUserInput(IViewModelCommon viewModel)
        {
                string valgtBil = viewModel.ValgtBil;
                string destination = viewModel.Destination;
                string chaufførNummer = viewModel.ChaufførNummer;

                if (string.IsNullOrWhiteSpace(valgtBil))
                {
                    SoundPlayer player = new SoundPlayer("Sounds\\error.wav");
                    player.Play();
                    MessageBox.Show("Bil kan ikke være tomt.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(destination))
                {
                    SoundPlayer player = new SoundPlayer("Sounds\\error.wav");
                    player.Play();
                    MessageBox.Show("Destination kan ikke være tomt.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
            }

                else if (string.IsNullOrWhiteSpace(chaufførNummer))
                {
                    SoundPlayer player = new SoundPlayer("Sounds\\error.wav");
                    player.Play();
                    MessageBox.Show("Chaufførnummer kan ikke være tomt.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;    
            }

                else if (!System.Text.RegularExpressions.Regex.IsMatch(chaufførNummer, @"^\d{4}$"))
                {
                    SoundPlayer player = new SoundPlayer("Sounds\\error.wav");
                    player.Play();
                    MessageBox.Show("Chaufførnummer skal bestå af præcis 4 numeriske cifre.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                else
                {
                    return true;
                // Alt er godkendt, bryd løkken

            }

            }
    }
}