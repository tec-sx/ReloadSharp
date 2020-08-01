using Reload.Editor.Scenes;
using Reload.Scenes;
using SpaceVIL;

namespace Reload.Editor.Extensions
{
    public static class SceneMachineExtensions
    {
        public static T AddSceneInViewport<T>(this SceneMachine sceneMachine, Prototype viewport) 
            where T : Scene, IViewportAttachable, new()
        {
            T scene = (T)sceneMachine.AddScene<T>();
            scene.ParentViewport = viewport;

            return scene;
        }
    }
}
