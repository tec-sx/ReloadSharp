using FluentAssertions;
using Reload.Core.Math3D.Vertices;
using Xunit;

namespace Reload.Core.Math3D.Tests.Vertices.IndexTests
{
    public class IndexTests
    {
        [Fact]
        public void Index_EqualsOverrideTests()
        {
            // Arrange
            Index index = DataHelper.CreateWithDefaultValues();
            Index indexWithSameValues = DataHelper.CreateWithDefaultValues();
            Index indexWithDiferentValues = DataHelper.CreateWithDifferentValues();

            // Act
            bool indicesAreEqual = index == indexWithSameValues;
            bool indicesAreDifferent = index == indexWithDiferentValues;

            // Assert
            indicesAreEqual.Should().Be(true);
            indicesAreDifferent.Should().Be(false);
        }
    }
}
