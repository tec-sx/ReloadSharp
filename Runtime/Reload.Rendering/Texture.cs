namespace Reload.Rendering
{
    using System;

    public abstract class Texture: IDisposable
    {
        public uint Width { get; protected set; }
        public uint Height { get; protected set; }

        public abstract void SetData(object data);
        public abstract void Bind(uint slot);

        public abstract void Dispose();
    }
}
