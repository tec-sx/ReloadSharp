namespace Engine.Scene
{
    using Engine.Scene.Enumerations;
    using System;

    public interface IScene : IDisposable
    {
        SceneState State { get; }
        IScene NextScene { get; set; }
        IScene PrevScene { get; set; }
        ISceneManager Manager { get; set; }

        abstract void OnEnter();
        abstract void OnLeave();
        abstract void OnUpdate(double deltaTime);
        abstract void OnRender(double deltaTime);

        void Run();
        void Update(double deltaTime);
        void Render(double deltaTime);
    }
}
