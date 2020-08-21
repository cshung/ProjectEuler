namespace Problem099
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            string inputString = EulerUtil.ReadResourceAsString("Problem099.base_exp.txt");
            string[] inputLines = inputString.Split(new char[]{'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<Tuple<int, int>> inputPairs = inputLines.Select(t => t.Split(',')).Select(a => Tuple.Create(int.Parse(a[0]), int.Parse(a[1])));
            Console.WriteLine(inputPairs.Select((p, i) => Tuple.Create(Math.Log(p.Item1) * p.Item2, i + 1)).OrderByDescending(t => t.Item1).First().Item2);
        }
    }
}
