namespace Reload.Rendering.Tests
{
    using Reload.Rendering.Structures;
    using Xunit;
    using FluentAssertions;

    public class BufferLayoutTests
    {
        [Fact]
        public void BufferLayoutCalculationsTest()
        {
            var layout = new BufferLayout
            {
                new BufferElement(ShaderDataType.Float3, "a_Position"),
                new BufferElement(ShaderDataType.Float4, "a_Color"),
                new BufferElement(ShaderDataType.Float3, "a_Normal")
            };

            var firstElement = layout[0];
            var secondElement = layout[1];
            var thirdElement = layout[2];

            layout.Stride.Should().Be(40);

            firstElement.Offset.Should().Be(0);
            firstElement.Size.Should().Be(12);

            secondElement.Offset.Should().Be(12);
            secondElement.Size.Should().Be(16);

            thirdElement.Offset.Should().Be(28);
            thirdElement.Size.Should().Be(12);
        }
    }
}
