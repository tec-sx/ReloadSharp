namespace ReloadGame
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var game = new ReloadGame(args);
            game.Run();
        }
    }
}