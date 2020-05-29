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

        public override void Walk(StateType state)
        {
            Console.Write("Player->");

            if (state == StateType.Pressed)
            {
                IsIdle = false;

                if (RunningIsHeld)
                {
                    base.Run(state);
                }
                else
                {
                    base.Walk(state);
                }
            }
            else
            {
                base.Idle();
                IsIdle = true;
            }
        }

        public override void Run(StateType state)
        {
            Console.Write("Player->");

            if (state == StateType.Pressed)
            {
                RunningIsHeld = true;

                if (!IsIdle)
                {
                    base.Run(state);
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
                    base.Walk(state);
                }
                else
                {
                    base.Idle();
                }
            }
        }
    }
}
