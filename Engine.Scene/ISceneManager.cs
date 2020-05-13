namespace Engine.Scene
{
    using Engine.AssetPipeline;
    using Engine.Events;
    using System;

    public interface ISceneManager
    {
        event Action ExitProgram;

        public IEventManager Event { get; }
        public IAssetsManager Assets { get; }
        public IScene ActiveScene { get; set; }

        IScene MoveToNextScreen();
        IScene MoveToPrevScreen();
        void Update(double deltaTime);
        void Render(double deltaTime);
        IScene AddScene<T>() where T : IScene, new();
    }
}
