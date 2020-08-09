using Reload.Core.Math3D.Vertices;
using System.Numerics;

namespace Reload.Core.Math3D.Tests.Vertices.VertexTests
{
    public static class DataHelper
    {
        public static Vertex CreateWithDefaultValues()
        {
            return new Vertex(
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector2.Zero);
        }

        public static Vertex CreateWithDifferentValues()
        {
            return new Vertex(
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector2.UnitX);
        }
    }
}
