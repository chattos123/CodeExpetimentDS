//using Soumyajit.System.DS.LanguageDS.Data;
//using Soumyajit.System.DS.LanguageDS.Interface.Data;
//using Soumyajit.System.DS.LanguageDS.Interface.Serialize;
//using System;
//using System.Collections.Generic;



//namespace Soumyajit.System.DS.LanguageDS.Dictionary
//{
//    //public class EnglishDictionary
//    //{
//    //    private readonly Dictionary<string, WordEntry> _storage = new Dictionary<string, WordEntry>(StringComparer.OrdinalIgnoreCase);

//    //    // Adds a word if it doesn't already exist
//    //    public void AddWord(string word)
//    //    {
//    //        if (!_storage.ContainsKey(word))
//    //        {
//    //            _storage.Add(word, new WordEntry(word));
//    //        }
//    //    }

//    //    // Deletes a word and removes its references from other entries
//    //    public bool DeleteWord(string word)
//    //    {
//    //        if (!_storage.TryGetValue(word, out var entry)) return false;

//    //        // Cleanup: Remove this word from its synonyms' and antonyms' lists
//    //        foreach (var syn in entry.Synonyms)
//    //            if (_storage.ContainsKey(syn)) _storage[syn].Synonyms.Remove(word);

//    //        foreach (var ant in entry.Antonyms)
//    //            if (_storage.ContainsKey(ant)) _storage[ant].Antonyms.Remove(word);

//    //        return _storage.Remove(word);
//    //    }

//    //    // Links two words as synonyms (Bidirectional)
//    //    public void AddSynonym(string word1, string word2)
//    //    {
//    //        AddWord(word1);
//    //        AddWord(word2);
//    //        _storage[word1].Synonyms.Add(word2);
//    //        _storage[word2].Synonyms.Add(word1);
//    //    }

//    //    // Links two words as antonyms (Bidirectional)
//    //    public void AddAntonym(string word1, string word2)
//    //    {
//    //        AddWord(word1);
//    //        AddWord(word2);
//    //        _storage[word1].Antonyms.Add(word2);
//    //        _storage[word2].Antonyms.Add(word1);
//    //    }

//    //    // Finds a word and returns its details
//    //    public WordEntry SearchWord(string word)
//    //    {
//    //        return _storage.TryGetValue(word, out var entry) ? entry : null;
//    //    }
//    //}


//    public class EnglishDictionary : ILanguageDictionary<WordEntry>
//    {
//        private readonly Dictionary<string, WordEntry> _storage = new(StringComparer.OrdinalIgnoreCase);

//        private readonly IDictionaryStorage<WordEntry>? _storageService = null;

//        public EnglishDictionary(IDictionaryStorage<WordEntry> storageService)
//        {
//            _storageService = storageService;
//        }


//        public void Save(string fileName)
//        {
//            if (_storageService != null)
//            {
//                _storageService.SaveToFile(fileName, _storage);
//            }
//        }

//        //public void Load(string fileName)
//        //{
//        //    if (_storageService != null)
//        //    {
//        //        _storageService.LoadFromFile(fileName);
//        //    }
//        //}

//        public void Load(string fileName)
//        {
//            if (_storageService != null)
//            {
//                var loadedData = _storageService.LoadFromFile(fileName);

//                if (loadedData != null)
//                {
//                    _storage.Clear(); // Clear existing data
//                    foreach (var item in loadedData)
//                    {
//                        _storage.Add(item.Key, item.Value);
//                    }
//                    Console.WriteLine($"Load successful. Total entries: {_storage.Count}");
//                }
//            }
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

//        public void AddWord(string word)
//        {
//            if (!_storage.ContainsKey(word))
//                _storage.Add(word, new WordEntry(word));
//        }

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
//        {
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

//        public WordEntry SearchWord(string word)
//        {
//            WordEntry? entry = null;
//            bool bSuccess = _storage.TryGetValue(word, out entry);

//            if (bSuccess)
//            {
//                if(entry != null)
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

//        }
//    }
//}

using Soumyajit.System.DS.LanguageDS.Data;
using Soumyajit.System.DS.LanguageDS.Interface.Serialize;

namespace Soumyajit.System.DS.LanguageDS.Dictionary
{
    public class EnglishDictionary : LanguageDictionaryBase<WordEntry>
    {
        public EnglishDictionary(IDictionaryStorage<WordEntry>? storageService = null) : base(storageService)
        {
        }

        public override void AddWord(string word)
        {
            if (!_storage.ContainsKey(word))
                _storage.Add(word, new WordEntry(word));
        }

        // All other behaviors (DeleteWord, AddSynonym, AddAntonym, GetSynonym, GetAntonym, SearchWord, Save, Load)
        // are inherited from LanguageDictionaryBase.
    }
}
