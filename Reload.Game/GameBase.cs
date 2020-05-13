namespace Reload.Game
{
    using Silk.NET.Windowing.Common;

    public abstract class GameBase : IGame
    {
        /// <inheritdoc />
        public IWindow Window { get; set; }

        /// <inheritdoc />
        public bool IsMouseVisible { get; set; }

        protected GameBase()
        {

        }

        /// <summary>
        /// Override to add logic upon intialization of the game.
        /// </summary>
        protected abstract void OnInitialize();
        protected abstract void OnLoadContent();
        protected abstract void OnUpdate(double deltaTime);
        protected abstract void OnRender(double deltaTime);
        protected abstract void OnShutDown();


        /// <summary>
        /// Initializes the main game system.
        /// </summary>
        private void Initialize()
        {
            OnInitialize();
        }

        /// <summary>
        /// Shut down logic (cleaning up resources, etc.) logic goes here.
        /// </summary>
        private void ShutDown()
        {
            OnShutDown();
        }

        /// <summary>
        /// Loading basic game content logic goes here.
        /// </summary>
        private void Load()
        {
            OnLoadContent();

            SceneManager.ActiveScene.OnEnter();
            SceneManager.ActiveScene.Run();
        }

        /// <summary>
        /// Main game system update logic goes here.
        /// </summary>
        /// <param name="deltaTime"></param>
        private void Update(double deltaTime)
        {
            OnUpdate(deltaTime);
            SceneManager.Update(deltaTime);
        }


        /// <summary>
        /// Main game system render logic goes here.
        /// </summary>
        /// <param name="deltaTime"></param>
        private void Render(double deltaTime)
        {
            OnRender(deltaTime);
            SceneManager.Render(deltaTime);
        }

        /// <summary>
        /// Run the game.
        /// </summary>
        public void Run()
        {
            Initialize();

            Window.Load += Load;
            Window.Update += Update;
            Window.Render += Render;
            Window.Closing += ShutDown;
            SceneManager.ExitProgram += Window.Close;

            Window.Run();

            ShutDown();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ShutDownSubSystems();
                }
                Window.Dispose();
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
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}