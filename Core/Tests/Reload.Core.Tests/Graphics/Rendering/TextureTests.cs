using FluentAssertions;
using Reload.Core.Common;
using Reload.Core.Graphics.Rendering.Textures;
using Reload.Core.Graphics.Rendering.Textures.NullTextures;
using Reload.Core.Tests.Fakes;
using System;
using Xunit;

namespace Reload.Core.Tests.Graphics.Rendering
{
    public class TextureTests
    {
        [Fact]
        public void Texture2D_Constraints()
        {
            Type texture2D = typeof(Texture2D);

            texture2D
                .Should().BeAbstract()
                .And.Implement<ITexture>()
                .And.Implement<IBindable>()
                .And.Implement<IDisposable>()
                .And.HaveDefaultConstructor();
        }

        [Fact]
        public void TextureCube_Constraints()
        {
            Type texture2D = typeof(TextureCube);

            texture2D
                .Should().BeAbstract()
                .And.Implement<ITexture>()
                .And.Implement<IBindable>()
                .And.Implement<IDisposable>()
                .And.HaveDefaultConstructor();
        }

        [Fact]
        public void CreateTexture2D_FactoryNotImplemented_ReturnsNullTexture2D()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithoutTextureFactoryImplementation();

            // Act
            Func<Texture2D> createBlankTextureAct = () => Texture2D.CreateBlank(0, 0);
            Func<Texture2D> createTextureFromFileAct = () => Texture2D.CreateFromFile("filename");

            Texture2D blankTexture = createBlankTextureAct?.Invoke();
            Texture2D texture = createTextureFromFileAct?.Invoke();
            
            //Assert
            createBlankTextureAct.Should().NotThrow();
            createTextureFromFileAct.Should().NotThrow();

            blankTexture
                .Should().NotBeNull()
                .And.BeOfType<NullTexture2D>();

            texture
                .Should().NotBeNull()
                .And.BeOfType<NullTexture2D>();
        }

        [Fact]
        public void CreateTexture2D_FactoryImplemented_ReturnsTexture2D()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithTextureFactoryImplementation();

            // Act
            Func<Texture2D> createBlankTextureAct = () => Texture2D.CreateBlank(0, 0);
            Func<Texture2D> createTextureFromFileAct = () => Texture2D.CreateFromFile("filename");

            Texture2D blankTexture = createBlankTextureAct?.Invoke();
            Texture2D texture = createTextureFromFileAct?.Invoke();

            //Assert
            createBlankTextureAct.Should().NotThrow();
            createTextureFromFileAct.Should().NotThrow();

            blankTexture
                .Should().NotBeNull()
                .And.NotBeOfType<NullTexture2D>();

            texture
                .Should().NotBeNull()
                .And.NotBeOfType<NullTexture2D>();
        }

        [Fact]
        public void CreateTextureCube_FactoryNotImplemented_ReturnsNullTextureCube()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithoutTextureFactoryImplementation();

            // Act
            Func<TextureCube> createBlankTextureAct = () => TextureCube.CreateBlank(TextureFormat.None, 0, 0);
            Func<TextureCube> createTextureFromFileAct = () => TextureCube.CreateFromFile("filename");

            TextureCube blankTexture = createBlankTextureAct?.Invoke();
            TextureCube texture = createTextureFromFileAct?.Invoke();

            //Assert
            createBlankTextureAct.Should().NotThrow();
            createTextureFromFileAct.Should().NotThrow();

            blankTexture
                .Should().NotBeNull()
                .And.BeOfType<NullTextureCube>();
            
            texture
                .Should().NotBeNull()
                .And.BeOfType<NullTextureCube>();
        }

        [Fact]
        public void CreateTextureCube_FactoryImplemented_ReturnsTextureCube()
        {
            // Arrange
            GraphicsAPIFake graphicsApi = new GraphicsAPIFake().WithTextureFactoryImplementation();

            // Act
            Func<TextureCube> createBlankTextureAct = () => TextureCube.CreateBlank(TextureFormat.None, 0, 0);
            Func<TextureCube> createTextureFromFileAct = () => TextureCube.CreateFromFile("filename");

            TextureCube blankTexture = createBlankTextureAct?.Invoke();
            TextureCube texture = createTextureFromFileAct?.Invoke();

            //Assert
            createBlankTextureAct.Should().NotThrow();
            createTextureFromFileAct.Should().NotThrow();

            blankTexture
                .Should().NotBeNull()
                .And.NotBeOfType<NullTextureCube>();

            texture
                .Should().NotBeNull()
                .And.NotBeOfType<NullTextureCube>();
        }
    }
}
