using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    /// <summary>
    /// Code Derived From:
    /// https://www.java-tips.org/java-se-tips-100019/24-java-lang/1896-quick-sort-implementation-with-median-of-three-partitioning-and-cutoff-for-small-arrays.html
    /// https://www.geeksforgeeks.org/quick-sort/
    /// </summary>
    public class QuickSort
    {
        private bool UseMedianOfThree;
        private int InsertionSortSize;

        public QuickSort(bool useMedianOfThree, int insertionSortSize)
        {
            UseMedianOfThree = useMedianOfThree;
            InsertionSortSize = insertionSortSize;
        }

        /// Quicksort algorithm
        /// <param name="a">An array of Comparable items</param>
        public int[] Sort(int[] a)
        {
            Sort(a, 0, a.Length - 1);
            return a;
        }
        
        private void Sort(int[] array, int low, int high)
        {
            if (InsertionSortSize > 0 && low + InsertionSortSize > high)
                return;
            else if (low < high)
            {
                if (UseMedianOfThree && low + 3 < high)
                {
                    int middle = (low + high) / 2;
                    if (array[middle].CompareTo(array[low]) < 0)
                        Swap(array, low, middle);
                    if (array[high].CompareTo(array[low]) < 0)
                        Swap(array, low, high);
                    if (array[high].CompareTo(array[middle]) < 0)
                        Swap(array, middle, high);
                }

                int partition = Partition(array, low, high);

                Sort(array, low, partition);
                Sort(array, partition + 1, high);
            }
        }

        private int Partition(int[] array, int low, int high)
        {
            int middle = (low + high) / 2;
            int pivot = array[middle];
            Swap(array, middle, high);

            int i = low;
            int j = high - 1;

            while (true)
            {
                while (i < j && array[i].CompareTo(pivot) <= 0) i++;
                while (j > i && array[j].CompareTo(pivot) >= 0) j--;

                if (i != j)
                {
                    Swap(array, i, j);
                }
                
                if (i >= j)
                {
                   break;
                }
            }

            if (array[i].CompareTo(array[high]) > 0)
            {
                Swap(array, i, high);
            }

            return i;
        } 

        /// Method to swap to elements in an array.
        /// <param name="a">An array of comparable items</param>
        /// <param name="index1">The index of the first object</param>
        /// <param name="index2">The index of the second object</param>
        private void Swap(int[] a, int index1, int index2)
        {
            int tmp = a[index1];
            a[index1] = a[index2];
            a[index2] = tmp;
        }


        /// Internal insertion sort routine
        /// <param name="a">An array of Comparable items</param>
        /// <param name="low">The left-most index of the subarray</param>
        /// <param name="high">The number of items to sort</param>
        public void InsertionSort(int[] a, int low, int high)
        {
            for (int p = low + 1; p <= high; p++)
            {
                int tmp = a[p];
                int j;

                for (j = p; j > low && tmp.CompareTo(a[j - 1]) < 0; j--)
                    a[j] = a[j - 1];

                a[j] = tmp;
            }
        }
    }
}
