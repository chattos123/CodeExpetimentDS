using System;
using System.Configuration;
using System.IO;

namespace Soumyajit.System.DS.LanguageDS.Storage
{
    public static class ConfigurationHelper
    {
        public static string GetStoragePath()
        {
            // 1. Try to get from App.config
            string? path = ConfigurationManager.AppSettings["StoragePath"];

            // 2. Fallback: If null or empty, use the application's execution folder
            if (string.IsNullOrWhiteSpace(path))
            {
                // Use the "Data" folder inside your Debug/Release folder
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DictionaryData");
                Console.WriteLine($"[Warning] 'StoragePath' not found in config. Using fallback: {path}");
            }

            // 3. Safety Check: Ensure the directory actually exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public static string GetFileName(string languageKey = "EnglishFile", string defaultName = "en_dict.json")
        {
            string? fileName = ConfigurationManager.AppSettings[languageKey];

            if (string.IsNullOrWhiteSpace(fileName)) 
            {
                fileName = defaultName;
            }

            return fileName;
        }
    }
}
