using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem037
{
    class Program
    {
        static void Main(string[] args)
        {
            // Just a guess - long truncation prime are rare.
            int max = 1000000;
            List<int> primes = EulerUtil.Primes(max).ToList();
            HashSet<int> primeSet = new HashSet<int>(primes);
            List<int> seeds = primes.Where(i => i / 10 == 0).ToList();

            HashSet<int> leftPrimes = LeftPrimes(primes, seeds);
            HashSet<int> rightPrimes = RightPrimes(primes, seeds);
            Console.WriteLine(leftPrimes.Intersect(rightPrimes).Where(t => t / 10 > 0).Aggregate((x, y) => x + y));
        }

        private static HashSet<int> LeftPrimes(List<int> primes, List<int> seeds)
        {
            HashSet<int> leftPrimes = new HashSet<int>(seeds);
            int multiplier = 1;
            for (int numDigits = 2; numDigits <= 7; numDigits++)
            {
                multiplier *= 10;
                List<int> temp = new List<int>();
                for (int i = 1; i <= 9; i++)
                {
                    foreach (int seed in leftPrimes)
                    {
                        int candidate = i * multiplier + seed;
                        if (primes.Contains(candidate))
                        {
                            temp.Add(candidate);
                        }
                    }
                }
                foreach (int i in temp)
                {
                    leftPrimes.Add(i);
                }
            }
            return leftPrimes;
        }

        private static HashSet<int> RightPrimes(List<int> primes, List<int> seeds)
        {
            HashSet<int> rightPrimes = new HashSet<int>(seeds);
            for (int numDigits = 2; numDigits <= 7; numDigits++)
            {
                List<int> temp = new List<int>();
                for (int i = 1; i <= 9; i++)
                {
                    foreach (int seed in rightPrimes)
                    {
                        int candidate = i + seed * 10 ;
                        if (primes.Contains(candidate))
                        {
                            temp.Add(candidate);
                        }
                    }
                }
                foreach (int i in temp)
                {
                    rightPrimes.Add(i);
                }
            }
            return rightPrimes;
        }        
    }
}
