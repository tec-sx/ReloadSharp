using System.ComponentModel;

namespace Reload.Core.Graphics
{
    public enum GraphicsAPIType
    {
        [Description("No graphics")]
        None = 0,

        [Description("OpenGL")]
        OpenGL,

        [Description("Vulkan")]
        Vulkan,

        [Description("DirectX")]
        DirectX,

        [Description("Metal")]
        Metal,

        [Description("Custom graphics backend")]
        Custom
    }
}
