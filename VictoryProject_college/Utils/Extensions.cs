namespace VictoryProject_college.Utils;
using System;

public static class ArrayExtensions
{
    private static Random rng = new Random();

    public static void Shuffle(this string[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1); // 0 <= k <= n
            (array[k], array[n]) = (array[n], array[k]); // Swap
        }
    }
}
