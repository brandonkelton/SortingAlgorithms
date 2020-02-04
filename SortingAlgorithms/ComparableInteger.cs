using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SortingAlgorithms
{
    public class ComparableInteger : IComparableItem<int>
    {
        public int Item { get; private set; }

        public ComparableInteger(int item)
        {
            Item = item;
        }

        public int CompareTo(int other)
        {
            return Item.CompareTo(other);
        }

        public override string ToString()
        {
            return Item.ToString();
        }
    }
}
