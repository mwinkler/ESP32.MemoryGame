using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebUI.Service
{
    public class HardwareEmulator : IHardware
    {
        private readonly AppState AppState;

        public HardwareEmulator(AppState appState)
        {
            AppState = appState;
        }

        public event ButtonTriggerEvent OnButtonStateChanged;

        public void SetLed(int index, bool state)
        {
            AppState.Leds[index].State = state;
        }
    }
}
