using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    /// <summary>
    /// Original Source Code: https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-9.php
    /// </summary>
    public class QuickSort
    {
        public static IComparableItem<T>[] Sort<T>(IComparableItem<T>[] a, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(a, left, right);

                if (pivot > 1)
                {
                    Sort(a, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Sort(a, pivot + 1, right);
                }
            }

            return a;
        }

        private static int Partition<T>(IComparableItem<T>[] a, int left, int right)
        {
            IComparableItem<T> pivot = a[left];

            while (true)
            {
                while (a[left].CompareTo(pivot.Item) < 0)
                {
                    left++;
                }

                while (a[right].CompareTo(pivot.Item) > 0)
                {
                    right--;
                }

                if (left < right)
                {
                    if (a[left].CompareTo(a[right].Item) == 0) return right;

                    IComparableItem<T> temp = a[left];
                    a[left] = a[right];
                    a[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
