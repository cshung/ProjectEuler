namespace Problem243
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Common;

    class Program
    {
        // Looking at the formula for Euler's function
        // n * Product(1 - 1/p) 
        // We know that the more primes it has, the smaller its value.
        // So we simply put in a lot of primes - that will minimize Euler/n.
        // For large n, n/(n - 1) is very close to 1 so it shouldn't matter - we will see.
        // By simple testing - we only need primes up to 29 - there is no point testing anything bigger because 2 x 3 x ... x 29 is a solution
        // And also 2 x 3 x ... x 23 is not a solution, meaning it must be something in between.
        static void Main(string[] args)
        {
            var primes = EulerUtil.Primes(23);
            // Others is really just guess work - trying small primes, 2 doesn't work, 3 doesn't work, 2 x 2 worked, great!
            var others = new int[] { 2, 2 };

            BigInteger n = primes.Concat(others).Select(e => new BigInteger(e)).Aggregate((x, y) => x * y);

            // By multiplicative property of the Euler function
            BigInteger euler = primes.Select(e => new BigInteger(e - 1)).Concat(others.Select(e => new BigInteger(e))).Aggregate((x, y) => x * y);
            if (euler * 94744 < 15499L * (n - 1))
            {
                Console.WriteLine(n);
            }
        }

        public static IEnumerable<long> LongRange(long start, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (i % 10000 == 0)
                {
                    Console.WriteLine(start);
                }
                yield return start++;
            }
        }
    }
}
