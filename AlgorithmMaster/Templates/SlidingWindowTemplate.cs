using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for Sliding Window technique
    /// Common patterns: Fixed window, Variable window, Two pointers window
    /// </summary>
    public static class SlidingWindowTemplate
    {
        // ========================================
        // FIXED SIZE WINDOW
        // ========================================
        
        /// <summary>
        /// Template for fixed-size sliding window
        /// Problem: Find max sum of k consecutive elements
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int MaxSumKConsecutive(int[] nums, int k)
        {
            if (nums.Length < k) return 0;
            
            // Initialize window
            int windowSum = 0;
            for (int i = 0; i < k; i++)
            {
                windowSum += nums[i];
            }
            
            int maxSum = windowSum;
            
            // Slide the window
            for (int i = k; i < nums.Length; i++)
            {
                windowSum = windowSum - nums[i - k] + nums[i];
                maxSum = Math.Max(maxSum, windowSum);
            }
            
            return maxSum;
        }
        
        /// <summary>
        /// Template for average of subarray of size k
        /// Time: O(n), Space: O(n)
        /// </summary>
        public static double[] FindAverages(int[] nums, int k)
        {
            if (nums.Length < k) return new double[0];
            
            double[] result = new double[nums.Length - k + 1];
            double windowSum = 0;
            
            // Initialize window
            for (int i = 0; i < k; i++)
            {
                windowSum += nums[i];
            }
            result[0] = windowSum / k;
            
            // Slide the window
            for (int i = k; i < nums.Length; i++)
            {
                windowSum = windowSum - nums[i - k] + nums[i];
                result[i - k + 1] = windowSum / k;
            }
            
            return result;
        }
        
        /// <summary>
        /// Template for first negative in every window of size k
        /// Time: O(n), Space: O(k)
        /// </summary>
        public static int[] FirstNegativeInWindow(int[] nums, int k)
        {
            if (nums.Length < k) return new int[0];
            
            List<int> result = new List<int>();
            Queue<int> negatives = new Queue<int>();
            
            for (int i = 0; i < nums.Length; i++)
            {
                // Add current element to queue if negative
                if (nums[i] < 0)
                    negatives.Enqueue(i);
                
                // Remove elements outside window
                while (negatives.Count > 0 && negatives.Peek() <= i - k)
                {
                    negatives.Dequeue();
                }
                
                // Start adding to result after first window
                if (i >= k - 1)
                {
                    result.Add(negatives.Count > 0 ? nums[negatives.Peek()] : 0);
                }
            }
            
            return result.ToArray();
        }
        
        // ========================================
        // VARIABLE SIZE WINDOW
        // ========================================
        
        /// <summary>
        /// Template for longest substring without repeating characters
        /// Variable window with hash map
        /// Time: O(n), Space: O(min(n, k)) where k is charset size
        /// </summary>
        public static int LengthOfLongestSubstring(string s)
        {
            Dictionary<char, int> charIndex = new Dictionary<char, int>();
            int maxLength = 0;
            int left = 0;
            
            for (int right = 0; right < s.Length; right++)
            {
                char c = s[right];
                
                // If character seen and within current window, move left
                if (charIndex.ContainsKey(c) && charIndex[c] >= left)
                {
                    left = charIndex[c] + 1;
                }
                
                charIndex[c] = right;
                maxLength = Math.Max(maxLength, right - left + 1);
            }
            
            return maxLength;
        }
        
        /// <summary>
        /// Template for minimum window substring
        /// Variable window with two pointers
        /// Time: O(n), Space: O(k) where k is charset size
        /// </summary>
        public static string MinWindow(string s, string t)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
                return "";
                
            Dictionary<char, int> targetCount = new Dictionary<char, int>();
            foreach (char c in t)
            {
                targetCount[c] = targetCount.GetValueOrDefault(c, 0) + 1;
            }
            
            int required = targetCount.Count;
            int formed = 0;
            Dictionary<char, int> windowCount = new Dictionary<char, int>();
            
            int left = 0;
            int minLength = int.MaxValue;
            int minLeft = 0;
            
            for (int right = 0; right < s.Length; right++)
            {
                char c = s[right];
                windowCount[c] = windowCount.GetValueOrDefault(c, 0) + 1;
                
                if (targetCount.ContainsKey(c) && windowCount[c] == targetCount[c])
                {
                    formed++;
                }
                
                // Try to contract window while it's valid
                while (left <= right && formed == required)
                {
                    if (right - left + 1 < minLength)
                    {
                        minLength = right - left + 1;
                        minLeft = left;
                    }
                    
                    char leftChar = s[left];
                    windowCount[leftChar]--;
                    
                    if (targetCount.ContainsKey(leftChar) && windowCount[leftChar] < targetCount[leftChar])
                    {
                        formed--;
                    }
                    
                    left++;
                }
            }
            
            return minLength == int.MaxValue ? "" : s.Substring(minLeft, minLength);
        }
        
        /// <summary>
        /// Template for longest substring with k distinct characters
        /// Variable window with distinct character count
        /// Time: O(n), Space: O(k)
        /// </summary>
        public static int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            if (k == 0 || s.Length == 0) return 0;
            
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            int left = 0;
            int maxLength = 0;
            
            for (int right = 0; right < s.Length; right++)
            {
                char c = s[right];
                charCount[c] = charCount.GetValueOrDefault(c, 0) + 1;
                
                // Shrink window if we have more than k distinct characters
                while (charCount.Count > k)
                {
                    char leftChar = s[left];
                    charCount[leftChar]--;
                    if (charCount[leftChar] == 0)
                    {
                        charCount.Remove(leftChar);
                    }
                    left++;
                }
                
                maxLength = Math.Max(maxLength, right - left + 1);
            }
            
            return maxLength;
        }
        
        /// <summary>
        /// Template for fruit into baskets (at most 2 distinct types)
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int TotalFruit(int[] fruits)
        {
            Dictionary<int, int> basket = new Dictionary<int, int>();
            int left = 0;
            int maxFruits = 0;
            
            for (int right = 0; right < fruits.Length; right++)
            {
                int fruit = fruits[right];
                basket[fruit] = basket.GetValueOrDefault(fruit, 0) + 1;
                
                // If we have more than 2 types, shrink window
                while (basket.Count > 2)
                {
                    int leftFruit = fruits[left];
                    basket[leftFruit]--;
                    if (basket[leftFruit] == 0)
                    {
                        basket.Remove(leftFruit);
                    }
                    left++;
                }
                
                maxFruits = Math.Max(maxFruits, right - left + 1);
            }
            
            return maxFruits;
        }
        
        // ========================================
        // SLIDING WINDOW WITH DEQUE
        // ========================================
        
        /// <summary>
        /// Template for sliding window maximum
        /// Using deque to maintain window maximums
        /// Time: O(n), Space: O(k)
        /// </summary>
        public static int[] MaxSlidingWindow(int[] nums, int k)
        {
            if (nums.Length == 0 || k == 0) return new int[0];
            if (k == 1) return nums;
            
            int[] result = new int[nums.Length - k + 1];
            LinkedList<int> deque = new LinkedList<int>();
            
            for (int i = 0; i < nums.Length; i++)
            {
                // Remove elements outside window from front
                while (deque.Count > 0 && deque.First.Value < i - k + 1)
                {
                    deque.RemoveFirst();
                }
                
                // Remove smaller elements from back
                while (deque.Count > 0 && nums[deque.Last.Value] < nums[i])
                {
                    deque.RemoveLast();
                }
                
                deque.AddLast(i);
                
                // Add to result when window is complete
                if (i >= k - 1)
                {
                    result[i - k + 1] = nums[deque.First.Value];
                }
            }
            
            return result;
        }
        
        /// <summary>
        /// Template for sliding window median
        /// Time: O(n log k), Space: O(k)
        /// </summary>
        public static double[] MedianSlidingWindow(int[] nums, int k)
        {
            if (nums.Length == 0 || k == 0) return new double[0];
            
            double[] result = new double[nums.Length - k + 1];
            List<int> window = new List<int>();
            
            for (int i = 0; i < nums.Length; i++)
            {
                // Add new element to window
                int insertPos = BinarySearchInsert(window, nums[i]);
                window.Insert(insertPos, nums[i]);
                
                // Remove old element if window is too large
                if (window.Count > k)
                {
                    int removePos = BinarySearch(window, nums[i - k]);
                    window.RemoveAt(removePos);
                }
                
                // Calculate median when window is complete
                if (window.Count == k)
                {
                    result[i - k + 1] = CalculateMedian(window);
                }
            }
            
            return result;
        }
        
        // ========================================
        // PREFIX SUM + SLIDING WINDOW
        // ========================================
        
        /// <summary>
        /// Template for subarray sum equals k
        /// Using prefix sum with sliding window concept
        /// Time: O(n), Space: O(n)
        /// </summary>
        public static int SubarraySum(int[] nums, int k)
        {
            Dictionary<int, int> prefixSumCount = new Dictionary<int, int> { [0] = 1 };
            int prefixSum = 0;
            int count = 0;
            
            foreach (int num in nums)
            {
                prefixSum += num;
                
                if (prefixSumCount.ContainsKey(prefixSum - k))
                {
                    count += prefixSumCount[prefixSum - k];
                }
                
                prefixSumCount[prefixSum] = prefixSumCount.GetValueOrDefault(prefixSum, 0) + 1;
            }
            
            return count;
        }
        
        /// <summary>
        /// Template for number of subarrays with sum at most k
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int CountSubarraysWithSumAtMost(int[] nums, int k)
        {
            int left = 0;
            int sum = 0;
            int count = 0;
            
            for (int right = 0; right < nums.Length; right++)
            {
                sum += nums[right];
                
                // Shrink window if sum exceeds k
                while (sum > k && left <= right)
                {
                    sum -= nums[left];
                    left++;
                }
                
                // Count all valid subarrays ending at right
                count += right - left + 1;
            }
            
            return count;
        }
        
        // ========================================
        // HELPER METHODS
        // ========================================
        
        private static int BinarySearchInsert(List<int> list, int target)
        {
            int left = 0;
            int right = list.Count;
            
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (list[mid] < target)
                    left = mid + 1;
                else
                    right = mid;
            }
            
            return left;
        }
        
        private static int BinarySearch(List<int> list, int target)
        {
            int left = 0;
            int right = list.Count - 1;
            
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (list[mid] == target)
                    return mid;
                else if (list[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            
            return -1;
        }
        
        private static double CalculateMedian(List<int> window)
        {
            int n = window.Count;
            if (n % 2 == 1)
            {
                return window[n / 2];
            }
            else
            {
                return (window[n / 2 - 1] + window[n / 2]) / 2.0;
            }
        }
        
        public static void PrintWindow(int[] nums, int left, int right, string message = "")
        {
            Console.WriteLine($"{message}");
            Console.WriteLine($"  Array: [{string.Join(", ", nums)}]");
            Console.WriteLine($"  Window: [{string.Join(", ", nums.Skip(left).Take(right - left + 1))}]");
            Console.WriteLine($"  Left: {left}, Right: {right}, Size: {right - left + 1}");
            Console.WriteLine();
        }
    }
}