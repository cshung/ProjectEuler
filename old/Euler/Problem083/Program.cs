using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem083
{
    class Program
    {
        static void Main(string[] args)
        {
            string matrixString = EulerUtil.ReadResourceAsString("Problem083.matrix.txt");
            int size = 80;
            int[][] matrix = matrixString.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Split(',').Select(u => int.Parse(u)).ToArray()).ToArray();
            //int size = 5;
            //int[][] matrix = new int[][]
            //{
            //    new int[]{131,673,234,103,18}, 
            //    new int[]{201,96,342,965,150},
            //    new int[]{630,803,746,422,111},
            //    new int[]{537,699,497,121,956},
            //    new int[]{805,732,524,37,331}
            //};

            // Construct a graph based on the matrix
            // Each cell in the matrix is a node
            // Each adjacent cell from left to right, and from top to down is an edge, the weight is the destination cell value

            Tuple<int, int>[,] nodeStructured = new Tuple<int, int>[size, size];
            Tuple<int, int>[] nodes = new Tuple<int, int>[size * size];
            int c = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    nodes[c++] = nodeStructured[i, j] = Tuple.Create(i, j);
                }
            }

            Tuple<Tuple<int, int>, Tuple<int, int>, int>[] edges = new Tuple<Tuple<int, int>, Tuple<int, int>, int>[(size - 1) * size * 4];
            c = 0;
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    edges[c++] = Tuple.Create(nodeStructured[i, j], nodeStructured[i + 1, j], matrix[i + 1][j]);
                    edges[c++] = Tuple.Create(nodeStructured[i + 1, j], nodeStructured[i, j], matrix[i][j]);
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    edges[c++] = Tuple.Create(nodeStructured[i, j], nodeStructured[i, j + 1], matrix[i][j + 1]);
                    edges[c++] = Tuple.Create(nodeStructured[i, j + 1], nodeStructured[i, j], matrix[i][j]);
                }
            }

            var parents = EulerUtil.BellmanFord(nodes[0], nodes, edges);
            var shortestPath = EulerUtil.GetShortestPath(parents, nodes[nodes.Length - 1]).Select(t => matrix[t.Item1][t.Item2]).ToArray();
            Console.WriteLine(shortestPath.ToConcatString(" "));
            Console.WriteLine(shortestPath.Aggregate((x, y) => x + y));
        }
    }
}
