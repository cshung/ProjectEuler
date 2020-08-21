namespace Problem124
{
    using System;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            var rads = Enumerable.Range(1, 100000).Select(e => Tuple.Create(e, EulerUtil.BruteForceFactor(e))).Select(e => Tuple.Create(e.Item1, e.Item2.GroupBy(f => f).Select(g => g.Key).Aggregate((x, y) => x * y))).OrderBy(e => e.Item2).ToArray();
            Console.WriteLine(rads[9999].Item1);
        }
    }
}
