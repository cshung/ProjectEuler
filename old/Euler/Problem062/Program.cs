using System;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Problem062
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long[] cubes = Enumerable.Range(1, 10000).Select(t => (long)t).Select(t => t * t * t).ToArray();
            var cubeWithRep = cubes.Select(t => Tuple.Create(t, t.ToString().OrderBy(c => c).ToConcatString()));
            var cubeSorted = cubeWithRep.Select(t => t.Item2);
            var cubeGroups = cubeSorted.GroupBy(t => t).Select(t => Tuple.Create(t.Key, t.Count()));
            var goodReps = cubeGroups.Where(t => t.Item2 == 5).Select(t => t.Item1).ToArray();
            var cubeWithGoodRep = cubeWithRep.Join(goodReps, (t => t.Item2), (t => t), (t, u) => t.Item1).ToArray();
            Console.WriteLine(cubeWithGoodRep.Min());
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
