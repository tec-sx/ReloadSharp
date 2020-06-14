using System;
using System.Numerics;

namespace Reload.Rendering.Camera
{
    public class OrtographicCamera
    {
        public Matrix4x4 ProjectionMatrix { get; private set; }
        public Matrix4x4 ViewMatrix { get; private set; }
        public Matrix4x4 ViewProjectionMatrix { get; private set; }

        private Vector3 _position;
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                RecalculateViewMatrix();
            }
        }

        private float _rotation;
        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = (float)(Math.PI / 180) * value;
                RecalculateViewMatrix();
            }
        }

        public OrtographicCamera(float width, float height)
        {
            ProjectionMatrix = Matrix4x4.CreateOrthographic(width, height, -1.0f, 1.0f);
            ViewMatrix = Matrix4x4.Identity;
            ViewProjectionMatrix = ProjectionMatrix * ViewMatrix;
        }

        private void RecalculateViewMatrix()
        {
            var transform = Matrix4x4.CreateTranslation(_position) * Matrix4x4.CreateRotationZ(_rotation);
            Matrix4x4.Invert(transform, out var viewMatrix);

            ViewMatrix = viewMatrix;
            ViewProjectionMatrix = ProjectionMatrix * ViewMatrix;
        }
    }
}
