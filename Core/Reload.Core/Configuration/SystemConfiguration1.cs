namespace Reload.Core.Configuration
{
    using Reload.Graphics;
    using System;

    [Serializable]
    public record SystemConfiguration
    {
        public int UserId { get; init; } = 0;

        public DisplayConfiguration Display { get; set; }

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
