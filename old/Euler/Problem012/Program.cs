namespace Problem012
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            long n = 1;
            while (true)
            {
                if (n % 10000 == 0)
                {
                    Console.WriteLine(n);
                }
                long t = n * (n + 1) / 2;
                List<long> factors = EulerUtil.BruteForceFactor(t).ToList();
                factors.Sort();
                int numberOfDivisors = factors.GroupBy(z => z).Select(y => y.Count() + 1).Aggregate((x, y) => x * y);
                if (numberOfDivisors > 500)
                {
                    Console.WriteLine(t + ": " + numberOfDivisors);
                    break;
                }
                n++;
            }
        }       
    }
}