using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            int count;

            while (true)
            {
                Console.WriteLine();
                Console.Write("How many numbers would you like? ");
                var countString = Console.ReadLine();
                
                if (int.TryParse(countString, out count)) break;

                Console.WriteLine();
                Console.WriteLine("Invalid number!");
            }

            Console.WriteLine();
            Console.WriteLine("Generating random number array...");

            var array = GetRandomNumberArray(count);

            Console.WriteLine();
            Console.WriteLine("Would you like to see the array?  (Y/n)");
            var response = Console.ReadKey();

            if (response.Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine(string.Join(' ', array.Select(a => a.ToString())));
            }

            Console.WriteLine();
            Console.Write("Sorting...");

            var sortResult = SortArrayWithTimeResult(array);

            Console.WriteLine();
            Console.WriteLine("Sorting Complete!");
            Console.WriteLine($"Sort Time in Milliseconds: {sortResult.TotalMilliseconds}");

            Console.WriteLine();
            Console.Write("Would you like to view the sorted array? (Y/n");
            var sortResponseQuery = Console.ReadKey();
            if (sortResponseQuery.Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine(string.Join(' ', sortResult.SortedArray.Select(a => a.ToString())));
            }

            Console.WriteLine();
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

        static IComparableItem<int>[] GetRandomNumberArray(int count)
        {
            var random = new Random();
            var array = Enumerable.Repeat(0, count).Select(n => new ComparableInteger(random.Next(0, count * 2))).ToArray();
            return array;
        }

        static SortResult<T> SortArrayWithTimeResult<T>(IComparableItem<T>[] array)
        {
            var quickSort = new QuickSort(true, 0);
            var startTime = DateTime.Now;
            var sortedArray = quickSort.Sort(array);
            var stopTime = DateTime.Now;

            var totalMilliseconds = (stopTime - startTime).TotalMilliseconds;

            return new SortResult<T>(totalMilliseconds, sortedArray);
        }
    }
}
