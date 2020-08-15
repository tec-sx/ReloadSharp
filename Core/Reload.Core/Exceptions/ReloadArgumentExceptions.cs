using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;

namespace Reload.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a null reference is passed to a method that does not accept it as a valid argument. 
    /// </summary>
    public class ReloadArgumentNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadArgumentNullException"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the Message property of the new instance to a 
        /// system-supplied message that describes the error, s
        /// </remarks>
        public ReloadArgumentNullException()
        {
            Logger.Log().Error(this, Resources.DefaultNullArgumentMessage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadArgumentNullException"/> class
        /// with the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        public ReloadArgumentNullException(string paramName) : base(paramName)
        {
            Logger.Log().Error(this, Resources.NullArgumentMessage, paramName);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadArgumentNullException"/> class 
        /// with a specified error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ReloadArgumentNullException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.Log().Error(this, message, innerException);
            Logger.Log().Error(innerException, $"{message} - Inner exception");
        }
    }
}
