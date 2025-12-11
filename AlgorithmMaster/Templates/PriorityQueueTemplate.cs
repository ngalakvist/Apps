using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for Priority Queue/Heap problems
    /// Common patterns: Min/Max heap, Top K elements, Scheduling, Graph algorithms
    /// </summary>
    public static class PriorityQueueTemplate
    {
        // ========================================
        // BASIC HEAP OPERATIONS
        // ========================================
        
        /// <summary>
        /// Template for finding top K frequent elements
        /// Time: O(n log k), Space: O(k)
        /// </summary>
        public static int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> frequency = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                frequency[num] = frequency.GetValueOrDefault(num, 0) + 1;
            }
            
            // Min heap: keep k most frequent elements
            var heap = new PriorityQueue<int, int>();
            
            foreach (var kvp in frequency)
            {
                heap.Enqueue(kvp.Key, kvp.Value);
                
                if (heap.Count > k)
                {
                    heap.Dequeue(); // Remove least frequent
                }
            }
            
            int[] result = new int[k];
            for (int i = k - 1; i >= 0; i--)
            {
                result[i] = heap.Dequeue();
            }
            
            return result;
        }
        
        /// <summary>
        /// Template for finding K closest points to origin
        /// Time: O(n log k), Space: O(k)
        /// </summary>
        public static int[][] KClosest(int[][] points, int k)
        {
            // Max heap based on distance from origin
            var heap = new PriorityQueue<int[], double>();
            
            foreach (int[] point in points)
            {
                double distance = Math.Sqrt(point[0] * point[0] + point[1] * point[1]);
                heap.Enqueue(point, -distance); // Negative for max heap
                
                if (heap.Count > k)
                {
                    heap.Dequeue();
                }
            }
            
            int[][] result = new int[k][];
            for (int i = 0; i < k; i++)
            {
                result[i] = heap.Dequeue();
            }
            
            return result;
        }
        
        // ========================================
        // MERGE K SORTED LISTS
        // ========================================
        
        /// <summary>
        /// Template for merging K sorted lists
        /// Time: O(N log k), Space: O(k) where N is total elements
        /// </summary>
        public static ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0) return null;
            
            // Min heap based on node values
            var heap = new PriorityQueue<ListNode, int>();
            
            // Add first node of each list
            foreach (ListNode node in lists)
            {
                if (node != null)
                    heap.Enqueue(node, node.Val);
            }
            
            ListNode dummy = new ListNode(0);
            ListNode current = dummy;
            
            while (heap.Count > 0)
            {
                ListNode node = heap.Dequeue();
                current.Next = node;
                current = current.Next;
                
                if (node.Next != null)
                {
                    heap.Enqueue(node.Next, node.Next.Val);
                }
            }
            
            return dummy.Next;
        }
        
        // ========================================
        // SCHEDULING PROBLEMS
        // ========================================
        
        /// <summary>
        /// Template for meeting rooms II (minimum rooms needed)
        /// Time: O(n log n), Space: O(n)
        /// </summary>
        public static int MinMeetingRooms(int[][] intervals)
        {
            if (intervals.Length == 0) return 0;
            
            // Sort by start time
            Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
            
            // Min heap to track end times
            var heap = new PriorityQueue<int, int>();
            
            foreach (int[] interval in intervals)
            {
                int start = interval[0];
                int end = interval[1];
                
                // If room is free (earliest end time <= current start)
                if (heap.Count > 0 && heap.Peek() <= start)
                {
                    heap.Dequeue(); // Reuse room
                }
                
                heap.Enqueue(end, end);
            }
            
            return heap.Count; // Number of rooms needed
        }
        
        /// <summary>
        /// Template for task scheduler (with cooldown)
        /// Time: O(n log n), Space: O(n)
        /// </summary>
        public static int LeastInterval(char[] tasks, int n)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();
            foreach (char task in tasks)
            {
                frequency[task] = frequency.GetValueOrDefault(task, 0) + 1;
            }
            
            // Max heap based on frequency
            var heap = new PriorityQueue<char, int>();
            foreach (var kvp in frequency)
            {
                heap.Enqueue(kvp.Key, -kvp.Value);
            }
            
            int time = 0;
            Queue<(char task, int availableTime)> cooldown = new Queue<(char, int)>();
            
            while (heap.Count > 0 || cooldown.Count > 0)
            {
                time++;
                
                // Check if any task is available after cooldown
                if (cooldown.Count > 0 && cooldown.Peek().availableTime <= time)
                {
                    var (task, _) = cooldown.Dequeue();
                    heap.Enqueue(task, -frequency[task]);
                }
                
                if (heap.Count > 0)
                {
                    char task = heap.Dequeue();
                    frequency[task]--;
                    
                    if (frequency[task] > 0)
                    {
                        cooldown.Enqueue((task, time + n + 1));
                    }
                }
            }
            
            return time;
        }
        
        // ========================================
        // GRAPH ALGORITHMS
        // ========================================
        
        /// <summary>
        /// Template for Dijkstra's algorithm (shortest path)
        /// Time: O((V + E) log V), Space: O(V)
        /// </summary>
        public static int[] Dijkstra(Dictionary<int, List<(int node, int weight)>> graph, int start, int nodes)
        {
            int[] distances = new int[nodes];
            Array.Fill(distances, int.MaxValue);
            distances[start] = 0;
            
            // Min heap: (distance, node)
            var heap = new PriorityQueue<int, int>();
            heap.Enqueue(start, 0);
            
            while (heap.Count > 0)
            {
                int node = heap.Dequeue();
                
                if (graph.ContainsKey(node))
                {
                    foreach (var (neighbor, weight) in graph[node])
                    {
                        int newDistance = distances[node] + weight;
                        
                        if (newDistance < distances[neighbor])
                        {
                            distances[neighbor] = newDistance;
                            heap.Enqueue(neighbor, newDistance);
                        }
                    }
                }
            }
            
            return distances;
        }
        
        /// <summary>
        /// Template for Prim's algorithm (minimum spanning tree)
        /// Time: O((V + E) log V), Space: O(V)
        /// </summary>
        public static int PrimMST(Dictionary<int, List<(int node, int weight)>> graph, int start, int nodes)
        {
            bool[] inMST = new bool[nodes];
            int totalWeight = 0;
            
            // Min heap: (weight, node)
            var heap = new PriorityQueue<int, int>();
            heap.Enqueue(start, 0);
            
            while (heap.Count > 0)
            {
                int node = heap.Dequeue();
                
                if (inMST[node]) continue;
                
                inMST[node] = true;
                
                if (graph.ContainsKey(node))
                {
                    foreach (var (neighbor, weight) in graph[node])
                    {
                        if (!inMST[neighbor])
                        {
                            heap.Enqueue(neighbor, weight);
                            totalWeight += weight;
                        }
                    }
                }
            }
            
            return totalWeight;
        }
        
        // ========================================
        // MEDIAN FINDING
        // ========================================
        
        /// <summary>
        /// Template for finding median in data stream
        /// Time: O(log n) per operation, Space: O(n)
        /// </summary>
        public class MedianFinder
        {
            private PriorityQueue<int, int> maxHeap; // Left half (max heap)
            private PriorityQueue<int, int> minHeap; // Right half (min heap)
            
            public MedianFinder()
            {
                maxHeap = new PriorityQueue<int, int>(); // Invert values for max heap
                minHeap = new PriorityQueue<int, int>();
            }
            
            public void AddNum(int num)
            {
                // Add to max heap
                maxHeap.Enqueue(num, -num);
                
                // Balance heaps
                if (maxHeap.Count > minHeap.Count + 1)
                {
                    int max = maxHeap.Dequeue();
                    minHeap.Enqueue(max, max);
                }
                
                // Ensure maxHeap values <= minHeap values
                if (minHeap.Count > 0 && maxHeap.Peek() > minHeap.Peek())
                {
                    int maxVal = maxHeap.Dequeue();
                    int minVal = minHeap.Dequeue();
                    maxHeap.Enqueue(minVal, -minVal);
                    minHeap.Enqueue(maxVal, maxVal);
                }
            }
            
            public double FindMedian()
            {
                if (maxHeap.Count == minHeap.Count)
                {
                    return (maxHeap.Peek() + minHeap.Peek()) / 2.0;
                }
                else
                {
                    return maxHeap.Peek();
                }
            }
        }
        
        // ========================================
        // TOP K ELEMENTS PATTERNS
        // ========================================
        
        /// <summary>
        /// Template for top K frequent words
        /// Time: O(n log k), Space: O(k)
        /// </summary>
        public static IList<string> TopKFrequentWords(string[] words, int k)
        {
            Dictionary<string, int> frequency = new Dictionary<string, int>();
            foreach (string word in words)
            {
                frequency[word] = frequency.GetValueOrDefault(word, 0) + 1;
            }
            
            // Min heap: (frequency, word)
            var heap = new PriorityQueue<string, (int freq, string word)>();
            
            foreach (var kvp in frequency)
            {
                heap.Enqueue(kvp.Key, (-kvp.Value, kvp.Key));
                
                if (heap.Count > k)
                {
                    heap.Dequeue();
                }
            }
            
            List<string> result = new List<string>();
            while (heap.Count > 0)
            {
                result.Insert(0, heap.Dequeue());
            }
            
            return result;
        }
        
        /// <summary>
        /// Template for Kth largest element
        /// Time: O(n log k), Space: O(k)
        /// </summary>
        public static int FindKthLargest(int[] nums, int k)
        {
            // Min heap
            var heap = new PriorityQueue<int, int>();
            
            foreach (int num in nums)
            {
                heap.Enqueue(num, num);
                
                if (heap.Count > k)
                {
                    heap.Dequeue();
                }
            }
            
            return heap.Dequeue();
        }
        
        // ========================================
        // CUSTOM HEAP IMPLEMENTATION
        // ========================================
        
        /// <summary>
        /// Custom heap implementation for learning purposes
        /// </summary>
        public class CustomHeap<T> where T : IComparable<T>
        {
            private List<T> heap;
            private bool isMinHeap;
            
            public CustomHeap(bool minHeap = true)
            {
                heap = new List<T>();
                isMinHeap = minHeap;
            }
            
            public int Count => heap.Count;
            
            public void Push(T value)
            {
                heap.Add(value);
                HeapifyUp(heap.Count - 1);
            }
            
            public T Pop()
            {
                if (heap.Count == 0)
                    throw new InvalidOperationException("Heap is empty");
                
                T result = heap[0];
                T last = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);
                
                if (heap.Count > 0)
                {
                    heap[0] = last;
                    HeapifyDown(0);
                }
                
                return result;
            }
            
            public T Peek()
            {
                if (heap.Count == 0)
                    throw new InvalidOperationException("Heap is empty");
                
                return heap[0];
            }
            
            private void HeapifyUp(int index)
            {
                while (index > 0)
                {
                    int parent = (index - 1) / 2;
                    
                    if (Compare(heap[index], heap[parent]) >= 0)
                        break;
                    
                    Swap(index, parent);
                    index = parent;
                }
            }
            
            private void HeapifyDown(int index)
            {
                while (index < heap.Count)
                {
                    int left = 2 * index + 1;
                    int right = 2 * index + 2;
                    int smallest = index;
                    
                    if (left < heap.Count && Compare(heap[left], heap[smallest]) < 0)
                        smallest = left;
                    
                    if (right < heap.Count && Compare(heap[right], heap[smallest]) < 0)
                        smallest = right;
                    
                    if (smallest == index)
                        break;
                    
                    Swap(index, smallest);
                    index = smallest;
                }
            }
            
            private int Compare(T a, T b)
            {
                int result = a.CompareTo(b);
                return isMinHeap ? result : -result;
            }
            
            private void Swap(int i, int j)
            {
                T temp = heap[i];
                heap[i] = heap[j];
                heap[j] = temp;
            }
        }
        
        // ========================================
        // HELPER METHODS
        // ========================================
        
        public static void PrintHeapContents<T>(IEnumerable<T> heap, string message = "Heap Contents")
        {
            Console.WriteLine($"{message}: [{string.Join(", ", heap)}]");
        }
        
        public static void PrintTopKElements<T>(int k, string type, IEnumerable<T> elements)
        {
            Console.WriteLine($"Top {k} {type}: [{string.Join(", ", elements)}]");
        }
    }
}