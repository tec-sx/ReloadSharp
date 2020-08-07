using System.Drawing;
using System.Numerics;

namespace Reload.Core.Utils.Extensions
{
    public static class SystemDrawingExtensions
    {
        /// <summary>
        /// Converts <see cref="Color"/> struct to <see cref="Vector4"/>
        /// to be used as a shader uniform value.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>A Vector4.</returns>
        public static Vector4 ToVector4(this Color color)
        {
            Vector4 result;

            result.X = color.R;
            result.Y = color.G;
            result.Z = color.B;
            result.W = color.A;

            return result;
        }
    }
}
