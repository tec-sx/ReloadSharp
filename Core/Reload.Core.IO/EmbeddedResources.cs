namespace Reload.Core.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class EmbeddedResources
    {
        public static byte[] LoadResourceBytes(Assembly assembly, string name)
        {

            using var stream = GetResourceStream(assembly, name);
            using var memoryStream = new MemoryStream();

            if (stream == null)
            {
                throw new ApplicationException($"Embedded resource {name} not found.");
            }

            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }

        public static string LoadResourceString(Assembly assembly, string name)
        {
            using var stream = GetResourceStream(assembly, name);

            if (stream == null)
            {
                throw new ApplicationException($"Embedded resource {name} not found.");
            }

            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        private static Stream GetResourceStream(Assembly assembly, string name)
        {
            var resourceName = assembly
                .GetManifestResourceNames()
                .Single(resource => resource.EndsWith(name));

            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
