
using SpaceVIL;

namespace Reload.Editor.Input
{
    /// <summary>
    /// The input manager class.
    /// </summary>
    internal class InputManager
    {
        private Prototype _element;

        /// <summary>
        /// Binds the to element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void BindToElement(Prototype element)
        {
            _element = element;
        }

        public void Update()
        {

        }
    }
}
