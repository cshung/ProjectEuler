namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        //
        // Given the two perfect squares x + y and x - y equals to, it is easy to compute x and y
        // There are relatively few perfect squares, so come up with a lot of (x, y) ordered pair is easy
        // We can then define a graph by (x, y) as directed edge, 
        // Then ask if there is a subgraph with edge (x, y), (y, z), (x, z)
        // As we generate the graph is quite sparse, so after indexing looping through isn't bad at all.
        //
        // The only problem is how big to generate the graph initially, I tried 10, 100, then 1000 worked
        //
        // To minimize (x + y + z), it is the same as minimizing (x + y) + (y + z) + (x + z) = 2(x + y + z)
        // So conceptually each edge has cost (x + y), and the cost of the cycle is the sum of all costs.
        // Increasing the limit amounts to adding more high cost edges in.
        //
        // But situation like this could happen, so adding high cost edges discover cycle with smaller cost
        // 1 + 1 + 5, 2 + 3 + 4
        // 
        public static void Problem142()
        {
            Dictionary<int, HashSet<int>> graph = new Dictionary<int, HashSet<int>>();

            // The value 1,000 is guessed, 10 and 100 does not work
            int[] squares = Enumerable.Range(1, 1000).Select(t => t * t).ToArray();
            for (int i = 0; i < squares.Length; i++)
            {
                // Only if i and j match parity the divide by 2 make sense, that save a significant number of tests
                for (int j = i + 2; j < squares.Length; j += 2)
                {
                    int x = (squares[i] + squares[j]) / 2;
                    int y = (squares[j] - squares[i]) / 2;
                    HashSet<int> neighbor;
                    if (!graph.TryGetValue(x, out neighbor))
                    {
                        neighbor = new HashSet<int>();
                        graph.Add(x, neighbor);
                    }
                    neighbor.Add(y);
                }
            }

            foreach (var x in graph.Keys)
            {
                HashSet<int> ys;
                if (graph.TryGetValue(x, out ys))
                {
                    foreach (var y in ys)
                    {
                        HashSet<int> zs;
                        if (graph.TryGetValue(y, out zs))
                        {
                            foreach (var z in zs)
                            {
                                if (ys.Contains(z))
                                {
                                    Console.WriteLine(x + "," + y + "," + z);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}