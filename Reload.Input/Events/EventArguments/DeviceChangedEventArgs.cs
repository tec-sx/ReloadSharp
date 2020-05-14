namespace Reload.Input.Events.EventArguments
{
    using Reload.Input.Source;
    using Silk.NET.Input.Common;
    using System;

    public class DeviceChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The device that changed
        /// </summary>
        public IInputDevice Device;

        /// <summary>
        /// The type of change that happened
        /// </summary>
        public DeviceChangedEventType Type;
    }
}
