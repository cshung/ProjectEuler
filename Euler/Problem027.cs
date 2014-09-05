namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        private static Dictionary<long, bool> primeCache = new Dictionary<long, bool>();

        public static void Problem027()
        {
            Tuple<int, int> best = Tuple.Create(-1, -1);
            for (int a = -1000; a <= 1000; a++)
            {
                for (int b = -1000; b <= 1000; b++)
                {

                    long current = 0;
                    int length = 0;
                    while (true)
                    {
                        long value = current * current + a * current + b;
                        if (!IsPrime(value))
                        {
                            break;
                        }

                        length++;
                        current++;
                    }
                    if (length > best.Item1)
                    {
                        best = Tuple.Create(length, a * b);
                    }
                };
            };
            Console.WriteLine(best.Item1);
            Console.WriteLine(best.Item2);
        }

        private static bool IsPrime(long x)
        {
            if (x < 0)
            {
                x = -x;
            }
            bool result;
            if (primeCache.TryGetValue(x, out result))
            {
                return result;
            }
            for (long y = 2; y < x; y++)
            {
                if (x % y == 0)
                {
                    primeCache.Add(x, false);
                    return false;
                }
            }
            primeCache.Add(x, true);
            return true;
        }
    }
}