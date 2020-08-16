namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// An graphic buffer factory used to abstract the creation of buffers
    /// from their implementation.
    /// </summary>
    public class BufferFactory : BridgeFactory<BufferFactoryImplementation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BufferFactory"/> class.
        /// </summary>
        /// <param name="factoryImplementation">The buffer factory implementation.</param>
        public BufferFactory(BufferFactoryImplementation factoryImplementation)
            : base(factoryImplementation)
        { }
    }
}
