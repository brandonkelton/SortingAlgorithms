using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    /// <summary>
    /// Original Source Code: https://www.java-tips.org/java-se-tips-100019/24-java-lang/1896-quick-sort-implementation-with-median-of-three-partitioning-and-cutoff-for-small-arrays.html
    /// </summary>
    public class QuickSortMedianOfThree
    {
        private static readonly int CUTOFF = 10;

        /// Quicksort algorithm
        /// <param name="a">An array of Comparable items</param>
        public static void Sort(IComparable[] a)
        {
            Sort(a, 0, a.Length - 1);
        }

        /// Internal quicksort method that makes recursive calls.
        /// Uses median-of-three partitioning and a cutoff of 10.
        /// <param name="a">An array of Comparable items</param>
        /// <param name="low">The left-most index of the subarray</param>
        /// <param name="high">The right-most index of the subarray</param>
        private static void Sort(IComparable[] a, int low, int high)
        {
            if (low + CUTOFF > high)
                InsertionSort(a, low, high);
            else
            {
                // Sort low, middle, high
                int middle = (low + high) / 2;
                if (a[middle].CompareTo(a[low]) < 0)
                    SwapReferences(a, low, middle);
                if (a[high].CompareTo(a[low]) < 0)
                    SwapReferences(a, low, high);
                if (a[high].CompareTo(a[middle]) < 0)
                    SwapReferences(a, middle, high);

                // Place pivot at position high - 1
                SwapReferences(a, middle, high - 1);
                IComparable pivot = a[high - 1];

                // Begin partitioning
                int i, j;
                for (i = low, j = high - 1; ;)
                {
                    while (a[++i].CompareTo(pivot) < 0)
                        ;
                    while (pivot.CompareTo(a[--j]) < 0)
                        ;
                    if (i >= j)
                        break;
                    SwapReferences(a, i, j);
                }

                // Restore pivot
                SwapReferences(a, i, high - 1);

                Sort(a, low, i - 1);    // Sort small elements
                Sort(a, i + 1, high);   // Sort large elements
            }
        }

        /// Method to swap to elements in an array.
        /// <param name="a">An array of objects</param>
        /// <param name="index1">The index of the first object</param>
        /// <param name="index2">The index of the second object</param>
        private static void SwapReferences(object[] a, int index1, int index2)
        {
            object tmp = a[index1];
            a[index1] = a[index2];
            a[index2] = tmp;
        }


        /// Internal insertion sort routine for subarrays that is used by quicksort.
        /// <param name="a">An array of Comparable items</param>
        /// <param name="low">The left-most index of the subarray</param>
        /// <param name="high">The number of items to sort</param>
        private static void InsertionSort(IComparable[] a, int low, int high)
        {
            for (int p = low + 1; p <= high; p++)
            {
                IComparable tmp = a[p];
                int j;

                for (j = p; j > low && tmp.CompareTo(a[j - 1]) < 0; j--)
                    a[j] = a[j - 1];

                a[j] = tmp;
            }
        }
    }
}
