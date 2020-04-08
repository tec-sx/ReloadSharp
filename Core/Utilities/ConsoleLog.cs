namespace Core.Utilities
{
    using System;
    
    public static class ConsoleLog
    {
        private static void PrintBase(ConsoleColor color, string tag, string from, string message)
        {
            Console.Write('[');
            Console.BackgroundColor = color;
            Console.Write(tag);
            Console.ResetColor();
            Console.WriteLine($"]::{from}::{message}");
        }

        public static void Info(string from, string message) => 
            PrintBase(ConsoleColor.Cyan, "INFO", from, message);
        
        public static void Success(string from, string message) => 
            PrintBase(ConsoleColor.Green, "OK", from, message);
        
        public static void Warning(string from, string message) => 
            PrintBase(ConsoleColor.Yellow, "WARN", from, message);
        
        public static void Error(string from, string message) => 
            PrintBase(ConsoleColor.Red, "ERR", from, message);
    }
}