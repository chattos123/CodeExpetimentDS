namespace Soumyajit.System.DS.LanguageDS.Interface.Serialize
{
    /// <summary>
    /// Contract for saving and loading dictionary-based data to and from storage.
    /// Implementers provide the concrete persistence mechanism (e.g., JSON, XML, binary, database, etc.).
    /// </summary>
    /// <typeparam name="T">The type of values stored in the dictionary.</typeparam>
    public interface IDictionaryStorage<T>
    {
        /// <summary>
        /// Persist the provided dictionary to a file or other storage medium.
        /// </summary>
        /// <param name="data">The dictionary data to persist. Must not be null.</param>
        /// <param name="filePath">
        /// Optional file path or storage identifier. If null, the implementation should use a sensible default
        /// location or naming convention.
        /// </param>
        /// <remarks>
        /// Implementations should document:
        /// - Any exceptions thrown (e.g., I/O errors, serialization errors).
        /// - Whether the operation is atomic.
        /// - Thread-safety guarantees, if any.
        /// </remarks>
        void SaveToFile(Dictionary<string, T> data, string? filePath = null);

        /// <summary>
        /// Load dictionary data from a file or other storage medium.
        /// </summary>
        /// <param name="filePath">
        /// Optional file path or storage identifier. If null, the implementation should use the same default
        /// location convention used by <see cref="SaveToFile(Dictionary{string, T}, string?)"/>.
        /// </param>
        /// <returns>
        /// A dictionary containing the persisted data. Implementations may return an empty dictionary if no data
        /// exists at the target location, or throw an exception if loading fails—behavior should be documented by
        /// the concrete implementation.
        /// </returns>
        /// <remarks>
        /// Implementations should document:
        /// - Whether a missing file results in an empty dictionary or an exception.
        /// - Exceptions thrown for malformed or incompatible persisted data.
        /// - Thread-safety guarantees, if any.
        /// </remarks>
        Dictionary<string, T> LoadFromFile(string? filePath = null);
    }
}
