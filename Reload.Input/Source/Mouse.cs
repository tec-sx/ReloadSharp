namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Reload.Game;
    using Reload.Input.Events;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;

    public class Mouse : IMouseDevice
    {
        private IMouse sourceDevice;
        private IGame game;

        private bool isMousePositionLocked;
        private bool wasMouseVisibleBeforeCapture;
        private Point relativeCapturedPosition;

        public string Name { get; }
        public int Index { get; }
        public bool IsConnected { get; }
        public bool IsPositionLocked { get; }

        public HashSet<MouseButton> PressedButtons { get; }
        public HashSet<MouseButton> ReleasedButtons { get; }
        public HashSet<MouseButton> DownButtons { get; }

        public Vector2 Position { get; private set; }
        public Vector2 Delta { get; private set; }

        public Mouse(IMouse source, IGame game)
        {
            sourceDevice = source;
            this.game = game;

            Name = sourceDevice.Name;
            Index = sourceDevice.Index;
            IsConnected = sourceDevice.IsConnected;

            source.Click += OnClick;
            source.DoubleClick += OnDoubleClick;
            source.MouseDown += OnButtonDown;
            source.MouseUp += OnButtonUp;
            source.MouseMove += OnMove;
            source.Scroll += OnScroll;
        }

        private void OnScroll(IMouse arg1, ScrollWheel arg2)
        {

        }

        private void OnMove(IMouse arg1, PointF arg2)
        {
        }

        private void OnButtonUp(IMouse arg1, MouseButton arg2)
        {
        }

        private void OnButtonDown(IMouse arg1, MouseButton arg2)
        {
        }

        private void OnDoubleClick(IMouse arg1, MouseButton arg2)
        {
        }

        private void OnClick(IMouse arg1, MouseButton arg2)
        {

        }

        public void LockPosition(bool forceCenter = false)
        {
            if (IsPositionLocked)
            {
                isMousePositionLocked = false;
                relativeCapturedPosition = Point.Empty;
                game.IsMouseVisible = wasMouseVisibleBeforeCapture;
            }
        }

        public void SetPosition(Vector2 normalizedPosition)
        {
            //Vector2 position = normalizedPosition * SurfaceSize;
            //sourceDevice.Position = new Point((int)position.X, (int)position.Y);
        }

        public void UnlockPosition()
        {
            throw new System.NotImplementedException();
        }

        //private void OnMouseWheelEvent(SDL.SDL_MouseWheelEvent sdlMouseWheelEvent)
        //{
        //    var flip = sdlMouseWheelEvent.direction == (uint)SDL.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_FLIPPED ? -1 : 1;
        //    MouseState.HandleMouseWheel(sdlMouseWheelEvent.y * flip);
        //}

        //private void OnMouseInputEvent(SDL.SDL_MouseButtonEvent e)
        //{
        //    MouseButton button = ConvertMouseButton(e.button);

        //    if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
        //    {
        //        MouseState.HandleButtonDown(button);
        //    }
        //    else
        //    {
        //        MouseState.HandleButtonUp(button);
        //    }
        //}

        //private void OnMouseMoveEvent(SDL.SDL_MouseMotionEvent e)
        //{
        //    if (IsPositionLocked)
        //    {
        //        MouseState.HandleMouseDelta(new Vector2(e.x - relativeCapturedPosition.X, e.y - relativeCapturedPosition.Y));

        //        // Restore position to prevent mouse from going out of the window where we would not get
        //        // mouse move event.
        //        uiControl.RelativeCursorPosition = relativeCapturedPosition;
        //    }
        //    else
        //    {
        //        MouseState.HandleMove(new Vector2(e.x, e.y));
        //    }
        //}
    }
}
