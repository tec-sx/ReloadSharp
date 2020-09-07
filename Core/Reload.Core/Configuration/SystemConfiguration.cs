namespace Reload.Core.Configuration
{
    using Reload.Core.Input.Models;
    using System;

    [Serializable]
    public record SystemConfiguration
    {
        public int UserId { get; init; } = 0;

        #region Info
        
        public const string ProgramName = "Reload";
        public const string ProgramVersion = "0.1";
        
        #endregion

        public DisplayConfiguration Display { get; set; }
        
        #region Audio System
        
        public const int AudioFrequency = 44100;
        public const int AudioChannels = 2;
        public const int AudioChunkSize = 512;
        
        #endregion

        #region Audio User

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

        #region Formats

        public const string AudioExtension = "ogg";
        public const string ImageExtension = "jpg";
        public const string TextureExtension = "png";
        public const string ModelExtension = "gltf";
        
        #endregion
    }
}
