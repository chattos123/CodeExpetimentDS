namespace Soumyajit.System.DS.LanguageDS.Interface.Serialize
{
    public interface IDictionaryStorage<T>
    {
        void SaveToFile(string filePath, Dictionary<string, T> data);
        Dictionary<string, T> LoadFromFile(string filePath);
    }
}
