using Reload.Core.Math3D.Vertices;

namespace Reload.Core.Math3D.Tests.Vertices.IndexTests
{
    public static class DataHelper
    {
        public static Index CreateWithDefaultValues()
        {
            return new Index(1, 2, 3);
        }

        public static Index CreateWithDifferentValues()
        {
            return new Index(4, 2, 3);
        }
    }
}
