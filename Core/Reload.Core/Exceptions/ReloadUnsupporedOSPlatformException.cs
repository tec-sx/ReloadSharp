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
