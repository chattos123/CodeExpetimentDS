    using Soumyajit.System.DS.LanguageDS.Data;
    using Soumyajit.System.DS.LanguageDS.Interface.Serialize;

    namespace Soumyajit.System.DS.LanguageDS.Dictionary
    {
        /// <summary>
        /// A dictionary implementation for German words.
        /// Manages German-specific metadata (gender and plural form) via <see cref="GermanWordEntry"/>.
        /// Inherits core dictionary behavior from <see cref="LanguageDictionaryBase{TEntry}"/>.
        /// </summary>
        /// <remarks>
        /// This class provides a language-specific overload for adding words that includes
        /// grammatical gender and plural form, while also implementing the parameterless
        /// AddWord(string) from <see cref="ILanguageDictionary{T}"/> by delegating to the
        /// German-specific overload with sensible defaults.
        /// </remarks>
        public class GermanDictionary : LanguageDictionaryBase<GermanWordEntry>
        {
            /// <summary>
            /// Initializes a new instance of <see cref="GermanDictionary"/>.
            /// </summary>
            /// <param name="storageService">
            /// Optional persistence provider used to save and load dictionary entries.
            /// If null, no external persistence is used and in-memory storage is relied upon.
            /// </param>
            public GermanDictionary(IDictionaryStorage<GermanWordEntry>? storageService = null) : base(storageService)
            {
            }

            /// <summary>
            /// Adds a word to the dictionary using language-default metadata.
            /// </summary>
            /// <param name="word">The word to add. Must not be null or empty.</param>
            /// <remarks>
            /// Implements <see cref="ILanguageDictionary{T}.AddWord(string)"/>.
            /// By default this maps to the German overload using <see cref="Gender.Neuter"/> and
            /// uses the provided word as its plural form.
            /// </remarks>
            public override void AddWord(string word) => AddWord(word, Gender.Neuter, word);

            /// <summary>
            /// Adds a German word with explicit grammatical gender and plural form.
            /// </summary>
            /// <param name="word">The base form of the German word to add.</param>
            /// <param name="gender">The grammatical gender of the word (Masculine/Feminine/Neuter).</param>
            /// <param name="plural">The plural form of the word.</param>
            /// <remarks>
            /// The method only adds the entry if the word is not already present
            /// in the underlying case-insensitive storage.
            /// </remarks>
            public void AddWord(string word, Gender gender, string plural)
            {
                if (!_storage.ContainsKey(word))
                    _storage.Add(word, new GermanWordEntry(word, gender, plural));
            }
        }
    }
