namespace Reload.Configuration
{
    internal static class SystemConfiguration
    {
        #region Info
        public const string ProgramName = "Reload";
        public const string ProgramVersion = "0.2";
        #endregion

        #region Graphics
        public const int TargetFps = 120;
        #endregion

        #region Audio
        public const int AudioFrequency = 44100;
        public const int AudioChannels = 2;
        public const int AudioChunkSize = 512;
        #endregion

        #region Formats
        public const string AudioExtension = "ogg";
        public const string ImageExtension = "jpg";
        public const string TextureExtension = "png";
        public const string ModelExtension = "gltf";
        #endregion
    }
}
