using AlgorithmMaster.Templates;
using System;
using System.Linq;

namespace AlgorithmMaster.Examples
{
    public static class ArrayExamples
    {
        public static void RunAll()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("         ARRAY ALGORITHM EXAMPLES");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Example1_BasicOperations();
            Example2_TwoPointers();
            Example3_PrefixSum();
            Example4_SlidingWindow();
            Example5_AdvancedPatterns();
        }

        private static void Example1_BasicOperations()
        {
            Console.WriteLine("EXAMPLE 1: Basic Array Operations");
            Console.WriteLine("----------------------------------");

            int[] nums = { 3, 1, 4, 1, 5, 9, 2, 6 };
            ArrayTemplate.PrintArray(nums, "Original Array");

            // Find maximum
            int max = ArrayTemplate.FindMax(nums);
            Console.WriteLine($"Maximum element: {max}");

            // Reverse array
            int[] reversed = (int[])nums.Clone();
            ArrayTemplate.Reverse(reversed, 0, reversed.Length - 1);
            ArrayTemplate.PrintArray(reversed, "Reversed Array");

            // Rotate array
            int[] rotated = (int[])nums.Clone();
            ArrayTemplate.Rotate(rotated, 3);
            ArrayTemplate.PrintArray(rotated, "Rotated by 3");

            Console.WriteLine();
        }

        private static void Example2_TwoPointers()
        {
            Console.WriteLine("EXAMPLE 2: Two Pointers Pattern");
            Console.WriteLine("--------------------------------");

            int[] sortedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ArrayTemplate.PrintArray(sortedArray, "Sorted Array");

            // Two sum in sorted array
            int target = 7;
            int[] indices = ArrayTemplate.TwoPointersBothEnds(sortedArray, target);
            Console.WriteLine($"Two sum for {target}: indices [{string.Join(", ", indices)}]");

            // Remove duplicates
            int[] withDuplicates = { 1, 1, 2, 2, 2, 3, 4, 4, 5 };
            int uniqueCount = ArrayTemplate.RemoveDuplicates(withDuplicates);
            Console.WriteLine($"Unique elements: {uniqueCount}");
            ArrayTemplate.PrintArray(withDuplicates.Take(uniqueCount).ToArray(), "Unique Elements");

            Console.WriteLine();
        }

        private static void Example3_PrefixSum()
        {
            Console.WriteLine("EXAMPLE 3: Prefix Sum Pattern");
            Console.WriteLine("-----------------------------");

            int[] nums = { 1, 2, 3, 4, 5, 6 };
            ArrayTemplate.PrintArray(nums, "Array");

            // Create prefix sum array
            var prefixSum = new ArrayTemplate.PrefixSum(nums);

            Console.WriteLine("Range sums:");
            Console.WriteLine($"  Sum from index 0 to 2: {prefixSum.SumRange(0, 2)}");
            Console.WriteLine($"  Sum from index 1 to 4: {prefixSum.SumRange(1, 4)}");
            Console.WriteLine($"  Sum from index 2 to 5: {prefixSum.SumRange(2, 5)}");

            Console.WriteLine();
        }

        private static void Example4_SlidingWindow()
        {
            Console.WriteLine("EXAMPLE 4: Sliding Window Pattern");
            Console.WriteLine("---------------------------------");

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ArrayTemplate.PrintArray(nums, "Array");

            // Maximum sum of 3 consecutive elements
            //int maxSum = ArrayTemplate.MaxSumKConsecutive(nums, 3);
            //Console.WriteLine($"Max sum of 3 consecutive: {maxSum}");

            // Subarray sum equals target
            int target = 15;
            bool hasSubarray = ArrayTemplate.SubarraySum(nums, target);
            Console.WriteLine($"Has subarray with sum {target}: {hasSubarray}");

            // Majority element
            int[] majorityArray = { 2, 2, 1, 1, 1, 2, 2 };
            ArrayTemplate.PrintArray(majorityArray, "Majority Array");
            int majority = ArrayTemplate.MajorityElement(majorityArray);
            Console.WriteLine($"Majority element: {majority}");

            Console.WriteLine();
        }

        private static void Example5_AdvancedPatterns()
        {
            Console.WriteLine("EXAMPLE 5: Advanced Patterns");
            Console.WriteLine("----------------------------");

            // First missing positive
            int[] missingPositive = { 3, 4, -1, 1 };
            //ArrayTemplate.PrintArray(missingPositive, "Missing Positive Array");
            //int missing = ArrayTemplate.FirstMissingPositive(missingPositive);
            //Console.WriteLine($"First missing positive: {missing}");

            // Container with most water
            //int[] heights = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
            //ArrayTemplate.PrintArray(heights, "Container Heights");
            //int maxArea = ArrayTemplate.MaxArea(heights);
            //Console.WriteLine($"Maximum container area: {maxArea}");

            Console.WriteLine();
        }

        // Test method for unit testing
        public static bool TestArrayOperations()
        {
            try
            {
                int[] testArray = { 1, 2, 3, 4, 5 };
                int max = ArrayTemplate.FindMax(testArray);
                if (max != 5) return false;

                int[] reversed = (int[])testArray.Clone();
                ArrayTemplate.Reverse(reversed, 0, reversed.Length - 1);
                if (!reversed.SequenceEqual(new int[] { 5, 4, 3, 2, 1 })) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}