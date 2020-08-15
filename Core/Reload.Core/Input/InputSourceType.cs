using System.ComponentModel;

namespace Reload.Core.Input
{
    public enum InputSourceType
    {
        [Description("No input device")]
        None = 0,

        [Description("Keyboard")]
        Keyboard,

        [Description("Mouse")]
        Mouse,

        [Description("Game pad")]
        GamePad,

        [Description("Touch screen")]
        ToushScreen
    }
}
