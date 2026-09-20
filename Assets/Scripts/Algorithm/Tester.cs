using CustomMath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlgorithmTester : MonoBehaviour
{
    [Header("Array Config")]
    [Tooltip("Size")]
    public int arraySize = 100;

    [Tooltip("Min (inclusive)")]
    public int minValue = 0;

    [Tooltip("Max (inclusive)")]
    public int maxValue = 1000;

    private void Start()
    {
        StartCoroutine(RunAllTestsCoroutine());
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(RunAllTestsCoroutine());
        }
    }

    private IEnumerator RunAllTestsCoroutine()
    {
        int[] originalArray = GenerateRandomArray();

        UnityEngine.Debug.Log($"<color=cyan>--- Start Test (size {arraySize}) ---</color>");

        List<Task<string>> pendingTasks = new List<Task<string>>
        {
             //O(n!)
            Task.Run(() => RunTest("Bogo Sort", Algorithm.BogoSort, originalArray)),
            
            //O(n^2)
            Task.Run(() => RunTest("Selection Sort", Algorithm.SelectionSort, originalArray)),
            Task.Run(() => RunTest("Bubble Sort", Algorithm.BubbleSort, originalArray)),
            Task.Run(() => RunTest("Insertion Sort", Algorithm.InsertionSort, originalArray)),
            Task.Run(() => RunTest("Cocktail Shaker Sort", Algorithm.CocktailShakerSort, originalArray)),
            Task.Run(() => RunTest("Gnome Sort", Algorithm.GnomeSort, originalArray)),

            //O(n^3/2) - O(n log^2 n)
            Task.Run(() => RunTest("Shell Sort", Algorithm.ShellSort, originalArray)),
            Task.Run(() => RunTest("Bitonic Sort", Algorithm.BitonicSort, originalArray)),

            //O(n log n)
            Task.Run(() => RunTest("Quick Sort", Algorithm.QuickSort, originalArray)),
            Task.Run(() => RunTest("Merge Sort", Algorithm.MergeSort, originalArray)),
            Task.Run(() => RunTest("Heap Sort", Algorithm.HeapSort, originalArray)),
            Task.Run(() => RunTest("Intro Sort", Algorithm.IntroSort, originalArray)),
            Task.Run(() => RunTest("Adaptive Merge Sort", Algorithm.AdaptiveMergeSort, originalArray)),

            //O(n)
            Task.Run(() => RunTest("Radix Sort (LSD)", Algorithm.RadixSortLSD, originalArray)),
            Task.Run(() => RunTest("Radix Sort (MSD)", Algorithm.RadixSortMSD, originalArray))
        };

        while (pendingTasks.Count > 0)
        {
            for (int i = pendingTasks.Count - 1; i >= 0; i--)
            {
                if (pendingTasks[i].IsCompleted)
                {
                    UnityEngine.Debug.Log(pendingTasks[i].Result);

                    pendingTasks.RemoveAt(i);
                }
            }
            yield return null;
        }

        UnityEngine.Debug.Log("<color=cyan>--- Tests have ended ---</color>");
    }

    private int[] GenerateRandomArray()
    {
        int[] arr = new int[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            arr[i] = UnityEngine.Random.Range(minValue, maxValue);
        }
        return arr;
    }

    private string RunTest(string name, Action<int[]> sortMethod, int[] originalArray)
    {
        int[] arrayCopy = new int[originalArray.Length];
        Array.Copy(originalArray, arrayCopy, originalArray.Length);

        bool success = false;
        Stopwatch stopwatch = new Stopwatch();

        try
        {
            stopwatch.Start();
            sortMethod(arrayCopy);
            stopwatch.Stop();

            success = IsSorted(arrayCopy);
        }
        catch (NotImplementedException)
        {
            stopwatch.Stop();
            success = false;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            return $"{name}: Failed: {ex.Message} ({stopwatch.ElapsedMilliseconds} ms)";
        }

        return $"{name}: {(success ? "Success" : "Failed")} ({stopwatch.ElapsedMilliseconds} ms)";
    }

    private bool IsSorted<T>(T[] array) where T : IComparable<T>
    {
        if (array == null || array.Length <= 1) return true;

        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i].CompareTo(array[i + 1]) > 0)
            {
                return false;
            }
        }
        return true;
    }
}