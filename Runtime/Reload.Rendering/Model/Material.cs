using System;

namespace Reload.Rendering.Model
{
    [Flags]
    public enum MaterialFlag
    {
        None = 0,
        DepthTest = 1 << 1,
        Blend = 1 << 2
    }

    public abstract class Material
    {
        private MaterialFlag _materialFlags;
        public MaterialFlag MaterialFlags
        {
            get => _materialFlags;
            set => _materialFlags |= value;
        }

        protected Material(ShaderProgram shader)
        {

        }

        public void Bind()
        {

        }

        public void Set<T>(string name, T value)
        {

        }

        public class MaterialInstance
        {

        }
    }
}
