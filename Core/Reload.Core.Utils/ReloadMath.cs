namespace Reload.Core.Utils
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Math helper class for methods not available
    /// in System.Numerics.
    /// </summary>
    public static class ReloadMath
    {
        /// <summary>
        /// Converts angle in degrees to radians.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <returns>A float.</returns>
        public static float DegreesToRadians(float degrees)
        {
            return (MathF.PI / 180) * degrees;
        }

        /// <summary>
        /// Converts angle in radians to degrees.
        /// </summary>
        /// <param name="radiants">The radiants.</param>
        /// <returns>A float.</returns>
        public static float RadiansToDegrees(float radians)
        {
            return 180.0f * (radians / MathF.PI);
        }

        /// <summary>
        /// <see cref="Quaternion"/> extension method that converts quaternion value
        /// to <see cref="Vector3"/> euler angles.
        /// </summary>
        /// <param name="q">The q.</param>
        /// <returns>A Vector3.</returns>
        public static Vector3 ToEulerAngles(this Quaternion q)
        {
            // Store the Euler angles in radians
            Vector3 pitchYawRoll = new Vector3();

            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;

            // If quaternion is normalised the unit is one, otherwise it is the correction factor
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;

            if (test > 0.499f * unit)
            {
                // Singularity at north pole
                pitchYawRoll.Y = 2f * (float)Math.Atan2(q.X, q.W); // Yaw
                pitchYawRoll.X = (float)Math.PI * 0.5f; // Pitch
                pitchYawRoll.Z = 0f; // Roll
                return pitchYawRoll;
            }
            else if (test < -0.499f * unit)
            {
                // Singularity at south pole
                pitchYawRoll.Y = -2f * (float)Math.Atan2(q.X, q.W); // Yaw
                pitchYawRoll.X = -(float)Math.PI * 0.5f; // Pitch
                pitchYawRoll.Z = 0f; // Roll
                return pitchYawRoll;
            }

            pitchYawRoll.Y = (float)Math.Atan2(2 * q.Y * q.W - 2 * q.X * q.Z, sqx - sqy - sqz + sqw); // Yaw
            pitchYawRoll.X = (float)Math.Asin(2 * test / unit); // Pitch
            pitchYawRoll.Z = (float)Math.Atan2(2 * q.X * q.W - 2 * q.Y * q.Z, -sqx + sqy - sqz + sqw); // Roll

            return pitchYawRoll;
        }
    }
}
