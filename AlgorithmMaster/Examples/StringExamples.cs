using AlgorithmMaster.Templates;
using System;

namespace AlgorithmMaster.Examples
{
    public static class StringExamples
    {
        public static void RunAll()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        STRING ALGORITHM EXAMPLES");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Example1_BasicOperations();
            Example2_SlidingWindow();
            Example3_TwoPointers();
            Example4_RegexPatterns();
            Example5_AdvancedOperations();
        }

        private static void Example1_BasicOperations()
        {
            Console.WriteLine("EXAMPLE 1: Basic String Operations");
            Console.WriteLine("----------------------------------");

            string text = "Hello World";
            StringTemplate.PrintStringInfo(text);

            // String reversal
            string reversed = StringTemplate.ReverseString(text);
            Console.WriteLine($"Reversed: \"{reversed}\"");

            // Palindrome check
            string[] testStrings = { "racecar", "hello", "A man a plan a canal Panama" };
            foreach (string s in testStrings)
            {
                bool isPalindrome = StringTemplate.IsPalindrome(s);
                Console.WriteLine($"\"{s}\" is palindrome: {isPalindrome}");
            }

            Console.WriteLine();
        }

        private static void Example2_SlidingWindow()
        {
            Console.WriteLine("EXAMPLE 2: Sliding Window Pattern");
            Console.WriteLine("---------------------------------");

            // Longest substring without repeating characters
            string s1 = "abcabcbb";
            int length1 = StringTemplate.LengthOfLongestSubstring(s1);
            Console.WriteLine($"Longest substring without repeating in \"{s1}\": {length1}");

            string s2 = "bbbbb";
            int length2 = StringTemplate.LengthOfLongestSubstring(s2);
            Console.WriteLine($"Longest substring without repeating in \"{s2}\": {length2}");

            // Minimum window substring
            string s = "ADOBECODEBANC";
            string t = "ABC";
            string minWindow = StringTemplate.MinWindow(s, t);
            Console.WriteLine($"Minimum window in \"{s}\" containing \"{t}\": \"{minWindow}\"");

            Console.WriteLine();
        }

        private static void Example3_TwoPointers()
        {
            Console.WriteLine("EXAMPLE 3: Two Pointers Pattern");
            Console.WriteLine("-------------------------------");

            // Anagram check
            string[] testPairs = {
                "listen", "silent",
                "hello", "world",
                "a gentleman", "elegant man"
            };

            for (int i = 0; i < testPairs.Length; i += 2)
            {
                bool isAnagram = StringTemplate.IsAnagram(testPairs[i], testPairs[i + 1]);
                Console.WriteLine($"\"{testPairs[i]}\" and \"{testPairs[i + 1]}\" are anagrams: {isAnagram}");
            }

            // String permutation
            string s1 = "ab";
            string s2 = "eidbaooo";
            bool hasPermutation = StringTemplate.CheckInclusion(s1, s2);
            Console.WriteLine($"\"{s1}\" is permutation in \"{s2}\": {hasPermutation}");

            Console.WriteLine();
        }

        private static void Example4_RegexPatterns()
        {
            Console.WriteLine("EXAMPLE 4: Regular Expression Patterns");
            Console.WriteLine("--------------------------------------");

            // Pattern matching demonstration
            string[] patterns = {
                "a", "a*", "a*b", ".*", "c*a*b"
            };

            string input = "aab";

            foreach (string pattern in patterns)
            {
                bool matches = StringTemplate.IsMatch(input, pattern);
                Console.WriteLine($"\"{input}\" matches pattern \"{pattern}\": {matches}\");
            }

            Console.WriteLine();
        }

        private static void Example5_AdvancedOperations()
        {
            Console.WriteLine("EXAMPLE 5: Advanced String Operations");
            Console.WriteLine("-------------------------------------");

            // String compression
            string compressed = "aaabbbcccddd";
            string compressedResult = StringTemplate.CompressString(compressed);
            Console.WriteLine($\"Compression of \"{compressed}\": \"{compressedResult}\"\");

            // String multiplication (large number multiplication)
            string num1 = "123";
            string num2 = "456";
            string product = StringTemplate.MultiplyStrings(num1, num2);
            Console.WriteLine($\"\"{num1}\" × \"{num2}\" = \"{product}\"\");

            // Character frequency analysis
            string analysisText = "algorithm patterns";
            StringTemplate.PrintCharFrequency(analysisText);

            Console.WriteLine();
        }

        // Test method for unit testing
        public static bool TestStringOperations()
        {
            try
            {
                // Test palindrome
                if (!StringTemplate.IsPalindrome("racecar")) return false;
                if (!StringTemplate.IsPalindrome("A man a plan a canal Panama")) return false;

                // Test anagram
                if (!StringTemplate.IsAnagram("listen", "silent")) return false;
                if (StringTemplate.IsAnagram("hello", "world")) return false;

                // Test longest substring
                if (StringTemplate.LengthOfLongestSubstring("abcabcbb") != 3) return false;
                if (StringTemplate.LengthOfLongestSubstring("bbbbb") != 1) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}