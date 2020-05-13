namespace Engine.Configuration
{
    using Engine.AssetPipeline;
    using Engine.Graphics;
    using Reload.Input.Configuration;

    public interface IConfigurationManager
    {
        DisplayConfiguration CreateDisplayConfiguration();
        AssetsConfiguration CreateAssetsConfiguration();
        KeyboardConfiguration CreateKeyboardConfiguration();
        MouseConfiguration CreateMouseConfiguration();
    }
}
