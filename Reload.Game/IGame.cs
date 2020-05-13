namespace Reload.Game
{
    using Silk.NET.Windowing.Common;
    using System;

    public interface IGame : IDisposable
    {
        /// <summary>
        /// The main game window.
        /// </summary>
        public IWindow Window { get; set; }
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
