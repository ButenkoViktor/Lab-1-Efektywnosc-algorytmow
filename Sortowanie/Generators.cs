using System;

public static class Generators
{
    static Random rand = new Random();

    public static int[] GenerateRandom(int size, int minVal, int maxVal)
    {
        int[] a = new int[size];

        for (int i = 0; i < size; i++)
        {
            a[i] = rand.Next(minVal, maxVal);
        }

        return a;
    }

    public static int[] GenerateSorted(int size, int minVal, int maxVal)
    {
        int[] a = GenerateRandom(size, minVal, maxVal);
        Array.Sort(a);
        return a;
    }

    public static int[] GenerateReversed(int size, int minVal, int maxVal)
    {
        int[] a = GenerateSorted(size, minVal, maxVal);
        Array.Reverse(a);
        return a;
    }

    public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal)
    {
        int[] a = GenerateSorted(size, minVal, maxVal);

        int swaps = size / 20;

        for (int i = 0; i < swaps; i++)
        {
            int i1 = rand.Next(size);
            int i2 = rand.Next(size);

            (a[i1], a[i2]) = (a[i2], a[i1]);
        }

        return a;
    }

    public static int[] GenerateFewUnique(int size)
    {
        int[] a = new int[size];

        for (int i = 0; i < size; i++)
        {
            a[i] = rand.Next(1, 10);
        }

        return a;
    }
}