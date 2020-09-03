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
using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Reload.Core.Exceptions
{
    /// <summary>
    /// Custom OS platform not supported exception that gets logged 
    /// by the <see cref="Utilities.Logger"/>.
    /// </summary>
    public class ReloadUnsupporedOSPlatformException : Exception
    {
        /// <summary>
        /// Gets the current OS description and logs a default message
        /// stating that the it is not supported.
        /// </summary>
        public ReloadUnsupporedOSPlatformException()
        {
            string message = string.Format(
                CultureInfo.InvariantCulture,
                Resources.DefaultOSPlatfromNotSupportedMessage,
                RuntimeInformation.OSDescription);

            Logger.Log().Error(this, message);
        }

        /// <summary>
        /// <see cref="ReloadArgumentNullException"/> constructor with custom message.
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public ReloadUnsupporedOSPlatformException(string message) : base(message)
        {
            Logger.Log().Error(this, message);
        }

        /// <summary>
        /// <see cref="ReloadArgumentNullException"/> constructor with custom message and inner exception.
        /// Logs the message and the inner exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ReloadUnsupporedOSPlatformException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.Log().Error(this, message, innerException);
            Logger.Log().Error(innerException, $"{message} - Inner exception");
        }
    }
}
