//
//

using System.Numerics;

namespace Reload.Rendering.Camera
{
    /// <summary>
    /// Class for encapsulating a view frustum camera, capable of either orthographic or perspective
    /// projections. Both view and projection matrices can be retrieved from a <see cref="Camera"/> object at
    /// any time, which reflect the current state of the <see cref="Camera"/>.
    /// Inherits from <seealso cref="Frustum"/> class.
    /// </summary>
    public class Camera: Frustum
    {
        #region Constants

        /// <summary>
        /// The maximum rotation hit count.
        /// </summary>
        private const ushort MaxRotationHitCount = 1000;

        #endregion

        #region Fields

        private readonly Vector3 _worldUp = Vector3.UnitY;

        private Vector3 _position;

        private Quaternion _orientation;

        private Vector3 _left;

        private Vector3 _up;

        private Vector3 _forward;

        private Matrix4x4 _viewMatrix;

        private bool _shouldRecalculateViewMatrix;

        private ushort _rotationHitCount;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the world position of the <see cref="Camera"/>.
        /// </summary>
        public Vector3 Position => _position;

        /// <summary>
        /// Gets the <see cref="Camera"/>'s left direction vector given in world space coordinates.
        /// </summary>
        public Vector3 Left => _left;

        /// <summary>
        /// Gets the <see cref="Camera"/>'s up direction vector given in world space coordinates.
        /// </summary>
        public Vector3 Up => _up;

        /// <summary>
        /// Gets the <see cref="Camera"/>'s forward direction vector given in world space coordinates.
        /// </summary>
        public Vector3 Forward => _forward;


        /// <summary>
        /// Gets the <see cref="Camera"/>'s orientation in the form of a float quaternion.
        /// <value>
        /// Orientation of the Camera is given by a float quaternion of the form
        /// <code>(cos(theta/2), sin(theta/2) * _up)</code> where the axis of rotation <see cref="_up"/> is given in
        /// world space coordinates.
        /// </value>
        /// </summary>
        public Quaternion Orientation => _orientation;

