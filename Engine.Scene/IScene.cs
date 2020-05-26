using Reload.Scene.Enumerations;
using Reload.Scene.Layers;

namespace Reload.Scene
{
    using Reload.Scene.Enumerations;
    using System;

    public interface IScene
    {
        /// <summary>
        /// Event fired when the scene state is changed.
        /// </summary>
        event Action<SceneState> SceneStateChange;

        IScene NextScene { get; set; }
        IScene PrevScene { get; set; }

        LayerStack Layers { get; set; }
        SceneManager SceneManager { get; set; }

        abstract void OnEnter();
        abstract void OnLeave();
        abstract void OnUpdate(double deltaTime);
        abstract void OnRender(double deltaTime);

        void Run();

        /// <summary>
        /// If screen is running, calls <see cref="OnUpdate(double)"/>
        /// for current screen and then updates all layers.
        /// </summary>
        /// <param name="deltaTime"></param>
        void Update(double deltaTime);

        /// <summary>
        /// If screen is running, calls <see cref="OnRender(double)"/>
        /// </summary>
        /// <param name="deltaTime"></param>
        void Render(double deltaTime);
        void CleanResources();

        /// <summary>
        /// Pause the scene.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resume paused scene.
        /// </summary>
        void Resume();
        /// <summary>
        /// Change the scene state and fire <see cref="SceneStateChange"/> event.
        /// </summary>
        /// <param name="state"></param>
        void ChangeSceneState(SceneState state);
    }
}
