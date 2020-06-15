namespace Reload.Rendering
{
    public abstract class Texture
    {
        public uint Width { get; protected set; }
        public uint Height { get; protected set; }

        public abstract void SetData(object data);
        public abstract void Bind(uint slot);
    }
}
