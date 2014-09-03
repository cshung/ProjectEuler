namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem021()
        {
            Dictionary<int, int> d = new Dictionary<int, int>();
            d.Add(1, 0);
            for (int i = 2; i <= 10000; i++)
            {
                d.Add(i, SumOfProperDivisor(i));
            }
            long maxSumOfDivisorWithin10000 = d.Values.Max();
            for (int i = 10001; i <= maxSumOfDivisorWithin10000; i++)
            {
                d.Add(i, SumOfProperDivisor(i));
            }
            Console.WriteLine(AmicableNumbers(d).Aggregate((x, y) => x + y));
        }

        private static IEnumerable<int> AmicableNumbers(Dictionary<int, int> d)
        {
            for (int i = 2; i <= 10000; i++)
            {
                if (d[d[i]] == i & i != d[i])
                {
                    yield return i;
                }
            }
        }

        
    }
}
