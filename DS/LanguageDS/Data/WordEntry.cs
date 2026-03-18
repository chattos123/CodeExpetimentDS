namespace Soumyajit.System.DS.LanguageDS.Data
{
    /// <summary>
    /// Represents a lexical entry for a word and its related terms.
    /// </summary>
    /// <remarks>
    /// The class stores the main word along with collections of synonyms and antonyms.
    /// Collections use a case-insensitive comparer to avoid duplicate entries that differ only by case.
    /// A parameterless constructor is provided to support JSON deserialization.
    /// </remarks>
    public class WordEntry
    {
        /// <summary>
        /// The primary word for this entry.
        /// </summary>
        public string Word { get; set; } = string.Empty;

        /// <summary>
        /// A set of synonyms for the <see cref="Word"/>.
        /// </summary>
        /// <value>A case-insensitive <see cref="HashSet{String}"/>.</value>
        public HashSet<string> Synonyms { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// A set of antonyms for the <see cref="Word"/>.
        /// </summary>
        /// <value>A case-insensitive <see cref="HashSet{String}"/>.</value>
        public HashSet<string> Antonyms { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="WordEntry"/> class.
        /// </summary>
        /// <remarks>
        /// Parameterless constructor is required for JSON deserialization.
        /// </remarks>
        public WordEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WordEntry"/> class with the specified word.
        /// </summary>
        /// <param name="word">The word to initialize this entry with.</param>
        public WordEntry(string word)
        {
            Word = word;
        }
    }
}
