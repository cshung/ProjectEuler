namespace Problem097
{
    using System;
    using System.Numerics;

    class Program
    {
        static void Main(string[] args)
        {
            // 28433 x 2^7830457 + 1.
            BigInteger modulus = BigInteger.Parse("10000000000");
            BigInteger bigPart = BigInteger.ModPow(new BigInteger(2), new BigInteger(7830457), modulus);
            BigInteger prime  = new BigInteger(28433) * bigPart + BigInteger.One;
            BigInteger result;
            BigInteger.DivRem(prime, modulus, out result);
            Console.WriteLine(result.ToString());
        }
    }
}
