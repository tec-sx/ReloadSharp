namespace Core.CoreSystem.Input.Controllers
{
    using System.Collections.Generic;
    using Silk.NET.Input.Common;
    
    public class Keyboard
    {
        public Keyboard(IEnumerable<IKeyboard> keyboards)
        {
            foreach (var keyboard in keyboards)
            {
                keyboard.KeyDown += KeyDown;
                keyboard.KeyUp += KeyUp;
            }
        }
        
        public void KeyDown(IKeyboard keyboard, Key key, int arg3)
        {
        }

        public void KeyUp(IKeyboard keyboard, Key key, int arg3)
        {
        }
    }
}