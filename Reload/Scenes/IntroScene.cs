namespace ReloadGame.Scenes
{
    using Reload.Game.Scenes;
    using Characters;
    using Commands;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;
    using Reload.Engine.Input;

    public class IntroScene : Scene
    {
        private Player player = new Player();

        public override void OnEnter()
        {
            var mainContext = new InputMappingContext();

            mainContext.MapKeyToActionPress(0, Key.Space, new JumpCommand(player));
            mainContext.MapKeyToState(0, Key.W, new WalkCommand(player));
            mainContext.MapKeyToState(0, Key.ShiftLeft, new RunCommand(player));
            mainContext.MapKeyToActionPress(0, Key.P, new OpenMenuCommand(this));

            var contexts = new Dictionary<string, InputMappingContext>
            {
                {"main", mainContext }
            };

            SceneManager.Input.Handler.LoadContexts(contexts);
            SceneManager.Input.Handler.PushActiveContext("main");
        }

        public override void OnLeave()
        {
            SceneManager.Input.Handler.ClearContexts();
        }

        public override void OnUpdate(double deltaTime)
        {
        }

        public override void OnRender(double deltaTime)
        {
        }
    }
}