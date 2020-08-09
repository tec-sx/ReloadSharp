using FluentAssertions;
using Reload.Core.Math3D.Vertices;
using System;
using Xunit;

namespace Reload.Core.Math3D.Tests.Vertices.AnimatedVertexTests
{
    public class AnimatedVertexTests
    {
        [Fact]
        public void AnimatedVertex_EqualsOverrideTests()
        {
            // Arrange
            AnimatedVertex vertex = DataHelper.CreateWithDefaultValues();
            AnimatedVertex vertexWithSameValues = DataHelper.CreateWithDefaultValues();
            AnimatedVertex vertexWithDiferentValues = DataHelper.CreateWithDifferentValues();

            // Act
            bool verticesAreEqual = vertex == vertexWithSameValues;
            bool verticesAreDifferent = vertex == vertexWithDiferentValues;

            // Assert
            verticesAreEqual.Should().Be(true);
            verticesAreDifferent.Should().Be(false);
        }

        [Fact]
        public void AnimatedVertex_AddBoneData_ShouldNotThrowExceptionIfExeedingDataSize()
        {
            // Arrange
            AnimatedVertex vertex = DataHelper.CreateWithDefaultValues();
            vertex.AddBoneData(1, 1.5f);
            vertex.AddBoneData(2, 2.5f);
            vertex.AddBoneData(3, 3.5f);
            vertex.AddBoneData(4, 4.5f);

            // Act
            Action act = () => vertex.AddBoneData(5, 5.5f);

            // Assert
            act.Should().NotThrow<Exception>();
        }
    }
}
