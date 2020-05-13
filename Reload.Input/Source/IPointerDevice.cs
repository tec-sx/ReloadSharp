﻿namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Reload.Input.Events.EventArguments;
    using Silk.NET.Input.Common;
    using System;
    using System.Numerics;

    /// <summary>
    /// Provides an interface for interacting with pointer devices, this can be a mouse, pen, touch screen, etc.
    /// </summary>
    public interface IPointerDevice : IInputDevice
    {
        /// <summary>
        /// The size of the surface used by the pointer, for a mouse this is the size of the window, for a touch device, the size of the touch area, etc.
        /// </summary>
        Vector2 SurfaceSize { get; }

        /// <summary>
        /// The aspect ratio of the touch surface area
        /// </summary>
        float SurfaceAspectRatio { get; }

        /// <summary>
        /// The index of the pointers that have been pressed since the last frame
        /// </summary>
        IReadOnlySet<PointerPoint> PressedPointers { get; }

        /// <summary>
        /// The index of the pointers that have been released since the last frame
        /// </summary>
        IReadOnlySet<PointerPoint> ReleasedPointers { get; }

        /// <summary>
        /// The index of the pointers that are down
        /// </summary>
        IReadOnlySet<PointerPoint> DownPointers { get; }

        /// <summary>
        /// Raised when the surface size of this pointer changed
        /// </summary>
        event EventHandler<SurfaceSizeChangedEventArgs> SurfaceSizeChanged;
    }
}
