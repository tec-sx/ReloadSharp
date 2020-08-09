using Reload.Core.Utils;
using Reload.Rendering.Properties;
using System;

namespace Reload.Rendering.Exceptions
{
    /// <summary>
    /// Exception to be thrown when trying to set the renderer api capabilities 
    /// more than once.
    /// </summary>
    public class RendererApiCapabilitesAlreadySetException : Exception
    {
        /// <summary>
        /// Initializes a new default instance of the <see cref="RendererApiCapabilitesAlreadySetException"/> class.
        /// </summary>
        public RendererApiCapabilitesAlreadySetException() : base(Resources.RendererApiCapabilitiesAlreadySet)
        {
            Logger.PrintError(Resources.RendererApiCapabilitiesAlreadySet);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RendererApiCapabilitesAlreadySetException"/> class
        /// with custom message.
        /// </summary>
        /// <param name="message">The message.</param>
        public RendererApiCapabilitesAlreadySetException(string message) : base(message)
        {
            Logger.PrintError(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RendererApiCapabilitesAlreadySetException"/> class
        /// with custom message and inner exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RendererApiCapabilitesAlreadySetException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.PrintError(message);
        }
    }

    /// <summary>
    /// Exception to be thrown when trying to get the renderer api capabilities 
    /// if they are not yet set.
    /// </summary>
    public class RendererApiCapabilitesNotSetException : Exception
    {
        /// <summary>
        /// Initializes a new default instance of the <see cref="RendererApiCapabilitesAlreadySetException"/> class.
        /// </summary>
        public RendererApiCapabilitesNotSetException() : base(Resources.RendererApiCapabilitiesNotSet)
        {
            Logger.PrintError(Resources.RendererApiCapabilitiesNotSet);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RendererApiCapabilitesAlreadySetException"/> class
        /// with custom message.
        /// </summary>
        /// <param name="message">The message.</param>
        public RendererApiCapabilitesNotSetException(string message) : base(message)
        {
            Logger.PrintError(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RendererApiCapabilitesAlreadySetException"/> class
        /// with custom message and inner exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RendererApiCapabilitesNotSetException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.PrintError(message);
        }
    }
}
