namespace Reload.Configuration
{
    using Reload.AssetPipeline;
    using Reload.Graphics;
    using Reload.Input.Configuration;

    public interface IConfigurationManager
    {
        DisplayConfiguration CreateDisplayConfiguration();
        AssetsConfiguration CreateAssetsConfiguration();
        InputConfiguration CreateKeyboardConfiguration();
    }
}
