namespace Reload.Core.Commands
{
    /// <summary>
    /// A state command class that keeps a
    /// <seealso cref="CurrentState"/> value to check
    /// whether to execute te command or not.
    /// </summary>
    public abstract class StateCommand : Command
    {
        public StateType CurrentState;


        /// <summary>
        /// Initializes a new instance of the <see cref="StateCommand"/> class
        /// with default <seealso cref="CurrentState"/> set to 
        /// <seealso cref="StateType.Released"/>
        /// </summary>
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