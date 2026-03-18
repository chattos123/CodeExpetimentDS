using Soumyajit.System.DS.LanguageDS.Interface.Serialize;
using Soumyajit.System.DS.LanguageDS.Interface.Data;

namespace Soumyajit.System.DS.LanguageDS.Dictionary
{
    /// <summary>
    /// Base implementation for a language dictionary that stores entries and manages
    /// relationships such as synonyms and antonyms.
    /// </summary>
    /// <typeparam name="TEntry">
    /// The type returned by <see cref="SearchWord(string)"/> and stored in the internal dictionary.
    /// This type is expected to expose Synonyms and Antonyms collections when accessed dynamically
    /// (e.g., <c>WordEntry</c> or <c>GermanWordEntry</c>).
    /// </typeparam>
    /// <remarks>
    /// The class maintains an in-memory dictionary with case-insensitive keys.
    /// An optional <see cref="IDictionaryStorage{T}"/> may be provided to enable persistence via
    /// <see cref="Save(string?)"/> and <see cref="Load(string?)"/>. Concrete subclasses must
    /// implement how entries are created by overriding <see cref="AddWord(string)"/>.
    /// </remarks>
    public abstract class LanguageDictionaryBase<TEntry> : ILanguageDictionary<TEntry> where TEntry : class
    {
        protected readonly Dictionary<string, TEntry> _storage = new(StringComparer.OrdinalIgnoreCase);
        protected readonly IDictionaryStorage<TEntry>? _storageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageDictionaryBase{TEntry}"/> class.
        /// </summary>
        /// <param name="storageService">
        /// Optional storage service used for persisting and loading dictionary data.
        /// If <c>null</c>, persistence methods will throw <see cref="InvalidOperationException"/>.
        /// </param>
        protected LanguageDictionaryBase(IDictionaryStorage<TEntry>? storageService = null)
        {
            _storageService = storageService;
        }

        /// <summary>
        /// Persist the current in-memory dictionary to storage.
        /// </summary>
        /// <param name="fileName">
        /// Optional file name or storage identifier. If <c>null</c>, the underlying storage
        /// implementation's default location will be used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no storage service was provided to the constructor.
        /// </exception>
        public virtual void Save(string? fileName = null)
        {
            if (_storageService is null)
                throw new InvalidOperationException("Storage service is not available. Cannot save dictionary.");

            if (fileName == null)
            {
                _storageService.SaveToFile(_storage);
            }
            else
            {
                _storageService.SaveToFile(_storage, fileName);
            }
        }



        /// <summary>
        /// Load dictionary data from the provided storage service into the in-memory dictionary.
        /// Existing in-memory entries will be cleared and replaced by the loaded data.
        /// </summary>
        /// <param name="fileName">
        /// Optional file name or storage identifier. If <c>null</c>, the underlying storage
        /// implementation's default location will be used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no storage service was provided to the constructor.
        /// </exception>
        public virtual void Load(string? fileName = null)
        {
            if (_storageService is null)
                throw new InvalidOperationException("Storage service is not available. Cannot load dictionary.");

            Dictionary<string, TEntry> loaded;
            if (fileName == null)
            {
                loaded = _storageService.LoadFromFile();
            }
            else
            {
                loaded = _storageService.LoadFromFile(fileName);
            }

            _storage.Clear();
            foreach (var kv in loaded)
                _storage[kv.Key] = kv.Value;
        }

        /// <summary>
        /// Add a word to the dictionary.
        /// </summary>
        /// <param name="word">The word to add. Must not be <c>null</c> or empty.</param>
        /// <remarks>
        /// Concrete implementations define how a <typeparamref name="TEntry"/> instance is created
        /// and inserted into the underlying storage.
        /// </remarks>
        public abstract void AddWord(string word);

        /// <summary>
        /// Delete a word from the dictionary and remove it from any related synonym/antonym lists.
        /// </summary>
        /// <param name="word">The word to delete.</param>
        /// <returns>
        /// <c>true</c> if the word existed and was removed; otherwise <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Add a symmetric synonym relationship between two words.
        /// </summary>
        /// <param name="word1">The first word in the synonym relationship.</param>
        /// <param name="word2">The second word in the synonym relationship.</param>
        /// <remarks>
        /// Both words will be added to the dictionary if they do not already exist.
        /// The method assumes underlying entry types expose a <c>Synonyms</c> collection.
        /// </remarks>
        public virtual void AddSynonym(string word1, string word2)
        {
            AddWord(word1);
            AddWord(word2);
            ((dynamic)_storage[word1]).Synonyms.Add(word2);
            ((dynamic)_storage[word2]).Synonyms.Add(word1);
        }

        /// <summary>
        /// Add a symmetric antonym relationship between two words.
        /// </summary>
        /// <param name="word1">The first word in the antonym relationship.</param>
        /// <param name="word2">The second word in the antonym relationship.</param>
        /// <remarks>
        /// Both words will be added to the dictionary if they do not already exist.
        /// The method assumes underlying entry types expose an <c>Antonyms</c> collection.
        /// </remarks>
        public virtual void AddAntonym(string word1, string word2)
        {
            AddWord(word1);
            AddWord(word2);
            ((dynamic)_storage[word1]).Antonyms.Add(word2);
            ((dynamic)_storage[word2]).Antonyms.Add(word1);
        }

        /// <summary>
        /// Attempt to retrieve synonyms for the specified word.
        /// </summary>
        /// <param name="word">The word whose synonyms are requested.</param>
        /// <param name="synonyms">
        /// When this method returns, contains an <see cref="IEnumerable{String}"/> of synonyms if any were found;
        /// otherwise <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if at least one synonym was found; otherwise <c>false</c>.
        /// </returns>
        public virtual bool GetSynonym(string word, out IEnumerable<string>? synonyms)
        {
            synonyms = null;
            if (_storage.TryGetValue(word, out var entry))
            {
                var dynEntry = (dynamic)entry;
                if (dynEntry?.Synonyms is IEnumerable<string> syns && syns.Any())
                {
                    synonyms = syns;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Attempt to retrieve antonyms for the specified word.
        /// </summary>
        /// <param name="word">The word whose antonyms are requested.</param>
        /// <param name="antonyms">
        /// When this method returns, contains an <see cref="IEnumerable{String}"/> of antonyms if any were found;
        /// otherwise <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if at least one antonym was found; otherwise <c>false</c>.
        /// </returns>
        public virtual bool GetAntonym(string word, out IEnumerable<string>? antonyms)
        {
            antonyms = null;
            if (_storage.TryGetValue(word, out var entry))
            {
                var dynEntry = (dynamic)entry;
                if (dynEntry?.Antonyms is IEnumerable<string> ants && ants.Any())
                {
                    antonyms = ants;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Search for a word and return its associated entry.
        /// </summary>
        /// <param name="word">The word to search for.</param>
        /// <returns>The stored <typeparamref name="TEntry"/> associated with the word.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the word is not present in the dictionary.</exception>
        public virtual TEntry SearchWord(string word)
        {
            if (_storage.TryGetValue(word, out var entry) && entry is not null)
                return entry;

            throw new KeyNotFoundException($"Word '{word}' not found in dictionary.");
        }
    }
}