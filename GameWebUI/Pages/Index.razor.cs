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
            // exit if game instance already created
            if (Game != null)
            {
                return;
            }

            var settings = new Settings
            {
                LedCount = State.Leds.Count,
                TickInterval = TimeSpan.FromMilliseconds(100)
            };

            // create game instance
            Game = new Game(Hardware, settings);

            // run game in new thread
            await Task.Run(() => 
            {
                Game.Run();
            });
        }
    }
}
