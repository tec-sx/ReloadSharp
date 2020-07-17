namespace Reload.Rendering.Camera
{
    using Reload.Core.Commands;
    using System;
    using System.Numerics;

    public class PerspectiveCameraController
    {
        public PerspectiveCamera Camera { get; }

        public ZoomCommand Zoom { get; }

        private (float, float) PanSpeed
        {
            get
            {
                float x = MathF.Min(Camera.ViewportSize.Width / 1000.0f, 2.4f); // max = 2.4f
                float xFactor = 0.0366f * (x * x) - 0.1778f * x + 0.3021f;

                float y = MathF.Min(Camera.ViewportSize.Height / 1000.0f, 2.4f); // max = 2.4f
                float yFactor = 0.0366f * (y * y) - 0.1778f * x + 0.3021f;

                return (xFactor, yFactor);
            }
        }

        private float ZoomSpeed
        {
            get
            {
                float distance = Camera.Distance * 0.2f;
                distance = MathF.Max(distance, 0.0f);
                float speed = distance * distance;
                speed = MathF.Min(speed, 100.0f);

                return speed;
            }
        }

        public PerspectiveCameraController(Matrix4x4 projectionMatrix)
        {
            Camera = new PerspectiveCamera(projectionMatrix);
            Zoom = new ZoomCommand(this);
        }

        public void OnUpdate(double deltaTime)
        {
            Camera.OnUpdate(deltaTime);
        }

        public class ZoomCommand : RangeCommand
        {
            private PerspectiveCameraController _cameraController;

            public ZoomCommand(PerspectiveCameraController cameraController)
            {
                _cameraController = cameraController;
            }

            public override void Execute(double deltaTime)
            {
                var camera = _cameraController.Camera;
                
                camera.Distance = (float)deltaTime * Value * _cameraController.ZoomSpeed;

                if (camera.Distance < 1.0f)
                {
                    camera.FocalPoint += camera.ForwardDirection;
                    camera.Distance = 1.0f;
                }
            }
        }
    }
}
