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

            var squareShaders = new Dictionary<ShaderType, string>
            {
                { ShaderType.VertexShader, "square.vert" },
                { ShaderType.FragmentShader, "square.frag" }
            };

            _squareShader = ShaderProgram.Create(squareShaders, null);
        }

        [Fact]
        public void Material_SetColor_Success()
        {

            var material = new Material();
        }
    }
}
