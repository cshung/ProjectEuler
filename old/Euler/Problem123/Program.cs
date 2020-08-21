namespace Problem123
{
    using System;
    using System.Linq;
    using Common;

    // From problem 123 - we already knew the remainder is 2 n a, so in our case 2 n (p_n) 
    // This is a simple brute force search for this condition, the key is to work with a reasonably sized 
    // sieve for prime number generation.
    class Program
    {
        static void Main(string[] args)
        {
            var primes = EulerUtil.Primes(1000000);
            var indexed = primes.Select((e, i) => Tuple.Create((long)i + 1, e));
            long hitIndex = indexed.Where(t => 2 * t.Item1 * t.Item2 >= 10000000000).First().Item1;
            if (hitIndex % 2 == 0)
            {
                hitIndex++;
            }

            Console.WriteLine(hitIndex);
        }
    }
}
