using System;
using System.Diagnostics;
using UnityEngine;

namespace CustomMath
{
    public static class Algorithm
    {

        #region Bogo Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n)
         *  Promedio:   O(n * n!)
         *  Peor:       O(infinite)
         *  
         *  Complejidad espacial:   O(1) auxiliar
         */

        /*
         *  Bogo Sort mezcla aleatoriamente el array y revisa
         *  si esta ordenado. Repite esto hasta ordenar el array
         */

        public static void BogoSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            if (array.Length > 10)
            {
                throw new InvalidOperationException("Array is too big!");
            }

            System.Random rnd = new System.Random();

            while (!IsArraySorted(array))
            {
                Shuffle(array, rnd);
            }

            bool IsArraySorted(T[] arr)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i].CompareTo(arr[i + 1]) > 0)
                        return false;
                }
                return true;
            }

            void Shuffle(T[] arr, System.Random random)
            {
                int n = arr.Length;
                for (int i = 0; i < n; i++)
                {
                    int randomIndex = random.Next(0, n);
                    T temp = arr[i];
                    arr[i] = arr[randomIndex];
                    arr[randomIndex] = temp;
                }
            }
        }

        #endregion
        

        
        #region Bitonic

        /*
         *  Costo computacional
         *  Mejor:      O(n log^2 n) secuencial / O(log^2(n)) paralelo
         *  Promedio:   O(n log^2 n)
         *  Peor:       O(n log^2 n)
         *  
         *  Complejidad espacial:   O(n log^2 n)
         */

        /*
         *  Bitonic Sort es un algoritmo ideal para procesamiento paralelo. 
         *  Funciona formando secuencias "bitonicas" (una mitad asciende y la otra desciende).
         *  Recursivamente fusiona estas secuencias comparando e intercambiando 
         *  elementos a distancias específicas hasta que toda la colección queda ordenada
        */

        public static void BitonicSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            // Bitonic Sort tradicional requiere que la longitud sea potencia de 2
            bool isPowerOfTwo = (array.Length != 0) && ((array.Length & (array.Length - 1)) == 0);
            if (!isPowerOfTwo)
            {
                throw new ArgumentException("Bitonic Sort requires the array to have a length equal to a power of two");
            }

            // up = 1 -> ascendente, up = 0 -> descendente
            int up = 1;
            BitonicSortRecursive(array, 0, array.Length, up);

            /* ================= FUNCIONES LOCALES ================= */

            // Compara e intercambia elementos basándose en la dirección
            void CompAndSwap(T[] arr, int i, int j, int direction)
            {
                // arr[i].CompareTo(arr[j]) > 0 equivale a arr[i] > arr[j]
                if ((direction == 1 && arr[i].CompareTo(arr[j]) > 0) ||
                    (direction == 0 && arr[i].CompareTo(arr[j]) < 0))
                {
                    T temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // Fusiona recursivamente una secuencia bitónica para ordenarla
            void BitonicMerge(T[] arr, int low, int cnt, int direction)
            {
                if (cnt > 1)
                {
                    int k = cnt / 2;
                    for (int i = low; i < low + k; i++)
                    {
                        CompAndSwap(arr, i, i + k, direction);
                    }
                    BitonicMerge(arr, low, k, direction);
                    BitonicMerge(arr, low + k, k, direction);
                }
            }

            // Construye de forma recursiva secuencias bitónicas y las ordena
            void BitonicSortRecursive(T[] arr, int low, int cnt, int direction)
            {
                if (cnt > 1)
                {
                    int k = cnt / 2;

                    // Ordena la primera mitad de forma ascendente
                    BitonicSortRecursive(arr, low, k, 1);

                    // Ordena la segunda mitad de forma descendente
                    BitonicSortRecursive(arr, low + k, k, 0);

                    // Fusiona la secuencia entera en la dirección indicada
                    BitonicMerge(arr, low, cnt, direction);
                }
            }
        }

        #endregion
        

    }
}