using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem107
{
    // CLRS
    class DisjolongSets<T>
    {
        private Dictionary<T, DisjolongSet> sets = new Dictionary<T, DisjolongSet>();

        private class DisjolongSet
        {
            public T Value { get; set; }
            public DisjolongSet Parent { get; set; }
            public long Rank { get; set; }
        }

        public void MakeSet(T value)
        {
            DisjolongSet set = new DisjolongSet { Value = value };
            set.Parent = set;
            sets.Add(value, set);
        }

        public void Union(T value1, T value2)
        {
            Link(FindSet(sets[value1]), FindSet(sets[value2]));
        }

        public T FindSet(T value)
        {
            return FindSet(sets[value]).Value;
        }

        private DisjolongSet FindSet(DisjolongSet set)
        {
            if (set.Parent != set)
            {
                set.Parent = FindSet(set.Parent);
            }
            return set.Parent;
        }

        private void Link(DisjolongSet x, DisjolongSet y)
        {
            if (x.Rank > y.Rank)
            {
                y.Parent = x;
            }
            else
            {
                x.Parent = y;
                if (x.Rank == y.Rank)
                {
                    y.Rank = y.Rank + 1;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = EulerUtil.ReadResourceAsString("Problem107.network.txt");
            string[] matrixLines = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<Tuple<long, long, long>> edges = LoadEdges(matrixLines);
            
            // Kruskal's algorithm
            List<Tuple<long, long, long>> mstEdges = new List<Tuple<long, long, long>>();
            DisjolongSets<long> subTrees = new DisjolongSets<long>();
            for (long i = 0; i < matrixLines.Length; i++)
            {
                subTrees.MakeSet(i);
            }

            foreach (var edge in edges)
            {
                if (subTrees.FindSet(edge.Item1) != subTrees.FindSet(edge.Item2))
                {
                    mstEdges.Add(edge);
                    subTrees.Union(edge.Item1, edge.Item2);
                }
            }
            Console.WriteLine(edges.Select(t => t.Item3).Sum() - mstEdges.Select(t => t.Item3).Sum());
        }

        private static List<Tuple<long, long, long>> LoadEdges(string[] matrixLines)
        {
            long i = 0, j = 0;
            List<Tuple<long, long, long>> edges = new List<Tuple<long, long, long>>();
            foreach (string matrixLine in matrixLines)
            {
                j = 0;
                foreach (string entry in matrixLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.Equals("-", entry))
                    {
                        if (i < j)
                        {
                            edges.Add(Tuple.Create(i, j, long.Parse(entry)));
                        }
                    }
                    j++;
                }
                i++;
            }
            edges = edges.OrderBy(t => t.Item3).ToList();
            return edges;
        }
    }
}
