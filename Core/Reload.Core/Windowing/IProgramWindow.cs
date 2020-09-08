#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion

using Reload.Core.Game;
using System;
using System.Drawing;

namespace Reload.Core.Windowing
{
    /// <summary>
    /// The game window base.
    /// </summary>
    public interface IProgramWindow : ISubSystem, IDisposable
    {
        /// <summary>
        /// Gets the windowing backend type.
        /// </summary>
        WindowingAPIType Api { get; }

        /// <summary>
        /// Gets the window size.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Gets or sets the window X position.
        /// </summary>
        Point Position { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the window full is screen.
        /// </summary>
        bool IsFullScreen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether vsync is on.
        /// </summary>
        bool IsVsyncOn { get; set; }

        /// <summary>
        /// Gets the native window handle.
        /// </summary>
        Func<string, IntPtr> GetProcAddress { get; }

        /// <summary>
        /// Executes on window startup.
        /// </summary>
        Action Load { get; set; }

        /// <summary>
        /// Executes on window update.
        /// </summary>
        Action<double> Update { get; set; }

        /// <summary>
        /// Executes on window render.
        /// </summary>
        Action<double> Render { get; set; }

        /// <summary>
        /// Executed when the window is moved.
        /// </summary>
        Action<Point> Move { get; set; }

        /// <summary>
        /// Executed when the window is resized.
        /// </summary>
        Action<Size> Resize { get; set; }

        /// <summary>
        /// Executed when the window focus changes.
        /// </summary>
        Action<bool> FocusChanged { get; set; }

        /// <summary>
        /// Executes on window closing.
        /// </summary>
        Action Closing { get; set; }
    }
}
