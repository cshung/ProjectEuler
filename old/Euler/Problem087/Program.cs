namespace Problem087
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            var primes = EulerUtil.Primes(100000);
            double max = 50000000;
            double squareRoot = Math.Pow(max, 1.0 / 2);
            double cubeRoot = Math.Pow(max, 1.0 / 3);
            double fourthRoot = Math.Pow(max, 1.0 / 3);
            var primeSquares = primes.Where(p => p < squareRoot).Select(t => (long)t * t).ToArray();
            var primeCubes = primes.Where(p => p < cubeRoot).Select(t => (long)t * t * t).ToArray();
            var primeFourthPowers = primes.Where(p => p < cubeRoot).Select(t => (long)t * t * t * t).ToArray();
            var sums = new HashSet<long>();
            foreach (var primeSquare in primeSquares)
            {
                foreach (var primeCube in primeCubes)
                {
                    foreach (var primeFourthPower in primeFourthPowers)
                    {
                        long sum = primeSquare + primeCube + primeFourthPower;
                        if (sum < max)
                        {
                            sums.Add(sum);
                        }
                    }
                }
            }

            Console.WriteLine(sums.Count);
        }
    }
}
