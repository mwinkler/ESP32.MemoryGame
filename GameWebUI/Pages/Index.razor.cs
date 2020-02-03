using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GameLogic;
using GameWebUI.Model;

namespace GameWebUI.Pages
{
    public partial class Index : IDisposable, IHardware
    {
        private IList<LedButton> LedButtons;

        private Game Game;

        public string Score { get; set; }

        public void Dispose() => Game?.Stop();

        #region IHardware

        public event ButtonTriggerEvent OnButtonStateChanged;

        public void SetLeds(bool[] states)
        {
            for (int i = 0; i < LedButtons.Count; i++)
            {
                LedButtons[i].State = states[i];
            }

            InvokeAsync(StateHasChanged);
        }

        public void DisplayScore(int count)
        {
            Score = $"Score: {count}";

            InvokeAsync(StateHasChanged);
        }

        #endregion

        protected override void OnInitialized()
        {
            // ui settings
            LedButtons = new[]
            {
                new LedButton { Index = 0, State = false, Color = "red" },
                new LedButton { Index = 1, State = false, Color = "green" },
                new LedButton { Index = 2, State = false, Color = "blue" },
                new LedButton { Index = 3, State = false, Color = "yellow" },
            };

            // game settings
            var settings = new Settings
            {
                LedCount = LedButtons.Count,
                TickInterval = TimeSpan.FromMilliseconds(100),
                MinimumSolutionSteps = 3,
                RandomSeed = (int)DateTime.Now.Ticks,
                StepDuration = TimeSpan.FromSeconds(1),
                StepInterval = TimeSpan.FromMilliseconds(250),
                Replay = true,
                ReplayDuration = TimeSpan.FromMilliseconds(250),
                ReplayInterval = TimeSpan.FromMilliseconds(125),
                WaitBeforeShowNextSolution = TimeSpan.FromMilliseconds(1000)
            };

            // create game instance
            Game = new Game(this, settings);

            // run game in new thread
            Task.Run(() =>
            {
                Game.Run();
            });
        }

        private void Press(int index, bool state) => OnButtonStateChanged?.Invoke(index, state);

        
    }
}
