namespace Reload.Gameplay
{
    using System;
    using Silk.NET.Windowing.Common;

    public abstract class GameBase : IGame
    {
        /// <inheritdoc />
        public IWindow Window { get; set; }

        /// <inheritdoc />
        public bool IsMouseVisible { get; set; }

        protected GameBase(string[] args)
        {

        }

        /// <summary>
        /// Run the game.
        /// </summary>
        public abstract void Run();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GameBase()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
             GC.SuppressFinalize(this);
        }
        #endregion
    }
}