namespace Reload.Rendering.Camera
{
    /// <summary>
    /// Class for encapsulating a view frustum camera with perspective projection
    /// Both view and projection matrices can be retrieved from a <see cref="PerspectiveCamera"/> object at
    /// any time, which reflect the current state of the <see cref="PerspectiveCamera"/>.
    /// Inherits from <seealso cref="Camera"/> class.
    /// </summary>
    public class PerspectiveCamera : Camera
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerspectiveCamera"/> class.
        /// </summary>
        /// <param name="fieldOfView">The field of view.</param>
        /// <param name="aspectRatio">The aspect ratio.</param>
        /// <param name="nearPlaneZ">The near plane z.</param>
        /// <param name="farPlaneZ">The far plane z.</param>
        public PerspectiveCamera(float fieldOfView, float aspectRatio, float nearPlaneZ, float farPlaneZ)
            : base(fieldOfView, aspectRatio, nearPlaneZ, farPlaneZ)
        { }
    }
}
