using Soumyajit.System.DS.LanguageDS.Data;
using Soumyajit.System.DS.LanguageDS.Interface.Serialize;

namespace Soumyajit.System.DS.LanguageDS.Dictionary
{
    /// <summary>
    /// Concrete dictionary implementation for the English language.
    /// </summary>
    /// <remarks>
    /// Inherits core behaviors from <see cref="LanguageDictionaryBase{WordEntry}"/>.
    /// Uses a case-insensitive internal storage (configured in the base class).
    /// Optionally integrates with an <see cref="IDictionaryStorage{WordEntry}"/> implementation
    /// for persistence via the constructor parameter.
    /// </remarks>
    public class EnglishDictionary : LanguageDictionaryBase<WordEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishDictionary"/> class.
        /// </summary>
        /// <param name="storageService">
        /// Optional persistence service used to save/load dictionary data. If null, no external
        /// persistence is configured and the dictionary operates in-memory only.
        /// </param>
        public EnglishDictionary(IDictionaryStorage<WordEntry>? storageService = null) : base(storageService)
        {
        }

        /// <summary>
        /// Adds a word to the dictionary.
        /// </summary>
        /// <param name="word">The word to add. Must not be null, empty, or whitespace.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="word"/> is null, empty, or whitespace.</exception>
        /// <remarks>
        /// This method creates a new <see cref="WordEntry"/> for the supplied word if it does not
        /// already exist in the internal storage. Key comparisons are case-insensitive due to the
        /// <see cref="LanguageDictionaryBase{TEntry}"/> storage configuration.
        /// </remarks>
        public override void AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentException("word must not be null, empty, or whitespace", nameof(word));

            if (!_storage.ContainsKey(word))
                _storage.Add(word, new WordEntry(word));
        }

        // All other behaviors (DeleteWord, AddSynonym, AddAntonym, GetSynonym, GetAntonym, SearchWord, Save, Load)
        // are inherited from LanguageDictionaryBase.
    }
}
