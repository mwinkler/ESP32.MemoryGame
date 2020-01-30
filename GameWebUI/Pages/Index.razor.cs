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
    public partial class Index : IDisposable
    {
        [Inject]
        private AppState State { get; set; }

        [Inject]
        private IHardware Hardware { get; set; }

        private Game Game { get; set; }

        public void Dispose()
        {
            if (Game != null)
            {
                Game.Stop();
            }
        }

        protected override void OnInitialized()
        {
            State.StateHasChanged = () => InvokeAsync(StateHasChanged);

            // settings
            var settings = new Settings
            {
                LedCount = State.Leds.Count,
                TickInterval = TimeSpan.FromMilliseconds(100),
                MinimumSolutionSteps = 3,
                RandomSeed = (int)DateTime.Now.Ticks,
                StepDuration = TimeSpan.FromSeconds(1),
                StepInterval = TimeSpan.FromMilliseconds(250)
            };

            // create game instance
            Game = new Game(Hardware, settings);

            // run game in new thread
            Task.Run(() =>
            {
                Game.Run();
            });
        }

        private void Start()
        {
            Game.Reset();
        }
    }
}
