namespace Engine.Events.Mouse
{
    using Silk.NET.Input.Common;
    using System;

    public class MouseEvent
    {
        public event Action Interact;

        private MouseConfiguration mouseInput;

        public MouseEvent(IMouse mouse)
        {
            mouse.MouseDown += OnMouseDown;
        }

        public void MapInput(MouseConfiguration configuration)
        {
            mouseInput = configuration;
        }

        private void OnMouseDown(IMouse mouse, MouseButton button)
        {
            if (button == mouseInput.Interact) Interact?.Invoke();
        }
    }
}
