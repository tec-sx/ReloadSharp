namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Reload.Input.Events;
    using Reload.Input.Events.EventArguments;
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    /// <summary>
    /// Base class for pointer devices
    /// </summary>
    public abstract class PointerDeviceBase : IPointerDevice
    {
        protected PointerDeviceState PointerState;

        protected PointerDeviceBase()
        {
            PointerState = new PointerDeviceState(this);
        }

        public Vector2 SurfaceSize => PointerState.SurfaceSize;
        public float SurfaceAspectRatio => PointerState.SurfaceAspectRatio;
        public IReadOnlySet<PointerPoint> PressedPointers => PointerState.PressedPointers;
        public IReadOnlySet<PointerPoint> ReleasedPointers => PointerState.ReleasedPointers;
        public IReadOnlySet<PointerPoint> DownPointers => PointerState.DownPointers;
        public event EventHandler<SurfaceSizeChangedEventArgs> SurfaceSizeChanged;

        public int Priority { get; set; }

        public abstract string Name { get; }
        public abstract int Index { get; }
        public abstract bool IsConnected { get; }

        public virtual void Update(List<InputEvent> inputEvents)
        {
            PointerState.Update(inputEvents);
        }

        /// <summary>
        /// Calls <see cref="PointerDeviceState.SetSurfaceSize"/> and invokes the <see cref="SurfaceSizeChanged"/> event
        /// </summary>
        /// <param name="newSize">New size of the surface</param>
        protected void SetSurfaceSize(Vector2 newSize)
        {
            PointerState.SetSurfaceSize(newSize);
            SurfaceSizeChanged?.Invoke(this, new SurfaceSizeChangedEventArgs { NewSurfaceSize = newSize });
        }

        protected Vector2 Normalize(Vector2 position)
        {
            return position * PointerState.InverseSurfaceSize;
        }
    }
}