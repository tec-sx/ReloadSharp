namespace Core.CoreSystem.ErrorHandling.Exceptions
{
    using System;
    using Core.Utilities;
    
    public class AudioEngineException : Exception
    {
        public AudioEngineException(string message)
        {
            ConsoleLog.Error($"AUDIO ENGINE", message);
        }
    }
    
    
    internal class CustomNotSupportedException : NotSupportedException
    {
        public CustomNotSupportedException(string tag, string message)
        {
            ConsoleLog.Error(tag, message);
        }
    }
    
    internal class CustomInvalidOperationException : InvalidOperationException
    {
        public CustomInvalidOperationException(string tag, string message)
        {
            ConsoleLog.Error(tag, message);
        }
    }
}