using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    class ComparableString : IComparableItem<string>
    {
        public string Item { get; private set; }

        public ComparableString(string item)
        {
            Item = item;
        }

        public int CompareTo(string other)
        {
            return Item.CompareTo(other);
        }

        public override string ToString()
        {
            return Item.ToString();
        }
    }
}
