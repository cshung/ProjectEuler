namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem023()
        {
            List<int> abundantNumbers = new List<int>();
            for (int i = 2; i <= 28124; i++)
            {
                if (IsAbundantNumbers(i))
                {
                    abundantNumbers.Add(i);
                }
            }

            // Find out the set of numbers that cannot be written as a sum of two abundant numbers.
            HashSet<int> abundantSums = new HashSet<int>();
            foreach (int abundantNumber1 in abundantNumbers)
            {
                foreach (int abundantNumber2 in abundantNumbers)
                {
                    abundantSums.Add(abundantNumber1 + abundantNumber2);
                }
            }
            long answer = (1 + 23) * 23 / 2;
            for (int i = 24; i < 28124; i++)
            {
                if (!abundantSums.Contains(i))
                {
                    answer += i;
                }
            }
            Console.WriteLine(answer);
        }

        private static bool IsAbundantNumbers(int i)
        {
            return SumOfProperDivisor(i) > i;
        }
    }
}
