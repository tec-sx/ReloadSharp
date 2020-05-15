namespace ReloadGame.Characters
{
    using System;
    using Reload.Core;

    public class Player : Actor
    {
        public override void Jump()
        {
            Console.Write("Player->");
            base.Jump();
        }
    }
}
