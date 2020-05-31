namespace Reload.Gameplay
{
    using System;
    using Silk.NET.Windowing.Common;

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
