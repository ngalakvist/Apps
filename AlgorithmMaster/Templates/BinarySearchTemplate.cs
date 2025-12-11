using System;
using System.Linq;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for Binary Search problems
    /// Common patterns: Classic binary search, Search in rotated array, Find peak, etc.
    /// </summary>
    public static class BinarySearchTemplate
    {
        // ========================================
        // CLASSIC BINARY SEARCH
        // ========================================

        /// <summary>
        /// Template for classic binary search
        /// Returns index of target if found, otherwise -1
        /// Time: O(log n), Space: O(1)
        /// </summary>
        public static int BinarySearch(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                // Avoid potential overflow
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1; // Not found
        }

        /// <summary>
        /// Template for finding leftmost position
        /// Returns insertion point if not found
        /// Time: O(log n), Space: O(1)
        /// </summary>
        public static int FindLeftmost(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            int result = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                {
                    result = mid;
                    right = mid - 1; // Continue searching left
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return result;
        }

        /// <summary>
        /// Template for finding rightmost position
        /// Time: O(log n), Space: O(1)
        /// </summary>
        public static int FindRightmost(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            int result = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                {
                    result = mid;
                    left = mid + 1; // Continue searching right
                }
                else if (nums[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return result;
        }

        // ========================================
        // SEARCH IN ROTATED SORTED ARRAY
        // ========================================

        /// <summary>
        /// Template for search in rotated sorted array
        /// Time: O(log n), Space: O(1)
        /// </summary>
        public static int SearchRotated(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] == target)
                    return mid;

                // Determine which side is sorted
                if (nums[left] <= nums[mid]) // Left side is sorted
                {
                    if (nums[left] <= target && target < nums[mid])
                    {
                        right = mid - 1; // Target in left half
                    }
                    else
                    {
                        left = mid + 1; // Target in right half
                    }
                }
                else // Right side is sorted
                {
                    if (nums[mid] < target && target <= nums[right])
                    {
                        left = mid + 1; // Target in right half
                    }
                    else
                    {
                        right = mid - 1; // Target in left half
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Template for finding minimum in rotated sorted array
        /// Time: O(log n), Space: O(1)
        /// </summary>
        public static int FindMinRotated(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] > nums[right])
                {
                    left = mid + 1; // Minimum is in right half
                }
                else
                {
                    right = mid; // Minimum is in left half (including mid)
                }
            }

            return nums[left];
        }

        // ========================================
        // FIND PEAK ELEMENT
        // ========================================

        /// <summary>
        /// Template for finding peak element
        /// Time: O(log n), Space: O(1)
        /// </summary>
        public static int FindPeakElement(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] > nums[mid + 1])
                {
                    right = mid; // Peak is in left half (including mid)
                }
                else
                {
                    left = mid + 1; // Peak is in right half
                }
            }

            return left;
        }

        // ========================================
        // SEARCH 2D MATRIX
        // ========================================

        /// <summary>
        /// Template for search in 2D matrix
        /// Matrix is sorted row-wise and column-wise
        /// Time: O(log(m*n)), Space: O(1)
        /// </summary>
        public static bool SearchMatrix(int[,] matrix, int target)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int left = 0;
            int right = rows * cols - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int midValue = matrix[mid / cols, mid % cols];

                if (midValue == target)
                    return true;
                else if (midValue < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return false;
        }

        /// <summary>
        /// Alternative: Search from top-right corner
        /// Time: O(m + n), Space: O(1)
        /// </summary>
        public static bool SearchMatrixAlternative(int[,] matrix, int target)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int row = 0;
            int col = cols - 1;

            while (row < rows && col >= 0)
            {
                int value = matrix[row, col];

                if (value == target)
                    return true;
                else if (value > target)
                    col--; // Move left
                else
                    row++; // Move down
            }

            return false;
        }

        // ========================================
        // FIND KTH ELEMENT
        // ========================================

        /// <summary>
        /// Template for finding kth smallest element
        /// Time: O(log(max-min)), Space: O(1)
        /// </summary>
        public static int KthSmallest(int[,] matrix, int k)
        {
            int n = matrix.GetLength(0);
            int left = matrix[0, 0];
            int right = matrix[n - 1, n - 1];

            while (left < right)
            {
                int mid = left + (right - left) / 2;
                int count = CountLessEqual(matrix, mid);

                if (count < k)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            return left;
        }

        private static int CountLessEqual(int[,] matrix, int target)
        {
            int n = matrix.GetLength(0);
            int count = 0;
            int row = n - 1;
            int col = 0;

            while (row >= 0 && col < n)
            {
                if (matrix[row, col] <= target)
                {
                    count += row + 1;
                    col++;
                }
                else
                {
                    row--;
                }
            }

            return count;
        }

        // ========================================
        // ANSWER IN BINARY SEARCH
        // ========================================

        /// <summary>
        /// Template for finding square root
        /// Time: O(log n), Space: O(1)
        /// </summary>
        public static int MySqrt(int x)
        {
            if (x == 0) return 0;

            int left = 1;
            int right = x;
            int result = 0;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                // To avoid overflow, compare mid with x/mid
                if (mid <= x / mid)
                {
                    result = mid;
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return result;
        }

        /// <summary>
        /// Template for capacity to ship packages within D days
        /// Time: O(n log S) where S is sum of weights, Space: O(1)
        /// </summary>
        public static int ShipWithinDays(int[] weights, int days)
        {
            int left = weights.Max(); // Minimum capacity needed
            int right = weights.Sum(); // Maximum capacity needed

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (CanShip(weights, mid, days))
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return left;
        }

        private static bool CanShip(int[] weights, int capacity, int days)
        {
            int current = 0;
            int requiredDays = 1;

            foreach (int weight in weights)
            {
                if (current + weight > capacity)
                {
                    requiredDays++;
                    current = 0;
                }
                current += weight;
            }

            return requiredDays <= days;
        }

        // ========================================
        // HELPER METHODS
        // ========================================

        public static void PrintSearchStep(int[] nums, int left, int right, int mid, int target)
        {
            Console.WriteLine($"Left: {left}, Right: {right}, Mid: {mid}");
            Console.WriteLine($"nums[{mid}] = {nums[mid]}, Target: {target}");
            Console.WriteLine($"Array slice: [{string.Join(", ", nums.Skip(left).Take(right - left + 1))}]");
            Console.WriteLine();
        }

        public static int[] GenerateSortedArray(int size, int min = 0, int max = 100)
        {
            Random rand = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(min, max);
            }
            Array.Sort(array);
            return array;
        }
    }
}