using Soumyajit.System.DS.LanguageDS.Interface.Serialize;
using System.Text.Json;


namespace Soumyajit.System.DS.LanguageDS.Storage
{
    public class JsonDictionaryStorage<T> : IDictionaryStorage<T>
    {
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public void SaveToFile(Dictionary<string, T> data, string? filePath = null)
        {
            string jsonString = JsonSerializer.Serialize(data, _options);

            if (string.IsNullOrEmpty(filePath))
            {
                filePath = ConfigProvider.GetFullPath(); // Get the default path from configuration
            }

            File.WriteAllText(filePath, jsonString);

        }

        public Dictionary<string, T> LoadFromFile(string?  filePath = null)
        {

            if (string.IsNullOrEmpty(filePath))
            {
                filePath = ConfigProvider.GetFullPath(); // Get the default path from configuration
            }

            if (!File.Exists(filePath)) return new Dictionary<string, T>();

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, T>>(jsonString, _options)
                   ?? new Dictionary<string, T>();
        }
    }
}
