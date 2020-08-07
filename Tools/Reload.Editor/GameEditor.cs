using Microsoft.Extensions.DependencyInjection;
using Reload.Editor.Input;
using Reload.Editor.Platform;
using Reload.Scenes;
using SpaceVIL.Common;
using System;

namespace Reload.Editor
{
    /// <summary>
    /// The game editor startup class.
    /// </summary>
    public sealed class GameEditor: IDisposable
    {
        private readonly ServiceProvider _provider;
        private readonly MainWindow _window;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameEditor"/> class
        /// and sets up the DI provider and services.
        /// Should call <seealso cref="Initialize"/> and <seealso cref="Start"/>
        /// after initialization.
        /// </summary>
        public GameEditor()
        {
            CommonService.InitSpaceVILComponents();

            ServiceCollection collection = new ServiceCollection();

            collection
                .AddSingleton<OpenGl>()
                .AddSingleton<MainWindow>()
                .AddSingleton<DefaultViewport>()
                .AddSingleton<SceneMachine>()
                .AddTransient<InputManager>();

            _provider = collection.BuildServiceProvider();
            _window = _provider.GetService<MainWindow>();
        }

        /// <summary>
        /// Initializes the services needed to run the program.
        /// </summary>
        public void Initialize()
        {
            _window.InitWindow();
        }

        /// <summary>
        /// Starts the main window loop.
        /// </summary>
        public void Start()
        {
            _window.Show();
        }

        ///<inheritdoc/>
        public void Dispose()
        { }
    }
}
