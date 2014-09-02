namespace Euler
{
    using System;
    using System.Linq;
    using System.Numerics;

    internal static partial class Program
    {
        public static void Problem020()
        {
            BigInteger prod = new BigInteger(1);
            for (int i = 2; i <= 100; i++)
            {
                prod = BigInteger.Multiply(prod, new BigInteger(i));
            }
            Console.WriteLine(prod.ToString().Select(i => i - '0').Aggregate((x, y) => x + y));
        }
    }
}
