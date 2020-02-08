using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.Write("Would you like to just let this run automatically? (Y/n) ");
            var response = Console.ReadKey();
            if (response.Key == ConsoleKey.Y)
            {
                AutoRun();
            } else
            {
                RunWithPrompts();
            }
        }

        private static void AutoRun()
        {
            const int arrayCount = 5;
            const int recordCount = 10000000;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Generating 'random number' arrays, each with {recordCount} records...");

            // Build random number arrays
            var randomNumberArrays = new List<IComparableItem<int>[]>(arrayCount);
            for (int i=0; i<10; i++)
            {
                randomNumberArrays.Add(GetRandomNumberArray(100000000));
            }
            
            var resultList = new List<SortResult<int>>();

            Console.WriteLine();
            Console.WriteLine($"Running plain old QuickSort on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("Plain QuickSort", array, false, 0));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with InsertionSort cutoff of 10 on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("InsertionSort Cutoff = 10", array, false, 10));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with InsertionSort cutoff of 15 on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("InsertionSort Cutoff = 15", array, false, 15));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with InsertionSort cutoff of 20 on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("InsertionSort Cutoff = 20", array, false, 20));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with InsertionSort cutoff of 50 on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("InsertionSort Cutoff = 50", array, false, 50));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with Median of Three on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("Median of Three", array, true, 0));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with Median of Three and InsertionSort cutoff of 10 on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("Median of Three with InsertionSort Cutoff = 10", array, true, 10));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with Median of Three and InsertionSort cutoff of 15 on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("Median of Three with InsertionSort Cutoff = 15", array, true, 15));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with Median of Three and InsertionSort cutoff of 20 on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("Median of Three with InsertionSort Cutoff = 20", array, true, 20));
            }

            Console.WriteLine();
            Console.WriteLine($"Running QuickSort with Median of Three and InsertionSort cutoff of 50 on {arrayCount} sets of {recordCount} records...");

            foreach (var array in randomNumberArrays)
            {
                resultList.Add(SortArrayWithTimeResult("Median of Three with InsertionSort Cutoff = 50", array, true, 50));
            }

            SaveResults(resultList);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Algorithm Tests Complete! Press any key to exit.");
            Console.ReadKey();
        }

        private static void SaveResults(List<SortResult<int>> results)
        {
            var resultsAsTextList = new List<string>();
            results.ForEach(r => resultsAsTextList.Add(r.ToString()));
            File.WriteAllText("D:\\CS4050\\QuickSortResults.csv", String.Join("\r\n", resultsAsTextList));
        }

        private static void RunWithPrompts()
        {
            int count;
            int insertionSortSize = 0;
            bool useMedianOfThree = false;

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
            Console.Write("Would you like to use Median of Three? (Y/n) ");
            var medianOfThree = Console.ReadKey();
            if (medianOfThree.Key == ConsoleKey.Y) useMedianOfThree = true;

            while (true)
            {
                Console.WriteLine();
                Console.Write("What size would you like to switch to InsertionSort? (0 = Do Not Use) ");
                var numberString = Console.ReadLine();

                if (int.TryParse(numberString, out insertionSortSize)) break;

                Console.WriteLine();
                Console.WriteLine("Invalid number!");
            }

            Console.WriteLine();
            Console.WriteLine("Generating random number array...");

            var array = GetRandomNumberArray(count);

            Console.WriteLine();
            Console.WriteLine("Would you like to see the array?  (Y/n) ");
            var response = Console.ReadKey();

            if (response.Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine(string.Join(' ', array.Select(a => a.ToString())));
            }

            Console.WriteLine();
            Console.Write("Sorting...");

            var sortResult = SortArrayWithTimeResult("Manual Execution", array, useMedianOfThree, insertionSortSize);

            Console.WriteLine();
            Console.WriteLine("Sorting Complete!");
            Console.WriteLine($"Sort Time in Milliseconds: {sortResult.TotalMilliseconds}");

            Console.WriteLine();
            Console.Write("Would you like to view the sorted array? (Y/n) ");
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

        private static IComparableItem<int>[] GetRandomNumberArray(int count)
        {
            var random = new Random();
            var array = Enumerable.Repeat(0, count).Select(n => new ComparableInteger(random.Next(0, count * 2))).ToArray();
            return array;
        }

        private static SortResult<T> SortArrayWithTimeResult<T>(string identifier, IComparableItem<T>[] array, bool useMedianOfThree, int insertionSortSize)
        {
            var quickSort = new QuickSort(useMedianOfThree, insertionSortSize);
            var startTime = DateTime.Now;
            var sortedArray = quickSort.Sort(array);
            var stopTime = DateTime.Now;

            var totalMilliseconds = (stopTime - startTime).TotalMilliseconds;

            return new SortResult<T>(identifier, totalMilliseconds, sortedArray);
        }
    }
}
