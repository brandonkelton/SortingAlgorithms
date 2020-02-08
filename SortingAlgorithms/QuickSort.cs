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
        public IComparableItem<T>[] Sort<T>(IComparableItem<T>[] a)
        {
            Sort(a, 0, a.Length - 1);
            return a;
        }
        
        private void Sort<T>(IComparableItem<T>[] array, int low, int high)
        {
            if (InsertionSortSize > 0 && low + InsertionSortSize > high)
                InsertionSort(array, low, high);
            else if (low < high)
            {
                int partition;

                if (UseMedianOfThree && low + 3 < high)
                {
                    int middle = (low + high) / 2;
                    if (array[middle].CompareTo(array[low].Item) < 0)
                        Swap(array, low, middle);
                    if (array[high].CompareTo(array[low].Item) < 0)
                        Swap(array, low, high);
                    if (array[high].CompareTo(array[middle].Item) < 0)
                        Swap(array, middle, high);
                }

                partition = Partition(array, low, high);

                Sort(array, low, partition - 1);
                Sort(array, partition + 1, high);
            }
        }

        private int Partition<T>(IComparableItem<T>[] array, int low, int high)
        {
            IComparableItem<T> pivot = array[high];
            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                if (array[j].CompareTo(pivot.Item) < 0)
                {
                    i++;
                    Swap(array, i, j);
                }
            }

            i++;
            Swap(array, i, high);
            return i;
        } 

        /// Method to swap to elements in an array.
        /// <param name="a">An array of comparable items</param>
        /// <param name="index1">The index of the first object</param>
        /// <param name="index2">The index of the second object</param>
        private void Swap<T>(IComparableItem<T>[] a, int index1, int index2)
        {
            IComparableItem<T> tmp = a[index1];
            a[index1] = a[index2];
            a[index2] = tmp;
        }


        /// Internal insertion sort routine
        /// <param name="a">An array of Comparable items</param>
        /// <param name="low">The left-most index of the subarray</param>
        /// <param name="high">The number of items to sort</param>
        private void InsertionSort<T>(IComparableItem<T>[] a, int low, int high)
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
