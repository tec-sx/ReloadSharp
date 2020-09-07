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

namespace Reload.Core.Input.Models
{
    /// <summary>
    /// Represents the indices of the mouse buttons.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The number of buttons provided depends on the input backend currently being used.
    /// </para>
    /// </remarks>
    public enum MouseButton
    {
        /// <summary>
        /// Indicates the input backend was unable to determine a button name for the button in question, or it does not support it.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// The left mouse button.
        /// </summary>
        Left = 0,

        /// <summary>
        /// The right mouse button.
        /// </summary>
        Right,

        /// <summary>
        /// The middle mouse button.
        /// </summary>
        Middle,

        /// <summary>
        /// The fourth mouse button.
        /// </summary>
        Button4,

        /// <summary>
        /// The fifth mouse button.
        /// </summary>
        Button5,

        /// <summary>
        /// The sixth mouse button.
        /// </summary>
        Button6,

        /// <summary>
        /// The seventh mouse button.
        /// </summary>
        Button7,

        /// <summary>
        /// The eighth mouse button.
        /// </summary>
        Button8,

        /// <summary>
        /// The ninth mouse button.
        /// </summary>
        Button9,

        /// <summary>
        /// The tenth mouse button.
        /// </summary>
        Button10,

        /// <summary>
        /// The eleventh mouse button.
        /// </summary>
        Button11,

        /// <summary>
        /// The twelth mouse button.
        /// </summary>
        Button12
    }
}
