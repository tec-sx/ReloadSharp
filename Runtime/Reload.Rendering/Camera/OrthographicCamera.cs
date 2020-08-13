namespace Reload.Resources.Camera
{
    /// <summary>
    /// Class for encapsulating a view frustum camera with orthographic projection
    ///  Both view and projection matrices can be retrieved from a <see cref="OrthographicCamera"/> object at
    /// any time, which reflect the current state of the <see cref="OrthographicCamera"/>.
    /// Inherits from <seealso cref="Camera"/> class.
    /// </summary>
    public class OrthographicCamera : Camera
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrthographicCamera"/> class.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="top">The top.</param>
        /// <param name="nearPlaneZ">The near plane z.</param>
        /// <param name="farPlaneZ">The far plane z.</param>
        public OrthographicCamera(float aspectRatio)
            : base(aspectRatio)
        { }
    }
}
