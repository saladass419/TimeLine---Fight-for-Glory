using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticImportantFunction
{
    public static System.Random rng;
    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
