using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Diagnostics;

namespace Problem077
{
    class Program
    {
        static void Main(string[] args)
        {
            var primes = EulerUtil.Primes(100).ToArray();
            for (int i = 3; i <= 100; i++) 
            {   
                int ways;
                if (TryWriteAsPrimeSum(primes, i, i, out ways))
                {
                    Console.WriteLine("way(" + i + ") = " + ways);
                    if (ways >= 5000)
                    {
                        Console.WriteLine("Done!");
                        break;
                    }
                }
            }

        }

        // Consider 4, 
        // 3 is a prime less than it, so count how many ways you can write the remaining as a sum of primes (well less than 3)
        // The remaining is 1 => cannot be written, so the choice of using 3 failed - in that case we return false
        // otherwise, now try 2
        private static bool TryWriteAsPrimeSum(int[] primes, int sum, int maxPrimeAllowed, out int ways)
        {
            Debug.Assert(sum > 0);
            if (sum == 1)
            {
                ways = 0;
                return false;
            }
            else
            {
                int waySum = 0;
                if (primes.Contains(sum) && sum <= maxPrimeAllowed)
                {
                    waySum++;
                }

                foreach (var validPrime in primes.Where(t => sum > t && t <= maxPrimeAllowed))
                {
                    int thisWay;
                    if (TryWriteAsPrimeSum(primes, sum - validPrime, validPrime, out thisWay))
                    {
                        waySum += thisWay;
                    }
                }

                if (waySum > 0)
                {
                    ways = waySum;
                    return true;
                }
                else
                {
                    ways = -1;
                    return false;
                }
            }
        }
    }
}
