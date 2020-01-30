using System;
using System.Collections;
using System.Threading;

namespace GameLogic
{
    public class Game
    {
        private enum State
        {
            IDLE,
            RESET,
            SHOW_SOLUTION,
            SHOW_NEXT_SOLUTION,
            PLAYER_INPUT,
            PLAYER_MISTAKE
        }
        
        private readonly IHardware Hardware;
        private readonly Settings Settings;
        private readonly Random Rnd;
        private readonly bool[] Leds;
        private State CurrentState;
        private bool IsRunning;
        private ArrayList Solution;
        private int CurrentPosition;
        private string LastLogMessage;

        public Game(IHardware hardware, Settings settings)
        {
            Hardware = hardware;
            Settings = settings;
            Rnd = new Random(settings.RandomSeed);
            Leds = new bool[settings.LedCount];

            // register button change event
            hardware.OnButtonStateChanged += OnButtonStateChanged;
        }

        private void OnButtonStateChanged(int button, bool down)
        {
            Log($"[Game.ButtonStateChanged] button: {button}, down: {down}");

            switch (CurrentState)
            {
                case State.PLAYER_INPUT:

                    SetLed(button, down);

                    // button down
                    if (down)
                    {
                        var expectedButton = (int)Solution[CurrentPosition];

                        Log($"[Game.ButtonStateChanged] Button pressed -> player: {button}, expected: {expectedButton}");

                        // check if correct button pressed
                        if (expectedButton == button)
                        {
                            CurrentPosition++;
                        }
                        else
                        {
                            CurrentState = State.PLAYER_MISTAKE;
                        }
                    }

                    // button up
                    else
                    {
                        if (CurrentPosition >= Solution.Count)
                        {
                            CurrentPosition = 0;
                            CurrentState = State.SHOW_NEXT_SOLUTION;
                        }
                    }

                    break;
            }
        }

        public void Run()
        {
            Log("[Game.Run]");

            // check if game is already running
            if (IsRunning)
            {
                return;
            }

            // set states
            IsRunning = true;
            CurrentState = State.IDLE;

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

        public void Reset()
        {
            Log("[Game.Reset]");

            CurrentState = State.RESET;
        }

        public void Stop()
        {
            Log("[Game.Stop]");

            IsRunning = false;
        }

        private void Tick()
        {
            switch (CurrentState)
            {
                // do nothing
                case State.IDLE:

                    Log("[Game.Tick] Idle");

                    break;


                // reset game to inital state
                case State.RESET:

                    Log("[Game.Tick] Reset game");

                    Solution = new ArrayList();
                    CurrentPosition = 0;

                    // create inital solution for game
                    AddSolution(Settings.MinimumSolutionSteps);

                    // disable leds
                    SetLeds(false);

                    // next step
                    CurrentState = State.SHOW_SOLUTION;

                    break;


                // game is presenting the solution to player
                case State.SHOW_SOLUTION:

                    Log("[Game.Tick] Show solution");

                    ShowSolution(0);

                    CurrentState = State.PLAYER_INPUT;

                    break;


                // wait for player input
                case State.PLAYER_INPUT:

                    Log("[Game.Tick] Player input");

                    break;


                // player press wrong button
                case State.PLAYER_MISTAKE:

                    Log("[Game.Tick] Player mistake");

                    break;


                // show next solution
                case State.SHOW_NEXT_SOLUTION:

                    Log("[Game.Tick] Show next solution");

                    AddSolution(1);

                    ShowSolution(Solution.Count - 1);

                    CurrentState = State.PLAYER_INPUT;

                    break;


            }
        }

        private void AddSolution(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Solution.Add(Rnd.Next(Settings.LedCount - 1));
            }
        }

        private void ShowSolution(int startIndex)
        {
            for (var step = startIndex; step < Solution.Count; step++)
            {
                // turn off leds
                SetLeds(false);

                // wait step interval
                Thread.Sleep(Settings.StepInterval);

                // enable led of current solution step
                SetLed((int)Solution[step], true);

                // wait for current step duration
                Thread.Sleep(Settings.StepDuration);
            }

            // turn off leds
            SetLeds(false);
        }

        private void SetLed(int index, bool state)
        {
            Leds[index] = state;

            Hardware.SetLeds(Leds);
        }

        private void SetLeds(bool state)
        {
            for (int i = 0; i < Settings.LedCount; i++)
            {
                Leds[i] = state;
            }

            Hardware.SetLeds(Leds);
        }

        private void Log(string message)
        {
            if (message == LastLogMessage)
            {
                Console.Write(".");
            }
            else
            {
                Console.Write("\n" + message);
            }

            LastLogMessage = message;
        }
    }
}
