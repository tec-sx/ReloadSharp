namespace Engine.Configuration
{
    using System;
    using System.Numerics;
    using Silk.NET.Input.Common;

    [Serializable]
    public class UserConfiguration
    {
        #region Display
        public Vector2 DisplayResolution { get; set; } = new Vector2(1280, 720);
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
        public Key Up { get; set; } = Key.W;
        public Key Down { get; set; } = Key.S;
        public Key Left { get; set; } = Key.A;
        public Key Right { get; set; } = Key.D;
        public Key Run { get; set; } = Key.ShiftLeft;
        public Key Duck { get; set; } = Key.AltLeft;
        public Key Jump { get; set; } = Key.Space;
        public Key OpenInventory { get; set; } = Key.Q;
        public Key ToggleFightMode { get; set; } = Key.E;
        public Key Select { get; set; } = Key.Enter;
        public Key Pause { get; set; } = Key.Escape;
        public MouseButton Interact { get; set; } = MouseButton.Left;
        #endregion
    }
}
