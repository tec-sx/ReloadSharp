using NSubstitute;
using Reload.Core.Graphics.Rendering.Shaders;
using System.Collections.Generic;

namespace Reload.Core.Tests.Fakes
{
    public class ShaderFactoryFake : ShaderFactory
    {
        protected override ShaderProgram CreateShaderProgram(string fileName, List<string> attributes) => Substitute.For<ShaderProgram>();
    }
}
