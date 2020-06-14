using System;
using System.Numerics;

namespace Reload.Rendering.Camera
{
    public class OrtographicCamera
    {
        public Matrix4x4 ProjectionMatrix { get; }
        public Matrix4x4 ViewMatrix =>
                Matrix4x4.Identity *
                Matrix4x4.CreateFromQuaternion(Rotation) *
                Matrix4x4.CreateTranslation(Position);

        public Matrix4x4 ViewProjectionMatrix => ProjectionMatrix * ViewMatrix;

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public OrtographicCamera(float width, float height)
        {
            ProjectionMatrix = Matrix4x4.CreateOrthographic(width, height, -1.0f, 1.0f);
        }
    }
}
