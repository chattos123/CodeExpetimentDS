using Soumyajit.System.DS.LanguageDS.Interface.Serialize;
using Soumyajit.System.DS.LanguageDS.Interface.Data;

namespace Soumyajit.System.DS.LanguageDS.Dictionary
{
    public abstract class LanguageDictionaryBase<TEntry> : ILanguageDictionary<TEntry> where TEntry : class
    {
        protected readonly Dictionary<string, TEntry> _storage = new(StringComparer.OrdinalIgnoreCase);
        protected readonly IDictionaryStorage<TEntry>? _storageService;

        protected LanguageDictionaryBase(IDictionaryStorage<TEntry>? storageService = null)
        {
            _storageService = storageService;
        }

        // Optional persistence helpers (not required by ILanguageDictionary)
        public virtual void Save(string fileName)
        {
            if (_storageService is null)
                throw new InvalidOperationException("Storage service is not available. Cannot save dictionary.");

            _storageService.SaveToFile(fileName, _storage);
        }

        public virtual void Load(string fileName)
        {
            if (_storageService is null)
                throw new InvalidOperationException("Storage service is not available. Cannot load dictionary.");

            var loaded = _storageService.LoadFromFile(fileName);
            _storage.Clear();
            foreach (var kv in loaded)
                _storage[kv.Key] = kv.Value;
        }

        // Concrete dictionaries must implement creation semantics for entries
        public abstract void AddWord(string word);

        public virtual bool DeleteWord(string word)
        {
            if (!_storage.TryGetValue(word, out var entry)) return false;

            // Use dynamic to access Synonyms/Antonyms on TEntry (WordEntry / GermanWordEntry)
            dynamic dEntry = entry;
            foreach (string syn in dEntry.Synonyms)
                if (_storage.ContainsKey(syn))
                    ((dynamic)_storage[syn]).Synonyms.Remove(word);

            foreach (string ant in dEntry.Antonyms)
                if (_storage.ContainsKey(ant))
                    ((dynamic)_storage[ant]).Antonyms.Remove(word);

            return _storage.Remove(word);
        }

        public virtual void AddSynonym(string word1, string word2)
        {
            AddWord(word1);
            AddWord(word2);
            ((dynamic)_storage[word1]).Synonyms.Add(word2);
            ((dynamic)_storage[word2]).Synonyms.Add(word1);
        }

        public virtual void AddAntonym(string word1, string word2)
        {
            AddWord(word1);
            AddWord(word2);
            ((dynamic)_storage[word1]).Antonyms.Add(word2);
            ((dynamic)_storage[word2]).Antonyms.Add(word1);
        }

        public virtual bool GetSynonym(string word, out IEnumerable<string>? synonyms)
        {
            if (_storage.TryGetValue(word, out var entry) && ((dynamic)entry).Synonyms.Count > 0)
            {
                synonyms = ((dynamic)entry).Synonyms;
                return true;
            }

            synonyms = null;
            return false;
        }

        public virtual bool GetAntonym(string word, out IEnumerable<string>? antonyms)
        {
            if (_storage.TryGetValue(word, out var entry) && ((dynamic)entry).Antonyms.Count > 0)
            {
                antonyms = ((dynamic)entry).Antonyms;
                return true;
            }

            antonyms = null;
            return false;
        }

        public virtual TEntry SearchWord(string word)
        {
            if (_storage.TryGetValue(word, out var entry) && entry is not null)
                return entry;

            throw new KeyNotFoundException($"Word '{word}' not found in dictionary.");
        }
    }
}