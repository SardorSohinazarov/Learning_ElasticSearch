// Juda sodda bu harflarni raqamlar bilan almashtiradi
// Bir xil raqamli harlar bir xil code hosil qiladi
// Boshidagi 4 ta harfni olib qoladi halos
// Shunaqa juda tez va noto'gri algorithm

using System.Text;

public class Program
{
    private static void Main(string[] args)
    {
        string word1 = "example";
        string soundexCode1 = Soundex(word1);
        Console.WriteLine($"Soundex code for '{word1}' is: {soundexCode1}");

        string word2 = "exam";
        string soundexCode2 = Soundex(word2);
        Console.WriteLine($"Soundex code for '{word2}' is: {soundexCode2}");

        Console.WriteLine(soundexCode1[..4] == soundexCode2[..4]);
    }

    public static string Soundex(string word)
    {
        if (string.IsNullOrEmpty(word))
            return "";

        word = word.ToUpperInvariant();
        char firstLetter = word[0];

        Dictionary<char, char> codes = new Dictionary<char, char>
        {
            {'B','1'}, {'F','1'}, {'P','1'}, {'V','1'},
            {'C','2'}, {'G','2'}, {'J','2'}, {'K','2'}, {'Q','2'}, {'S','2'}, {'X','2'}, {'Z','2'},
            {'D','3'}, {'T','3'},
            {'L','4'},
            {'M','5'}, {'N','5'},
            {'R','6'}
        };

        var result = new StringBuilder();
        result.Append(firstLetter);

        char? previousCode = codes.ContainsKey(firstLetter) ? codes[firstLetter] : (char?)null;

        for (int i = 1; i < word.Length; i++)
        {
            char c = word[i];
            if (codes.TryGetValue(c, out char code))
            {
                if (code != previousCode)
                {
                    result.Append(code);
                    previousCode = code;
                }
            }
            else
            {
                previousCode = null;
            }

            if (result.Length == 4)
                break;
        }

        while (result.Length < 4)
            result.Append('0');

        return result.ToString();
    }
}