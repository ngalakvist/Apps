# Algorithm Master - C# Problem-Solving Templates

A comprehensive collection of algorithmic problem-solving templates in C# for .NET 8+, complete with examples, performance analysis, and best practices.

## 🎯 What's Included

### Algorithm Templates
- **Arrays & Strings** - Two pointers, sliding window, prefix sum patterns
- **Hash Maps** - Frequency counting, LRU cache, union-find
- **Binary Search** - Classic search, rotated arrays, peak finding
- **Trees & Graphs** - DFS, BFS, path finding, connected components
- **Backtracking** - Subsets, permutations, combinations, N-Queens
- **Priority Queues** - Heaps, top K elements, scheduling algorithms

### Key Features
- ✅ **Production-ready code** - Following .NET best practices
- ✅ **Comprehensive documentation** - Line-by-line explanations
- ✅ **Performance analysis** - Big O notation and benchmarking
- ✅ **Real-world examples** - Practical problem-solving demonstrations
- ✅ **Unit test support** - Built-in testing utilities
- ✅ **Performance profiling** - BenchmarkDotNet integration

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 or higher
- C# 12 or higher
- Visual Studio 2022 or VS Code

### Installation

1. **Clone or download** the project files
2. **Navigate to project directory**:
   ```bash
   cd AlgorithmMaster
   ```

3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

### Usage

```csharp
using AlgorithmMaster.Templates;
using AlgorithmMaster.Utils;

// Example: Two Sum problem
int[] nums = {2, 7, 11, 15};
int target = 9;
int[] result = HashMapTemplate.TwoSum(nums, target);

// Example: Test your solution
TestRunner.RunTest("Two Sum Test", 
    () => HashMapTemplate.TwoSum(new int[] {2, 7, 11, 15}, 9),
    new int[] {0, 1});
```

## 📚 Documentation

### Algorithm Categories

#### 1. Arrays & Strings
- **Two Pointers**: Opposite and same direction patterns
- **Sliding Window**: Fixed and variable size windows
- **Prefix Sum**: Efficient range queries
- **In-place Operations**: Memory-efficient algorithms

#### 2. Hash Maps & Dictionaries
- **Frequency Counting**: Character and element frequencies
- **Two Sum Variations**: Multiple sum problems
- **LRU Cache**: Least Recently Used cache implementation
- **Union-Find**: Disjoint set operations

#### 3. Binary Search
- **Classic Search**: Standard binary search
- **Rotated Arrays**: Search in rotated sorted arrays
- **Peak Finding**: Find peak elements
- **Answer Space**: Binary search on possible answers

#### 4. Tree & Graph Algorithms
- **DFS**: Depth-first traversal patterns
- **BFS**: Breadth-first traversal patterns
- **Path Finding**: Shortest paths, all paths
- **Connected Components**: Island counting, region marking

#### 5. Backtracking
- **Subsets**: All possible subsets
- **Permutations**: All possible arrangements
- **Combinations**: K-element combinations
- **Path Finding**: N-Queens, word search

#### 6. Priority Queues & Heaps
- **Top K Elements**: K largest/smallest elements
- **Scheduling**: Meeting rooms, task scheduling
- **Graph Algorithms**: Dijkstra's, Prim's algorithms
- **Median Finding**: Stream median calculation

## 📊 Performance Analysis

### Big O Complexity Reference

| Complexity | Name | Use Cases |
|------------|------|-----------|
| O(1) | Constant | Array access, Hash lookup |
| O(log n) | Logarithmic | Binary search, Tree operations |
| O(n) | Linear | Array traversal, Linear search |
| O(n log n) | Linearithmic | Merge sort, Quick sort |
| O(n²) | Quadratic | Nested loops, Bubble sort |

### Benchmarking

Run performance comparisons:

```bash
dotnet run
# Select "Performance Profiling" from the menu
```

## 🧪 Testing

### Unit Testing

```csharp
// Built-in test runner
TestRunner.RunTest("Test Name", 
    () => YourAlgorithm(input),
    expectedResult);

// Performance testing
TestRunner.RunPerformanceTest("Algorithm Name", 
    () => YourAlgorithm(input), 
    iterations: 1000);
```

### Test Coverage
Each template includes:
- Edge cases (empty inputs, single elements)
- Boundary conditions (minimum/maximum values)
- Performance tests with large datasets
- Real-world example scenarios

## 🛠️ Development

### Project Structure
```
AlgorithmMaster/
├── Templates/          # Algorithm implementations
├── Examples/           # Usage demonstrations
├── Utils/              # Helper utilities
├── Program.cs          # Main application
└── AlgorithmMaster.csproj
```

### Adding New Templates

1. **Create template file** in `Templates/` directory
2. **Follow existing patterns** for consistency
3. **Add examples** in `Examples/` directory
4. **Write tests** using TestRunner utilities
5. **Update documentation** with usage examples

### Code Style
- Follow C# naming conventions
- Use meaningful variable names
- Add XML documentation comments
- Include complexity analysis
- Write comprehensive tests

## 📈 Performance Tips

1. **Choose appropriate data structures**
2. **Consider time-space trade-offs**
3. **Use built-in .NET optimizations**
4. **Profile before optimizing**
5. **Test with realistic data sizes**

## 🎓 Learning Path

### Beginner
1. Start with Array and String templates
2. Practice Two Pointers and Sliding Window
3. Understand Big O notation
4. Implement templates from scratch

### Intermediate
1. Master Hash Map and Binary Search patterns
2. Learn Tree and Graph algorithms
3. Practice backtracking problems
4. Analyze performance trade-offs

### Advanced
1. Optimize for specific constraints
2. Combine multiple patterns
3. Handle edge cases efficiently
4. Contribute to template improvements

## 🤝 Contributing

Contributions are welcome! Please:

1. Fork the repository
2. Create a feature branch
3. Add comprehensive tests
4. Update documentation
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

- 📖 **Documentation**: Check the PDF guide for detailed explanations
- 💬 **Issues**: Report bugs and request features
- 📧 **Contact**: Reach out for questions and feedback

## 🎯 Success Tips

1. **Practice regularly** - Solve 1-2 problems daily
2. **Understand patterns** - Don't just memorize solutions
3. **Code by hand** - Build muscle memory
4. **Explain solutions** - Teach others to solidify understanding
5. **Time yourself** - Simulate interview conditions

---

**Happy Coding!** 🚀

*Master algorithms, master your career.*