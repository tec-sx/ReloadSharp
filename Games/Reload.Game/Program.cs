namespace Reload.Game
{
    using System;
    using Reload.Engine;
    using Silk.NET.Windowing.Common;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var game = new ReloadGame(args);
            var gameThread = new GameThread(game);
            
            game.CreateWindow();
            gameThread.Run();

            Console.ReadKey();
        }
    }
}