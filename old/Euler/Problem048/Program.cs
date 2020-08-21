namespace Problem048
{
    using System;
    using System.Linq;
    using System.Numerics;

    class Program
    {
        static void Main(string[] args)
        {
            var index = Enumerable.Range(1, 1000).ToList();
            var modulus = BigInteger.Parse("10000000000");
            Console.WriteLine(index.Select(t => BigInteger.ModPow(new BigInteger(t), new BigInteger(t), modulus)).Aggregate((x, y) => BigInteger.ModPow(BigInteger.Add(x, y), BigInteger.One, modulus)));
        }
    }
}
