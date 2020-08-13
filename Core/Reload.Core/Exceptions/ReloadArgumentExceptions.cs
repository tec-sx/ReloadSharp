using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;

namespace Reload.Core.Exceptions
{
    /// <summary>
    /// Custom argument null exception that gets logged by the <see cref="Utilities.Logger"/>.
    /// </summary>
    public class ReloadArgumentNullException : Exception
    {
        /// <summary>
        /// The default <see cref="ReloadArgumentNullException"/> constructor.
        /// Logs a default message 
        /// </summary>
        public ReloadArgumentNullException()
        {
            Logger.Log().Error(this, Resources.DefaultNullArgumentMessage);
        }

        /// <summary>
        /// <see cref="ReloadArgumentNullException"/> constructor with custom message.
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public ReloadArgumentNullException(string message) : base(message)
        {
            Logger.Log().Error(this, message);
        }

        /// <summary>
        /// <see cref="ReloadArgumentNullException"/> constructor with custom message and inner exception.
        /// Logs the message and the inner exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ReloadArgumentNullException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.Log().Error(this, message, innerException);
            Logger.Log().Error(innerException, $"{message} - Inner exception");
        }
    }
}
