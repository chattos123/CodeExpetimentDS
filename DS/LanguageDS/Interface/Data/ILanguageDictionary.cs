namespace Soumyajit.System.DS.LanguageDS.Interface.Data
{
    /// <summary>
    /// Represents a generic language dictionary that stores words and relationships
    /// such as synonyms and antonyms. Implementations are responsible for managing
    /// the underlying storage and relationship consistency.
    /// </summary>
    /// <typeparam name="T">
    /// The type returned by <see cref="SearchWord(string)"/> when a word is looked up.
    /// This can be a domain model, metadata object, or any value providing word details.
    /// </typeparam>
    public interface ILanguageDictionary<T>
    {
        /// <summary>
        /// Adds a word to the dictionary.
        /// </summary>
        /// <param name="word">The word to add. Must not be null or empty.</param>
        void AddWord(string word);

        /// <summary>
        /// Deletes a word from the dictionary.
        /// </summary>
        /// <param name="word">The word to delete.</param>
        /// <returns>
        /// <c>true</c> if the word was found and removed; otherwise <c>false</c>.
        /// </returns>
        bool DeleteWord(string word);

        /// <summary>
        /// Adds a synonym relationship between two words.
        /// Implementations should ensure the relationship is recorded according to
        /// the dictionary's semantics (e.g., symmetric if desired).
        /// </summary>
        /// <param name="word1">The first word in the synonym relationship.</param>
        /// <param name="word2">The second word in the synonym relationship.</param>
        void AddSynonym(string word1, string word2);

        /// <summary>
        /// Adds an antonym relationship between two words.
        /// Implementations should ensure relationship semantics are preserved.
        /// </summary>
        /// <param name="word1">The first word in the antonym relationship.</param>
        /// <param name="word2">The second word in the antonym relationship.</param>
        void AddAntonym(string word1, string word2);

        /// <summary>
        /// Searches for a word in the dictionary and returns associated data.
        /// </summary>
        /// <param name="word">The word to search for.</param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> containing the word data.
        /// Implementations may return a default or throw if the word is not found,
        /// depending on chosen contract semantics.
        /// </returns>
        T SearchWord(string word);

        /// <summary>
        /// Attempts to retrieve synonyms for the specified word.
        /// </summary>
        /// <param name="word">The word whose synonyms are requested.</param>
        /// <param name="synonyms">
        /// When this method returns, contains an <see cref="IEnumerable{String}"/>
        /// of synonyms if any are found; otherwise, <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if at least one synonym was found and assigned to
        /// <paramref name="synonyms"/>; otherwise, <c>false</c>.
        /// </returns>
        bool GetSynonym(string word, out IEnumerable<string>? synonyms);

        /// <summary>
        /// Attempts to retrieve antonyms for the specified word.
        /// </summary>
        /// <param name="word">The word whose antonyms are requested.</param>
        /// <param name="antonyms">
        /// When this method returns, contains an <see cref="IEnumerable{String}"/>
        /// of antonyms if any are found; otherwise, <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if at least one antonym was found and assigned to
        /// <paramref name="antonyms"/>; otherwise, <c>false</c>.
        /// </returns>
        bool GetAntonym(string word, out IEnumerable<string>? antonyms);
    }
}
