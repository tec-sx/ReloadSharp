namespace Reload.Rendering.Camera
{
    using Reload.Core.Commands;
    using System.Drawing;
    using System.Numerics;

    public class OrtographicCameraController
    {
        private float _aspectRatio;
        private float _zoomLevel;
        private bool _rotationIsEnabled;

        private Vector3 _cameraPosition;
        private float _cameraRotation;

        private float _cameraTranslationSpeed;
        private float _cameraRotationSpeed;
        private float _cameraScrollZoom;

        public OrtographicCamera Camera { get; }

        public ZoomCommand Zoom;
        public MoveLeftCommand MoveLeft;
        public MoveRightCommand MoveRight;
        public MoveUpCommand MoveUp;
        public MoveDownCommand MoveDown;
        public RotateLeftCommand RotateLeft;
        public RotateRightCommand RotateRight;

        public OrtographicCameraController(float width, float height, bool rotationIsEnabled)
        {
            _zoomLevel = 1.0f;
            _aspectRatio = width / height;

            Camera = new OrtographicCamera(width, height);

            _rotationIsEnabled = rotationIsEnabled;

            _cameraPosition = Vector3.Zero;
            _cameraRotation = 0.0f;

            _cameraTranslationSpeed = 3.0f;
            _cameraRotationSpeed = 3.0f;
            _cameraScrollZoom = 5.0f;

            Zoom = new ZoomCommand(this);
            MoveLeft = new MoveLeftCommand(this);
            MoveRight = new MoveRightCommand(this);
            MoveUp = new MoveUpCommand(this);
            MoveDown = new MoveDownCommand(this);
            RotateLeft = new RotateLeftCommand(this);
            RotateRight = new RotateRightCommand(this);
        }

        public void OnResize(Size size)
        {

        }

        public void OnUpdate(double deltaTime)
        {
            Camera.Position = _cameraPosition;
            Camera.Rotation = _cameraRotation;

            Camera.RecalculateViewMatrix();
        }

        #region Commands

        public class ZoomCommand : RangeCommand
        {
            private OrtographicCameraController _controller;

            public ZoomCommand(OrtographicCameraController controller)
            {
                _controller = controller;
            }

            public override void Execute(double deltaTime)
            {
                _controller._zoomLevel += (float)(_controller._cameraTranslationSpeed * deltaTime * Value);
                _controller.Camera.SetProjection(
                   -_controller._aspectRatio * _controller._zoomLevel,
                    _controller._aspectRatio * _controller._zoomLevel,
                   -_controller._zoomLevel,
                    _controller._zoomLevel);
            }
        }

        public class MoveLeftCommand : StateCommand
        {
            private OrtographicCameraController _controller;

            public MoveLeftCommand(OrtographicCameraController controller)
            {
                _controller = controller;
            }

            public override void Execute(double deltaTime)
            {
                if (CurrentState == StateType.Pressed)
                {
                    _controller._cameraPosition.X -= (float)(_controller._cameraTranslationSpeed * deltaTime);
                }
            }
        }

        public class MoveRightCommand : StateCommand
        {
            private OrtographicCameraController _controller;

            public MoveRightCommand(OrtographicCameraController controller)
            {
                _controller = controller;
            }

            public override void Execute(double deltaTime)
            {
                if (CurrentState == StateType.Pressed)
                {
                    _controller._cameraPosition.X += (float)(_controller._cameraTranslationSpeed * deltaTime);
                }
            }
        }

        public class MoveUpCommand : StateCommand
        {
            private OrtographicCameraController _controller;

            public MoveUpCommand(OrtographicCameraController controller)
            {
                _controller = controller;
            }

            public override void Execute(double deltaTime)
            {
                if (CurrentState == StateType.Pressed)
                {
                    _controller._cameraPosition.Y += (float)(_controller._cameraTranslationSpeed * deltaTime);
                }
            }
        }

        public class MoveDownCommand : StateCommand
        {
            private OrtographicCameraController _controller;

            public MoveDownCommand(OrtographicCameraController controller)
            {
                _controller = controller;
            }

            public override void Execute(double deltaTime)
            {
                if (CurrentState == StateType.Pressed)
                {
                    _controller._cameraPosition.Y -= (float)(_controller._cameraTranslationSpeed * deltaTime);
                }
            }
        }

        public class RotateLeftCommand : StateCommand
        {
            private OrtographicCameraController _controller;

            public RotateLeftCommand(OrtographicCameraController controller)
            {
                _controller = controller;
            }

            public override void Execute(double deltaTime)
            {
                if (CurrentState == StateType.Pressed && _controller._rotationIsEnabled)
                {
                    _controller._cameraRotation += (float)(_controller._cameraRotationSpeed * deltaTime);
                }
            }
        }

        public class RotateRightCommand : StateCommand
        {
            private OrtographicCameraController _controller;

            public RotateRightCommand(OrtographicCameraController controller)
            {
                _controller = controller;
            }

            public override void Execute(double deltaTime)
            {
                if (CurrentState == StateType.Pressed && _controller._rotationIsEnabled)
                {
                    _controller._cameraRotation -= (float)(_controller._cameraRotationSpeed * deltaTime);
                }
            }
        }

        #endregion
    }
}
