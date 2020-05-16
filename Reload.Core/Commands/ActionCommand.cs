namespace Reload.Core.Commands
{
    public abstract class ActionCommand : Command
    {
        protected ActionCommand() : base(InputType.ActionPress)
        {
        }

        public abstract void Execute(Actor actor);
    }
}