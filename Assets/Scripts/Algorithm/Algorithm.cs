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

        #region Quick Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n log n)
         *  Promedio:   O(n log n)
         *  Peor:       O(n^2)
         *  
         *  Complejidad espacial:   O(log n) a O(n)
         */

        public static void QuickSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            QuickSortRecursive(array, 0, array.Length - 1);

            void QuickSortRecursive(T[] arr, int low, int high)
            {
                if (low < high)
                {
                    int pi = Partition(arr, low, high);

                    QuickSortRecursive(arr, low, pi - 1);
                    QuickSortRecursive(arr, pi + 1, high);
                }
            }

            int Partition(T[] arr, int low, int high)
            {
                T pivot = arr[high];

                int i = low - 1;

                for (int j = low; j <= high - 1; j++)
                {
                    if (arr[j].CompareTo(pivot) < 0)
                    {
                        i++;
                        Swap(arr, i, j);
                    }
                }

                Swap(arr, i + 1, high);
                return i + 1;
            }

            void Swap(T[] arr, int i, int j)
            {
                T temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        #endregion

        #region Merge Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n log n)
         *  Promedio:   O(n log n)
         *  Peor:       O(n log n)
         *  
         *  Complejidad espacial:   O(n) auxiliar
         */

        public static void MergeSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            MergeSortRecursive(array, 0, array.Length - 1);


            void MergeSortRecursive(T[] arr, int l, int r)
            {
                if (l < r)
                {
                    int m = l + (r - l) / 2;

                    MergeSortRecursive(arr, l, m);
                    MergeSortRecursive(arr, m + 1, r);

                    Merge(arr, l, m, r);
                }
            }

            void Merge(T[] arr, int l, int m, int r)
            {
                int n1 = m - l + 1;
                int n2 = r - m;

                T[] L = new T[n1];
                T[] R = new T[n2];

                for (int idx = 0; idx < n1; ++idx)
                    L[idx] = arr[l + idx];
                for (int jdx = 0; jdx < n2; ++jdx)
                    R[jdx] = arr[m + 1 + jdx];

                int i = 0, j = 0;

                int k = l;
                while (i < n1 && j < n2)
                {
                    if (L[i].CompareTo(R[j]) <= 0)
                    {
                        arr[k] = L[i];
                        i++;
                    }
                    else
                    {
                        arr[k] = R[j];
                        j++;
                    }
                    k++;
                }

                while (i < n1)
                {
                    arr[k] = L[i];
                    i++;
                    k++;
                }

                while (j < n2)
                {
                    arr[k] = R[j];
                    j++;
                    k++;
                }
            }
        }

        #endregion

        #region Heap Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n log n)
         *  Promedio:   O(n log n)
         *  Peor:       O(n log n)
         *  
         *  Complejidad espacial:   O(1) auxiliar
         */

        public static void HeapSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i);
            }

            for (int i = n - 1; i > 0; i--)
            {
                T temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                Heapify(array, i, 0);
            }

            void Heapify(T[] arr, int size, int rootIndex)
            {
                int largest = rootIndex;
                int left = 2 * rootIndex + 1;
                int right = 2 * rootIndex + 2;

                if (left < size && arr[left].CompareTo(arr[largest]) > 0)
                {
                    largest = left;
                }

                if (right < size && arr[right].CompareTo(arr[largest]) > 0)
                {
                    largest = right;
                }

                if (largest != rootIndex)
                {
                    T swapTemp = arr[rootIndex];
                    arr[rootIndex] = arr[largest];
                    arr[largest] = swapTemp;

                    Heapify(arr, size, largest);
                }
            }
        }

        #endregion

        #region Intro Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n log n)
         *  Promedio:   O(n log n)
         *  Peor:       O(n log n)
         *  
         *  Complejidad espacial:   O(log n)
         */
        public static void IntroSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;
            int depthLimit = (int)(2 * Math.Floor(Math.Log(n) / Math.Log(2)));

            SortDataUtil(0, n - 1, depthLimit);


            void Swap(int i, int j)
            {
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            void MaxHeap(int i, int heapN, int begin)
            {
                T temp = array[begin + i - 1];
                int child;

                while (i <= heapN / 2)
                {
                    child = 2 * i;

                    if (child < heapN && array[begin + child - 1].CompareTo(array[begin + child]) < 0)
                        child++;

                    if (temp.CompareTo(array[begin + child - 1]) >= 0)
                        break;

                    array[begin + i - 1] = array[begin + child - 1];
                    i = child;
                }
                array[begin + i - 1] = temp;
            }

            void Heapify(int begin, int end, int heapN)
            {
                for (int i = heapN / 2; i >= 1; i--)
                    MaxHeap(i, heapN, begin);
            }

            void HeapSort(int begin, int end)
            {
                int heapN = end - begin;

                Heapify(begin, end, heapN);

                for (int i = heapN; i >= 1; i--)
                {
                    Swap(begin, begin + i);
                    MaxHeap(1, i, begin);
                }
            }

            void InsertionSort(int left, int right)
            {
                for (int i = left; i <= right; i++)
                {
                    T key = array[i];
                    int j = i;

                    while (j > left && array[j - 1].CompareTo(key) > 0)
                    {
                        array[j] = array[j - 1];
                        j--;
                    }
                    array[j] = key;
                }
            }

            int FindPivot(int a, int b, int c)
            {
                if (array[a].CompareTo(array[b]) < 0)
                {
                    if (array[b].CompareTo(array[c]) < 0) return b;
                    if (array[a].CompareTo(array[c]) < 0) return c;
                    return a;
                }
                else
                {
                    if (array[a].CompareTo(array[c]) < 0) return a;
                    if (array[b].CompareTo(array[c]) < 0) return c;
                    return b;
                }
            }

            int Partition(int low, int high)
            {
                T pivot = array[high];
                int i = low - 1;

                for (int j = low; j <= high - 1; j++)
                {
                    if (array[j].CompareTo(pivot) <= 0)
                    {
                        i++;
                        Swap(i, j);
                    }
                }
                Swap(i + 1, high);
                return i + 1;
            }

            void SortDataUtil(int begin, int end, int currentDepthLimit)
            {
                if (end - begin > 16)
                {
                    if (currentDepthLimit == 0)
                    {
                        HeapSort(begin, end);
                        return;
                    }

                    currentDepthLimit--;

                    int mid = begin + ((end - begin) / 2) + 1;
                    int pivotIdx = FindPivot(begin, mid, end);
                    Swap(pivotIdx, end);

                    int p = Partition(begin, end);

                    SortDataUtil(begin, p - 1, currentDepthLimit);
                    SortDataUtil(p + 1, end, currentDepthLimit);
                }
                else
                {
                    InsertionSort(begin, end);
                }
            }
        }

        #endregion

        #region Adaptive Merge Sort

        /*
         *  Costo computacional
         *  Mejor:      O(n)
         *  Promedio:   O(n log n)
         *  Peor:       O(n log n)
         *  
         *  Complejidad espacial:   O(n) auxiliar
         */

        public static void AdaptiveMergeSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;
            T[] temp = new T[n];
            bool sorted = false;

            while (!sorted)
            {
                sorted = true;
                int left = 0;

                while (left < n)
                {
                    int mid = left;

                    while (mid < n - 1 && array[mid].CompareTo(array[mid + 1]) <= 0)
                    {
                        mid++;
                    }

                    if (mid == n - 1)
                    {
                        break;
                    }

                    int right = mid + 1;

                    while (right < n - 1 && array[right].CompareTo(array[right + 1]) <= 0)
                    {
                        right++;
                    }

                    MergeRuns(array, temp, left, mid, right);

                    sorted = false;

                    left = right + 1;
                }
            }


            void MergeRuns(T[] arr, T[] tmp, int l, int m, int r)
            {
                int i = l;
                int j = m + 1;
                int k = l;

                while (i <= m && j <= r)
                {
                    if (arr[i].CompareTo(arr[j]) <= 0)
                    {
                        tmp[k++] = arr[i++];
                    }
                    else
                    {
                        tmp[k++] = arr[j++];
                    }
                }

                while (i <= m)
                {
                    tmp[k++] = arr[i++];
                }

                while (j <= r)
                {
                    tmp[k++] = arr[j++];
                }

                for (int p = l; p <= r; p++)
                {
                    arr[p] = tmp[p];
                }
            }
        }

        #endregion

        #region Radix Sort (LSD)

        /*
         *  Costo computacional
         *  donde d es el num de dígitos y k es la base (10)
         *  Mejor:      O(d * (n + k))
         *  Promedio:   O(d * (n + k))
         *  Peor:       O(d * (n + k))
         *  
         *  Complejidad espacial:   O(n + k)
         */

        public static void RadixSortLSD<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;

            int n = array.Length;

            int min = Convert.ToInt32(array[0]);
            int max = Convert.ToInt32(array[0]);
            for (int i = 1; i < n; i++)
            {
                int val = Convert.ToInt32(array[i]);
                if (val > max) max = val;
                if (val < min) min = val;
            }

            int offset = min < 0 ? -min : 0;
            max += offset;

            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountSortLSD(array, n, exp, offset);
            }


            void CountSortLSD(T[] arr, int length, int exp, int off)
            {
                T[] output = new T[length];
                int[] count = new int[10];

                for (int i = 0; i < length; i++)
                {
                    int val = Convert.ToInt32(arr[i]) + off;
                    count[(val / exp) % 10]++;
                }

                for (int i = 1; i < 10; i++)
                {
                    count[i] += count[i - 1];
                }

                for (int i = length - 1; i >= 0; i--)
                {
                    int val = Convert.ToInt32(arr[i]) + off;
                    int digit = (val / exp) % 10;
                    output[count[digit] - 1] = arr[i];
                    count[digit]--;
                }

                for (int i = 0; i < length; i++)
                {
                    arr[i] = output[i];
                }
            }
        }

        #endregion

    }
}