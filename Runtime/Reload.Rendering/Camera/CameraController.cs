using System;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Reload.Resources.Camera
{
    public struct TranslationState
    {
        public bool forward;
        public bool backward;
        public bool left;
        public bool right;
        public bool up;
        public bool down;
    }

    public struct RollState
    {
        public bool left;
        public bool right;
    }

    /// <summary>
    /// Camera controller class. Used to controll transformations
    /// (position, rotation, zoom etc) of the <see cref="Rendering.Camera.Camera"/> calss.
    /// </summary>
    public class CameraController
    {

        #region Constants

        /// <summary>
        /// Default angle value used to calculate the other scale factor default values.
        /// </summary>
        private const float DefaultAngle = MathF.PI * 0.5f * 0.001f;

        /// <summary>
        /// The default roll angle scale factor.
        /// </summary>
        private const float DefaultRollAngleScaleFactor = 10.0f * DefaultAngle;

        /// <summary>
        /// The default pitch angle scale factor.
        /// </summary>
        private const float DefaultPitchAngleScaleFactor = DefaultAngle;

        /// <summary>
        /// The default yaw angle scale factor.
        /// </summary>
        private const float DefaultYawAngleScaleFactor = DefaultAngle;

        /// <summary>
        /// The default forward delta scale factor.
        /// </summary>
        private const float DefaultForwardDeltaScaleFactor = 1.0f;

        /// <summary>
        /// The default side strafe delta scale factor.
        /// </summary>
        private const float DefaultSideStrafeDeltaScaleFactor = 1.0f;

        /// <summary>
        /// The default up delta scale factor.
        /// </summary>
        private const float DefaultUpDeltaScaleFactor = 1.0f;

        /// <summary>
        /// The default zoom delta scale factor.
        /// </summary>
        private const float DefaultZoomDeltaScaleFactor = 5.0f;

        #endregion

        #region Fields

        private Vector3 _yawAxis;

        private bool _shouldRotate;

        private TranslationState _translationState;

        private RollState _rollState;

        private float _previousCursorPositionX;

        private float _previousCursorPositionY;

        private float _cursorPositionX;

        private float _cursorPositionY;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="Camera"/> controled by the controller.
        /// </summary>
        public Camera Camera { get; private set; }

        /// <summary>
        /// Gets or sets the roll angle scale factor.
        /// </summary>
        public float RollAngleScaleFactor { get; set; }

        /// <summary>
        /// Gets or sets the pitch angle scale factor.
        /// </summary>
        public float PitchAngleScaleFactor { get; set; }

        /// <summary>
        /// Gets or sets the yaw angle scale factor.
        /// </summary>
        public float YawAngleScaleFactor { get; set; }

        /// <summary>
        /// Gets or sets the forward delta scale factor.
        /// </summary>
        public float ForwardDeltaScaleFactor { get; set; }

        /// <summary>
        /// Gets or sets the side strafe delta scale factor.
        /// </summary>
        public float SideStrafeDeltaScaleFactor { get; set; }

        /// <summary>
        /// Gets or sets the up delta scale factor.
        /// </summary>
        public float UpDeltaScaleFactor { get; set; }

        /// <summary>
        /// Gets or sets the zoom delta scale factor.
        /// </summary>
        public float ZoomDeltaScaleFactor { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraController"/> class
        /// with deafult values. Camera should be registered manualy via <seealso cref="RegisterCamera(Camera)"/>
        /// method.
        /// </summary>
        public CameraController()
        {
            RollAngleScaleFactor = DefaultRollAngleScaleFactor;
            PitchAngleScaleFactor = DefaultPitchAngleScaleFactor;
            YawAngleScaleFactor = DefaultYawAngleScaleFactor;
            ForwardDeltaScaleFactor = DefaultForwardDeltaScaleFactor;
            SideStrafeDeltaScaleFactor = DefaultSideStrafeDeltaScaleFactor;
            UpDeltaScaleFactor = DefaultUpDeltaScaleFactor;
            ZoomDeltaScaleFactor = DefaultZoomDeltaScaleFactor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraController"/> class
        /// with default values, and registers the <seealso cref="Rendering.Camera.Camera"/> 
        /// sent as parmeter automaticaly.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public CameraController(Camera camera)
            : this()
        {
            RegisterCamera(camera);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers the <seealso cref="Rendering.Camera.Camera"/> sent as parameer to the <seealso cref="CameraController"/>.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public void RegisterCamera(Camera camera)
        {
            Debug.Assert(camera != null);

            Camera = camera;
            _yawAxis = camera.Up;
        }

        public void CursorPosition(float xPosition, float yPosition)
        {
            _previousCursorPositionX = _cursorPositionX;
            _previousCursorPositionY = _cursorPositionY;

            _cursorPositionX = xPosition;
            _cursorPositionY = yPosition;

            _shouldRotate = true;
        }

        /// <summary>
        /// Translates the <see cref="Camera"/> forward.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop translating</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TranslateForward(bool state) => _translationState.forward = state;

        /// <summary>
        /// Translates the <see cref="Camera"/> backwards.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop translating</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TranslateBackward(bool state) => _translationState.backward = state;

        /// <summary>
        /// Translates the <see cref="Camera"/> up.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop translating</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TranslateUp(bool state) => _translationState.up = state;

        /// <summary>
        /// Translates the <see cref="Camera"/> down.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop translating</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TranslateDown(bool state) => _translationState.down = state;

        /// <summary>
        /// Translates the <see cref="Camera"/> left.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop translating</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TranslateLeft(bool state) => _translationState.left = state;

        /// <summary>
        /// Translates the <see cref="Camera"/> right.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop translating</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TranslateRight(bool state) => _translationState.right = state;

        /// <summary>
        /// Rolls the <see cref="Camera"/> left.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop rolling</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RollLeft(bool state) => _rollState.left = state;

        /// <summary>
        /// Rolls the <see cref="Camera"/> right.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop rolling</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RollRight(bool state) => _rollState.right = state;

        /// <summary>
        /// Zooms in or out the <see cref="Camera"/>.
        /// </summary>
        /// <param name="state">The state of the action. Sets a flag whether to start or stop rolling</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Zoom(bool state)
        {
            Camera.FieldOfView = state 
                ? Camera.FieldOfView += ZoomDeltaScaleFactor 
                : Camera.FieldOfView -= ZoomDeltaScaleFactor;
        }

        /// <summary>
        /// Updates the <seealso cref="Rendering.Camera.Camera"/> translation.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        private void UpdateTranslation(double deltaTime)
        {
            if (_translationState.forward)
            {
                Camera.TranslateLocal(0.0f, 0.0f, ForwardDeltaScaleFactor * (float)deltaTime);
            }
            if (_translationState.backward)
            {
                Camera.TranslateLocal(0.0f, 0.0f, -ForwardDeltaScaleFactor * (float)deltaTime);
            }
            if (_translationState.up)
            {
                Camera.TranslateLocal(0.0f, UpDeltaScaleFactor * (float)deltaTime, 0.0f);
            }
            if (_translationState.down)
            {
                Camera.TranslateLocal(0.0f, -UpDeltaScaleFactor * (float)deltaTime, 0.0f);
            }
            if (_translationState.left)
            {
                Camera.TranslateLocal(SideStrafeDeltaScaleFactor * (float)deltaTime, 0.0f, 0.0f);
            }
            if (_translationState.right)
            {
                Camera.TranslateLocal(-SideStrafeDeltaScaleFactor * (float)deltaTime, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Updates the <seealso cref="Rendering.Camera.Camera"/> roll value.
        /// </summary>
        private void UpdateRoll()
        {
            float angle = RollAngleScaleFactor;

            if (_rollState.left)
            {
                angle *= -1;
                Camera.Roll(angle);
                RotateYawAxis(angle, Camera.Forward);
            }
            else if (_rollState.right)
            {
                Camera.Roll(angle);
                RotateYawAxis(angle, Camera.Forward);
            }
        }

        /// <summary>
        /// Rotates the yaw axis used to prevent rolling effect when concatentating yaw and pitch maneuvers..
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="axis">The axis.</param>
        private void RotateYawAxis(float angle, Vector3 axis)
        {
            Quaternion quaternion = Quaternion.CreateFromAxisAngle(axis, angle);
            _yawAxis = Vector3.Transform(_yawAxis, quaternion);
        }

        /// <summary>
        /// Updates the <see cref="Camera"/> look-at.
        /// </summary>
        private void UpdateLookAt()
        {
            if (!_shouldRotate)
            {
                return;
            }
            if (_previousCursorPositionX == 0 || _previousCursorPositionY == 0)
            {
                return;
            }

            float deltaX = _cursorPositionX - _previousCursorPositionX;
            float deltaY = _cursorPositionY - _previousCursorPositionY;
            float pitchAngle = deltaY * PitchAngleScaleFactor;

            Camera.Pitch(pitchAngle);
            Camera.Rotate(-deltaX * YawAngleScaleFactor, _yawAxis);

            _shouldRotate = false;
        }

        /// <summary>
        /// Updates the <seealso cref="Rendering.Camera.Camera"/>.
        /// </summary>
        public void UpdateCamera(double deltaTime)
        {
            Debug.Assert(Camera != null);

            UpdateTranslation(deltaTime);
            UpdateRoll();
            UpdateLookAt();
        }

        /// <summary>
        /// Reset the <see cref="CameraController"/> object state. The registered camera does not change.
        /// </summary>
        public void Reset()
        {
            _previousCursorPositionX = 0;
            _previousCursorPositionY = 0;
            _cursorPositionX = 0;
            _cursorPositionY = 0;
            _translationState.forward = false;
            _translationState.backward = false;
            _translationState.up = false;
            _translationState.down = false;
            _translationState.left = false;
            _translationState.right = false;
            _rollState.left = false;
            _rollState.right = false;
        }

        #endregion
    }
}
