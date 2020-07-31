using Microsoft.Extensions.DependencyInjection;
using Reload.Scenes;
using SpaceVIL.Common;

namespace Reload.Editor
{
    public class GameEditor
    {

        private ServiceProvider _provider;
        private MainWindow _window;
        private OpenGlViewport _viewport;

        public GameEditor()
        {
            CommonService.InitSpaceVILComponents();

            ServiceCollection collection = new ServiceCollection();

            collection
                .AddSingleton<MainWindow>()
                .AddSingleton<OpenGlViewport>()
                .AddSingleton<SceneMachine>();

            _provider = collection.BuildServiceProvider();
        }

        public void Initialize()
        {
            _window = _provider.GetService<MainWindow>();
            _viewport = _provider.GetService<OpenGlViewport>();

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
