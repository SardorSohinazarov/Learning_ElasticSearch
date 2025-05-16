// https://en.wikipedia.org/wiki/Metaphone

using System.Text;

public class Program
{
    private static void Main(string[] args)
    {
        string word1 = "example";
        string metaphoneCode1 = GetMetaphone(word1);
        Console.WriteLine($"Metaphone code for '{word1}' is: {metaphoneCode1}");
        string word2 = "exam";
        string metaphoneCode2 = GetMetaphone(word2);
        Console.WriteLine($"Metaphone code for '{word2}' is: {metaphoneCode2}");
        Console.WriteLine(metaphoneCode1 == metaphoneCode2);
    }

    public static string GetMetaphone(string word)
    {
        if (string.IsNullOrEmpty(word)) return string.Empty;

        word = word.ToUpperInvariant();

        // Ignore non-alphabetical characters
        var sbClean = new StringBuilder();
        foreach (char c in word)
        {
            if (char.IsLetter(c))
                sbClean.Append(c);
        }

        word = sbClean.ToString();
        if (word.Length == 0) return string.Empty;

        StringBuilder result = new StringBuilder();
        int current = 0;
        int length = word.Length;

        // Remove duplicate adjacent letters, except for C
        var sb = new StringBuilder();
        sb.Append(word[0]);
        for (int i = 1; i < word.Length; i++)
        {
            if (word[i] != word[i - 1] || word[i] == 'C')
                sb.Append(word[i]);
        }
        word = sb.ToString();
        length = word.Length;

        // Drop silent letters at the beginning
        if (word.StartsWith("KN") || word.StartsWith("GN") || word.StartsWith("PN") || word.StartsWith("AE") || word.StartsWith("WR"))
            word = word.Substring(1);

        length = word.Length;

        while (current < length)
        {
            char c = word[current];
            char next = current + 1 < length ? word[current + 1] : '\0';

            switch (c)
            {
                case 'A':
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                    if (current == 0)
                        result.Append(c);
                    break;

                case 'B':
                    if (!(current == length - 1 && word[current - 1] == 'M'))
                        result.Append('B');
                    break;

                case 'C':
                    if (next == 'H')
                    {
                        result.Append('X');
                        current++;
                    }
                    else if (next == 'I' || next == 'E' || next == 'Y')
                    {
                        result.Append('S');
                    }
                    else
                    {
                        result.Append('K');
                    }
                    break;

                case 'D':
                    if (next == 'G')
                    {
                        char afterNext = current + 2 < length ? word[current + 2] : '\0';
                        if (afterNext == 'E' || afterNext == 'I' || afterNext == 'Y')
                        {
                            result.Append('J');
                            current += 2;
                        }
                        else
                        {
                            result.Append('T');
                        }
                    }
                    else
                    {
                        result.Append('T');
                    }
                    break;

                case 'G':
                    if (next == 'H')
                    {
                        char afterNext = current + 2 < length ? word[current + 2] : '\0';
                        if (afterNext == '\0' || !"AEIOU".Contains(afterNext))
                        {
                            break;
                        }
                        else
                        {
                            result.Append('K');
                            current++;
                        }
                    }
                    else if (next == 'N')
                    {
                        break;
                    }
                    else if (next == 'E' || next == 'I' || next == 'Y')
                    {
                        result.Append('J');
                    }
                    else
                    {
                        result.Append('K');
                    }
                    break;

                case 'H':
                    if (current == 0 || !"AEIOU".Contains(word[current - 1]) || !"AEIOU".Contains(next))
                        break;
                    result.Append('H');
                    break;

                case 'F':
                case 'J':
                case 'L':
                case 'M':
                case 'N':
                case 'R':
                    result.Append(c);
                    break;

                case 'K':
                    if (word[current - 1] != 'C')
                        result.Append('K');
                    break;

                case 'P':
                    result.Append(next == 'H' ? 'F' : 'P');
                    break;

                case 'Q':
                    result.Append('K');
                    break;

                case 'S':
                    if (next == 'H')
                    {
                        result.Append('X');
                        current++;
                    }
                    else if (next == 'I' && (current + 2 < length && (word[current + 2] == 'O' || word[current + 2] == 'A')))
                    {
                        result.Append('X');
                        current += 2;
                    }
                    else
                    {
                        result.Append('S');
                    }
                    break;

                case 'T':
                    if (next == 'I' && (current + 2 < length && (word[current + 2] == 'O' || word[current + 2] == 'A')))
                    {
                        result.Append('X');
                        current += 2;
                    }
                    else if (next == 'H')
                    {
                        result.Append('0'); // "th" sound
                        current++;
                    }
                    else if (word.Substring(current, Math.Min(3, length - current)) != "TCH")
                    {
                        result.Append('T');
                    }
                    break;

                case 'V':
                    result.Append('F');
                    break;

                case 'W':
                case 'Y':
                    if ("AEIOU".Contains(next))
                        result.Append(c);
                    break;

                case 'X':
                    result.Append("KS");
                    break;

                case 'Z':
                    result.Append('S');
                    break;
            }

            current++;
        }

        return result.ToString().Substring(0, Math.Min(6, result.Length)); // Cheklangan uzunlik
    }
}
