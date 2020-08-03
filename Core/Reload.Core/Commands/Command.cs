namespace Reload.Core.Commands
{
    /// <summary>
    /// An abstract used class for implementing the 'Command pattern'.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public abstract void Execute(double deltaTime);
    }
}