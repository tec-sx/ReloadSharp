namespace Reload.Core.Commands
{
    public abstract class Command
    {
        public InputType Type { get; }

        protected Command(InputType type)
        {
            Type = type;
        }
    }
}