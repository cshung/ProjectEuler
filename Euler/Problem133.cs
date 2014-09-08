namespace Euler
{
    using System;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem133()
        {
            new Solution133().Execute();
        }

        private class Solution133
        {
            public void Execute()
            {
                int sum = 0;
                // We need to skip 2, 3, 5 as 2 and 5 will never repeats
                foreach (var i in Primes(100000).Skip(3))
                {
                        int periodLength = PeriodLength(i);
                        if (!Correct(periodLength))
                        {
                            sum += i;
                        }
                }

                // We know 2, 3, 5 cannot be solution
                Console.WriteLine(sum + 2 + 3 + 5);
            }

            // If the period length is of form 2^a 5^b, then it will divide 10^(max(a, b)), and therefore R(10^(max(a, b))) == 0
            // Otherwise it will never divides
            private static bool Correct(int periodLength)
            {
                while (periodLength % 2 == 0)
                {
                    periodLength /= 2;
                }
                while (periodLength % 5 == 0)
                {
                    periodLength /= 5;
                }
                return periodLength == 1;
            }

            // By repeating computing the R(k) mod p, the sequence will eventually repeats
            // It can be proved that it will eventually goes 0 (if p is not 2 or 5)
            private static int PeriodLength(int p)
            {
                int residue = 0;
                int periodLength = 0;
                do
                {
                    residue = (residue * 10 + 1) % p;
                    periodLength++;
                } while (residue != 0);
                return periodLength;
            }
        }
    }
}
