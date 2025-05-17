using System;
using System.Collections.Generic;
using System.Linq;

class TrigramMatcher
{
    // Trigram generatsiya qiluvchi metod
    public static List<string> GenerateTrigrams(string word)
    {
        var trigrams = new List<string>();
        if (word.Length < 3) return trigrams;

        for (int i = 0; i < word.Length - 2; i++)
        {
            trigrams.Add(word.Substring(i, 3));
        }

        return trigrams;
    }

    // Jaccard o'xshashlik koeffitsienti
    public static double CalculateSimilarity(string source, string query)
    {
        var sourceTrigrams = GenerateTrigrams(source);
        var queryTrigrams = GenerateTrigrams(query);

        var intersection = sourceTrigrams.Intersect(queryTrigrams).Count();
        var union = sourceTrigrams.Union(queryTrigrams).Count();

        return union == 0 ? 0 : (double)intersection / union;
    }

    // Ro‘yxatdan eng mos so‘zlarni topish
    public static List<(string word, double score)> MatchWords(string query, List<string> wordList, double threshold = 0.3)
    {
        var results = new List<(string word, double score)>();

        foreach (var word in wordList)
        {
            double score = CalculateSimilarity(word, query);
            if (score >= threshold)
                results.Add((word, score));
        }

        // O'xshashlik bo‘yicha kamayish tartibida saralaymiz
        return results.OrderByDescending(r => r.score).ToList();
    }

    // Test
    public static void Main()
    {
        List<string> dictionary = new List<string>
        {
            "hello", "hallo", "hullo", "help", "helmet", "hell", "yellow"
        };

        string query = "hello";

        var matches = MatchWords(query, dictionary);

        Console.WriteLine($"Trigram matching results for: {query}");
        foreach (var match in matches)
        {
            Console.WriteLine($"{match.word} → Similarity: {match.score:F2}");
        }
    }
}
