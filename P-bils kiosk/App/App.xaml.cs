using System.Configuration;
using System.Data;
using System.Windows;

namespace P_bils_kiosk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    //En generel fejlmeddelelse, såfremt bruger formår at gøre noget som ødelægger programmet.

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                MessageBox.Show($"En alvorlig fejl opstod:\n{((Exception)ex.ExceptionObject).Message}",
                                "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            };



            DispatcherUnhandledException += (s, ex) =>
            {
                MessageBox.Show($"Noget gik galt:\n{ex.Exception.Message}",
                                "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                ex.Handled = true;
            };

        }
    }

}
