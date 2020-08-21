using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Common;

namespace Problem063
{
    class Program
    {
        static void Main(string[] args)
        {
            // Let's start with some arithmetic
            // We need a k^n to be a n-digit number, it means
            // 10^{n-1} <= k^n < 10^n
            // Now it is obvious that k < n, on the lower bound side, take log on both side
            // (n-1) ln 10 <= n ln k
            // ln k >= (n - 1) ln 10 / n
            // k >= e^{(n - 1) ln 10 / n}
            // 
            // With that, a simple algorithm is as follow
            // For all possible n (there are finite number of them only since with increasing n, the lower bound grows larger than 10 and we can stop)
            // Start with Ceil(lowerBound) to 10, try and see if the condition hit
            // Output the counts
            // QED.

            int count = 0; // Forget about n = 1 case, we know it works
            for (int i = 1; i < 10; i++)
            {
                count++;
                Console.WriteLine("{3}\t: {0}^{1} = {2} has {1} digits", i, 1, "1", count);
            }
            int n = 2;
            double ln10 = Math.Log(10);
            while (true)
            {
                int lowerBound = (int)Math.Ceiling(Math.Exp((n - 1) * ln10 / n));
                if (lowerBound >= 10)
                {
                    break;
                }
                for (int k = lowerBound; k < 10; k++)
                {
                    BigInteger power = EulerUtil.Power<BigInteger>(new BigInteger(k), n, (x, y) => x * y, BigInteger.One);
                    if (power.ToString().Length == n)
                    {
                        count++;
                        Console.WriteLine("{3}\t: {0}^{1} = {2} has {1} digits", k, n, power.ToString(), count);
                        
                    }
                }
                n++;
            }
            Console.WriteLine(count);
        }
    }
}
