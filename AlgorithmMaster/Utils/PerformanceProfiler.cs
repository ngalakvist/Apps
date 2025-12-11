using AlgorithmMaster.Templates;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmMaster.Utils
{
    /// <summary>
    /// Performance profiling utilities for algorithm comparison
    /// </summary>
    public static class PerformanceProfiler
    {
        public static void RunPerformanceTests()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("       PERFORMANCE PROFILING TESTS");
            Console.WriteLine("========================================");
            Console.WriteLine();

            CompareArrayOperations();
            CompareSearchAlgorithms();
            CompareSortingAlgorithms();
            CompareStringOperations();
        }

        private static void CompareArrayOperations()
        {
            Console.WriteLine("ARRAY OPERATIONS COMPARISON");
            Console.WriteLine("---------------------------");

            int[] sizes = { 100, 1000, 10000 };

            foreach (int size in sizes)
            {
                Console.WriteLine($"Array Size: {size}");

                // Generate test data
                int[] testArray = AlgorithmUtils.GenerateRandomArray(size, 0, size);
                int[] sortedArray = (int[])testArray.Clone();
                Array.Sort(sortedArray);

                // Test FindMax
                TestRunner.RunPerformanceTest($"FindMax (size {size})",
                    () => ArrayTemplate.FindMax(testArray), 1000);

                // Test Two Pointers (if array is sorted)
                TestRunner.RunPerformanceTest($"TwoPointers (size {size})",
                    () => ArrayTemplate.TwoPointersBothEnds(sortedArray, size / 2), 1000);

                Console.WriteLine();
            }
        }

        private static void CompareSearchAlgorithms()
        {
            Console.WriteLine("SEARCH ALGORITHMS COMPARISON");
            Console.WriteLine("----------------------------");

            int[] sizes = { 1000, 10000, 100000 };

            foreach (int size in sizes)
            {
                Console.WriteLine($"Array Size: {size}");

                int[] sortedArray = BinarySearchTemplate.GenerateSortedArray(size, 0, size * 2);
                int target = sortedArray[size / 2]; // Known to exist

                // Linear Search (for comparison)
                TestRunner.RunPerformanceTest($"Linear Search (size {size})",
                    () => LinearSearch(sortedArray, target), 100);

                // Binary Search
                TestRunner.RunPerformanceTest($"Binary Search (size {size})",
                    () => BinarySearchTemplate.BinarySearch(sortedArray, target), 1000);

                Console.WriteLine();
            }
        }

        private static void CompareSortingAlgorithms()
        {
            Console.WriteLine("SORTING ALGORITHMS COMPARISON");
            Console.WriteLine("-----------------------------");

            int[] sizes = { 100, 1000, 5000 };

            foreach (int size in sizes)
            {
                Console.WriteLine($"Array Size: {size}");

                int[] testArray = AlgorithmUtils.GenerateRandomArray(size, 0, size);

                // Built-in sort (for reference)
                TestRunner.RunPerformanceTest($"Built-in Sort (size {size})", () => { int[] arr = (int[])testArray.Clone(); Array.Sort(arr); }, 100);

                // Merge Sort (Two Pointers template)
                TestRunner.RunPerformanceTest($"Merge Sort (size {size})",
                    () => { int[] arr = (int[])testArray.Clone(); TwoPointersTemplate.MergeSort(arr, 0, arr.Length - 1); }, 10);

                Console.WriteLine();
            }
        }

        private static void CompareStringOperations()
        {
            Console.WriteLine("STRING OPERATIONS COMPARISON");
            Console.WriteLine("----------------------------");

            string[] testStrings = {
                "hello world",
                new string('a', 1000),
                "The quick brown fox jumps over the lazy dog"
            };

            foreach (string testString in testStrings)
            {
                Console.WriteLine($"String Length: {testString.Length}");

                // String reversal
                TestRunner.RunPerformanceTest($"String Reversal (len {testString.Length})",
                    () => StringTemplate.ReverseString(testString), 1000);

                // Palindrome check
                TestRunner.RunPerformanceTest($"Palindrome Check (len {testString.Length})",
                    () => StringTemplate.IsPalindrome(testString), 1000);

                Console.WriteLine();
            }
        }

        private static int LinearSearch(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Benchmark different algorithmic approaches
        /// </summary>
        public static void BenchmarkAlgorithms()
        {
            Console.WriteLine("Running comprehensive benchmarks...");
            Console.WriteLine("This may take several minutes...");
            Console.WriteLine();

            var summary = BenchmarkRunner.Run<AlgorithmBenchmarks>();

            Console.WriteLine("Benchmark completed!");
            Console.WriteLine("Check the output for detailed performance metrics.");
        }

        /// <summary>
        /// Memory usage analysis
        /// </summary>
        public static void AnalyzeMemoryUsage()
        {
            Console.WriteLine("MEMORY USAGE ANALYSIS");
            Console.WriteLine("---------------------");

            long memoryBefore = GC.GetTotalMemory(true);

            // Test different data structures
            int size = 10000;

            // Array
            int[] array = new int[size];
            long arrayMemory = GC.GetTotalMemory(true) - memoryBefore;
            Console.WriteLine($"Array of {size} integers: ~{arrayMemory / 1024} KB");

            // List
            memoryBefore = GC.GetTotalMemory(true);
            List<int> list = new List<int>(size);
            for (int i = 0; i < size; i++) list.Add(i);
            long listMemory = GC.GetTotalMemory(true) - memoryBefore;
            Console.WriteLine($"List of {size} integers: ~{listMemory / 1024} KB");

            // Dictionary
            memoryBefore = GC.GetTotalMemory(true);
            Dictionary<int, int> dict = new Dictionary<int, int>(size);
            for (int i = 0; i < size; i++) dict[i] = i;
            long dictMemory = GC.GetTotalMemory(true) - memoryBefore;
            Console.WriteLine($"Dictionary of {size} key-value pairs: ~{dictMemory / 1024} KB");

            Console.WriteLine();
        }

        /// <summary>
        /// Generate performance report
        /// </summary>
        public static void GenerateReport()
        {
            Console.WriteLine("PERFORMANCE ANALYSIS REPORT");
            Console.WriteLine("===========================");
            Console.WriteLine();

            Console.WriteLine("Algorithm Performance Summary:");
            Console.WriteLine("• Arrays: O(1) access, O(n) search/insert/delete");
            Console.WriteLine("• Hash Maps: O(1) average-case operations");
            Console.WriteLine("• Binary Search: O(log n) on sorted data");
            Console.WriteLine("• Two Pointers: O(n) for many array problems");
            Console.WriteLine("• Sliding Window: O(n) for subarray problems");
            Console.WriteLine("• BFS/DFS: O(V + E) for graph traversal");
            Console.WriteLine();

            Console.WriteLine("Recommendations:");
            Console.WriteLine("• Use hash maps for frequent lookups");
            Console.WriteLine("• Use binary search on sorted data");
            Console.WriteLine("• Use two pointers for array problems");
            Console.WriteLine("• Use sliding window for subarray problems");
            Console.WriteLine("• Consider space-time trade-offs");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Benchmark class for BenchmarkDotNet
    /// </summary>
    public class AlgorithmBenchmarks
    {
        private int[] _sortedArray;
        private int[] _randomArray;
        private string _testString;

        [GlobalSetup]
        public void Setup()
        {
            _sortedArray = Enumerable.Range(0, 10000).ToArray();
            _randomArray = AlgorithmUtils.GenerateRandomArray(10000, 0, 20000);
            _testString = new string('a', 1000);
        }

        [Benchmark]
        public void BinarySearch_SortedArray()
        {
            BinarySearchTemplate.BinarySearch(_sortedArray, 5000);
        }

        [Benchmark]
        public void LinearSearch_RandomArray()
        {
            for (int i = 0; i < _randomArray.Length; i++)
            {
                if (_randomArray[i] == 10000) break;
            }
        }

        [Benchmark]
        public void ArrayReverse()
        {
            int[] copy = (int[])_randomArray.Clone();
            ArrayTemplate.Reverse(copy, 0, copy.Length - 1);
        }

        [Benchmark]
        public void StringReverse()
        {
            StringTemplate.ReverseString(_testString);
        }

        [Benchmark]
        public void HashMapLookup()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < 1000; i++)
            {
                dict[i] = i * 2;
            }

            for (int i = 0; i < 1000; i++)
            {
                int value = dict[i];
            }
        }
    }
}