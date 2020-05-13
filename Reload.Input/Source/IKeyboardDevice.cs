using System;
using System.Collections.Generic;
using System.Text;

namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Silk.NET.Input.Common;

    /// <summary>
    /// A keyboard device
    /// </summary>
    public interface IKeyboardDevice : IInputDevice
    {
        /// <summary>
        /// The keys that have been pressed since the last frame
        /// </summary>
        IReadOnlySet<Key> PressedKeys { get; }

        /// <summary>
        /// The keys that have been released since the last frame
        /// </summary>
        IReadOnlySet<Key> ReleasedKeys { get; }

        /// <summary>
        /// List of keys that are currently down on this keyboard
        /// </summary>
        IReadOnlySet<Key> DownKeys { get; }

        /// <summary>
        /// Determines whether the specified key is pressed since the previous update.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns><c>true</c> if the specified key is pressed; otherwise, <c>false</c>.</returns>
        bool IsKeyPressed(Key key);

        /// <summary>
        /// Determines whether the specified key is released since the previous update.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns><c>true</c> if the specified key is released; otherwise, <c>false</c>.</returns>
        bool IsKeyReleased(Key key);

        /// <summary>
        /// Determines whether the specified key is being pressed down
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns><c>true</c> if the specified key is being pressed down; otherwise, <c>false</c>.</returns>
        bool IsKeyDown(Key key);
    }
}
