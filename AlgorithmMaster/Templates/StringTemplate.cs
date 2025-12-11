using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for String-based problems
    /// Common patterns: Two Pointers, Sliding Window, Hash Map, Dynamic Programming
    /// </summary>
    public static class StringTemplate
    {
        // ========================================
        // BASIC STRING OPERATIONS
        // ========================================
        
        /// <summary>
        /// Template for string reversal
        /// Time: O(n), Space: O(n) or O(1) if in-place
        /// </summary>
        public static string ReverseString(string s)
        {
            char[] chars = s.ToCharArray();
            int left = 0;
            int right = chars.Length - 1;
            
            while (left < right)
            {
                char temp = chars[left];
                chars[left] = chars[right];
                chars[right] = temp;
                left++;
                right--;
            }
            
            return new string(chars);
        }
        
        /// <summary>
        /// Template for checking palindrome
        /// Time: O(n), Space: O(1)
        /// </summary>
        public static bool IsPalindrome(string s)
        {
            int left = 0;
            int right = s.Length - 1;
            
            while (left < right)
            {
                while (left < right && !char.IsLetterOrDigit(s[left])) left++;
                while (left < right && !char.IsLetterOrDigit(s[right])) right--;
                
                if (char.ToLower(s[left]) != char.ToLower(s[right]))
                    return false;
                    
                left++;
                right--;
            }
            
            return true;
        }
        
        // ========================================
        // SLIDING WINDOW PATTERN
        // ========================================
        
        /// <summary>
        /// Template for longest substring without repeating characters
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
                
                if (charIndex.ContainsKey(c))
                {
                    left = Math.Max(left, charIndex[c] + 1);
                }
                
                charIndex[c] = right;
                maxLength = Math.Max(maxLength, right - left + 1);
            }
            
            return maxLength;
        }
        
        /// <summary>
        /// Template for minimum window substring
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
        
        // ========================================
        // TWO POINTERS PATTERN
        // ========================================
        
        /// <summary>
        /// Template for valid anagram check
        /// Time: O(n), Space: O(1) or O(k) where k is charset size
        /// </summary>
        public static bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;
            
            int[] count = new int[26];
            
            for (int i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a']++;
                count[t[i] - 'a']--;
            }
            
            return count.All(c => c == 0);
        }
        
        /// <summary>
        /// Template for string permutation check
        /// Time: O(n), Space: O(k) where k is charset size
        /// </summary>
        public static bool CheckInclusion(string s1, string s2)
        {
            if (s1.Length > s2.Length) return false;
            
            int[] count1 = new int[26];
            int[] count2 = new int[26];
            
            for (int i = 0; i < s1.Length; i++)
            {
                count1[s1[i] - 'a']++;
                count2[s2[i] - 'a']++;
            }
            
            if (count1.SequenceEqual(count2)) return true;
            
            for (int i = s1.Length; i < s2.Length; i++)
            {
                count2[s2[i] - 'a']++;
                count2[s2[i - s1.Length] - 'a']--;
                
                if (count1.SequenceEqual(count2)) return true;
            }
            
            return false;
        }
        
        // ========================================
        // REGULAR EXPRESSION PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for regex pattern matching
        /// Time: O(n * m) where n is string length, m is pattern complexity
        /// Space: O(n * m) for DP table
        /// </summary>
        public static bool IsMatch(string s, string p)
        {
            int m = s.Length;
            int n = p.Length;
            bool[,] dp = new bool[m + 1, n + 1];
            
            dp[0, 0] = true;
            
            for (int j = 1; j <= n; j++)
            {
                if (p[j - 1] == '*')
                    dp[0, j] = dp[0, j - 2];
            }
            
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (p[j - 1] == s[i - 1] || p[j - 1] == '.')
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else if (p[j - 1] == '*')
                    {
                        if (p[j - 2] == s[i - 1] || p[j - 2] == '.')
                        {
                            dp[i, j] = dp[i, j - 2] || dp[i - 1, j];
                        }
                        else
                        {
                            dp[i, j] = dp[i, j - 2];
                        }
                    }
                }
            }
            
            return dp[m, n];
        }
        
        // ========================================
        // STRING BUILDER PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for string compression
        /// Time: O(n), Space: O(n)
        /// </summary>
        public static string CompressString(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            
            StringBuilder compressed = new StringBuilder();
            int count = 1;
            
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == s[i - 1])
                {
                    count++;
                }
                else
                {
                    compressed.Append(s[i - 1]);
                    compressed.Append(count);
                    count = 1;
                }
            }
            
            compressed.Append(s[s.Length - 1]);
            compressed.Append(count);
            
            string result = compressed.ToString();
            return result.Length < s.Length ? result : s;
        }
        
        /// <summary>
        /// Template for string multiplication (like "123" * "456")
        /// Time: O(n * m), Space: O(n + m)
        /// </summary>
        public static string MultiplyStrings(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0") return "0";
            
            int m = num1.Length;
            int n = num2.Length;
            int[] result = new int[m + n];
            
            for (int i = m - 1; i >= 0; i--)
            {
                for (int j = n - 1; j >= 0; j--)
                {
                    int product = (num1[i] - '0') * (num2[j] - '0');
                    int sum = product + result[i + j + 1];
                    
                    result[i + j + 1] = sum % 10;
                    result[i + j] += sum / 10;
                }
            }
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                if (i == 0 && result[i] == 0) continue;
                sb.Append(result[i]);
            }
            
            return sb.ToString();
        }
        
        // ========================================
        // HELPER METHODS
        // ========================================
        
        public static void PrintStringInfo(string s)
        {
            Console.WriteLine($"String: '{s}'");
            Console.WriteLine($"  Length: {s.Length}");
            Console.WriteLine($"  IsNullOrEmpty: {string.IsNullOrEmpty(s)}");
            Console.WriteLine($"  IsNullOrWhiteSpace: {string.IsNullOrWhiteSpace(s)}");
        }
        
        public static void PrintCharFrequency(string s)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach (char c in s)
            {
                freq[c] = freq.GetValueOrDefault(c, 0) + 1;
            }
            
            Console.WriteLine($"Character frequency for '{s}':");
            foreach (var kvp in freq.OrderBy(x => x.Key))
            {
                Console.WriteLine($"  '{kvp.Key}': {kvp.Value}");
            }
        }
    }
}