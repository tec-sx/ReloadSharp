namespace Reload.Input.Contexts
{
    using Silk.NET.Input.Common;

    internal abstract class InputContextBase
    {
        public abstract void HandleKeyDown(Key key);
        public abstract void HandleKeyUp(Key key);
    }
}
