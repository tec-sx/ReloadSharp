namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Reload.Game;
    using Reload.Input.Events;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;

    public class Mouse : PointerDeviceBase, IMouseDevice
    {
        private IMouse silkMouse;
        private IGame game;

        private bool isMousePositionLocked;
        private bool wasMouseVisibleBeforeCapture;
        private Point relativeCapturedPosition;

        protected MouseState MouseState;
        public override string Name { get; }
        public override int Index { get; }
        public override bool IsConnected { get; }

        public bool IsPositionLocked { get; }
        public IReadOnlySet<MouseButton> PressedButtons => MouseState.PressedButtons;
        public IReadOnlySet<MouseButton> ReleasedButtons => MouseState.ReleasedButtons;
        public IReadOnlySet<MouseButton> DownButtons => MouseState.DownButtons;
        public Vector2 Position => MouseState.Position;
        public Vector2 Delta => MouseState.Delta;

        public Mouse(IMouse mouse, IGame game)
        {
            silkMouse = mouse;
            this.game = game;
            MouseState = new MouseState(PointerState, this);

            Name = silkMouse.Name;
            Index = silkMouse.Index;
            IsConnected = silkMouse.IsConnected;
        }

        public override void Update(List<InputEvent> inputEvents)
        {
            base.Update(inputEvents);
            MouseState.Update(inputEvents);
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
            Vector2 position = normalizedPosition * SurfaceSize;
            silkMouse.Position = new Point((int)position.X, (int)position.Y);
        }

        public void UnlockPosition()
        {
            throw new System.NotImplementedException();
        }
    }
}
