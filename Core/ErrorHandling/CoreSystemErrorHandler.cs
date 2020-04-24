namespace Core.CoreSystem.ErrorHandling
{
    using System;
    using Utilities;
    using Exceptions;

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

    public static class GraphicsDebugNotSupported
    {
        private const string TAG = "GRAPHICS DEBUG";

        public static Exception Exception(string message)
        {
            return new CustomNotSupportedException(TAG, message);
        }

        public static void Warning(string message)
        {
            ConsoleLog.Warning(TAG, message);
        }
    }

    public static class VulkanInvalidOperation
    {
        private const string TAG = "VULKAN BACKEND";

        public static Exception Exception(string message)
        {
            return new CustomInvalidOperationException(TAG, message);
        }

        public static void Warning(string message)
        {
            ConsoleLog.Warning(TAG, message);
        }
    }
}