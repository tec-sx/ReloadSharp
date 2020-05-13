namespace Reload.Input.Events
{
    using Reload.Input.Source;
    using Silk.NET.Input.Common;

    /// <summary>
    /// Event for a keyboard button changing state
    /// </summary>
    public class KeyEvent : ButtonEvent
    {
        /// <summary>
        /// The key that is being pressed or released.
        /// </summary>
        public Key Key;

        /// <summary>
        /// The repeat count for this key. If it is 0 this is the initial press of the key
        /// </summary>
        public int RepeatCount;

        /// <summary>
        /// The keyboard that sent this event
        /// </summary>
        public IKeyboardDevice Keyboard => (IKeyboardDevice)Device;

        public override string ToString()
        {
            return $"{nameof(Key)}: {Key}, {nameof(IsDown)}: {IsDown}, {nameof(RepeatCount)}: {RepeatCount}, {nameof(Keyboard)}: {Keyboard}";
        }
    }
}
