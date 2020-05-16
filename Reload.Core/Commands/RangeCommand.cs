namespace Reload.Core.Commands
{
    public abstract class RangeCommand : Command
    {
        protected RangeCommand() : base(InputType.Range)
        { }

        public abstract void Execute(Actor actor, int range);
    }
}