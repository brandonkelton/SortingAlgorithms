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
        public static IComparableItem<T>[] Sort<T>(IComparableItem<T>[] a)
        {
            Sort(a, 0, a.Length - 1);
            return a;
        }

        /// Internal quicksort method that makes recursive calls.
        /// Uses median-of-three partitioning and a cutoff of 10.
        /// <param name="a">An array of Comparable items</param>
        /// <param name="low">The left-most index of the subarray</param>
        /// <param name="high">The right-most index of the subarray</param>
        private static void Sort<T>(IComparableItem<T>[] a, int low, int high)
        {
            if (low + CUTOFF > high)
                InsertionSort(a, low, high);
            else
            {
                // Sort low, middle, high
                int middle = (low + high) / 2;
                if (a[middle].CompareTo(a[low].Item) < 0)
                    SwapReferences(a, low, middle);
                if (a[high].CompareTo(a[low].Item) < 0)
                    SwapReferences(a, low, high);
                if (a[high].CompareTo(a[middle].Item) < 0)
                    SwapReferences(a, middle, high);

                // Place pivot at position high - 1
                SwapReferences(a, middle, high - 1);
                IComparableItem<T> pivot = a[high - 1];

                // Begin partitioning
                int i, j;
                for (i = low, j = high - 1; ;)
                {
                    while (a[++i].CompareTo(pivot.Item) < 0)
                        ;
                    while (pivot.CompareTo(a[--j].Item) < 0)
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
        /// <param name="a">An array of comparable items</param>
        /// <param name="index1">The index of the first object</param>
        /// <param name="index2">The index of the second object</param>
        private static void SwapReferences<T>(IComparableItem<T>[] a, int index1, int index2)
        {
            IComparableItem<T> tmp = a[index1];
            a[index1] = a[index2];
            a[index2] = tmp;
        }


        /// Internal insertion sort routine for subarrays that is used by quicksort.
        /// <param name="a">An array of Comparable items</param>
        /// <param name="low">The left-most index of the subarray</param>
        /// <param name="high">The number of items to sort</param>
        private static void InsertionSort<T>(IComparableItem<T>[] a, int low, int high)
        {
            for (int p = low + 1; p <= high; p++)
            {
                IComparableItem<T> tmp = a[p];
                int j;

                for (j = p; j > low && tmp.CompareTo(a[j - 1].Item) < 0; j--)
                    a[j] = a[j - 1];

                a[j] = tmp;
            }
        }
    }
}
