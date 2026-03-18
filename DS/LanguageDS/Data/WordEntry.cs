namespace Soumyajit.System.DS.LanguageDS.Data
{
    public class WordEntry
    {
        public string Word { get; set; } = string.Empty;
        public HashSet<string> Synonyms { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public HashSet<string> Antonyms { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Parameterless constructor is REQUIRED for JSON deserialization
        public WordEntry() { }

        public WordEntry(string word)
        {
            Word = word;
        }
    }
}
