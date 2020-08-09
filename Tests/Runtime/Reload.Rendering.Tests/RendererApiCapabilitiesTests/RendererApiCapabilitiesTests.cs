using FluentAssertions;
using Xunit;

namespace Reload.Rendering.Tests.RendererApiCapabilitiesTests
{
    public class RendererApiCapabilitiesTests
    {
        [Fact]
        public void RendererApiCapabilities_EqualsOverrideTests()
        {
            // Arrange
            RendererBackendCapabilities renderer = DataHelper.CreateWithDefaultValues();
            RendererBackendCapabilities rendererWithSameValues = DataHelper.CreateWithDefaultValues();
            RendererBackendCapabilities rendererWithDiferentValues = DataHelper.CreateWithDifferentValues();

            // Act
            bool renderersAreEqual = renderer == rendererWithSameValues;
            bool renderersAreDifferent = renderer == rendererWithDiferentValues;

            // Assert
            renderersAreEqual.Should().Be(true);
            renderersAreDifferent.Should().Be(false);
        }
    }
}
