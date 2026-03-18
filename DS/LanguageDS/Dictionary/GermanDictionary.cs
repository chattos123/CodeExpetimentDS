//using System;
//using System.Collections.Generic;
//using Soumyajit.System.DS.LanguageDS.Data;
//using Soumyajit.System.DS.LanguageDS.Interface.Data;

//namespace Soumyajit.System.DS.LanguageDS.Dictionary
//{
//    public class GermanDictionary : ILanguageDictionary<GermanWordEntry>
//    {
//        private readonly Dictionary<string, GermanWordEntry> _storage = new(StringComparer.OrdinalIgnoreCase);

//        // Implementation of interface methods...
//        public void AddWord(string word) => AddWord(word, Gender.Neuter, word); // Default

//        // Overloaded method specific to German
//        public void AddWord(string word, Gender gender, string plural)
//        {
//            if (!_storage.ContainsKey(word))
//                _storage.Add(word, new GermanWordEntry(word, gender, plural));
//        }

//        public GermanWordEntry SearchWord(string word)
//        {
//            GermanWordEntry? entry = null;
//            bool bSuccess = _storage.TryGetValue(word, out entry);

//            if (bSuccess)
//            {
//                if (entry != null)
//                {
//                    return entry;
//                }
//                else
//                {
//                    throw new Exception();
//                }
//            }
//            else
//            {
//                throw new Exception();
//            }
//            //return _storage.TryGetValue(word, out var entry) ? entry : null;
//        }

//        // Interface requires these, even if logic is similar to English
//        public bool DeleteWord(string word)
//        {
//            if (!_storage.TryGetValue(word, out var entry)) return false;

//            foreach (var syn in entry.Synonyms)
//                if (_storage.ContainsKey(syn)) _storage[syn].Synonyms.Remove(word);

//            foreach (var ant in entry.Antonyms)
//                if (_storage.ContainsKey(ant)) _storage[ant].Antonyms.Remove(word);

//            return _storage.Remove(word);
//        }

//        public void AddSynonym(string word1, string word2) 
//        { /* Logic similar to English */
//            AddWord(word1);
//            AddWord(word2);
//            _storage[word1].Synonyms.Add(word2);
//            _storage[word2].Synonyms.Add(word1);
//        }
//        public void AddAntonym(string word1, string word2)
//        {
//            AddWord(word1);
//            AddWord(word2);
//            _storage[word1].Antonyms.Add(word2);
//            _storage[word2].Antonyms.Add(word1);
//        }

//        public bool GetSynonym(string word, out IEnumerable<string>? synonyms)
//        {
//            if (_storage.TryGetValue(word, out var entry) && entry.Synonyms.Count > 0)
//            {
//                synonyms = entry.Synonyms;
//                return true;
//            }

//            synonyms = null;
//            return false;
//        }

//        public bool GetAntonym(string word, out IEnumerable<string>? antonyms)
//        {
//            if (_storage.TryGetValue(word, out var entry) && entry.Antonyms.Count > 0)
//            {
//                antonyms = entry.Antonyms;
//                return true;
//            }

//            antonyms = null;
//            return false;
//        }
//    }
//}

using Soumyajit.System.DS.LanguageDS.Data;
using Soumyajit.System.DS.LanguageDS.Interface.Serialize;

namespace Soumyajit.System.DS.LanguageDS.Dictionary
{
    public class GermanDictionary : LanguageDictionaryBase<GermanWordEntry>
    {
        public GermanDictionary(IDictionaryStorage<GermanWordEntry>? storageService = null) : base(storageService)
        {
        }

        // ILanguageDictionary<string> implementation: default AddWord maps to German overload
        public override void AddWord(string word) => AddWord(word, Gender.Neuter, word);

        // Overloaded method specific to German
        public void AddWord(string word, Gender gender, string plural)
        {
            if (!_storage.ContainsKey(word))
                _storage.Add(word, new GermanWordEntry(word, gender, plural));
        }

        // Other behaviors are inherited from LanguageDictionaryBase.
    }
}
