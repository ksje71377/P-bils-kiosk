using P_bils_kiosk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace P_bils_kiosk
{
    /// <summary>
    /// Interaction logic for CarIn.xaml
    /// </summary>
    public partial class CarIn : Window
    {
            public CarIn()
            {
                InitializeComponent();
                DataContext = new CarInViewModel(this);
            }
        }
    }

