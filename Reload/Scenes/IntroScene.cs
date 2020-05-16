namespace ReloadGame.Scenes
{
    using Engine.AssetPipeline.Audio.Models;
    using Engine.Scene;
    using ReloadGame.Characters;
    using Scenes.Commands;
    using Silk.NET.Input.Common;
    using System;

    public class IntroScene : SceneBase
    {
        private IMusic _bgMusicStream;
        private Player player = new Player();
        //public MoveDirection moveDirection;
        //public MoveStatus moveStatus;

        public override void OnEnter()
        {
            _bgMusicStream = Manager.Assets.LoadMusic("Intro");

            var moveCommand = new MoveCommand();
            Manager.Input.RegisterCommand(Key.Space, new JumpCommand());
            Manager.Input.RegisterCommand(Key.W, moveCommand);
            Manager.Input.RegisterCommand(Key.A, moveCommand);
            Manager.Input.RegisterCommand(Key.D, moveCommand);

            Manager.Input.FireCommand += player.HandleCommand;
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