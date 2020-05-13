
namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Silk.NET.Input.Common;
    using System;

    /// <summary>
    /// Base class for input sources, implements common parts of the <see cref="IInputSource"/> interface and keeps track of registered devices through <see cref="RegisterDevice"/> and <see cref="UnregisterDevice"/>
    /// </summary>
    public abstract class InputSourceBase : IInputSource
    {
        public ObservableDictionary<string, IInputDevice> Devices { get; }

        public InputSourceBase()
        {
            Devices = new ObservableDictionary<string, IInputDevice>();
        }

        public abstract void Initialize(InputManager inputManager);

        public virtual void Update()
        { }

        public virtual void Pause()
        { }

        public virtual void Resume()
        { }

        public virtual void Scan()
        { }

        /// <summary>
        /// Unregisters all devices registered with <see cref="RegisterDevice"/> which have not been unregistered yet
        /// </summary>
        public virtual void Dispose()
        {
            Devices.Clear();
        }

        /// <summary>
        /// Adds the device to the list <see cref="Devices"/>
        /// </summary>
        /// <param name="device">The device</param>
        protected void RegisterDevice(IInputDevice device)
        {
            if (Devices.ContainsKey(device.Name))
                throw new InvalidOperationException($"Input device {device.Name} already registered");

            Devices.Add(device.Name, device);
        }

        /// <summary>
        /// CRemoves the device from the list <see cref="Devices"/>
        /// </summary>
        /// <param name="device">The device</param>
        protected void UnregisterDevice(IInputDevice device)
        {
            if (!Devices.ContainsKey(device.Name))
                throw new InvalidOperationException($"Input device {device.Name} was not registered");

            Devices.Remove(device.Name);
        }
    }
}
