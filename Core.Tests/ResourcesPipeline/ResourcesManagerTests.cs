using System.Collections.Generic;
using Core.ResourcesPipeline.Shaders.Models;

namespace CoreTests.ResourcesPipeline
{
    using Core.Config;
    using Core.ResourcesPipeline;
    using Xunit;
    
    public class ResourcesManagerTests
    {
        [Fact]
        public void LoadGlShadersTest()
        {
            #region Arrange

            Configuration.LoadDefaultConfiguration();
            Configuration.Settings.Display.UseVulkan = false;
            
            #endregion

            #region Act
            
            var resourcesManager = new ResourcesManager();
            var shaderList = new List<string>
            {
                "main.vert",
                "main.frag",
                "main.geom"
            };
            
            resourcesManager.LoadShader(shaderList);

            #endregion

            #region Assert

            #endregion
        }
    }
}