using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebUI.Service
{
    public class AppState
    {
        public class Led
        {
            public int Index { get; set; }
            public bool State { get; set; }
            public string Color { get; set; }
        }

        public IEnumerable<Led> Leds { get; set; }
    }
}
