namespace Reload.Gameplay.Entities
{
    using System;
    using Reload.Core.Commands;

    public abstract class Actor
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }

        public ActorState ActiveState = ActorState.Idle;
        public ActorState PreviousState = ActorState.Idle;

        public virtual void Jump()
        {
            Console.WriteLine("Jump");
        }

        public virtual void Walk(StateType state)
        {
            Console.WriteLine("Walk");
        }

        public virtual void Run(StateType state)
        {
            Console.WriteLine("Run");
        }

        public virtual void Idle()
        {
            Console.WriteLine("Idle");
        }

    }

    public enum ActorState
    {
        Idle,
        Walking,
        Running
    }

}
