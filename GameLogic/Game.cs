using System;
using System.Collections;

namespace GameLogic
{
    public class Game
    {
        private enum Sate
        {
            DISPLAY_PATH,
            PLAYER_INPUTS
        }
        
        private readonly IHardware Hardware;
        private readonly int LedCount;

        // state
        private ArrayList Solution;
        private int PlayerPosition;

        public Game(IHardware hardware, int ledCount)
        {
            Hardware = hardware;
            LedCount = ledCount;

            // register button change event
            hardware.OnButtonStateChanged += OnButtonStateChanged;
        }

        private void OnButtonStateChanged(int index, bool state)
        {
            Console.WriteLine($"[ButtonStateChanged] index: {index}, state: {state}");
        }

        public void Start()
        {
            // reset
            Solution = new ArrayList();
            PlayerPosition = 0;
        }
    }
}
