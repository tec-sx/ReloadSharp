namespace Reload.Core.Commands
{
    public abstract class StateCommand<T> : Command
    {
        protected StateCommand() : base(InputType.State)
        { }

        public abstract void Execute(T targetObject, bool state);
    }
}