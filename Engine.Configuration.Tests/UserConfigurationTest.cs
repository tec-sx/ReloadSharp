using Reload.Configuration.Extensions;
using System.Drawing;
using System.IO;
using Xunit;
using Xunit.Extensions.Ordering;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Reload.Configuration.Tests
{
    public class UserConfigurationTest
    {
        [Fact, Order(1)]
        public void SetOptimalConfigurationSucces()
        {
            // TODO: Implement tests for 'set optimal configuration logic' success.
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact, Order(2)]
        public void SaveSettingsSuccess()
        {
            #region Arrange
            var userConfiguration = new UserConfiguration();
            #endregion

            #region Act
            userConfiguration.DisplayResolution = new Point(640, 480);
            userConfiguration.Save();
            #endregion

            #region Assert
            Assert.True(File.Exists(userConfiguration.GetConfigurationFilePath()));
            #endregion
        }

        [Fact, Order(3)]
        public void LoadSettingsSuccess()
        {
            #region Arrange
            var userConfiguration = new UserConfiguration();
            #endregion

            #region Act
            userConfiguration.Load();
            #endregion

            #region Assert
            Assert.True(userConfiguration.DisplayResolution.X == 640);
            Assert.True(userConfiguration.DisplayResolution.Y == 480);
            #endregion
        }
    }
}
