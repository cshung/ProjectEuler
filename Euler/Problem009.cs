namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem009()
        {
            foreach (var triple in GetPrimitivePythTriples(1000))
            {
                int tripleSum = triple.Item1 + triple.Item2 + triple.Item3;
                if (1000 % tripleSum == 0)
                {
                    int scale = 1000 / tripleSum;
                    Console.WriteLine(scale * scale * scale * triple.Item1 * triple.Item2 * triple.Item3);
                    return;
                }
            }
        }
    }
}
