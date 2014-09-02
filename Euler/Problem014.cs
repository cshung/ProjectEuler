namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem014()
        {
            var lengths = new Dictionary<long, long>();
            for (long i = 1; i <= 1000000; i++)
            {
                Length(i, lengths);
            }
            var lengthSorted = lengths.ToList();
            lengthSorted.Sort((x, y) => (x.Value < y.Value) ? 1 : ((x.Value == y.Value) ? 0 : -1));


            Console.WriteLine(lengthSorted[0].Key);
        }

        private static long Length(long x, Dictionary<long, long> lengths)
        {
            if (x == 1)
            {
                return 1;
            }
            else
            {
                long result;
                if (lengths.TryGetValue(x, out result))
                {
                    return result;
                }
                else
                {
                    long next = x % 2 == 0 ? x / 2 : (3 * x + 1);
                    result = 1 + Length(next, lengths);
                    lengths.Add(x, result);
                    return result;
                }
            }
        }
    }
}
