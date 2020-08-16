namespace Reload.Core.Graphics.Rendering.Shaders
{
    /// <summary>
    /// The shader factory.
    /// </summary>
    public class ShaderFactory : BridgeFactory<ShaderFactoryImplementation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderFactory"/> class.
        /// </summary>
        /// <param name="factoryImplementation">The factory implementation.</param>
        public ShaderFactory(ShaderFactoryImplementation factoryImplementation) 
            : base(factoryImplementation)
        { }
    }
}
