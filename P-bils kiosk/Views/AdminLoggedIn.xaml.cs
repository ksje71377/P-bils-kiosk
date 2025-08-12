using System;
using System.Windows;

namespace P_bils_kiosk.ViewModels
{
    public partial class AdminLoggedIn : Window
    {
        public AdminLoggedIn()
        {
            InitializeComponent();
        }

        private void SendTestmail_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SendDailyMailStatic(DateTime.Today); // eller AddDays(-1)
        }
    }
}