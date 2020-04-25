namespace Reload.Scenes
{
    using System;
    using Engine.AssetPipeline.Audio.Models;
    using Engine.GamePlay;
    using Engine.AssetPipeline.Textures;
    using Engine.Scene;

    public class IntroScene : SceneBase
    {
        private IMusic _bgMusicStream;

        public override void OnEnter()
        {
            //Manager.PlayerAction.Jump += () => Console.WriteLine("Jump");
            //Manager.PlayerAction.Move += direction =>
            //{
            //    switch(direction)
            //    {
            //        case MoveDirection.UP:
            //            Console.WriteLine("Up");
            //            break;
            //        case MoveDirection.DOWN:
            //            Console.WriteLine("Down");
            //            break;
            //        case MoveDirection.LEFT:
            //            Console.WriteLine("Left");
            //            break;
            //        case MoveDirection.RIGHT:
            //            Console.WriteLine("Right");
            //            break;
            //    };
            //};
            _bgMusicStream = Manager.Assets.LoadMusic("Intro");
            _bgMusicStream.Play();
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