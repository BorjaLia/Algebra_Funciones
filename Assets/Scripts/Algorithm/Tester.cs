using CustomMath;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlgorithmTester : MonoBehaviour
{
    [Header("Configuración del Array")]
    [Tooltip("Cantidad de elementos que tendrá el array a ordenar.")]
    public int arraySize = 100;

    [Tooltip("Valor mínimo (inclusivo) para los números aleatorios.")]
    public int minValue = 0;

    [Tooltip("Valor máximo (exclusivo) para los números aleatorios.")]
    public int maxValue = 1000;

    private void Start()
    {
        RunAllTests();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RunAllTests();
        }
    }

    private void RunAllTests()
    {
        // 1. Generar el array aleatorio base para esta ronda de pruebas
        int[] originalArray = GenerateRandomArray();

        // Usamos StringBuilder para construir un solo gran string y hacer un solo Debug.Log al final
        StringBuilder report = new StringBuilder();
        report.AppendLine("Tests de algoritmos:\n");

        report.AppendLine(originalArray.ToString());

        // 2. Ejecutar cada algoritmo.
        // Le pasamos el nombre, la referencia al método estático y el array original.
        TestAlgorithm("Bitonic", Algorithm.BitonicSort, originalArray, report);
        TestAlgorithm("Selection Sort", Algorithm.SelectionSort, originalArray, report);
        TestAlgorithm("Cocktail Shaker Sort", Algorithm.CocktailShakerSort, originalArray, report);
        TestAlgorithm("Quick Sort", Algorithm.QuickSort, originalArray, report);
        TestAlgorithm("Radix Sort (LSD)", Algorithm.RadixSortLSD, originalArray, report);
        TestAlgorithm("Shell Sort", Algorithm.ShellSort, originalArray, report);
        TestAlgorithm("Bogo Sort", Algorithm.BogoSort, originalArray, report);
        TestAlgorithm("Radix Sort (MSD)", Algorithm.RadixSortMSD, originalArray, report);
        TestAlgorithm("Intro Sort", Algorithm.IntroSort, originalArray, report);
        TestAlgorithm("Adaptive Merge Sort", Algorithm.AdaptiveMergeSort, originalArray, report);
        TestAlgorithm("Bubble Sort", Algorithm.BubbleSort, originalArray, report);
        TestAlgorithm("Gnome Sort", Algorithm.GnomeSort, originalArray, report);
        TestAlgorithm("Merge Sort", Algorithm.MergeSort, originalArray, report);
        TestAlgorithm("Heap Sort", Algorithm.HeapSort, originalArray, report);
        TestAlgorithm("Insertion Sort", Algorithm.InsertionSort, originalArray, report);

        // 3. Imprimir el resultado en la consola de Unity
        Debug.Log(report.ToString());
    }

    /// <summary>
    /// Genera un array de enteros aleatorios basado en las propiedades del inspector.
    /// </summary>
    private int[] GenerateRandomArray()
    {
        int[] arr = new int[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            // Unity Random.Range para enteros es inclusivo en el mínimo y exclusivo en el máximo
            arr[i] = UnityEngine.Random.Range(minValue, maxValue);
        }
        return arr;
    }

    /// <summary>
    /// Toma el método de ordenamiento, lo ejecuta de manera segura en una copia del array
    /// y verifica si logró ordenarlo. Agrega el resultado al reporte.
    /// </summary>
    private void TestAlgorithm(string name, Action<int[]> sortMethod, int[] originalArray, StringBuilder report)
    {
        // Hacemos una copia profunda del array para que cada algoritmo se enfrente
        // al mismo desafío desordenado, sin beneficiarse del ordenamiento de algoritmos previos.
        int[] arrayCopy = new int[originalArray.Length];
        Array.Copy(originalArray, arrayCopy, originalArray.Length);

        bool success = false;

        try
        {
            // Ejecutamos el algoritmo
            sortMethod(arrayCopy);

            // Si el método no tiró excepción, verificamos si realmente está ordenado
            success = IsSorted(arrayCopy);
        }
        catch (NotImplementedException)
        {
            // Atrapamos la excepción del esqueleto para que el test no se rompa
            success = false;
        }
        catch (Exception ex)
        {
            // Atrapamos cualquier otro error (índices fuera de rango, stack overflow, etc.)
            Debug.LogWarning($"El algoritmo {name} falló con una excepción: {ex.Message}");
            success = false;
        }

        // Formateamos la salida esperada
        report.AppendLine($"{name}: {(success ? "Logrado" : "No logrado")}");
    }

    /// <summary>
    /// Función auxiliar genérica que verifica si un array está ordenado de menor a mayor.
    /// </summary>
    private bool IsSorted<T>(T[] array) where T : IComparable<T>
    {
        if (array == null || array.Length <= 1) return true;

        for (int i = 0; i < array.Length - 1; i++)
        {
            // Si el elemento actual es mayor que el siguiente, la lista no está ordenada
            if (array[i].CompareTo(array[i + 1]) > 0)
            {
                return false;
            }
        }

        return true;
    }
}