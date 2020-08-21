namespace Problem108
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    /*
     *    1/x + 1/y = 1/n
     *     n(y + x) = xy
     * xy - nx - ny = 0
     * xy - nx - ny + n^2 = n^2
     * (x - n)(y - n) = n^2
     * 
     * number of solutions = number of ways to represent n^2 as a product p x q where p <= q, p, q >= 1
     * Note that p not necessary a factor of n.
     * 
     * The clever solution is that foreach factor less than n, there is a factor larger than n
     * Therefore, the number of factor of representation is (number of divisor of n^2 + 1) / 2
     */
    class Program
    {
        /*
Minimize n such that 1/x + 1/y + 1/n has at least 4,000,000 solutions
           such that (tau(n^2) + 1)/2 >= 4,000,000 
           such that tau(n^2) + 1 >= 8,000,000 
           such that tau(n^2) >= 7,999,999

Suppose n is factorized => (p1^n1)(p1^n2)...(pk^nk), then tau(n^2) = (2n1 + 1)(2n2 + 1)...(2nk + 1) [A product of odd numbers]

base case: 2n1 + 1 = 7,999,999, n1 = 3,999,999
2^3,999,999         
         */
        static void Main(string[] args)
        {
            int n = 4;
            while (true)
            {
                int squareFactors = EulerUtil.BruteForceFactor(n).GroupBy(t => t).Select(g => g.Count() * 2 + 1).Aggregate(1, (x, y) => x * y);
                int count = (squareFactors + 1) / 2;
                if (count >= 1000)
                {
                    Console.WriteLine(EulerUtil.BruteForceFactor(7999999).ToConcatString(","));
                    Console.WriteLine(n);
                    break;
                }

                n++;
            }
        }

        // Useful function to keep
        //private static IEnumerable<double> Factors(int n)
        //{
        //    var primeFactors = EulerUtil.BruteForceFactor(n).ToList();
        //    var grouped = primeFactors.GroupBy(t => t).Select(g => Tuple.Create(g.Key, g.Count() * 2)).ToList();
        //    var bases = grouped.Select(t => t.Item2).ToList();
        //    foreach (var powers in EulerUtil.MultiRadixNumbers(bases))
        //    {
        //        var factor = Enumerable.Zip(grouped.Select(t => t.Item1), powers, (x, y) => Math.Pow(x, y));
        //        yield return factor.Aggregate(1.0, (x, y) => x * y);
        //    }
        //}
    }
}
