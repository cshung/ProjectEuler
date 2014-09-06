namespace Euler
{
    using System;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem022()
        {
            string[] names = ReadResourceAsString("Euler.Problem022.txt").Split(new char[] { '"', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(names);
            var nameValues = names.Select((s, i) => Tuple.Create(s, i + 1L)).Select(t => Tuple.Create(t.Item1, t.Item1.Select(c => c - 'A' + 1).Aggregate((x, y) => x + y), t.Item2));
            Console.WriteLine(nameValues.Select(t => t.Item2 * t.Item3).Aggregate((x, y) => x + y));
        }
    }
}
