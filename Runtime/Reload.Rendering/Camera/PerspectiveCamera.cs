namespace Reload.Rendering.Camera
{
    using Reload.Core.Utils;
    using System;
    using System.Drawing;
    using System.Numerics;

    public class PerspectiveCamera
    {
        private Vector3 _rotation;
        

        private bool _panning;
        private bool _rotating;

        private Vector2 _initialMousePosition;
        private Vector3 _initialFocalPoint;
        private Vector3 _initialRotation;

        private float _yaw;
        private float _pitch;
        private float _roll;

        public Size ViewportSize { get; set; }

        public Vector3 RightDirection => Vector3.Transform(Vector3.UnitX, Orientation);
        public Vector3 UpDirection => Vector3.Transform(Vector3.UnitY, Orientation);
        public Vector3 ForwardDirection => Vector3.Transform(Vector3.UnitZ, Orientation);
        private Vector3 Position => FocalPoint - ForwardDirection * Distance;

        public Vector3 FocalPoint { get; set; }
        public float Distance { get; set; }

        public Matrix4x4 ViewMatrix { get; private set; }
        public Matrix4x4 ProjectionMatrix { get; set; }
        public Matrix4x4 ViewProjectionMatrix => ProjectionMatrix * ViewMatrix;

        public float Exposure { get; private set; }
        public float RotationSpeed { get; private set; }

        private Quaternion Orientation => Quaternion.CreateFromYawPitchRoll(_yaw, _pitch, _roll);
        
        public PerspectiveCamera(Matrix4x4 projectionMatrix)
           : this()
        {
            ProjectionMatrix = projectionMatrix;
            _rotation = new Vector3(90.0f, 0.0f, 0.0f);
            FocalPoint = Vector3.Zero;

            Vector3 position = new Vector3( -5.0f, 5.0f, 5.0f );
            Distance = Vector3.Distance(position, FocalPoint);

            _yaw = 3.0f * MathF.PI / 4.0f;
            _pitch = MathF.PI / 4.0f;
            _roll = 0.0f;

            UpdateCameraView();
        }

        public void RecalculateProjectionViewMatrix(Matrix4x4 projectionMatrix)
        {
            ProjectionMatrix = projectionMatrix;
            UpdateCameraView();
        }

        public PerspectiveCamera()
        {
            Exposure = 0.8f;
            RotationSpeed = 0.8f;
        }

        public void Focus()
        {

        }

        private void UpdateCameraView()
        {
            Vector3 position = Position;
            Quaternion orientation = Orientation;
            
            _rotation = orientation.ToEulerAngles() * (180.0f / MathF.PI);
            
            var newViewMatrix = Matrix4x4.CreateTranslation(position) * Matrix4x4.CreateFromQuaternion(orientation);
            Matrix4x4.Invert(newViewMatrix, out var invertedViewMatrix);

            ViewMatrix = invertedViewMatrix;
        }

        public void OnUpdate(double deltaTime)
        {
            UpdateCameraView();
        }
    }
}
