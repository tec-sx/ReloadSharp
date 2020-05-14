﻿namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;
    using System.Numerics;

    /// <summary>
    /// Represents functionality specific to mouse input such as buttons, wheels, mouse locking and setting cursor position
    /// </summary>
    public interface IMouseDevice : IInputDevice
    {
        /// <summary>
        /// Normalized position of the mouse inside the window
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        /// Mouse delta
        /// </summary>
        Vector2 Delta { get; }

        /// <summary>
        /// The mouse buttons that have been pressed since the last frame
        /// </summary>
        HashSet<MouseButton> PressedButtons { get; }

        /// <summary>
        /// The mouse buttons that have been released since the last frame
        /// </summary>
        HashSet<MouseButton> ReleasedButtons { get; }

        /// <summary>
        /// The mouse buttons that are down
        /// </summary>
        HashSet<MouseButton> DownButtons { get; }

        /// <summary>
        /// Gets or sets if the mouse is locked to the screen
        /// </summary>
        bool IsPositionLocked { get; }

        /// <summary>
        /// Locks the mouse position to the screen
        /// </summary>
        /// <param name="forceCenter">Force the mouse position to the center of the screen</param>
        void LockPosition(bool forceCenter = false);

        /// <summary>
        /// Unlocks the mouse position if it was locked
        /// </summary>
        void UnlockPosition();

        /// <summary>
        /// Attempts to set the pointer position, this only makes sense for mouse pointers
        /// </summary>
        /// <param name="normalizedPosition"></param>
        void SetPosition(Vector2 normalizedPosition);
    }
}
