namespace Reload.Configuration
{
    using System;
    using System.IO;

    public static class ContentPaths
    {
        #region Configuration

        public static readonly string Configuration = Path.Combine(Environment.CurrentDirectory);
        #endregion

        #region Assets
        private static readonly string Assets = Path.Combine(Environment.CurrentDirectory, "Assets");

        public static readonly string Music = Path.Combine(Assets, "Music");
        public static readonly string Sounds = Path.Combine(Assets, "Sounds");
        public static readonly string Textures = Path.Combine(Assets, "Textures");
        public static readonly string Models = Path.Combine(Assets, "Models");
        public static readonly string Fonts = Path.Combine(Assets, "Fonts");
        public static readonly string Shaders = Path.Combine(Assets, "Shaders");
        #endregion
    }
}
