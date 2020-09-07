namespace Reload.Core.Configuration.Extensions
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    /// <summary>
    /// Encapsulationg user configuration extension methods.
    /// </summary>
    public static class UserConfigurationExtension
    {
        private static readonly string ConfigurationFilePath = Path.Combine(ContentPaths.MasterConfiguration, "settings.recfg");
        private static readonly byte[] Header = Encoding.UTF8.GetBytes("YOUDIDNTSAYTHEMAGICWORD");

        /// <summary>
        /// Loads the configuration from a binary file.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>An UserConfiguration.</returns>
        public static SystemConfiguration Load(this SystemConfiguration destination)
        {
#if DEBUG
            if (File.Exists(ConfigurationFilePath))
            {
                File.Delete(ConfigurationFilePath);
            }
#endif
            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (!File.Exists(ConfigurationFilePath))
            {
                destination.Save();
                return destination;
            }

            var formatter = new BinaryFormatter();
            var stream = new FileStream(ConfigurationFilePath, FileMode.Open);

            try
            {
                var source = formatter.Deserialize(stream) as SystemConfiguration;
                var properties = source.GetType().GetProperties();

                foreach (var property in properties)
                {
                    property.SetValue(destination, property.GetValue(source));
                }
            }
            finally
            {
                stream.Close();
            }

            return destination;
        }

        /// <summary>
        /// Saves the configuration to a binary file.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void Save(this SystemConfiguration source)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(ConfigurationFilePath, FileMode.OpenOrCreate);

            try
            {
                formatter.Serialize(stream, source);
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        /// Sets the optimal configuration for the machine that the game is running on.
        /// </summary>
        /// <param name="destination">The destination.</param>
        public static void SetOptimalConfiguration(this SystemConfiguration destination)
        {
            // TODO: Implement logic to set optimal configuration on first run.
            throw new NotImplementedException();
        }
    }
}
