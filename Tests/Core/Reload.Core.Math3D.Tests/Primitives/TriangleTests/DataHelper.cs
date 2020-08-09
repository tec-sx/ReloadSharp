using Reload.Core.Math3D.Primitives;
using VertexTests = Reload.Core.Math3D.Tests.Vertices.VertexTests;

namespace Reload.Core.Math3D.Tests.Primitives.TriangleTests
{
    public static class DataHelper
    {
        public static Triangle CreateWithDefaultValues()
        {
            return new Triangle(
                VertexTests.DataHelper.CreateWithDefaultValues(),
                VertexTests.DataHelper.CreateWithDefaultValues(),
                VertexTests.DataHelper.CreateWithDefaultValues());
        }

        public static Triangle CreateWithDifferentValues()
        {
            return new Triangle(
            VertexTests.DataHelper.CreateWithDefaultValues(),
            VertexTests.DataHelper.CreateWithDefaultValues(),
            VertexTests.DataHelper.CreateWithDifferentValues());
        }
    }
}
