namespace Soumyajit.System.DS.LanguageDS.Interface.Data
{
    public interface ILanguageDictionary<T>
    {
        void AddWord(string word);
        bool DeleteWord(string word);
        void AddSynonym(string word1, string word2);
        void AddAntonym(string word1, string word2);
        T SearchWord(string word);

        // New relationship lookup functions
        bool GetSynonym(string word, out IEnumerable<string>? synonyms);
        bool GetAntonym(string word, out IEnumerable<string>? antonyms);
    }
}
