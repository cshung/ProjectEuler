namespace Problem046
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            int max = 1000000;
            List<int> primes = EulerUtil.Primes(max).ToList();
            HashSet<int> primeSet = new HashSet<int>(primes);
            for (int i = 3; i < max; i += 2)
            {
                if (!primeSet.Contains(i))
                {
                    // It is an odd composite, now trial subtraction
                    int odd = 3;
                    bool found = true;
                    for (int square = 1; 2 * square < i; square += odd, odd += 2)
                    {
                        int trialPrime = i - 2 * square;
                        if (primeSet.Contains(trialPrime))
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found)
                    {
                        Console.WriteLine(i);
                        return;
                    }
                }
            }
        }
    }
}