        /// <summary>
        /// Gets the view matrix representing the <see cref="Camera"/> object's view transformation.
        /// </summary>
        public Matrix4x4 ViewMatrix
        {
            get
            {
                if (_shouldRecalculateViewMatrix)
                {
                    _viewMatrix = Matrix4x4.CreateFromQuaternion(-_orientation);
                    _viewMatrix += Matrix4x4.CreateTranslation(-_position);
                    _shouldRecalculateViewMatrix = false;
                }

                return _viewMatrix;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class
        /// with all the properties set to their default value (zero/false).
        /// </summary>
        public Camera()
        {
            _shouldRecalculateViewMatrix = true;
            _rotationHitCount = 0;

            InitLocalCoordinateSystem();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class
        /// with perspective projection.
        /// </summary>
        /// <remarks>
        /// Depth buffer precision is affected by the values specified for
        /// zNear and zFar.The greater the ratio of zFar to zNear is, the less
        /// effective the depth buffer will be at distinguishing between surfaces
        /// that are near each other.If  r = zFar zNear roughly log2(r) bits
        /// of depth buffer precision are lost. Because r approaches infinity as
        /// zNear approaches 0, zNear must never be set to 0.
        /// </remarks>
        /// <param name="fieldOfView">The camera field of view.</param>
        /// <param name="aspectRatio">The camera aspect ration.</param>
        /// <param name="nearPlaneZ">Distance to the near z clipping plane (always positive).</param>
        /// <param name="farPlaneZ">Distance to the far z clipping plane (always positive).</param>
        public Camera(float fieldOfView, float aspectRatio, float nearPlaneZ, float farPlaneZ) 
            : base(fieldOfView, aspectRatio, nearPlaneZ, farPlaneZ)
        {
            _shouldRecalculateViewMatrix = true;
            _rotationHitCount = 0;

            InitLocalCoordinateSystem();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class
        /// with orthographic projection.
        /// </summary>
        /// <remarks>
        /// If NearPlaneZ or FarPlaneZ are negative, corresponding Z clipping planes
        /// are considered behind the viewer.
        /// </remarks>
        /// <param name="left">Specify location of left clipping plane.</param>
        /// <param name="right">Specify location of right clipping plane.</param>
        /// <param name="bottom">Specify location of bottom clipping plane.</param>
        /// <param name="top">Specify location of top clipping plane.</param>
        /// <param name="nearPlaneZ">Distance to the near z clipping plane.</param>
        /// <param name="farPlaneZ">Distance to the far z clipping plane.</param>
        public Camera(float left, float right, float bottom, float top, float nearPlaneZ, float farPlaneZ) 
            : base(left, right, bottom, top, nearPlaneZ, farPlaneZ)
        {
            _shouldRecalculateViewMatrix = true;
            _rotationHitCount = 0;

            InitLocalCoordinateSystem();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the local coordinate system for the <see cref="Camera"/>.
        /// </summary>
        public void InitLocalCoordinateSystem()
        {
            _left = -Vector3.UnitX;
            _up = Vector3.UnitY;
            _forward = -Vector3.UnitZ;
            _position = Vector3.Zero;
        }

        /// <summary>
        /// Sets the the world position of the <see cref="Camera"/>.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public void SetPosition(float x, float y, float z)
        {
            _position.X = x;
            _position.Y = y;
            _position.Z = z;
            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Sets the the world position of the <see cref="Camera"/>.
        /// </summary>
        /// <param name="newPosition">The new position.</param>
        public void SetPosition(Vector3 newPosition)
        {
            _position = newPosition;
            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Normalizes the <see cref="Camera"/> vectors.
        /// </summary>
        public void Normalize()
        {
            _left = Vector3.Normalize(_left);
            _up = Vector3.Normalize(_up);
            _forward = Vector3.Normalize(_forward);

            _left = Vector3.Cross(_up, _forward);
            _up = Vector3.Cross(_forward, _left);
        }


        /// <summary>
        /// Adda a rotation hit. if it reaches the mas value
        /// resets it to 0 and normalizes the camera.
        /// </summary>
        public void RegisterRotation()
        {
            _rotationHitCount++;

            if (_rotationHitCount > MaxRotationHitCount)
            {
                _rotationHitCount = 0;
                Normalize();
            }
        }

        /// <summary>
        /// Rotates <see cref="Camera"/> about its local negative z-axis (forward direction)
        /// by <paramref name="angle"/> radians.
        /// </summary>
        /// <remarks>
        /// Rotation is counter-clockwise if angle > 0, and clockwise otherwise
        /// </remarks>
        /// <param name="angle">The angle.</param>
        public void Roll(float angle)
        {
            Quaternion quaternion = Quaternion.CreateFromAxisAngle(_forward, angle);

            _up = Vector3.Transform(_up, quaternion);
            _left = Vector3.Transform(_left, quaternion);

            _orientation = quaternion * _orientation;

            RegisterRotation();
            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Rotates <see cref="Camera"/> about its local x-axis (right direction)
        /// by <paramref name="angle"/> radians.
        /// </summary>
        /// <remarks>
        /// Rotation is counter-clockwise if angle > 0, and clockwise otherwise
        /// </remarks>
        /// <param name="angle">The angle.</param>
        public void Pitch(float angle)
        {
            Quaternion quaternion = Quaternion.CreateFromAxisAngle(-_left, angle);

            _up = Vector3.Transform(_up, quaternion);
            _left = Vector3.Transform(_left, quaternion);

            _orientation = quaternion * _orientation;

            RegisterRotation();
            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Rotates <see cref="Camera"/> about its local y-axis (up direction)
        /// by <paramref name="angle"/> radians.
        /// </summary>
        /// <remarks>
        /// Rotation is counter-clockwise if angle > 0, and clockwise otherwise
        /// </remarks>
        /// <param name="angle">The angle.</param>
        public void Yaw(float angle)
        {
            Quaternion quaternion = Quaternion.CreateFromAxisAngle(_up, angle);

            _up = Vector3.Transform(_up, quaternion);
            _left = Vector3.Transform(_left, quaternion);
            _forward = Vector3.Transform(_forward, quaternion);

            _orientation = quaternion * _orientation;

            RegisterRotation();
            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Rotates <see cref="Camera"/>  by <paramref name="angle"/> radians about 
        /// <paramref name="axis"/> whose components are expressed using the 
        /// <see cref="Camera"/>'s local coordinate system.
        /// </summary>
        /// <remarks>
        /// Rotation is counter-clockwise if angle > 0, and clockwise otherwise
        /// </remarks>
        /// <param name="angle">The angle.</param>
        /// <param name="axis">The axis.</param>
        public void Rotate(float angle, Vector3 axis)
        {
            Vector3 normalizedAxis = Vector3.Normalize(axis);
            Quaternion quaternion = Quaternion.CreateFromAxisAngle(normalizedAxis, angle);

            _left = Vector3.Transform(_left, quaternion);
            _forward = Vector3.Transform(_forward, quaternion);
            _orientation = quaternion * _orientation;

            RegisterRotation();
            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Translates the <see cref="Camera"/> with respect to the world coordinate system.
        /// </summary>
        /// <param name="x">The x axis value.</param>
        /// <param name="y">The y axis valur.</param>
        /// <param name="z">The z axis value.</param>
        public void Translate(float x, float y, float z)
        {
            _position.X += x;
            _position.Y += y;
            _position.Z += z;

            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Translates the <see cref="Camera"/> with respect to the world coordinate system.
        /// </summary>
        /// <param name="vector">The vector to translate by.</param>
        public void Translate(Vector3 vector) => Translate(vector.X, vector.Y, vector.Z);


        /// <summary>
        /// Translates the <see cref="Camera"/> relative to its local coordinate system.
        /// </summary>
        /// <param name="left">Translate along the <see cref="Camera"/>'s left direction</param>
        /// <param name="up">Translate along the <see cref="Camera"/>'s up direction</param>
        /// <param name="forward">Translate along the <see cref="Camera"/>'s forward direction.</param>
        public void TranslateLocal(float left, float up, float forward)
        {
            _position += left * _left;
            _position += up * _up;
            _position += forward * _forward;

            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Translates the <see cref="Camera"/> relative to its local coordinate system.
        /// </summary>
        /// <remarks>
        /// <paramref name="vector"/>.X - translation along the <see cref="Camera"/>'s left direction.
        /// <paramref name="vector"/>.Y - translation along the <see cref="Camera"/>'s up direction.
        /// <paramref name="vector"/>.Z - translation along the <see cref="Camera"/>'s forward direction.
        /// </remarks>
        /// <param name="vector">The vector.</param>
        public void TranslateLocal(Vector3 vector) => TranslateLocal(vector.X, vector.Y, vector.Z);

        /// <summary>
        /// Create a camera LookAt.
        /// </summary>
        /// <param name="cameraPosition">The new camera position vector.</param>
        /// <param name="cameraTarget">The new camera target vector.</param>
        /// <param name="cameraUp">The new camera up vector.</param>
        public void LookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUp)
        {
            _position = cameraPosition;

            _forward = Vector3.Normalize(cameraPosition - cameraTarget);
            _left = Vector3.Normalize(Vector3.Cross(cameraUp, _forward));
            _up = Vector3.Cross(_forward, _left);

            Matrix4x4 lookAtMatrix = Matrix4x4.CreateLookAt(cameraPosition, cameraTarget, cameraUp);
            _orientation = Quaternion.CreateFromRotationMatrix(lookAtMatrix);

            RegisterRotation();
            _shouldRecalculateViewMatrix = true;
        }

        /// <summary>
        /// Create a camera LookAt.
        /// </summary>
        /// <param name="centerX">The target vector x value.</param>
        /// <param name="centerY">The target vector y value.</param>
        /// <param name="centerZ">The target vector z value.</param>
        public void LookAt(float centerX, float centerY, float centerZ)
        {
            Vector3 target = new Vector3(centerX, centerY, centerZ);

            LookAt(_position, target, _up);
        }

        #endregion
    }
}
