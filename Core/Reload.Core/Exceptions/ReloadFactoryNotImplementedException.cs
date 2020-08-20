using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;

namespace Reload.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when factory class is not implemented and null reference is returned. 
    /// </summary>
    public class ReloadFactoryNotImplementedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadFactoryNotImplementedException"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the Message property of the new instance to a 
        /// system-supplied message that describes the error, s
        /// </remarks>
        public ReloadFactoryNotImplementedException()
        {
            Logger.Log().Error(this, Resources.DefaultFactoryNotImplementedMessage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadFactoryNotImplementedException"/> class
        /// with the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="factoryName">The name of the parameter that caused the exception.</param>
        public ReloadFactoryNotImplementedException(string factoryName) : base(factoryName)
        {
            Logger.Log().Error(this, Resources.FactoryNotImplementedException, factoryName);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReloadFactoryNotImplementedException"/> class 
        /// with a specified error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ReloadFactoryNotImplementedException(string message, Exception innerException) : base(message, innerException)
        {
            Logger.Log().Error(this, message, innerException);
            Logger.Log().Error(innerException, $"{message} - Inner exception");
        }
    }
}
