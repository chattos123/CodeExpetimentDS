using System;
using System.Configuration;
using System.IO;

namespace Soumyajit.System.DS.LanguageDS.Storage
{
    public static class ConfigProvider
    {
        public static string GetFullPath()
        {
            
            string? path = ConfigurationHelper.GetStoragePath();
            string? file = ConfigurationHelper.GetFileName();

            if (string.IsNullOrEmpty(path))
                throw new InvalidOperationException("StoragePath configuration is missing or empty.");
            if (string.IsNullOrEmpty(file))
                throw new InvalidOperationException("FileName configuration is missing or empty.");

            return Path.Combine(path, file);
        }
    }
}
