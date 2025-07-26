using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace P_bils_kiosk.ViewModels
    {
        public interface IViewModelCommon
        {
            string ValgtBil { get; }
            string Destination { get; }
            string ChaufførNummer { get; }
        }
    }
