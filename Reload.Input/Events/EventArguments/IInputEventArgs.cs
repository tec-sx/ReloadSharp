namespace Reload.Input.Events.EventArguments
{
    using Silk.NET.Input.Common;

    public interface IInputEventArgs
    {
        /// <summary>
        /// The device that sent this event
        /// </summary>
        IInputDevice Device { get; }
    }
}
