namespace Core.CoreSystem.ErrorHandling.Exceptions
{
    using System;
    using Engine.Utilities.Logger;

    public class AudioEngineException : Exception
    {
        public AudioEngineException(string message)
        {
            ConsoleLogger.LogError($"AUDIO ENGINE", message);
        }
    }


    internal class CustomNotSupportedException : NotSupportedException
    {
        public CustomNotSupportedException(string tag, string message)
        {
            ConsoleLogger.LogError(tag, message);
        }
    }

    internal class CustomInvalidOperationException : InvalidOperationException
    {
        public CustomInvalidOperationException(string tag, string message)
        {
            ConsoleLogger.LogError(tag, message);
        }
    }
}