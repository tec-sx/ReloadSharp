using Reload.Core.Math3D.Vertices;
using System.Numerics;

namespace Reload.Core.Math3D.Tests.Vertices.BoneInfoTests
{
    public static class DataHelper
    {
        public static BoneInfo CreateWithDefaultValues()
        {
            BoneInfo boneInfo = new BoneInfo();

            boneInfo.BoneOffset = Matrix4x4.Identity;
            boneInfo.FinalTransformation = Matrix4x4.Identity;

            return boneInfo;
        }

        public static BoneInfo CreateWithDifferentValues()
        {
            BoneInfo boneInfo = new BoneInfo();

            boneInfo.BoneOffset = Matrix4x4.Identity;
            boneInfo.FinalTransformation = Matrix4x4.CreateRotationX(2.5f);

            return boneInfo;
        }
    }
}
