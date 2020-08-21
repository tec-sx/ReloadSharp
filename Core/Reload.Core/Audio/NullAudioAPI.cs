using Reload.Core.Properties;
using Reload.Core.Utilities;

namespace Reload.Core.Audio
{
    /// <summary>
    /// The null implementation of the audio API. Used to be 
    /// able to run the game without audio.
    /// </summary>
    public class NullAudioAPI : AudioAPI
    {
        private readonly string _type = "Audio API";

        /// <inheritdoc/>
        public override void Initialize()
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullInstanceMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override void ShutDown()
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullInstanceMessage, _type);
#endif
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullInstanceMessage, _type);
#endif
        }
    }
}
