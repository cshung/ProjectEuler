using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Numerics;

namespace Problem110
{
    class Program
    {
        // This is not really a solution - it is some guess work
        // Basically, we know (from problem 108)
        // number of solutions = (tau(n^2) - 1)/2 = (product of odd numbers - 1) / 2
        // Those elements in the product are (2 * power_of_prime in factorization of n + 1) - so in order to minimize n
        // it is important to keep those powers small, as a heuristic, I just take 3, 5 and 7
        // Then we compute n
        // Again - there is no guarantee the smallest n yields from least tau(n^2) - this is really just a heuristic, but it worked.

        // I double checked with the forum - there is a reasonable brute force solution to this - but it is still heuristics anyway.
        static void Main(string[] args)
        {
            int required = 4000000;
            int tau = required * 2 - 1;
            var primes = EulerUtil.Primes(50).ToList();
            while (true)
            {
                int current = tau;
                int n3 = 0;
                int n5 = 0;
                int n7 = 0;
                while (true)
                {
                    if (current % 3 == 0)
                    {
                        current /= 3;
                        n3++;
                    }
                    else if (current % 5 == 0)
                    {
                        n5++;
                        current /= 5;
                    }
                    else if (current % 7 == 0)
                    {
                        n7++;
                        current /= 7;
                    }
                    else
                    {
                        if (current == 1)
                        {
                            //Console.WriteLine("3^" + n3 + " x " + "5^" + n5 + " x "  +"7^" + n7);
                            //Console.WriteLine(tau);
                            var powers = Enumerable.Range(1, n7).Select(_ => 3).Concat(Enumerable.Range(1, n5).Select(_ => 2).Concat(Enumerable.Range(1, n3).Select(_ => 1))).ToList();
                            var zip = Enumerable.Zip(primes, powers, (x, y) => BigInteger.Parse(Math.Pow(x, y).ToString()));
                            var n = zip.Aggregate((x, y) => x * y);
                            Console.WriteLine(n);
                            return;
                        }
                        break;
                    }
                }

                tau++;
            }
        }
    }
}
