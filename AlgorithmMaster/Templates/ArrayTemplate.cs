using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for Array-based problems
    /// Common patterns: Two Pointers, Sliding Window, Prefix Sum, Sorting
    /// </summary>
    public static class ArrayTemplate
    {
        // ========================================
        // BASIC ARRAY OPERATIONS
        // ========================================
        
        /// <summary>
        /// Template for finding maximum/minimum in array
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int FindMax(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                throw new ArgumentException("Array cannot be null or empty");
                
            int max = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > max)
                    max = nums[i];
            }
            return max;
        }
        
        /// <summary>
        /// Template for array reversal
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static void Reverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                int temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;
                start++;
                end--;
            }
        }
        
        /// <summary>
        /// Template for rotating array
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static void Rotate(int[] nums, int k)
        {
            k = k % nums.Length;
            if (k == 0) return;
            
            Reverse(nums, 0, nums.Length - 1);
            Reverse(nums, 0, k - 1);
            Reverse(nums, k, nums.Length - 1);
        }
        
        // ========================================
        // TWO POINTERS PATTERN
        // ========================================
        
        /// <summary>
        /// Template for two pointers from both ends
        /// Time: O(n), Space: O(1) or O(n) depending on problem
        /// </summary>
        public static int[] TwoPointersBothEnds(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            
            while (left < right)
            {
                int sum = nums[left] + nums[right];
                
                if (sum == target)
                {
                    return new int[] { left, right };
                }
                else if (sum < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
            
            return new int[0]; // Not found
        }
        
        /// <summary>
        /// Template for two pointers moving together
        /// Time: O(n), Space: O(1) or O(n)
        /// </summary>
        public static int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0) return 0;
            
            int slow = 0;
            for (int fast = 1; fast < nums.Length; fast++)
            {
                if (nums[fast] != nums[slow])
                {
                    slow++;
                    nums[slow] = nums[fast];
                }
            }
            
            return slow + 1;
        }
        
        // ========================================
        // PREFIX SUM PATTERN
        // ========================================
        
        /// <summary>
        /// Template for prefix sum calculations
        /// Time: O(n) preprocessing, O(1) query, Space: O(n)
        /// </summary>
        public class PrefixSum
        {
            private int[] prefixSum;
            
            public PrefixSum(int[] nums)
            {
                prefixSum = new int[nums.Length + 1];
                for (int i = 0; i < nums.Length; i++)
                {
                    prefixSum[i + 1] = prefixSum[i] + nums[i];
                }
            }
            
            public int SumRange(int left, int right)
            {
                return prefixSum[right + 1] - prefixSum[left];
            }
        }
        
        // ========================================
        // COMMON ALGORITHM PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for finding subarray with given sum (Sliding Window)
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static bool SubarraySum(int[] nums, int target)
        {
            int left = 0;
            int currentSum = 0;
            
            for (int right = 0; right < nums.Length; right++)
            {
                currentSum += nums[right];
                
                while (currentSum > target && left <= right)
                {
                    currentSum -= nums[left];
                    left++;
                }
                
                if (currentSum == target)
                    return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Template for finding majority element (Boyer-Moore Voting)
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int MajorityElement(int[] nums)
        {
            int candidate = nums[0];
            int count = 1;
            
            for (int i = 1; i < nums.Length; i++)
            {
                if (count == 0)
                {
                    candidate = nums[i];
                    count = 1;
                }
                else if (nums[i] == candidate)
                {
                    count++;
                }
                else
                {
                    count--;
                }
            }
            
            return candidate;
        }
        
        // ========================================
        // HELPER METHODS
        // ========================================
        
        public static void PrintArray(int[] nums, string name = "Array")
        {
            Console.WriteLine($"{name}: [{string.Join(", ", nums)}]");
        }
        
        public static void PrintMatrix(int[,] matrix, string name = "Matrix")
        {
            Console.WriteLine($"{name}:");
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            for (int i = 0; i < rows; i++)
            {
                Console.Write("  [");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j]);
                    if (j < cols - 1) Console.Write(", ");
                }
                Console.WriteLine("]");
            }
        }
    }
}