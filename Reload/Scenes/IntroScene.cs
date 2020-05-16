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

            Manager.Input.Handler.RegisterKeyCommand(Manager.Input.Keyboards[0], Key.Space, new JumpCommand());

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