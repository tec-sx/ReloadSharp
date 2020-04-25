namespace Engine.Configuration
{
    internal class SystemConfiguration
    {
        #region Info
        public readonly string ProgramName = "Reload";
        public readonly string ProgramVersion = "0.2";
        #endregion

        #region Graphics
        public readonly int TargetFps = 60;
        #endregion

        #region Audio
        public readonly int AudioFrequency = 44100;
        public readonly int AudioChannels = 2;
        public readonly int AudioChunkSize = 512;
        #endregion

        #region Formats
        public readonly string AudioExtension = "mp3";
        public readonly string ImageExtension = "jpg";
        public readonly string TextureExtension = "png";
        public readonly string ModelExtension = "gltf";
        #endregion
    }
}
