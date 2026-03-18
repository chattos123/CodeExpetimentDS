namespace Soumyajit.System.DS.LanguageDS.Data
{
    public enum Gender { Masculine, Feminine, Neuter }
    public class GermanWordEntry : WordEntry
    {
        public Gender WordGender { get; set; }
        public string PluralForm { get; set; }

        public GermanWordEntry(string word, Gender gender, string plural) : base(word)
        {
            WordGender = gender;
            PluralForm = plural;
        }
    }
}
