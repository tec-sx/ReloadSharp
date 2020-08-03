using Reload.Core.Utils;
using System.Numerics;

namespace Reload.Rendering.Camera
{
    /// <summary>
    /// Abstract class that defines a frustum volume. Inherited by the
    /// <see cref="Camera"/> class to filter calculation for
    /// objects outside of the the visible field.
    /// </summary>
    public abstract class Frustum
    {
        #region Fields

        protected float fieldOfView;

        protected float aspectRatio;

        protected float nearPlaneZ;

        protected float farPlaneZ;

        protected bool isPerspective;

        protected bool shouldRecalculatePerspectiveMatrix;

        protected static ushort floatTolerance = 5;

        protected Matrix4x4 projectionMatrix;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the camera is perspective.
        /// </summary>
        public bool IsPerspective => isPerspective;

        /// <summary>
        /// Gets a value indicating whether the camera is orthographic.
        /// </summary>
        public bool IsOrthographic => !isPerspective;

        /// <summary>
        /// Gets or sets the field of view value.
        /// </summary>
        public float FieldOfView
        {
            get => fieldOfView;
            set
            {
                if (value <= 0.0f)
                {
                    fieldOfView = 0.01f;
                }
                else if (value >= 180.0f)
                {
                    fieldOfView = 179.9f;
                }
                else
                {
                    fieldOfView = value;
                }

                shouldRecalculatePerspectiveMatrix = true;
            }
        }

        /// <summary>
        /// Gets or sets the aspect ratio of the camera.
        /// </summary>
        public float AspectRatio
        {
            get => aspectRatio;
            set
            {
                aspectRatio = value;
                shouldRecalculatePerspectiveMatrix = true;
            }
        }

        /// <summary>
        /// Gets or sets the distance to the near Z axis plane.
        /// </summary>
        public float NearPlaneZ
        {
            get => nearPlaneZ;
            set
            {
                nearPlaneZ = value;
                shouldRecalculatePerspectiveMatrix = true;
            }
        }

        /// <summary>
        /// Gets or sets the distance to the far Z axis plane.
        /// </summary>
        public float FarPlaneZ
        {
            get => farPlaneZ;
            set
            {
                farPlaneZ = value;
                shouldRecalculatePerspectiveMatrix = true;
            }
        }

        /// <summary>
        /// Gets or sets the camera projection matrix.
        /// </summary>
        public Matrix4x4 ProjectionMatrix
        {
            get
            {
                if (shouldRecalculatePerspectiveMatrix)
                {
                    projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                        ReloadMath.DegreesToRadians(fieldOfView), 
                        aspectRatio, 
                        nearPlaneZ, 
                        farPlaneZ);
                    shouldRecalculatePerspectiveMatrix = false;
                }

                return projectionMatrix;
            }
            set => projectionMatrix = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Frustum"/> class
        /// with all the properties set to their default value (zero/false).
        /// </summary>
        public Frustum()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frustum"/> class
        /// with values from other <see cref="Frustum"/> class
        /// </summary>
        /// <param name="copyFrustum">
        /// The <see cref="Frustum"/> class to copy the properties from.
        /// </param>
        public Frustum(Frustum copyFrustum)
        {
            fieldOfView = copyFrustum.fieldOfView;
            aspectRatio = copyFrustum.aspectRatio;
            NearPlaneZ = copyFrustum.NearPlaneZ;
            FarPlaneZ = copyFrustum.FarPlaneZ;
            isPerspective = copyFrustum.isPerspective;
            shouldRecalculatePerspectiveMatrix = copyFrustum.shouldRecalculatePerspectiveMatrix;
            projectionMatrix = copyFrustum.ProjectionMatrix;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frustum"/> class
        /// with orthographic projection view.
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
        public Frustum(float left, float right, float bottom, float top, float nearPlaneZ, float farPlaneZ)
        {
            fieldOfView = 0.0f;
            aspectRatio = (left - right) / (top - bottom);
            this.nearPlaneZ = nearPlaneZ;
            this.farPlaneZ = farPlaneZ;
            isPerspective = false;
            shouldRecalculatePerspectiveMatrix = false;
            projectionMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, this.nearPlaneZ, this.farPlaneZ);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Frustum"/> class
        /// with perspective projection view.
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
        public Frustum(float fieldOfView, float aspectRatio, float nearPlaneZ, float farPlaneZ)
        {
            FieldOfView = fieldOfView;
            this.aspectRatio = aspectRatio;
            this.nearPlaneZ = nearPlaneZ;
            this.farPlaneZ = farPlaneZ;
            isPerspective = true;
            shouldRecalculatePerspectiveMatrix = true;
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
                ReloadMath.DegreesToRadians(FieldOfView), 
                this.aspectRatio, 
                this.nearPlaneZ, 
                this.farPlaneZ);
        }

        #endregion
    }
}
