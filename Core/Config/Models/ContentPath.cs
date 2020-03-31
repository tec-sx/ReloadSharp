namespace Core.Config.Models
{
    using System;
    using System.IO;
    
    public class ContentPath
    {
        public string Music { get; }
        public string Sounds { get; }
        public string Textures { get; }

        public ContentPath(string basePath)
        {
            Music = Path.Combine(basePath, "Music");
            Sounds = Path.Combine(basePath, "Sounds");
            Textures = Path.Combine(basePath, "Textures");
        }
    }
}