using System.Numerics;

namespace Reload.Core.Math3D
{
    /// <summary>
    /// Ray struct.
    /// </summary>
    public struct Ray
    {
        /// <summary>
        /// The ray origin vector.
        /// </summary>
        public Vector3 Origin { get; set; }

        /// <summary>
        /// The ray direction vector.
        /// </summary>
        public Vector3 Direction { get; set; }

        /// <summary>
        /// Constructor asigning the origin and direction
        /// of the ray.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
