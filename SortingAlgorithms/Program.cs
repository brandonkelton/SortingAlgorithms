﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SortingAlgorithms
{
    class Program
    {
        private const int ArrayCount = 5;
        private const int RecordCount = 10000000;

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.Write("Would you like to automatically run all tests? (Alternative is prompt-driven) (Y/n) ");
            var response = Console.ReadKey();
            if (response.Key == ConsoleKey.Y)
            {
                AutoRun();
            } else
            {
                Console.WriteLine();
                Console.WriteLine("Run Plain QuickSort with 10 million Random Integers 10 Times? (Y/n) ");
                var qsResponse = Console.ReadKey();
                if (qsResponse.Key == ConsoleKey.Y)
                {
                    RunPlainQuickSort();
                }
                else
                {
                    RunWithPrompts();
                }
            }
        }

        private static void AutoRun()
        {
            CreateFile();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Generating {ArrayCount} Sets of {RecordCount} Random Numbers...");

            // Build random number arrays
            var randomNumberArrays = new List<int[]>(ArrayCount);
            for (int i=0; i<ArrayCount; i++)
            {
                randomNumberArrays.Add(GetRandomNumberArray(RecordCount));
            }

            RunTestSort(randomNumberArrays, "Plain QuickSort", false, 0);
            RunTestSort(randomNumberArrays, "QuickSort with InsertionSort CutOff of 10", false, 10);
            RunTestSort(randomNumberArrays, "QuickSort with InsertionSort CutOff of 15", false, 15);
            RunTestSort(randomNumberArrays, "QuickSort with InsertionSort CutOff of 20", false, 20);
            RunTestSort(randomNumberArrays, "QuickSort with InsertionSort CutOff of 50", false, 50);
            RunTestSort(randomNumberArrays, "QuickSort with Median of Three", true, 0);
            RunTestSort(randomNumberArrays, "QuickSort with Median of Three and InsertionSort CutOff of 10", true, 10);
            RunTestSort(randomNumberArrays, "QuickSort with Median of Three and InsertionSort CutOff of 15", true, 15);
            RunTestSort(randomNumberArrays, "QuickSort with Median of Three and InsertionSort CutOff of 20", true, 20);
            RunTestSort(randomNumberArrays, "QuickSort with Median of Three and InsertionSort CutOff of 50", true, 50);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Algorithm Tests Complete! Press any key to exit.");
            Console.ReadKey();
        }

        private static void RunTestSort(List<int[]> randomNumberArrays, string sortDetail, bool useMedianOfThree, int insertionSortCutoff)
        {
            var resultList = new List<SortResult>();

            Console.WriteLine();
            Console.WriteLine($"Running {sortDetail} on {ArrayCount} Sets of {RecordCount} Records...");

            for (var i = 0; i < randomNumberArrays.Count; i++)
            {
                var array = randomNumberArrays[i];
                Console.Write($"Sorting Set {i + 1}... ");
                var copiedArray = array.Select(a => a).ToArray();
                var sortResult = SortArrayWithTimeResult(sortDetail, copiedArray, useMedianOfThree, insertionSortCutoff);
                Console.WriteLine($"Completed In {sortResult.TotalMilliseconds} ms");
                Console.Write("Validating Sort... ");
                if (IsSorted(sortResult))
                {
                    Console.WriteLine("Good!");
                    resultList.Add(sortResult);
                }
                else
                {
                    throw new Exception($"Array was not sorted correctly: {sortDetail}");
                }
            }

            Console.Write("Saving Results... ");
            SaveResults(resultList);
            resultList.Clear();
            Console.WriteLine("Done!");
        }

        private static bool IsSorted(SortResult result)
        {
            for (int i=1; i<result.SortedArray.Length; i++)
            {
                if (result.SortedArray[i].CompareTo(result.SortedArray[i - 1]) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static void CreateFile()
        {
            var header = "Algorithm,RunTime (ms),First 20 Values,Last 20 Values\r\n";
            File.WriteAllText("D:\\CS4050\\QuickSortResults.csv", header);
        }

        private static void SaveResults(List<SortResult> results)
        {
            var resultsAsTextList = new List<string>();
            results.ForEach(r => resultsAsTextList.Add(r.ToString()));
            File.AppendAllLines("D:\\CS4050\\QuickSortResults.csv", resultsAsTextList);
        }

        private static void RunPlainQuickSort()
        {
            Console.WriteLine();
            Console.WriteLine("Generating random number array of size 10000000...");

            var array = GetRandomNumberArray(10000000);

            for (int i=0; i<10; i++)
            {
                Console.WriteLine();
                Console.Write($"Sorting Iteration {i}...");

                var copiedArray = array.Select(x => x).ToArray();
                var sortResult = SortArrayWithTimeResult("Manual Execution", copiedArray, false, 0);

                Console.WriteLine();
                Console.WriteLine("Sorting Complete!");
                Console.WriteLine($"Sort Time in Milliseconds: {sortResult.TotalMilliseconds}");
                Console.Write("Validating Sort... ");
                if (IsSorted(sortResult)) Console.WriteLine("Good!");
                else Console.WriteLine("BAD!");
            }

            Console.WriteLine();
            Console.Write("Press any key to exit...");
            Console.ReadKey();
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
            Console.Write("Validating Sort... ");
            if (IsSorted(sortResult)) Console.WriteLine("Good!");
            else Console.WriteLine("BAD!");

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

        private static int[] GetRandomNumberArray(int count)
        {
            var random = new Random();
            var array = Enumerable.Repeat(0, count).Select(n => random.Next(0, count * 2)).ToArray();
            return array;
        }

        private static SortResult SortArrayWithTimeResult(string identifier, int[] array, bool useMedianOfThree, int insertionSortSize)
        {
            var quickSort = new QuickSort(useMedianOfThree, insertionSortSize);
            var startTime = DateTime.Now;
            var sortedArray = quickSort.Sort(array);
            if (insertionSortSize > 0)
            {
                quickSort.InsertionSort(sortedArray, 0, sortedArray.Length - 1);
            }
            var stopTime = DateTime.Now;

            var totalMilliseconds = (stopTime - startTime).TotalMilliseconds;

            return new SortResult(identifier, totalMilliseconds, sortedArray);
        }
    }
}
