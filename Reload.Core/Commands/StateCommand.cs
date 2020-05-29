namespace Reload.Core.Commands
{
    public abstract class StateCommand : Command
    {
        public StateType CurrentState;

        protected StateCommand()
        {
            CurrentState = StateType.Released;
        }
    }

    public enum StateType
    {
        Pressed,
        Released
    }
}