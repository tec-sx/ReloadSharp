using System;
using System.Collections.Generic;
using System.Text;

namespace Reload.Input.Events
{
    public abstract class ButtonEvent : InputEvent
    {
        /// <summary>
        /// The new state of the button
        /// </summary>
        public bool IsDown;
    }
}
