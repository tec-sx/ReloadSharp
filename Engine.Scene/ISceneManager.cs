namespace Engine.Scene
{
    using Engine.AssetPipeline;
    using Reload.Input;
    using System;

    public interface ISceneManager
    {
        event Action ExitProgram;

        ReloadInput Input { get; }
        public IAssetsManager Assets { get; }
        public IScene ActiveScene { get; set; }

        IScene MoveToNextScreen();
        IScene MoveToPrevScreen();
        void Update(double deltaTime);
        void Render(double deltaTime);
        IScene AddScene<T>() where T : IScene, new();
    }
}
