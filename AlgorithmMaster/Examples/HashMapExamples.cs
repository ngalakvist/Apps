using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmMaster.Templates;
using AlgorithmMaster.Utils;

namespace AlgorithmMaster.Examples
{
    public static class HashMapExamples
    {
        public static void RunAll()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        HASHMAP ALGORITHM EXAMPLES");
            Console.WriteLine("========================================");
            Console.WriteLine();
            
            Example1_FrequencyCounting();
            Example2_TwoSumPattern();
            Example3_Grouping();
            Example4_SubarrayPatterns();
            Example5_AdvancedStructures();
        }
        
        private static void Example1_FrequencyCounting()
        {
            Console.WriteLine("EXAMPLE 1: Frequency Counting");
            Console.WriteLine("-----------------------------");
            
            // Character frequency
            string text = "hello world";
            var charFreq = HashMapTemplate.CountCharacterFrequency(text);
            HashMapTemplate.PrintFrequencyTable(charFreq, "Character Frequency");
            
            // Element frequency
            int[] numbers = { 1, 2, 3, 2, 1, 2, 4, 5, 1 };
            var numFreq = HashMapTemplate.CountFrequency(numbers);
            HashMapTemplate.PrintFrequencyTable(numFreq, "Number Frequency");
            
            // Most frequent element
            int mostFrequent = HashMapTemplate.MostFrequent(numbers);
            Console.WriteLine($"Most frequent number: {mostFrequent}");
            
            Console.WriteLine();
        }
        
        private static void Example2_TwoSumPattern()
        {
            Console.WriteLine("EXAMPLE 2: Two Sum Pattern");
            Console.WriteLine("--------------------------");
            
            // Two sum
            int[] nums = { 2, 7, 11, 15 };
            int target = 9;
            int[] indices = HashMapTemplate.TwoSum(nums, target);
            Console.WriteLine($"Two sum for {target}: indices [{string.Join(", ", indices)}]");
            
            // Three sum
            int[] threeSumArray = { -1, 0, 1, 2, -1, -4 };
            var triplets = HashMapTemplate.ThreeSum(threeSumArray);
            Console.WriteLine("Three sum results:");
            foreach (var triplet in triplets)
            {
                Console.WriteLine($"  [{string.Join(", ", triplet)}]");
            }
            
            Console.WriteLine();
        }
        
        private static void Example3_Grouping()
        {
            Console.WriteLine("EXAMPLE 3: Grouping Patterns");
            Console.WriteLine("----------------------------");
            
            // Group anagrams
            string[] words = { "eat", "tea", "tan", "ate", "nat", "bat" };
            var anagramGroups = HashMapTemplate.GroupAnagrams(words);
            Console.WriteLine("Anagram groups:");
            foreach (var group in anagramGroups)
            {
                Console.WriteLine($"  [{string.Join(", ", group.Select(w => $"\"{w}\""))}]");
            }
            
            // Custom grouping
            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var parityGroups = HashMapTemplate.GroupBy(numbers, x => x % 2 == 0 ? "Even" : "Odd");
            Console.WriteLine("\nParity groups:");
            foreach (var group in parityGroups)
            {
                Console.WriteLine($"  {group.Key}: [{string.Join(", ", group.Value)}]");
            }
            
            Console.WriteLine();
        }
        
        private static void Example4_SubarrayPatterns()
        {
            Console.WriteLine("EXAMPLE 4: Subarray Patterns");
            Console.WriteLine("----------------------------");
            
            // Subarray sum equals k
            int[] nums = { 1, 2, 3, 4, 5, 6 };
            int k = 9;
            int count = HashMapTemplate.SubarraySum(nums, k);
            Console.WriteLine($"Number of subarrays with sum {k}: {count}");
            
            // Longest substring with K distinct characters
            string s = "eceba";
            int distinctK = 2;
            int length = HashMapTemplate.LengthOfLongestSubstringKDistinct(s, distinctK);
            Console.WriteLine($"Longest substring with {distinctK} distinct chars: length {length}");
            
            Console.WriteLine();
        }
        
        private static void Example5_AdvancedStructures()
        {
            Console.WriteLine("EXAMPLE 5: Advanced Data Structures");
            Console.WriteLine("-----------------------------------");
            
            // LRU Cache demonstration
            var lruCache = new HashMapTemplate.LRUCache(3);
            lruCache.Put(1, 1);
            lruCache.Put(2, 2);
            lruCache.Put(3, 3);
            Console.WriteLine("LRU Cache after inserting 1,2,3:");
            Console.WriteLine($"  Get(1): {lruCache.Get(1)}");
            Console.WriteLine($"  Get(2): {lruCache.Get(2)}");
            
            lruCache.Put(4, 4); // Should evict key 3
            Console.WriteLine("After inserting 4 (capacity exceeded):");
            Console.WriteLine($"  Get(3): {lruCache.Get(3)} (should be -1 - evicted)");
            Console.WriteLine($"  Get(4): {lruCache.Get(4)}");
            
            // Union-Find demonstration
            var uf = new HashMapTemplate.UnionFind(5);
            Console.WriteLine("\nUnion-Find operations:");
            Console.WriteLine($"  Initial components: {uf.Count()}");
            
            uf.Union(0, 1);
            uf.Union(2, 3);
            Console.WriteLine($"  After union(0,1) and union(2,3): {uf.Count()} components");
            Console.WriteLine($"  Are 0 and 1 connected? {uf.Connected(0, 1)}");
            Console.WriteLine($"  Are 1 and 2 connected? {uf.Connected(1, 2)}");
            
            uf.Union(0, 2);
            Console.WriteLine($"  After union(0,2): {uf.Count()} components");
            Console.WriteLine($"  Are 1 and 3 connected? {uf.Connected(1, 3)}");
            
            Console.WriteLine();
        }
        
        // Test method for unit testing
        public static bool TestHashMapOperations()
        {
            try
            {
                // Test two sum
                int[] nums = { 2, 7, 11, 15 };
                int[] result = HashMapTemplate.TwoSum(nums, 9);
                if (!result.SequenceEqual(new int[] { 0, 1 })) return false;
                
                // Test frequency counting
                var freq = HashMapTemplate.CountCharacterFrequency("hello");
                if (freq['l'] != 2) return false;
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}