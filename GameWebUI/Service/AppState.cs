using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebUI.Service
{
    public class AppState
    {
        public Action StateHasChanged { get; set; }

        public AppState()
        {
            Leds = new[]
            {
                new Led { Index = 0, State = false, Color = "red" },
                new Led { Index = 1, State = false, Color = "green" },
                new Led { Index = 2, State = false, Color = "blue" },
                new Led { Index = 3, State = false, Color = "yellow" },
            };
        }

        public class Led
        {
            public int Index { get; set; }
            public bool State { get; set; }
            public string Color { get; set; }
        }

        public IList<Led> Leds { get; set; }
    }
}
