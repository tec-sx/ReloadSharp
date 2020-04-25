namespace Engine.Libraries
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public static class LibraryManager
    {
        public static void LoadNativeLibraries()
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
                //NativeLibrary.Load(library);
                Console.WriteLine($"[LOADING LIB]::{library}::SUCCESS");
            }
        }
    }
}