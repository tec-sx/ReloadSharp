namespace ReloadGame.Scenes
{
    using Reload.AssetPipeline.Audio.Models;
    using Reload.Scene;
    using Reload.Input;
    using global::ReloadGame.Characters;
    using Scenes.Commands;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class IntroScene : SceneBase
    {
        private IMusic _bgMusicStream;
        private Player player = new Player();
        //public MoveDirection moveDirection;
        //public MoveStatus moveStatus;

        public override void OnEnter()
        {
            _bgMusicStream = Manager.Assets.LoadMusic("Intro");

            var mainContext = new InputMappingContext();

            mainContext.MapKeyToCommand(0, Key.Space, new JumpCommand());
            mainContext.MapKeyToCommand(0, Key.W, new WalkCommand());
            mainContext.MapKeyToCommand(0, Key.ShiftLeft, new RunCommand());

            var contexts = new Dictionary<string, InputMappingContext>
            {
                {"main", mainContext }
            };

            Manager.Input.Handler.LoadContexts(contexts);
            Manager.Input.Handler.PushActiveContext("main");

            Manager.Input.Handler.FireActionCommand += player.HandleActionCommand;
            Manager.Input.Handler.FireStateCommand += player.HandleStateCommand;
            Manager.Input.Handler.FireRangeCommand += player.HandleRangeCommand;
            //_bgMusicStream.Play();
        }

        public override void OnLeave()
        {
            _bgMusicStream.Stop();
        }

        public override void OnUpdate(double deltaTime)
        {
            Manager.Input.Update();
        }

        public override void OnRender(double deltaTime)
        {
        }
    }
}