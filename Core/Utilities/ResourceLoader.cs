namespace Core.Utilities
{
    using System;
    using System.IO;

    public class ResourceLoader
    {
        internal static byte[] LoadEmbeddedResourceBytes(string path)
        {
            using var stream = typeof(GameBase).Assembly.GetManifestResourceStream(path);
            using var ms = new MemoryStream();

            if (stream == null)
            {
                throw new ApplicationException($"Embedded resource {path} not found.");
            }

            stream.CopyTo(ms);
            return ms.ToArray();
        }

        public static string LoadEmbeddedResourceString(string path)
        {
            using var stream = typeof(GameBase).Assembly.GetManifestResourceStream(path);

            if (stream == null)
            {
                throw new ApplicationException($"Embedded resource {path} not found.");
            }

            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}