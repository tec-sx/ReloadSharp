namespace Engine.Common.Models
{
    public interface ISubSystem
    {
        /// <summary>
        /// Start subsystem.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Shut down subsystem.
        /// </summary>
        void ShutDown();
    }
}
