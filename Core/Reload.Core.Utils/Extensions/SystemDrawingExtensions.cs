using System;
using System.Drawing;
using System.Numerics;

namespace Reload.Core.Utils.Extensions
{
    /// <summary>
    /// <see cref="System.Drawing"/> extension methods.
    /// </summary>
    public static class SystemDrawingExtensions
    {
        /// <summary>
        /// Byte unit for converting to Normalized (0 - 1) value.
        /// </summary>
        public const float ByteUnit = 1.0f / 255.0f;

        /// <summary>
        /// Converts <see cref="Color"/> struct to <see cref="Vector4"/>
        /// to be used as a shader uniform value.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>A Vector4.</returns>
        public static Vector4 ToVector4(this Color color)
        {
            Vector4 result;

            result.X = ByteUnit * color.R;
            result.Y = ByteUnit * color.G;
            result.Z = ByteUnit * color.B;
            result.W = ByteUnit * color.A;

            return result;
        }
    }
}
