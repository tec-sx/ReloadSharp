namespace Reload
{
    using Engine.Core;
    using Engine.Scene;
    using Scenes;

    public class Reload : GameBase
    {
        protected override void OnInitialize()
        {
        }

        protected override void AddScenes()
        {
            var introScreen = sceneManager.CreateScene<IntroScene>();
            sceneManager.ActiveScene = introScreen;
        }

        protected override void OnDispose()
        {
        }
    }
}