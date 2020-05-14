namespace ReloadGame.Scenes
{
    using Engine.AssetPipeline.Audio.Models;
    using Engine.Scene;
    using System;
    using System.Drawing;

    public class IntroScene : SceneBase
    {
        private IMusic _bgMusicStream;

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
            //_bgMusicStream.Play();
        }

        public override void OnLeave()
        {
            _bgMusicStream.Stop();
        }

        public override void OnUpdate(double deltaTime)
        {
            //if (moveStatus != MoveStatus.Stop)
            //{
            //    Console.WriteLine($"Direction: {moveDirection}, Status: {moveStatus}");
            //}
        }

        public override void OnRender(double deltaTime)
        {
        }
    }
}