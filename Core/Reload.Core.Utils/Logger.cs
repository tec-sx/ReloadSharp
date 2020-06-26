namespace Reload.Core.Utils
{
    using System;

    public static class Logger
    {
        private static void PrintBase(ConsoleColor color, string tag, string message)
        {
            Console.Write("[");
            Console.ForegroundColor = color;
            Console.Write(tag);
            Console.ResetColor();
            Console.Write("]:");
            Console.WriteLine(message);
        }

        public static void PrintInfo(string message)
        {
            PrintBase(ConsoleColor.Cyan, "INFO", message);
        }

        public static void PrintWarning(string message)
        {
            PrintBase(ConsoleColor.Yellow, "WARNING", message);
        }

        public static void PrintError(string message)
        {
            PrintBase(ConsoleColor.Red, "ERROR", message);
        }
    }
}
