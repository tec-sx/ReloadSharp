namespace ReloadGame.Scenes
{
    using Reload.AssetPipeline.Audio.Models;
    using Reload.Scene;
    using Reload.Input;
    using Characters;
    using Commands;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;

    public class IntroScene : SceneBase
    {
        private Player player = new Player();
        //public MoveDirection moveDirection;
        //public MoveStatus moveStatus;

        public override void OnEnter()
        {
            var mainContext = new InputMappingContext();

            mainContext.MapKeyToCommand(0, Key.Space, new JumpCommand());
            mainContext.MapKeyToCommand(0, Key.W, new WalkCommand());
            mainContext.MapKeyToCommand(0, Key.ShiftLeft, new RunCommand());

            var contexts = new Dictionary<string, InputMappingContext>
            {
                {"main", mainContext }
            };

            SceneManager.Input.Handler.LoadContexts(contexts);
            SceneManager.Input.Handler.PushActiveContext("main");

            SceneManager.Input.Handler.FireActionCommand += player.HandleActionCommand;
            SceneManager.Input.Handler.FireStateCommand += player.HandleStateCommand;
            SceneManager.Input.Handler.FireRangeCommand += player.HandleRangeCommand;
        }

        public override void OnLeave()
        {
            SceneManager.Input.Handler.FireActionCommand -= player.HandleActionCommand;
            SceneManager.Input.Handler.FireStateCommand -= player.HandleStateCommand;
            SceneManager.Input.Handler.FireRangeCommand -= player.HandleRangeCommand;
        }

        public override void OnUpdate(double deltaTime)
        {
            SceneManager.Input.Update();
        }

        public override void OnRender(double deltaTime)
        {
        }
    }
}