using Reload.Core.Game;

namespace Reload.Core
{
    public interface IPlatformOS
    {
        /// <summary>
        /// Builds the game for the selected OS platform.
        /// </summary>
        /// <returns>A GameSystem.</returns>
        GameSystem BuildForPlatform<T>() where T : GameSystem, new();
    }
}
