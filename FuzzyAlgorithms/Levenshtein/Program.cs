// Todolar
// https://en.wikipedia.org/wiki/Levenshtein_distance
// https://medium.com/@art3330/levenshtein-distance-fundamentals-817b6f7f1718
// https://medium.com/@ethannam/understanding-the-levenshtein-distance-equation-for-beginners-c4285a5604f0
// https://medium.com/@anirudhm187/edit-distance-d314bc204350
// https://leetcode.com/problems/edit-distance/submissions/1635705322/

// Lekin
// Tushundim hammasini
// Ikkita so'zni boshidan harflarni birma bir tekshirib keturadi
// xato bo'sa 1 qo'shib keturadi qisqacha asosiy farq
// https://chatgpt.com/share/68276bda-efa0-8005-9cf2-c75870985dea


public class Program
{
    private static void Main(string[] args)
    {
        string str1 = "APPLE";
        string str2 = "APRLE";
        int distance = LevenshteinDistance(str1, str2);
        Console.WriteLine($"Levenshtein distance between '{str1}' and '{str2}' is {distance}.");
    }

    public static int LevenshteinDistance(string a, string b)
    {
        int[,] dp = new int[a.Length + 1, b.Length + 1];

        for (int i = 0; i <= a.Length; i++)
            dp[i, 0] = i;

        PrintMatrix(dp);

        for (int j = 0; j <= b.Length; j++)
            dp[0, j] = j;

        PrintMatrix(dp);

        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                int cost = a[i - 1] == b[j - 1] ? 0 : 1;

                var insertion = dp[i, j - 1] + 1; // qo'shish
                var deletion = dp[i - 1, j] + 1; // o'chirish
                var substitution = dp[i - 1, j - 1] + cost; // almashtirish

                /// Shu joy o'zgarishni minimumini qo'yib ketadi
                dp[i, j] = Min(insertion, deletion, substitution);
            }

            PrintMatrix(dp);
        }

        return dp[a.Length, b.Length];
    }

    private static int Min(int insertion, int deletion, int substitution) 
        => Math.Min(Math.Min(insertion, deletion), substitution);

    private static void PrintMatrix(int[,] matrix)
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