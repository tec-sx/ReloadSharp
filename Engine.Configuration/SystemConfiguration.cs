namespace Engine.Configuration
{
    public static class SystemConfiguration
    {
        #region Info
        public const string ApplicationName = "Reload";
        public const string Version = "0.2";
        #endregion

        #region Graphics
        public const int TargetFps = 60;
        #endregion

        #region Audio
        public const int AudioFrequency = 44100;
        public const int AudioChannels = 2;
        public const int AudioChunkSize = 512;
        #endregion

        #region Formats
        public const string AudioExtension = "ogg";
        public static readonly string[] ImageExtensions = { "png", "jpg" };
        public const string ModelExtension = "gltf";
        #endregion
    }
}
