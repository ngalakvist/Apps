using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlgorithmMaster.Utils
{
    /// <summary>
    /// Test runner for algorithm validation and performance testing
    /// </summary>
    public static class TestRunner
    {
        public delegate T TestFunction<T>();
        
        public static void RunTest<T>(string testName, TestFunction<T> testFunction, T expectedResult, bool compareResults = true)
        {
            Console.WriteLine($"Running: {testName}");
            
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                T result = testFunction();
                stopwatch.Stop();
                
                bool passed = true;
                if (compareResults)
                {
                    if (result is Array && expectedResult is Array)
                    {
                        passed = CompareArrays((Array)(object)result, (Array)(object)expectedResult);
                    }
                    else if (!EqualityComparer<T>.Default.Equals(result, expectedResult))
                    {
                        passed = false;
                    }
                }
                
                Console.WriteLine($"  Result: {(passed ? "PASS" : "FAIL")}");
                Console.WriteLine($"  Expected: {FormatResult(expectedResult)}");
                Console.WriteLine($"  Got:      {FormatResult(result)}");
                Console.WriteLine($"  Time:     {stopwatch.ElapsedMilliseconds}ms");
                
                if (!passed)
                {
                    Console.WriteLine($"  ❌ Test failed!");
                }
                else
                {
                    Console.WriteLine($"  ✅ Test passed!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ ERROR: {ex.Message}");
                Console.WriteLine($"  Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine();
        }
        
        public static void RunPerformanceTest<T>(string testName, TestFunction<T> testFunction, int iterations = 1000)
        {
            Console.WriteLine($"Performance Test: {testName}");
            Console.WriteLine($"Running {iterations} iterations...");
            
            long totalTime = 0;
            long minTime = long.MaxValue;
            long maxTime = long.MinValue;
            
            for (int i = 0; i < iterations; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                testFunction();
                stopwatch.Stop();
                
                long elapsed = stopwatch.ElapsedTicks;
                totalTime += elapsed;
                minTime = Math.Min(minTime, elapsed);
                maxTime = Math.Max(maxTime, elapsed);
            }
            
            double avgTimeMs = (totalTime / (double)iterations) * 1000 / Stopwatch.Frequency;
            double minTimeMs = minTime * 1000 / Stopwatch.Frequency;
            double maxTimeMs = maxTime * 1000 / Stopwatch.Frequency;
            
            Console.WriteLine($"  Average: {avgTimeMs:F4}ms");
            Console.WriteLine($"  Min:     {minTimeMs:F4}ms");
            Console.WriteLine($"  Max:     {maxTimeMs:F4}ms");
            Console.WriteLine();
        }
        
        private static string FormatResult<T>(T result)
        {
            if (result == null)
                return "null";
                
            if (result is Array array)
                return $"[{string.Join(", ", array.Cast<object>())}]";
                
            if (result is IEnumerable<object> enumerable)
                return $"[{string.Join(", ", enumerable)}]";
                
            return result.ToString();
        }
        
        private static bool CompareArrays(Array arr1, Array arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;
                
            for (int i = 0; i < arr1.Length; i++)
            {
                if (!Equals(arr1.GetValue(i), arr2.GetValue(i)))
                    return false;
            }
            
            return true;
        }
    }
}