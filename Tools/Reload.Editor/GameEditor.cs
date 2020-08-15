using Microsoft.Extensions.DependencyInjection;
using Reload.Core.Game;
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
    public sealed class GameEditor: GameSystem, IDisposable
    {
        private readonly ServiceProvider _provider;
        private readonly MainWindow _window;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameEditor"/> class
        /// and sets up the DI provider and services.
        /// Should call <seealso cref="OnInitialize"/> and <seealso cref="Start"/>
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

        /// <inheritdoc/>
        public override void OnInitialize()
        {
            _window.InitWindow();
        }

        /// <inheritdoc/>
        public override void Run()
        {
            _window.Show();
        }

        /// <inheritdoc/>
        public override void OnShutDown()
        {
            
        }

        ///<inheritdoc/>
        public void Dispose()
        { }
    }
}
