using Microsoft.Extensions.DependencyInjection;
using Reload.Editor.Input;
using Reload.Editor.Platform;
using Reload.Scenes;
using SpaceVIL.Common;

namespace Reload.Editor
{
    public class GameEditor
    {

        private ServiceProvider _provider;
        private MainWindow _window;

        public GameEditor()
        {
            CommonService.InitSpaceVILComponents();

            ServiceCollection collection = new ServiceCollection();

            collection
                .AddSingleton<OpenGl>()
                .AddSingleton<MainWindow>()
                .AddSingleton<DefaultViewport>()
                .AddSingleton<SceneMachine>()
                .AddSingleton<InputManager>();

            _provider = collection.BuildServiceProvider();
        }

        public void Initialize()
        {
            _window = _provider.GetService<MainWindow>();
            _window.InitWindow();
        }

        public void Start()
        {
            _window.Show();
        }

        public void ShutDown()
        { }
    }
}
