using AlgorithmMaster.Utils;
using System;
using System.Collections.Generic;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for Breadth-First Search (BFS)
    /// Common patterns: Level order traversal, Shortest path, Connected components
    /// </summary>
    public static class BFSTemplate
    {
        // ========================================
        // TREE BFS PATTERNS
        // ========================================

        /// <summary>
        /// Template for level order traversal (tree)
        /// Time: O(n), Space: O(w) where w is maximum width
        /// </summary>
        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> result = new List<IList<int>>();
            if (root == null) return result;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                List<int> currentLevel = new List<int>();

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    currentLevel.Add(node.Val);

                    if (node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }

                result.Add(currentLevel);
            }

            return result;
        }

        /// <summary>
        /// Template for zigzag level order traversal
        /// Time: O(n), Space: O(w)
        /// </summary>
        public static IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            IList<IList<int>> result = new List<IList<int>>();
            if (root == null) return result;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            bool leftToRight = true;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                List<int> currentLevel = new List<int>();

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode node = queue.Dequeue();

                    if (leftToRight)
                    {
                        currentLevel.Add(node.Val);
                    }
                    else
                    {
                        currentLevel.Insert(0, node.Val);
                    }

                    if (node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }

                result.Add(currentLevel);
                leftToRight = !leftToRight;
            }

            return result;
        }

        /// <summary>
        /// Template for finding minimum depth of binary tree
        /// Time: O(n), Space: O(w)
        /// </summary>
        public static int MinDepth(TreeNode root)
        {
            if (root == null) return 0;

            Queue<(TreeNode node, int depth)> queue = new Queue<(TreeNode, int)>();
            queue.Enqueue((root, 1));

            while (queue.Count > 0)
            {
                var (node, depth) = queue.Dequeue();

                // Return depth when we find the first leaf node
                if (node.Left == null && node.Right == null)
                    return depth;

                if (node.Left != null) queue.Enqueue((node.Left, depth + 1));
                if (node.Right != null) queue.Enqueue((node.Right, depth + 1));
            }

            return 0;
        }

        // ========================================
        // GRAPH BFS PATTERNS
        // ========================================

        /// <summary>
        /// Template for basic graph BFS
        /// Time: O(V + E), Space: O(V)
        /// </summary>
        public static List<int> GraphBFS(Dictionary<int, List<int>> graph, int start)
        {
            List<int> result = new List<int>();
            Queue<int> queue = new Queue<int>();
            HashSet<int> visited = new HashSet<int>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                result.Add(node);

                if (graph.ContainsKey(node))
                {
                    foreach (int neighbor in graph[node])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Template for shortest path in unweighted graph
        /// Time: O(V + E), Space: O(V)
        /// </summary>
        public static int ShortestPath(Dictionary<int, List<int>> graph, int start, int target)
        {
            Queue<(int node, int distance)> queue = new Queue<(int, int)>();
            HashSet<int> visited = new HashSet<int>();

            queue.Enqueue((start, 0));
            visited.Add(start);

            while (queue.Count > 0)
            {
                var (node, distance) = queue.Dequeue();

                if (node == target)
                    return distance;

                if (graph.ContainsKey(node))
                {
                    foreach (int neighbor in graph[node])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue((neighbor, distance + 1));
                        }
                    }
                }
            }

            return -1; // Target not reachable
        }

        /// <summary>
        /// Template for finding all paths from source to target
        /// Time: O(V + E), Space: O(V + E)
        /// </summary>
        public static List<List<int>> AllPathsSourceTarget(int[][] graph)
        {
            List<List<int>> result = new List<List<int>>();
            List<int> path = new List<int> { 0 };

            Queue<List<int>> queue = new Queue<List<int>>();
            queue.Enqueue(path);

            while (queue.Count > 0)
            {
                List<int> currentPath = queue.Dequeue();
                int lastNode = currentPath[currentPath.Count - 1];

                if (lastNode == graph.Length - 1)
                {
                    result.Add(new List<int>(currentPath));
                    continue;
                }

                foreach (int neighbor in graph[lastNode])
                {
                    List<int> newPath = new List<int>(currentPath);
                    newPath.Add(neighbor);
                    queue.Enqueue(newPath);
                }
            }

            return result;
        }

        // ========================================
        // MATRIX BFS PATTERNS
        // ========================================

        /// <summary>
        /// Template for island counting (connected components)
        /// Time: O(m * n), Space: O(m * n)
        /// </summary>
        public static int NumIslands(char[,] grid)
        {
            if (grid == null || grid.GetLength(0) == 0) return 0;

            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int islandCount = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i, j] == '1')
                    {
                        BFSIsland(grid, i, j, rows, cols);
                        islandCount++;
                    }
                }
            }

            return islandCount;
        }

        private static void BFSIsland(char[,] grid, int row, int col, int rows, int cols)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((row, col));
            grid[row, col] = '0'; // Mark as visited

            int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

            while (queue.Count > 0)
            {
                var (r, c) = queue.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int newRow = r + directions[i, 0];
                    int newCol = c + directions[i, 1];

                    if (IsValidCell(grid, newRow, newCol, rows, cols) && grid[newRow, newCol] == '1')
                    {
                        queue.Enqueue((newRow, newCol));
                        grid[newRow, newCol] = '0';
                    }
                }
            }
        }

        /// <summary>
        /// Template for shortest path in grid with obstacles
        /// Time: O(m * n), Space: O(m * n)
        /// </summary>
        public static int ShortestPathGrid(int[,] grid, int[] start, int[] target)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            Queue<(int row, int col, int distance)> queue = new Queue<(int, int, int)>();
            bool[,] visited = new bool[rows, cols];

            queue.Enqueue((start[0], start[1], 0));
            visited[start[0], start[1]] = true;

            int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

            while (queue.Count > 0)
            {
                var (row, col, dist) = queue.Dequeue();

                if (row == target[0] && col == target[1])
                    return dist;

                for (int i = 0; i < 4; i++)
                {
                    int newRow = row + directions[i, 0];
                    int newCol = col + directions[i, 1];

                    if (IsValidPath(grid, newRow, newCol, rows, cols) && !visited[newRow, newCol])
                    {
                        visited[newRow, newCol] = true;
                        queue.Enqueue((newRow, newCol, dist + 1));
                    }
                }
            }

            return -1; // No path found
        }

        // ========================================
        // WORD LADDER PATTERN
        // ========================================

        /// <summary>
        /// Template for word ladder (shortest path in word graph)
        /// Time: O(M² * N) where M is word length, N is number of words
        /// Space: O(M * N)
        /// </summary>
        public static int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            HashSet<string> wordSet = new HashSet<string>(wordList);
            if (!wordSet.Contains(endWord)) return 0;

            Queue<(string word, int length)> queue = new Queue<(string, int)>();
            queue.Enqueue((beginWord, 1));
            wordSet.Remove(beginWord);

            while (queue.Count > 0)
            {
                var (currentWord, length) = queue.Dequeue();

                if (currentWord == endWord)
                    return length;

                // Generate all possible words by changing one character
                for (int i = 0; i < currentWord.Length; i++)
                {
                    char[] chars = currentWord.ToCharArray();

                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (c == currentWord[i]) continue;

                        chars[i] = c;
                        string newWord = new string(chars);

                        if (wordSet.Contains(newWord))
                        {
                            queue.Enqueue((newWord, length + 1));
                            wordSet.Remove(newWord);
                        }
                    }
                }
            }

            return 0;
        }

        // ========================================
        // TOPOLOGICAL SORT (KAHN'S ALGORITHM)
        // ========================================

        /// <summary>
        /// Template for topological sort using BFS (Kahn's algorithm)
        /// Time: O(V + E), Space: O(V + E)
        /// </summary>
        public static int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            // Build adjacency list and in-degree count
            List<List<int>> graph = new List<List<int>>();
            int[] inDegree = new int[numCourses];

            for (int i = 0; i < numCourses; i++)
            {
                graph.Add(new List<int>());
            }

            for (int i = 0; i < prerequisites.GetLength(0); i++)
            {
                int course = prerequisites[i, 0];
                int prereq = prerequisites[i, 1];
                graph[prereq].Add(course);
                inDegree[course]++;
            }

            // Queue for courses with no prerequisites
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < numCourses; i++)
            {
                if (inDegree[i] == 0)
                    queue.Enqueue(i);
            }

            List<int> result = new List<int>();

            while (queue.Count > 0)
            {
                int course = queue.Dequeue();
                result.Add(course);

                foreach (int nextCourse in graph[course])
                {
                    inDegree[nextCourse]--;
                    if (inDegree[nextCourse] == 0)
                    {
                        queue.Enqueue(nextCourse);
                    }
                }
            }

            return result.Count == numCourses ? result.ToArray() : new int[0];
        }

        // ========================================
        // HELPER METHODS
        // ========================================

        private static bool IsValidCell(char[,] grid, int row, int col, int rows, int cols)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }

        private static bool IsValidPath(int[,] grid, int row, int col, int rows, int cols)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols && grid[row, col] == 0;
        }

        public static void PrintBFSTraversal<T>(IEnumerable<T> traversal, string message = "BFS Traversal")
        {
            Console.WriteLine($"{message}: [{string.Join(", ", traversal)}]");
        }

        public static Dictionary<int, List<int>> CreateSampleGraph()
        {
            return new Dictionary<int, List<int>>
            {
                [0] = new List<int> { 1, 2 },
                [1] = new List<int> { 0, 3, 4 },
                [2] = new List<int> { 0, 5 },
                [3] = new List<int> { 1 },
                [4] = new List<int> { 1, 5 },
                [5] = new List<int> { 2, 4 }
            };
        }
    }
}