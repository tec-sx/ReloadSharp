using FluentAssertions;
using Reload.Core.Math3D.Vertices;
using Xunit;

namespace Reload.Core.Math3D.Tests.Vertices.VertexTests
{
    public class VertexTests
    {
        [Fact]
        public void Vertex_EqualsOverrideTests()
        {
            // Arrange
            Vertex vertex = DataHelper.CreateWithDefaultValues();
            Vertex vertexWithSameValues = DataHelper.CreateWithDefaultValues();
            Vertex vertexWithDiferentValues = DataHelper.CreateWithDifferentValues();

            // Act
            bool verticesAreEqual = vertex == vertexWithSameValues;
            bool verticesAreDifferent = vertex == vertexWithDiferentValues;

            // Assert
            verticesAreEqual.Should().Be(true);
            verticesAreDifferent.Should().Be(false);
        }
    }
}
