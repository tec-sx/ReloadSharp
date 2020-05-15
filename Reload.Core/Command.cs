namespace Reload.Core
{
    public abstract class Command
    {
        public abstract void Execute(Actor actor);
        public abstract CommandType Type { get; }
    }
}
