namespace Euler
{
    using System;
    using System.Numerics;

    internal static partial class Program
    {
        public static void Problem015()
        {
            Console.WriteLine(BinomialCoefficient(40, 20));
        }

        public static BigInteger BinomialCoefficient(long n, long c)
        {
            return BinomialCoefficient(n, c, BuildPascalTriangle(n));
        }

        public static BigInteger BinomialCoefficient(long n, long c, BigInteger[,] pascalTriangle)
        {
            return pascalTriangle[n - 1, c];
        }

        public static BigInteger[,] BuildPascalTriangle(long n)
        {
            BigInteger[,] content = new BigInteger[n, n + 1];
            content[0, 0] = BigInteger.One;
            content[0, 1] = BigInteger.One;
            for (long i = 1; i < n; i++)
            {
                content[i, 0] = 1;
                for (long j = 1; j < i + 2; j++)
                {
                    content[i, j] = content[i - 1, j - 1] + content[i - 1, j];
                }
                content[i, i + 1] = 1;
            }
            return content;
        }
    }
}
