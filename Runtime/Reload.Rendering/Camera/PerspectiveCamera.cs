namespace Reload.Rendering.Camera
{
    using Reload.Core.Utils;
    using System;
    using System.Numerics;

    public class PerspectiveCamera
    {
        private Vector3 _up;
        private Vector3 _right;
        private Vector3 _forward;
        
        private Matrix4x4 _modelMatrix;
        private Matrix4x4 _projectionMatrix;
        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _modelViewProjectionMatrix;

        private float _initialFoV;

        private Vector3 _position;
        public float HorizontalAngle { get; set; }
        public float VerticalAngle { get; set; }

        public Vector3 Up => _up;
        public Vector3 Right => _right;
        public Vector3 Forward => _forward;

        public Matrix4x4 ViewMatrix => _viewMatrix;
        public Matrix4x4 ProjectionMatrix => _projectionMatrix;
        public Matrix4x4 ModelViewProjectionMatrix => _modelViewProjectionMatrix;

        public PerspectiveCamera()
            : this(new Vector3(0.0f, 0.0f, 0.0f), -0.0f, 0.0f, 45.0f)
        { }

        public PerspectiveCamera(Vector3 position, float horizontalAngle, float verticalAngle, float initialFoV)
        {
            _modelMatrix = Matrix4x4.Identity;
            _position = position;
            HorizontalAngle = horizontalAngle;
            VerticalAngle = verticalAngle;
            _initialFoV = initialFoV;

            Update();
        }

        public void Update()
        {
            _forward = new Vector3(
                (float)(Math.Cos(VerticalAngle) * Math.Sin(HorizontalAngle)),
                (float)Math.Sin(HorizontalAngle),
                (float)(Math.Cos(VerticalAngle) * Math.Cos(HorizontalAngle)));

            _right = new Vector3(
                (float)Math.Sin(HorizontalAngle - Math.PI / 2.0f),
                0.0f,
                (float)Math.Cos(HorizontalAngle - Math.PI / 2.0f));

            _up = Vector3.Cross(_right, _forward);

            float fovInRadiants = ReloadMath.DegreesToRadiants(_initialFoV);

            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(fovInRadiants, 16 / 9, 0.001f, 10000.0f);
            _viewMatrix = Matrix4x4.CreateLookAt(_position, _position + _forward, _up);
            _modelViewProjectionMatrix = _projectionMatrix * _viewMatrix * _modelMatrix;
        }

        public void MoveCamera(Vector3 movementVector) => _position = movementVector;
    }
}
