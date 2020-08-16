using Reload.Core.Exceptions;

namespace Reload.Core.Audio.Buffers
{
    /// <summary>
    /// An audio buffer factory used to abstract the creation of buffers
    /// from their implementation.
    /// </summary>
    public abstract class AudioFactory : BridgeFactory<AudioFactoryImplementation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFactory"/> class.
        /// </summary>
        /// <param name="factoryImplementation">The factory implementation.</param>
        protected AudioFactory(AudioFactoryImplementation factoryImplementation) 
            : base(factoryImplementation)
        { }
    }
}
