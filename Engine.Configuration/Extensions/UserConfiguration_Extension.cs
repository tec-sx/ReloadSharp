﻿namespace Engine.Configuration.Extensions
{
    using Engine.Configuration;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    public static class UserConfiguration_Extension
    {
        private static readonly string ConfigurationFilePath = Path.Combine(ContentPaths.Configuration, "settings.recfg");
        private static readonly byte[] Header = Encoding.UTF8.GetBytes("YOUDIDNTSAYTHEMAGICWORD");

        public static string GetConfigurationFilePath(this UserConfiguration configuration)
        {
            return ConfigurationFilePath;
        }

        public static void Load(this UserConfiguration destination)
        {
            if (!File.Exists(ConfigurationFilePath))
            {
                destination.Save();
                return;
            }

            var formatter = new BinaryFormatter();
            var stream = new FileStream(ConfigurationFilePath, FileMode.Open);

            try
            {
                var source = formatter.Deserialize(stream) as UserConfiguration;
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
        }

        public static void Save(this UserConfiguration source)
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

        public static void SetOptimalConfiguration(this UserConfiguration destination)
        {
            // TODO: Implement logic to set optimal configuration on first run.
            throw new NotImplementedException();
        }
    }
}
