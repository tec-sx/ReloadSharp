using System.Runtime.CompilerServices;

namespace Reload.Core
{
    /// <summary>
    /// An abstract bridge factory.
    /// </summary>
    public abstract class BridgeFactory<T>
    {
        private static T _factoryImplementation;

        /// <summary>
        /// Returns the implementation of the factory.
        /// The name is intentionaly Create because it
        /// connects well with the factory classes methods.
        /// </summary>
        /// <returns>A T.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T Create() => _factoryImplementation;

        /// <summary>
        /// Prevents a default instance of the <see cref="BridgeFactory"/> class from being created.
        /// </summary>
        private BridgeFactory()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BridgeFactory"/> class.
        /// </summary>
        /// <param name="factoryImplementation">The factory implementation.</param>
        public BridgeFactory(T factoryImplementation)
        {
            _factoryImplementation = factoryImplementation;
        }
    }
}
