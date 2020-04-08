namespace Core.Utilities
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    
    public static class EmbeddedResourceManager
    {
        internal static byte[] LoadShader(string file)
        {
            return LoadEmbeddedResourceBytes($"Core.Resources.Shaders.{file}");
        }
        
        private static byte[] LoadEmbeddedResourceBytes(string path)
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
        
        internal static void LoadNativeLibraries()
        {
            string platform;

            if (!Environment.Is64BitProcess)
            {
                throw new ApplicationException("Only 64 bit platform supported.");    
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                platform = "Windows";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                platform = "Linux";
            }
            else
            {
                throw new ApplicationException("Platform not recognized.");
            }

            var librariesPath = Path.Combine(Environment.CurrentDirectory, "Libraries", platform);
            var libraries = Directory.EnumerateFiles(librariesPath);
            
            foreach (var library in libraries)
            {
                NativeLibrary.Load(library);
                Console.WriteLine($"[LOADING LIB]::{library}::SUCCESS");
            }
        }
    }
}