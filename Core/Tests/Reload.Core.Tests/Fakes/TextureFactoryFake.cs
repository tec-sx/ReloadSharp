using NSubstitute;
using Reload.Core.Graphics.Rendering.Textures;

namespace Reload.Core.Tests.Fakes
{
    public class TextureFactoryFake : TextureFactory
    {
        protected override Texture2D CreateBlankTexture2D(uint width, uint height) => Substitute.For<Texture2D>();

        protected override TextureCube CreateBlankTextureCube(TextureFormat format, uint width, uint height) => Substitute.For<TextureCube>();

        protected override Texture2D CreateTexture2DFromFile(string path) => Substitute.For<Texture2D>();

        protected override TextureCube CreateTextureCubeFromFile(string path) => Substitute.For<TextureCube>();
    }
}
