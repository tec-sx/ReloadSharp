using System.ComponentModel;

namespace Reload.Core.Graphics
{
    public enum GraphicsBackendType
    {
        [Description("No graphics")]
        None = 0,

        [Description("OpenGL")]
        OpenGL,

        [Description("Vulkan")]
        Vulkan,

        [Description("DirectX 11")]
        DirectX11,

        [Description("DirectX 12")]
        DirectX12,

        [Description("Metal")]
        Metal,

        [Description("Custom graphics backend")]
        Custom
    }
}
