// https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance#:~:text=Informally%2C%20the%20Damerau%E2%80%93Levenshtein%20distance,one%20word%20into%20the%20other.
// https://www.geeksforgeeks.org/damerau-levenshtein-distance/
// https://medium.com/@meysamagah/damerau-levenshtein-distance-678dde773821

/// <summary>
/// Damerau-Levenshtein distance algorithm implementation.
/// Levenshtein distance dan farqi shundaki
/// Surilib ketgan harflarni ham hisobga oladi
/// Transpozitsiya ya'ni ketma-ket ikki harfni joyini almashtirish (masalan, ad ↔ da) operatsiyasini ham qo‘shadi.
/// </summary>

public class Program
{
    private static void Main(string[] args)
    {
        string str1 = "sardor";
        string str2 = "asardor";
        int distance = DamerauLevenshteinDistance(str1, str2);
        Console.WriteLine($"Damerau Levenshtein distance between '{str1}' and '{str2}' is {distance}.");
    }

    public static int DamerauLevenshteinDistance(string source, string target)
    {
        int lenSrc = source.Length;
        int lenTgt = target.Length;

        int[,] dp = new int[lenSrc + 1, lenTgt + 1];

        for (int i = 0; i <= lenSrc; i++)
            dp[i, 0] = i;

        PrintMatrix(dp);

        for (int j = 0; j <= lenTgt; j++)
            dp[0, j] = j;

        PrintMatrix(dp);

        for (int i = 1; i <= lenSrc; i++)
        {
            for (int j = 1; j <= lenTgt; j++)
            {
                int cost = source[i - 1] == target[j - 1] ? 0 : 1;

                var deletion = dp[i - 1, j] + 1; // o'chirish
                var insertion = dp[i, j - 1] + 1; // qo'shish
                var substitution = dp[i - 1, j - 1] + cost; // almashtirish

                dp[i, j] = Min(deletion, insertion, substitution);

                // Levenshtein distance dan yagona farqi shu
                // transpozitsiya qo'shadi
                if (i > 1 && j > 1 &&
                    source[i - 1] == target[j - 2] &&
                    source[i - 2] == target[j - 1])
                {
                    dp[i, j] = Math.Min(dp[i, j], dp[i - 2, j - 2] + 1);
                }
            }
            
            PrintMatrix(dp);
        }

        return dp[lenSrc, lenTgt];
    }

    private static int Min(int deletion, int insertion, int substitution) 
        => Math.Min(Math.Min(deletion, insertion), substitution);

    public static void PrintMatrix(int[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------------------------");
    }
}