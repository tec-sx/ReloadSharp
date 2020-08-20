using Reload.Core.Graphics;

namespace Reload.Core.Tests.Fakes
{
    internal class GraphicsAPIFake : GraphicsAPI
    {
        public override void Initialize()
        { }

        public override void ShutDown()
        { }

        protected override void Dispose(bool disposing)
        { }

        public GraphicsAPIFake WithBufferFactoryImplementation()
        {
            BufferFactory = new BufferfactoryFake();
            return this;
        }

        public GraphicsAPIFake WithoutBufferFactoryImplementation()
        {
            BufferFactory = null;
            return this;
        }

        public GraphicsAPIFake WithShaderFactoryImplementation()
        {
            ShaderFactory = new ShaderFactoryFake();
            return this;
        }

        public GraphicsAPIFake WithoutShaderFactoryImplementation()
        {
            ShaderFactory = null;
            return this;
        }

        public GraphicsAPIFake WithTextureFactoryImplementation()
        {
            TextureFactory = new TextureFactoryFake();
            return this;
        }

        public GraphicsAPIFake WithoutTextureFactoryImplementation()
        {
            TextureFactory = null;
            return this;
        }
    }
}
