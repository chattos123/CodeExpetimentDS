namespace Soumyajit.System.DS.LanguageDS.Data
{
    /// <summary>
    /// Represents the grammatical gender of a German noun.
    /// </summary>
    public enum Gender
    {
        /// <summary>Masculine gender (der).</summary>
        Masculine,
        /// <summary>Feminine gender (die).</summary>
        Feminine,
        /// <summary>Neuter gender (das).</summary>
        Neuter
    }
    /// <summary>
    /// Represents an entry for a German word, including its grammatical gender and plural form.
    /// Inherits common word metadata from <see cref="WordEntry"/>.
    /// </summary>
    public class GermanWordEntry : WordEntry
    {
        /// <summary>
        /// Gets or sets the grammatical gender of the word.
        /// </summary>
        public Gender WordGender { get; set; }

        /// <summary>
        /// Gets or sets the plural form of the word.
        /// </summary>
        public string PluralForm { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GermanWordEntry"/> class with the specified word, gender, and plural form.
        /// </summary>
        /// <param name="word">The base form of the German word.</param>
        /// <param name="gender">The grammatical gender of the word.</param>
        /// <param name="plural">The plural form of the word.</param>
        public GermanWordEntry(string word, Gender gender, string plural) : base(word)
        {
            WordGender = gender;
            PluralForm = plural;
        }
    }
}
