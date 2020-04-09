namespace Core.CoreSystem.Input.Controllers
{
    using System.Collections.Generic;
    using System.Drawing;
    using Silk.NET.Input.Common;
    
    public class Mouse
    {
        public Mouse(IEnumerable<IMouse> mice)
        {
            foreach (var mouse in mice)
            {
                mouse.MouseDown += ButtonDown;
                mouse.MouseUp += ButtonUp;
                mouse.MouseMove += MouseMove;
                mouse.DoubleClick += MouseDoubleClick;
            }
        }
        
        public void ButtonDown(IMouse mouse, MouseButton button)
        {
        }

        public void ButtonUp(IMouse mouse, MouseButton button)
        {
        }

        public void MouseDoubleClick(IMouse mouse, MouseButton button)
        {
        }

        public void MouseMove(IMouse mouse, PointF position)
        {
        }
    }
}