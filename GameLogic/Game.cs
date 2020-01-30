using System;
using System.Collections;
using System.Threading;

namespace GameLogic
{
    public class Game
    {
        private enum State
        {
            RESET,
            SHOW_SOLUTION,
            //SHOW_SOLUTION_WAIT,
            PLAYER_INPUT
        }
        
        private readonly IHardware Hardware;
        private readonly Settings Settings;
        private readonly Random Rnd;
        private State CurrentState;
        private bool IsRunning;
        private ArrayList Solution;
        private int CurrentPosition;

        public Game(IHardware hardware, Settings settings)
        {
            Hardware = hardware;
            Settings = settings;
            Rnd = new Random(settings.RandomSeed);

            // register button change event
            hardware.OnButtonStateChanged += OnButtonStateChanged;
        }

        private void OnButtonStateChanged(int index, bool state)
        {
            Console.WriteLine($"[ButtonStateChanged] index: {index}, state: {state}");
        }

        public void Run()
        {
            // check if game is already running
            if (IsRunning)
            {
                return;
            }

            // set states
            IsRunning = true;
            CurrentState = State.RESET;

            // game loop
            while (true)
            {
                // exit when game is stopped
                if (!IsRunning)
                {
                    break;
                }

                // process game
                Tick();

                // wait for next tick interval
                Thread.Sleep(Settings.TickInterval);
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private void Tick()
        {
            switch (CurrentState)
            {
                // reset game to inital state
                case State.RESET:

                    Console.WriteLine($"[Tick] Reset game");

                    Solution = new ArrayList();
                    CurrentPosition = 0;

                    // create inital solution for game
                    for (int i = 0; i < Settings.MinimumSolutionSteps; i++)
                    {
                        Solution.Add(Rnd.Next(Settings.LedCount - 1));
                    }

                    // disable leds
                    SetLeds(false);

                    // next step
                    CurrentState = State.SHOW_SOLUTION;

                    break;

                // game is presenting the solution to player
                case State.SHOW_SOLUTION:

                    Console.WriteLine($"[Tick] Show solution");

                    for (var step = 0; step < Solution.Count; step++)
                    {
                        // wait step interval
                        Thread.Sleep(Settings.StepInterval);
                        
                        // disable all leds
                        SetLeds(false);

                        // enable led of current solution step
                        Hardware.SetLed((int)Solution[step], true);

                        // wait for current step duration
                        Thread.Sleep(Settings.StepDuration);
                    }

                    // next step
                    CurrentState = State.PLAYER_INPUT;

                    break;

                case State.PLAYER_INPUT:
                    break;
            }
        }

        private void SetLeds(bool state)
        {
            for (int i = 0; i < Settings.LedCount; i++)
            {
                Hardware.SetLed(i, state);
            }
        }
    }
}
