namespace Reload.Core.Models.Rendering
{
    public interface IBuffer
    {
        /// <summary>
        /// Binds the buffer to the context for usage.
        /// </summary>
        void Bind();

        /// <summary>
        /// Unbinds the buffer from the context.
        /// </summary>
        void Unbind();
    }
}
