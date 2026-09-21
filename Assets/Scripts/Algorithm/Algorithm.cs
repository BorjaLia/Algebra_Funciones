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
        
        #region Selection Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n^2)
         *  Promedio:   O(n^2)
         *  Peor:       O(n^2)
         *  
         *  Complejidad espacial:   O(1) auxiliar
         */

        /*
         *  Divide el arreglo en dos partes una sublista ordenada (al principio) 
         *  y otra desordenada. (el primer for loop)
         *  En cada iteracion busca el elemento  más pequeño en la 
         *  parte desordenada y lo intercambia con el primer 
         *  elemento de esa parte desordenada.
         */

        public static void SelectionSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;

            //todo el array
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                //parte desordenada
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j].CompareTo(array[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    T temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
        }

        #endregion
        
        #region Bubble Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n)
         *  Promedio:   O(n^2)
         *  Peor:       O(n^2)
         *  
         *  Complejidad espacial:   O(1) auxiliar
         */

        /*
         *  Bubble Sort recorre repetidamente el array, 
         *  compara elementos adyacentes y los intercambia 
         *  si están en el orden equivocado. 
         *  El proceso se repite hasta que no se necesitan más intercambios.
         *  
         */

        public static void BubbleSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }
        }

        #endregion

        #region Insertion Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n)
         *  Promedio:   O(n^2)
         *  Peor:       O(n^2)
         *  
         *  Complejidad espacial:   O(1) auxiliar
         */

        /*
         *  Insertion Sort construye el array final ordenado
         *  elemento por elemento. Itera el arreglo y toma 
         *  un elemento y lo inserta en su posición correcta 
         *  dentro de la parte del arreglo que ya ha recorrido y ordenado.
         */

        public static void InsertionSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;
            for (int i = 1; i < n; ++i)
            {
                T key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j].CompareTo(key) > 0)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
        }

        #endregion

        #region Cocktail Shaker Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n)
         *  Promedio:   O(n^2)
         *  Peor:       O(n^2)
         *  
         *  Complejidad espacial:   O(1) auxiliar
         */

        /*
         *  Igual que un bubble sort pero ordena hacia ambos lados
         *  al mismo tiempo
         */

        public static void CocktailShakerSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            bool swapped = true;
            int start = 0;
            int end = array.Length;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end - 1; ++i)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;

                end = end - 1;

                for (int i = end - 1; i >= start; i--)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                start = start + 1;
            }
        }

        #endregion

        #region Gnome Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n)
         *  Promedio:   O(n^2)
         *  Peor:       O(n^2)
         *  
         *  Complejidad espacial:   O(1) auxiliar
         */

        /*
         *  Gnome Sort recorre todo el array revisando pares de numeros
         *  si estan ordenados, pasa al siguiente par.
         *  si no, los cambia.
         */

        public static void GnomeSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;
            int index = 0;

            while (index < n)
            {
                if (index == 0)
                {
                    index++;
                }

                if (array[index].CompareTo(array[index - 1]) >= 0)
                {
                    index++;
                }
                else
                {
                    T temp = array[index];
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                    index--;
                }
            }
        }

        #endregion

        #region Shell Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n log n)
         *  Promedio:   O(n^(1.25)) a O(n^(1.5))
         *  Peor:       O(n^2)
         *  
         *  Complejidad espacial:   O(1) auxiliar
         */

        public static void ShellSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = array[i];
                    int j = i;

                    while (j >= gap && array[j - gap].CompareTo(temp) > 0)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }

                    array[j] = temp;
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

            bool isPowerOfTwo = (array.Length != 0) && ((array.Length & (array.Length - 1)) == 0);
            if (!isPowerOfTwo)
            {
                throw new ArgumentException("Bitonic Sort requires the array to have a length equal to a power of two");
            }

            int up = 1;
            BitonicSortRecursive(array, 0, array.Length, up);


            void CompAndSwap(T[] arr, int i, int j, int direction)
            {
                if ((direction == 1 && arr[i].CompareTo(arr[j]) > 0) ||
                    (direction == 0 && arr[i].CompareTo(arr[j]) < 0))
                {
                    T temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

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

            void BitonicSortRecursive(T[] arr, int low, int cnt, int direction)
            {
                if (cnt > 1)
                {
                    int k = cnt / 2;

                    BitonicSortRecursive(arr, low, k, 1);

                    BitonicSortRecursive(arr, low + k, k, 0);

                    BitonicMerge(arr, low, cnt, direction);
                }
            }
        }

        #endregion
        
    }
}