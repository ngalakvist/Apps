using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmMaster.Utils
{
    /// <summary>
    /// Common data structures used across algorithms
    /// </summary>
    public class TreeNode
    {
        public int Val { get; set; }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }
        
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            Val = val;
            Left = left;
            Right = right;
        }
    }

    public class ListNode
    {
        public int Val { get; set; }
        public ListNode? Next { get; set; }
        
        public ListNode(int val = 0, ListNode? next = null)
        {
            Val = val;
            Next = next;
        }
    }

    public class GraphNode
    {
        public int Val { get; set; }
        public List<GraphNode> Neighbors { get; set; }
        
        public GraphNode(int val = 0)
        {
            Val = val;
            Neighbors = new List<GraphNode>();
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// Utility class for common algorithm operations
    /// </summary>
    public static class AlgorithmUtils
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static void PrintArray<T>(T[] array, string name = "Array")
        {
            Console.WriteLine($"{name}: [{string.Join(", ", array)}]");
        }

        public static void PrintList<T>(List<T> list, string name = "List")
        {
            Console.WriteLine($"{name}: [{string.Join(", ", list)}]");
        }

        public static bool ArraysEqual<T>(T[] arr1, T[] arr2) where T : IEquatable<T>
        {
            if (arr1.Length != arr2.Length) return false;
            return !arr1.Where((t, i) => !t.Equals(arr2[i])).Any();
        }

        public static int[] GenerateRandomArray(int size, int min = 0, int max = 100)
        {
            Random rand = new Random();
            return Enumerable.Range(0, size).Select(_ => rand.Next(min, max)).ToArray();
        }

        public static void PrintMatrix<T>(T[,] matrix, string name = "Matrix")
        {
            Console.WriteLine($"{name}:");
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            for (int i = 0; i < rows; i++)
            {
                Console.Write("  [");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j]);
                    if (j < cols - 1) Console.Write(", ");
                }
                Console.WriteLine("]");
            }
        }
    }
}