namespace Euler
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    internal static partial class Program
    {
        private class Solution030
        {
            public void Run()
            {
                Console.WriteLine(GetPowers().Aggregate((x, y) => x + y));
            }

            private IEnumerable<int> GetPowers()
            {
                int max = 9 * 9 * 9 * 9 * 9 * 6;
                int[] powers = Enumerable.Range(0, 10).Select(x => x * x * x * x * x).ToArray();
                for (int i = 1; i <= max; i++)
                {
                    IEnumerable<int> digits = i.ToString().Select(c => c - '0');
                    int digitPowerSum = digits.Select(d => powers[d]).Aggregate((x, y) => x + y);
                    if (digitPowerSum == i && i != 1)
                    {
                        yield return i;
                    }
                }
            }
        }

        public static void Problem030()
        {
            new Solution030().Run();
        }
    }
}