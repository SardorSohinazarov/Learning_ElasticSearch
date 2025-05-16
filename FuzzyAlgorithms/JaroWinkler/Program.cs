// https://en.wikipedia.org/wiki/Jaro%E2%80%93Winkler_distance
// https://www.geeksforgeeks.org/jaro-and-jaro-winkler-similarity/
// https://docs.vertica.com/23.3.x/en/sql-reference/functions/data-type-specific-functions/string-functions/jaro-winkler-distance/
// https://srinivas-kulkarni.medium.com/jaro-winkler-vs-levenshtein-distance-2eab21832fd6

public class Program
{
    private static void Main(string[] args)
    {
        string str1 = "kitten";
        string str2 = "sitting";
        Console.WriteLine(JaroWinklerDistance(str1, str2));
    }

    public static double JaroWinklerDistance(string s1, string s2)
    {
        if (s1 == s2)
            return 1.0;

        int len1 = s1.Length;
        int len2 = s2.Length;

        if (len1 == 0 || len2 == 0)
            return 0.0;

        int matchDistance = Math.Max(len1, len2) / 2 - 1;
        bool[] s1Matches = new bool[len1];
        bool[] s2Matches = new bool[len2];

        int matches = 0;
        for (int i = 0; i < len1; i++)
        {
            int start = Math.Max(0, i - matchDistance);
            int end = Math.Min(i + matchDistance + 1, len2);

            for (int j = start; j < end; j++)
            {
                if (s2Matches[j]) continue;
                if (s1[i] != s2[j]) continue;
                s1Matches[i] = true;
                s2Matches[j] = true;
                matches++;
                break;
            }
        }

        if (matches == 0) return 0.0;

        double t = 0;
        int k = 0;
        for (int i = 0; i < len1; i++)
        {
            if (!s1Matches[i]) continue;
            while (!s2Matches[k]) k++;
            if (s1[i] != s2[k]) t++;
            k++;
        }

        t /= 2.0;

        double jaro = ((matches / (double)len1) +
                       (matches / (double)len2) +
                       ((matches - t) / matches)) / 3.0;

        // Jaro-Winkler adjustment
        int prefix = 0;
        for (int i = 0; i < Math.Min(4, Math.Min(len1, len2)); i++)
        {
            if (s1[i] == s2[i])
                prefix++;
            else
                break;
        }

        double jaroWinkler = jaro + prefix * 0.1 * (1 - jaro);
        return jaroWinkler;
    }
}