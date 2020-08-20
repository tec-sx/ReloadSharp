namespace Reload.Core.Common
{
    public interface IBindable
    {
        /// <summary>
        /// Binds the object.
        /// </summary>
        void Bind();

        /// <summary>
        /// Unbinds the object.
        /// </summary>
        void Unbind();
    }
}
