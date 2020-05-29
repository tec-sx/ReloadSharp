namespace Reload.Core.Commands
{
    public abstract class RangeCommand<T> : Command
    {
        protected RangeCommand() : base(InputType.Range)
        { }

        public abstract void Execute(T targetObject, int range);
    }
}