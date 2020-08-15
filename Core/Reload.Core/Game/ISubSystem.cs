namespace Reload.Core.Game
{
    public interface ISubSystem
    {
        /// <summary>
        /// Initializes the sub system.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Shuts down the sub system.
        /// </summary>
        void ShutDown();
    }
}
