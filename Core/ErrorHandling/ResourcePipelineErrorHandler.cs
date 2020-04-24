namespace Core.CoreSystem.ErrorHandling
{
    using System;
    using Exceptions;
    using Utilities;
    
    public static class OpenGlShaderOperationCompleted
    {
        private const string TAG = "OPENGL SHADER";
        
        public static Exception Exception(string message) => new CustomInvalidOperationException(TAG, message);
        public static void Warning(string message) => ConsoleLog.Warning(TAG, message);
    }
}