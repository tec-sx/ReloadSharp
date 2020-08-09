using FluentAssertions;
using Reload.Core.Math3D.Vertices;
using Xunit;

namespace Reload.Core.Math3D.Tests.Vertices.BoneInfoTests
{
    public class BoneInfoTests
    {
        [Fact]
        public void BoneInfo_EqualsOverrideTests()
        {
            // Arrange
            BoneInfo boneInfo = DataHelper.CreateWithDefaultValues();
            BoneInfo boneInfoWithSameValues = DataHelper.CreateWithDefaultValues();
            BoneInfo boneInfoWithDiferentValues = DataHelper.CreateWithDifferentValues();

            // Act
            bool boneInfosAreEqual = boneInfo == boneInfoWithSameValues;
            bool boneInfosAreDifferent = boneInfo == boneInfoWithDiferentValues;

            // Assert
            boneInfosAreEqual.Should().Be(true);
            boneInfosAreDifferent.Should().Be(false);
        }
    }
}
