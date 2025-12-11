using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for Backtracking problems
    /// Common patterns: Subsets, Permutations, Combinations, Path finding
    /// </summary>
    public static class BacktrackingTemplate
    {
        // ========================================
        // SUBSETS PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for generating all subsets
        /// Time: O(2^n * n), Space: O(2^n * n)
        /// </summary>
        public static IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> current = new List<int>();
            
            BacktrackSubsets(nums, 0, current, result);
            
            return result;
        }
        
        private static void BacktrackSubsets(int[] nums, int start, List<int> current, IList<IList<int>> result)
        {
            // Add current subset to result
            result.Add(new List<int>(current));
            
            // Explore all possible extensions
            for (int i = start; i < nums.Length; i++)
            {
                current.Add(nums[i]);
                BacktrackSubsets(nums, i + 1, current, result);
                current.RemoveAt(current.Count - 1); // Backtrack
            }
        }
        
        /// <summary>
        /// Template for subsets with duplicates
        /// Time: O(2^n * n), Space: O(2^n * n)
        /// </summary>
        public static IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> current = new List<int>();
            Array.Sort(nums); // Sort to handle duplicates
            
            BacktrackSubsetsDup(nums, 0, current, result);
            
            return result;
        }
        
        private static void BacktrackSubsetsDup(int[] nums, int start, List<int> current, IList<IList<int>> result)
        {
            result.Add(new List<int>(current));
            
            for (int i = start; i < nums.Length; i++)
            {
                // Skip duplicates
                if (i > start && nums[i] == nums[i - 1]) continue;
                
                current.Add(nums[i]);
                BacktrackSubsetsDup(nums, i + 1, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }
        
        // ========================================
        // PERMUTATIONS PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for generating all permutations
        /// Time: O(n! * n), Space: O(n! * n)
        /// </summary>
        public static IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> current = new List<int>();
            bool[] used = new bool[nums.Length];
            
            BacktrackPermute(nums, used, current, result);
            
            return result;
        }
        
        private static void BacktrackPermute(int[] nums, bool[] used, List<int> current, IList<IList<int>> result)
        {
            if (current.Count == nums.Length)
            {
                result.Add(new List<int>(current));
                return;
            }
            
            for (int i = 0; i < nums.Length; i++)
            {
                if (used[i]) continue;
                
                used[i] = true;
                current.Add(nums[i]);
                BacktrackPermute(nums, used, current, result);
                current.RemoveAt(current.Count - 1);
                used[i] = false;
            }
        }
        
        /// <summary>
        /// Template for permutations with duplicates
        /// Time: O(n! * n), Space: O(n! * n)
        /// </summary>
        public static IList<IList<int>> PermuteUnique(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> current = new List<int>();
            bool[] used = new bool[nums.Length];
            Array.Sort(nums);
            
            BacktrackPermuteUnique(nums, used, current, result);
            
            return result;
        }
        
        private static void BacktrackPermuteUnique(int[] nums, bool[] used, List<int> current, IList<IList<int>> result)
        {
            if (current.Count == nums.Length)
            {
                result.Add(new List<int>(current));
                return;
            }
            
            for (int i = 0; i < nums.Length; i++)
            {
                if (used[i]) continue;
                // Skip duplicates
                if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) continue;
                
                used[i] = true;
                current.Add(nums[i]);
                BacktrackPermuteUnique(nums, used, current, result);
                current.RemoveAt(current.Count - 1);
                used[i] = false;
            }
        }
        
        // ========================================
        // COMBINATIONS PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for combinations (n choose k)
        /// Time: O(C(n,k) * k), Space: O(C(n,k) * k)
        /// </summary>
        public static IList<IList<int>> Combine(int n, int k)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> current = new List<int>();
            
            BacktrackCombine(n, k, 1, current, result);
            
            return result;
        }
        
        private static void BacktrackCombine(int n, int k, int start, List<int> current, IList<IList<int>> result)
        {
            if (current.Count == k)
            {
                result.Add(new List<int>(current));
                return;
            }
            
            // Optimization: only iterate while there's enough elements left
            for (int i = start; i <= n - (k - current.Count) + 1; i++)
            {
                current.Add(i);
                BacktrackCombine(n, k, i + 1, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }
        
        /// <summary>
        /// Template for combination sum
        /// Time: O(2^n * n), Space: O(2^n * n)
        /// </summary>
        public static IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> current = new List<int>();
            
            BacktrackCombinationSum(candidates, target, 0, current, result);
            
            return result;
        }
        
        private static void BacktrackCombinationSum(int[] candidates, int remaining, int start, List<int> current, IList<IList<int>> result)
        {
            if (remaining == 0)
            {
                result.Add(new List<int>(current));
                return;
            }
            
            if (remaining < 0) return;
            
            for (int i = start; i < candidates.Length; i++)
            {
                current.Add(candidates[i]);
                // Allow reuse of same element
                BacktrackCombinationSum(candidates, remaining - candidates[i], i, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }
        
        /// <summary>
        /// Template for combination sum II (each number used once)
        /// Time: O(2^n * n), Space: O(2^n * n)
        /// </summary>
        public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> current = new List<int>();
            Array.Sort(candidates);
            
            BacktrackCombinationSum2(candidates, target, 0, current, result);
            
            return result;
        }
        
        private static void BacktrackCombinationSum2(int[] candidates, int remaining, int start, List<int> current, IList<IList<int>> result)
        {
            if (remaining == 0)
            {
                result.Add(new List<int>(current));
                return;
            }
            
            if (remaining < 0) return;
            
            for (int i = start; i < candidates.Length; i++)
            {
                // Skip duplicates
                if (i > start && candidates[i] == candidates[i - 1]) continue;
                
                current.Add(candidates[i]);
                // Don't allow reuse of same element
                BacktrackCombinationSum2(candidates, remaining - candidates[i], i + 1, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }
        
        // ========================================
        // N-QUEENS PATTERN
        // ========================================
        
        /// <summary>
        /// Template for N-Queens problem
        /// Time: O(n!), Space: O(n²)
        /// </summary>
        public static IList<IList<string>> SolveNQueens(int n)
        {
            IList<IList<string>> result = new List<IList<string>>();
            char[,] board = new char[n, n];
            
            // Initialize board
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    board[i, j] = '.';
            
            BacktrackNQueens(board, 0, n, result);
            
            return result;
        }
        
        private static void BacktrackNQueens(char[,] board, int row, int n, IList<IList<string>> result)
        {
            if (row == n)
            {
                result.Add(ConvertBoardToList(board));
                return;
            }
            
            for (int col = 0; col < n; col++)
            {
                if (IsSafeQueen(board, row, col, n))
                {
                    board[row, col] = 'Q';
                    BacktrackNQueens(board, row + 1, n, result);
                    board[row, col] = '.';
                }
            }
        }
        
        private static bool IsSafeQueen(char[,] board, int row, int col, int n)
        {
            // Check column
            for (int i = 0; i < row; i++)
                if (board[i, col] == 'Q') return false;
            
            // Check upper-left diagonal
            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
                if (board[i, j] == 'Q') return false;
            
            // Check upper-right diagonal
            for (int i = row - 1, j = col + 1; i >= 0 && j < n; i--, j++)
                if (board[i, j] == 'Q') return false;
            
            return true;
        }
        
        private static IList<string> ConvertBoardToList(char[,] board)
        {
            List<string> result = new List<string>();
            int n = board.GetLength(0);
            
            for (int i = 0; i < n; i++)
            {
                string row = "";
                for (int j = 0; j < n; j++)
                    row += board[i, j];
                result.Add(row);
            }
            
            return result;
        }
        
        // ========================================
        // PALINDROME PARTITIONING
        // ========================================
        
        /// <summary>
        /// Template for palindrome partitioning
        /// Time: O(2^n * n), Space: O(2^n * n)
        /// </summary>
        public static IList<IList<string>> Partition(string s)
        {
            IList<IList<string>> result = new List<IList<string>>();
            List<string> current = new List<string>();
            
            BacktrackPartition(s, 0, current, result);
            
            return result;
        }
        
        private static void BacktrackPartition(string s, int start, List<string> current, IList<IList<string>> result)
        {
            if (start == s.Length)
            {
                result.Add(new List<string>(current));
                return;
            }
            
            for (int end = start + 1; end <= s.Length; end++)
            {
                string substring = s.Substring(start, end - start);
                if (IsPalindrome(substring))
                {
                    current.Add(substring);
                    BacktrackPartition(s, end, current, result);
                    current.RemoveAt(current.Count - 1);
                }
            }
        }
        
        private static bool IsPalindrome(string s)
        {
            int left = 0;
            int right = s.Length - 1;
            
            while (left < right)
            {
                if (s[left] != s[right])
                    return false;
                left++;
                right--;
            }
            
            return true;
        }
        
        // ========================================
        // LETTER COMBINATIONS OF PHONE NUMBER
        // ========================================
        
        /// <summary>
        /// Template for letter combinations of phone number
        /// Time: O(4^n * n), Space: O(4^n * n)
        /// </summary>
        public static IList<string> LetterCombinations(string digits)
        {
            if (string.IsNullOrEmpty(digits)) return new List<string>();
            
            Dictionary<char, string> phoneMap = new Dictionary<char, string>
            {
                ['2'] = "abc", ['3'] = "def", ['4'] = "ghi",
                ['5'] = "jkl", ['6'] = "mno", ['7'] = "pqrs",
                ['8'] = "tuv", ['9'] = "wxyz"
            };
            
            IList<string> result = new List<string>();
            string current = "";
            
            BacktrackLetterCombinations(digits, 0, phoneMap, current, result);
            
            return result;
        }
        
        private static void BacktrackLetterCombinations(string digits, int index, Dictionary<char, string> phoneMap, string current, IList<string> result)
        {
            if (index == digits.Length)
            {
                result.Add(current);
                return;
            }
            
            char digit = digits[index];
            string letters = phoneMap[digit];
            
            foreach (char letter in letters)
            {
                BacktrackLetterCombinations(digits, index + 1, phoneMap, current + letter, result);
            }
        }
        
        // ========================================
        // GENERIC BACKTRACKING TEMPLATE
        // ========================================
        
        /// <summary>
        /// Generic backtracking template
        /// </summary>
        public static void GenericBacktrack<T>(
            T[] elements,
            Func<List<T>, bool> isValid,
            Func<List<T>, bool> isComplete,
            Action<List<T>> processResult,
            List<T> current = null,
            int start = 0) where T : IComparable<T>
        {
            if (current == null)
                current = new List<T>();
            
            if (!isValid(current)) return;
            
            if (isComplete(current))
            {
                processResult(new List<T>(current));
                return;
            }
            
            for (int i = start; i < elements.Length; i++)
            {
                // Skip duplicates for sorted arrays
                if (i > start && elements[i].CompareTo(elements[i - 1]) == 0) continue;
                
                current.Add(elements[i]);
                GenericBacktrack(elements, isValid, isComplete, processResult, current, i + 1);
                current.RemoveAt(current.Count - 1);
            }
        }
        
        // ========================================
        // HELPER METHODS
        // ========================================
        
        public static void PrintCombination<T>(IEnumerable<T> combination, string message = "Combination")
        {
            Console.WriteLine($"{message}: [{string.Join(", ", combination)}]");
        }
        
        public static void PrintBoard(IList<string> board, string message = "Board")
        {
            Console.WriteLine($"{message}:");
            foreach (string row in board)
            {
                Console.WriteLine($"  {row}");
            }
        }
        
        public static void PrintPartitions(IList<IList<string>> partitions, string message = "Partitions")
        {
            Console.WriteLine($"{message}:")
            foreach (var partition in partitions)
            {
                Console.WriteLine($"  [{string.Join(", ", partition.Select(s => $"\"{s}\""))}]");
            }
        }
    }
}