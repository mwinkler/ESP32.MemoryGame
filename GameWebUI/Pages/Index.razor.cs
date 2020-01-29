using GameLogic;
using GameWebUI.Service;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebUI.Pages
{
    public partial class Index
    {
        [Inject]
        private AppState State { get; set; }

        [Inject]
        private IHardware Hardware { get; set; }

        private Game Game { get; set; }

        private async Task Start()
        {
            Game = new Game(Hardware, State.Leds.Count);

            await Task.Run(() => 
            {
                Game.Start();
            });
        }
    }
}
