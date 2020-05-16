namespace Reload.Core.Commands
{
    public abstract class StateCommand : Command
    {
        protected StateCommand() : base(InputType.State)
        { }

        public abstract void Execute(Actor actor, bool state);
    }
}