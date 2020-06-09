namespace Reload.Gameplay
{
    using Reload.Graphics;
    using System;

    public interface IGame : IDisposable
    {
        /// <summary>
        /// The main game window.
        /// </summary>
        public GameWindow Window { get; set; }
        /// <summary>
        /// Is mouse visible.
        /// </summary>
        bool IsMouseVisible { get; set; }

        /// <summary>
        /// Run the game.
        /// </summary>
        void Run();
    }
}
