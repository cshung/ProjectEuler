namespace Euler
{
    using System;
    using System.Linq;
    using System.Numerics;

    internal static partial class Program
    {
        public static void Problem016()
        {
            BigInteger p = BigInteger.Pow(new BigInteger(2), 1000);
            Console.WriteLine(p.ToString().Select(x => x - '0').Aggregate((x, y) => (x + y)));
        }
    }
}
