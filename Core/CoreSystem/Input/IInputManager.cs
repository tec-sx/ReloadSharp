namespace Core.CoreSystem.Input
{
    using System.Drawing;
    using Controllers;
    using Silk.NET.Input.Common;
    
    public interface IInputManager
    {
        public Mouse Mouse { get; }
        public Keyboard Keyboard { get; }
    }
}