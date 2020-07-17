using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Silk.NET.OpenGL;
using Reload.Platform.Graphics.OpenGl;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Common;

namespace Reload.Rendering.Tests
{
    public class MaterialTests
    {
        ShaderProgram _squareShader;

        public MaterialTests()
        {
            var window = Window.Create(WindowOptions.Default);
            
            //new GlContext();

        }

        [Fact]
        public void Material_SetColor_Success()
        {

            
        }
    }
}
