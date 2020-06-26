using System;
using System.Numerics;

namespace Reload.Rendering.Camera
{
    public class OrtographicCamera
    {
        private Matrix4x4 _projectionMatrix;

        private Matrix4x4 _viewMatrix;

        private Matrix4x4 _viewProjectionMatrix;

        public Matrix4x4 ViewProjectionMatrix => _viewProjectionMatrix;
        public Matrix4x4 ViewMatrix => _viewMatrix;
        public Matrix4x4 ProjectionMatrix => _projectionMatrix;

        public Vector3 Position;
        public float Rotation;

        public OrtographicCamera(float width, float height)
        {
            _projectionMatrix = Matrix4x4.CreateOrthographic(width, height, -10.0f, 10.0f);
            _viewMatrix = Matrix4x4.Identity;
            _viewProjectionMatrix = _projectionMatrix * _viewMatrix;

            Position = Vector3.Zero;
            Rotation = 0;
        }

        public void SetProjection(float width, float height)
        {
            _projectionMatrix = Matrix4x4.CreateOrthographic(width, height, -1.0f, 1.0f);
            _viewProjectionMatrix = _projectionMatrix * _viewMatrix;
        }

        public void RecalculateViewMatrix()
        {
            var transform = Matrix4x4.CreateTranslation(Position) *
                            Matrix4x4.CreateRotationZ((float)(Rotation * Math.PI));

            Matrix4x4.Invert(transform, out _viewMatrix);
            _viewProjectionMatrix = _projectionMatrix * _viewMatrix;
        }
    }
}
