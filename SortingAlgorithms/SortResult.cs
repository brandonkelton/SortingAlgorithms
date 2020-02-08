﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    public class SortResult<T>
    {
        public readonly string Identifier;
        public readonly double TotalMilliseconds;
        public readonly IComparableItem<T>[] SortedArray;

        public SortResult(string identifier, double totalMilliseconds, IComparableItem<T>[] sortedArray)
        {
            Identifier = identifier;
            TotalMilliseconds = totalMilliseconds;
            SortedArray = sortedArray;
        }

        public override string ToString()
        {
            return $"{Identifier},{TotalMilliseconds},{String.Join("  ", SortedArray.Take(10).Select(a => a.Item.ToString()).ToArray())},{String.Join("  ", SortedArray.Skip(SortedArray.Length - 10).Select(a => a.Item.ToString()))}";
        }
    }
}
