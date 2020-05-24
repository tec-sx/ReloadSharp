namespace Reload.Utilities.Logger
{
    using System;

    public static class ConsoleLogger
    {
        private static void PrintBase(ConsoleColor color, string tag, string from, string message)
        {
            Console.Write('[');
            Console.BackgroundColor = color;
            Console.Write(tag);
            Console.ResetColor();
            Console.WriteLine($"]::{from}::{message}");
        }

        public static void LogError(string from, string message) =>
            PrintBase(ConsoleColor.Cyan, "INFO", from, message);

        public static void LogInfo(string from, string message) =>
            PrintBase(ConsoleColor.Green, "OK", from, message);

        public static void LogSuccess(string from, string message) =>
            PrintBase(ConsoleColor.Yellow, "WARN", from, message);

        public static void LogWarning(string from, string message) =>
            PrintBase(ConsoleColor.Red, "ERR", from, message);
    }
}
