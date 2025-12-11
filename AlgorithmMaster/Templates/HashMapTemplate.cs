using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for HashMap/Dictionary-based problems
    /// Common patterns: Frequency counting, Two Sum, Grouping, Caching
    /// </summary>
    public static class HashMapTemplate
    {
        // ========================================
        // FREQUENCY COUNTING PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for character frequency counting
        /// Time: O(n), Space: O(k) where k is charset size
        /// </summary>
        public static Dictionary<char, int> CountCharacterFrequency(string s)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach (char c in s)
            {
                freq[c] = freq.GetValueOrDefault(c, 0) + 1;
            }
            return freq;
        }
        
        /// <summary>
        /// Template for element frequency counting
        /// Time: O(n), Space: O(n)
        /// </summary>
        public static Dictionary<T, int> CountFrequency<T>(IEnumerable<T> elements) where T : notnull
        {
            Dictionary<T, int> freq = new Dictionary<T, int>();
            foreach (T element in elements)
            {
                freq[element] = freq.GetValueOrDefault(element, 0) + 1;
            }
            return freq;
        }
        
        /// <summary>
        /// Template for finding most frequent element
        /// Time: O(n), Space: O(n)
        /// </summary>
        public static T MostFrequent<T>(IEnumerable<T> elements) where T : notnull
        {
            Dictionary<T, int> freq = CountFrequency(elements);
            return freq.OrderByDescending(kvp => kvp.Value).First().Key;
        }
        
        // ========================================
        // TWO SUM PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for Two Sum problem
        /// Time: O(n), Space: O(n)
        /// </summary>
        public static int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> seen = new Dictionary<int, int>();
            
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                
                if (seen.ContainsKey(complement))
                {
                    return new int[] { seen[complement], i };
                }
                
                seen[nums[i]] = i;
            }
            
            return new int[0]; // Not found
        }
        
        /// <summary>
        /// Template for Three Sum problem (using hash map)
        /// Time: O(n²), Space: O(n)
        /// </summary>
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            Array.Sort(nums);
            
            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                
                HashSet<int> seen = new HashSet<int>();
                for (int j = i + 1; j < nums.Length; j++)
                {
                    int complement = -nums[i] - nums[j];
                    
                    if (seen.Contains(complement))
                    {
                        result.Add(new List<int> { nums[i], complement, nums[j] });
                        
                        while (j + 1 < nums.Length && nums[j] == nums[j + 1]) j++;
                    }
                    
                    seen.Add(nums[j]);
                }
            }
            
            return result;
        }
        
        // ========================================
        // GROUPING PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for grouping anagrams
        /// Time: O(n * k log k) where n is number of strings, k is max length
        /// Space: O(n * k)
        /// </summary>
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, IList<string>> groups = new Dictionary<string, IList<string>>();
            
            foreach (string str in strs)
            {
                char[] chars = str.ToCharArray();
                Array.Sort(chars);
                string key = new string(chars);
                
                if (!groups.ContainsKey(key))
                    groups[key] = new List<string>();
                    
                groups[key].Add(str);
            }
            
            return new List<IList<string>>(groups.Values);
        }
        
        /// <summary>
        /// Template for grouping by custom key
        /// Time: O(n), Space: O(n)
        /// </summary>
        public static Dictionary<TKey, List<TElement>> GroupBy<TElement, TKey>(
            IEnumerable<TElement> elements, 
            Func<TElement, TKey> keySelector) where TKey : notnull
        {
            Dictionary<TKey, List<TElement>> groups = new Dictionary<TKey, List<TElement>>();
            
            foreach (TElement element in elements)
            {
                TKey key = keySelector(element);
                
                if (!groups.ContainsKey(key))
                    groups[key] = new List<TElement>();
                    
                groups[key].Add(element);
            }
            
            return groups;
        }
        
        // ========================================
        // SUBARRAY/SUBSTRING PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for finding subarray sum equals k
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
        /// Template for finding longest substring with k distinct characters
        /// Time: O(n), Space: O(k)
        /// </summary>
        public static int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            if (k == 0 || s.Length == 0) return 0;
            
            Dictionary<char, int> charIndex = new Dictionary<char, int>();
            int left = 0;
            int maxLength = 0;
            
            for (int right = 0; right < s.Length; right++)
            {
                char c = s[right];
                charIndex[c] = right;
                
                if (charIndex.Count > k)
                {
                    int leftMost = charIndex.Values.Min();
                    charIndex.Remove(s[leftMost]);
                    left = leftMost + 1;
                }
                
                maxLength = Math.Max(maxLength, right - left + 1);
            }
            
            return maxLength;
        }
        
        // ========================================
        // CACHING/MEMOIZATION PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for LRU Cache implementation
        /// Time: O(1) for get and put operations
        /// Space: O(capacity)
        /// </summary>
        public class LRUCache
        {
            private class ListNode
            {
                public int Key { get; set; }
                public int Value { get; set; }
                public ListNode? Prev { get; set; }
                public ListNode? Next { get; set; }
            }
            
            private readonly int _capacity;
            private readonly Dictionary<int, ListNode> _cache;
            private ListNode _head;
            private ListNode _tail;
            
            public LRUCache(int capacity)
            {
                _capacity = capacity;
                _cache = new Dictionary<int, ListNode>();
                _head = new ListNode();
                _tail = new ListNode();
                _head.Next = _tail;
                _tail.Prev = _head;
            }
            
            public int Get(int key)
            {
                if (!_cache.ContainsKey(key))
                    return -1;
                    
                ListNode node = _cache[key];
                MoveToHead(node);
                return node.Value;
            }
            
            public void Put(int key, int value)
            {
                if (_cache.ContainsKey(key))
                {
                    ListNode node = _cache[key];
                    node.Value = value;
                    MoveToHead(node);
                }
                else
                {
                    ListNode newNode = new ListNode { Key = key, Value = value };
                    _cache[key] = newNode;
                    AddToHead(newNode);
                    
                    if (_cache.Count > _capacity)
                    {
                        ListNode tail = RemoveTail();
                        _cache.Remove(tail.Key);
                    }
                }
            }
            
            private void MoveToHead(ListNode node)
            {
                RemoveNode(node);
                AddToHead(node);
            }
            
            private void RemoveNode(ListNode node)
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }
            
            private void AddToHead(ListNode node)
            {
                node.Prev = _head;
                node.Next = _head.Next;
                _head.Next.Prev = node;
                _head.Next = node;
            }
            
            private ListNode RemoveTail()
            {
                ListNode res = _tail.Prev;
                RemoveNode(res);
                return res;
            }
        }
        
        // ========================================
        // UNION-FIND PATTERN
        // ========================================
        
        /// <summary>
        /// Template for Union-Find (Disjoint Set Union)
        /// Time: O(α(n)) for operations with path compression
        /// Space: O(n)
        /// </summary>
        public class UnionFind
        {
            private int[] _parent;
            private int[] _rank;
            private int _count;
            
            public UnionFind(int size)
            {
                _parent = new int[size];
                _rank = new int[size];
                _count = size;
                
                for (int i = 0; i < size; i++)
                {
                    _parent[i] = i;
                    _rank[i] = 1;
                }
            }
            
            public int Find(int x)
            {
                if (_parent[x] != x)
                {
                    _parent[x] = Find(_parent[x]); // Path compression
                }
                return _parent[x];
            }
            
            public bool Union(int x, int y)
            {
                int rootX = Find(x);
                int rootY = Find(y);
                
                if (rootX == rootY) return false;
                
                // Union by rank
                if (_rank[rootX] > _rank[rootY])
                {
                    _parent[rootY] = rootX;
                }
                else if (_rank[rootX] < _rank[rootY])
                {
                    _parent[rootX] = rootY;
                }
                else
                {
                    _parent[rootY] = rootX;
                    _rank[rootX]++;
                }
                
                _count--;
                return true;
            }
            
            public bool Connected(int x, int y)
            {
                return Find(x) == Find(y);
            }
            
            public int Count()
            {
                return _count;
            }
        }
        
        // ========================================
        // HELPER METHODS
        // ========================================
        
        public static void PrintHashMap<TKey, TValue>(Dictionary<TKey, TValue> dict, string name = "Dictionary") where TKey : notnull
        {
            Console.WriteLine($"{name} (Count: {dict.Count}):");
            foreach (var kvp in dict)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
        }
        
        public static void PrintFrequencyTable<T>(Dictionary<T, int> freq, string name = "Frequency") where T : notnull
        {
            Console.WriteLine($"{name} Table:");
            foreach (var kvp in freq.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value} times");
            }
        }
    }
}