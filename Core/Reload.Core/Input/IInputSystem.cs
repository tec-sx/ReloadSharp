using Reload.Core.Game;

namespace Reload.Core.Input
{
    public interface IInputSystem : ICoreSystem
    {
        /// <summary>
        /// Gets the input source the system is listening to.
        /// </summary>
        InputSourceType Source { get; }
    }
}
