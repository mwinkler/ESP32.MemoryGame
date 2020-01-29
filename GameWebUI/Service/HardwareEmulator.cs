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
        public event ButtonTriggerEvent OnButtonStateChanged;

        public void SetLed(int index, bool state)
        {
        }
    }
}
