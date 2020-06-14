namespace Reload.Rendering.Structures
{
    using System.Collections.ObjectModel;

    public class BufferLayout : Collection<BufferElement>
    {
        public uint Stride { get; private set; } = 0;

        public BufferLayout()
        {}

        public new void Add(BufferElement bufferElement)
        {
            bufferElement.Offset = Stride;
            Stride += bufferElement.Size;

            base.Add(bufferElement);
        }
    }
}
