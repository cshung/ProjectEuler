namespace Problem061
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            // Find the numbers
            var triangleNumbers = new HashSet<int>(Enumerable.Range(1, 1000).Select(i => i * (i + 1) / 2).Where(i => i >= 1000 & i <= 9999));
            var squareNumbers = new HashSet<int>(Enumerable.Range(1, 1000).Select(i => i * i).Where(i => i >= 1000 & i <= 9999));
            var pentagonNumbers = new HashSet<int>(Enumerable.Range(1, 1000).Select(i => i * (3 * i - 1) / 2).Where(i => i >= 1000 & i <= 9999));
            var hexagonNumbers = new HashSet<int>(Enumerable.Range(1, 1000).Select(i => i * (2 * i - 1)).Where(i => i >= 1000 & i <= 9999));
            var heptagonNumbers = new HashSet<int>(Enumerable.Range(1, 1000).Select(i => i * (5 * i - 3) / 2).Where(i => i >= 1000 & i <= 9999));
            var octagonNumbers = new HashSet<int>(Enumerable.Range(1, 1000).Select(i => i * (3 * i - 2)).Where(i => i >= 1000 & i <= 9999));

            // Build the graph
            var figurateNumbers = new HashSet<int>(triangleNumbers.Concat(squareNumbers).Concat(pentagonNumbers).Concat(hexagonNumbers).Concat(heptagonNumbers).Concat(octagonNumbers));
            var graph = new Dictionary<int, HashSet<int>>();
            var figurateNumbersArray = figurateNumbers.ToArray();
            for (int i = 0; i < figurateNumbersArray.Length; i++)
            {
                graph[figurateNumbersArray[i]] = new HashSet<int>();
            }

            for (int i = 0; i < figurateNumbersArray.Length; i++)
            {
                for (int j = 0; j < figurateNumbersArray.Length; j++)
                {
                    int from = figurateNumbersArray[i];
                    int to = figurateNumbersArray[j];
                    if (from % 100 == to / 100)
                    {
                        graph[from].Add(to);
                    }
                }
            }

            // Build the color map
            Dictionary<int, List<int>> colorMap = new Dictionary<int, List<int>>();
            foreach (int figurateNumber in figurateNumbers)
            {
                List<int> colors = new List<int>();
                colorMap.Add(figurateNumber, colors);
                if (triangleNumbers.Contains(figurateNumber)) { colors.Add(3); }
                if (squareNumbers.Contains(figurateNumber)) { colors.Add(4); }
                if (pentagonNumbers.Contains(figurateNumber)) { colors.Add(5); }
                if (hexagonNumbers.Contains(figurateNumber)) { colors.Add(6); }
                if (heptagonNumbers.Contains(figurateNumber)) { colors.Add(7); }
                if (octagonNumbers.Contains(figurateNumber)) { colors.Add(8); }
            }

            foreach (int node in graph.Keys)
            {
                FindCycle(graph, colorMap, node, new List<int> { node });
            }
        }

        private static void FindCycle(Dictionary<int, HashSet<int>> graph, Dictionary<int, List<int>> colorMap, int node, List<int> currentPath)
        {
            if (currentPath.Count >= 6)
            {

                if (graph[currentPath.Last()].Contains(currentPath.First()))
                {
                    Console.WriteLine(currentPath.ToConcatString(" "));
                    Console.WriteLine(BuildMatching(currentPath, colorMap).ToConcatString(" "));
                }
                return;
            }

            {
                foreach (int neighbor in graph[node])
                {
                    if (IsCompatibleWith(neighbor, currentPath, colorMap))
                    {
                        FindCycle(graph, colorMap, neighbor, new List<int>(currentPath.Concat(new List<int> { neighbor })));
                    }
                }
            }
        }

        private static bool IsCompatibleWith(int neighbor, List<int> currentPath, Dictionary<int, List<int>> colorMap)
        {
            return BuildMatching(new List<int>(new List<int> { neighbor }.Concat(currentPath)), colorMap).Count == currentPath.Count + 1;
        }

        private static List<Tuple<NumberNode, ColorNode>> BuildMatching(List<int> nodes, Dictionary<int, List<int>> colorMap)
        {
            Dictionary<NumberNode, HashSet<ColorNode>> graph = new Dictionary<NumberNode, HashSet<ColorNode>>();
            foreach (int node in nodes)
            {
                foreach (int nodeColor in colorMap[node])
                {
                    AddEdge(graph, new NumberNode { Number = node }, new ColorNode { Color = nodeColor });
                }
            }

            return BipartiteGraphHelper.Matching(graph);
        }

        private static void AddEdge<TNode, TColor>(Dictionary<TNode, HashSet<TColor>> graph, TNode node, TColor color)
        {
            HashSet<TColor> colors;
            if (!graph.TryGetValue(node, out colors))
            {
                colors = new HashSet<TColor>();
                graph.Add(node, colors);
            }

            colors.Add(color);
        }

        class NumberNode { public int Number { get; set; } public override string ToString() { return this.Number.ToString(); } public override bool Equals(object obj) { NumberNode that = obj as NumberNode; if (that != null) { return this.Number == that.Number; } return false; } public override int GetHashCode() { return this.Number.GetHashCode(); } }
        class ColorNode { public int Color { get; set; } public override string ToString() { return this.Color.ToString(); } public override bool Equals(object obj) { ColorNode that = obj as ColorNode; if (that != null) { return this.Color == that.Color; } return false; } public override int GetHashCode() { return this.Color.GetHashCode(); } }
    }
}
