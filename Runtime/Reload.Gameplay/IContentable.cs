namespace Reload.Gameplay
{
    /// <summary>
    /// An interface to load and unload asset.
    /// </summary>
    public interface IContentable
    {
        /// <summary>
        /// Loads the assets.
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Called when graphics resources need to be unloaded. Override this method to unload any game-specific graphics resources.
        /// </summary>
        void UnloadContent();
    }
}
