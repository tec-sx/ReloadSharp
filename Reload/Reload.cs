namespace Reload
{
    using Engine.Core;
    using Scenes;

    public class Reload : GameBase
    {
        protected override void OnInitialize()
        {
        }

        protected override void AddScenes()
        {
            var introScreen = sceneManager.AddScene<IntroScene>();
            sceneManager.ActiveScene = introScreen;
        }

        protected override void OnCleanUp()
        {

        }
    }
}