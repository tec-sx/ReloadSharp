namespace Engine.Scene
{
    using Engine.AssetPipeline;
    using System;

    public interface ISceneManager
    {
        event Action ExitGame;

        public IAssetsManager Assets { get; }
        public IScene ActiveScene { get; set; }

        IScene MoveToNextScreen();
        IScene MoveToPrevScreen();
        void Update(double deltaTime);
        void Render(double deltaTime);
        IScene CreateScene<T>() where T : IScene, new();
    }
}
