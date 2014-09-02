namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem012()
        {
            long n = 1;
            while (true)
            {
                long t = n * (n + 1) / 2;
                int numberOfDivisors = BruteForceFactor(t).Select(p => p.Item2 + 1).Aggregate((x, y) => x * y);
                if (numberOfDivisors > 500)
                {
                    Console.WriteLine(t);
                    break;
                }
                n++;
            }
        }
    }
}