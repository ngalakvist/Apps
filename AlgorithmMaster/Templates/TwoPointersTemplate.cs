using AlgorithmMaster.Utils;
using System;
using System.Collections.Generic;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for Two Pointers technique
    /// Common patterns: Opposite pointers, Same direction pointers, Fast/Slow pointers
    /// </summary>
    public static class TwoPointersTemplate
    {
        // ========================================
        // OPPOSITE POINTERS (FROM BOTH ENDS)
        // ========================================

        /// <summary>
        /// Template for two pointers from opposite ends
        /// Classic pattern: sorted array problems
        /// Time: O(n), Space: O(1) or O(n)
        /// </summary>
        public static int[] TwoSumSorted(int[] nums, int target)
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
                    left++; // Need larger sum
                }
                else
                {
                    right--; // Need smaller sum
                }
            }

            return new int[0]; // Not found
        }

        /// <summary>
        /// Template for valid palindrome check
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static bool IsPalindrome(string s)
        {
            int left = 0;
            int right = s.Length - 1;

            while (left < right)
            {
                // Skip non-alphanumeric characters
                while (left < right && !char.IsLetterOrDigit(s[left])) left++;
                while (left < right && !char.IsLetterOrDigit(s[right])) right--;

                if (char.ToLower(s[left]) != char.ToLower(s[right]))
                    return false;

                left++;
                right--;
            }

            return true;
        }

        /// <summary>
        /// Template for container with most water
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int MaxArea(int[] height)
        {
            int left = 0;
            int right = height.Length - 1;
            int maxArea = 0;

            while (left < right)
            {
                int width = right - left;
                int minHeight = Math.Min(height[left], height[right]);
                int area = width * minHeight;

                maxArea = Math.Max(maxArea, area);

                // Move the pointer with smaller height
                if (height[left] < height[right])
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return maxArea;
        }

        // ========================================
        // SAME DIRECTION POINTERS (SLOW/FAST)
        // ========================================

        /// <summary>
        /// Template for removing duplicates from sorted array
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0) return 0;

            int slow = 0; // Position to place the next unique element

            for (int fast = 1; fast < nums.Length; fast++)
            {
                if (nums[fast] != nums[slow])
                {
                    slow++;
                    nums[slow] = nums[fast];
                }
            }

            return slow + 1; // Length of unique elements
        }

        /// <summary>
        /// Template for removing element from array
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int RemoveElement(int[] nums, int val)
        {
            int slow = 0; // Position to place the next non-val element

            for (int fast = 0; fast < nums.Length; fast++)
            {
                if (nums[fast] != val)
                {
                    nums[slow] = nums[fast];
                    slow++;
                }
            }

            return slow; // Length of elements not equal to val
        }

        /// <summary>
        /// Template for finding first missing positive
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static int FirstMissingPositive(int[] nums)
        {
            int n = nums.Length;

            // Place each number in its correct position
            for (int i = 0; i < n; i++)
            {
                while (nums[i] > 0 && nums[i] <= n && nums[nums[i] - 1] != nums[i])
                {
                    // Swap nums[i] with nums[nums[i] - 1]
                    int temp = nums[nums[i] - 1];
                    nums[nums[i] - 1] = nums[i];
                    nums[i] = temp;
                }
            }

            // Find the first missing positive
            for (int i = 0; i < n; i++)
            {
                if (nums[i] != i + 1)
                    return i + 1;
            }

            return n + 1;
        }

        // ========================================
        // FAST/SLOW POINTERS (CYCLE DETECTION)
        // ========================================

        /// <summary>
        /// Template for detecting cycle in linked list
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static bool HasCycle(ListNode head)
        {
            if (head == null || head.Next == null) return false;

            ListNode slow = head;
            ListNode fast = head.Next;

            while (fast != null && fast.Next != null)
            {
                if (slow == fast) return true;

                slow = slow.Next;
                fast = fast.Next.Next;
            }

            return false;
        }

        /// <summary>
        /// Template for finding cycle start in linked list
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static ListNode DetectCycle(ListNode head)
        {
            if (head == null || head.Next == null) return null;

            // Phase 1: Detect cycle
            ListNode slow = head;
            ListNode fast = head;
            bool hasCycle = false;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;

                if (slow == fast)
                {
                    hasCycle = true;
                    break;
                }
            }

            if (!hasCycle) return null;

            // Phase 2: Find cycle start
            slow = head;
            while (slow != fast)
            {
                slow = slow.Next;
                fast = fast.Next;
            }

            return slow;
        }

        /// <summary>
        /// Template for finding middle of linked list
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            return slow;
        }

        // ========================================
        // THREE POINTERS PATTERN
        // ========================================

        /// <summary>
        /// Template for 3Sum problem
        /// Time: O(n²), Space: O(1) or O(n) for result
        /// </summary>
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            Array.Sort(nums);

            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue; // Skip duplicates

                int left = i + 1;
                int right = nums.Length - 1;

                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];

                    if (sum == 0)
                    {
                        result.Add(new List<int> { nums[i], nums[left], nums[right] });

                        // Skip duplicates
                        while (left < right && nums[left] == nums[left + 1]) left++;
                        while (left < right && nums[right] == nums[right - 1]) right--;

                        left++;
                        right--;
                    }
                    else if (sum < 0)
                    {
                        left++; // Need larger sum
                    }
                    else
                    {
                        right--; // Need smaller sum
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Template for sorting colors (Dutch National Flag)
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static void SortColors(int[] nums)
        {
            int left = 0;     // Position for 0s
            int right = nums.Length - 1; // Position for 2s
            int current = 0;  // Current element being examined

            while (current <= right)
            {
                if (nums[current] == 0)
                {
                    Swap(nums, current, left);
                    left++;
                    current++;
                }
                else if (nums[current] == 2)
                {
                    Swap(nums, current, right);
                    right--; // Don't increment current, need to check swapped element
                }
                else
                {
                    current++;
                }
            }
        }

        // ========================================
        // MERGE WITH TWO POINTERS
        // ========================================

        /// <summary>
        /// Template for merging two sorted arrays
        /// Time: O(m + n), Space: O(m + n)
        /// </summary>
        public static int[] MergeSortedArrays(int[] nums1, int[] nums2)
        {
            int[] result = new int[nums1.Length + nums2.Length];
            int i = 0, j = 0, k = 0;

            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] <= nums2[j])
                {
                    result[k++] = nums1[i++];
                }
                else
                {
                    result[k++] = nums2[j++];
                }
            }

            while (i < nums1.Length)
            {
                result[k++] = nums1[i++];
            }

            while (j < nums2.Length)
            {
                result[k++] = nums2[j++];
            }

            return result;
        }

        /// <summary>
        /// Template for merge sort using two pointers
        /// Time: O(n log n), Space: O(n)
        /// </summary>
        public static void MergeSort(int[] nums, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                MergeSort(nums, left, mid);
                MergeSort(nums, mid + 1, right);
                Merge(nums, left, mid, right);
            }
        }

        private static void Merge(int[] nums, int left, int mid, int right)
        {
            int[] temp = new int[right - left + 1];
            int i = left, j = mid + 1, k = 0;

            while (i <= mid && j <= right)
            {
                if (nums[i] <= nums[j])
                {
                    temp[k++] = nums[i++];
                }
                else
                {
                    temp[k++] = nums[j++];
                }
            }

            while (i <= mid)
            {
                temp[k++] = nums[i++];
            }

            while (j <= right)
            {
                temp[k++] = nums[j++];
            }

            for (int p = 0; p < temp.Length; p++)
            {
                nums[left + p] = temp[p];
            }
        }

        // ========================================
        // HELPER METHODS
        // ========================================

        private static void Swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        public static void PrintPointers(int[] nums, int left, int right, string message = "")
        {
            Console.WriteLine($"{message}");
            Console.WriteLine($"  Array: [{string.Join(", ", nums)}]");
            Console.WriteLine($"  Left: {left} (value: {nums[left]}), Right: {right} (value: {nums[right]})");
            Console.WriteLine();
        }

        public static void PrintPointers(int[] nums, int slow, int fast, int current, string message = "")
        {
            Console.WriteLine($"{message}");
            Console.WriteLine($"  Array: [{string.Join(", ", nums)}]");
            Console.WriteLine($"  Slow: {slow} (value: {nums[slow]}), Fast: {fast} (value: {nums[fast]}), Current: {current} (value: {nums[current]})");
            Console.WriteLine();
        }
    }
}