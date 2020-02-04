using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    public class SortResult<T>
    {
        public readonly double TotalMilliseconds;
        public readonly IComparableItem<T>[] SortedArray;

        public SortResult(double totalMilliseconds, IComparableItem<T>[] sortedArray)
        {
            TotalMilliseconds = totalMilliseconds;
            SortedArray = sortedArray;
        }
    }
}
