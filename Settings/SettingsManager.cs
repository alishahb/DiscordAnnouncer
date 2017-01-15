using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Windows;
using Newtonsoft.Json;


namespace Alisha.DiscordAnnouncer.Settings
{
    class SettingsManager<T> where T : class, new()
    {
        public static event EventHandler OnDeserializeFailed;

        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto,
            DefaultValueHandling = DefaultValueHandling.Populate
        };

        public static string Directory { get; set; } = "Settings";
        private static string FileName { get; set; }
        private static string Path => Environment.CurrentDirectory;
        private static string FullPath => System.IO.Path.Combine(Path, Directory, $"{FileName}.json");


        private static T _instance { get; set; }
        static SettingsManager() { }

        public static T Create(string fileName)
        {
            if (_instance != null) return _instance;

            FileName = fileName;

            ValidateDirectory();

            if (!File.Exists(fileName))
                _instance = new T();

            try
            {
                _instance = JsonConvert.DeserializeObject<T>(File.ReadAllText(FullPath), JsonSerializerSettings);
            }
            catch (Exception e)
            {
                _instance = new T();
                OnDeserializeFailed?.Invoke(e, null);
            }
            return _instance;
        }



        private static void ValidateDirectory()
        {
            try
            {
                if (!System.IO.Directory.Exists(Directory))
                    System.IO.Directory.CreateDirectory(Directory);
            }
            catch (Exception)
            {
                throw new AccessViolationException($"Can't create Path: {Directory}");
            }
        }

        public static bool Save()
        {
            try
            {
                File.WriteAllText(FullPath, JsonConvert.SerializeObject(_instance, Formatting.Indented, JsonSerializerSettings));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
