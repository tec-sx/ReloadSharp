namespace Reload.Rendering
{
    using System;

    public enum TextureFormat
    {
        None = 0,
        Rgb,
        Rgba,
        Float16
    }

    public enum TextureWrap
    {
        None = 0,
        Clamp,
        Repeat
    }
    
    public abstract class Texture: IDisposable
    {
        public uint Width { get; protected set; }
        public uint Height { get; protected set; }

        public abstract void SetData(object data);
        public abstract void Bind(uint slot = 0);

        public abstract void Dispose();
    }
}
