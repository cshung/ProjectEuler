using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem035
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CircularPrime(1000000).ToList().Count());
        }

        static IEnumerable<int> CircularPrime(int max) 
        {
            IEnumerable<int> primes = EulerUtil.Primes(max).ToList();            
            HashSet<int> s = new HashSet<int>(primes);
            Console.WriteLine(primes.Count());
            int k = 0;
            foreach (int prime in s)
            {
                if (k++ % 800 == 0) { Console.Write('.'); }
                bool isCircularPrime = true;
                foreach (int shift in CircularShift(prime).ToList())
                {
                    if (!primes.Contains(shift))
                    {
                        isCircularPrime = false;
                    }
                }
                if (isCircularPrime)
                {
                    yield return prime;
                }
            }
        }

        static IEnumerable<int> CircularShift(int input)
        {
            int numDigits = input.ToString().Length;
            int p = 1;
            for (int i = 1; i < numDigits; i++) {
                p *= 10;
            }
            for (int i = 0; i < numDigits; i++)
            {
                yield return input;
                input = input % 10 * p + input / 10;
            }
        }        
    }
}
