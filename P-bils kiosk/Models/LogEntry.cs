using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_bils_kiosk.Models
{
        public class CarLogEntry
        {
            public string chaufførNummer { get; set; }
            public string valgtBil { get; set; }
            public string destination { get; set; }
            public DateTime Tidspunkt { get; set; } = DateTime.Now;
           public bool isOutbound { get; set; } = true; // true for udgående, false for indgående
    }
    }
