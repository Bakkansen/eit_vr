using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Misc {

    private static readonly System.Random random = new System.Random();
    private static readonly object synlock = new object();    

	public static int RandomNumber(int min, int max) {
        lock (synlock) {
            return random.Next(min, max);
        }
    }

    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
