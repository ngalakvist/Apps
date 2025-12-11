using AlgorithmMaster.Utils;
using System;
using System.Collections.Generic;

namespace AlgorithmMaster.Templates
{
    /// <summary>
    /// Template for Depth-First Search (DFS)
    /// Common patterns: Tree traversal, Graph traversal, Path finding, Connected components
    /// </summary>
    public static class DFSTemplate
    {
        // ========================================
        // TREE DFS PATTERNS
        // ========================================

        /// <summary>
        /// Template for pre-order traversal (Root -> Left -> Right)
        /// Time: O(n), Space: O(h) where h is height of tree
        /// </summary>
        public static IList<int> PreorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();
            PreorderDFS(root, result);
            return result;
        }

        private static void PreorderDFS(TreeNode node, List<int> result)
        {
            if (node == null) return;

            result.Add(node.Val);        // Visit root
            PreorderDFS(node.Left, result);  // Visit left
            PreorderDFS(node.Right, result); // Visit right
        }

        /// <summary>
        /// Template for in-order traversal (Left -> Root -> Right)
        /// Time: O(n), Space: O(h)
        /// </summary>
        public static IList<int> InorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();
            InorderDFS(root, result);
            return result;
        }

        private static void InorderDFS(TreeNode node, List<int> result)
        {
            if (node == null) return;

            InorderDFS(node.Left, result);   // Visit left
            result.Add(node.Val);          // Visit root
            InorderDFS(node.Right, result);  // Visit right
        }

        /// <summary>
        /// Template for post-order traversal (Left -> Right -> Root)
        /// Time: O(n), Space: O(h)
        /// </summary>
        public static IList<int> PostorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();
            PostorderDFS(root, result);
            return result;
        }

        private static void PostorderDFS(TreeNode node, List<int> result)
        {
            if (node == null) return;

            PostorderDFS(node.Left, result);   // Visit left
            PostorderDFS(node.Right, result);  // Visit right
            result.Add(node.Val);            // Visit root
        }

        /// <summary>
        /// Template for maximum depth of binary tree
        /// Time: O(n), Space: O(h)
        /// </summary>
        public static int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;

            return 1 + Math.Max(MaxDepth(root.Left), MaxDepth(root.Right));
        }

        /// <summary>
        /// Template for path sum (any path)
        /// Time: O(n), Space: O(h)
        /// </summary>
        public static bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null) return false;

            // Check if it's a leaf node and path sum equals target
            if (root.Left == null && root.Right == null)
                return root.Val == targetSum;

            int remainingSum = targetSum - root.Val;
            return HasPathSum(root.Left, remainingSum) || HasPathSum(root.Right, remainingSum);
        }

        /// <summary>
        /// Template for finding all root-to-leaf paths
        /// Time: O(n * h), Space: O(n * h)
        /// </summary>
        public static IList<string> BinaryTreePaths(TreeNode root)
        {
            List<string> result = new List<string>();
            if (root != null)
                FindPaths(root, root.Val.ToString(), result);
            return result;
        }

        private static void FindPaths(TreeNode node, string path, List<string> result)
        {
            if (node.Left == null && node.Right == null)
            {
                result.Add(path);
                return;
            }

            if (node.Left != null)
                FindPaths(node.Left, path + "->" + node.Left.Val, result);

            if (node.Right != null)
                FindPaths(node.Right, path + "->" + node.Right.Val, result);
        }

        // ========================================
        // GRAPH DFS PATTERNS
        // ========================================

        /// <summary>
        /// Template for basic graph DFS (recursive)
        /// Time: O(V + E), Space: O(V)
        /// </summary>
        public static List<int> GraphDFSRecursive(Dictionary<int, List<int>> graph, int start)
        {
            List<int> result = new List<int>();
            HashSet<int> visited = new HashSet<int>();
            DFSRecursive(graph, start, visited, result);
            return result;
        }

        private static void DFSRecursive(Dictionary<int, List<int>> graph, int node, HashSet<int> visited, List<int> result)
        {
            if (visited.Contains(node)) return;

            visited.Add(node);
            result.Add(node);

            if (graph.ContainsKey(node))
            {
                foreach (int neighbor in graph[node])
                {
                    DFSRecursive(graph, neighbor, visited, result);
                }
            }
        }

        /// <summary>
        /// Template for graph DFS (iterative)
        /// Time: O(V + E), Space: O(V)
        /// </summary>
        public static List<int> GraphDFSIterative(Dictionary<int, List<int>> graph, int start)
        {
            List<int> result = new List<int>();
            Stack<int> stack = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();

            stack.Push(start);

            while (stack.Count > 0)
            {
                int node = stack.Pop();

                if (visited.Contains(node)) continue;

                visited.Add(node);
                result.Add(node);

                if (graph.ContainsKey(node))
                {
                    // Push in reverse order for consistent traversal
                    for (int i = graph[node].Count - 1; i >= 0; i--)
                    {
                        stack.Push(graph[node][i]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Template for connected components in undirected graph
        /// Time: O(V + E), Space: O(V)
        /// </summary>
        public static int CountComponents(int n, int[][] edges)
        {
            // Build adjacency list
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }

            foreach (var edge in edges)
            {
                graph[edge[0]].Add(edge[1]);
                graph[edge[1]].Add(edge[0]);
            }

            HashSet<int> visited = new HashSet<int>();
            int components = 0;

            for (int i = 0; i < n; i++)
            {
                if (!visited.Contains(i))
                {
                    DFSCountComponents(graph, i, visited);
                    components++;
                }
            }

            return components;
        }

        private static void DFSCountComponents(Dictionary<int, List<int>> graph, int node, HashSet<int> visited)
        {
            if (visited.Contains(node)) return;

            visited.Add(node);

            foreach (int neighbor in graph[node])
            {
                DFSCountComponents(graph, neighbor, visited);
            }
        }

        // ========================================
        // MATRIX DFS PATTERNS
        // ========================================

        /// <summary>
        /// Template for number of islands (DFS version)
        /// Time: O(m * n), Space: O(m * n)
        /// </summary>
        public static int NumIslandsDFS(char[,] grid)
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
                        DFSIsland(grid, i, j, rows, cols);
                        islandCount++;
                    }
                }
            }

            return islandCount;
        }

        private static void DFSIsland(char[,] grid, int row, int col, int rows, int cols)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols || grid[row, col] != '1')
                return;

            grid[row, col] = '0'; // Mark as visited

            // Explore all 4 directions
            DFSIsland(grid, row - 1, col, rows, cols); // Up
            DFSIsland(grid, row + 1, col, rows, cols); // Down
            DFSIsland(grid, row, col - 1, rows, cols); // Left
            DFSIsland(grid, row, col + 1, rows, cols); // Right
        }

        /// <summary>
        /// Template for surrounded regions
        /// Time: O(m * n), Space: O(m * n)
        /// </summary>
        public static void SolveSurroundedRegions(char[,] board)
        {
            if (board == null || board.GetLength(0) == 0) return;

            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            // Mark 'O' regions connected to border
            for (int i = 0; i < rows; i++)
            {
                if (board[i, 0] == 'O') DFSMark(board, i, 0, rows, cols);
                if (board[i, cols - 1] == 'O') DFSMark(board, i, cols - 1, rows, cols);
            }

            for (int j = 0; j < cols; j++)
            {
                if (board[0, j] == 'O') DFSMark(board, 0, j, rows, cols);
                if (board[rows - 1, j] == 'O') DFSMark(board, rows - 1, j, rows, cols);
            }

            // Flip surrounded regions
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board[i, j] == 'O')
                        board[i, j] = 'X';
                    else if (board[i, j] == 'T')
                        board[i, j] = 'O';
                }
            }
        }

        private static void DFSMark(char[,] board, int row, int col, int rows, int cols)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols || board[row, col] != 'O')
                return;

            board[row, col] = 'T'; // Temporary mark

            DFSMark(board, row - 1, col, rows, cols);
            DFSMark(board, row + 1, col, rows, cols);
            DFSMark(board, row, col - 1, rows, cols);
            DFSMark(board, row, col + 1, rows, cols);
        }

        // ========================================
        // BACKTRACKING DFS PATTERNS
        // ========================================

        /// <summary>
        /// Template for all paths in graph
        /// Time: O(V!), Space: O(V)
        /// </summary>
        public static List<List<int>> AllPathsSourceTargetDFS(int[][] graph)
        {
            List<List<int>> result = new List<List<int>>();
            List<int> path = new List<int> { 0 };

            DFSAllPaths(graph, 0, path, result);

            return result;
        }

        private static void DFSAllPaths(int[][] graph, int node, List<int> path, List<List<int>> result)
        {
            if (node == graph.Length - 1)
            {
                result.Add(new List<int>(path));
                return;
            }

            foreach (int neighbor in graph[node])
            {
                path.Add(neighbor);
                DFSAllPaths(graph, neighbor, path, result);
                path.RemoveAt(path.Count - 1);
            }
        }

        /// <summary>
        /// Template for word search in grid
        /// Time: O(m * n * 4^L) where L is word length
        /// Space: O(L)
        /// </summary>
        public static bool Exist(char[,] board, string word)
        {
            if (string.IsNullOrEmpty(word)) return true;
            if (board == null || board.GetLength(0) == 0) return false;

            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board[i, j] == word[0])
                    {
                        if (DFSWordSearch(board, word, 0, i, j, rows, cols))
                            return true;
                    }
                }
            }

            return false;
        }

        private static bool DFSWordSearch(char[,] board, string word, int index, int row, int col, int rows, int cols)
        {
            if (index == word.Length) return true;

            if (row < 0 || row >= rows || col < 0 || col >= cols || board[row, col] != word[index])
                return false;

            char temp = board[row, col];
            board[row, col] = '#'; // Mark as visited

            bool found = DFSWordSearch(board, word, index + 1, row - 1, col, rows, cols) || // Up
                         DFSWordSearch(board, word, index + 1, row + 1, col, rows, cols) || // Down
                         DFSWordSearch(board, word, index + 1, row, col - 1, rows, cols) || // Left
                         DFSWordSearch(board, word, index + 1, row, col + 1, rows, cols);   // Right

            board[row, col] = temp; // Backtrack

            return found;
        }

        // ========================================
        // HELPER METHODS
        // ========================================

        public static void PrintDFSTraversal<T>(IEnumerable<T> traversal, string message = "DFS Traversal")
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