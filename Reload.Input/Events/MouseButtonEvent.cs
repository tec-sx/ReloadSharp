namespace Reload.Input.Events
{
    using Reload.Input.Source;
    using Silk.NET.Input.Common;

    /// <summary>
    /// Describes a button on a mouse changing state
    /// </summary>
    public class MouseButtonEvent : ButtonEvent
    {
        /// <summary>
        /// The button that changed state
        /// </summary>
        public MouseButton Button;

        /// <summary>
        /// The mouse that sent this event
        /// </summary>
        public IMouseDevice Mouse => (IMouseDevice)Device;

        public override string ToString()
        {
            return $"{nameof(Button)}: {Button}, {nameof(IsDown)}: {IsDown}, {nameof(Mouse)}: {Mouse.Name}";
        }
    }
}
