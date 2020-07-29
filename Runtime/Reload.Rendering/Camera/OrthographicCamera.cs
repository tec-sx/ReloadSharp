namespace Reload.Rendering.Camera
{
    using System.Numerics;
    using System;

    public class OrthographicCamera
    {
        private Matrix4x4 _projectionMatrix;

        private Matrix4x4 _viewMatrix;

        private Matrix4x4 _viewProjectionMatrix;

        public Matrix4x4 ViewProjectionMatrix => _viewProjectionMatrix;
        public Matrix4x4 ViewMatrix => _viewMatrix;
        public Matrix4x4 ProjectionMatrix => _projectionMatrix;

        public Vector3 Position;
        public float Rotation;

        public OrthographicCamera(float width, float height)
        {
            _projectionMatrix = Matrix4x4.CreateOrthographic(width, height, 0.01f, 100.0f);
            _viewMatrix = Matrix4x4.Identity;
            _viewProjectionMatrix = _projectionMatrix * _viewMatrix;

            Position = Vector3.Zero;
            Rotation = 0;
        }

        public void SetProjection(float width, float height, float nearPane, float farPane)
        {
            _projectionMatrix = Matrix4x4.CreateOrthographic(width, height, nearPane, farPane);
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
