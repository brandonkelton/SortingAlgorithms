using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SortingAlgorithms
{
    public interface IComparableItem<T> : IComparable<T>
    {
        public T Item { get; }
    }
}
