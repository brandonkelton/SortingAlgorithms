This paper examines the Quick Sort algorithm and analyzes a few implementation variations.  This research involved three distinct applications, plain old Quick Sort, Median of Three and applying Insertion Sort at a specific cutoff point.  Each algorithm execution was timed strictly around the sorting function so that other functions did not influence the overall run time.  Each algorithm was executed five times with each of those five iterations involving a distinct random integer array of size 10 million.
There are various algorithms that can be applied to a data set with each algorithm applicable to specific circumstances. Quick Sort is an extremely fast algorithm that breaks up an array into subarrays, selects a pivot from each subarray and then swaps values around that pivot so that lower values are below the pivot and higher values are above the pivot.  It does this recursively such that each pivot value indicates a partition, separating lower values from higher values, at which point the algorithm is called again on each partition.  In the end, the final subarrays contain one value with nothing more to sort.  
The two algorithm modifications I applied make small tweaks to the overall Quick Sort function.  Median of Three is a technique that helps prevent inadvertently selecting bad pivots, which may occasionally result in longer sort times if the pivot value happens to be the highest or lowest value in the subarray.  Insertion Sort is very fast for arrays that are mostly sorted, so applying it near the end of a Quick Sort can result in an improvement of the algorithm’s speed.

Table 1, below, provides the average run times of each algorithm.  To minimize data set influenced variation of the results, I created five distinct arrays of random integers and then copied the arrays for each algorithm execution so that each algorithm ran against the same unsorted set of arrays that each other algorithm ran against.  More specifically, the plain Quick Sort algorithm ran five times with each iteration using a distinct unsorted array, while the Quick Sort with Median of Three used a copy of those same arrays, and so forth.

Table 1. Quick Sort Algorithm Results
Algorithm	Run Time (ms)
Plain Quick Sort	4398.15036
Quick Sort with Insertion Sort Cutoff of 10	3581.34146
Quick Sort with Insertion Sort Cutoff of 15	3878.353
Quick Sort with Insertion Sort Cutoff of 20	3446.16366
Quick Sort with Insertion Sort Cutoff of 50	3673.77414
Quick Sort Median of Three	4351.25262
QuickSort with Median of Three and InsertionSort CutOff of 10	3730.47664
QuickSort with Median of Three and InsertionSort CutOff of 15	3617.03658
QuickSort with Median of Three and InsertionSort CutOff of 20	3477.04902
QuickSort with Median of Three and InsertionSort CutOff of 50	3476.55314

I created a console application in C#, which is very similar to Java, and added prompts so that I could test regular Quick Sort and various combinations of Quick Sort with Median of Three and Insertion Sort.  Additionally, I wrote functionality to apply these algorithms in an automated assortment of timed tests that, upon completion, save their results to a file for later analysis.
I wrote Quick Sort such that the first call to sort the array selects a pivot from the middle, swaps it with the last element in the array and then moves a cursor up from the bottom looking for a value that is higher than the pivot.  After that, a cursor moves down from the last-element-minus-one position in the array looking for an element that is lower in value than the pivot.  Those values are swapped and the position of the lower swap is returned as a partition index.  At that point, a recursive call to sort the lower half of the partition is made and the algorithm continues in that manner until it makes a recursive call to sort the upper half of the array in the same fashion.  This plain Quick Sort method took the longest of all the algorithms at approximately 4.4 seconds.
Following that test, I added variations of Insertion Sort with cutoffs of 10, 15, 20 and 50.  This algorithm stopped quick sorting when the subarray decreased to sizes of these values.  Upon completion of quick sorting the entire array, with regard to these stops, an Insertion Sort was applied to the full array.  This resulted in a significant decrease of run time with a cutoff of 20 achieving the best run time of approximately 3.4 seconds, a decrease of almost one second.
Upon testing the application of Median of Three, I did not see a significant decrease in run time.  In fact, it was only slightly better or slightly worse than Median of Three vs. Plain Quick Sort and their corresponding Insertion Sort variations.  The benefit of using Median of Three, which prevents bad pivot selections, would still outweigh any longer run time, as the run time difference is not significant enough at 10 million random integer records.
During my effort to build this solution, I referenced several online sources to gain a better understanding of Quick Sort and the variations that I applied.  In the case of Median of Three, I copied and tweaked some code, while my Quick Sort code is mostly my own derived from several examples.  As stated by the website GeeksForGeeks, the following is a mathematical description of the run time for Quick Sort.

Best Case:			T(n) = 2T(n/2) + θ(n)			->	θ(nLogn)
Average Case:		T(n) = T(n/9) + T(9n/10) + θ(n)	->	θ(nLogn)
Worst Case:  		T(n) = T(n-1) + θ(n)			->	θ(n2)


 
References

https://stackoverflow.com/questions/7559608/median-of-three-values-strategy
https://www.hackerearth.com/practice/algorithms/sorting/quick-sort/visualize/
https://www.java-tips.org/java-se-tips-100019/24-java-lang/1896-quick-sort-implementation-with-median-of-three-partitioning-and-cutoff-for-small-arrays.html
https://www.geeksforgeeks.org/quick-sort/

