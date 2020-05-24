using System;

namespace ReloadGame
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var game = new ReloadGame(args);
            game.Run();
        }
    }
}