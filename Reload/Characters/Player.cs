using Reload.Core.Commands;

namespace ReloadGame.Characters
{
    using System;
    using Reload.Core;
    using Reload.Scene;

    public class Player : Actor
    {
        public bool IsIdle { get; set; }
        public bool RunningIsHeld { get; set; }

        public Player()
        {
            IsIdle = true;
            RunningIsHeld = false;
            Name = "Ivan";
        }

        public override void Jump()
        {
            Console.Write("Player->");
            base.Jump();
        }

        public override void Walk(bool state)
        {
            Console.Write("Player->");

            if (state)
            {
                IsIdle = false;

                if (RunningIsHeld)
                {
                    base.Run(true);
                }
                else
                {
                    base.Walk(true);
                }
            }
            else
            {
                base.Idle();
                IsIdle = true;
            }
        }

        public override void Run(bool state)
        {
            Console.Write("Player->");

            if (state)
            {
                RunningIsHeld = true;

                if (!IsIdle)
                {
                    base.Run(true);
                }
                else
                {
                    base.Idle();
                }
            }
            else
            {
                RunningIsHeld = false;

                if (!IsIdle)
                {
                    base.Walk(true);
                }
                else
                {
                    base.Idle();
                }
            }
        }
    }
}
