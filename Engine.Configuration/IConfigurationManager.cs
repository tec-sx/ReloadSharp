namespace Engine.Configuration
{
    using Engine.AssetPipeline;
    using Engine.Graphics;

    public interface IConfigurationManager
    {
        DisplayConfiguration CreateDisplayConfiguration();
        AssetsConfiguration CreateAssetsConfiguration();
    }
}
