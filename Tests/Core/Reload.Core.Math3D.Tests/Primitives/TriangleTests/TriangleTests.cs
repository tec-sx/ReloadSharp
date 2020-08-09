using FluentAssertions;
using Reload.Core.Math3D.Primitives;
using Xunit;

namespace Reload.Core.Math3D.Tests.Primitives.TriangleTests
{
    public class TriangleTests
    {
        [Fact]
        public void Triangle_EqualsOverrideTests()
        {
            // Arrange
            Triangle triangle = DataHelper.CreateWithDefaultValues();
            Triangle triangleWithSameValues = DataHelper.CreateWithDefaultValues();
            Triangle triangleWithDiferentValues = DataHelper.CreateWithDifferentValues();

            // Act
            bool trianglesAreEqual = triangle == triangleWithSameValues;
            bool trianglesAreDifferent = triangle == triangleWithDiferentValues;

            // Assert
            trianglesAreEqual.Should().Be(true);
            trianglesAreDifferent.Should().Be(false);
        }
    }
}
