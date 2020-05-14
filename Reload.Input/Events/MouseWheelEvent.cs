namespace Reload.Input.Events
{
    using Reload.Input.Source;

    /// <summary>
    /// Event for a mouse wheel being used
    /// </summary>
    public class MouseWheelEvent : InputEvent
    {
        /// <summary>
        /// The amount the mouse wheel scrolled
        /// </summary>
        public float WheelDelta;

        /// <summary>
        /// The mouse that sent this event
        /// </summary>
        public IMouseDevice Mouse => (IMouseDevice)Device;

        public override string ToString()
        {
            return $"{nameof(WheelDelta)}: {WheelDelta} {nameof(Mouse)}: {Mouse.Name}";
        }
    }
}
