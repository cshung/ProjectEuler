namespace Problem121
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Common;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            int round = 15;

            BigInteger denominator = 1;
            for (int i = 2; i <= round + 1; i++)
            {
                denominator *= i;
            }

            BigInteger numerator = 1;

            for (int redCount = 1; redCount <= round; redCount++)
            {
                int blueCount = round - redCount;
                bool win = blueCount > redCount;
                if (win)
                {
                    // Round numbers
                    List<int> roundNumbers = Enumerable.Range(1, round).ToList();
                    foreach (var losingRounds in EulerUtil.Combinations(roundNumbers, redCount))
                    {
                        BigInteger currentNumerator = 1;
                        foreach (var losingRound in losingRounds)
                        {
                            currentNumerator *= losingRound;
                        }
                        numerator += currentNumerator;
                    }
                }
            }
            BigInteger difference = denominator - numerator;
            BigInteger rem;
            BigInteger result = BigInteger.DivRem(difference, numerator, out rem);
            if (rem > 0)
            {
                // We wanted the ceiling of the quotient
                result = result + 1;
            }
            Console.WriteLine(result);
        }
    }
}
