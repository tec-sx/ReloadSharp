using System.ComponentModel;

namespace Reload.Core.Graphics
{
    public enum WindowBackendType
    {
        [Description("No window")]
        None = 0,
        
        [Description("SDL2 window")]
        SDL2,

        [Description("GLFW window")]
        Glfw,

        [Description("Custom window")]
        Custom
    }
}
