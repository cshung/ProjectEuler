namespace Euler
{
    using System;
    using System.Collections.Generic;

    internal static partial class Program
    {
        public static void Problem007()
        {
            List<int> primes = new List<int>();
            int primeCount = 0;
            int current = 2;
            while (true)
            {
                bool isPrime = true;
                foreach (int prime in primes)
                {
                    if (current % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(current);
                    primeCount++;
                    if (primeCount == 10001)
                    {
                        Console.WriteLine(current);
                        return;
                    }
                }
                current++;
            }
        }
    }
}
