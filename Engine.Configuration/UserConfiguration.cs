namespace Engine.Configuration
{
    using Silk.NET.Input.Common;
    using System;
    using System.Drawing;

    [Serializable]
    public class UserConfiguration
    {
        public int UserId { get; } = 0;

        #region Display

        public Point DisplayResolution { get; set; } = new Point(1280, 720);
        public int DisplayRefreshRate { get; set; } = 60;
        public bool DisplayInFullScreen { get; set; } = false;
        public bool DisplayEnableVsync { get; set; } = false;
        public bool DisplayEnableVulkan { get; set; } = false;
        #endregion

        #region Audio

        public float AudioMasterVolume { get; set; } = 0.5f;
        public float AudioMusicVolume { get; set; } = 0.5f;
        public float AudioSfxVolume { get; set; } = 0.5f;
        #endregion

        #region Controls

        public int KeyboardId { get; set; }
        public int MouseId { get; set; }

        public Key KeyUp { get; set; } = Key.W;
        public Key KeyDown { get; set; } = Key.S;
        public Key KeyLeft { get; set; } = Key.A;
        public Key KeyRight { get; set; } = Key.D;
        public Key KeyRun { get; set; } = Key.ShiftLeft;
        public Key KeyDuck { get; set; } = Key.AltLeft;
        public Key KeyJump { get; set; } = Key.Space;
        public Key KeyOpenInventory { get; set; } = Key.Q;
        public Key KeyToggleFightMode { get; set; } = Key.E;
        public Key KeySelect { get; set; } = Key.Enter;
        public Key KeyPause { get; set; } = Key.Escape;
        public MouseButton MouseInteract { get; set; } = MouseButton.Left;
        #endregion
    }
}
