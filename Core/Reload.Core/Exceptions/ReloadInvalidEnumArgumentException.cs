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
