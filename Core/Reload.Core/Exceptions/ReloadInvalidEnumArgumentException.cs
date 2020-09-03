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

namespace Reload.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown if you pass an invalid enumeration value to a method or when setting a property.
    /// </summary>
    public class ReloadInvalidEnumArgumentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadInvalidEnumArgumentException"/> class
        /// with default message.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the Message property of the new instance to a 
        /// system-supplied message that describes the error.
        /// </remarks>
        public ReloadInvalidEnumArgumentException()
        {
            Logger.Log().Error(this, Resources.DefaultInvalidEnumArgumentMessage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadInvalidEnumArgumentException"/> class
        /// with the name of the enum that causes this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        public ReloadInvalidEnumArgumentException(string enumName) : base(enumName)
        {
            Logger.Log().Error(this, Resources.InvalidEnumArgumentMessage, enumName);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadInvalidEnumArgumentException"/> class 
        /// with a specified error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ReloadInvalidEnumArgumentException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.Log().Error(this, message, innerException);
            Logger.Log().Error(innerException, $"{message} - Inner exception");
        }
    }
}
