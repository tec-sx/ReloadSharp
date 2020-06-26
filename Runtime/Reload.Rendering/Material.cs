namespace Reload.Rendering
{
    using System;
    using System.Numerics;

    public abstract class Material
    {
        public Material(ShaderProgram shader)
        {

        }

        public abstract void Set(string uniformName, float value);

        public abstract void Set(string uniformName, Vector4 value);

        public abstract void Set(string uniformName, Matrix4x4 value);
    }
}
