namespace Engine.Configuration
{
    using System;
    using System.IO;

    internal class ContentPaths
    {
        #region Configuration
        public readonly string Configuration = Path.Combine(Environment.CurrentDirectory);
        #endregion

        #region Assets
        private static readonly string Assets = Path.Combine(Environment.CurrentDirectory, "Assets");

        public readonly string Music = Path.Combine(Assets, "Music");
        public readonly string Sounds = Path.Combine(Assets, "Sounds");
        public readonly string Textures = Path.Combine(Assets, "Textures");
        public readonly string Models = Path.Combine(Assets, "Models");
        #endregion
    }
}
