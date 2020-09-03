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
using Reload.Core.Audio;
using Reload.Core.Graphics;
using Reload.Core.Input;
using Reload.Core.Windowing;

namespace Reload.Core
{
    /// <summary>
    /// The OS platform base class. Every opering system implementation
    /// must inherit from this class.
    /// </summary>
    public abstract class PlatformOS
    {
        /// <summary>
        /// Checks if the window is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckWindowCompatability<T>() where T : IProgramWindow;

        /// <summary>
        /// Checks if the garphics backend is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckGraphicsBackendCompatability<T>() where T : GraphicsAPI;

        /// <summary>
        /// Checks if the audio backend is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckAudioBackendCompatability<T>() where T : AudioAPI;

        /// <summary>
        /// Checks if the input is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckInputCompatability<T>() where T : IInputSystem;
    }
}
