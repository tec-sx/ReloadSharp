using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;

namespace Reload.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is an attempt to dereference a null object reference.
    /// </summary>
    public class ReloadNullReferenceException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadNullReferenceException"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the Message property of the new instance to a 
        /// system-supplied message that describes the error, s
        /// </remarks>
        public ReloadNullReferenceException()
        {
            Logger.Log().Error(this, Resources.DefaultNullReferenceExceptionMessage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadNullReferenceException"/> class
        /// with the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="type">The name of the parameter that caused the exception.</param>
        public ReloadNullReferenceException(string type) : base(type)
        {
            Logger.Log().Error(this, Resources.NullReferenceExceptionMessage, type);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadNullReferenceException"/> class 
        /// with a specified error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ReloadNullReferenceException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.Log().Error(this, message, innerException);
            Logger.Log().Error(innerException, $"{message} - Inner exception");
        }
    }
}
