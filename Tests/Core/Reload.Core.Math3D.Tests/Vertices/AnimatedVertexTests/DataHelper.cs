using Reload.Core.Math3D.Vertices;
using System.Numerics;

namespace Reload.Core.Math3D.Tests.Vertices.AnimatedVertexTests
{
    public static class DataHelper
    {
        public static AnimatedVertex CreateWithDefaultValues()
        {
            return new AnimatedVertex(
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector2.Zero);
        }

        public static AnimatedVertex CreateWithDifferentValues()
        {
            return new AnimatedVertex(
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector2.UnitX);
        }
    }
}
