
using SilkNetWindowBorder = Silk.NET.Windowing.Common.WindowBorder;
using ReloadWindowBorder = Reload.Core.Windowing.WindowBorder;
using Reload.Core.Exceptions;

namespace Reload.Platform.Windowing.GLFW.Extensions
{
    /// <summary>
    /// The display configuration extensions.
    /// </summary>
    public static class DisplayConfigurationExtensions
    {
        /// <summary>
        /// Converts reload window border enum value to Silk.Net window border enum value.
        /// </summary>
        /// <param name="windowBorder">The window border.</param>
        /// <returns>A SilkNetWindowBorder.</returns>
        public static SilkNetWindowBorder ToSilkNetWindowBorder(this ReloadWindowBorder windowBorder)
        {
            return windowBorder switch
            {
                ReloadWindowBorder.Resizable => SilkNetWindowBorder.Resizable,
                ReloadWindowBorder.Fixed => SilkNetWindowBorder.Fixed,
                ReloadWindowBorder.Hidden => SilkNetWindowBorder.Hidden,
                _ => throw new ReloadInvalidEnumArgumentException()
            };
        }
    }
}
