namespace Reload.Core.Commands
{
    public abstract class ActionCommand<T> : Command
    {
        protected ActionCommand() : base(InputType.ActionPress)
        {
        }

        public abstract void Execute(T targetObject);
    }
}