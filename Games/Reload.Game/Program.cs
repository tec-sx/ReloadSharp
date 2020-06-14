namespace Reload.Game
{
    using System;
    using Reload.Engine;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var game = new ReloadGame(args);
            var gameThread = new GameThread(game);

            gameThread.Run();

            Console.ReadKey();
        }
    }
}