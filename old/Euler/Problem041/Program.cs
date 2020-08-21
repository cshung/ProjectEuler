using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem041
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<long> allPandigitalPrimes = new List<long>();
            for (int i = 1; i <= 9; i++)
            {
                allPandigitalPrimes = allPandigitalPrimes.Concat(PandigitalPrimes(i));
            }

            Console.WriteLine(allPandigitalPrimes.Max());
        }

        private static IEnumerable<long> PandigitalPrimes(int numDigits)
        {
            var permutations = EulerUtil.Permutations(Enumerable.Range(1, numDigits).ToArray());
            foreach (var permutation in permutations)
            {
                long candidate = long.Parse(permutation.Aggregate("", (x, y) => x + y));
                if (EulerUtil.IsPrime(candidate))
                {
                    yield return candidate;
                }
            }
        }
    }
}
