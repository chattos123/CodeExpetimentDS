using Soumyajit.System.DS.LanguageDS.Data;
using Soumyajit.System.DS.LanguageDS.Dictionary;
using Soumyajit.System.DS.LanguageDS.Storage;


namespace Soumyajit.System.DS.LanguageDictonaryApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var storage = new JsonDictionaryStorage<WordEntry>();
            var englishDict = new EnglishDictionary(storage);

            // 1. Add some data
            englishDict.AddWord("Fast");
            englishDict.AddSynonym("Fast", "Quick");
            englishDict.AddAntonym("Fast", "Slow");
            englishDict.AddSynonym("Fast", "Rapid");

            string filePath = @"C:\soumyajit\data\dictionary\EN\myDictionary.json";
            // 2. Save to disk
            englishDict.Save(filePath);
            Console.WriteLine("Dictionary Saved!");

            // 3. Load from disk
            var newDict = new EnglishDictionary(storage);
            newDict.Load(filePath);

            try
            {                  
                var result = newDict.SearchWord("Fast");
                Console.WriteLine($"Loaded word: {result.Word} has {result.Synonyms.Count} synonyms.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading word: {ex.Message}");
            }

            if (newDict.GetSynonym("Fast", out var synonyms))
            {
                Console.WriteLine("Synonyms found:");

                if (synonyms != null)
                {
                    foreach (var syn in synonyms)
                    {
                        Console.WriteLine($"- {syn}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No synonyms found for 'Fast'.");
            }
        }
    }
}
