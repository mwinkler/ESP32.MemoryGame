using System;

namespace GameLogic
{
    public delegate void ButtonTriggerEvent(int index, bool state);

    public interface IHardware
    {
        void SetLed(int index, bool state);
        void OnButtonStateChanged(ButtonTriggerEvent callback);
    }
}
