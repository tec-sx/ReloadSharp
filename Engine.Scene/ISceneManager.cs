using Reload.AssetPipeline;

namespace Reload.Scene
{
    using Reload.AssetPipeline;
    using Reload.Input;
    using System;

    public interface ISceneManager
    {
        event Action ExitProgram;

        InputManager Input { get; }
        public IAssetsManager Assets { get; }
        public IScene ActiveScene { get; set; }

        IScene MoveToNextScreen();
        IScene MoveToPrevScreen();

        /// <summary>
        /// Run the scene manager and the active scene.
        /// </summary>
        void Run();
        void Update(double deltaTime);
        void Render(double deltaTime);
        IScene AddScene<T>() where T : IScene, new();
    }
}
