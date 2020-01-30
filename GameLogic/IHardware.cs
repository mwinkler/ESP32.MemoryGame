using System;

namespace GameLogic
{
    public delegate void ButtonTriggerEvent(int index, bool state);

    public interface IHardware
    {
        void SetLeds(bool[] states);

        event ButtonTriggerEvent OnButtonStateChanged;
    }
}
