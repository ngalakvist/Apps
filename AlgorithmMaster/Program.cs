using AlgorithmMaster.Examples;
using AlgorithmMaster.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlgorithmMaster
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    Algorithm Master - Problem Solver");
            Console.WriteLine("========================================");
            Console.WriteLine();

            var examples = new Dictionary<string, Action>
            {
                ["Array Examples"] = ArrayExamples.RunAll,
                ["String Examples"] = StringExamples.RunAll,
                ["HashMap Examples"] = HashMapExamples.RunAll,
                ["Two Pointers Examples"] = TwoPointersExamples.RunAll,
                ["Sliding Window Examples"] = SlidingWindowExamples.RunAll,
                ["Binary Search Examples"] = BinarySearchExamples.RunAll,
                ["BFS Examples"] = BFSExamples.RunAll,
                ["DFS Examples"] = DFSExamples.RunAll,
                ["Backtracking Examples"] = BacktrackingExamples.RunAll,
                ["Priority Queue Examples"] = PriorityQueueExamples.RunAll,
                ["Performance Profiling"] = PerformanceProfiler.RunPerformanceTests
            };

            while (true)
            {
                Console.WriteLine("Available Categories:");
                Console.WriteLine();

                for (int i = 0; i < examples.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {examples.ElementAt(i).Key}");
                }

                Console.WriteLine($"  {examples.Count + 1}. Exit");
                Console.WriteLine();
                Console.Write("Select a category (number): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice >= 1 && choice <= examples.Count)
                    {
                        Console.Clear();
                        examples.ElementAt(choice - 1).Value();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (choice == examples.Count + 1)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Thank you for using Algorithm Master!");
        }
    }
}