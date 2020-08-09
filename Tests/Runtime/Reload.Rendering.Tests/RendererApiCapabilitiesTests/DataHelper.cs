namespace Reload.Rendering.Tests.RendererApiCapabilitiesTests
{
    public static class DataHelper
    {
        public static RendererBackendCapabilities CreateWithDefaultValues()
        {
            return new RendererBackendCapabilities(
                vendor: "Kronos",
                renderer: "OpenGL",
                version: "4.5",
                maxSamples: 200,
                maxAnisotropy: 200.0f,
                maxTextureUnits: 200);
        }

        public static RendererBackendCapabilities CreateWithDifferentValues()
        {
            return new RendererBackendCapabilities(
                vendor: "Microsoft",
                renderer: "DirectX",
                version: "11",
                maxSamples: 200,
                maxAnisotropy: 200.0f,
                maxTextureUnits: 200);
        }
    }
}
