using BenchmarkDotNet.Attributes;
using System;

public class SortingAlgorithms
{
    private int[] data;

    [Params(10, 1000, 100000)]
    public int Size;

    [Params("random", "sorted", "reversed", "almost", "fewunique")]
    public string DataType;

    [GlobalSetup]
    public void Setup()
    {
        switch (DataType)
        {
            case "random":
                data = Generators.GenerateRandom(Size, 1, 100000);
                break;

            case "sorted":
                data = Generators.GenerateSorted(Size, 1, 100000);
                break;

            case "reversed":
                data = Generators.GenerateReversed(Size, 1, 100000);
                break;

            case "almost":
                data = Generators.GenerateAlmostSorted(Size, 1, 100000);
                break;

            case "fewunique":
                data = Generators.GenerateFewUnique(Size);
                break;
        }
    }

    private int[] CloneArray()
    {
        return (int[])data.Clone();
    }

    [Benchmark]
    public void InsertionSort()
    {
        var arr = CloneArray();

        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            arr[j + 1] = key;
        }
    }

    [Benchmark]
    public void MergeSort()
    {
        var arr = CloneArray();
        MergeSortRec(arr, 0, arr.Length - 1);
    }

    private void MergeSortRec(int[] arr, int left, int right)
    {
        if (left >= right) return;

        int mid = (left + right) / 2;

        MergeSortRec(arr, left, mid);
        MergeSortRec(arr, mid + 1, right);

        Merge(arr, left, mid, right);
    }

    private void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

        Array.Copy(arr, left, L, 0, n1);
        Array.Copy(arr, mid + 1, R, 0, n2);

        int i = 0, j = 0, k = left;

        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
                arr[k++] = L[i++];
            else
                arr[k++] = R[j++];
        }

        while (i < n1)
            arr[k++] = L[i++];

        while (j < n2)
            arr[k++] = R[j++];
    }

    [Benchmark]
    public void QuickSortClassical()
    {
        var arr = CloneArray();
        QuickSort(arr, 0, arr.Length - 1);
    }

    private void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high);

            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    private int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }

        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);

        return i + 1;
    }

    [Benchmark]
    public void QuickSortHeuristic()
    {
        var arr = CloneArray();
        Array.Sort(arr);
    }
}