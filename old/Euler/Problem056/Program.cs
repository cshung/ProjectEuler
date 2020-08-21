namespace Problem056
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DigitSums().Max());
            
        }

        static IEnumerable<int> DigitSums()
        {
            for (int a = 1; a < 100; a++)
            {
                for (int b = 1; b < 100; b++)
                {
                    yield return EulerUtil.Power(new BigInteger(a), b, (x, y) => x * y, BigInteger.One).ToString().Select(t => t - '0').Aggregate((x, y) => x + y);
                }
            }
        }
    }
}
