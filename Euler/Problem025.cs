namespace Euler
{
    using System;
    using System.Numerics;

    internal static partial class Program
    {
        public static void Problem025()
        {
            new Solution025().Execute();
        }

        private class Solution025
        {
            public void Execute()
            {
                FibMatrix one = new FibMatrix
                {
                    a = 1,
                    b = 1,
                    d = 0
                };

                // By Binet formula approximation, we know the solution is around this
                int n = 4784;
                FibMatrix m = Power(one, n, one, Multiply);

                BigInteger big = m.a;
                BigInteger small = m.b;
                // small is the nth Fibonacci number

                // I already know small has 1000 digits by debugging, so I know I need to going backwards
                // in full generality I need to check if I need forward as well
                while (small.ToString().Length >= 1000)
                {
                    BigInteger prev = big - small;
                    big = small;
                    small = prev;
                    n--;
                    // small is still the nth Fibonacci number after the line
                }

                // Small has 999 digits, big has 1000 digit, so the answer is n + 1
                Console.WriteLine(n + 1);
            }

            private FibMatrix Multiply(FibMatrix p, FibMatrix q)
            {
                FibMatrix result = new FibMatrix();
                result.a = p.a * q.a + p.b * q.b;
                result.b = p.a * q.b + p.b * q.d;
                result.d = p.b * q.b + p.d * q.d;
                return result;
            }

            private class FibMatrix
            {
                public BigInteger a;
                public BigInteger b;
                public BigInteger d;
            }
        }
    }
}