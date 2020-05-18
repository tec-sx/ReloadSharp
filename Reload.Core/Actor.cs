using Reload.Core.Commands;

namespace Reload.Core
{
    using System;

    public abstract class Actor
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }

        public ActorState ActiveState = ActorState.Idle;
        public ActorState PreviousState = ActorState.Idle;

        public void HandleActionCommand(Command command)
        {
            ((ActionCommand)command).Execute(this);
        }

        public void HandleStateCommand(Command command, bool state)
        {
            ((StateCommand)command).Execute(this, state);
        }

        public void HandleRangeCommand(Command command, int range)
        {
            ((RangeCommand)command).Execute(this, range);
        }

        public virtual void Jump()
        {
            Console.WriteLine("Jump");
        }

        public virtual void Walk(bool state)
        {
            Console.WriteLine("Walk");
        }

        public virtual void Run(bool state)
        {
            Console.WriteLine("Run");
        }

        public virtual void Idle()
        {
            Console.WriteLine("Idle");
        }

    }
}
