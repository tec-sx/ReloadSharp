namespace Core.Logging.Exceptions
{
    using System;

    public class AudioEngineException : Exception
    {
        public AudioEngineException(string message)
        {
            ConsoleLog.Error($"AUDIO ENGINE", message);
        }
    }

    public class GraphicsEngineException : Exception
    {
        public GraphicsEngineException(string message)
        {
            ConsoleLog.Error($"GRAPHICS ENGINE", message);
        }
    }
    
    internal class CustomNotSupportedException : NotSupportedException
    {
        public CustomNotSupportedException(string tag, string message)
        {
            ConsoleLog.Error(tag, message);
        }
    }

    public static class GraphicsBackendNotSupported
    {
        private const string TAG = "GRAPHICS DEVICE";
        
        public static Exception Exception(string message)
        {
            return new CustomNotSupportedException(TAG, message);
        }
        
        public static void Warning(string message)
        {
            ConsoleLog.Warning(TAG, message);
        }
    }
    
    public static class GraphicsFeatureNotSupported
    {
        private const string TAG = "GRAPHICS DEVICE";
        
        public static Exception Exception(string message)
        {
            return new CustomNotSupportedException(TAG, message);
        }
        
        public static void Warning(string message)
        {
            ConsoleLog.Warning(TAG, message);
        }
    }
}