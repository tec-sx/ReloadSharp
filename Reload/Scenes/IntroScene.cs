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
            //Manager.Event.KeyEvent.Jump += () => Console.WriteLine("Jump");
            //Manager.Event.KeyEvent.Move += (direction, status) =>
            //{
            //    moveDirection = direction;
            //    moveStatus = status;
            //};

            Console.WriteLine("Entering Intro.");
            _bgMusicStream = Manager.Assets.LoadMusic("Intro");

            var moveCommand = new MoveCommand();
            Manager.Input.Keyboard.RegisterCommand(Key.Space, new JumpCommand());
            Manager.Input.Keyboard.RegisterCommand(Key.W, moveCommand);
            Manager.Input.Keyboard.RegisterCommand(Key.A, moveCommand);
            Manager.Input.Keyboard.RegisterCommand(Key.D, moveCommand);

            Manager.Input.CommandFired += player.HandleCommand;
            //_bgMusicStream.Play();
        }

        public override void OnLeave()
        {
            _bgMusicStream.Stop();
        }

        public override void OnUpdate(double deltaTime)
        {
        }

        public override void OnRender(double deltaTime)
        {
        }
    }
}